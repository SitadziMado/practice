#include "GameTree.h"

GameTree::GameTree(std::unique_ptr<Node>&& root) :
	m_root(std::move(root))
{
	assert(m_root.get() != nullptr);
}

GameTree&& GameTree::makeGameTree(const std::string& gameTreeString)
{
	auto s = std::stringstream(gameTreeString);
	auto nodes = std::vector<Node*>({ new Node(1, NodeType::Inner) });
	int count = 1, n, parent, winner;
	char type;
	NodeType nodeType;

	s >> n;
	assert(n > 0);

	for (int i = 0; i < n; ++i)
	{
		s >> type;

		switch (type)
		{
		case 'N':
			s >> parent;
			nodeType = NodeType::Inner;
			break;

		case 'L':
			s >> parent;
			s >> winner;

			assert(winner >= -1 && winner <= +1);

			if (winner == +1)
				nodeType = NodeType::FirstLeaf;
			if (winner == -1)
				nodeType = NodeType::SecondLeaf;
			else // if (winner == 0)
				nodeType = NodeType::DrawLeaf;

			break;

		default:
			assert(false);
		}

		assert(parent > 0);

		auto n = new Node(++count, nodeType);
		nodes[parent]->add(std::make_unique<Node>(n));
		nodes.push_back(n);
	}

	return GameTree(std::make_unique<Node>(nodes[0]));
}

GameResult GameTree::play() const
{
	auto s = std::stack<Node*>();
	GameResult rc = GameResult::SecondWin;

	s.push(m_root.get());

	while (rc != GameResult::FirstWin && s.size() > 0)
	{
		auto* p = s.top();
		s.pop();

		for (int i = 0; i < p->size(); ++i)
		{

		}
	}

	return rc;
}
