#pragma once

#include <memory>
#include <sstream>
#include <stack>
#include <utility>
#include <vector>

#include "Node.h"

enum class GameResult
{
	FirstWin,
	SecondWin,
	Draw,
};

class GameTree
{
public:
	~GameTree() = default;
	
	static GameTree&& makeGameTree(const std::string& gameTreeString);
	
	GameResult play() const;

private:
	GameTree(std::unique_ptr<Node>&& root);

	std::unique_ptr<Node> m_root;
};