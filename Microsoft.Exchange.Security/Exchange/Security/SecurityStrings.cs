using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security
{
	// Token: 0x0200000D RID: 13
	internal static class SecurityStrings
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00005C04 File Offset: 0x00003E04
		static SecurityStrings()
		{
			SecurityStrings.stringIDs.Add(3271480525U, "ResultThumbprintNotSet");
			SecurityStrings.stringIDs.Add(1735881142U, "ReadCertificates");
			SecurityStrings.stringIDs.Add(3167281586U, "LoadPartnerApplication");
			SecurityStrings.stringIDs.Add(3026893530U, "CheckCurrentCertificate");
			SecurityStrings.stringIDs.Add(1008692634U, "CheckNextCertificate");
			SecurityStrings.stringIDs.Add(3117434263U, "CheckAuthConfigRealm");
			SecurityStrings.stringIDs.Add(227590970U, "ResultAuthServerDisabled");
			SecurityStrings.stringIDs.Add(2753441348U, "ResultAuthServerOK");
			SecurityStrings.stringIDs.Add(140719347U, "LoadAuthServer");
			SecurityStrings.stringIDs.Add(3171288429U, "ResultAuthConfigFound");
			SecurityStrings.stringIDs.Add(1849315756U, "LoadConfiguration");
			SecurityStrings.stringIDs.Add(1673247710U, "CheckServiceName");
			SecurityStrings.stringIDs.Add(1391827654U, "MissingWindowsAccessToken");
			SecurityStrings.stringIDs.Add(92707853U, "ResultPartnerApplicationDisabled");
			SecurityStrings.stringIDs.Add(3173209329U, "ResultPartnerApplicationOK");
			SecurityStrings.stringIDs.Add(2239003722U, "LoadAuthConfig");
			SecurityStrings.stringIDs.Add(664393098U, "CheckPreviousCertificate");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005D94 File Offset: 0x00003F94
		public static LocalizedString CannotResolveOrganization(string orgName)
		{
			return new LocalizedString("CannotResolveOrganization", SecurityStrings.ResourceManager, new object[]
			{
				orgName
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005DBC File Offset: 0x00003FBC
		public static LocalizedString LowPasswordConfidence(string upn)
		{
			return new LocalizedString("LowPasswordConfidence", SecurityStrings.ResourceManager, new object[]
			{
				upn
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public static LocalizedString UserIsDisabled(string upn)
		{
			return new LocalizedString("UserIsDisabled", SecurityStrings.ResourceManager, new object[]
			{
				upn
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00005E0C File Offset: 0x0000400C
		public static LocalizedString ResultThumbprintNotSet
		{
			get
			{
				return new LocalizedString("ResultThumbprintNotSet", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005E24 File Offset: 0x00004024
		public static LocalizedString SourceServerNoTokenSerializationPermission(string serverIdentity)
		{
			return new LocalizedString("SourceServerNoTokenSerializationPermission", SecurityStrings.ResourceManager, new object[]
			{
				serverIdentity
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00005E4C File Offset: 0x0000404C
		public static LocalizedString ReadCertificates
		{
			get
			{
				return new LocalizedString("ReadCertificates", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00005E63 File Offset: 0x00004063
		public static LocalizedString LoadPartnerApplication
		{
			get
			{
				return new LocalizedString("LoadPartnerApplication", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005E7C File Offset: 0x0000407C
		public static LocalizedString ResultCertHasNoPrivateKey(string thumbprint)
		{
			return new LocalizedString("ResultCertHasNoPrivateKey", SecurityStrings.ResourceManager, new object[]
			{
				thumbprint
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static LocalizedString CheckCurrentCertificate
		{
			get
			{
				return new LocalizedString("CheckCurrentCertificate", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00005EBB File Offset: 0x000040BB
		public static LocalizedString CheckNextCertificate
		{
			get
			{
				return new LocalizedString("CheckNextCertificate", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005ED4 File Offset: 0x000040D4
		public static LocalizedString ResultADOperationFailure(string code, string message)
		{
			return new LocalizedString("ResultADOperationFailure", SecurityStrings.ResourceManager, new object[]
			{
				code,
				message
			});
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005F00 File Offset: 0x00004100
		public static LocalizedString FailedToLogon(string upn)
		{
			return new LocalizedString("FailedToLogon", SecurityStrings.ResourceManager, new object[]
			{
				upn
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005F28 File Offset: 0x00004128
		public static LocalizedString CheckAuthServer(string name)
		{
			return new LocalizedString("CheckAuthServer", SecurityStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00005F50 File Offset: 0x00004150
		public static LocalizedString CheckAuthConfigRealm
		{
			get
			{
				return new LocalizedString("CheckAuthConfigRealm", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005F68 File Offset: 0x00004168
		public static LocalizedString ResultCertInvalid(string thumbprint, DateTime notBefore, DateTime notAfer)
		{
			return new LocalizedString("ResultCertInvalid", SecurityStrings.ResourceManager, new object[]
			{
				thumbprint,
				notBefore,
				notAfer
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005FA4 File Offset: 0x000041A4
		public static LocalizedString MissingExtensionDataKey(string key)
		{
			return new LocalizedString("MissingExtensionDataKey", SecurityStrings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00005FCC File Offset: 0x000041CC
		public static LocalizedString ResultAuthServerDisabled
		{
			get
			{
				return new LocalizedString("ResultAuthServerDisabled", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00005FE3 File Offset: 0x000041E3
		public static LocalizedString ResultAuthServerOK
		{
			get
			{
				return new LocalizedString("ResultAuthServerOK", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005FFC File Offset: 0x000041FC
		public static LocalizedString InvalidOAuthLinkedAccountException(string partnerApplication, string linkedAccount)
		{
			return new LocalizedString("InvalidOAuthLinkedAccountException", SecurityStrings.ResourceManager, new object[]
			{
				partnerApplication,
				linkedAccount
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00006028 File Offset: 0x00004228
		public static LocalizedString ResultMatchServiceNameDefaultValue(string actual)
		{
			return new LocalizedString("ResultMatchServiceNameDefaultValue", SecurityStrings.ResourceManager, new object[]
			{
				actual
			});
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00006050 File Offset: 0x00004250
		public static LocalizedString LoadAuthServer
		{
			get
			{
				return new LocalizedString("LoadAuthServer", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00006068 File Offset: 0x00004268
		public static LocalizedString CannotLookupUser(string partitionId, string sid, string reason)
		{
			return new LocalizedString("CannotLookupUser", SecurityStrings.ResourceManager, new object[]
			{
				partitionId,
				sid,
				reason
			});
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00006098 File Offset: 0x00004298
		public static LocalizedString ResultAuthConfigFound
		{
			get
			{
				return new LocalizedString("ResultAuthConfigFound", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000060AF File Offset: 0x000042AF
		public static LocalizedString LoadConfiguration
		{
			get
			{
				return new LocalizedString("LoadConfiguration", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000060C8 File Offset: 0x000042C8
		public static LocalizedString ResultDidNotMatchServiceNameDefaultValue(string actual, string expected)
		{
			return new LocalizedString("ResultDidNotMatchServiceNameDefaultValue", SecurityStrings.ResourceManager, new object[]
			{
				actual,
				expected
			});
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000060F4 File Offset: 0x000042F4
		public static LocalizedString ResultCertNotFound(string thumbprint)
		{
			return new LocalizedString("ResultCertNotFound", SecurityStrings.ResourceManager, new object[]
			{
				thumbprint
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000611C File Offset: 0x0000431C
		public static LocalizedString ResultAuthConfigRealmOverwrote(string overwrite)
		{
			return new LocalizedString("ResultAuthConfigRealmOverwrote", SecurityStrings.ResourceManager, new object[]
			{
				overwrite
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00006144 File Offset: 0x00004344
		public static LocalizedString ResultLoadConfigurationException(object exception)
		{
			return new LocalizedString("ResultLoadConfigurationException", SecurityStrings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000616C File Offset: 0x0000436C
		public static LocalizedString CheckServiceName
		{
			get
			{
				return new LocalizedString("CheckServiceName", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00006184 File Offset: 0x00004384
		public static LocalizedString ResultReadCertificatesException(object exception)
		{
			return new LocalizedString("ResultReadCertificatesException", SecurityStrings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000061AC File Offset: 0x000043AC
		public static LocalizedString LowPasswordConfidenceWithException(string upn, Exception e)
		{
			return new LocalizedString("LowPasswordConfidenceWithException", SecurityStrings.ResourceManager, new object[]
			{
				upn,
				e
			});
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000061D8 File Offset: 0x000043D8
		public static LocalizedString MissingWindowsAccessToken
		{
			get
			{
				return new LocalizedString("MissingWindowsAccessToken", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000061EF File Offset: 0x000043EF
		public static LocalizedString ResultPartnerApplicationDisabled
		{
			get
			{
				return new LocalizedString("ResultPartnerApplicationDisabled", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00006206 File Offset: 0x00004406
		public static LocalizedString ResultPartnerApplicationOK
		{
			get
			{
				return new LocalizedString("ResultPartnerApplicationOK", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00006220 File Offset: 0x00004420
		public static LocalizedString AccessTokenTypeNotSupported(string type)
		{
			return new LocalizedString("AccessTokenTypeNotSupported", SecurityStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00006248 File Offset: 0x00004448
		public static LocalizedString InvalidExtensionDataKey(string key)
		{
			return new LocalizedString("InvalidExtensionDataKey", SecurityStrings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00006270 File Offset: 0x00004470
		public static LocalizedString BackendRehydrationException(LocalizedString reason)
		{
			return new LocalizedString("BackendRehydrationException", SecurityStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000062A0 File Offset: 0x000044A0
		public static LocalizedString ResultPartnerApplicationsFound(int count)
		{
			return new LocalizedString("ResultPartnerApplicationsFound", SecurityStrings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000062D0 File Offset: 0x000044D0
		public static LocalizedString ResultAuthServersFound(int count)
		{
			return new LocalizedString("ResultAuthServersFound", SecurityStrings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00006300 File Offset: 0x00004500
		public static LocalizedString CheckPartnerApplication(string name)
		{
			return new LocalizedString("CheckPartnerApplication", SecurityStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00006328 File Offset: 0x00004528
		public static LocalizedString LoadAuthConfig
		{
			get
			{
				return new LocalizedString("LoadAuthConfig", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006340 File Offset: 0x00004540
		public static LocalizedString CannotLookupUserEx(string puid, string domainName)
		{
			return new LocalizedString("CannotLookupUserEx", SecurityStrings.ResourceManager, new object[]
			{
				puid,
				domainName
			});
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000636C File Offset: 0x0000456C
		public static LocalizedString ResultCertValid(string thumbprint)
		{
			return new LocalizedString("ResultCertValid", SecurityStrings.ResourceManager, new object[]
			{
				thumbprint
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00006394 File Offset: 0x00004594
		public static LocalizedString CheckPreviousCertificate
		{
			get
			{
				return new LocalizedString("CheckPreviousCertificate", SecurityStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000063AB File Offset: 0x000045AB
		public static LocalizedString GetLocalizedString(SecurityStrings.IDs key)
		{
			return new LocalizedString(SecurityStrings.stringIDs[(uint)key], SecurityStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040000F4 RID: 244
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(17);

		// Token: 0x040000F5 RID: 245
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Security.Strings", typeof(SecurityStrings).GetTypeInfo().Assembly);

		// Token: 0x0200000E RID: 14
		public enum IDs : uint
		{
			// Token: 0x040000F7 RID: 247
			ResultThumbprintNotSet = 3271480525U,
			// Token: 0x040000F8 RID: 248
			ReadCertificates = 1735881142U,
			// Token: 0x040000F9 RID: 249
			LoadPartnerApplication = 3167281586U,
			// Token: 0x040000FA RID: 250
			CheckCurrentCertificate = 3026893530U,
			// Token: 0x040000FB RID: 251
			CheckNextCertificate = 1008692634U,
			// Token: 0x040000FC RID: 252
			CheckAuthConfigRealm = 3117434263U,
			// Token: 0x040000FD RID: 253
			ResultAuthServerDisabled = 227590970U,
			// Token: 0x040000FE RID: 254
			ResultAuthServerOK = 2753441348U,
			// Token: 0x040000FF RID: 255
			LoadAuthServer = 140719347U,
			// Token: 0x04000100 RID: 256
			ResultAuthConfigFound = 3171288429U,
			// Token: 0x04000101 RID: 257
			LoadConfiguration = 1849315756U,
			// Token: 0x04000102 RID: 258
			CheckServiceName = 1673247710U,
			// Token: 0x04000103 RID: 259
			MissingWindowsAccessToken = 1391827654U,
			// Token: 0x04000104 RID: 260
			ResultPartnerApplicationDisabled = 92707853U,
			// Token: 0x04000105 RID: 261
			ResultPartnerApplicationOK = 3173209329U,
			// Token: 0x04000106 RID: 262
			LoadAuthConfig = 2239003722U,
			// Token: 0x04000107 RID: 263
			CheckPreviousCertificate = 664393098U
		}

		// Token: 0x0200000F RID: 15
		private enum ParamIDs
		{
			// Token: 0x04000109 RID: 265
			CannotResolveOrganization,
			// Token: 0x0400010A RID: 266
			LowPasswordConfidence,
			// Token: 0x0400010B RID: 267
			UserIsDisabled,
			// Token: 0x0400010C RID: 268
			SourceServerNoTokenSerializationPermission,
			// Token: 0x0400010D RID: 269
			ResultCertHasNoPrivateKey,
			// Token: 0x0400010E RID: 270
			ResultADOperationFailure,
			// Token: 0x0400010F RID: 271
			FailedToLogon,
			// Token: 0x04000110 RID: 272
			CheckAuthServer,
			// Token: 0x04000111 RID: 273
			ResultCertInvalid,
			// Token: 0x04000112 RID: 274
			MissingExtensionDataKey,
			// Token: 0x04000113 RID: 275
			InvalidOAuthLinkedAccountException,
			// Token: 0x04000114 RID: 276
			ResultMatchServiceNameDefaultValue,
			// Token: 0x04000115 RID: 277
			CannotLookupUser,
			// Token: 0x04000116 RID: 278
			ResultDidNotMatchServiceNameDefaultValue,
			// Token: 0x04000117 RID: 279
			ResultCertNotFound,
			// Token: 0x04000118 RID: 280
			ResultAuthConfigRealmOverwrote,
			// Token: 0x04000119 RID: 281
			ResultLoadConfigurationException,
			// Token: 0x0400011A RID: 282
			ResultReadCertificatesException,
			// Token: 0x0400011B RID: 283
			LowPasswordConfidenceWithException,
			// Token: 0x0400011C RID: 284
			AccessTokenTypeNotSupported,
			// Token: 0x0400011D RID: 285
			InvalidExtensionDataKey,
			// Token: 0x0400011E RID: 286
			BackendRehydrationException,
			// Token: 0x0400011F RID: 287
			ResultPartnerApplicationsFound,
			// Token: 0x04000120 RID: 288
			ResultAuthServersFound,
			// Token: 0x04000121 RID: 289
			CheckPartnerApplication,
			// Token: 0x04000122 RID: 290
			CannotLookupUserEx,
			// Token: 0x04000123 RID: 291
			ResultCertValid
		}
	}
}
