using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000045 RID: 69
	internal static class Strings
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00008F1C File Offset: 0x0000711C
		static Strings()
		{
			Strings.stringIDs.Add(957461802U, "InvalidFormatQuery");
			Strings.stringIDs.Add(819773363U, "ErrorInvalidVersion");
			Strings.stringIDs.Add(3351215994U, "UnknownError");
			Strings.stringIDs.Add(3512077190U, "ADOperationError");
			Strings.stringIDs.Add(3594659945U, "ADTransientError");
			Strings.stringIDs.Add(136560791U, "ErrorOverBudget");
			Strings.stringIDs.Add(2772872006U, "DataMartTimeoutException");
			Strings.stringIDs.Add(4183935500U, "InvalidQueryException");
			Strings.stringIDs.Add(238685540U, "ErrorMissingTenantDomainInRequest");
			Strings.stringIDs.Add(3447632727U, "ErrorSchemaInitializationFail");
			Strings.stringIDs.Add(267521403U, "UserUnauthenticated");
			Strings.stringIDs.Add(3837723474U, "CreateRunspaceConfigTimeoutError");
			Strings.stringIDs.Add(2878617116U, "ErrorVersionAmbiguous");
			Strings.stringIDs.Add(3760355046U, "UserNotSet");
			Strings.stringIDs.Add(3646759096U, "ConnectionFailedException");
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009084 File Offset: 0x00007284
		public static LocalizedString InvalidFormatQuery
		{
			get
			{
				return new LocalizedString("InvalidFormatQuery", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000909C File Offset: 0x0000729C
		public static LocalizedString ErrorTenantNotInOrgScope(string tenant)
		{
			return new LocalizedString("ErrorTenantNotInOrgScope", Strings.ResourceManager, new object[]
			{
				tenant
			});
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000090C4 File Offset: 0x000072C4
		public static LocalizedString ErrorInvalidVersion
		{
			get
			{
				return new LocalizedString("ErrorInvalidVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000090DB File Offset: 0x000072DB
		public static LocalizedString UnknownError
		{
			get
			{
				return new LocalizedString("UnknownError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000090F2 File Offset: 0x000072F2
		public static LocalizedString ADOperationError
		{
			get
			{
				return new LocalizedString("ADOperationError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009109 File Offset: 0x00007309
		public static LocalizedString ADTransientError
		{
			get
			{
				return new LocalizedString("ADTransientError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009120 File Offset: 0x00007320
		public static LocalizedString ErrorOverBudget
		{
			get
			{
				return new LocalizedString("ErrorOverBudget", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009138 File Offset: 0x00007338
		public static LocalizedString ErrorSchemaConfigurationFileMissing(string path)
		{
			return new LocalizedString("ErrorSchemaConfigurationFileMissing", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009160 File Offset: 0x00007360
		public static LocalizedString ErrorTenantNotResolved(string tenant)
		{
			return new LocalizedString("ErrorTenantNotResolved", Strings.ResourceManager, new object[]
			{
				tenant
			});
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009188 File Offset: 0x00007388
		public static LocalizedString DataMartTimeoutException
		{
			get
			{
				return new LocalizedString("DataMartTimeoutException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000919F File Offset: 0x0000739F
		public static LocalizedString InvalidQueryException
		{
			get
			{
				return new LocalizedString("InvalidQueryException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000091B8 File Offset: 0x000073B8
		public static LocalizedString ErrorOAuthAuthorizationNoAccount(string oauthIdentity)
		{
			return new LocalizedString("ErrorOAuthAuthorizationNoAccount", Strings.ResourceManager, new object[]
			{
				oauthIdentity
			});
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000091E0 File Offset: 0x000073E0
		public static LocalizedString ErrorMissingTenantDomainInRequest
		{
			get
			{
				return new LocalizedString("ErrorMissingTenantDomainInRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000091F7 File Offset: 0x000073F7
		public static LocalizedString ErrorSchemaInitializationFail
		{
			get
			{
				return new LocalizedString("ErrorSchemaInitializationFail", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000920E File Offset: 0x0000740E
		public static LocalizedString UserUnauthenticated
		{
			get
			{
				return new LocalizedString("UserUnauthenticated", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009225 File Offset: 0x00007425
		public static LocalizedString CreateRunspaceConfigTimeoutError
		{
			get
			{
				return new LocalizedString("CreateRunspaceConfigTimeoutError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000923C File Offset: 0x0000743C
		public static LocalizedString ErrorVersionAmbiguous
		{
			get
			{
				return new LocalizedString("ErrorVersionAmbiguous", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00009253 File Offset: 0x00007453
		public static LocalizedString UserNotSet
		{
			get
			{
				return new LocalizedString("UserNotSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000926A File Offset: 0x0000746A
		public static LocalizedString ConnectionFailedException
		{
			get
			{
				return new LocalizedString("ConnectionFailedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009281 File Offset: 0x00007481
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000139 RID: 313
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(15);

		// Token: 0x0400013A RID: 314
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ReportingWebService.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000046 RID: 70
		public enum IDs : uint
		{
			// Token: 0x0400013C RID: 316
			InvalidFormatQuery = 957461802U,
			// Token: 0x0400013D RID: 317
			ErrorInvalidVersion = 819773363U,
			// Token: 0x0400013E RID: 318
			UnknownError = 3351215994U,
			// Token: 0x0400013F RID: 319
			ADOperationError = 3512077190U,
			// Token: 0x04000140 RID: 320
			ADTransientError = 3594659945U,
			// Token: 0x04000141 RID: 321
			ErrorOverBudget = 136560791U,
			// Token: 0x04000142 RID: 322
			DataMartTimeoutException = 2772872006U,
			// Token: 0x04000143 RID: 323
			InvalidQueryException = 4183935500U,
			// Token: 0x04000144 RID: 324
			ErrorMissingTenantDomainInRequest = 238685540U,
			// Token: 0x04000145 RID: 325
			ErrorSchemaInitializationFail = 3447632727U,
			// Token: 0x04000146 RID: 326
			UserUnauthenticated = 267521403U,
			// Token: 0x04000147 RID: 327
			CreateRunspaceConfigTimeoutError = 3837723474U,
			// Token: 0x04000148 RID: 328
			ErrorVersionAmbiguous = 2878617116U,
			// Token: 0x04000149 RID: 329
			UserNotSet = 3760355046U,
			// Token: 0x0400014A RID: 330
			ConnectionFailedException = 3646759096U
		}

		// Token: 0x02000047 RID: 71
		private enum ParamIDs
		{
			// Token: 0x0400014C RID: 332
			ErrorTenantNotInOrgScope,
			// Token: 0x0400014D RID: 333
			ErrorSchemaConfigurationFileMissing,
			// Token: 0x0400014E RID: 334
			ErrorTenantNotResolved,
			// Token: 0x0400014F RID: 335
			ErrorOAuthAuthorizationNoAccount
		}
	}
}
