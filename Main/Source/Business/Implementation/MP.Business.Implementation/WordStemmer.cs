
namespace MP.Business.Implementation
{
    using System;

    public class WordStemmer
    {
        private char[] _wordArray;
        private int _endIndex;

        private int _stemIndex;

        
        public string StemWord(string word)
        {

            if (string.IsNullOrWhiteSpace(word) || word.Length <= 2) return word;

            _wordArray = word.ToCharArray();

            _stemIndex = 0;
            _endIndex = word.Length - 1;
            Step1();
            Step2();
            Step3();
            Step4();
            Step5();
            

            var length = _endIndex + 1;
            return new string(_wordArray, 0, length);
        }


        private void Step1()
        {
            if (_wordArray[_endIndex] == 's')
            {
                if (EndsWith("sses"))
                {
                    _endIndex -= 2;
                }
                else if (EndsWith("ies"))
                {
                    SetEnd("i");
                }
                else if (_wordArray[_endIndex - 1] != 's')
                {
                    _endIndex--;
                }
            }
            if (EndsWith("eed"))
            {
                if (MeasureConsontantSequence() > 0)
                    _endIndex--;
            }
            else if ((EndsWith("ed") || EndsWith("ing")) && VowelInStem())
            {
                _endIndex = _stemIndex;
                if (EndsWith("at"))
                    SetEnd("ate");
                else if (EndsWith("bl"))
                    SetEnd("ble");
                else if (EndsWith("iz"))
                    SetEnd("ize");
                else if (IsDoubleConsontant(_endIndex))
                {
                    _endIndex--;
                    int ch = _wordArray[_endIndex];
                    if (ch == 'l' || ch == 's' || ch == 'z')
                        _endIndex++;
                }
                else if (MeasureConsontantSequence() == 1 && IsCvc(_endIndex)) SetEnd("e");
            }
        }
        
        private void Step2()
        {
            if (EndsWith("y") && VowelInStem())
                _wordArray[_endIndex] = 'i';
        }
        private void Step3()
        {
            if (_endIndex == 0) return;

          
            switch (_wordArray[_endIndex - 1])
            {
                case 'a':
                    if (EndsWith("ational"))
                    {
                        ReplaceEnd("ate");
                        break;
                    }
                    if (EndsWith("tional"))
                    {
                        ReplaceEnd("tion");
                    }
                    break;
                case 'c':
                    if (EndsWith("enci"))
                    {
                        ReplaceEnd("ence");
                        break;
                    }
                    if (EndsWith("anci"))
                    {
                        ReplaceEnd("ance");
                    }
                    break;
                case 'e':
                    if (EndsWith("izer"))
                    {
                        ReplaceEnd("ize");
                    }
                    break;
                case 'l':
                    if (EndsWith("bli"))
                    {
                        ReplaceEnd("ble");
                        break;
                    }
                    if (EndsWith("alli"))
                    {
                        ReplaceEnd("al");
                        break;
                    }
                    if (EndsWith("entli"))
                    {
                        ReplaceEnd("ent");
                        break;
                    }
                    if (EndsWith("eli"))
                    {
                        ReplaceEnd("e");
                        break;
                    }
                    if (EndsWith("ousli"))
                    {
                        ReplaceEnd("ous");
                    }
                    break;
                case 'o':
                    if (EndsWith("ization"))
                    {
                        ReplaceEnd("ize");
                        break;
                    }
                    if (EndsWith("ation"))
                    {
                        ReplaceEnd("ate");
                        break;
                    }
                    if (EndsWith("ator"))
                    {
                        ReplaceEnd("ate");
                    }
                    break;
                case 's':
                    if (EndsWith("alism"))
                    {
                        ReplaceEnd("al");
                        break;
                    }
                    if (EndsWith("iveness"))
                    {
                        ReplaceEnd("ive");
                        break;
                    }
                    if (EndsWith("fulness"))
                    {
                        ReplaceEnd("ful");
                        break;
                    }
                    if (EndsWith("ousness"))
                    {
                        ReplaceEnd("ous");
                    }
                    break;
                case 't':
                    if (EndsWith("aliti"))
                    {
                        ReplaceEnd("al");
                        break;
                    }
                    if (EndsWith("iviti"))
                    {
                        ReplaceEnd("ive");
                        break;
                    }
                    if (EndsWith("biliti"))
                    {
                        ReplaceEnd("ble");
                    }
                    break;
                case 'g':
                    if (EndsWith("logi"))
                    {
                        ReplaceEnd("log");
                    }
                    break;
            }
        }
        
        
        private void Step4()
        {
            switch (_wordArray[_endIndex])
            {
                case 'e':
                    if (EndsWith("icate"))
                    {
                        ReplaceEnd("ic");
                        break;
                    }
                    if (EndsWith("ative"))
                    {
                        ReplaceEnd("");
                        break;
                    }
                    if (EndsWith("alize"))
                    {
                        ReplaceEnd("al");
                    }
                    break;
                case 'i':
                    if (EndsWith("iciti"))
                    {
                        ReplaceEnd("ic");
                    }
                    break;
                case 'l':
                    if (EndsWith("ical"))
                    {
                        ReplaceEnd("ic");
                        break;
                    }
                    if (EndsWith("ful"))
                    {
                        ReplaceEnd("");
                    }
                    break;
                case 's':
                    if (EndsWith("ness"))
                    {
                        ReplaceEnd("");
                    }
                    break;
            }
        }

       

        private void Step5()
        {
            if (_endIndex == 0) return;

            switch (_wordArray[_endIndex - 1])
            {
                case 'a':
                    if (EndsWith("al")) break;
                    return;
                case 'c':
                    if (EndsWith("ance")) break;
                    if (EndsWith("ence")) break;
                    return;
                case 'e':
                    if (EndsWith("er")) break;
                    return;
                case 'i':
                    if (EndsWith("ic")) break;
                    return;
                case 'l':
                    if (EndsWith("able")) break;
                    if (EndsWith("ible")) break;
                    return;
                case 'n':
                    if (EndsWith("ant")) break;
                    if (EndsWith("ement")) break;
                    if (EndsWith("ment")) break;
                
                    if (EndsWith("ent")) break;
                    return;
                case 'o':
                    if (EndsWith("ion") && _stemIndex >= 0 &&
                        (_wordArray[_stemIndex] == 's' || _wordArray[_stemIndex] == 't')) break;
              if (EndsWith("ou")) break;
                    return;
                
                case 's':
                    if (EndsWith("ism")) break;
                    return;
                case 't':
                    if (EndsWith("ate")) break;
                    if (EndsWith("iti")) break;
                    return;
                case 'u':
                    if (EndsWith("ous")) break;
                    return;
                case 'v':
                    if (EndsWith("ive")) break;
                    return;
                case 'z':
                    if (EndsWith("ize")) break;
                    return;
                default:
                    return;
            }
            if (MeasureConsontantSequence() > 1)
                _endIndex = _stemIndex;
        }

   
     
        private bool IsConsonant(int index)
        {
            var c = _wordArray[index];
            if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') return false;
            return c != 'y' || (index == 0 || !IsConsonant(index - 1));
        }


        private int MeasureConsontantSequence()
        {
            var n = 0;
            var index = 0;
            while (true)
            {
                if (index > _stemIndex) return n;
                if (!IsConsonant(index)) break;
                index++;
            }
            index++;
            while (true)
            {
                while (true)
                {
                    if (index > _stemIndex) return n;
                    if (IsConsonant(index)) break;
                    index++;
                }
                index++;
                n++;
                while (true)
                {
                    if (index > _stemIndex) return n;
                    if (!IsConsonant(index)) break;
                    index++;
                }
                index++;
            }
        }
        
        private bool VowelInStem()
        {
            int i;
            for (i = 0; i <= _stemIndex; i++)
            {
                if (!IsConsonant(i)) return true;
            }
            return false;
        }
        
        private bool IsDoubleConsontant(int index)
        {
            if (index < 1) return false;
            return _wordArray[index] == _wordArray[index - 1] && IsConsonant(index);
        }

        private bool IsCvc(int index)
        {
            if (index < 2 || !IsConsonant(index) || IsConsonant(index - 1) || !IsConsonant(index - 2)) return false;
            var c = _wordArray[index];
            return c != 'w' && c != 'x' && c != 'y';
        }


        private bool EndsWith(string s)
        {
            var length = s.Length;
            var index = _endIndex - length + 1;
            if (index < 0) return false;

            for (var i = 0; i < length; i++)
            {
                if (_wordArray[index + i] != s[i]) return false;
            }
            _stemIndex = _endIndex - length;
            return true;
        }

        private void SetEnd(string s)
        {
            var length = s.Length;
            var index = _stemIndex + 1;
            for (var i = 0; i < length; i++)
            {
                _wordArray[index + i] = s[i];
            }
        
            _endIndex = _stemIndex + length;
        }

      
        private void ReplaceEnd(string s)
        {
            if (MeasureConsontantSequence() > 0) SetEnd(s);
        }
    }
}