using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000598 RID: 1432
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddressDictionaryEntryType
	{
		// Token: 0x0600285F RID: 10335 RVA: 0x000AB7A6 File Offset: 0x000A99A6
		[OnDeserializing]
		public void Initialize(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000AB7AE File Offset: 0x000A99AE
		public static implicit operator EmailAddressWrapper(EmailAddressDictionaryEntryType entry)
		{
			return entry.emailAddress;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000AB7B6 File Offset: 0x000A99B6
		public EmailAddressDictionaryEntryType()
		{
			this.Initialize();
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000AB7C4 File Offset: 0x000A99C4
		public EmailAddressDictionaryEntryType(EmailAddressKeyType key, string emailAddress)
		{
			this.Initialize();
			this.Key = key;
			this.emailAddress.EmailAddress = emailAddress;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000AB7E5 File Offset: 0x000A99E5
		private void Initialize()
		{
			this.emailAddress = new EmailAddressWrapper();
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x000AB7F2 File Offset: 0x000A99F2
		// (set) Token: 0x06002865 RID: 10341 RVA: 0x000AB7FA File Offset: 0x000A99FA
		[IgnoreDataMember]
		[XmlAttribute]
		public EmailAddressKeyType Key { get; set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x000AB803 File Offset: 0x000A9A03
		// (set) Token: 0x06002867 RID: 10343 RVA: 0x000AB810 File Offset: 0x000A9A10
		[DataMember(Name = "Key", EmitDefaultValue = false, Order = 0)]
		[XmlIgnore]
		public string KeyString
		{
			get
			{
				return EnumUtilities.ToString<EmailAddressKeyType>(this.Key);
			}
			set
			{
				this.Key = EnumUtilities.Parse<EmailAddressKeyType>(value);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x000AB81E File Offset: 0x000A9A1E
		// (set) Token: 0x06002869 RID: 10345 RVA: 0x000AB82B File Offset: 0x000A9A2B
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Name
		{
			get
			{
				return this.emailAddress.Name;
			}
			set
			{
				this.emailAddress.Name = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x000AB839 File Offset: 0x000A9A39
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x000AB846 File Offset: 0x000A9A46
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[XmlAttribute]
		public string RoutingType
		{
			get
			{
				return this.emailAddress.RoutingType;
			}
			set
			{
				this.emailAddress.RoutingType = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x000AB854 File Offset: 0x000A9A54
		// (set) Token: 0x0600286D RID: 10349 RVA: 0x000AB861 File Offset: 0x000A9A61
		[DataMember(Name = "MailboxType", EmitDefaultValue = false, Order = 3)]
		[XmlAttribute]
		public string MailboxType
		{
			get
			{
				return this.emailAddress.MailboxType;
			}
			set
			{
				this.emailAddress.MailboxType = value;
				this.MailboxTypeSpecified = true;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x000AB876 File Offset: 0x000A9A76
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x000AB87E File Offset: 0x000A9A7E
		[IgnoreDataMember]
		[XmlIgnore]
		public bool MailboxTypeSpecified { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x000AB887 File Offset: 0x000A9A87
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x000AB894 File Offset: 0x000A9A94
		[DataMember(Name = "EmailAddress", EmitDefaultValue = false, Order = 4)]
		[XmlText]
		public string Value
		{
			get
			{
				return this.emailAddress.EmailAddress;
			}
			set
			{
				this.emailAddress.EmailAddress = value;
			}
		}

		// Token: 0x040019C7 RID: 6599
		private EmailAddressWrapper emailAddress;
	}
}
