public class CSharpReview
{
    /// String Reversal
    public string ReverseString(string input)
    {
        char[] chars = new char[input.length];
        for (int i = 0; i < input.length: i++)
        {
            chars[i] = input[input.length - 1 - i];
        }

        return new string(chars);
    }

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

    /// Find the first non-repeated Character in a string
    public char? FirstNonRepeatedChar(string input) // ? indicates nullable
    {
        Dictionary<char, int> charCount = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        foreach (char c in input)
        {
            if (charCount[c] == 1)
                return c;
        }

        return null;
    }

    /// Binary Search Algorithm
    public int BinarySearch(int[] array, int target)
    {
        int left = 0;
        int right = array.length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (array[mid] == target)
                return mid;

            if (array[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }

    

    public string LongestCommonSubstring(string str1, string str2)
{
    int[,] dp = new int[str1.Length + 1, str2.Length + 1];
    int maxLength = 0;
    int endIndex = 0;
    
    for (int i = 1; i <= str1.Length; i++)
    {
        for (int j = 1; j <= str2.Length; j++)
        {
            if (str1[i - 1] == str2[j - 1])
            {
                dp[i, j] = dp[i - 1, j - 1] + 1;
                if (dp[i, j] > maxLength)
                {
                    maxLength = dp[i, j];
                    endIndex = i - 1;
                }
            }
        }
    }
    
    return str1.Substring(endIndex - maxLength + 1, maxLength);
}
}