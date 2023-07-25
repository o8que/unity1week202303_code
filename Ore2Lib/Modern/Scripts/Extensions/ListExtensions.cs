using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ore2Lib
{
    public static class ListExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> list) {
            for (int i = list.Count - 1; i > 0; i--) {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
            return list;
        }

        public static void InsertionSort<T>(this IList<T> list, Comparison<T> comparison = null) {
            if (comparison != null) {
                InsertionSort(list, 0, list.Count, new ComparisonComparer<T>(comparison));
            } else {
                InsertionSort(list, 0, list.Count, Comparer<T>.Default);
            }
        }

        public static void InsertionSort<T, TComparer>(this IList<T> list, TComparer comparer) where TComparer : IComparer<T> {
            InsertionSort(list, 0, list.Count, comparer);
        }

        public static void InsertionSort<T, TComparer>(this IList<T> list, int index, int count, TComparer comparer) where TComparer : IComparer<T> {
            int begin = Mathf.Max(0, index);
            int end = Mathf.Min(begin + count, list.Count);

            for (int i = begin + 1; i < end; i++) {
                var element = list[i];
                // 要素がソート済みならスキップ
                if (comparer.Compare(list[i - 1], element) <= 0) { continue; }
                // リストの先頭を番兵として、要素を適切な位置に挿入する
                int j = i;
                if (comparer.Compare(list[begin], element) > 0) {
                    while (j > begin) {
                        list[j] = list[j - 1];
                        j--;
                    }
                } else {
                    do {
                        list[j] = list[j - 1];
                        j--;
                    } while (comparer.Compare(list[j - 1], element) > 0);
                }
                list[j] = element;
            }
        }
    }

    public readonly struct ComparisonComparer<T> : IComparer<T>
    {
        private readonly Comparison<T> comparison;

        public ComparisonComparer(Comparison<T> comparison) {
            this.comparison = comparison;
        }

        int IComparer<T>.Compare(T x, T y) {
            return comparison(x, y);
        }
    }
}
