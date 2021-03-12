using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000019 RID: 25
	public class GetAdServerSettingsCommand : SyntheticCommandWithPipelineInput<RunspaceServerSettingsPresentationObject, RunspaceServerSettingsPresentationObject>
	{
		// Token: 0x060014F9 RID: 5369 RVA: 0x00032F70 File Offset: 0x00031170
		private GetAdServerSettingsCommand() : base("Get-AdServerSettings")
		{
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00032F7D File Offset: 0x0003117D
		public GetAdServerSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
