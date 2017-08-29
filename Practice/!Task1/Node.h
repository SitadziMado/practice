#pragma once

#include <memory>
#include <vector>

#include <assert.h>

enum class NodeType
{
	Inner,
	FirstLeaf,
	SecondLeaf,
	DrawLeaf,
};

class Node
{
public:
	Node(int id, NodeType type);
	~Node() = default;

	const Node& operator[](int index) const;
	bool operator==(const Node& rhs) const;
	bool operator!=(const Node& rhs) const;
	bool operator>(const Node& rhs) const;
	bool operator<(const Node& rhs) const;

	void add(std::unique_ptr<Node>&& node);
	const int getId() const;
	const NodeType getNodeType() const;
	int size() const;

private:
	const int m_id;
	const NodeType m_type;
	int m_weight;
	std::vector<std::unique_ptr<Node>> m_nodes;
};