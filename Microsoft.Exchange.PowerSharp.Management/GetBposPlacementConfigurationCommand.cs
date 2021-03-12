using System;
using Microsoft.Exchange.Management.ForwardSyncTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000110 RID: 272
	public class GetBposPlacementConfigurationCommand : SyntheticCommandWithPipelineInput<BposPlacementConfiguration, BposPlacementConfiguration>
	{
		// Token: 0x06001F32 RID: 7986 RVA: 0x0004030A File Offset: 0x0003E50A
		private GetBposPlacementConfigurationCommand() : base("Get-BposPlacementConfiguration")
		{
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00040317 File Offset: 0x0003E517
		public GetBposPlacementConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
