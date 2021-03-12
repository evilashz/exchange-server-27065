using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x02000010 RID: 16
	[Cmdlet("remove", "attachmentfilterentry", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveAttachmentFilterEntry : SingletonSystemConfigurationObjectActionTask<AttachmentFilteringConfig>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003862 File Offset: 0x00001A62
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003879 File Offset: 0x00001A79
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public string Identity
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000388C File Offset: 0x00001A8C
		protected override ObjectId RootId
		{
			get
			{
				return ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000389E File Offset: 0x00001A9E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAttachmentfilterentry(this.Identity.ToString());
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000038B0 File Offset: 0x00001AB0
		protected override void InternalProcessRecord()
		{
			AttachmentFilteringConfig attachmentFilteringConfig = null;
			try
			{
				attachmentFilteringConfig = AFilterUtils.GetAFilterConfig(base.DataSession);
			}
			catch (AttachmentFilterADEntryNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				return;
			}
			string identity = this.Identity;
			string[] array = attachmentFilteringConfig.AttachmentNames.ToArray();
			foreach (string text in array)
			{
				if (text.Equals(identity, StringComparison.InvariantCultureIgnoreCase))
				{
					attachmentFilteringConfig.AttachmentNames.Remove(text);
					base.DataSession.Save(attachmentFilteringConfig);
					return;
				}
			}
			base.WriteError(new ArgumentException(Strings.AttachmentFilterEntryNotFound, "AttachmentFilterEntry"), ErrorCategory.InvalidArgument, null);
		}
	}
}
