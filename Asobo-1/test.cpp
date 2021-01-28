#include "stdafx.h"
#include "test.h"


class MyClassA
{
protected:
	int	a;
public:
	MyClassA()
	{
		a = 0;
	}

	void SetA(const int _val)
	{
		a = _val;
	}

	virtual void Process() = 0;
};

class MyClassB : public MyClassA
{
private:
	int	b;
public:
	MyClassB()
	{
		a = 1;
		b = 2;
	}

	void Process()
	{
		//do Something
	}
};

void MyFunction()
{
	MyClassB	toto;
	toto.Process();
}
