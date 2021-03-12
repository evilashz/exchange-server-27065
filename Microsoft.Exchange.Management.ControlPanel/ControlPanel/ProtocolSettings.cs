using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002DA RID: 730
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ProtocolSettings : DataSourceService, IProtocolSettings, IGetObjectService<ProtocolSettingsData>
	{
		// Token: 0x06002CC7 RID: 11463 RVA: 0x00089B4C File Offset: 0x00087D4C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-CasMailbox?Identity&ProtocolSettings@R:Self")]
		public PowerShellResults<ProtocolSettingsData> GetObject(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Get-CasMailbox");
			pscommand.AddParameter("ProtocolSettings", true);
			return base.GetObject<ProtocolSettingsData>(pscommand, Identity.FromExecutingUserId());
		}

		// Token: 0x0400220E RID: 8718
		private const string GetObjectRole = "Get-CasMailbox?Identity&ProtocolSettings@R:Self";

		// Token: 0x0400220F RID: 8719
		internal const string GetCmdlet = "Get-CasMailbox";

		// Token: 0x04002210 RID: 8720
		internal const string ReadScope = "@R:Self";
	}
}
