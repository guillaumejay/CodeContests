// Asobo-2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "math.h"
#include <string.h>
//#include "LinkedDatas.h"
#include <iostream>
using namespace std;
#define	SIZE_X 100
#define	SIZE_Y 200

#define	IS_FREE		0
#define	IS_WALL		1
#define	IS_TREE		2
#define	IS_WATER	3
#define	IS_ENEMY	4
#define	IS_EXIT		5

class TheWorld
{
public:
	int	Cell[SIZE_X][SIZE_Y];
};

class Vec2i
{
public:
	int	x;
	int	y;
};

int MyFunction1(int *pDatas, int Size)
{
	for (int i = 0; i < Size; i++)
	{
		if (pDatas[i] != ((pDatas[i] / 2) * 2))
		{
			memmove(pDatas + i, pDatas + i + 1, (Size - i - 1)*sizeof(int));
			Size--;
			i--;
		}
	}

	return Size;
}
int MyFunction(int *pDatas, int Size)
{
	int nbEven = Size;
	int indexEven = 0;
	for (int i = 0; i < Size; i++)
	{
		if ((pDatas[i] % 2==0) )
		{
			pDatas[indexEven++] = pDatas[i];		
		}	
		else
		{
			nbEven--;
		}
	}
	return nbEven;
}
int MyFunction2(char *_text)
{
	char  *pTextTp;
	pTextTp = new char[2];
	int         retVal = 0;
	for (unsigned int i = 0; i < strlen(_text); i++)
	{
		pTextTp[0] = _text[i];
		pTextTp[1] = 0;
		if (atoi(pTextTp) > 0)
		{
			retVal = retVal + atoi(pTextTp);
		}
	}
	delete pTextTp;
	return retVal;
}

int MyFunction2O(char *_text)
{
	int retVal = 0;
	while (*_text)
	{
		if (*_text >= 48 && *_text <= 57)
		{
			retVal += (*_text - 48);
		}
		_text++;
	}
	return retVal;
}
char *MyFunction4(char* pText)
{
	int size = strlen(pText) + 1;
	char *pDest = (char *)malloc(size);
	for (int i = 0; i <= size; i++)
	{
		*(pDest + i) = *(pText + i);
	}
	return pDest;
}
int	RemoveDuplicate(int *Tab, int Size)
{
	int nbElement = Size;
	int i = 0;
	while (i < nbElement)

	{
		int j = i + 1;

		while (j < nbElement)
		{
			if (Tab[i] == Tab[j])
			{ // this could be done faster without the inplace requirement
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
	return nbElement;
}
bool IsCellBlocked(int cellType)
{
	return (cellType == IS_WALL || cellType == IS_TREE);
}
bool	CanShootEnemy(TheWorld *pMesh, Vec2i &PosPlayer)
{
	int cellType;
	for (int i = PosPlayer.x - 1; i >= 0; i--)
	{
		cellType = pMesh->Cell[i][PosPlayer.y];
		if (IsCellBlocked(cellType))
		{
			break;
		}
		if (cellType == IS_ENEMY)
		{
			return true;
		}
	}
	for (int i = PosPlayer.x + 1; i < SIZE_X; i++)
	{
		cellType = pMesh->Cell[i][PosPlayer.y];
		if (IsCellBlocked(cellType))
		{
			break;
		}
		if (cellType == IS_ENEMY)
		{
			return true;
		}
	}
	for (int i = PosPlayer.y - 1; i >= 0; i--)
	{
		cellType = pMesh->Cell[PosPlayer.x][i];
		if (IsCellBlocked(cellType))
		{
			break;
		}
		if (cellType == IS_ENEMY)
		{
			return true;
		}
	}
	for (int i = PosPlayer.y + 1; i < SIZE_Y; i++)
	{
		cellType = pMesh->Cell[PosPlayer.x][i];
		if (IsCellBlocked(cellType))
		{
			break;
		}
		if (cellType == IS_ENEMY)
		{
			return true;
		}
	}
	return false;
}


// I'm assuming there's only one exit
int		CanReachExit(TheWorld *pMesh, Vec2i &PosPlayer)
{
	int	distances[SIZE_X][SIZE_Y];
	// init distance map
	for (int x = 0; x < SIZE_X; x++)
	{
		for (int y = 0; y < SIZE_Y; y++)
		{
			int cellType = pMesh->Cell[x][y];
			switch (cellType)
			{
			case IS_EXIT:
				distances[x][y] = 0;
				break;
			case IS_FREE:
			case IS_ENEMY:
				distances[x][y] = -1;
				break;
			default:
				distances[x][y] = -2;
			}
		}
	}
	int currentDistance = 0;
	bool stillSearching;
	do
	{
		stillSearching = false;
		for (int x = 0; x < SIZE_X; x++)
		{
			for (int y = 0; y < SIZE_Y; y++)
			{
				int currentCell = distances[x][y];
				if (currentCell == currentDistance)
				{
					stillSearching = true;
					if (x>0 && distances[x - 1][y] == -1)
					{// LEFT
						distances[x - 1][y] = currentDistance + 1;
					}
					if ((x < (SIZE_X - 1)) && (distances[x + 1][y] == -1))
					{// RIGTH
						distances[x + 1][y] = currentDistance + 1;
					}
					if ((y > 0) && (distances[x][y - 1] == -1))
					{ // BOTTOM
						distances[x][y - 1] = currentDistance + 1;
					}
					if ((x < (SIZE_Y - 1)) && (distances[x][y + 1] == -1))
					{ // TOP
						distances[x][y + 1] = currentDistance + 1;
					}
				}
			}
		}
		currentDistance++;
	} while (stillSearching);

	return distances[PosPlayer.x][PosPlayer.x] >= 0; // = just in case player already on exit
}

int _tmain(int argc, _TCHAR* argv[])
{
	int tab[5] = { 1, 4,2,3,1 };//, 4, 1};
	int size = MyFunction1(tab, 5);
	cout << size << "\r\n";
	int tab2[5] = { 1, 4,2 ,3,1};//, 4, 1};
	int size2 = MyFunction(tab2, 5);
	cout << size2 << "\r\n";
	//char *Test = "01289";
	//int result = MyFunction2O(Test);
	//cout << result << "\r\n";
	//char *text = "TOsaasOT";
	//cout << MyFunction4(text);
	//int	Tab[] = { 10, 1, 2, 1, 1, 3, 5, 2, 6, 3, 7, 8, 5, 10 };

	//_ASSERT(RemoveDuplicate(Tab, 14) == 8);
	return 0;
}

