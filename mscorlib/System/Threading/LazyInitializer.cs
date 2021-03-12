using System;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000511 RID: 1297
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class LazyInitializer
	{
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000E548D File Offset: 0x000E368D
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target) where T : class
		{
			if (Volatile.Read<T>(ref target) != null)
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, LazyHelpers<T>.s_activatorFactorySelector);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000E54AE File Offset: 0x000E36AE
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
		{
			if (Volatile.Read<T>(ref target) != null)
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x000E54CC File Offset: 0x000E36CC
		private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Lazy_StaticInit_InvalidOperation"));
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000E550F File Offset: 0x000E370F
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, LazyHelpers<T>.s_activatorFactorySelector);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000E552D File Offset: 0x000E372D
		[__DynamicallyInvokable]
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000E5548 File Offset: 0x000E3748
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			object obj = syncLock;
			if (obj == null)
			{
				object obj2 = new object();
				obj = Interlocked.CompareExchange(ref syncLock, obj2, null);
				if (obj == null)
				{
					obj = obj2;
				}
			}
			object obj3 = obj;
			lock (obj3)
			{
				if (!Volatile.Read(ref initialized))
				{
					target = valueFactory();
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}
	}
}
