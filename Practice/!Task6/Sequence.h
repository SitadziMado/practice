#pragma once

#include <initializer_list>
#include <vector>

template<typename T, class TContainer = std::vector<T>>
class Sequence
{
public:
    Sequence(T first, T second, T third);
    // ~Sequence();

    const T& operator[](std::size_t index) const;

    const T& recursiveAt(std::size_t index) const;

private:
    const T& recursiveAtImpl(std::size_t index) const;

    mutable TContainer data_;
};

template<typename T, class TContainer>
inline Sequence<T, TContainer>::Sequence(T first, T second, T third) :
    data_({ first, second, third })
{
}

template<typename T, class TContainer>
inline const T& Sequence<T, TContainer>::operator[](std::size_t index) const
{
    while (index >= data_.size())
    {
        // Кэшируем.
        auto last = data_.end() - 1;
        auto preLast = last - 1;
        auto prePreLast = preLast - 1;

        // Генерируем очередной член последовательности.
        auto newElement = (*last) * (*preLast) + (*prePreLast);

        // Копируем новый элемент в контейнер.
        data_.push_back(newElement);
    }

    return *(data_.cbegin() + index);
}