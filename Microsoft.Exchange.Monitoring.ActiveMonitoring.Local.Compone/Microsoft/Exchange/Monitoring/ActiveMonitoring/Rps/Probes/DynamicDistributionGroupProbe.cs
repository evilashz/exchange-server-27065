using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps.Probes
{
	// Token: 0x0200043C RID: 1084
	public class DynamicDistributionGroupProbe : RpsTipProbeBase
	{
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0009D5B0 File Offset: 0x0009B7B0
		protected override void ExecuteTipScenarioes(PowerShell powershell)
		{
			string uniqueName = base.GetUniqueName("DynamicDG");
			Collection<PSObject> collection = base.ExecuteCmdlet(powershell, new Command("New-DynamicDistributionGroup")
			{
				Parameters = 
				{
					{
						"Name",
						uniqueName
					},
					{
						"IncludedRecipients",
						"MailboxUsers"
					}
				}
			});
			if (collection.Count <= 0)
			{
				throw new ApplicationException("New-DynamicDistributionGroup didn't return any result");
			}
			try
			{
				string text = "Rename_" + uniqueName;
				collection = base.ExecuteCmdlet(powershell, new Command("Set-DynamicDistributionGroup")
				{
					Parameters = 
					{
						{
							"Identity",
							uniqueName
						},
						{
							"DisplayName",
							text
						}
					}
				});
				collection = base.ExecuteCmdlet(powershell, new Command("Get-DynamicDistributionGroup")
				{
					Parameters = 
					{
						{
							"Identity",
							uniqueName
						}
					}
				});
				if (collection.Count <= 0)
				{
					throw new ApplicationException("Get-DynamicDistributionGroup didn't return any result");
				}
				string text2 = (string)collection[0].Properties["DisplayName"].Value;
				if (string.Compare(text, text2, true) != 0)
				{
					throw new ApplicationException(string.Format("DisplayName is not set successfully. Expected:{0} Actual:{1}", text, text2));
				}
			}
			finally
			{
				base.RemoveObject(powershell, "DynamicDistributionGroup", uniqueName);
			}
		}
	}
}
