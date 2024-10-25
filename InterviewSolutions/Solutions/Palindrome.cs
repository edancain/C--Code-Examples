/// Is a string a Palindrome i.e. can be read both ways.
    public bool IsPalindrome(string input)
    {
        input = input.ToLower();
        int left = 0;
        int right = input.length - 1;

        while (left < right)
        {
            if (input[left] != input[right])
                return false;
            
            left++;
            right--;
        }
        return true;
    }
