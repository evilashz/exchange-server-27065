using System;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002FB RID: 763
	public class DisableExchangePiiMappingCommand : SyntheticCommand<object>
	{
		// Token: 0x06003332 RID: 13106 RVA: 0x0005A49C File Offset: 0x0005869C
		private DisableExchangePiiMappingCommand() : base("Disable-ExchangePiiMapping")
		{
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0005A4A9 File Offset: 0x000586A9
		public DisableExchangePiiMappingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
