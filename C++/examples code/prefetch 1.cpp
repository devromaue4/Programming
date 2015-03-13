
#include <chrono>
#include <vector>
#include <iostream>

#include <DirectXMath.h>
using namespace DirectX;

#include <xmmintrin.h>

struct __declspec(align(16)) Test_OO_no_dirty
{
  bool dirty;
  XMMATRIX matrix;

  __forceinline void Update()
  {
    matrix = XMMatrixIdentity();
    matrix = XMMatrixMultiply(matrix, matrix);
#if 0
    matrix = XMMatrixMultiply(matrix, matrix);
    matrix = XMMatrixMultiply(matrix, matrix);
#endif
  }
};

struct __declspec(align(16)) Test_OO_dirty
{
  bool dirty;
  XMMATRIX matrix;

  Test_OO_dirty()
  {
    dirty = (rand()%1)!=0;
  }

  __forceinline void Update()
  {
    if (dirty) {
      matrix = XMMatrixIdentity();
      matrix = XMMatrixMultiply(matrix, matrix);
#if 0
      matrix = XMMatrixMultiply(matrix, matrix);
      matrix = XMMatrixMultiply(matrix, matrix);
#endif
    }
    dirty = !dirty;
  }
};

struct __declspec(align(16)) Test_DO_dirty
{
  bool dirty;
  XMMATRIX matrix;
  
  Test_DO_dirty()
  {
    dirty = (rand() % 1) != 0;
  }
  __declspec(noinline) static void Update(std::vector<Test_DO_dirty>& data)
  {
    int numItems = 0;

    const size_t kItemsPerBucket = 4;
    _mm_prefetch(reinterpret_cast<const char*>(data.data()), _MM_HINT_T0);

    for (size_t i = 0; i < data.size() / kItemsPerBucket; i += kItemsPerBucket) {

      _mm_prefetch(reinterpret_cast<const char*>(data.data()) + i + kItemsPerBucket * sizeof(Test_DO_dirty), _MM_HINT_T0);

      for (size_t j = 0; j < kItemsPerBucket; ++j) {

        Test_DO_dirty* item = &data[i + j];
        if (!item->dirty) {
          item->dirty = !item->dirty;
          continue;
        }
        else {
          numItems++;

          item->matrix = XMMatrixIdentity();
          item->matrix = XMMatrixMultiply(item->matrix, item->matrix);
#if 0
          item->matrix = XMMatrixMultiply(item->matrix, item->matrix);
          item->matrix = XMMatrixMultiply(item->matrix, item->matrix);
#endif
          item->dirty = !item->dirty;
        }
      }
    }

    std::cout << "dirty num items: " << numItems << std::endl;;
  }
};

struct __declspec(align(16)) Test_DO_no_dirty
{
  bool dirty;
  XMMATRIX matrix;

  __declspec(noinline) static void Update(std::vector<Test_DO_no_dirty>& data)
  {
    int numItems = 0;

    const size_t kItemsPerBucket = 4;
    _mm_prefetch(reinterpret_cast<const char*>(data.data()), _MM_HINT_T0);

    for (size_t i = 0; i < data.size() / kItemsPerBucket; i += kItemsPerBucket) {

      _mm_prefetch(reinterpret_cast<const char*>(data.data()) + i + kItemsPerBucket * sizeof(Test_DO_no_dirty), _MM_HINT_T0);

      for (size_t j = 0; j < kItemsPerBucket; ++j) {

        Test_DO_no_dirty* item = &data[i + j];

        item->matrix = XMMatrixIdentity();
        item->matrix = XMMatrixMultiply(item->matrix, item->matrix);
#if 0
        item->matrix = XMMatrixMultiply(item->matrix, item->matrix);
        item->matrix = XMMatrixMultiply(item->matrix, item->matrix);
#endif

        numItems++;
      }
    }

    std::cout << "no_dirty num items: " << numItems << std::endl;
  }
};

int main()
{
  const int itemCount = 20 * 1024 * 1024;
  {
    std::vector<Test_OO_no_dirty> testData;
    testData.resize(itemCount);

    auto tmStart = std::chrono::high_resolution_clock::now();
    for (auto & obj : testData)
    {
      obj.Update();
    }
    auto tmEnd = std::chrono::high_resolution_clock::now();

    std::cout << "Test_OO_no_dirty: " << std::chrono::duration_cast<std::chrono::duration<double>>(tmEnd - tmStart).count() << std::endl;
  }
  _mm_sfence();
  {
    std::vector<Test_OO_dirty> testData;
    testData.resize(itemCount);

    auto tmStart = std::chrono::high_resolution_clock::now();
    for (auto & obj : testData)
    {
      obj.Update();
    }
    auto tmEnd = std::chrono::high_resolution_clock::now();

    std::cout << "Test_OO_dirty: " << std::chrono::duration_cast<std::chrono::duration<double>>(tmEnd - tmStart).count() << std::endl;
  }
  _mm_sfence();
  {
    std::vector<Test_DO_dirty> testData;
    testData.resize(itemCount);

    auto tmStart = std::chrono::high_resolution_clock::now();
    Test_DO_dirty::Update(testData);
    auto tmEnd = std::chrono::high_resolution_clock::now();

    std::cout << "Test_DO_dirty: " << std::chrono::duration_cast<std::chrono::duration<double>>(tmEnd - tmStart).count() << std::endl;
  }
  _mm_sfence();
  {
    std::vector<Test_DO_no_dirty> testData;
    testData.resize(itemCount);

    auto tmStart = std::chrono::high_resolution_clock::now();
    Test_DO_no_dirty::Update(testData);
    auto tmEnd = std::chrono::high_resolution_clock::now();

    std::cout << "Test_DO_no_dirty: " << std::chrono::duration_cast<std::chrono::duration<double>>(tmEnd - tmStart).count() << std::endl;
  }

  int i;
  std::cin >> i;
  return 0;
}