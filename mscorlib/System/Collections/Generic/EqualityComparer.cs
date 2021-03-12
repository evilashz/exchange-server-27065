using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000493 RID: 1171
	[TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000DA3E1 File Offset: 0x000D85E1
		[__DynamicallyInvokable]
		public static EqualityComparer<T> Default
		{
			[__DynamicallyInvokable]
			get
			{
				return EqualityComparer<T>.defaultComparer;
			}
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000DA3E8 File Offset: 0x000D85E8
		[SecuritySafeCritical]
		private static EqualityComparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (runtimeType == typeof(byte))
			{
				return (EqualityComparer<T>)new ByteEqualityComparer();
			}
			if (typeof(IEquatable<T>).IsAssignableFrom(runtimeType))
			{
				return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericEqualityComparer<int>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IEquatable<>).MakeGenericType(new Type[]
				{
					runtimeType2
				}).IsAssignableFrom(runtimeType2))
				{
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(NullableEqualityComparer<int>), runtimeType2);
				}
			}
			if (runtimeType.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(runtimeType)))
				{
				case TypeCode.SByte:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(SByteEnumEqualityComparer<sbyte>), runtimeType);
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(EnumEqualityComparer<int>), runtimeType);
				case TypeCode.Int16:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(ShortEnumEqualityComparer<short>), runtimeType);
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return (EqualityComparer<T>)RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(LongEnumEqualityComparer<long>), runtimeType);
				}
			}
			return new ObjectEqualityComparer<T>();
		}

		// Token: 0x06003952 RID: 14674
		[__DynamicallyInvokable]
		public abstract bool Equals(T x, T y);

		// Token: 0x06003953 RID: 14675
		[__DynamicallyInvokable]
		public abstract int GetHashCode(T obj);

		// Token: 0x06003954 RID: 14676 RVA: 0x000DA56C File Offset: 0x000D876C
		internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000DA5A0 File Offset: 0x000D87A0
		internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000DA5D3 File Offset: 0x000D87D3
		[__DynamicallyInvokable]
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is T)
			{
				return this.GetHashCode((T)((object)obj));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return 0;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000DA5F6 File Offset: 0x000D87F6
		[__DynamicallyInvokable]
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is T && y is T)
			{
				return this.Equals((T)((object)x), (T)((object)y));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000DA630 File Offset: 0x000D8830
		[__DynamicallyInvokable]
		protected EqualityComparer()
		{
		}

		// Token: 0x04001895 RID: 6293
		private static readonly EqualityComparer<T> defaultComparer = EqualityComparer<T>.CreateComparer();
	}
}
