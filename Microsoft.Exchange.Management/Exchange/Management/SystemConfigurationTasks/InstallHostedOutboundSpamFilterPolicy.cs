using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A54 RID: 2644
	[Cmdlet("Install", "HostedOutboundSpamFilterPolicy")]
	public sealed class InstallHostedOutboundSpamFilterPolicy : NewMultitenancySystemConfigurationObjectTask<HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06005EC0 RID: 24256 RVA: 0x0018CC5C File Offset: 0x0018AE5C
		// (set) Token: 0x06005EC1 RID: 24257 RVA: 0x0018CC64 File Offset: 0x0018AE64
		[Parameter]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x06005EC2 RID: 24258 RVA: 0x0018CC6D File Offset: 0x0018AE6D
		// (set) Token: 0x06005EC3 RID: 24259 RVA: 0x0018CC75 File Offset: 0x0018AE75
		[Parameter(Mandatory = true, Position = 0)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x06005EC4 RID: 24260 RVA: 0x0018CC7E File Offset: 0x0018AE7E
		// (set) Token: 0x06005EC5 RID: 24261 RVA: 0x0018CC8B File Offset: 0x0018AE8B
		[Parameter]
		public MultiValuedProperty<SmtpAddress> NotifyOutboundSpamRecipients
		{
			get
			{
				return this.DataObject.NotifyOutboundSpamRecipients;
			}
			set
			{
				this.DataObject.NotifyOutboundSpamRecipients = value;
			}
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x06005EC6 RID: 24262 RVA: 0x0018CC99 File Offset: 0x0018AE99
		// (set) Token: 0x06005EC7 RID: 24263 RVA: 0x0018CCA6 File Offset: 0x0018AEA6
		[Parameter]
		public MultiValuedProperty<SmtpAddress> BccSuspiciousOutboundAdditionalRecipients
		{
			get
			{
				return this.DataObject.BccSuspiciousOutboundAdditionalRecipients;
			}
			set
			{
				this.DataObject.BccSuspiciousOutboundAdditionalRecipients = value;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x06005EC8 RID: 24264 RVA: 0x0018CCB4 File Offset: 0x0018AEB4
		// (set) Token: 0x06005EC9 RID: 24265 RVA: 0x0018CCC1 File Offset: 0x0018AEC1
		[Parameter]
		public bool BccSuspiciousOutboundMail
		{
			get
			{
				return this.DataObject.BccSuspiciousOutboundMail;
			}
			set
			{
				this.DataObject.BccSuspiciousOutboundMail = value;
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06005ECA RID: 24266 RVA: 0x0018CCCF File Offset: 0x0018AECF
		// (set) Token: 0x06005ECB RID: 24267 RVA: 0x0018CCDC File Offset: 0x0018AEDC
		[Parameter]
		public bool NotifyOutboundSpam
		{
			get
			{
				return this.DataObject.NotifyOutboundSpam;
			}
			set
			{
				this.DataObject.NotifyOutboundSpam = value;
			}
		}

		// Token: 0x06005ECC RID: 24268 RVA: 0x0018CCEC File Offset: 0x0018AEEC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			HostedOutboundSpamFilterPolicy hostedOutboundSpamFilterPolicy = (HostedOutboundSpamFilterPolicy)base.PrepareDataObject();
			hostedOutboundSpamFilterPolicy.SetId((IConfigurationSession)base.DataSession, this.Name);
			TaskLogger.LogExit();
			return hostedOutboundSpamFilterPolicy;
		}

		// Token: 0x06005ECD RID: 24269 RVA: 0x0018CD28 File Offset: 0x0018AF28
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<HostedOutboundSpamFilterPolicy>(this, this.DataObject, null);
			TaskLogger.LogExit();
		}
	}
}
