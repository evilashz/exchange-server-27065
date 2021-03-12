using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200020D RID: 525
	[Cmdlet("New", "ApprovalApplication")]
	public sealed class NewApprovalApplication : NewMultitenancyFixedNameSystemConfigurationObjectTask<ApprovalApplication>
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004E6C6 File Offset: 0x0004C8C6
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0004E6DD File Offset: 0x0004C8DD
		[Parameter(Mandatory = false)]
		public RetentionPolicyTagIdParameter ELCRetentionPolicyTag
		{
			get
			{
				return (RetentionPolicyTagIdParameter)base.Fields[ApprovalApplicationSchema.ELCRetentionPolicyTag];
			}
			set
			{
				base.Fields[ApprovalApplicationSchema.ELCRetentionPolicyTag] = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x0004E716 File Offset: 0x0004C916
		[Parameter(Mandatory = true, ParameterSetName = "AutoGroup")]
		public SwitchParameter AutoGroup
		{
			get
			{
				return (SwitchParameter)(base.Fields["AutoGroup"] ?? false);
			}
			set
			{
				base.Fields["AutoGroup"] = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004E72E File Offset: 0x0004C92E
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0004E754 File Offset: 0x0004C954
		[Parameter(Mandatory = true, ParameterSetName = "ModeratedRecipients")]
		public SwitchParameter ModeratedRecipients
		{
			get
			{
				return (SwitchParameter)(base.Fields["ModeratedRecipients"] ?? false);
			}
			set
			{
				base.Fields["ModeratedRecipients"] = value;
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0004E76C File Offset: 0x0004C96C
		protected override IConfigurable PrepareDataObject()
		{
			ApprovalApplication approvalApplication = (ApprovalApplication)base.PrepareDataObject();
			approvalApplication.Name = base.ParameterSetName;
			approvalApplication.SetId(this.ConfigurationSession, approvalApplication.Name);
			if (this.ELCRetentionPolicyTag != null)
			{
				approvalApplication.ELCRetentionPolicyTag = this.ELCRetentionPolicyTag.InternalADObjectId;
			}
			return approvalApplication;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0004E7C0 File Offset: 0x0004C9C0
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
	}
}
