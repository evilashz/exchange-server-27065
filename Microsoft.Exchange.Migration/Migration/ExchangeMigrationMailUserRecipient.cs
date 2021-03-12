using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A3 RID: 163
	internal sealed class ExchangeMigrationMailUserRecipient : ExchangeMigrationRecipientWithHomeServer
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x00027631 File Offset: 0x00025831
		public ExchangeMigrationMailUserRecipient() : base(MigrationUserRecipientType.Mailbox)
		{
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0002763A File Offset: 0x0002583A
		public override HashSet<PropTag> SupportedProperties
		{
			get
			{
				return ExchangeMigrationMailUserRecipient.supportedProperties;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00027641 File Offset: 0x00025841
		public override HashSet<PropTag> RequiredProperties
		{
			get
			{
				return ExchangeMigrationMailUserRecipient.requiredProperties;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00027648 File Offset: 0x00025848
		public override bool TryValidateRequiredProperties(out LocalizedString errorMessage)
		{
			if (!base.TryValidateRequiredProperties(out errorMessage))
			{
				return false;
			}
			if (base.IsPropertyRequired((PropTag)2147876895U) && string.IsNullOrEmpty(base.MsExchHomeServerName))
			{
				errorMessage = ServerStrings.MigrationNSPIMissingRequiredField((PropTag)2147876895U);
				return false;
			}
			return true;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00027684 File Offset: 0x00025884
		protected override void UpdateDependentProperties(PropTag proptag)
		{
			base.UpdateDependentProperties(proptag);
			if (proptag == (PropTag)2147876895U)
			{
				string propertyValue = base.GetPropertyValue<string>((PropTag)2147876895U);
				if (!string.IsNullOrEmpty(propertyValue))
				{
					int num = propertyValue.IndexOf("/cn=Microsoft Private MDB", StringComparison.OrdinalIgnoreCase);
					base.MsExchHomeServerName = ((num >= 0) ? propertyValue.Substring(0, num) : null);
				}
			}
		}

		// Token: 0x0400038D RID: 909
		public const string HomeMDBSuffix = "/cn=Microsoft Private MDB";

		// Token: 0x0400038E RID: 910
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
			PropTag.Account,
			(PropTag)2147876895U,
			(PropTag)2147811359U,
			(PropTag)2148864031U,
			(PropTag)134676483U,
			(PropTag)2361524482U,
			(PropTag)2154364959U,
			(PropTag)2359230495U
		});

		// Token: 0x0400038F RID: 911
		private static HashSet<PropTag> requiredProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.EmailAddress,
			PropTag.SmtpAddress,
			PropTag.DisplayName,
			(PropTag)2147876895U
		});
	}
}
