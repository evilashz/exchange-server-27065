using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Core.LocStrings
{
	// Token: 0x0200002D RID: 45
	internal static class Strings
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00007080 File Offset: 0x00005280
		static Strings()
		{
			Strings.stringIDs.Add(3249200815U, "MissingPartitionId");
			Strings.stringIDs.Add(2408588661U, "InvalidFlighting");
			Strings.stringIDs.Add(1505454489U, "MissingDelegatedPrincipal");
			Strings.stringIDs.Add(2738351686U, "MissingVersion");
			Strings.stringIDs.Add(945651915U, "MissingUserSid");
			Strings.stringIDs.Add(2311412808U, "MissingWindowsLiveId");
			Strings.stringIDs.Add(3093790777U, "MissingAppPasswordUsed");
			Strings.stringIDs.Add(3582203768U, "MissingManagedOrganization");
			Strings.stringIDs.Add(1519135930U, "MissingUserName");
			Strings.stringIDs.Add(1121806018U, "MissingAuthenticationType");
			Strings.stringIDs.Add(481556215U, "MissingOrganization");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007198 File Offset: 0x00005398
		public static LocalizedString FailedToReceiveWinRMData(string identity)
		{
			return new LocalizedString("FailedToReceiveWinRMData", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000071C0 File Offset: 0x000053C0
		public static LocalizedString InvalidPartitionId(string value)
		{
			return new LocalizedString("InvalidPartitionId", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000071E8 File Offset: 0x000053E8
		public static LocalizedString InvalidDelegatedPrincipal(string value)
		{
			return new LocalizedString("InvalidDelegatedPrincipal", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007210 File Offset: 0x00005410
		public static LocalizedString WinRMDataKeyNotFound(string identity, string key)
		{
			return new LocalizedString("WinRMDataKeyNotFound", Strings.ResourceManager, new object[]
			{
				identity,
				key
			});
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000723C File Offset: 0x0000543C
		public static LocalizedString MissingPartitionId
		{
			get
			{
				return new LocalizedString("MissingPartitionId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007253 File Offset: 0x00005453
		public static LocalizedString InvalidFlighting
		{
			get
			{
				return new LocalizedString("InvalidFlighting", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000726A File Offset: 0x0000546A
		public static LocalizedString MissingDelegatedPrincipal
		{
			get
			{
				return new LocalizedString("MissingDelegatedPrincipal", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007281 File Offset: 0x00005481
		public static LocalizedString MissingVersion
		{
			get
			{
				return new LocalizedString("MissingVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007298 File Offset: 0x00005498
		public static LocalizedString InvalidAuthenticationType(string value)
		{
			return new LocalizedString("InvalidAuthenticationType", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000072C0 File Offset: 0x000054C0
		public static LocalizedString MissingUserSid
		{
			get
			{
				return new LocalizedString("MissingUserSid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000072D7 File Offset: 0x000054D7
		public static LocalizedString MissingWindowsLiveId
		{
			get
			{
				return new LocalizedString("MissingWindowsLiveId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000072EE File Offset: 0x000054EE
		public static LocalizedString MissingAppPasswordUsed
		{
			get
			{
				return new LocalizedString("MissingAppPasswordUsed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00007305 File Offset: 0x00005505
		public static LocalizedString MissingManagedOrganization
		{
			get
			{
				return new LocalizedString("MissingManagedOrganization", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000731C File Offset: 0x0000551C
		public static LocalizedString InvalidOrganization(string value)
		{
			return new LocalizedString("InvalidOrganization", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00007344 File Offset: 0x00005544
		public static LocalizedString MissingUserName
		{
			get
			{
				return new LocalizedString("MissingUserName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000735C File Offset: 0x0000555C
		public static LocalizedString UserTokenException(string reason)
		{
			return new LocalizedString("UserTokenException", Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00007384 File Offset: 0x00005584
		public static LocalizedString MissingAuthenticationType
		{
			get
			{
				return new LocalizedString("MissingAuthenticationType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000739B File Offset: 0x0000559B
		public static LocalizedString MissingOrganization
		{
			get
			{
				return new LocalizedString("MissingOrganization", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000073B4 File Offset: 0x000055B4
		public static LocalizedString InvalidUserSid(string value)
		{
			return new LocalizedString("InvalidUserSid", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000073DC File Offset: 0x000055DC
		public static LocalizedString IllegalItemValue(string value)
		{
			return new LocalizedString("IllegalItemValue", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007404 File Offset: 0x00005604
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040000AE RID: 174
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(11);

		// Token: 0x040000AF RID: 175
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Configuration.Core.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200002E RID: 46
		public enum IDs : uint
		{
			// Token: 0x040000B1 RID: 177
			MissingPartitionId = 3249200815U,
			// Token: 0x040000B2 RID: 178
			InvalidFlighting = 2408588661U,
			// Token: 0x040000B3 RID: 179
			MissingDelegatedPrincipal = 1505454489U,
			// Token: 0x040000B4 RID: 180
			MissingVersion = 2738351686U,
			// Token: 0x040000B5 RID: 181
			MissingUserSid = 945651915U,
			// Token: 0x040000B6 RID: 182
			MissingWindowsLiveId = 2311412808U,
			// Token: 0x040000B7 RID: 183
			MissingAppPasswordUsed = 3093790777U,
			// Token: 0x040000B8 RID: 184
			MissingManagedOrganization = 3582203768U,
			// Token: 0x040000B9 RID: 185
			MissingUserName = 1519135930U,
			// Token: 0x040000BA RID: 186
			MissingAuthenticationType = 1121806018U,
			// Token: 0x040000BB RID: 187
			MissingOrganization = 481556215U
		}

		// Token: 0x0200002F RID: 47
		private enum ParamIDs
		{
			// Token: 0x040000BD RID: 189
			FailedToReceiveWinRMData,
			// Token: 0x040000BE RID: 190
			InvalidPartitionId,
			// Token: 0x040000BF RID: 191
			InvalidDelegatedPrincipal,
			// Token: 0x040000C0 RID: 192
			WinRMDataKeyNotFound,
			// Token: 0x040000C1 RID: 193
			InvalidAuthenticationType,
			// Token: 0x040000C2 RID: 194
			InvalidOrganization,
			// Token: 0x040000C3 RID: 195
			UserTokenException,
			// Token: 0x040000C4 RID: 196
			InvalidUserSid,
			// Token: 0x040000C5 RID: 197
			IllegalItemValue
		}
	}
}
