using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200003E RID: 62
	internal class ForwardSyncCompanyProbeStxLogger : StxLoggerBase
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000068AC File Offset: 0x00004AAC
		public ForwardSyncCompanyProbeStxLogger()
		{
			base.ExtendedFields.AddRange(new List<FieldInfo>
			{
				new FieldInfo(StxLoggerBase.MandatoryFields.Count, "OrganizationStatus"),
				new FieldInfo(StxLoggerBase.MandatoryFields.Count + 1, "OrganizationName")
			});
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006907 File Offset: 0x00004B07
		internal override string LogTypeName
		{
			get
			{
				return "ForwardSyncCompanyProbe Logs";
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000690E File Offset: 0x00004B0E
		internal override string LogComponent
		{
			get
			{
				return "ForwardSyncCompanyProbe";
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006915 File Offset: 0x00004B15
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ForwardSyncCompanyProbe_";
			}
		}
	}
}
