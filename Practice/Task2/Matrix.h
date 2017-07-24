#pragma once

#include <algorithm>
#include <initializer_list>
#include <type_traits>
#include <utility>
#include <string>
#include <vector>

#include <assert.h>

template <typename T>
class Matrix;

template <typename T>
class MatrixRow;

template <typename T>
Matrix<T> operator*(const Matrix<T>& lhs, const Matrix<T>& rhs)
{
    assert(lhs.m_height == rhs.m_width);
    auto size = lhs.m_height;
    Matrix<T> result = Matrix<T>(rhs.m_width, lhs.m_height);

    for (int i = 0; i < lhs.m_height; ++i)
        for (int j = 0; j < rhs.m_width; ++j)
        {
            T sum = T();
            for (int k = 0; k < size; ++k)
            {
                sum += lhs[i][k] * rhs[k][j];
            }

            result[i][j] = sum;
        }

    return std::move(result);
}

template <typename T>
Matrix<T> operator%(const Matrix<T>& lhs, T divider)
{
    Matrix<T> result = Matrix<T>(lhs);

    for (int i = 0; i < lhs.m_height; ++i)
        for (int j = 0; j < lhs.m_width; ++j)
            result[i][j] %= divider;

    return std::move(result);
}

template <typename T>
class Matrix
{
public:
    Matrix(int width, int height);
    Matrix(int width, int height, T init[]);
    Matrix(int width, int height, std::vector<T>& init);
    Matrix(int width, int height, std::initializer_list<T> init);
    Matrix(int width, int height, MatrixRow<T> init[]);
    ~Matrix();
    Matrix(const Matrix<T>& rhs);
    Matrix<T>& operator=(const Matrix<T>& rhs);
    Matrix(Matrix<T>&& rhs);
    Matrix<T>& operator=(Matrix<T>&& rhs);

    const MatrixRow<T>& operator[](int index) const;
    MatrixRow<T>& operator[](int index);
    const T& operator()(int i, int j) const;
    T& operator()(int i, int j);

    std::string toString() const;

    friend Matrix<T> operator*<>(const Matrix<T>& lhs, const Matrix<T>& rhs);
    friend Matrix<T> operator%<>(const Matrix<T>& lhs, T divider);

private:
    int m_width;
    int m_height;
    MatrixRow<T>* m_rows = nullptr;
};

// using T = int;
template <typename T>
class MatrixRow
{
public:
    MatrixRow() {};
    MatrixRow(int width);
    MatrixRow(int width, T init[]);
    MatrixRow(int width, std::vector<T>& init);
    ~MatrixRow();
    MatrixRow(const MatrixRow<T>& rhs);
    MatrixRow<T>& operator=(const MatrixRow<T>& rhs);
    MatrixRow(MatrixRow<T>&& rhs);
    MatrixRow<T>& operator=(MatrixRow<T>&& rhs);

    const T& operator[](int index) const;
    T& operator[](int index);

private:
    void internalInit(T init[]);

    int m_width;
    T* m_data = nullptr;
};

template<typename T>
inline MatrixRow<T>::MatrixRow(int width) :
    m_width(width), m_data(new T[width])
{
    assert(m_width > 0);
}

template<typename T>
inline MatrixRow<T>::MatrixRow(int width, T init[]) :
    MatrixRow(width)
{
    assert(m_width > 0);
    m_data = new T[width];
    auto p = init;
    std::advance(p, width);
    std::copy(init, p, m_data);
    // internalInit(init.data());
}

template<typename T>
inline MatrixRow<T>::MatrixRow(int width, std::vector<T>& init) :
    MatrixRow(width, init.data())
{
    // internalInit(init.data());
}

template<typename T>
inline MatrixRow<T>::~MatrixRow()
{
    // if (m_data != nullptr)
    delete[] m_data;
}

template<typename T>
inline MatrixRow<T>::MatrixRow(const MatrixRow<T>& rhs)
{
    this->operator=(rhs);
}

template<typename T>
inline MatrixRow<T>& MatrixRow<T>::operator=(const MatrixRow<T>& rhs)
{
    this->~MatrixRow<T>();

    return *this = MatrixRow<T>(rhs.m_width, rhs._mdata);
}

template<typename T>
inline MatrixRow<T>::MatrixRow(MatrixRow<T>&& rhs)
{
    this->operator=(rhs);
}

template<typename T>
inline MatrixRow<T>& MatrixRow<T>::operator=(MatrixRow<T>&& rhs)
{
    this->~MatrixRow<T>();

    m_width = rhs.m_width;
    m_data = rhs.m_data;
    rhs.m_data = nullptr;

    return *this;
}

template<typename T>
inline const T & MatrixRow<T>::operator[](int index) const
{
    assert(index >= 0 && index < m_width);
    return m_data[index];
}

template<typename T>
inline T & MatrixRow<T>::operator[](int index)
{
    assert(index >= 0 && index < m_width);
    return m_data[index];
}

template<typename T>
inline void MatrixRow<T>::internalInit(T init[])
{
    assert(m_width > 0);
    auto p = init;
    std::advance(p, m_width);
    std::copy(init, p, m_data);
}

template<typename T>
inline Matrix<T>::Matrix(int width, int height) :
    m_width(width), m_height(height)
{
    assert(width > 0 && height > 0);
    m_rows = new MatrixRow<T>[m_height];

    for (int i = 0; i < m_height; ++i)
        m_rows[i] = std::move(MatrixRow<T>(m_width));
}

template<typename T>
inline Matrix<T>::Matrix(int width, int height, T init[]) :
    m_width(width), m_height(height)
{
    assert(width > 0 && height > 0);
    m_rows = new MatrixRow<T>[m_height];

    for (int i = 0; i < m_height; ++i)
    {
        std::advance(init, i * m_width);
        m_rows[i] = MatrixRow<T>(m_width, init);
    }
}

template<typename T>
inline Matrix<T>::Matrix(int width, int height, std::vector<T>& init) :
    Matrix(width, height, init.data())
{
}

template<typename T>
inline Matrix<T>::Matrix(int width, int height, std::initializer_list<T> init) :
    Matrix<T>(width, height)
{
    int i = 0, j = 0;

    for (auto a : init)
    {
        m_rows[i][j++] = a;

        if (j >= m_width)
            j = 0, ++i;
    }
}

template<typename T>
inline Matrix<T>::Matrix(int width, int height, MatrixRow<T> init[]) :
    Matrix<T>(width, height)
{
    for (int i = 0; i < m_height; ++i)
        for (int j = 0; j < m_width; ++j)
            m_rows[i][j] = init[i][j];
}

template<typename T>
inline Matrix<T>::~Matrix()
{
    // for (int i = 0; i < m_height; ++i)
    //	m_rows[i].~MatrixRow<T>();

    delete[] m_rows;
}

template<typename T>
inline Matrix<T>::Matrix(const Matrix<T>& rhs)
{
    this->operator=(rhs);
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
    this->operator=(rhs);
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
inline const MatrixRow<T>& Matrix<T>::operator[](int index) const
{
    assert(index >= 0 && index < m_height);
    return m_rows[index];
}

template<typename T>
inline MatrixRow<T>& Matrix<T>::operator[](int index)
{
    assert(index >= 0 && index < m_height);
    return m_rows[index];
}

template<typename T>
inline const T & Matrix<T>::operator()(int i, int j) const
{
    assert(i >= 0 && i < m_height);
    return m_rows[i][j];
}

template<typename T>
inline T & Matrix<T>::operator()(int i, int j)
{
    assert(i >= 0 && i < m_height);
    return m_rows[i][j];
}

template<typename T>
inline std::string Matrix<T>::toString() const
{
    auto result = std::string("{");

    for (int i = 0; i < m_height; ++i)
    {
        result.append("\n\t");

        for (int j = 0; j < m_width; ++j)
        {
            result.append(std::to_string((*this)[i][j]));
            result.append(", ");
        }

        result.pop_back();
        result.pop_back();
    }

    result.append("\n};\n\n");

    return std::move(result);
}