﻿using Tekla.Structures.Model;

namespace SpliceConn
{
    public static class ExtensionMethods
    {
        public static T GetReportProperty<T>(this ModelObject modelObject, string property)
        {
            var stringValue = string.Empty;
            var intValue = 0;
            var doubleValue = 0.0;

            if (typeof(T) == typeof(string))
            {
                modelObject.GetReportProperty(property, ref stringValue);
                return (T)(stringValue as object);
            }
            if (typeof(T) == typeof(int))
            {
                modelObject.GetReportProperty(property, ref intValue);
                return (T)(intValue as object);
            }
            if (typeof(T) == typeof(double))
            {
                modelObject.GetReportProperty(property, ref doubleValue);
                return (T)(doubleValue as object);
            }

            return (T)(new object());
        }
    }
}