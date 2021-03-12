using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.Tasks.UM;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C7 RID: 1223
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class UMMailboxResetPin : DataSourceService, IUMMailboxResetPin, IEditObjectService<SetUMMailboxPinConfiguration, SetUMMailboxPinParameters>, IGetObjectService<SetUMMailboxPinConfiguration>
	{
		// Token: 0x06003BF2 RID: 15346 RVA: 0x000B4B78 File Offset: 0x000B2D78
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization")]
		public PowerShellResults<SetUMMailboxPinConfiguration> GetObject(Identity identity)
		{
			PowerShellResults<SetUMMailboxPinConfiguration> @object = base.GetObject<SetUMMailboxPinConfiguration>("Get-UMMailbox", identity);
			if (@object.SucceededWithValue)
			{
				PowerShellResults<UMMailboxPin> powerShellResults = @object.MergeErrors<UMMailboxPin>(base.GetObject<UMMailboxPin>("Get-UMMailboxPin", identity));
				if (powerShellResults.SucceededWithValue)
				{
					@object.Value.UMMailboxPin = powerShellResults.Value;
				}
			}
			return @object;
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000B4BC8 File Offset: 0x000B2DC8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization+Set-UMMailboxPin?Identity@W:Organization")]
		public PowerShellResults<SetUMMailboxPinConfiguration> SetObject(Identity identity, SetUMMailboxPinParameters properties)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Set-UMMailboxPin");
			pscommand.AddParameter("Identity", identity);
			pscommand.AddParameters(properties);
			PowerShellResults powerShellResults = base.Invoke(pscommand);
			PowerShellResults<SetUMMailboxPinConfiguration> powerShellResults2;
			if (powerShellResults.Succeeded)
			{
				powerShellResults2 = this.GetObject(identity);
			}
			else
			{
				powerShellResults2 = new PowerShellResults<SetUMMailboxPinConfiguration>();
				powerShellResults2.MergeErrors(powerShellResults);
			}
			return powerShellResults2;
		}

		// Token: 0x0400278A RID: 10122
		private const string GetObjectRole = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization";

		// Token: 0x0400278B RID: 10123
		private const string SetObjectRole = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization+Set-UMMailboxPin?Identity@W:Organization";
	}
}
