#include <algorithm>
#include <iostream>
#include <fstream>
#include <vector>

#include "Matrix.h"

#define HOME

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

		matrices.push_back(IntMatrix(size, size, data.data()));
		data.clear();
	}

	IntMatrix result = std::move(matrices[0]);

	std::for_each(++matrices.cbegin(), matrices.cend(), [&result](auto& m)
	{
		result *= m;
	});

	outs << result[i][j];
}