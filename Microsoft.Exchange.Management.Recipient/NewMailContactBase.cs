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
	// Token: 0x0200007C RID: 124
	public abstract class NewMailContactBase : NewMailEnabledRecipientObjectTask<ADContact>
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00026AE6 File Offset: 0x00024CE6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailContact(base.Name.ToString(), this.ExternalEmailAddress.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00026B0E File Offset: 0x00024D0E
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x00026B1B File Offset: 0x00024D1B
		[Parameter(Mandatory = true)]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return this.DataObject.ExternalEmailAddress;
			}
			set
			{
				this.DataObject.ExternalEmailAddress = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00026B29 File Offset: 0x00024D29
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00026B36 File Offset: 0x00024D36
		[Parameter]
		public string FirstName
		{
			get
			{
				return this.DataObject.FirstName;
			}
			set
			{
				this.DataObject.FirstName = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00026B44 File Offset: 0x00024D44
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x00026B51 File Offset: 0x00024D51
		[Parameter]
		public string Initials
		{
			get
			{
				return this.DataObject.Initials;
			}
			set
			{
				this.DataObject.Initials = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00026B5F File Offset: 0x00024D5F
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x00026B6C File Offset: 0x00024D6C
		[Parameter]
		public string LastName
		{
			get
			{
				return this.DataObject.LastName;
			}
			set
			{
				this.DataObject.LastName = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00026B7A File Offset: 0x00024D7A
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x00026B87 File Offset: 0x00024D87
		[Parameter(Mandatory = false)]
		public bool UsePreferMessageFormat
		{
			get
			{
				return this.DataObject.UsePreferMessageFormat;
			}
			set
			{
				this.DataObject.UsePreferMessageFormat = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00026B95 File Offset: 0x00024D95
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00026BA2 File Offset: 0x00024DA2
		[Parameter(Mandatory = false)]
		public MessageFormat MessageFormat
		{
			get
			{
				return this.DataObject.MessageFormat;
			}
			set
			{
				this.DataObject.MessageFormat = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00026BB0 File Offset: 0x00024DB0
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00026BBD File Offset: 0x00024DBD
		[Parameter(Mandatory = false)]
		public MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return this.DataObject.MessageBodyFormat;
			}
			set
			{
				this.DataObject.MessageBodyFormat = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00026BCB File Offset: 0x00024DCB
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00026BD8 File Offset: 0x00024DD8
		[Parameter(Mandatory = false)]
		public MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return this.DataObject.MacAttachmentFormat;
			}
			set
			{
				this.DataObject.MacAttachmentFormat = value;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00026BE6 File Offset: 0x00024DE6
		public NewMailContactBase()
		{
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00026BEE File Offset: 0x00024DEE
		protected override void StampDefaultValues(ADContact dataObject)
		{
			base.StampDefaultValues(dataObject);
			dataObject.StampDefaultValues(RecipientType.MailContact);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00026BFE File Offset: 0x00024DFE
		protected override void PrepareRecipientObject(ADContact dataObject)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(dataObject);
			MailContactTaskHelper.ValidateExternalEmailAddress(dataObject, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			dataObject.UseMapiRichTextFormat = UseMapiRichTextFormat.Never;
			TaskLogger.LogExit();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00026C36 File Offset: 0x00024E36
		protected override void InternalValidate()
		{
			base.InternalValidate();
			DistributionGroupTaskHelper.CheckModerationInMixedEnvironment(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), Strings.WarningLegacyExchangeServerForMailContact);
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00026C5B File Offset: 0x00024E5B
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(MailContact).FullName;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00026C6C File Offset: 0x00024E6C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailContact.FromDataObject((ADContact)dataObject);
		}
	}
}
