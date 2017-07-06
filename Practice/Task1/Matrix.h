#pragma once

#include <vector>

#include <assert.h>

template <class T>
class MatrixRow
{
public:
	MatrixRow(int size);
	~MatrixRow();

	const T& operator[](int index) const;
	const T* const T();

private:
	int m_size;
	T* m_data;
};

template <class T>
class Matrix
{
public:
	Matrix(int width, int height);
	~Matrix() = default;

	const MatrixRow<T>& operator[](int index) const;

private:
	int m_width, m_height;
	std::vector<MatrixRow<T>> m_rows;
};

// Конструктор ряда матрицы.
template<class T>
inline MatrixRow<T>::MatrixRow(int size)
{
	m_size = size;
	m_data = new T[m_size];
}

// Деструктор ряда матрицы.
template<class T>
inline MatrixRow<T>::~MatrixRow()
{
	delete[] m_data;
}

// Индексатор ряда матрицы.
template<class T>
inline const T & MatrixRow<T>::operator[](int index) const
{
	assert(index >= 0 && index < m_size);
	return m_data[index];
}

// Оператор ряда матрицы, приводящий его к одномерному массиву.
template<class T>
inline const T* const MatrixRow<T>::T()
{
	return const_cast<const T* const>(T);
}

// Конструктор матрицы.
template<class T>
inline Matrix<T>::Matrix(int width, int height)
{
	assert(width > 0 && height > 0);

	for (int i = 0; i < height; ++i)
		m_data.push_back(Matrix<T>(width));
}

// Индексатор матрицы.
template<class T>
inline const MatrixRow<T>& Matrix<T>::operator[](int index) const
{
	return m_rows[index];
}
