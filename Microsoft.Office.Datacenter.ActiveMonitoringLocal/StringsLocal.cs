using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A9 RID: 169
	internal static class StringsLocal
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x0002109C File Offset: 0x0001F29C
		static StringsLocal()
		{
			StringsLocal.stringIDs.Add(3314367409U, "GenericServiceProbeTargetResource");
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000210EB File Offset: 0x0001F2EB
		public static LocalizedString GenericServiceProbeTargetResource
		{
			get
			{
				return new LocalizedString("GenericServiceProbeTargetResource", StringsLocal.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00021102 File Offset: 0x0001F302
		public static LocalizedString GetLocalizedString(StringsLocal.IDs key)
		{
			return new LocalizedString(StringsLocal.stringIDs[(uint)key], StringsLocal.ResourceManager, new object[0]);
		}

		// Token: 0x04000615 RID: 1557
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000616 RID: 1558
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Office.Datacenter.ActiveMonitoringLocal.StringsLocal", typeof(StringsLocal).GetTypeInfo().Assembly);

		// Token: 0x020000AA RID: 170
		public enum IDs : uint
		{
			// Token: 0x04000618 RID: 1560
			GenericServiceProbeTargetResource = 3314367409U
		}
	}
}
