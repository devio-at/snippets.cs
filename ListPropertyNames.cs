using System;
using System.Linq;
using System.Linq.Expressions;

namespace ListPropertyNames
{
    // database entities

    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }

    class Program
    {
        public static string GetPropertyName<T, TP>(Expression<Func<T, TP>> expression)
        {
            if (expression.Body is UnaryExpression { NodeType: ExpressionType.Convert } unex)
            {
                var ex = unex.Operand;
                var mex = (MemberExpression)ex;
                return mex.Member.Name;
            }

            var memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }

        public static string EnumerateColumns<T>(string tablePrefix, string columnPrefix, params Expression<Func<T, object>>[] columns)
        {
            return string.Join(", ", columns.Select(c =>
                (tablePrefix != null ? (tablePrefix + ".") : "") +
                GetPropertyName(c) +
                (columnPrefix != null ? (" AS " + columnPrefix + GetPropertyName(c)) : "")));
        }

        static void Main(string[] args)
        {
            var actualSql = "SELECT "
                            + EnumerateColumns<Supplier>("s", "Supplier", s => s.Id, s => s.Name)
                            + ", "
                            + EnumerateColumns<Address>("a", "Address", a => a.ZipCode, a => a.City, a => a.City)
                            + @"
FROM " + nameof(Supplier) + @" s
INNER JOIN " + nameof(Address) + " a ON s." + nameof(Supplier.AddressId) + " = a." + nameof(Addresse.Id);

            Console.WriteLine(actualSql);
        }
    }
}
