using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004DD RID: 1245
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MicrosoftExchangeRecipient : ADConfigurationObject
	{
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x000D9345 File Offset: 0x000D7545
		internal override ADObjectSchema Schema
		{
			get
			{
				return MicrosoftExchangeRecipient.schema;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000D934C File Offset: 0x000D754C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MicrosoftExchangeRecipient.MostDerivedClass;
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000D9353 File Offset: 0x000D7553
		internal MicrosoftExchangeRecipient(IConfigurationSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000D9380 File Offset: 0x000D7580
		public MicrosoftExchangeRecipient()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000D9394 File Offset: 0x000D7594
		public bool EmailAddressPolicyEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.EmailAddressPolicyEnabled];
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000D93A6 File Offset: 0x000D75A6
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[ADRecipientSchema.EmailAddresses];
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000D93B8 File Offset: 0x000D75B8
		public string DisplayName
		{
			get
			{
				return (string)this[ADRecipientSchema.DisplayName];
			}
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000D93CA File Offset: 0x000D75CA
		public string Alias
		{
			get
			{
				return (string)this[ADRecipientSchema.Alias];
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000D93DC File Offset: 0x000D75DC
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.HiddenFromAddressListsEnabled];
			}
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000D93EE File Offset: 0x000D75EE
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[ADRecipientSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000D9400 File Offset: 0x000D7600
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x060037DA RID: 14298 RVA: 0x000D9412 File Offset: 0x000D7612
		public ADObjectId ForwardingAddress
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.ForwardingAddress];
			}
		}

		// Token: 0x040025BA RID: 9658
		private static MicrosoftExchangeRecipientSchema schema = ObjectSchema.GetInstance<MicrosoftExchangeRecipientSchema>();

		// Token: 0x040025BB RID: 9659
		internal static string MostDerivedClass = "msExchExchangeServerRecipient";
	}
}
