using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200004E RID: 78
	internal static class Strings
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public static LocalizedString NonOperationalAdmissionControl(ResourceKey resource)
		{
			return new LocalizedString("NonOperationalAdmissionControl", Strings.ResourceManager, new object[]
			{
				resource
			});
		}

		// Token: 0x04000197 RID: 407
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.WorkloadManagement.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200004F RID: 79
		private enum ParamIDs
		{
			// Token: 0x04000199 RID: 409
			NonOperationalAdmissionControl
		}
	}
}
