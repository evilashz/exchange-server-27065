using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E0 RID: 480
	internal class ContactConverter
	{
		// Token: 0x020001E1 RID: 481
		internal enum PostalAddressIndexType
		{
			// Token: 0x04000A5C RID: 2652
			None,
			// Token: 0x04000A5D RID: 2653
			Home,
			// Token: 0x04000A5E RID: 2654
			Business,
			// Token: 0x04000A5F RID: 2655
			Other
		}

		// Token: 0x020001E2 RID: 482
		internal class PostalAddressIndex : BaseConverter
		{
			// Token: 0x06000CCC RID: 3276 RVA: 0x00041E60 File Offset: 0x00040060
			public static ContactConverter.PostalAddressIndexType Parse(string value)
			{
				if (value != null)
				{
					if (value == "None")
					{
						return ContactConverter.PostalAddressIndexType.None;
					}
					if (value == "Business")
					{
						return ContactConverter.PostalAddressIndexType.Business;
					}
					if (value == "Home")
					{
						return ContactConverter.PostalAddressIndexType.Home;
					}
					if (value == "Other")
					{
						return ContactConverter.PostalAddressIndexType.Other;
					}
				}
				throw new InvalidValueForPropertyException(new PropertyUri(PropertyUriEnum.PostalAddressIndex), null);
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x00041EC0 File Offset: 0x000400C0
			public static string ToString(ContactConverter.PostalAddressIndexType postalAddressIndex)
			{
				switch (postalAddressIndex)
				{
				case ContactConverter.PostalAddressIndexType.None:
					return "None";
				case ContactConverter.PostalAddressIndexType.Home:
					return "Home";
				case ContactConverter.PostalAddressIndexType.Business:
					return "Business";
				case ContactConverter.PostalAddressIndexType.Other:
					return "Other";
				default:
					throw new InvalidValueForPropertyException(new PropertyUri(PropertyUriEnum.PostalAddressIndex), null);
				}
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x00041F0F File Offset: 0x0004010F
			public override object ConvertToObject(string propertyString)
			{
				return ContactConverter.PostalAddressIndex.Parse(propertyString);
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x00041F1C File Offset: 0x0004011C
			public override string ConvertToString(object propertyValue)
			{
				return ContactConverter.PostalAddressIndex.ToString((ContactConverter.PostalAddressIndexType)propertyValue);
			}

			// Token: 0x04000A60 RID: 2656
			private const string None = "None";

			// Token: 0x04000A61 RID: 2657
			private const string Business = "Business";

			// Token: 0x04000A62 RID: 2658
			private const string Home = "Home";

			// Token: 0x04000A63 RID: 2659
			private const string Other = "Other";
		}

		// Token: 0x020001E3 RID: 483
		internal class FileAsMapping : EnumConverter
		{
			// Token: 0x06000CD1 RID: 3281 RVA: 0x00041F34 File Offset: 0x00040134
			public static Microsoft.Exchange.Data.Storage.FileAsMapping Parse(string value)
			{
				switch (value)
				{
				case "Company":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.Company;
				case "CompanyLastCommaFirst":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.CompanyLastCommaFirst;
				case "CompanyLastFirst":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.CompanyLastFirst;
				case "CompanyLastSpaceFirst":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.CompanyLastSpaceFirst;
				case "FirstSpaceLast":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.FirstSpaceLast;
				case "LastCommaFirst":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastCommaFirst;
				case "LastCommaFirstCompany":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastCommaFirstCompany;
				case "LastFirst":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirst;
				case "LastFirstCompany":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirstCompany;
				case "LastFirstSuffix":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirstSuffix;
				case "LastSpaceFirst":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastSpaceFirst;
				case "LastSpaceFirstCompany":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastSpaceFirstCompany;
				case "None":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.None;
				case "DisplayName":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.DisplayName;
				case "Empty":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.Empty;
				case "FirstName":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.GivenName;
				case "LastFirstMiddleSuffix":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirstMiddleSuffix;
				case "LastName":
					return Microsoft.Exchange.Data.Storage.FileAsMapping.LastName;
				}
				throw new InvalidValueForPropertyException(new PropertyUri(PropertyUriEnum.FileAsMapping), null);
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x0004211C File Offset: 0x0004031C
			public static string ToString(Microsoft.Exchange.Data.Storage.FileAsMapping fileAsMapping)
			{
				if (fileAsMapping <= Microsoft.Exchange.Data.Storage.FileAsMapping.Company)
				{
					if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.None)
					{
						return "None";
					}
					if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.Company)
					{
						return "Company";
					}
				}
				else
				{
					switch (fileAsMapping)
					{
					case Microsoft.Exchange.Data.Storage.FileAsMapping.LastCommaFirst:
						return "LastCommaFirst";
					case Microsoft.Exchange.Data.Storage.FileAsMapping.CompanyLastCommaFirst:
						return "CompanyLastCommaFirst";
					case Microsoft.Exchange.Data.Storage.FileAsMapping.LastCommaFirstCompany:
						return "LastCommaFirstCompany";
					default:
						switch (fileAsMapping)
						{
						case Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirst:
							return "LastFirst";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.LastSpaceFirst:
							return "LastSpaceFirst";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.CompanyLastFirst:
							return "CompanyLastFirst";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.CompanyLastSpaceFirst:
							return "CompanyLastSpaceFirst";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirstCompany:
							return "LastFirstCompany";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.LastSpaceFirstCompany:
							return "LastSpaceFirstCompany";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirstSuffix:
							return "LastFirstSuffix";
						case Microsoft.Exchange.Data.Storage.FileAsMapping.FirstSpaceLast:
							return "FirstSpaceLast";
						}
						break;
					}
				}
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
				{
					if (fileAsMapping <= Microsoft.Exchange.Data.Storage.FileAsMapping.DisplayName)
					{
						if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.Empty)
						{
							return "Empty";
						}
						if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.DisplayName)
						{
							return "DisplayName";
						}
					}
					else
					{
						if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.GivenName)
						{
							return "FirstName";
						}
						if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.LastName)
						{
							return "LastName";
						}
						if (fileAsMapping == Microsoft.Exchange.Data.Storage.FileAsMapping.LastFirstMiddleSuffix)
						{
							return "LastFirstMiddleSuffix";
						}
					}
					return null;
				}
				return null;
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x0004223F File Offset: 0x0004043F
			public override object ConvertToObject(string propertyString)
			{
				return ContactConverter.FileAsMapping.Parse(propertyString);
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x0004224C File Offset: 0x0004044C
			public override string ConvertToString(object propertyValue)
			{
				return ContactConverter.FileAsMapping.ToString((Microsoft.Exchange.Data.Storage.FileAsMapping)propertyValue);
			}

			// Token: 0x04000A64 RID: 2660
			private const string Company = "Company";

			// Token: 0x04000A65 RID: 2661
			private const string CompanyLastCommaFirst = "CompanyLastCommaFirst";

			// Token: 0x04000A66 RID: 2662
			private const string CompanyLastFirst = "CompanyLastFirst";

			// Token: 0x04000A67 RID: 2663
			private const string CompanyLastSpaceFirst = "CompanyLastSpaceFirst";

			// Token: 0x04000A68 RID: 2664
			private const string Empty = "Empty";

			// Token: 0x04000A69 RID: 2665
			private const string DisplayName = "DisplayName";

			// Token: 0x04000A6A RID: 2666
			private const string FirstName = "FirstName";

			// Token: 0x04000A6B RID: 2667
			private const string FirstSpaceLast = "FirstSpaceLast";

			// Token: 0x04000A6C RID: 2668
			private const string LastCommaFirst = "LastCommaFirst";

			// Token: 0x04000A6D RID: 2669
			private const string LastCommaFirstCompany = "LastCommaFirstCompany";

			// Token: 0x04000A6E RID: 2670
			private const string LastFirst = "LastFirst";

			// Token: 0x04000A6F RID: 2671
			private const string LastFirstCompany = "LastFirstCompany";

			// Token: 0x04000A70 RID: 2672
			private const string LastFirstMiddleSuffix = "LastFirstMiddleSuffix";

			// Token: 0x04000A71 RID: 2673
			private const string LastFirstSuffix = "LastFirstSuffix";

			// Token: 0x04000A72 RID: 2674
			private const string LastName = "LastName";

			// Token: 0x04000A73 RID: 2675
			private const string LastSpaceFirst = "LastSpaceFirst";

			// Token: 0x04000A74 RID: 2676
			private const string LastSpaceFirstCompany = "LastSpaceFirstCompany";

			// Token: 0x04000A75 RID: 2677
			private const string None = "None";
		}
	}
}
