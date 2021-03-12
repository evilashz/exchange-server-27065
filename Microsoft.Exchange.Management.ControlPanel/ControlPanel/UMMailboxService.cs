using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004D0 RID: 1232
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class UMMailboxService : DataSourceService, IUMMailboxService, IEditObjectService<SetUMMailboxConfiguration, SetUMMailboxParameters>, IGetObjectService<SetUMMailboxConfiguration>, INewGetObjectService<UMMailboxFeatureInfo, NewUMMailboxParameters, RecipientRow>, INewObjectService<UMMailboxFeatureInfo, NewUMMailboxParameters>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06003C5E RID: 15454 RVA: 0x000B53FC File Offset: 0x000B35FC
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity@R:Organization")]
		public PowerShellResults<RecipientRow> GetObjectForNew(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Get-Recipient");
			pscommand.AddParameter("Identity", identity);
			return base.GetObject<RecipientRow>(pscommand);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000B545C File Offset: 0x000B365C
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-UMMailbox?Identity&UMMailboxPolicy&ValidateOnly&Extensions@W:Organization+Get-UMMailboxPolicy?Identity@R:Organization")]
		public PowerShellResults<NewUMMailboxConfiguration> GetConfigurationForNewUMMailbox(Identity identity, UMEnableSelectedPolicyParameters properties)
		{
			properties.FaultIfNull();
			PSCommand pscommand = new PSCommand().AddCommand("Enable-UMMailbox");
			pscommand.AddParameter("Identity", identity);
			pscommand.AddParameter("ValidateOnly");
			if (properties.UMMailboxPolicy != null)
			{
				pscommand.AddParameter("UMMailboxPolicy", properties.UMMailboxPolicy);
			}
			PowerShellResults<NewUMMailboxConfiguration> powerShellResults = base.Invoke<NewUMMailboxConfiguration>(pscommand);
			if (!powerShellResults.Succeeded)
			{
				powerShellResults.ErrorRecords = Array.FindAll<ErrorRecord>(powerShellResults.ErrorRecords, (ErrorRecord x) => !(x.Exception is CouldNotGenerateExtensionException) && !(x.Exception is SipResourceIdAndExtensionsNeededException) && !(x.Exception is E164ResourceIdNeededException));
			}
			if (powerShellResults.SucceededWithValue)
			{
				PowerShellResults<UMMailboxPolicy> powerShellResults2 = powerShellResults.MergeErrors<UMMailboxPolicy>(base.GetObject<UMMailboxPolicy>("Get-UMMailboxPolicy", properties.UMMailboxPolicy));
				if (powerShellResults2.SucceededWithValue)
				{
					powerShellResults.Value.Policy = powerShellResults2.Value;
				}
			}
			return powerShellResults;
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000B5530 File Offset: 0x000B3730
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-UMMailbox?Identity&UMMailboxPolicy&ValidateOnly&Extensions@W:Organization")]
		public PowerShellResults<UMMailboxFeatureInfo> NewObject(NewUMMailboxParameters properties)
		{
			PowerShellResults<UMMailboxFeatureInfo> powerShellResults = base.NewObject<UMMailboxFeatureInfo, NewUMMailboxParameters>("Enable-UMMailbox", properties);
			PowerShellResults<SetUMMailboxConfiguration> @object = this.GetObject(properties.Identity);
			if (powerShellResults.SucceededWithValue && @object.SucceededWithValue)
			{
				powerShellResults.Output[0].WhenChanged = @object.Output[0].WhenChanged;
			}
			return powerShellResults;
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000B5584 File Offset: 0x000B3784
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization")]
		public PowerShellResults<SetUMMailboxConfiguration> GetObject(Identity identity)
		{
			PowerShellResults<SetUMMailboxConfiguration> @object = base.GetObject<SetUMMailboxConfiguration>("Get-UMMailbox", identity);
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

		// Token: 0x06003C62 RID: 15458 RVA: 0x000B55D4 File Offset: 0x000B37D4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization+Set-Mailbox?Identity&EmailAddresses@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization+Set-UMMailbox?Identity@W:Organization")]
		public PowerShellResults<SetUMMailboxConfiguration> SetObject(Identity identity, SetUMMailboxParameters properties)
		{
			PowerShellResults<SetUMMailboxConfiguration> powerShellResults = new PowerShellResults<SetUMMailboxConfiguration>();
			properties.FaultIfNull();
			properties.ReturnObjectType = ReturnObjectTypes.Full;
			if (properties.SetUMExtensionParameteres.SecondaryExtensions != null)
			{
				PowerShellResults<SetUMMailboxConfiguration> @object = this.GetObject(identity);
				powerShellResults.MergeErrors<SetUMMailboxConfiguration>(@object);
				if (@object.HasValue)
				{
					try
					{
						properties.SetUMExtensionParameteres.UpdateSecondaryExtensions(@object.Output[0].UMMailbox);
					}
					catch (ProxyAddressExistsException exception)
					{
						powerShellResults.ErrorRecords = new ErrorRecord[]
						{
							new ErrorRecord(exception)
						};
						return powerShellResults;
					}
				}
				PSCommand pscommand = new PSCommand().AddCommand("Set-Mailbox");
				pscommand.AddParameter("Identity", identity);
				pscommand.AddParameters(properties.SetUMExtensionParameteres);
				powerShellResults.MergeErrors(base.Invoke(pscommand));
				if (powerShellResults.Failed)
				{
					return powerShellResults;
				}
			}
			return base.SetObject<SetUMMailboxConfiguration, SetUMMailboxParameters>("Set-UMMailbox", identity, properties);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000B56B8 File Offset: 0x000B38B8
		[PrincipalPermission(SecurityAction.Demand, Role = "Disable-UMMailbox?Identity&Confirm@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Disable-UMMailbox", identities, parameters);
		}

		// Token: 0x04002798 RID: 10136
		private const string GetDataForNewUMMailboxRole = "Get-Recipient?Identity@R:Organization";

		// Token: 0x04002799 RID: 10137
		private const string NewUMMailboxRole = "Enable-UMMailbox?Identity&UMMailboxPolicy&ValidateOnly&Extensions@W:Organization";

		// Token: 0x0400279A RID: 10138
		private const string GetConfigurationForNewUMMailboxRole = "Enable-UMMailbox?Identity&UMMailboxPolicy&ValidateOnly&Extensions@W:Organization+Get-UMMailboxPolicy?Identity@R:Organization";

		// Token: 0x0400279B RID: 10139
		private const string GetUMMailboxRole = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization";

		// Token: 0x0400279C RID: 10140
		private const string SetUMMailboxRole = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization+Set-UMMailbox?Identity@W:Organization";

		// Token: 0x0400279D RID: 10141
		private const string SetMailboxRole = "Get-UMMailbox?Identity@R:Organization+Get-UMMailboxPin?Identity@R:Organization+Set-Mailbox?Identity&EmailAddresses@W:Organization";

		// Token: 0x0400279E RID: 10142
		internal const string RemoveUMMailboxRole = "Disable-UMMailbox?Identity&Confirm@W:Organization";
	}
}
