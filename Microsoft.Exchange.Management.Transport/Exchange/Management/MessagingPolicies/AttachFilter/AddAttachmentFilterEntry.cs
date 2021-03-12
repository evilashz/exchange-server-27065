using System;
using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000C RID: 12
	[Cmdlet("add", "attachmentfilterentry", SupportsShouldProcess = true)]
	public class AddAttachmentFilterEntry : SingletonSystemConfigurationObjectActionTask<AttachmentFilteringConfig>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003471 File Offset: 0x00001671
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddAttachmentfilterentry(this.Name.ToString(), this.Type.ToString());
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003493 File Offset: 0x00001693
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000034AA File Offset: 0x000016AA
		[Parameter(Mandatory = true)]
		public AttachmentType Type
		{
			get
			{
				return (AttachmentType)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000034C2 File Offset: 0x000016C2
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000034D9 File Offset: 0x000016D9
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000034EC File Offset: 0x000016EC
		protected override ObjectId RootId
		{
			get
			{
				return ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003500 File Offset: 0x00001700
		protected override void InternalValidate()
		{
			if (this.Type == AttachmentType.FileName)
			{
				try
				{
					string text;
					Regex regex;
					string text2;
					AttachmentFilterEntrySpecification.ParseFileSpec(this.Name, out text, out regex, out text2);
				}
				catch (InvalidDataException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003548 File Offset: 0x00001748
		protected override void InternalProcessRecord()
		{
			AttachmentFilteringConfig afilterConfig = AFilterUtils.GetAFilterConfig(base.DataSession);
			string text = this.Type.ToString() + ":" + this.Name;
			string[] array = afilterConfig.AttachmentNames.ToArray();
			foreach (string text2 in array)
			{
				if (text2.Equals(text, StringComparison.InvariantCultureIgnoreCase))
				{
					base.WriteError(new ArgumentException(Strings.AttachmentFilterEntryExists, "AttachmentFilterEntry"), ErrorCategory.InvalidArgument, null);
				}
			}
			afilterConfig.AttachmentNames.Add(text);
			base.DataSession.Save(afilterConfig);
			base.WriteObject(new AttachmentFilterEntrySpecification(this.Type, this.Name));
		}
	}
}
