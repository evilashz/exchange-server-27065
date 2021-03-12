using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200001B RID: 27
	[Cmdlet("disable", "DistributionGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableDistributionGroup : RecipientObjectActionTask<DistributionGroupIdParameter, ADGroup>
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000871E File Offset: 0x0000691E
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00008726 File Offset: 0x00006926
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000872F File Offset: 0x0000692F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableDistributionGroup(this.Identity.ToString());
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008744 File Offset: 0x00006944
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			if (adgroup != null && (adgroup.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || adgroup.RecipientTypeDetails == RecipientTypeDetails.RemoteGroupMailbox))
			{
				base.WriteError(new RecipientTaskException(Strings.NotAValidDistributionGroup), ExchangeErrorCategory.Client, this.Identity.ToString());
			}
			MailboxTaskHelper.ClearExchangeProperties(adgroup, DisableDistributionGroup.PropertiesToReset);
			adgroup.SetExchangeVersion(null);
			adgroup.OverrideCorruptedValuesWithDefault();
			TaskLogger.LogExit();
			return adgroup;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000087C5 File Offset: 0x000069C5
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DistributionGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x04000032 RID: 50
		internal static readonly PropertyDefinition[] PropertiesToReset = new PropertyDefinition[]
		{
			ADRecipientSchema.AcceptMessagesOnlyFrom,
			ADRecipientSchema.AcceptMessagesOnlyFromDLMembers,
			ADRecipientSchema.AddressListMembership,
			ADRecipientSchema.Alias,
			ADRecipientSchema.EmailAddresses,
			ADGroupSchema.ExpansionServer,
			ADRecipientSchema.GrantSendOnBehalfTo,
			ADRecipientSchema.HiddenFromAddressListsEnabled,
			ADRecipientSchema.HomeMTA,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.MaxReceiveSize,
			ADRecipientSchema.MaxSendSize,
			ADRecipientSchema.PoliciesExcluded,
			ADRecipientSchema.PoliciesIncluded,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.RecipientTypeDetails,
			ADRecipientSchema.RejectMessagesFrom,
			ADRecipientSchema.RejectMessagesFromDLMembers,
			ADRecipientSchema.RequireAllSendersAreAuthenticated,
			ADGroupSchema.ReportToManagerEnabled,
			ADGroupSchema.ReportToOriginatorEnabled,
			ADGroupSchema.SendDeliveryReportsTo,
			ADGroupSchema.SendOofMessageToOriginatorEnabled,
			ADRecipientSchema.SimpleDisplayName,
			ADRecipientSchema.TextEncodedORAddress,
			ADRecipientSchema.WindowsEmailAddress,
			ADRecipientSchema.CustomAttribute1,
			ADRecipientSchema.CustomAttribute2,
			ADRecipientSchema.CustomAttribute3,
			ADRecipientSchema.CustomAttribute4,
			ADRecipientSchema.CustomAttribute5,
			ADRecipientSchema.CustomAttribute6,
			ADRecipientSchema.CustomAttribute7,
			ADRecipientSchema.CustomAttribute8,
			ADRecipientSchema.CustomAttribute9,
			ADRecipientSchema.CustomAttribute10,
			ADRecipientSchema.CustomAttribute11,
			ADRecipientSchema.CustomAttribute12,
			ADRecipientSchema.CustomAttribute13,
			ADRecipientSchema.CustomAttribute14,
			ADRecipientSchema.CustomAttribute15,
			ADRecipientSchema.ExtensionCustomAttribute1,
			ADRecipientSchema.ExtensionCustomAttribute2,
			ADRecipientSchema.ExtensionCustomAttribute3,
			ADRecipientSchema.ExtensionCustomAttribute4,
			ADRecipientSchema.ExtensionCustomAttribute5
		};
	}
}
