using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A0 RID: 160
	internal sealed class ExchangeMigrationMailContactRecipient : ExchangeMigrationRecipient
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0002733E File Offset: 0x0002553E
		public ExchangeMigrationMailContactRecipient() : base(MigrationUserRecipientType.Contact)
		{
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00027347 File Offset: 0x00025547
		public override HashSet<PropTag> SupportedProperties
		{
			get
			{
				return ExchangeMigrationMailContactRecipient.supportedProperties;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002734E File Offset: 0x0002554E
		public override HashSet<PropTag> RequiredProperties
		{
			get
			{
				return ExchangeMigrationMailContactRecipient.requiredProperties;
			}
		}

		// Token: 0x04000388 RID: 904
		private static HashSet<PropTag> supportedProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
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
			PropTag.Account,
			(PropTag)2154364959U,
			(PropTag)2147811359U,
			(PropTag)2148864031U
		});

		// Token: 0x04000389 RID: 905
		private static HashSet<PropTag> requiredProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.EmailAddress,
			PropTag.SmtpAddress,
			PropTag.DisplayName
		});
	}
}
