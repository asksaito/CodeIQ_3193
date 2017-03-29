using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIQ_3193
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "39", "3", "19" };  // サンプル1 出力：3
            //args = new string[] { "70", "9", "30" };  // サンプル2 出力：2
            //args = new string[] { "40", "21", "39" }; // サンプル3 出力：0
            //args = new string[] { "5", "2", "5" };    // 一番簡単なサンプルデータ

            // 標準入力からテストデータを取得
            string input = Console.ReadLine();
            string[] inputParams = input.Split(new char[] {' '});

            int target = int.Parse(inputParams[0]);
            int min = int.Parse(inputParams[1]);
            int max = int.Parse(inputParams[2]);

            // 素数の抽出
            var primeNumbers = new List<int>();
            for (int i = min; i <= max; i++)
            {
                if (isPrimeNumber(i))
                {
                    // 素数のみ追加
                    primeNumbers.Add(i);
                }
            }

            int count = 0;
            for (int i = 1; i <= primeNumbers.Count; i++) // nC1 + nC2 + ... + nCn
            {
                // 組み合わせを取得する（nCr）
                List<List<int>> resultList = getCombination(primeNumbers, i);
                foreach(List<int> result in resultList)
                {
                    if (result.Sum() == target)
                    {
                        // 合計値と一致した
                        count++;
                    }
                }
            }

            // 標準出力へ結果出力
            Console.WriteLine(count.ToString().ToUpper());
        }

        /// <summary>
        /// 組み合わせの数のリストを返す
        /// </summary>
        /// <param name="primeNumbers">素数のリスト</param>
        /// <param name="selectCnt">選択する数の個数</param>
        /// <returns></returns>
        private static List<List<int>> getCombination(List<int> primeNumbers, int selectCnt)
        {
            var resultList = new List<List<int>>();

            for (int i = 0; i <= primeNumbers.Count - selectCnt; i++)
            {
                int baseNum = primeNumbers[i];
                List<int> remainPrimeNumbers = primeNumbers.GetRange(i + 1, primeNumbers.Count - 1 - i);

                if (selectCnt == 1)
                {
                    // 再帰終了
                    resultList.Add(new List<int> { baseNum });
                }
                else
                {
                    // 再帰的に組み合わせを取得する（nCr）
                    List<List<int>> remainCombinationList = getCombination(remainPrimeNumbers, selectCnt - 1);
                    foreach (List<int> list in remainCombinationList)
                    {
                        var result = new List<int>();
                        result.Add(baseNum);
                        result.AddRange(list);

                        resultList.Add(result);
                    }
                }
            }

            return resultList;
        }

        /// <summary>
        /// 素数かどうか判定する
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static bool isPrimeNumber(int num)
        {
            for(int i = 2; i < num; i++)
            {
                if(num % i == 0)
                {
                    // 素数ではない
                    return false;
                }
            }

            // 素数である
            return true;
        }
    }
}
