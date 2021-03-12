using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	internal abstract class SimpleContactInfoBase : ContactInfo
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		internal override string DisplayName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		internal override string Title
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CDF7 File Offset: 0x0000AFF7
		internal override string Company
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000CDFA File Offset: 0x0000AFFA
		internal override string FirstName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000CDFD File Offset: 0x0000AFFD
		internal override string LastName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CE00 File Offset: 0x0000B000
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000CE08 File Offset: 0x0000B008
		internal override string BusinessPhone
		{
			get
			{
				return this.businessPhone;
			}
			set
			{
				this.businessPhone = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000CE11 File Offset: 0x0000B011
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000CE19 File Offset: 0x0000B019
		internal override string MobilePhone
		{
			get
			{
				return this.mobilePhone;
			}
			set
			{
				this.mobilePhone = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000CE22 File Offset: 0x0000B022
		internal override string FaxNumber
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000CE25 File Offset: 0x0000B025
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000CE2D File Offset: 0x0000B02D
		internal override string HomePhone
		{
			get
			{
				return this.homePhone;
			}
			set
			{
				this.homePhone = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000CE36 File Offset: 0x0000B036
		internal override string IMAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000CE39 File Offset: 0x0000B039
		internal override string EMailAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000CE3C File Offset: 0x0000B03C
		internal override FoundByType FoundBy
		{
			get
			{
				return FoundByType.NotSpecified;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000CE3F File Offset: 0x0000B03F
		internal override string Id
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000CE42 File Offset: 0x0000B042
		internal override string EwsId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000CE45 File Offset: 0x0000B045
		internal override string EwsType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000CE48 File Offset: 0x0000B048
		internal override string City
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000CE4B File Offset: 0x0000B04B
		internal override string Country
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000CE4E File Offset: 0x0000B04E
		internal override ICollection<string> SanitizedPhoneNumbers
		{
			get
			{
				return new List<string>();
			}
		}

		// Token: 0x0400028B RID: 651
		private string businessPhone;

		// Token: 0x0400028C RID: 652
		private string mobilePhone;

		// Token: 0x0400028D RID: 653
		private string homePhone;
	}
}
