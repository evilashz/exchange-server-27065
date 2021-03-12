using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class MailMessage : ConfigurableObject
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000268D File Offset: 0x0000088D
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000026A4 File Offset: 0x000008A4
		[Parameter]
		public string Subject
		{
			get
			{
				return (string)this.propertyBag[MailMessageSchema.Subject];
			}
			set
			{
				this.propertyBag[MailMessageSchema.Subject] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000026B7 File Offset: 0x000008B7
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000026CE File Offset: 0x000008CE
		[Parameter]
		public string Body
		{
			get
			{
				return (string)this.propertyBag[MailMessageSchema.Body];
			}
			set
			{
				this.propertyBag[MailMessageSchema.Body] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000026E1 File Offset: 0x000008E1
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000026F8 File Offset: 0x000008F8
		[Parameter]
		public MailBodyFormat BodyFormat
		{
			get
			{
				return (MailBodyFormat)this.propertyBag[MailMessageSchema.BodyFormat];
			}
			set
			{
				this.propertyBag[MailMessageSchema.BodyFormat] = value;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002710 File Offset: 0x00000910
		internal void SetIdentity(ObjectId id)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = id;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002724 File Offset: 0x00000924
		public MailMessage() : base(new MailMessagePropertyBag())
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002731 File Offset: 0x00000931
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailMessage.s_schema;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002738 File Offset: 0x00000938
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000017 RID: 23
		private static ObjectSchema s_schema = ObjectSchema.GetInstance<MailMessageSchema>();
	}
}
