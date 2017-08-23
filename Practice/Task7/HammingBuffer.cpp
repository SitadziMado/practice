#include "stdafx.h"
#include "HammingBuffer.h"

HammingBuffer::HammingBuffer(const TContainer& rawBuffer)
{
    TContainer temp(rawBuffer.cbegin(), rawBuffer.cend());
    auto tempControl = std::vector<bool>();

    std::size_t size = temp.size();
    
    // јппроксимируем количество контрольных битов.
    std::size_t controlBitCount = std::log2(size);

    // ƒоводим количество контрольных битов до четного числа.
    while (controlBitCount >= std::log2(controlBitCount + size + 1))
        ++controlBitCount;

    std::size_t increment(1U);

    for (std::size_t i = 0U; i < controlBitCount; ++i)
    {
        std::size_t offset(increment - 1);

        for (size_t j = 0; j < size * ElementBitSize; ++j)
        {

        }
    }
}