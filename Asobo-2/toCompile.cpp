#include "stdafx.h"

class MyClassA
{
protected: // was changed from private, so that NyClassB can access this field
	int	a;
public:
	MyClassA()
	{
		a = 0;
	}

	void SetA(const int _val) // removed const, since that's not logical with a Set function...
	{
		a = _val;
	}

	virtual void Process() = 0;
};

 class  MyClassB : public MyClassA
{
private:
	int	b;
public:
	MyClassB()
	{
		a = 1;
		b = 2;
	}

	void Process() // Implemented the Process function, so that MyClassB is no more Abtract
	{  
		// do something
	}
};

void MyFunction()
{
	MyClassB	toto;
	toto.Process();
}
