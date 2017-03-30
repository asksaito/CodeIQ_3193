using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeIQ_3193
{
    /// <summary>
    /// CodeIQ - 素数の足し算で
    /// @Nabetani   鍋谷 武典さんからの問題
    /// https://codeiq.jp/q/3193
    /// </summary>
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

            int target = int.Parse(inputParams[0]); // 合計値（求めるターゲットとなる数）
            int min = int.Parse(inputParams[1]);    // 最小値
            int max = int.Parse(inputParams[2]);    // 最大値

            // 素数の抽出
            var primeNumbers = new List<int>();
            for (int i = min; i <= max; i++)
            {
                if(i > target)
                {
                    // 合計値より大きい素数はすべて除外（処理速度のため）
                    break;
                }

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
                List<int> sumList = GetCombinationSum(primeNumbers, i, target);

                // 合計値に一致する件数を加算
                count += sumList.Where(sum => sum == target).Count();
            }

            // 標準出力へ結果出力
            Console.WriteLine(count.ToString());
        }

        /// <summary>
        /// 組み合わせの数のリストを返す
        /// </summary>
        /// <param name="primeNumbers">素数のリスト</param>
        /// <param name="selectCnt">選択する数の個数</param>
        /// <param name="target">合計値</param>
        /// <returns></returns>
        private static List<int> GetCombinationSum(List<int> primeNumbers, int selectCnt, int target)
        {
            var resultList = new List<int>();

            for (int i = 0; i <= primeNumbers.Count - selectCnt; i++)
            {
                int baseNum = primeNumbers[i];
                List<int> remainPrimeNumbers = primeNumbers.GetRange(i + 1, primeNumbers.Count - 1 - i);

                if(baseNum > (target / selectCnt))
                {
                    // 基数が合計値/選択数を超える場合、以降の数はすべて合計値以上になるので選択を打ち切る（処理速度のため）
                    break;
                }

                if (selectCnt == 1)
                {
                    // 再帰終了
                    resultList.Add(baseNum);
                }
                else
                {
                    // 再帰的に組み合わせを取得する（nCr）
                    List<int> remainCombinationSumList = GetCombinationSum(remainPrimeNumbers, selectCnt - 1, target);
                    foreach (int sum in remainCombinationSumList)
                    {
                        if (baseNum + sum <= target)
                        {
                            // 合計値より小さい場合のみ追加（処理速度のため）
                            resultList.Add(baseNum + sum);
                        }
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
            if(num <= 1)
            {
                // 1以下の数は素数ではない
                return false;
            }

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
