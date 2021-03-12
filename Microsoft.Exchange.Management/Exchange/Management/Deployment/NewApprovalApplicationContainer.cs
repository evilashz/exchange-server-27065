using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200020E RID: 526
	[Cmdlet("New", "ApprovalApplicationContainer")]
	public sealed class NewApprovalApplicationContainer : NewMultitenancyFixedNameSystemConfigurationObjectTask<ApprovalApplicationContainer>
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004E821 File Offset: 0x0004CA21
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x0004E838 File Offset: 0x0004CA38
		[Parameter]
		public MailboxPolicyIdParameter RetentionPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["RetentionPolicy"];
			}
			set
			{
				base.Fields["RetentionPolicy"] = value;
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004E84C File Offset: 0x0004CA4C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.RetentionPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new LocalizedException(Strings.ErrorLinkOpOnDehydratedTenant("RetentionPolicy")), ExchangeErrorCategory.Client, null);
				}
				RetentionPolicy retentionPolicy = (RetentionPolicy)base.GetDataObject<RetentionPolicy>(this.RetentionPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRetentionPolicyNotFound(this.RetentionPolicy.ToString())), new LocalizedString?(Strings.ErrorRetentionPolicyNotUnique(this.RetentionPolicy.ToString())));
				this.retentionPolicyId = (ADObjectId)retentionPolicy.Identity;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0004E8F0 File Offset: 0x0004CAF0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ApprovalApplicationContainer approvalApplicationContainer = (ApprovalApplicationContainer)base.PrepareDataObject();
			approvalApplicationContainer.Name = ApprovalApplicationContainer.DefaultName;
			approvalApplicationContainer.SetId(this.ConfigurationSession, approvalApplicationContainer.Name);
			if (base.Fields.IsModified("RetentionPolicy") && this.retentionPolicyId != null)
			{
				approvalApplicationContainer.RetentionPolicy = this.retentionPolicyId;
			}
			TaskLogger.LogExit();
			return approvalApplicationContainer;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0004E958 File Offset: 0x0004CB58
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADRawEntry adrawEntry = ((IDirectorySession)base.DataSession).ReadADRawEntry(this.DataObject.Id, new PropertyDefinition[0]);
			if (adrawEntry != null)
			{
				base.WriteVerbose(Strings.VerboseApprovalApplicationObjectExists(this.DataObject.Name));
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x040007BE RID: 1982
		private ADObjectId retentionPolicyId;
	}
}
