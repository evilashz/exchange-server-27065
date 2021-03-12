using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000B6 RID: 182
	internal static class AddressFormatTable
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x000357FC File Offset: 0x000339FC
		private static Dictionary<PhysicalAddressType, PropertyDefinition[]> LoadAddressPropertyTable()
		{
			Dictionary<PhysicalAddressType, PropertyDefinition[]> dictionary = new Dictionary<PhysicalAddressType, PropertyDefinition[]>();
			dictionary[PhysicalAddressType.Business] = AddressFormatTable.BusinessAddressParts;
			dictionary[PhysicalAddressType.Home] = AddressFormatTable.HomeAddressParts;
			dictionary[PhysicalAddressType.Other] = AddressFormatTable.OtherAddressParts;
			return dictionary;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00035834 File Offset: 0x00033A34
		private static Dictionary<int, AddressFormatTable.AddressPart[]> LoadCultureAddressMap()
		{
			Dictionary<int, AddressFormatTable.AddressPart[]> dictionary = new Dictionary<int, AddressFormatTable.AddressPart[]>();
			dictionary[1025] = AddressFormatTable.AmericanAddressFormat;
			dictionary[2049] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[3073] = AddressFormatTable.EgyptianAddressFormat;
			dictionary[4097] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[5121] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[6145] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[7169] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[8193] = AddressFormatTable.OmanAddressFormat;
			dictionary[9217] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[10241] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[11265] = AddressFormatTable.AmericanAddressFormat;
			dictionary[12289] = AddressFormatTable.AmericanAddressFormat;
			dictionary[13313] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[14337] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[15361] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[16385] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1069] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1027] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[5124] = AddressFormatTable.zhMOAddressFormat;
			dictionary[1028] = AddressFormatTable.zhTWAddressFormat;
			dictionary[2052] = AddressFormatTable.zhTWAddressFormat;
			dictionary[3076] = AddressFormatTable.RussianAddressFormat;
			dictionary[4100] = AddressFormatTable.AmericanAddressFormat;
			dictionary[1029] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1030] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1043] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2067] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[9225] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1033] = AddressFormatTable.AmericanAddressFormat;
			dictionary[2057] = AddressFormatTable.AmericanAddressFormat;
			dictionary[3081] = AddressFormatTable.AmericanAddressFormat;
			dictionary[4105] = AddressFormatTable.AmericanAddressFormat;
			dictionary[5129] = AddressFormatTable.AmericanAddressFormat;
			dictionary[6153] = AddressFormatTable.AmericanAddressFormat;
			dictionary[7177] = AddressFormatTable.AmericanAddressFormat;
			dictionary[12297] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[8201] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[10249] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[11273] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[13321] = AddressFormatTable.TurkishAddressFormat;
			dictionary[16393] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[17417] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[18441] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[1035] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1036] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2060] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[3084] = AddressFormatTable.AmericanAddressFormat;
			dictionary[4108] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[5132] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[6156] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1031] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2055] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[3079] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[4103] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[5127] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1032] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1037] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1038] = AddressFormatTable.HungarianAddressFormat;
			dictionary[1040] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2064] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1041] = AddressFormatTable.JapaneseAddressFormat;
			dictionary[1042] = AddressFormatTable.JapaneseAddressFormat;
			dictionary[1045] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1046] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2070] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1048] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1049] = AddressFormatTable.RussianAddressFormat;
			dictionary[1034] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2058] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[3082] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[4106] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[5130] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[6154] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[7178] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[8202] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[9226] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[10250] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[11274] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[12298] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[13322] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[14346] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[15370] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[16394] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[17418] = AddressFormatTable.AmericanAddressFormat;
			dictionary[18442] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[19466] = AddressFormatTable.AmericanAddressFormat;
			dictionary[20490] = AddressFormatTable.AmericanAddressFormat;
			dictionary[21514] = AddressFormatTable.AmericanAddressFormat;
			dictionary[1053] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2077] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1055] = AddressFormatTable.TurkishAddressFormat;
			dictionary[1044] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1081] = AddressFormatTable.AmericanAddressFormat;
			dictionary[1086] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1054] = AddressFormatTable.AmericanAddressFormat;
			dictionary[1051] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1050] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1057] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[1060] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1026] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1039] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[2074] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[3098] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1058] = AddressFormatTable.RussianAddressFormat;
			dictionary[1087] = AddressFormatTable.zhMOAddressFormat;
			dictionary[1061] = AddressFormatTable.EuropeanAddressFormat;
			dictionary[1062] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[1063] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[1066] = AddressFormatTable.OmanAddressFormat;
			dictionary[1056] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[1065] = AddressFormatTable.AmericanAddressFormat;
			dictionary[1124] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[2110] = AddressFormatTable.IndonesianAddressFormat;
			dictionary[2068] = AddressFormatTable.EuropeanAddressFormat;
			return dictionary;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00035F68 File Offset: 0x00034168
		public static AddressFormatTable.AddressPart[] GetCultureAddressMap(int lcid)
		{
			AddressFormatTable.AddressPart[] result;
			if (AddressFormatTable.cultureAddressMap.TryGetValue(lcid, out result))
			{
				return result;
			}
			return AddressFormatTable.AmericanAddressFormat;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00035F8C File Offset: 0x0003418C
		public static PropertyDefinition LookupAddressProperty(AddressFormatTable.AddressPart addressPart, PhysicalAddressType type)
		{
			PropertyDefinition[] array;
			if (AddressFormatTable.addressPropertyTable.TryGetValue(type, out array))
			{
				return array[(int)addressPart];
			}
			return null;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00035FAD File Offset: 0x000341AD
		public static PropertyDefinition LookupAddressPropertyAd(AddressFormatTable.AddressPart addressPart)
		{
			if (AddressFormatTable.BusinessAdAddressParts[(int)addressPart] != null)
			{
				return AddressFormatTable.BusinessAdAddressParts[(int)addressPart];
			}
			return null;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00035FC4 File Offset: 0x000341C4
		// Note: this type is marked as 'beforefieldinit'.
		static AddressFormatTable()
		{
			AddressFormatTable.AddressPart[] array = new AddressFormatTable.AddressPart[5];
			array[0] = AddressFormatTable.AddressPart.PostalCode;
			array[1] = AddressFormatTable.AddressPart.Country;
			array[2] = AddressFormatTable.AddressPart.State;
			array[3] = AddressFormatTable.AddressPart.City;
			AddressFormatTable.zhTWAddressFormat = array;
			AddressFormatTable.AddressPart[] array2 = new AddressFormatTable.AddressPart[5];
			array2[0] = AddressFormatTable.AddressPart.Country;
			array2[1] = AddressFormatTable.AddressPart.PostalCode;
			array2[2] = AddressFormatTable.AddressPart.State;
			array2[3] = AddressFormatTable.AddressPart.City;
			AddressFormatTable.RussianAddressFormat = array2;
			AddressFormatTable.AddressPart[] array3 = new AddressFormatTable.AddressPart[5];
			array3[0] = AddressFormatTable.AddressPart.Country;
			array3[1] = AddressFormatTable.AddressPart.State;
			array3[2] = AddressFormatTable.AddressPart.City;
			array3[3] = AddressFormatTable.AddressPart.PostalCode;
			AddressFormatTable.zhMOAddressFormat = array3;
			AddressFormatTable.AmericanAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.PostalCode,
				AddressFormatTable.AddressPart.Country
			};
			AddressFormatTable.EuropeanAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.PostalCode,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.Country
			};
			AddressFormatTable.JapaneseAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.PostalCode,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.Country
			};
			AddressFormatTable.TurkishAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.PostalCode,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.Country
			};
			AddressFormatTable.OmanAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.Country,
				AddressFormatTable.AddressPart.PostalCode
			};
			AddressFormatTable.EgyptianAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.Country,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.PostalCode
			};
			AddressFormatTable.HungarianAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.PostalCode,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.Country
			};
			AddressFormatTable.IndonesianAddressFormat = new AddressFormatTable.AddressPart[]
			{
				AddressFormatTable.AddressPart.Street,
				AddressFormatTable.AddressPart.City,
				AddressFormatTable.AddressPart.PostalCode,
				AddressFormatTable.AddressPart.State,
				AddressFormatTable.AddressPart.Country
			};
			AddressFormatTable.HomeAddressParts = new PropertyDefinition[]
			{
				ContactSchema.HomeStreet,
				ContactSchema.HomeCity,
				ContactSchema.HomeState,
				ContactSchema.HomePostalCode,
				ContactSchema.HomeCountry
			};
			AddressFormatTable.BusinessAddressParts = new PropertyDefinition[]
			{
				ContactSchema.WorkAddressStreet,
				ContactSchema.WorkAddressCity,
				ContactSchema.WorkAddressState,
				ContactSchema.WorkAddressPostalCode,
				ContactSchema.WorkAddressCountry
			};
			AddressFormatTable.OtherAddressParts = new PropertyDefinition[]
			{
				ContactSchema.OtherStreet,
				ContactSchema.OtherCity,
				ContactSchema.OtherState,
				ContactSchema.OtherPostalCode,
				ContactSchema.OtherCountry
			};
			AddressFormatTable.BusinessAdAddressParts = new PropertyDefinition[]
			{
				ADOrgPersonSchema.StreetAddress,
				ADOrgPersonSchema.City,
				ADOrgPersonSchema.StateOrProvince,
				ADOrgPersonSchema.PostalCode,
				ADOrgPersonSchema.Co
			};
			AddressFormatTable.addressPropertyTable = AddressFormatTable.LoadAddressPropertyTable();
			AddressFormatTable.cultureAddressMap = AddressFormatTable.LoadCultureAddressMap();
		}

		// Token: 0x040004A4 RID: 1188
		private static readonly AddressFormatTable.AddressPart[] zhTWAddressFormat;

		// Token: 0x040004A5 RID: 1189
		private static readonly AddressFormatTable.AddressPart[] RussianAddressFormat;

		// Token: 0x040004A6 RID: 1190
		private static readonly AddressFormatTable.AddressPart[] zhMOAddressFormat;

		// Token: 0x040004A7 RID: 1191
		private static readonly AddressFormatTable.AddressPart[] AmericanAddressFormat;

		// Token: 0x040004A8 RID: 1192
		private static readonly AddressFormatTable.AddressPart[] EuropeanAddressFormat;

		// Token: 0x040004A9 RID: 1193
		private static readonly AddressFormatTable.AddressPart[] JapaneseAddressFormat;

		// Token: 0x040004AA RID: 1194
		private static readonly AddressFormatTable.AddressPart[] TurkishAddressFormat;

		// Token: 0x040004AB RID: 1195
		private static readonly AddressFormatTable.AddressPart[] OmanAddressFormat;

		// Token: 0x040004AC RID: 1196
		private static readonly AddressFormatTable.AddressPart[] EgyptianAddressFormat;

		// Token: 0x040004AD RID: 1197
		private static readonly AddressFormatTable.AddressPart[] HungarianAddressFormat;

		// Token: 0x040004AE RID: 1198
		private static readonly AddressFormatTable.AddressPart[] IndonesianAddressFormat;

		// Token: 0x040004AF RID: 1199
		private static readonly PropertyDefinition[] HomeAddressParts;

		// Token: 0x040004B0 RID: 1200
		private static readonly PropertyDefinition[] BusinessAddressParts;

		// Token: 0x040004B1 RID: 1201
		private static readonly PropertyDefinition[] OtherAddressParts;

		// Token: 0x040004B2 RID: 1202
		private static readonly PropertyDefinition[] BusinessAdAddressParts;

		// Token: 0x040004B3 RID: 1203
		private static Dictionary<PhysicalAddressType, PropertyDefinition[]> addressPropertyTable;

		// Token: 0x040004B4 RID: 1204
		private static Dictionary<int, AddressFormatTable.AddressPart[]> cultureAddressMap;

		// Token: 0x020000B7 RID: 183
		public enum AddressPart
		{
			// Token: 0x040004B6 RID: 1206
			Street,
			// Token: 0x040004B7 RID: 1207
			City,
			// Token: 0x040004B8 RID: 1208
			State,
			// Token: 0x040004B9 RID: 1209
			PostalCode,
			// Token: 0x040004BA RID: 1210
			Country
		}
	}
}
