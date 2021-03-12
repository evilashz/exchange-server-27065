using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x02000012 RID: 18
	[Cmdlet("set", "attachmentfilteringconfigdefaults")]
	public class SetAttachmentFilteringConfigDefaults : SetSingletonSystemConfigurationObjectTask<AttachmentFilteringConfig>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003BB4 File Offset: 0x00001DB4
		protected override ObjectId RootId
		{
			get
			{
				return ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003BC6 File Offset: 0x00001DC6
		protected override void InternalProcessRecord()
		{
			this.DataObject.AdminMessage = DirectoryStrings.AttachmentsWereRemovedMessage;
			this.DataObject.RejectResponse = "Message rejected due to unacceptable attachments";
			base.InternalProcessRecord();
		}
	}
}
