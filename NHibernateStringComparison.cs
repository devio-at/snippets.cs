using System.Collections.ObjectModel;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;

namespace deviolib
{
    public static class StringComparison
    {
        public static bool GreaterThan(this string s, string other)
        {
            return string.Compare(s, other) > 0;
        }
        public static bool GreaterThanOrEqual(this string s, string other)
        {
            return string.Compare(s, other) >= 0;
        }
        public static bool LessThan(this string s, string other)
        {
            return string.Compare(s, other) < 0;
        }
        public static bool LessThanOrEqual(this string s, string other)
        {
            return string.Compare(s, other) <= 0;
        }
    }
}

namespace deviolib.NHibernate
{
    public class StringGreaterThanGenerator : BaseHqlGeneratorForMethod
    {
        public StringGreaterThanGenerator()
        {
            SupportedMethods = new[]
		                   	{
		                   		ReflectionHelper.GetMethodDefinition<string>(x => x.GreaterThan(null))
		                   	};
        }

        public override HqlTreeNode BuildHql(MethodInfo method, System.Linq.Expressions.Expression targetObject, ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.GreaterThan(
                visitor.Visit(targetObject).AsExpression(),
                visitor.Visit(arguments[0]).AsExpression());
        }
    }

    public class StringGreaterThanOrEqualGenerator : BaseHqlGeneratorForMethod
    {
        public StringGreaterThanOrEqualGenerator()
        {
            SupportedMethods = new[]
		                   	{
		                   		ReflectionHelper.GetMethodDefinition<string>(x => x.GreaterThanOrEqual(null))
		                   	};
        }

        public override HqlTreeNode BuildHql(MethodInfo method, System.Linq.Expressions.Expression targetObject, ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.GreaterThanOrEqual(
                visitor.Visit(arguments[0]).AsExpression(),
                visitor.Visit(arguments[1]).AsExpression());
        }
    }

    public class StringLessThanGenerator : BaseHqlGeneratorForMethod
    {
        public StringLessThanGenerator()
        {
            SupportedMethods = new[]
		                   	{
		                   		ReflectionHelper.GetMethodDefinition<string>(x => x.LessThan(null))
		                   	};
        }

        public override HqlTreeNode BuildHql(MethodInfo method, System.Linq.Expressions.Expression targetObject, ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.LessThan(
                visitor.Visit(targetObject).AsExpression(),
                visitor.Visit(arguments[0]).AsExpression());
        }
    }

    public class StringLessThanOrEqualGenerator : BaseHqlGeneratorForMethod
    {
        public StringLessThanOrEqualGenerator()
        {
            SupportedMethods = new[]
		                   	{
		                   		ReflectionHelper.GetMethodDefinition<string>(x => x.LessThanOrEqual(null))
		                   	};
        }

        public override HqlTreeNode BuildHql(MethodInfo method, System.Linq.Expressions.Expression targetObject, ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.LessThanOrEqual(
                visitor.Visit(arguments[0]).AsExpression(),
                visitor.Visit(arguments[1]).AsExpression());
        }
    }

    public class StringComparisonLinqtoHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public StringComparisonLinqtoHqlGeneratorsRegistry()
        {
            this.Merge(new StringGreaterThanGenerator());
            this.Merge(new StringGreaterThanOrEqualGenerator());
            this.Merge(new StringLessThanGenerator());
            this.Merge(new StringLessThanOrEqualGenerator());
        }
    }
}

/*
	activate using
	
	            configuration.LinqToHqlGeneratorsRegistry<StringComparisonLinqtoHqlGeneratorsRegistry>();
*/