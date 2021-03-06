using System;
using System.Collections.Generic;
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
	// Token: 0x02000078 RID: 120
	[Cmdlet("Enable", "MailContact", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnableMailContact : EnableRecipientObjectTask<ContactIdParameter, ADContact>
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000265FC File Offset: 0x000247FC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableMailContact(this.Identity.ToString(), this.ExternalEmailAddress.ToString());
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00026619 File Offset: 0x00024819
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00026630 File Offset: 0x00024830
		[Parameter(Mandatory = true)]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)base.Fields[MailContactSchema.ExternalEmailAddress];
			}
			set
			{
				base.Fields[MailContactSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00026643 File Offset: 0x00024843
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x00026664 File Offset: 0x00024864
		[Parameter(Mandatory = false)]
		public bool UsePreferMessageFormat
		{
			get
			{
				return (bool)(base.Fields[ADRecipientSchema.UsePreferMessageFormat] ?? false);
			}
			set
			{
				base.Fields[ADRecipientSchema.UsePreferMessageFormat] = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0002667C File Offset: 0x0002487C
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x000266A1 File Offset: 0x000248A1
		[Parameter(Mandatory = false)]
		public MessageFormat MessageFormat
		{
			get
			{
				return (MessageFormat)(base.Fields[ADRecipientSchema.MessageFormat] ?? MessageFormat.Mime);
			}
			set
			{
				base.Fields[ADRecipientSchema.MessageFormat] = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000266B9 File Offset: 0x000248B9
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x000266DE File Offset: 0x000248DE
		[Parameter(Mandatory = false)]
		public MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return (MessageBodyFormat)(base.Fields[ADRecipientSchema.MessageBodyFormat] ?? MessageBodyFormat.TextAndHtml);
			}
			set
			{
				base.Fields[ADRecipientSchema.MessageBodyFormat] = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000266F6 File Offset: 0x000248F6
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00026717 File Offset: 0x00024917
		[Parameter(Mandatory = false)]
		public MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return (MacAttachmentFormat)(base.Fields[ADRecipientSchema.MacAttachmentFormat] ?? MacAttachmentFormat.BinHex);
			}
			set
			{
				base.Fields[ADRecipientSchema.MacAttachmentFormat] = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002672F File Offset: 0x0002492F
		protected override bool DelayProvisioning
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002673C File Offset: 0x0002493C
		protected override void PrepareRecipientObject(ref ADContact contact)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(ref contact);
			if (RecipientType.Contact != contact.RecipientType)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidRecipientType(this.Identity.ToString(), contact.RecipientType.ToString())), ErrorCategory.InvalidArgument, contact.Id);
			}
			contact.SetExchangeVersion(contact.MaximumSupportedExchangeObjectVersion);
			List<PropertyDefinition> list = new List<PropertyDefinition>(DisableMailContact.PropertiesToReset);
			MailboxTaskHelper.RemovePersistentProperties(list);
			MailboxTaskHelper.ClearExchangeProperties(contact, list);
			contact.SetExchangeVersion(contact.MaximumSupportedExchangeObjectVersion);
			if (this.DelayProvisioning && base.IsProvisioningLayerAvailable)
			{
				this.ProvisionDefaultValues(new ADContact(), contact);
			}
			contact.ExternalEmailAddress = this.ExternalEmailAddress;
			contact.DeliverToForwardingAddress = false;
			contact.UsePreferMessageFormat = this.UsePreferMessageFormat;
			contact.MessageFormat = this.MessageFormat;
			contact.MessageBodyFormat = this.MessageBodyFormat;
			contact.MacAttachmentFormat = this.MacAttachmentFormat;
			contact.RequireAllSendersAreAuthenticated = false;
			contact.UseMapiRichTextFormat = UseMapiRichTextFormat.UseDefaultSettings;
			contact.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.RemoteMailUser);
			MailContactTaskHelper.ValidateExternalEmailAddress(contact, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			TaskLogger.LogExit();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002686F File Offset: 0x00024A6F
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			this.WriteResult();
			TaskLogger.LogExit();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00026888 File Offset: 0x00024A88
		private void WriteResult()
		{
			TaskLogger.LogEnter();
			MailContact sendToPipeline = new MailContact(this.DataObject);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000268B2 File Offset: 0x00024AB2
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailContact.FromDataObject((ADContact)dataObject);
		}
	}
}
