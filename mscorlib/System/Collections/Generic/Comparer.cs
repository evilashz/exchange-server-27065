using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x0200048D RID: 1165
	[TypeDependency("System.Collections.Generic.ObjectComparer`1")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Comparer<T> : IComparer, IComparer<T>
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000D9272 File Offset: 0x000D7472
		[__DynamicallyInvokable]
		public static Comparer<T> Default
		{
			[__DynamicallyInvokable]
			get
			{
				return Comparer<T>.defaultComparer;
			}
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000D9279 File Offset: 0x000D7479
		[__DynamicallyInvokable]
		public static Comparer<T> Create(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return new ComparisonComparer<T>(comparison);
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000D9290 File Offset: 0x000D7490
		[SecuritySafeCritical]
		private static Comparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (typeof(IComparable<T>).IsAssignableFrom(runtimeType))
			{
				return (Comparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericComparer<int>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IComparable<>).MakeGenericType(new Type[]
				{
					runtimeType2
				}).IsAssignableFrom(runtimeType2))
				{
					return (Comparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(NullableComparer<int>), runtimeType2);
				}
			}
			return new ObjectComparer<T>();
		}

		// Token: 0x06003908 RID: 14600
		[__DynamicallyInvokable]
		public abstract int Compare(T x, T y);

		// Token: 0x06003909 RID: 14601 RVA: 0x000D9348 File Offset: 0x000D7548
		[__DynamicallyInvokable]
		int IComparer.Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				if (x is T && y is T)
				{
					return this.Compare((T)((object)x), (T)((object)y));
				}
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
				return 0;
			}
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000D9383 File Offset: 0x000D7583
		[__DynamicallyInvokable]
		protected Comparer()
		{
		}

		// Token: 0x04001885 RID: 6277
		private static readonly Comparer<T> defaultComparer = Comparer<T>.CreateComparer();
	}
}
