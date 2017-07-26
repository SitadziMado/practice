#include <iostream>
#include <stack>
#include <string>
#include <vector>

#include <cassert>
#include <cstdio>

enum class Winner
{
    FirstPU = +1,
    SecondPlayer = -1,
    Draw = 0,
    Noone = -2,
};

struct Node
{
    const Winner winner;
    std::vector<const Node*> children = std::vector<const Node*>();
};

int main()
{
    char type;
    int count, parent;
    Winner winner;
    auto nodes = std::vector<Node>();
    auto first = std::stack<Node&>();
    auto second = std::stack<Node&>();

#ifndef HOME
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
#endif

    std::cin >> count;
    nodes.reserve(count);

    for (size_t i = 0; i < count; ++i)
    {
        std::cin >> type >> parent;

        if (type == 'L')
            std::cin >> (int&)winner;
        else
            winner = Winner::Noone;

        nodes.emplace_back(const Node{ winner });
        nodes[parent - 1].children.push_back( &nodes[nodes.size() - 1] );
    }

    bool firstChoice = true;
    first.push(nodes[0]);

    do
    {
        if (firstChoice)
        {

        }
        else
        {

        }

        firstChoice = !firstChoice;
    } while (true);

    return 0;
}