using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A1 RID: 161
	internal sealed class ExchangeMigrationMailEnabledUserRecipient : ExchangeMigrationRecipient
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x00027482 File Offset: 0x00025682
		public ExchangeMigrationMailEnabledUserRecipient() : base(MigrationUserRecipientType.Mailuser)
		{
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002748B File Offset: 0x0002568B
		public override HashSet<PropTag> SupportedProperties
		{
			get
			{
				return ExchangeMigrationMailEnabledUserRecipient.supportedProperties;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00027492 File Offset: 0x00025692
		public override HashSet<PropTag> RequiredProperties
		{
			get
			{
				return ExchangeMigrationMailEnabledUserRecipient.requiredProperties;
			}
		}

		// Token: 0x0400038A RID: 906
		private static HashSet<PropTag> supportedProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.DisplayTypeEx,
			PropTag.PrimaryFaxNumber,
			PropTag.BusinessTelephoneNumber,
			PropTag.CompanyName,
			PropTag.DepartmentName,
			PropTag.DisplayName,
			PropTag.EmailAddress,
			PropTag.GivenName,
			PropTag.Initials,
			PropTag.MobileTelephoneNumber,
			PropTag.OfficeLocation,
			PropTag.SmtpAddress,
			PropTag.Surname,
			PropTag.Title,
			(PropTag)2148470815U,
			PropTag.HomeTelephoneNumber,
			PropTag.StreetAddress,
			PropTag.Locality,
			PropTag.StateOrProvince,
			PropTag.PostalCode,
			PropTag.Comment,
			(PropTag)2154364959U,
			(PropTag)2147811359U,
			(PropTag)2148864031U,
			PropTag.Account
		});

		// Token: 0x0400038B RID: 907
		private static HashSet<PropTag> requiredProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.EmailAddress,
			PropTag.SmtpAddress,
			PropTag.DisplayName
		});
	}
}
