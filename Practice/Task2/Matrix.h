#pragma once

#include <string>
#include <sstream>
#include <utility>
#include <vector>

#include <assert.h>

template <typename T>
class MatrixRow
{
public:
    MatrixRow() : m_columns(nullptr), m_width(0) {}
    MatrixRow(std::size_t width);
    MatrixRow(std::size_t width, T const* data);
    ~MatrixRow();
    MatrixRow(const MatrixRow<T> & rhs);
    MatrixRow<T>& operator=(const MatrixRow<T>& rhs);
    MatrixRow(MatrixRow<T> && rhs);
    MatrixRow<T>& operator=(MatrixRow<T>&& rhs);

    T& operator[](std::size_t index);
    T& operator[](std::size_t index) const;

private:
    T* m_columns = nullptr;
    std::size_t m_width;
};

template <typename T>
class Matrix
{
public:
    Matrix(std::size_t width, std::size_t height);
    Matrix(std::size_t width, std::size_t height, T const* data);
    ~Matrix();
    Matrix(const Matrix<T>& rhs);
    Matrix<T>& operator=(const Matrix<T>& rhs);
    Matrix(Matrix<T>&& rhs);
    Matrix<T>& operator=(Matrix<T>&& rhs);

    MatrixRow<T>& operator[](std::size_t index);
    MatrixRow<T>& operator[](std::size_t index) const;
    Matrix<T> operator*=(const Matrix<T>& rhs);

private:
    Matrix(std::size_t width, std::size_t height, MatrixRow<T> const* data);

    MatrixRow<T>* m_rows = nullptr;
    std::size_t m_width;
    std::size_t m_height;
};

template<typename T>
inline Matrix<T>::Matrix(std::size_t width, std::size_t height)
{
    assert(width > 0 && height > 0);

    m_width = width;
    m_height = height;

    m_rows = new MatrixRow<T>[m_height];

    for (std::size_t i = 0; i < m_height; ++i)
        m_rows[i] = MatrixRow<T>(m_width);
}

template<typename T>
inline Matrix<T>::Matrix(std::size_t width, std::size_t height, T const* data) :
    Matrix<T>(width, height)
{
    // assert(data.size() == m_width * m_height);

    for (std::size_t i = 0; i < m_height; ++i)
        for (std::size_t j = 0; j < m_width; ++j)
            m_rows[i][j] = data[j + i * m_width];
}

template<typename T>
inline Matrix<T>::~Matrix()
{
    delete[] m_rows;
    m_rows = nullptr;
}

template<typename T>
inline Matrix<T>::Matrix(const Matrix<T>& rhs)
{
    this->~Matrix<T>();
    *this = Matrix<T>(rhs.m_width, rhs.m_height, rhs.m_rows);
}

template<typename T>
inline Matrix<T>& Matrix<T>::operator=(const Matrix<T>& rhs)
{
    this->~Matrix<T>();
    return *this = Matrix<T>(rhs.m_width, rhs.m_height, rhs.m_rows);
}

template<typename T>
inline Matrix<T>::Matrix(Matrix<T>&& rhs)
{
    this->~Matrix<T>();
    m_width = rhs.m_width;
    m_height = rhs.m_height;
    m_rows = rhs.m_rows;
    rhs.m_rows = nullptr;
}

template<typename T>
inline Matrix<T>& Matrix<T>::operator=(Matrix<T>&& rhs)
{
    this->~Matrix<T>();
    m_width = rhs.m_width;
    m_height = rhs.m_height;
    m_rows = rhs.m_rows;
    rhs.m_rows = nullptr;
    return *this;
}

template<typename T>
inline MatrixRow<T>& Matrix<T>::operator[](std::size_t index)
{
    assert(index >= 0 && index < m_width);

    return m_rows[index];
}

template<typename T>
inline MatrixRow<T>& Matrix<T>::operator[](std::size_t index) const
{
    assert(index >= 0 && index < m_width);

    return m_rows[index];
}

template<typename T>
inline Matrix<T> Matrix<T>::operator*=(const Matrix<T>& rhs)
{
    assert(m_width == rhs.m_height);

    Matrix<T> result = Matrix<T>(m_width, rhs.m_height);
    auto size = m_width;

    for (std::size_t i = 0; i < rhs.m_height; ++i)
        for (std::size_t j = 0; j < m_width; ++j)
        {
            T sum = T();
            for (std::size_t k = 0; k < size; ++k)
                sum += (*this)[i][k] * rhs[k][j];
            result[i][j] = sum;
        }

    return result;
}

template<typename T>
inline Matrix<T>::Matrix(std::size_t width, std::size_t height, MatrixRow<T> const* data) :
    Matrix<T>(width, height)
{
    for (std::size_t i = 0; i < m_height; ++i)
        for (std::size_t j = 0; j < m_width; ++j)
            m_rows[i][j] = data[i][j];
}

template<typename T>
inline MatrixRow<T>::MatrixRow(std::size_t width)
{
    assert(width > 0);

    m_width = width;
    m_columns = new T[m_width];
}

template<typename T>
inline MatrixRow<T>::MatrixRow(std::size_t width, T const* data) :
    MatrixRow<T>(width)
{
    for (std::size_t i = 0; i < m_width; ++i)
        m_columns = data[i];
}

template<typename T>
inline MatrixRow<T>::~MatrixRow()
{
    delete[] m_columns;
    m_columns = nullptr;
}

template<typename T>
inline MatrixRow<T>::MatrixRow(const MatrixRow<T>& rhs)
{
    this->~MatrixRow<T>();
    *this = MatrixRow<T>(rhs.m_width, rhs.m_columns);
}

template<typename T>
inline MatrixRow<T>& MatrixRow<T>::operator=(const MatrixRow<T>& rhs)
{
    this->~MatrixRow<T>();
    return *this = MatrixRow<T>(rhs.m_width, rhs.m_columns);
}

template<typename T>
inline MatrixRow<T>::MatrixRow(MatrixRow<T>&& rhs)
{
    this->~MatrixRow<T>();
    m_width = rhs.m_width;
    m_columns = rhs.m_columns;
    rhs.m_columns = nullptr;
}

template<typename T>
inline MatrixRow<T>& MatrixRow<T>::operator=(MatrixRow<T>&& rhs)
{
    this->~MatrixRow<T>();
    m_width = rhs.m_width;
    m_columns = rhs.m_columns;
    rhs.m_columns = nullptr;
    return *this;
}

template<typename T>
inline T& MatrixRow<T>::operator[](std::size_t index)
{
    assert(index >= 0 && index < m_width);

    return m_columns[index];
}

template<typename T>
inline T& MatrixRow<T>::operator[](std::size_t index) const
{
    assert(index >= 0 && index < m_width);

    return m_columns[index];
}
