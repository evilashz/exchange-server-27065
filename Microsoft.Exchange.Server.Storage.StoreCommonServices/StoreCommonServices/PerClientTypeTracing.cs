using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E9 RID: 233
	public static class PerClientTypeTracing
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0002B84E File Offset: 0x00029A4E
		private static IPerIdentityTracing<ClientType> Instance
		{
			get
			{
				return PerClientTypeTracing.hookableInstance.Value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0002B85A File Offset: 0x00029A5A
		public static bool IsConfigured
		{
			get
			{
				return PerClientTypeTracing.Instance.IsConfigured;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0002B866 File Offset: 0x00029A66
		public static bool IsTurnedOn
		{
			get
			{
				return PerClientTypeTracing.Instance.IsTurnedOn;
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002B872 File Offset: 0x00029A72
		public static bool IsEnabled(ClientType clientType)
		{
			return PerClientTypeTracing.Instance.IsEnabled(clientType);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002B87F File Offset: 0x00029A7F
		public static void TurnOn()
		{
			PerClientTypeTracing.Instance.TurnOn();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002B88B File Offset: 0x00029A8B
		public static void TurnOff()
		{
			PerClientTypeTracing.Instance.TurnOff();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002B897 File Offset: 0x00029A97
		internal static IDisposable SetTestHook(IPerIdentityTracing<ClientType> testHook)
		{
			return PerClientTypeTracing.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x04000544 RID: 1348
		private static Hookable<IPerIdentityTracing<ClientType>> hookableInstance = Hookable<IPerIdentityTracing<ClientType>>.Create(true, new PerClientTypeTracing.PerClientTypeTracingImpl());

		// Token: 0x020000EA RID: 234
		private class PerClientTypeTracingImpl : ExCustomTracingAdaptor<ClientType>, IPerIdentityTracing<ClientType>
		{
			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000948 RID: 2376 RVA: 0x0002B8B6 File Offset: 0x00029AB6
			public bool IsConfigured
			{
				get
				{
					return ExTraceConfiguration.Instance.PerThreadTracingConfigured;
				}
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000949 RID: 2377 RVA: 0x0002B8C2 File Offset: 0x00029AC2
			public bool IsTurnedOn
			{
				get
				{
					return BaseTrace.CurrentThreadSettings.IsEnabled;
				}
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0002B8CE File Offset: 0x00029ACE
			public bool IsEnabled(ClientType clientType)
			{
				return base.IsTracingEnabled(clientType);
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x0002B8D7 File Offset: 0x00029AD7
			public void TurnOn()
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0002B8E3 File Offset: 0x00029AE3
			public void TurnOff()
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}

			// Token: 0x0600094D RID: 2381 RVA: 0x0002B8F0 File Offset: 0x00029AF0
			protected override HashSet<ClientType> LoadEnabledIdentities(ExTraceConfiguration currentInstance)
			{
				HashSet<ClientType> hashSet = new HashSet<ClientType>();
				List<string> list;
				if (currentInstance.CustomParameters.TryGetValue("ClientType", out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						ClientType item;
						if (Enum.TryParse<ClientType>(list[i], true, out item))
						{
							hashSet.Add(item);
						}
					}
				}
				return hashSet;
			}
		}
	}
}
