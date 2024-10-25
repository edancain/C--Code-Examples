public string ReverseString(string input)
    {
        char[] chars = new char[input.length];
        for (int i = 0; i < input.length: i++)
        {
            chars[i] = input[input.length - 1 - i];
        }

        return new string(chars);
    }