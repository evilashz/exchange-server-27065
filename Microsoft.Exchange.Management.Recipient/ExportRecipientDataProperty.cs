using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000B0 RID: 176
	[Cmdlet("Export", "RecipientDataProperty", DefaultParameterSetName = "ExportPicture", SupportsShouldProcess = true)]
	public sealed class ExportRecipientDataProperty : RecipientObjectActionTask<MailboxUserContactIdParameter, ADRecipient>
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002FAD0 File Offset: 0x0002DCD0
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0002FAE7 File Offset: 0x0002DCE7
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MailboxUserContactIdParameter Identity
		{
			get
			{
				return (MailboxUserContactIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002FAFA File Offset: 0x0002DCFA
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0002FB20 File Offset: 0x0002DD20
		[Parameter(ParameterSetName = "ExportPicture")]
		public SwitchParameter Picture
		{
			get
			{
				return (SwitchParameter)(base.Fields["Picture"] ?? false);
			}
			set
			{
				base.Fields["Picture"] = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002FB38 File Offset: 0x0002DD38
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0002FB5E File Offset: 0x0002DD5E
		[Parameter(ParameterSetName = "ExportSpokenName")]
		public SwitchParameter SpokenName
		{
			get
			{
				return (SwitchParameter)(base.Fields["SpokenName"] ?? false);
			}
			set
			{
				base.Fields["SpokenName"] = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002FB76 File Offset: 0x0002DD76
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportRecipientDataProperty(this.Identity.ToString());
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002FB88 File Offset: 0x0002DD88
		public ExportRecipientDataProperty()
		{
			this.data = new BinaryFileDataObject();
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002FB9C File Offset: 0x0002DD9C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Picture.IsPresent)
			{
				if (this.DataObject.ThumbnailPhoto == null)
				{
					base.WriteError(new LocalizedException(Strings.ErrorRecipientDoesNotHavePicture(this.DataObject.Name)), ErrorCategory.InvalidData, null);
					return;
				}
			}
			else if (this.SpokenName.IsPresent)
			{
				if (this.DataObject.UMSpokenName == null)
				{
					base.WriteError(new LocalizedException(Strings.ErrorRecipientDoesNotHaveSpokenName(this.DataObject.Name)), ErrorCategory.InvalidData, null);
					return;
				}
			}
			else
			{
				base.WriteError(new LocalizedException(Strings.ErrorUseDataPropertyNameParameter), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002FC38 File Offset: 0x0002DE38
		protected override void InternalProcessRecord()
		{
			this.data.SetIdentity(this.DataObject.Identity);
			if (this.Picture.IsPresent)
			{
				this.data.FileData = this.DataObject.ThumbnailPhoto;
			}
			else if (this.SpokenName.IsPresent)
			{
				this.data.FileData = this.DataObject.UMSpokenName;
			}
			base.WriteObject(this.data);
		}

		// Token: 0x04000272 RID: 626
		private BinaryFileDataObject data;
	}
}
