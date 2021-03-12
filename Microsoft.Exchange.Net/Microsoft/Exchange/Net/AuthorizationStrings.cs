using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000C5 RID: 197
	internal static class AuthorizationStrings
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000125BC File Offset: 0x000107BC
		static AuthorizationStrings()
		{
			AuthorizationStrings.stringIDs.Add(1929848388U, "UnknownExtensionDataKey");
			AuthorizationStrings.stringIDs.Add(508499146U, "InvalidExtensionDataLength");
			AuthorizationStrings.stringIDs.Add(3093504254U, "SidNodeExpected");
			AuthorizationStrings.stringIDs.Add(1823279302U, "InvalidCommonAccessTokenString");
			AuthorizationStrings.stringIDs.Add(2930595838U, "LogonNameIsMissing");
			AuthorizationStrings.stringIDs.Add(524350434U, "InvalidGroupAttributesValue");
			AuthorizationStrings.stringIDs.Add(2738351686U, "MissingVersion");
			AuthorizationStrings.stringIDs.Add(3881300465U, "InvalidGroupAttributes");
			AuthorizationStrings.stringIDs.Add(3419671342U, "AuthenticationTypeIsMissing");
			AuthorizationStrings.stringIDs.Add(3730243391U, "InvalidRestrictedGroupLength");
			AuthorizationStrings.stringIDs.Add(2708243460U, "UserSidMustNotHaveAttributes");
			AuthorizationStrings.stringIDs.Add(4181701447U, "InvalidExtensionDataValue");
			AuthorizationStrings.stringIDs.Add(945651915U, "MissingUserSid");
			AuthorizationStrings.stringIDs.Add(2952593655U, "InvalidGroupSidValue");
			AuthorizationStrings.stringIDs.Add(3725269515U, "InvalidFieldType");
			AuthorizationStrings.stringIDs.Add(3321965778U, "InvalidXml");
			AuthorizationStrings.stringIDs.Add(2765023819U, "InvalidRestrictedGroupAttributesValue");
			AuthorizationStrings.stringIDs.Add(3428857605U, "InvalidWindowsAccessToken");
			AuthorizationStrings.stringIDs.Add(1782138211U, "MultipleUserSid");
			AuthorizationStrings.stringIDs.Add(832686951U, "ExpectingEndOfSid");
			AuthorizationStrings.stringIDs.Add(3955902527U, "InvalidRoot");
			AuthorizationStrings.stringIDs.Add(4129109959U, "InvalidSidType");
			AuthorizationStrings.stringIDs.Add(1629171792U, "ExpectingSidValue");
			AuthorizationStrings.stringIDs.Add(787285511U, "MissingTokenType");
			AuthorizationStrings.stringIDs.Add(102074919U, "InvalidExtensionDataKey");
			AuthorizationStrings.stringIDs.Add(986794111U, "ExpectingWindowsAccessToken");
			AuthorizationStrings.stringIDs.Add(3967521285U, "MissingIsCompressed");
			AuthorizationStrings.stringIDs.Add(4168034456U, "InvalidGroupLength");
			AuthorizationStrings.stringIDs.Add(31770294U, "InvalidRestrictedGroupSidValue");
			AuthorizationStrings.stringIDs.Add(369811396U, "InvalidAttributeValue");
			AuthorizationStrings.stringIDs.Add(1681390866U, "InvalidRestrictedGroupAttributes");
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00012864 File Offset: 0x00010A64
		public static LocalizedString InvalidSidAttribute(string attribute)
		{
			return new LocalizedString("InvalidSidAttribute", "Ex24A1BE", false, true, AuthorizationStrings.ResourceManager, new object[]
			{
				attribute
			});
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00012893 File Offset: 0x00010A93
		public static LocalizedString UnknownExtensionDataKey
		{
			get
			{
				return new LocalizedString("UnknownExtensionDataKey", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000128B1 File Offset: 0x00010AB1
		public static LocalizedString InvalidExtensionDataLength
		{
			get
			{
				return new LocalizedString("InvalidExtensionDataLength", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000128CF File Offset: 0x00010ACF
		public static LocalizedString SidNodeExpected
		{
			get
			{
				return new LocalizedString("SidNodeExpected", "Ex5C061D", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000128ED File Offset: 0x00010AED
		public static LocalizedString InvalidCommonAccessTokenString
		{
			get
			{
				return new LocalizedString("InvalidCommonAccessTokenString", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001290B File Offset: 0x00010B0B
		public static LocalizedString LogonNameIsMissing
		{
			get
			{
				return new LocalizedString("LogonNameIsMissing", "ExAC36C7", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001292C File Offset: 0x00010B2C
		public static LocalizedString InvalidRootAttribute(string attribute)
		{
			return new LocalizedString("InvalidRootAttribute", "Ex14F4DF", false, true, AuthorizationStrings.ResourceManager, new object[]
			{
				attribute
			});
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001295B File Offset: 0x00010B5B
		public static LocalizedString InvalidGroupAttributesValue
		{
			get
			{
				return new LocalizedString("InvalidGroupAttributesValue", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00012979 File Offset: 0x00010B79
		public static LocalizedString MissingVersion
		{
			get
			{
				return new LocalizedString("MissingVersion", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00012997 File Offset: 0x00010B97
		public static LocalizedString InvalidGroupAttributes
		{
			get
			{
				return new LocalizedString("InvalidGroupAttributes", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000129B5 File Offset: 0x00010BB5
		public static LocalizedString AuthenticationTypeIsMissing
		{
			get
			{
				return new LocalizedString("AuthenticationTypeIsMissing", "Ex58BE70", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000129D3 File Offset: 0x00010BD3
		public static LocalizedString InvalidRestrictedGroupLength
		{
			get
			{
				return new LocalizedString("InvalidRestrictedGroupLength", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000129F1 File Offset: 0x00010BF1
		public static LocalizedString UserSidMustNotHaveAttributes
		{
			get
			{
				return new LocalizedString("UserSidMustNotHaveAttributes", "Ex7DC171", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00012A0F File Offset: 0x00010C0F
		public static LocalizedString InvalidExtensionDataValue
		{
			get
			{
				return new LocalizedString("InvalidExtensionDataValue", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00012A2D File Offset: 0x00010C2D
		public static LocalizedString MissingUserSid
		{
			get
			{
				return new LocalizedString("MissingUserSid", "Ex256AEB", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00012A4B File Offset: 0x00010C4B
		public static LocalizedString InvalidGroupSidValue
		{
			get
			{
				return new LocalizedString("InvalidGroupSidValue", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00012A6C File Offset: 0x00010C6C
		public static LocalizedString SerializedAccessTokenParserException(int lineNumber, int position, LocalizedString reason)
		{
			return new LocalizedString("SerializedAccessTokenParserException", "Ex0D7072", false, true, AuthorizationStrings.ResourceManager, new object[]
			{
				lineNumber,
				position,
				reason
			});
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00012AB2 File Offset: 0x00010CB2
		public static LocalizedString InvalidFieldType
		{
			get
			{
				return new LocalizedString("InvalidFieldType", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00012AD0 File Offset: 0x00010CD0
		public static LocalizedString InvalidXml
		{
			get
			{
				return new LocalizedString("InvalidXml", "Ex844887", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00012AEE File Offset: 0x00010CEE
		public static LocalizedString InvalidRestrictedGroupAttributesValue
		{
			get
			{
				return new LocalizedString("InvalidRestrictedGroupAttributesValue", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00012B0C File Offset: 0x00010D0C
		public static LocalizedString InvalidWindowsAccessToken
		{
			get
			{
				return new LocalizedString("InvalidWindowsAccessToken", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00012B2A File Offset: 0x00010D2A
		public static LocalizedString MultipleUserSid
		{
			get
			{
				return new LocalizedString("MultipleUserSid", "Ex29A505", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00012B48 File Offset: 0x00010D48
		public static LocalizedString ExpectingEndOfSid
		{
			get
			{
				return new LocalizedString("ExpectingEndOfSid", "Ex66DF42", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00012B66 File Offset: 0x00010D66
		public static LocalizedString InvalidRoot
		{
			get
			{
				return new LocalizedString("InvalidRoot", "Ex5F98BF", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00012B84 File Offset: 0x00010D84
		public static LocalizedString InvalidSidType
		{
			get
			{
				return new LocalizedString("InvalidSidType", "ExF4750F", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public static LocalizedString CommonAccessTokenException(int version, LocalizedString reason)
		{
			return new LocalizedString("CommonAccessTokenException", "", false, false, AuthorizationStrings.ResourceManager, new object[]
			{
				version,
				reason
			});
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00012BE1 File Offset: 0x00010DE1
		public static LocalizedString ExpectingSidValue
		{
			get
			{
				return new LocalizedString("ExpectingSidValue", "Ex06465A", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00012BFF File Offset: 0x00010DFF
		public static LocalizedString MissingTokenType
		{
			get
			{
				return new LocalizedString("MissingTokenType", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00012C1D File Offset: 0x00010E1D
		public static LocalizedString InvalidExtensionDataKey
		{
			get
			{
				return new LocalizedString("InvalidExtensionDataKey", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00012C3B File Offset: 0x00010E3B
		public static LocalizedString ExpectingWindowsAccessToken
		{
			get
			{
				return new LocalizedString("ExpectingWindowsAccessToken", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00012C59 File Offset: 0x00010E59
		public static LocalizedString MissingIsCompressed
		{
			get
			{
				return new LocalizedString("MissingIsCompressed", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00012C78 File Offset: 0x00010E78
		public static LocalizedString TooManySidNodes(string userName, int maximumSidCount)
		{
			return new LocalizedString("TooManySidNodes", "Ex823B5C", false, true, AuthorizationStrings.ResourceManager, new object[]
			{
				userName,
				maximumSidCount
			});
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00012CB0 File Offset: 0x00010EB0
		public static LocalizedString InvalidGroupLength
		{
			get
			{
				return new LocalizedString("InvalidGroupLength", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00012CCE File Offset: 0x00010ECE
		public static LocalizedString InvalidRestrictedGroupSidValue
		{
			get
			{
				return new LocalizedString("InvalidRestrictedGroupSidValue", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00012CEC File Offset: 0x00010EEC
		public static LocalizedString InvalidAttributeValue
		{
			get
			{
				return new LocalizedString("InvalidAttributeValue", "Ex44BCDA", false, true, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012D0A File Offset: 0x00010F0A
		public static LocalizedString InvalidRestrictedGroupAttributes
		{
			get
			{
				return new LocalizedString("InvalidRestrictedGroupAttributes", "", false, false, AuthorizationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00012D28 File Offset: 0x00010F28
		public static LocalizedString GetLocalizedString(AuthorizationStrings.IDs key)
		{
			return new LocalizedString(AuthorizationStrings.stringIDs[(uint)key], AuthorizationStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040003EB RID: 1003
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(31);

		// Token: 0x040003EC RID: 1004
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.AuthorizationStrings", typeof(AuthorizationStrings).GetTypeInfo().Assembly);

		// Token: 0x020000C6 RID: 198
		public enum IDs : uint
		{
			// Token: 0x040003EE RID: 1006
			UnknownExtensionDataKey = 1929848388U,
			// Token: 0x040003EF RID: 1007
			InvalidExtensionDataLength = 508499146U,
			// Token: 0x040003F0 RID: 1008
			SidNodeExpected = 3093504254U,
			// Token: 0x040003F1 RID: 1009
			InvalidCommonAccessTokenString = 1823279302U,
			// Token: 0x040003F2 RID: 1010
			LogonNameIsMissing = 2930595838U,
			// Token: 0x040003F3 RID: 1011
			InvalidGroupAttributesValue = 524350434U,
			// Token: 0x040003F4 RID: 1012
			MissingVersion = 2738351686U,
			// Token: 0x040003F5 RID: 1013
			InvalidGroupAttributes = 3881300465U,
			// Token: 0x040003F6 RID: 1014
			AuthenticationTypeIsMissing = 3419671342U,
			// Token: 0x040003F7 RID: 1015
			InvalidRestrictedGroupLength = 3730243391U,
			// Token: 0x040003F8 RID: 1016
			UserSidMustNotHaveAttributes = 2708243460U,
			// Token: 0x040003F9 RID: 1017
			InvalidExtensionDataValue = 4181701447U,
			// Token: 0x040003FA RID: 1018
			MissingUserSid = 945651915U,
			// Token: 0x040003FB RID: 1019
			InvalidGroupSidValue = 2952593655U,
			// Token: 0x040003FC RID: 1020
			InvalidFieldType = 3725269515U,
			// Token: 0x040003FD RID: 1021
			InvalidXml = 3321965778U,
			// Token: 0x040003FE RID: 1022
			InvalidRestrictedGroupAttributesValue = 2765023819U,
			// Token: 0x040003FF RID: 1023
			InvalidWindowsAccessToken = 3428857605U,
			// Token: 0x04000400 RID: 1024
			MultipleUserSid = 1782138211U,
			// Token: 0x04000401 RID: 1025
			ExpectingEndOfSid = 832686951U,
			// Token: 0x04000402 RID: 1026
			InvalidRoot = 3955902527U,
			// Token: 0x04000403 RID: 1027
			InvalidSidType = 4129109959U,
			// Token: 0x04000404 RID: 1028
			ExpectingSidValue = 1629171792U,
			// Token: 0x04000405 RID: 1029
			MissingTokenType = 787285511U,
			// Token: 0x04000406 RID: 1030
			InvalidExtensionDataKey = 102074919U,
			// Token: 0x04000407 RID: 1031
			ExpectingWindowsAccessToken = 986794111U,
			// Token: 0x04000408 RID: 1032
			MissingIsCompressed = 3967521285U,
			// Token: 0x04000409 RID: 1033
			InvalidGroupLength = 4168034456U,
			// Token: 0x0400040A RID: 1034
			InvalidRestrictedGroupSidValue = 31770294U,
			// Token: 0x0400040B RID: 1035
			InvalidAttributeValue = 369811396U,
			// Token: 0x0400040C RID: 1036
			InvalidRestrictedGroupAttributes = 1681390866U
		}

		// Token: 0x020000C7 RID: 199
		private enum ParamIDs
		{
			// Token: 0x0400040E RID: 1038
			InvalidSidAttribute,
			// Token: 0x0400040F RID: 1039
			InvalidRootAttribute,
			// Token: 0x04000410 RID: 1040
			SerializedAccessTokenParserException,
			// Token: 0x04000411 RID: 1041
			CommonAccessTokenException,
			// Token: 0x04000412 RID: 1042
			TooManySidNodes
		}
	}
}
