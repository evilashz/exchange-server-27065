using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Office.CsmSdk;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000387 RID: 903
	internal class FlightQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x0600306D RID: 12397 RVA: 0x0009391F File Offset: 0x00091B1F
		public FlightQueryProcessor(string featureName)
		{
			this.featureName = featureName;
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x0009392E File Offset: 0x00091B2E
		public override bool CanCache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x00093931 File Offset: 0x00091B31
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return new bool?(FlightProvider.Instance.IsFeatureEnabled(this.featureName));
		}

		// Token: 0x0400236E RID: 9070
		private readonly string featureName;
	}
}
