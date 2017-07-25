/*#include <algorithm>
#include <iostream>
#include <fstream>
#include <vector>

#include "Matrix.h"

// #define HOME

int main(int argc, char** argv)
{
	using IntMatrix = Matrix<int>;
	std::vector<int> data;
	std::vector<IntMatrix> matrices;

#ifdef HOME
#define ins std::cin
#define outs std::cout
#else
    auto ins = std::ifstream("input.txt");
    auto outs = std::ofstream("output.txt");
#endif // HOME

	int matrixCount, size;
	int i, j;
	int modulo;

	ins >> matrixCount >> size >> i >> j >> modulo;
	
	for (int m = 0; m < matrixCount; ++m)
	{
		for (int d = 0; d < size * size; ++d)
		{
			int cell;
			ins >> cell;
			data.push_back(cell);
		}

		matrices.push_back(IntMatrix(size, size, data));
		data.clear();
	}

	IntMatrix result = matrices[0];

    if (matrices.size() > 1)
    {
        std::for_each(++matrices.begin(), matrices.end(), [&result, modulo](const IntMatrix& m)
        {
            result = result * m;
            result = result % modulo;
        });
    }

	outs << result[--i][--j] % modulo;
}*/

#include <stdlib.h>
#include <stdio.h>

#define MATRIX_SIZE 200
#define MATRIX_COUNT 200

typedef int *MATRIX, **PMATRIX;

MATRIX makematrix(int size)
{
    return (MATRIX)malloc(sizeof(int) * MATRIX_SIZE * MATRIX_SIZE);
}

MATRIX readmatrix(MATRIX dst, int size)
{
    int i;

    for (i = 0; i < size * size; ++i)
        scanf("%d", &dst[i]);

    return dst;
}

MATRIX mulmodij(MATRIX dst, MATRIX src, int size, int modulo, int row, int col)
{
    int i, j, k;
    int sum;
    MATRIX result = makematrix(size);

    i = row;
    const int ioff = i * size;
    // j = col;
    // for (i = 0; i < size; ++i)
    for (j = 0; j < size; ++j)
    {
        sum = 0;
        for (k = 0; k < size; ++k)
        {
            sum += dst[ioff + k] * src[k * size + j];
        }

        result[ioff + j] = sum;
    }

    // for (i = 0; i < size; ++i)
    for (j = 0; j < size; ++j)
    {
        int index = i * size + j;
        dst[index] = result[index] % modulo;
    }

    return dst;
}

int main()
{
    MATRIX first, next;
    int matrixCount, size;
    int i, j;
    int modulo;
    int x;

    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);

    scanf("%d %d %d %d %d", &matrixCount, &size, &i, &j, &modulo);
    --i, --j;

    first = makematrix(size);
    next = makematrix(size);
    readmatrix(first, size);

    for (x = 1; x < matrixCount; ++x)
    {
        readmatrix(next, size);
        mulmodij(first, next, size, modulo, i, j);
    }

    printf("%d", first[i * size + j] % modulo);

    free(first);
    free(next);
}