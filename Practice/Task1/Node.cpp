#include "Node.h"

// Конструктор по умолчанию.
Node::Node(int id, NodeType type) :
	m_id(id), m_type(type)
{
	assert(id > 0);

	if (type == NodeType::FirstLeaf)
		m_weight = +1;
	else if (type == NodeType::SecondLeaf)
		m_weight = -1;
	else
		m_weight = 0;
}

const Node& Node::operator[](int index) const
{
	assert(index >= 0 && index < m_nodes.size());
	return *m_nodes[index].get();
}

bool Node::operator==(const Node & rhs) const
{
	return m_type == rhs.m_type;
}

bool Node::operator!=(const Node & rhs) const
{
	return !(*this == rhs);
}

bool Node::operator>(const Node& rhs) const
{
	if (m_type == rhs.m_type)
		return false;
	else if (m_type != NodeType::Inner && rhs.m_type != NodeType::Inner)
		return m_weight > rhs.m_weight;
	else if (m_type == NodeType::Inner)
		return false;
	else // if (rhs.m_type == NodeType::Inner)
		return true;
}

bool Node::operator<(const Node & rhs) const
{
	return !((*this > rhs) || (*this == rhs));
}

void Node::add(std::unique_ptr<Node>&& node)
{
	m_nodes.emplace_back(node);
}

const int Node::getId() const
{
	return m_id;
}

const NodeType Node::getNodeType() const
{
	return m_type;
}

int Node::size() const
{
	return m_nodes.size();
}
