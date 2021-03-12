using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000075 RID: 117
	public static class PerUserTracing
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00012FDF File Offset: 0x000111DF
		private static IPerIdentityTracing<string> Instance
		{
			get
			{
				return PerUserTracing.hookableInstance.Value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00012FEB File Offset: 0x000111EB
		public static bool IsConfigured
		{
			get
			{
				return PerUserTracing.Instance.IsConfigured;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00012FF7 File Offset: 0x000111F7
		public static bool IsTurnedOn
		{
			get
			{
				return PerUserTracing.Instance.IsTurnedOn;
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00013003 File Offset: 0x00011203
		public static bool IsEnabledForUser(string userName)
		{
			return PerUserTracing.Instance.IsEnabled(userName);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00013010 File Offset: 0x00011210
		public static void TurnOn()
		{
			PerUserTracing.Instance.TurnOn();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001301C File Offset: 0x0001121C
		public static void TurnOff()
		{
			PerUserTracing.Instance.TurnOff();
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00013028 File Offset: 0x00011228
		internal static IDisposable SetTestHook(IPerIdentityTracing<string> testHook)
		{
			return PerUserTracing.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x04000621 RID: 1569
		private static Hookable<IPerIdentityTracing<string>> hookableInstance = Hookable<IPerIdentityTracing<string>>.Create(true, new PerUserTracing.PerUserTracingImpl());

		// Token: 0x02000076 RID: 118
		private class PerUserTracingImpl : IPerIdentityTracing<string>
		{
			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00013047 File Offset: 0x00011247
			public bool IsConfigured
			{
				get
				{
					return ExTraceConfiguration.Instance.PerThreadTracingConfigured;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060006AA RID: 1706 RVA: 0x00013053 File Offset: 0x00011253
			public bool IsTurnedOn
			{
				get
				{
					return BaseTrace.CurrentThreadSettings.IsEnabled;
				}
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x0001305F File Offset: 0x0001125F
			public bool IsEnabled(string userName)
			{
				return ExUserTracingAdaptor.Instance.IsTracingEnabledUser(userName);
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x0001306C File Offset: 0x0001126C
			public void TurnOn()
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x00013078 File Offset: 0x00011278
			public void TurnOff()
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}
	}
}
