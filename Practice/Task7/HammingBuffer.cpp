#include "stdafx.h"
#include "HammingBuffer.h"

HammingBuffer::HammingBuffer(const TContainer& rawBuffer)
{
    TContainer temp(rawBuffer.cbegin(), rawBuffer.cend());
    auto tempControl = std::vector<bool>();

    std::size_t size = temp.size();
    
    // �������������� ���������� ����������� �����.
    std::size_t controlBitCount = std::log2(size);

    // ������� ���������� ����������� ����� �� ������� �����.
    while (controlBitCount >= std::log2(controlBitCount + size + 1))
        ++controlBitCount;

}