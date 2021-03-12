using System;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002FC RID: 764
	public class EnableExchangePiiMappingCommand : SyntheticCommand<object>
	{
		// Token: 0x06003334 RID: 13108 RVA: 0x0005A4B8 File Offset: 0x000586B8
		private EnableExchangePiiMappingCommand() : base("Enable-ExchangePiiMapping")
		{
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0005A4C5 File Offset: 0x000586C5
		public EnableExchangePiiMappingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
