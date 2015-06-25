/*
 * Copyright © 2012
 * This code is for the codeproject article "A N-Tier Architecture Sample with ASP.NET MVC3, WCF and Entity Framework" at  
 * http://www.codeproject.com/Articles/434282/A-N-Tier-Architecture-Sample-with-ASP.NET-MVC3-WCF-and-Entity-Framework. 
 * Permission to use, copy or modify this software freely is hereby granted, 
 * provided that this copyright notice appears in the orginal or modified copies. 
 * 
 * This code isn't guaranteed to work correctly; it is your own responsibility for 
 * any result from using this code. 
 *                           
 * @description
 * 
 * @author  
 * @version July 18, 2012
 * @see
 * @since
 */

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GH.Common.Utils
{
    public static class Util
    {
        public static string GetMemberName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        public static string GetMemberNameExtra<T, V>(Expression<Func<T, V>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        public static Object GetPropertyValueByName<T>(Object objWithProperties, String name)
        {
            Type t = typeof (T);
            PropertyInfo p = t.GetProperty(name);
            return p.GetValue(objWithProperties, null);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <typeparam name = "U"></typeparam>
        /// <param name = "expression"></param>
        /// <returns></returns>
        public static MemberInfo GetMemberInfo<T, U>(Expression<Func<T, U>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member;

            throw new ArgumentException("Expression is not a member access", "expression");
        }
    }
}