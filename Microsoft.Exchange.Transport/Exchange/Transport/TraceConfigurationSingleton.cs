using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200007A RID: 122
	internal class TraceConfigurationSingleton<T> where T : TraceConfigurationBase, new()
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public static T Instance
		{
			get
			{
				if (TraceConfigurationSingleton<T>.NeedInstance(TraceConfigurationSingleton<T>.instance))
				{
					lock (TraceConfigurationSingleton<T>.staticSyncObject)
					{
						if (TraceConfigurationSingleton<T>.NeedInstance(TraceConfigurationSingleton<T>.instance))
						{
							T t = Activator.CreateInstance<T>();
							t.Load(ExTraceConfiguration.Instance);
							TraceConfigurationSingleton<T>.instance = t;
						}
					}
				}
				return TraceConfigurationSingleton<T>.instance;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
		private static bool NeedInstance(T instance)
		{
			return instance == null || instance.IsUpdateNeeded;
		}

		// Token: 0x040001FB RID: 507
		private static object staticSyncObject = new object();

		// Token: 0x040001FC RID: 508
		private static T instance = default(T);
	}
}
