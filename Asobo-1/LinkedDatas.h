#pragma once
class	LinkedDatas
{
public:
	void		*Datas;
	LinkedDatas	*pNext;
};
class	LinkedDatasMgr
{
protected:
	LinkedDatas	*pHead;

public:
	LinkedDatasMgr();
	~LinkedDatasMgr();

	void Add(void *pDatas);
	
	void *ExtractFirst();

	void *ExtractLast();
	
};