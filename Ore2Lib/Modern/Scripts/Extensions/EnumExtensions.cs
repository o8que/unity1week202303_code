using System;

namespace Ore2Lib
{
    public static class EnumExtensions
    {
        /// <summary>
        /// byteの値を列挙型に変換する
        /// </summary>
        public static T ToEnum<T>(this byte value) where T : Enum {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>
        /// shortの値を列挙型に変換する
        /// </summary>
        public static T ToEnum<T>(this short value) where T : Enum {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>
        /// intの値を列挙型に変換する
        /// </summary>
        public static T ToEnum<T>(this int value) where T : Enum {
            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
