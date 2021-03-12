using System;
using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000E RID: 14
	[Cmdlet("get", "attachmentfilterentry")]
	public class GetAttachmentFilterEntry : GetSingletonSystemConfigurationObjectTask<AttachmentFilteringConfig>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003678 File Offset: 0x00001878
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000368F File Offset: 0x0000188F
		[Parameter(Mandatory = false, Position = 0)]
		public string Identity
		{
			internal get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000036A4 File Offset: 0x000018A4
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
			string text = (string)base.Fields["Identity"];
			try
			{
				if (string.IsNullOrEmpty(text))
				{
					using (MultiValuedProperty<string>.Enumerator enumerator = attachmentFilteringConfig.AttachmentNames.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string attachmentName = enumerator.Current;
							this.WriteOutput(attachmentName);
						}
						goto IL_D6;
					}
				}
				foreach (string text2 in attachmentFilteringConfig.AttachmentNames)
				{
					if (string.Equals(text, text2, StringComparison.InvariantCultureIgnoreCase))
					{
						this.WriteOutput(text2);
						return;
					}
				}
				base.WriteError(new ArgumentException(Strings.AddressRewriteIdentityNotFound, "Identity"), ErrorCategory.InvalidArgument, null);
				IL_D6:;
			}
			catch (ArgumentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (InvalidDataException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000037E8 File Offset: 0x000019E8
		private void WriteOutput(string attachmentName)
		{
			AttachmentFilterEntrySpecification attachmentFilterEntrySpecification = AttachmentFilterEntrySpecification.Parse(attachmentName);
			if (attachmentFilterEntrySpecification.Type == AttachmentType.FileName)
			{
				try
				{
					string text;
					Regex regex;
					string text2;
					AttachmentFilterEntrySpecification.ParseFileSpec(attachmentFilterEntrySpecification.Name, out text, out regex, out text2);
				}
				catch (InvalidDataException ex)
				{
					base.WriteWarning(ex.Message);
				}
			}
			this.WriteResult(attachmentFilterEntrySpecification);
		}
	}
}
