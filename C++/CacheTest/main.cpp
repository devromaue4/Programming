#include "math.h"
#include "windows.h"
#include "stdio.h"
//#include <chrono>
#include <vector>
//#include <iostream>
//#include <mmintrin.h>

const int count = 1000000;
int *staticRefs[count];
int *dynamicRefs[count];

int staticArray[count];

int main()
{
	for (int i = 0; i < count; i++)
	{
		staticArray[i] = 0;
		staticRefs[i] = &staticArray[i];

		int *val = new int(0);
		dynamicRefs[i] = val;
	}

	int CountTest = 0;
	int startTime1 = GetTickCount();
	for (int j = 0; j < 100; j++)
	{
		for (int i = 0; i < count; i++)
		{
			(*staticRefs[i])++;
			CountTest++;
		}
	}
	printf("%d %d %d\n", *staticRefs[0], GetTickCount() - startTime1, CountTest);
	CountTest = 0;

	_mm_sfence();

	int startTime2 = GetTickCount();
	const size_t kItemsPerBucket = 4;
	_mm_prefetch(reinterpret_cast<const char*>(dynamicRefs), _MM_HINT_T0);
	for (int j = 0; j < 100; j++)
	{
		for (size_t i = 0; i < count / kItemsPerBucket; i += kItemsPerBucket)
		{
			_mm_prefetch(reinterpret_cast<const char*>(dynamicRefs) + i + kItemsPerBucket * sizeof(int), _MM_HINT_T0);

			for (size_t k = 0; k < kItemsPerBucket; ++k)
			{

				int* item = dynamicRefs[i + k];
				(*item)++;
				CountTest++;
			}			
		}
	}
	printf("%d %d %d\n", *dynamicRefs[40], GetTickCount() - startTime2, CountTest);
	CountTest = 0;

	_mm_sfence();

	int startTime3 = GetTickCount();
	const size_t kItemsPerBucket2 = 25;
	_mm_prefetch(reinterpret_cast<const char*>(dynamicRefs), _MM_HINT_T0);
	for (int j = 0; j < 100; j++)
	{
		for (size_t i = 0; i < count / kItemsPerBucket2; i += kItemsPerBucket2)
		{
			_mm_prefetch(reinterpret_cast<const char*>(dynamicRefs) + i + kItemsPerBucket2 * sizeof(int), _MM_HINT_T0);
			for (size_t k = 0; k < kItemsPerBucket2; ++k)
			{
				(*dynamicRefs[i + k])++;
				CountTest++;
			}	
		}
	}
	printf("%d %d %d\n", *dynamicRefs[100], GetTickCount() - startTime3, CountTest);

	system("PAUSE");
}