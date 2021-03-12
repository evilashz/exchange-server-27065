using System;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000521 RID: 1313
	public class GetHybridMailflowDatacenterIPsCommand : SyntheticCommand<object>
	{
		// Token: 0x060046AB RID: 18091 RVA: 0x00073325 File Offset: 0x00071525
		private GetHybridMailflowDatacenterIPsCommand() : base("Get-HybridMailflowDatacenterIPs")
		{
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x00073332 File Offset: 0x00071532
		public GetHybridMailflowDatacenterIPsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
