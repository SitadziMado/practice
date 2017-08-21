#include <algorithm>
#include <iostream>
#include <stack>
#include <string>
#include <vector>

#include <cassert>
#include <cstdio>

enum class Winner
{
    FirstPlayer = +1,
    SecondPlayer = -1,
    Draw = 0,
    Noone = -2,
};

struct Node
{
    Node(Winner _winner) : winner(_winner){  }

    const Winner winner;
    std::vector<Node*> children = std::vector<Node*>();
};

Winner getWinner(std::vector<Node> nodes)
{
    int firstWins = 0, secondWins = 0, draws = 0;
    bool firstChoice = true;
    auto first = std::stack<Node*>();
    auto second = std::stack<Node*>();
    auto wins = std::vector<Winner>();

    first.push(&nodes[0]);

    do
    {
        bool pushedAny = false;

        if (firstChoice && !first.empty())
        {
            const auto& c = *first.top();
            first.pop();

            if (c.children.empty())
                wins.push_back(c.winner); // Записать выигрыш
            else
            {
                for (const auto a : c.children)
                {
                    if (a->winner != Winner::SecondPlayer)
                    {
                        second.push(a);
                        pushedAny = true;
                    }
                }

                if (!pushedAny)
                    second.push(c.children[0]);
            }
        }
        else if (!second.empty())
        {
            const auto& c = *second.top();
            second.pop();

            if (c.children.empty())
                wins.push_back(c.winner); // Записать выигрыш
            else
            {
                for (const auto a : c.children)
                {
                    if (a->winner != Winner::FirstPlayer)
                    {
                        first.push(a);
                        pushedAny = true;
                    }
                }

                if (!pushedAny)
                    first.push(c.children[0]);
            }
        }

        firstChoice = !firstChoice;
    } while (!first.empty() || !second.empty());
 
    return *std::max_element(wins.cbegin(), wins.cend());
}

#undef HOME

int main()
{
    char type;
    int count, parent;
    Winner winner;
    auto nodes = std::vector<Node>();

#ifndef HOME
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
#endif

    std::cin >> count;
    nodes.reserve(count);
    nodes.push_back(Node(Winner::Noone));

    for (size_t i = 1; i < count; ++i)
    {
        std::cin >> type >> parent;

        if (type == 'L')
            std::cin >> (int&)winner;
        else
            winner = Winner::Noone;

        nodes.emplace_back(Node(winner));
        nodes[parent - 1].children.push_back( &nodes[i] );
    }

    switch (getWinner(nodes))
    {
    case Winner::FirstPlayer:
        std::cout << "+1";
        break;

    case Winner::SecondPlayer:
        std::cout << "-1";
        break;

    case Winner::Draw:
        std::cout << "0";
        break;

    default:
        assert(0);
        break;
    }

    return 0;
}