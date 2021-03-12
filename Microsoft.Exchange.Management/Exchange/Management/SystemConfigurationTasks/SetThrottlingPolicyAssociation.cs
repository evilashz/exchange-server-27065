using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B25 RID: 2853
	[Cmdlet("Set", "ThrottlingPolicyAssociation", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetThrottlingPolicyAssociation : SetRecipientObjectTask<ThrottlingPolicyAssociationIdParameter, ThrottlingPolicyAssociation, ADRecipient>
	{
		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x060066C7 RID: 26311 RVA: 0x001A8F76 File Offset: 0x001A7176
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.ThrottlingPolicy == null)
				{
					return Strings.ConfirmationMessageSetThrottlingPolicyAssociationToNull(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageSetThrottlingPolicyAssociation(this.Identity.ToString(), this.ThrottlingPolicy.ToString());
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x001A8FAC File Offset: 0x001A71AC
		// (set) Token: 0x060066C9 RID: 26313 RVA: 0x001A8FC3 File Offset: 0x001A71C3
		[Parameter]
		public ThrottlingPolicyIdParameter ThrottlingPolicy
		{
			get
			{
				return (ThrottlingPolicyIdParameter)base.Fields["ThrottlingPolicy"];
			}
			set
			{
				base.Fields["ThrottlingPolicy"] = value;
			}
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x001A8FD8 File Offset: 0x001A71D8
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ADRecipient adrecipient = (ADRecipient)dataObject;
			if (this.ThrottlingPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new ArgumentException(Strings.ErrorLinkOpOnDehydratedTenant("ThrottlingPolicy")), ErrorCategory.InvalidArgument, (this.DataObject != null) ? this.DataObject.Identity : null);
				}
				ThrottlingPolicy throttlingPolicy = (ThrottlingPolicy)base.GetDataObject<ThrottlingPolicy>(this.ThrottlingPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorThrottlingPolicyNotFound(this.ThrottlingPolicy.ToString())), new LocalizedString?(Strings.ErrorThrottlingPolicyNotUnique(this.ThrottlingPolicy.ToString())));
				adrecipient.ThrottlingPolicy = (ADObjectId)throttlingPolicy.Identity;
			}
			else
			{
				adrecipient.ThrottlingPolicy = null;
			}
			base.StampChangesOn(adrecipient);
			TaskLogger.LogExit();
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x001A90A3 File Offset: 0x001A72A3
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (!base.Fields.IsModified("ThrottlingPolicy"))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorThrottlingPolicyMustBeSpecified), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x001A90E0 File Offset: 0x001A72E0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADRecipient dataObject2 = (ADRecipient)dataObject;
			return ThrottlingPolicyAssociation.FromDataObject(dataObject2);
		}
	}
}
