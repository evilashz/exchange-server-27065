using System;

namespace System.Threading
{
	// Token: 0x02000512 RID: 1298
	internal static class LazyHelpers<T>
	{
		// Token: 0x06003DC7 RID: 15815 RVA: 0x000E55B8 File Offset: 0x000E37B8
		private static T ActivatorFactorySelector()
		{
			T result;
			try
			{
				result = (T)((object)Activator.CreateInstance(typeof(T)));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
			}
			return result;
		}

		// Token: 0x040019BB RID: 6587
		internal static Func<T> s_activatorFactorySelector = new Func<T>(LazyHelpers<T>.ActivatorFactorySelector);
	}
}
