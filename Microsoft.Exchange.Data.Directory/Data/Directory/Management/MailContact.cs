using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000739 RID: 1849
	[ProvisioningObjectTag("MailContact")]
	[Serializable]
	public class MailContact : MailEnabledOrgPerson, IExternalAndEmailAddresses
	{
		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x0600596C RID: 22892 RVA: 0x0013C013 File Offset: 0x0013A213
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MailContact.schema;
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x0600596D RID: 22893 RVA: 0x0013C01A File Offset: 0x0013A21A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x0013C021 File Offset: 0x0013A221
		public MailContact()
		{
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x0013C029 File Offset: 0x0013A229
		public MailContact(ADContact dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x0013C032 File Offset: 0x0013A232
		internal static MailContact FromDataObject(ADContact dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new MailContact(dataObject);
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x06005971 RID: 22897 RVA: 0x0013C03F File Offset: 0x0013A23F
		// (set) Token: 0x06005972 RID: 22898 RVA: 0x0013C051 File Offset: 0x0013A251
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[MailContactSchema.ExternalEmailAddress];
			}
			set
			{
				this[MailContactSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x0013C05F File Offset: 0x0013A25F
		// (set) Token: 0x06005974 RID: 22900 RVA: 0x0013C071 File Offset: 0x0013A271
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxRecipientPerMessage
		{
			get
			{
				return (Unlimited<int>)this[MailContactSchema.MaxRecipientPerMessage];
			}
			set
			{
				this[MailContactSchema.MaxRecipientPerMessage] = value;
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06005975 RID: 22901 RVA: 0x0013C084 File Offset: 0x0013A284
		// (set) Token: 0x06005976 RID: 22902 RVA: 0x0013C096 File Offset: 0x0013A296
		[Parameter(Mandatory = false)]
		public UseMapiRichTextFormat UseMapiRichTextFormat
		{
			get
			{
				return (UseMapiRichTextFormat)this[MailContactSchema.UseMapiRichTextFormat];
			}
			set
			{
				this[MailContactSchema.UseMapiRichTextFormat] = value;
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x0013C0A9 File Offset: 0x0013A2A9
		// (set) Token: 0x06005978 RID: 22904 RVA: 0x0013C0BB File Offset: 0x0013A2BB
		[Parameter(Mandatory = false)]
		public bool UsePreferMessageFormat
		{
			get
			{
				return (bool)this[MailContactSchema.UsePreferMessageFormat];
			}
			set
			{
				this[MailContactSchema.UsePreferMessageFormat] = value;
			}
		}

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06005979 RID: 22905 RVA: 0x0013C0CE File Offset: 0x0013A2CE
		// (set) Token: 0x0600597A RID: 22906 RVA: 0x0013C0E0 File Offset: 0x0013A2E0
		[Parameter(Mandatory = false)]
		public MessageFormat MessageFormat
		{
			get
			{
				return (MessageFormat)this[MailContactSchema.MessageFormat];
			}
			set
			{
				this[MailContactSchema.MessageFormat] = value;
			}
		}

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x0600597B RID: 22907 RVA: 0x0013C0F3 File Offset: 0x0013A2F3
		// (set) Token: 0x0600597C RID: 22908 RVA: 0x0013C105 File Offset: 0x0013A305
		[Parameter(Mandatory = false)]
		public MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return (MessageBodyFormat)this[MailContactSchema.MessageBodyFormat];
			}
			set
			{
				this[MailContactSchema.MessageBodyFormat] = value;
			}
		}

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x0600597D RID: 22909 RVA: 0x0013C118 File Offset: 0x0013A318
		// (set) Token: 0x0600597E RID: 22910 RVA: 0x0013C12A File Offset: 0x0013A32A
		[Parameter(Mandatory = false)]
		public MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return (MacAttachmentFormat)this[MailContactSchema.MacAttachmentFormat];
			}
			set
			{
				this[MailContactSchema.MacAttachmentFormat] = value;
			}
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x0600597F RID: 22911 RVA: 0x0013C13D File Offset: 0x0013A33D
		// (set) Token: 0x06005980 RID: 22912 RVA: 0x0013C14F File Offset: 0x0013A34F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailContactSchema.UserCertificate];
			}
			set
			{
				this[MailContactSchema.UserCertificate] = value;
			}
		}

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x06005981 RID: 22913 RVA: 0x0013C15D File Offset: 0x0013A35D
		// (set) Token: 0x06005982 RID: 22914 RVA: 0x0013C16F File Offset: 0x0013A36F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserSMimeCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailContactSchema.UserSMimeCertificate];
			}
			set
			{
				this[MailContactSchema.UserSMimeCertificate] = value;
			}
		}

		// Token: 0x04003C12 RID: 15378
		private static MailContactSchema schema = ObjectSchema.GetInstance<MailContactSchema>();
	}
}
