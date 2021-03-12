using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006EB RID: 1771
	[Serializable]
	public class ConsumerMailbox : ADPresentationObject
	{
		// Token: 0x060052D4 RID: 21204 RVA: 0x0012FE11 File Offset: 0x0012E011
		public ConsumerMailbox()
		{
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0012FE19 File Offset: 0x0012E019
		public ConsumerMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x060052D6 RID: 21206 RVA: 0x0012FE22 File Offset: 0x0012E022
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "user";
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x060052D7 RID: 21207 RVA: 0x0012FE29 File Offset: 0x0012E029
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[ConsumerMailboxSchema.Database];
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x060052D8 RID: 21208 RVA: 0x0012FE3C File Offset: 0x0012E03C
		public string Description
		{
			get
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this[ConsumerMailboxSchema.Description];
				if (multiValuedProperty != null && multiValuedProperty.Count > 0)
				{
					return multiValuedProperty[0];
				}
				return string.Empty;
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x0012FE73 File Offset: 0x0012E073
		public string DisplayName
		{
			get
			{
				return (string)this[ConsumerMailboxSchema.DisplayName];
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x060052DA RID: 21210 RVA: 0x0012FE85 File Offset: 0x0012E085
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[ConsumerMailboxSchema.EmailAddresses];
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x0012FE98 File Offset: 0x0012E098
		public Guid? ExchangeGuid
		{
			get
			{
				if (this.Database == null)
				{
					return null;
				}
				return (Guid?)this[ConsumerMailboxSchema.ExchangeGuid];
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x060052DC RID: 21212 RVA: 0x0012FEC7 File Offset: 0x0012E0C7
		public string LegacyExchangeDN
		{
			get
			{
				if (this.Database == null)
				{
					return null;
				}
				return (string)this[ConsumerMailboxSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x0012FEE3 File Offset: 0x0012E0E3
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x060052DE RID: 21214 RVA: 0x0012FEEC File Offset: 0x0012E0EC
		public NetID NetID
		{
			get
			{
				ulong netID;
				if (this.ExchangeGuid != null && ConsumerIdentityHelper.TryGetPuidFromGuid(this.ExchangeGuid.Value, out netID))
				{
					return new NetID((long)netID);
				}
				return null;
			}
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x0012FF28 File Offset: 0x0012E128
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[ConsumerMailboxSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x060052E0 RID: 21216 RVA: 0x0012FF3A File Offset: 0x0012E13A
		public string ServerName
		{
			get
			{
				if (this.Database == null)
				{
					return null;
				}
				return (string)this[ConsumerMailboxSchema.ServerName];
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x0012FF56 File Offset: 0x0012E156
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[MailboxSchema.WindowsLiveID];
			}
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x060052E2 RID: 21218 RVA: 0x0012FF68 File Offset: 0x0012E168
		public PrimaryMailboxSourceType PrimaryMailboxSource
		{
			get
			{
				return (PrimaryMailboxSourceType)this[ADUserSchema.PrimaryMailboxSource];
			}
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x0012FF7A File Offset: 0x0012E17A
		public IPAddress SatchmoClusterIp
		{
			get
			{
				return (IPAddress)this[ADUserSchema.SatchmoClusterIp];
			}
		}

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x060052E4 RID: 21220 RVA: 0x0012FF8C File Offset: 0x0012E18C
		public string SatchmoDGroup
		{
			get
			{
				return (string)this[ADUserSchema.SatchmoDGroup];
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x060052E5 RID: 21221 RVA: 0x0012FF9E File Offset: 0x0012E19E
		public bool FblEnabled
		{
			get
			{
				return (bool)this[ADUserSchema.FblEnabled];
			}
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x060052E6 RID: 21222 RVA: 0x0012FFB0 File Offset: 0x0012E1B0
		public string Gender
		{
			get
			{
				return (string)this[ADUserSchema.Gender];
			}
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x0012FFC2 File Offset: 0x0012E1C2
		public string Occupation
		{
			get
			{
				return (string)this[ADUserSchema.Occupation];
			}
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x060052E8 RID: 21224 RVA: 0x0012FFD4 File Offset: 0x0012E1D4
		public string Region
		{
			get
			{
				return (string)this[ADUserSchema.Region];
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x060052E9 RID: 21225 RVA: 0x0012FFE6 File Offset: 0x0012E1E6
		public string Timezone
		{
			get
			{
				return (string)this[ADUserSchema.Timezone];
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x060052EA RID: 21226 RVA: 0x0012FFF8 File Offset: 0x0012E1F8
		public DateTime? Birthdate
		{
			get
			{
				return (DateTime?)this[ADUserSchema.Birthdate];
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x060052EB RID: 21227 RVA: 0x0013000A File Offset: 0x0012E20A
		public string BirthdayPrecision
		{
			get
			{
				return (string)this[ADUserSchema.BirthdayPrecision];
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x060052EC RID: 21228 RVA: 0x0013001C File Offset: 0x0012E21C
		public string NameVersion
		{
			get
			{
				return (string)this[ADUserSchema.NameVersion];
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x0013002E File Offset: 0x0012E22E
		public string AlternateSupportEmailAddresses
		{
			get
			{
				return (string)this[ADUserSchema.AlternateSupportEmailAddresses];
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x060052EE RID: 21230 RVA: 0x00130040 File Offset: 0x0012E240
		public string PostalCode
		{
			get
			{
				return (string)this[ADUserSchema.PostalCode];
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x060052EF RID: 21231 RVA: 0x00130052 File Offset: 0x0012E252
		public bool OptInUser
		{
			get
			{
				return (bool)this[ADUserSchema.OptInUser];
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x060052F0 RID: 21232 RVA: 0x00130064 File Offset: 0x0012E264
		public bool MigrationDryRun
		{
			get
			{
				return (bool)this[ADUserSchema.MigrationDryRun];
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x00130076 File Offset: 0x0012E276
		public bool IsMigratedConsumerMailbox
		{
			get
			{
				return (bool)this[ADUserSchema.IsMigratedConsumerMailbox];
			}
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x00130088 File Offset: 0x0012E288
		public string FirstName
		{
			get
			{
				return (string)this[ADUserSchema.FirstName];
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x060052F3 RID: 21235 RVA: 0x0013009A File Offset: 0x0012E29A
		public string LastName
		{
			get
			{
				return (string)this[ADUserSchema.LastName];
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x001300AC File Offset: 0x0012E2AC
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)this[ADRecipientSchema.UsageLocation];
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x060052F5 RID: 21237 RVA: 0x001300BE File Offset: 0x0012E2BE
		public MultiValuedProperty<int> LocaleID
		{
			get
			{
				return (MultiValuedProperty<int>)this[ADUserSchema.LocaleID];
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x060052F6 RID: 21238 RVA: 0x001300D0 File Offset: 0x0012E2D0
		public bool IsPremiumConsumerMailbox
		{
			get
			{
				return (bool)this[ADUserSchema.IsPremiumConsumerMailbox];
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x060052F7 RID: 21239 RVA: 0x001300E2 File Offset: 0x0012E2E2
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ConsumerMailbox.schema;
			}
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x001300E9 File Offset: 0x0012E2E9
		internal static ConsumerMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new ConsumerMailbox(dataObject);
		}

		// Token: 0x04003805 RID: 14341
		private static ConsumerMailboxSchema schema = ObjectSchema.GetInstance<ConsumerMailboxSchema>();
	}
}
