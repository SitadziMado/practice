#pragma once

#include <cmath>

#include <limits>
#include <type_traits>
#include <vector>
#include <utility>

class HammingBuffer
{
private:
    using T = char;
    using TContainer = std::vector<T>;

public:
    HammingBuffer(const TContainer& rawBuffer);
    // ~HammingBuffer();

    void swap(HammingBuffer rhs) { data_.swap(rhs.data_); }

private:
    bool isPowerOfTwo(unsigned int number) { return !(number & (number - 1U)); }

    constexpr static std::size_t ElementSize = sizeof(T);
    constexpr static std::size_t ElementBitSize = ElementSize * 8;

    std::size_t bitSize_;
    std::size_t controlBitCount_;
    TContainer data_;
    std::vector<bool> controlBits_;
};