﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebBookmarkBo.Service;

namespace WebBookmarkBo
{
    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 根据某个Key去重
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {

            var seenKeys = new HashSet<TKey>();

            return source.Where(element => seenKeys.Add(keySelector(element)));

        }

        /// <summary>
        /// 随机排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(List<T> inputList)
        {
            //Copy to a array
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);

            //Add range
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);

            while (copyList.Count > 0)
            {
                //Select an index and item
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];

                //remove it from copyList and add it to output
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }



        /// <summary>
        /// long 转换成string密文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ConvertToCiphertext(this long id)
        {
            string val = id.ToString();
            return Encryption.EncryptString(val);
        }

        /// <summary>
        /// string密文 转换成long
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long ConvertToPlainLong(this string val)
        {
            var dec = Encryption.DecryptString(val);
            return Convert.ToInt64(dec);
        }

        /// <summary>
        /// 明文字符串转换成密文
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string ConvertToCiphertext(this string plaintext)
        {
            return Encryption.EncryptString(plaintext);
        }

        /// <summary>
        /// 密文字符串转换成明文
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        public static string ConvertToPlaintext(this string ciphertext)
        {
            return Encryption.DecryptString(ciphertext);
        }


        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        public static string GetHost(this string href)
        {
            if (string.IsNullOrEmpty(href))
                return string.Empty;
            Uri uri = new Uri(href);
            return uri.Host;
        }


       
    }
}
