using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000009 RID: 9
	internal static class EASServerStrings
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00004B68 File Offset: 0x00002D68
		static EASServerStrings()
		{
			EASServerStrings.stringIDs.Add(1807771821U, "AdminMailDevicePhoneNumber");
			EASServerStrings.stringIDs.Add(842327697U, "AdminMailBodyDeviceAccessState");
			EASServerStrings.stringIDs.Add(2127272853U, "AdminMailBodyDeviceModel");
			EASServerStrings.stringIDs.Add(3481243890U, "AdminMailBody4");
			EASServerStrings.stringIDs.Add(4167251934U, "MissingDiscoveryInfoError");
			EASServerStrings.stringIDs.Add(2341808075U, "AdminMailBodyDeviceID");
			EASServerStrings.stringIDs.Add(4280143591U, "AdminMailDeviceAccessControlRule");
			EASServerStrings.stringIDs.Add(3127591392U, "ExBegin");
			EASServerStrings.stringIDs.Add(1743625299U, "Null");
			EASServerStrings.stringIDs.Add(2057529303U, "AdminMailBodyEASVersion");
			EASServerStrings.stringIDs.Add(4013839307U, "SchemaDirectoryNotAccessible");
			EASServerStrings.stringIDs.Add(1704351524U, "AdminMailBodyDeviceType");
			EASServerStrings.stringIDs.Add(2227082144U, "ExEnd");
			EASServerStrings.stringIDs.Add(25353299U, "AnonymousAccessError");
			EASServerStrings.stringIDs.Add(508148408U, "MismatchSyncStateError");
			EASServerStrings.stringIDs.Add(1179008650U, "AdminMailBodyDeviceOS");
			EASServerStrings.stringIDs.Add(2628658688U, "UnableToLoadAddressBookProvider");
			EASServerStrings.stringIDs.Add(2920971799U, "AdminMailUser");
			EASServerStrings.stringIDs.Add(3077959363U, "AdminMailBody1");
			EASServerStrings.stringIDs.Add(862352803U, "ExInner");
			EASServerStrings.stringIDs.Add(2474028085U, "AdminMailBodyDeviceAccessStateReason");
			EASServerStrings.stringIDs.Add(134200922U, "UnhandledException");
			EASServerStrings.stringIDs.Add(2674674836U, "AdminMailBody2");
			EASServerStrings.stringIDs.Add(3929860227U, "NoXmlResponse");
			EASServerStrings.stringIDs.Add(3181624402U, "AdminMailDeviceInformation");
			EASServerStrings.stringIDs.Add(2585038184U, "AdminMailDevicePolicyStatus");
			EASServerStrings.stringIDs.Add(1372331172U, "AdminMailBodyDeviceUserAgent");
			EASServerStrings.stringIDs.Add(3870834299U, "AdminMailDevicePolicyApplied");
			EASServerStrings.stringIDs.Add(655864836U, "AdminMailBodyDeviceIMEI");
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public static LocalizedString AdminMailDevicePhoneNumber
		{
			get
			{
				return new LocalizedString("AdminMailDevicePhoneNumber", "ExD3244A", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004E06 File Offset: 0x00003006
		public static LocalizedString AdminMailBodyDeviceAccessState
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceAccessState", "Ex65D6B4", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004E24 File Offset: 0x00003024
		public static LocalizedString AdminMailBodyDeviceModel
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceModel", "Ex63810E", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004E42 File Offset: 0x00003042
		public static LocalizedString AdminMailBody4
		{
			get
			{
				return new LocalizedString("AdminMailBody4", "Ex27F5E1", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004E60 File Offset: 0x00003060
		public static LocalizedString SchemaUnknownCompilationError(string dirPath)
		{
			return new LocalizedString("SchemaUnknownCompilationError", "Ex5B1997", false, true, EASServerStrings.ResourceManager, new object[]
			{
				dirPath
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004E90 File Offset: 0x00003090
		public static LocalizedString CannotFindSchemaClassException(string objclass, string schemaDN)
		{
			return new LocalizedString("CannotFindSchemaClassException", "Ex9C5355", false, true, EASServerStrings.ResourceManager, new object[]
			{
				objclass,
				schemaDN
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004EC4 File Offset: 0x000030C4
		public static LocalizedString XmlResponse(string response)
		{
			return new LocalizedString("XmlResponse", "Ex650E29", false, true, EASServerStrings.ResourceManager, new object[]
			{
				response
			});
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004EF3 File Offset: 0x000030F3
		public static LocalizedString MissingDiscoveryInfoError
		{
			get
			{
				return new LocalizedString("MissingDiscoveryInfoError", "Ex8E227F", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004F14 File Offset: 0x00003114
		public static LocalizedString FailedToCreateNewActiveDevice(string deviceId, string deviceType, string user)
		{
			return new LocalizedString("FailedToCreateNewActiveDevice", "Ex8040ED", false, true, EASServerStrings.ResourceManager, new object[]
			{
				deviceId,
				deviceType,
				user
			});
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004F4B File Offset: 0x0000314B
		public static LocalizedString AdminMailBodyDeviceID
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceID", "Ex84E4C2", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004F6C File Offset: 0x0000316C
		public static LocalizedString MultipleVirtualDirectoriesDetected(string root, string metabaseUrl)
		{
			return new LocalizedString("MultipleVirtualDirectoriesDetected", "Ex668707", false, true, EASServerStrings.ResourceManager, new object[]
			{
				root,
				metabaseUrl
			});
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004FA0 File Offset: 0x000031A0
		public static LocalizedString MissingADVirtualDirectory(string root, string metabaseUrl)
		{
			return new LocalizedString("MissingADVirtualDirectory", "ExA47A39", false, true, EASServerStrings.ResourceManager, new object[]
			{
				root,
				metabaseUrl
			});
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004FD3 File Offset: 0x000031D3
		public static LocalizedString AdminMailDeviceAccessControlRule
		{
			get
			{
				return new LocalizedString("AdminMailDeviceAccessControlRule", "Ex740E1A", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004FF1 File Offset: 0x000031F1
		public static LocalizedString ExBegin
		{
			get
			{
				return new LocalizedString("ExBegin", "ExA5BD26", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000500F File Offset: 0x0000320F
		public static LocalizedString Null
		{
			get
			{
				return new LocalizedString("Null", "Ex4BE0F9", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005030 File Offset: 0x00003230
		public static LocalizedString SyncStatusCode(int statusCode)
		{
			return new LocalizedString("SyncStatusCode", "Ex9FC7DD", false, true, EASServerStrings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005064 File Offset: 0x00003264
		public static LocalizedString ExMessage(string msg)
		{
			return new LocalizedString("ExMessage", "ExB94A69", false, true, EASServerStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005093 File Offset: 0x00003293
		public static LocalizedString AdminMailBodyEASVersion
		{
			get
			{
				return new LocalizedString("AdminMailBodyEASVersion", "ExC3AE87", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000050B1 File Offset: 0x000032B1
		public static LocalizedString SchemaDirectoryNotAccessible
		{
			get
			{
				return new LocalizedString("SchemaDirectoryNotAccessible", "Ex49AF6B", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000050CF File Offset: 0x000032CF
		public static LocalizedString AdminMailBodyDeviceType
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceType", "Ex1F6A6D", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000050ED File Offset: 0x000032ED
		public static LocalizedString ExEnd
		{
			get
			{
				return new LocalizedString("ExEnd", "Ex2ABF18", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000510B File Offset: 0x0000330B
		public static LocalizedString AnonymousAccessError
		{
			get
			{
				return new LocalizedString("AnonymousAccessError", "Ex3C5591", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005129 File Offset: 0x00003329
		public static LocalizedString MismatchSyncStateError
		{
			get
			{
				return new LocalizedString("MismatchSyncStateError", "Ex73418B", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005148 File Offset: 0x00003348
		public static LocalizedString SchemaFileCorrupted(string fileName)
		{
			return new LocalizedString("SchemaFileCorrupted", "", false, false, EASServerStrings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005178 File Offset: 0x00003378
		public static LocalizedString FailedToApplySecurityToContainer(string container)
		{
			return new LocalizedString("FailedToApplySecurityToContainer", "Ex032E9A", false, true, EASServerStrings.ResourceManager, new object[]
			{
				container
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000051A8 File Offset: 0x000033A8
		public static LocalizedString InvalidDeviceFilterOperatorError(string filterOperator)
		{
			return new LocalizedString("InvalidDeviceFilterOperatorError", "", false, false, EASServerStrings.ResourceManager, new object[]
			{
				filterOperator
			});
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000051D7 File Offset: 0x000033D7
		public static LocalizedString AdminMailBodyDeviceOS
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceOS", "Ex23054B", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000051F8 File Offset: 0x000033F8
		public static LocalizedString HttpStatusCode(int statusCode)
		{
			return new LocalizedString("HttpStatusCode", "Ex00CE77", false, true, EASServerStrings.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000522C File Offset: 0x0000342C
		public static LocalizedString UnableToLoadAddressBookProvider
		{
			get
			{
				return new LocalizedString("UnableToLoadAddressBookProvider", "Ex31DE6B", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000524C File Offset: 0x0000344C
		public static LocalizedString ExType(string type)
		{
			return new LocalizedString("ExType", "Ex06B4F9", false, true, EASServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000527C File Offset: 0x0000347C
		public static LocalizedString InvalidDeviceFilterSettingsInAD(string organizationId, string errorDescription)
		{
			return new LocalizedString("InvalidDeviceFilterSettingsInAD", "", false, false, EASServerStrings.ResourceManager, new object[]
			{
				organizationId,
				errorDescription
			});
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000052B0 File Offset: 0x000034B0
		public static LocalizedString ExStackTrace(string trace)
		{
			return new LocalizedString("ExStackTrace", "Ex23A291", false, true, EASServerStrings.ResourceManager, new object[]
			{
				trace
			});
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000052DF File Offset: 0x000034DF
		public static LocalizedString AdminMailUser
		{
			get
			{
				return new LocalizedString("AdminMailUser", "ExC95404", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000052FD File Offset: 0x000034FD
		public static LocalizedString AdminMailBody1
		{
			get
			{
				return new LocalizedString("AdminMailBody1", "ExCBE0FE", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000531B File Offset: 0x0000351B
		public static LocalizedString ExInner
		{
			get
			{
				return new LocalizedString("ExInner", "Ex8687A5", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005339 File Offset: 0x00003539
		public static LocalizedString AdminMailBodyDeviceAccessStateReason
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceAccessStateReason", "ExE25DF3", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005358 File Offset: 0x00003558
		public static LocalizedString AdminMailBodySentAt(string dateTime, string recipientsSMTP)
		{
			return new LocalizedString("AdminMailBodySentAt", "Ex0512BC", false, true, EASServerStrings.ResourceManager, new object[]
			{
				dateTime,
				recipientsSMTP
			});
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000538C File Offset: 0x0000358C
		public static LocalizedString MandatoryVirtualDirectoryDeleted(string dn)
		{
			return new LocalizedString("MandatoryVirtualDirectoryDeleted", "ExB7423B", false, true, EASServerStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000053BC File Offset: 0x000035BC
		public static LocalizedString AdminMailSubject(string displayName, string alias)
		{
			return new LocalizedString("AdminMailSubject", "ExA6B592", false, true, EASServerStrings.ResourceManager, new object[]
			{
				displayName,
				alias
			});
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000053EF File Offset: 0x000035EF
		public static LocalizedString UnhandledException
		{
			get
			{
				return new LocalizedString("UnhandledException", "ExA2F74B", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000540D File Offset: 0x0000360D
		public static LocalizedString AdminMailBody2
		{
			get
			{
				return new LocalizedString("AdminMailBody2", "ExFCFC8F", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000542C File Offset: 0x0000362C
		public static LocalizedString FailedToResolveWellKnownGuid(string guid, string name)
		{
			return new LocalizedString("FailedToResolveWellKnownGuid", "ExCF1342", false, true, EASServerStrings.ResourceManager, new object[]
			{
				guid,
				name
			});
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000545F File Offset: 0x0000365F
		public static LocalizedString NoXmlResponse
		{
			get
			{
				return new LocalizedString("NoXmlResponse", "Ex4C1BFF", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000547D File Offset: 0x0000367D
		public static LocalizedString AdminMailDeviceInformation
		{
			get
			{
				return new LocalizedString("AdminMailDeviceInformation", "Ex660EE0", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000549B File Offset: 0x0000369B
		public static LocalizedString AdminMailDevicePolicyStatus
		{
			get
			{
				return new LocalizedString("AdminMailDevicePolicyStatus", "Ex18EE20", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000054B9 File Offset: 0x000036B9
		public static LocalizedString AdminMailBodyDeviceUserAgent
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceUserAgent", "Ex591F4C", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000054D7 File Offset: 0x000036D7
		public static LocalizedString AdminMailDevicePolicyApplied
		{
			get
			{
				return new LocalizedString("AdminMailDevicePolicyApplied", "Ex9A8459", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000054F8 File Offset: 0x000036F8
		public static LocalizedString NullNTSD(string name)
		{
			return new LocalizedString("NullNTSD", "ExAF3C19", false, true, EASServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005528 File Offset: 0x00003728
		public static LocalizedString SchemaDirectoryVersionNotAccessible(string dirPath)
		{
			return new LocalizedString("SchemaDirectoryVersionNotAccessible", "", false, false, EASServerStrings.ResourceManager, new object[]
			{
				dirPath
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005558 File Offset: 0x00003758
		public static LocalizedString ExLevel(string level)
		{
			return new LocalizedString("ExLevel", "Ex4B2ED2", false, true, EASServerStrings.ResourceManager, new object[]
			{
				level
			});
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005588 File Offset: 0x00003788
		public static LocalizedString MissingADServer(string serverId)
		{
			return new LocalizedString("MissingADServer", "Ex114E29", false, true, EASServerStrings.ResourceManager, new object[]
			{
				serverId
			});
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000055B7 File Offset: 0x000037B7
		public static LocalizedString AdminMailBodyDeviceIMEI
		{
			get
			{
				return new LocalizedString("AdminMailBodyDeviceIMEI", "Ex4D98C5", false, true, EASServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000055D5 File Offset: 0x000037D5
		public static LocalizedString GetLocalizedString(EASServerStrings.IDs key)
		{
			return new LocalizedString(EASServerStrings.stringIDs[(uint)key], EASServerStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000155 RID: 341
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(29);

		// Token: 0x04000156 RID: 342
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.AirSync.EASServerStrings", typeof(EASServerStrings).GetTypeInfo().Assembly);

		// Token: 0x0200000A RID: 10
		public enum IDs : uint
		{
			// Token: 0x04000158 RID: 344
			AdminMailDevicePhoneNumber = 1807771821U,
			// Token: 0x04000159 RID: 345
			AdminMailBodyDeviceAccessState = 842327697U,
			// Token: 0x0400015A RID: 346
			AdminMailBodyDeviceModel = 2127272853U,
			// Token: 0x0400015B RID: 347
			AdminMailBody4 = 3481243890U,
			// Token: 0x0400015C RID: 348
			MissingDiscoveryInfoError = 4167251934U,
			// Token: 0x0400015D RID: 349
			AdminMailBodyDeviceID = 2341808075U,
			// Token: 0x0400015E RID: 350
			AdminMailDeviceAccessControlRule = 4280143591U,
			// Token: 0x0400015F RID: 351
			ExBegin = 3127591392U,
			// Token: 0x04000160 RID: 352
			Null = 1743625299U,
			// Token: 0x04000161 RID: 353
			AdminMailBodyEASVersion = 2057529303U,
			// Token: 0x04000162 RID: 354
			SchemaDirectoryNotAccessible = 4013839307U,
			// Token: 0x04000163 RID: 355
			AdminMailBodyDeviceType = 1704351524U,
			// Token: 0x04000164 RID: 356
			ExEnd = 2227082144U,
			// Token: 0x04000165 RID: 357
			AnonymousAccessError = 25353299U,
			// Token: 0x04000166 RID: 358
			MismatchSyncStateError = 508148408U,
			// Token: 0x04000167 RID: 359
			AdminMailBodyDeviceOS = 1179008650U,
			// Token: 0x04000168 RID: 360
			UnableToLoadAddressBookProvider = 2628658688U,
			// Token: 0x04000169 RID: 361
			AdminMailUser = 2920971799U,
			// Token: 0x0400016A RID: 362
			AdminMailBody1 = 3077959363U,
			// Token: 0x0400016B RID: 363
			ExInner = 862352803U,
			// Token: 0x0400016C RID: 364
			AdminMailBodyDeviceAccessStateReason = 2474028085U,
			// Token: 0x0400016D RID: 365
			UnhandledException = 134200922U,
			// Token: 0x0400016E RID: 366
			AdminMailBody2 = 2674674836U,
			// Token: 0x0400016F RID: 367
			NoXmlResponse = 3929860227U,
			// Token: 0x04000170 RID: 368
			AdminMailDeviceInformation = 3181624402U,
			// Token: 0x04000171 RID: 369
			AdminMailDevicePolicyStatus = 2585038184U,
			// Token: 0x04000172 RID: 370
			AdminMailBodyDeviceUserAgent = 1372331172U,
			// Token: 0x04000173 RID: 371
			AdminMailDevicePolicyApplied = 3870834299U,
			// Token: 0x04000174 RID: 372
			AdminMailBodyDeviceIMEI = 655864836U
		}

		// Token: 0x0200000B RID: 11
		private enum ParamIDs
		{
			// Token: 0x04000176 RID: 374
			SchemaUnknownCompilationError,
			// Token: 0x04000177 RID: 375
			CannotFindSchemaClassException,
			// Token: 0x04000178 RID: 376
			XmlResponse,
			// Token: 0x04000179 RID: 377
			FailedToCreateNewActiveDevice,
			// Token: 0x0400017A RID: 378
			MultipleVirtualDirectoriesDetected,
			// Token: 0x0400017B RID: 379
			MissingADVirtualDirectory,
			// Token: 0x0400017C RID: 380
			SyncStatusCode,
			// Token: 0x0400017D RID: 381
			ExMessage,
			// Token: 0x0400017E RID: 382
			SchemaFileCorrupted,
			// Token: 0x0400017F RID: 383
			FailedToApplySecurityToContainer,
			// Token: 0x04000180 RID: 384
			InvalidDeviceFilterOperatorError,
			// Token: 0x04000181 RID: 385
			HttpStatusCode,
			// Token: 0x04000182 RID: 386
			ExType,
			// Token: 0x04000183 RID: 387
			InvalidDeviceFilterSettingsInAD,
			// Token: 0x04000184 RID: 388
			ExStackTrace,
			// Token: 0x04000185 RID: 389
			AdminMailBodySentAt,
			// Token: 0x04000186 RID: 390
			MandatoryVirtualDirectoryDeleted,
			// Token: 0x04000187 RID: 391
			AdminMailSubject,
			// Token: 0x04000188 RID: 392
			FailedToResolveWellKnownGuid,
			// Token: 0x04000189 RID: 393
			NullNTSD,
			// Token: 0x0400018A RID: 394
			SchemaDirectoryVersionNotAccessible,
			// Token: 0x0400018B RID: 395
			ExLevel,
			// Token: 0x0400018C RID: 396
			MissingADServer
		}
	}
}
