// ConsoleApplication5.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "math.h"
#include <string.h>
#include "LinkedDatas.h"
#include <iostream>
using namespace std;
float Test(float x, float a[6])
{
	float result = a[0] + a[1] * x;
	// I need a profiling tool to check if I shold continue to unloop thie
	for (int i = 2; i < 6; i++)
	{
		result = result + a[i] * pow(x, i);
	}

	return  result;
}
void DisplayText(char *text)
{
	cout << text << "\r\n";
}
void	MyFunction(char *Text)
{
	char *Start = Text;


	while (*Start != 0)
	{
		while (*Start == ' ')
		{
			Start++;
		}

		//int i = 0;
		char *End = Start;
		while ((*End != 0) && (*End != ' '))
		{
			End++;
		}
		char c = *(End);
		*End = '\0';
		/*while ((Start[i] != 0) && (Start[i] != ' '))
			i++;
			char *End = Start + i;
			memcpy(Buffer, Start, i);
			Buffer[i] = 0;*/
		//DisplayText(Buffer);	// Display text is provided and can't be optimized.
		DisplayText(Start);	// Display text is provided and can't be optimized.
		*(End) = c;
		Start = End;
	}

}

void	MyFunction2(char *Text)
{
	char *Start = Text;
	int length = strlen(Text);
	char *Buffer = new char[length + 1];

	while (*Start != 0)
	{
		while (*Start == ' ')
		{
			Start++;
		}

		int i = 0;

		while ((Start[i] != 0) && (Start[i] != ' '))
			i++;
		memcpy(Buffer, Start, i);
		Buffer[i] = 0;
		DisplayText(Buffer);	// Display text is provided and can't be optimized.

		Start += i;
	}
	delete[] Buffer;
}
int	RemoveDuplicate(int *Tab, int Size)
{
	int nbElement = Size;
	int i = 0;
	while (i<nbElement)
	
	{
		int j      = i + 1;
	
		while (j < nbElement)
			{
				if (Tab[i] == Tab[j])
				{
					for (int k = j + 1; k < nbElement; k++)
					{
						Tab[k - 1] = Tab[k];
					}
					nbElement--;
				}
				else
				{
					j++;
				}
			}
		i++;
	}
	/*int t1, t2;
	t1 = 0; t2 = 1;
	 while (t1<Size)
	{ 
		 if (Tab[t1] == -1)
		 {
			 if (t2 < Size-1)
			 {

				 Tab[t1] = Tab[t2++];
			 }
		 }
		
		else
		{
			t1++;
			t2 = t1 + 1;
			
		}
	 }*/
	return nbElement;
}
//http://stackoverflow.com/questions/5883169/intersection-between-a-line-and-a-sphere

int _tmain(int argc, _TCHAR* argv[])
{
	//float a[6] = { 2, 4, 5, 6, 7, 8 };
	//float r = Test(4, a);
	//cout << r << "\r\n";
	//_ASSERT(r == 10466);
	// r = Test(3,a);

	//cout << r;
	//_ASSERT(r == 2732);
	//char *test  = new[]
	//char str[] = "Test Chaque Mot";
	//MyFunction2(str);
	/*
	LinkedDatasMgr lm;
	lm.Add("ete");
	lm.Add("toto");
	void *first = lm.ExtractFirst();
	first = lm.ExtractFirst();
	lm.Add("ete");
	lm.Add("toto");
	first = lm.ExtractLast();
	*/
	int	Tab[] = { 10, 1, 2, 1, 1, 3, 5, 2, 6, 3, 7, 8, 9, 10 };

	_ASSERT(RemoveDuplicate(Tab, 14) == 9);
	return 0;


}

