using System;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000633 RID: 1587
	public class GetExchangeServerAccessLicenseCommand : SyntheticCommand<object>
	{
		// Token: 0x060050AB RID: 20651 RVA: 0x0007FC78 File Offset: 0x0007DE78
		private GetExchangeServerAccessLicenseCommand() : base("Get-ExchangeServerAccessLicense")
		{
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0007FC85 File Offset: 0x0007DE85
		public GetExchangeServerAccessLicenseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
