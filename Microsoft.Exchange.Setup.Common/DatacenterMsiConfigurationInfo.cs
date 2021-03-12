using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatacenterMsiConfigurationInfo : MsiConfigurationInfo
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000604C File Offset: 0x0000424C
		public override string Name
		{
			get
			{
				return "Datacenter.msi";
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006053 File Offset: 0x00004253
		public override Guid ProductCode
		{
			get
			{
				return DatacenterMsiConfigurationInfo.productCode;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000605A File Offset: 0x0000425A
		protected override string LogFileName
		{
			get
			{
				return "DatacenterSetup.msilog";
			}
		}

		// Token: 0x04000054 RID: 84
		private const string MsiFileName = "Datacenter.msi";

		// Token: 0x04000055 RID: 85
		private const string MsiLogFileName = "DatacenterSetup.msilog";

		// Token: 0x04000056 RID: 86
		protected static readonly Guid productCode = new Guid("{7FD073ED-7852-4798-A223-A0266176B4DA}");
	}
}
