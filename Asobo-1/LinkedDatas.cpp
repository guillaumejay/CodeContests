#include "stdafx.h"
#include "LinkedDatas.h"





	LinkedDatasMgr::LinkedDatasMgr() {}
	LinkedDatasMgr::~LinkedDatasMgr() {}

	void LinkedDatasMgr::Add(void *pDatas)
	{
		LinkedDatas *pElem = new LinkedDatas();

		pElem->pNext = pHead;
		pElem->Datas = pDatas;

		pHead = pElem;
	}

	void * LinkedDatasMgr::ExtractFirst()
	{
		LinkedDatas *pFirst = pHead;
		pHead = pFirst->pNext;

		return pFirst->Datas;
	}

	void * LinkedDatasMgr::ExtractLast()
	{
		LinkedDatas *pPrevPrev = 0;
		LinkedDatas *pPrev = 0;
		LinkedDatas *pFirst = pHead;

		while (pFirst)
		{
			pPrevPrev = pPrev;
			pPrev = pFirst;
			pFirst = pFirst->pNext;
		}

		if (!pPrev)
			return 0;

		if (pPrevPrev)
			pPrevPrev->pNext = 0;
		else
			pHead = 0;

		return pPrev->Datas;
	}



