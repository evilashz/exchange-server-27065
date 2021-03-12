using System;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004F6 RID: 1270
	public class UpdateAspnetConfigCommand : SyntheticCommand<object>
	{
		// Token: 0x0600456D RID: 17773 RVA: 0x00071A7E File Offset: 0x0006FC7E
		private UpdateAspnetConfigCommand() : base("Update-AspnetConfig")
		{
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00071A8B File Offset: 0x0006FC8B
		public UpdateAspnetConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
