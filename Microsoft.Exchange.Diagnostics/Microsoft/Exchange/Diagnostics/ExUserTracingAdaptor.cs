using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000094 RID: 148
	internal sealed class ExUserTracingAdaptor : ExCustomTracingAdaptor<string>
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		private ExUserTracingAdaptor()
		{
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		public static ExUserTracingAdaptor Instance
		{
			get
			{
				if (ExUserTracingAdaptor.instance == null)
				{
					lock (ExUserTracingAdaptor.instanceLock)
					{
						if (ExUserTracingAdaptor.instance == null)
						{
							ExUserTracingAdaptor.instance = new ExUserTracingAdaptor();
						}
					}
				}
				return ExUserTracingAdaptor.instance;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C724 File Offset: 0x0000A924
		public bool IsTracingEnabledUser(string userName)
		{
			return base.IsTracingEnabled(userName);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C730 File Offset: 0x0000A930
		protected override HashSet<string> LoadEnabledIdentities(ExTraceConfiguration currentInstance)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<string> list;
			if (currentInstance.CustomParameters.TryGetValue("UserDN", out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					hashSet.Add(list[i]);
				}
			}
			if (currentInstance.CustomParameters.TryGetValue("WindowsIdentity", out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					hashSet.Add(list[j]);
				}
			}
			return hashSet;
		}

		// Token: 0x0400030F RID: 783
		private static ExUserTracingAdaptor instance;

		// Token: 0x04000310 RID: 784
		private static object instanceLock = new object();
	}
}
