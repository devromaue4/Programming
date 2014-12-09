#include <iostream>
//#include <algorithm> 
using namespace std;

class TestCls
{};

template <class T> T swap(T x, T y)
{
	return x + y;
}


int main()
{

	int i = swap(2, 2);
	float f = swap(20.0f, 2.0f);
	double d = swap(5.0, 10.0);

	cout << i << endl;
	cout << f << endl;
	cout << d << endl;

	system("PAUSE");

	return 0;
}