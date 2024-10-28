/// Is a string a Palindrome i.e. can be read both ways.
public class Palindrome
{
    public bool IsPalindrome(string input)
    {
        if (input == null)
            return false;

        input = input.ToLower();
        int left = 0;
        int right = input.Length - 1;

        // if using a for loop I would work out the midpoint, test if we are there
        // and test input[i] != input[input.Length - i - 1]

        while (left < right)
        {
            if (input[left] != input[right])
                return false;

            left++;
            right--;
        }
        return true;
    }
}
