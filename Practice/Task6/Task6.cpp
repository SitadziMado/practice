#include "stdafx.h"

#include "Sequence.h"

void test();

using std::cin;
using std::cout;

int main()
{
    test();

    ::setlocale(LC_ALL, "Russian");
    cout << "Введите три действительных числа a[1], a[2], a[3] и число M - лимит последовательности: ";

    double first, second, third, m;

    cin >> first >> second >> third >> m;

    auto seq = Sequence<double>(first, second, third);
    int i = -1;
    double cur;

    cout << "a[k] = a[k - 1] * a[k - 2] + a[k - 3]" << std::endl;
    cout << std::endl << "Последовательность: ";

    while ((cur = seq[++i]) < m)
        cout << cur << " ";

    cout << cur << " ";

    const int n = (i + 1);

    cout << std::endl << "Значение N: " << n << std::endl;
    cout << std::endl << "a[N] ==  " << ((cur == m) ? ("истина") : ("ложь")) << std::endl;

    return 0;
}

void test()
{
    auto seq = Sequence<double>(2., 3., 5.);

    auto e13 = seq[13];
    auto e21 = seq[21];
}