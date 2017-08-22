#include "stdafx.h"

#include "Sequence.h"

void test();

using std::cin;
using std::cout;

int main()
{
    test();

    ::setlocale(LC_ALL, "Russian");
    cout << "������� ��� �������������� ����� a[1], a[2], a[3] � ����� M - ����� ������������������: ";

    double first, second, third, m;

    cin >> first >> second >> third >> m;

    auto seq = Sequence<double>(first, second, third);
    int i = -1;
    double cur;

    cout << "a[k] = a[k - 1] * a[k - 2] + a[k - 3]" << std::endl;
    cout << std::endl << "������������������: ";

    while ((cur = seq[++i]) < m)
        cout << cur << " ";

    cout << cur << " ";

    const int n = (i + 1);

    cout << std::endl << "�������� N: " << n << std::endl;
    cout << std::endl << "a[N] ==  " << ((cur == m) ? ("������") : ("����")) << std::endl;

    return 0;
}

void test()
{
    auto seq = Sequence<double>(2., 3., 5.);

    auto e13 = seq[13];
    auto e21 = seq[21];
}