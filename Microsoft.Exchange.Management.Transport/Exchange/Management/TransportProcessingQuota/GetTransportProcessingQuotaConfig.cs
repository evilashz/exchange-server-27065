using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000AE RID: 174
	[Cmdlet("Get", "TransportProcessingQuotaConfig")]
	public sealed class GetTransportProcessingQuotaConfig : TransportProcessingQuotaBaseTask
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x0001A421 File Offset: 0x00018621
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.WriteObject(base.Session.GetTransportThrottlingConfig());
		}
	}
}
