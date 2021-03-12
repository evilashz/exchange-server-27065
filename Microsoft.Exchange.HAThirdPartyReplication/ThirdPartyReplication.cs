using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000011 RID: 17
	internal static class ThirdPartyReplication
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002D20 File Offset: 0x00000F20
		static ThirdPartyReplication()
		{
			ThirdPartyReplication.stringIDs.Add(3969839167U, "NoPAMDesignated");
			ThirdPartyReplication.stringIDs.Add(3499394288U, "NotAuthorizedError");
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D84 File Offset: 0x00000F84
		public static LocalizedString TPRBaseError(string error)
		{
			return new LocalizedString("TPRBaseError", "Ex382280", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public static LocalizedString NoCopyOnServer(Guid dbId, string dbName, string serverName)
		{
			return new LocalizedString("NoCopyOnServer", "ExCDCA66", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				dbId,
				dbName,
				serverName
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public static LocalizedString FailedCommunication(string reason)
		{
			return new LocalizedString("FailedCommunication", "Ex90A3E6", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E20 File Offset: 0x00001020
		public static LocalizedString ImmediateDismountMailboxDatabaseFailed(Guid dbId, string reason)
		{
			return new LocalizedString("ImmediateDismountMailboxDatabaseFailed", "Ex7FF234", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				dbId,
				reason
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E58 File Offset: 0x00001058
		public static LocalizedString NoPAMDesignated
		{
			get
			{
				return new LocalizedString("NoPAMDesignated", "ExE29DBC", false, true, ThirdPartyReplication.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E78 File Offset: 0x00001078
		public static LocalizedString OnlyPAMError(string apiName)
		{
			return new LocalizedString("OnlyPAMError", "Ex754C02", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				apiName
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static LocalizedString NoSuchDatabase(Guid dbId)
		{
			return new LocalizedString("NoSuchDatabase", "ExC6D439", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				dbId
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EDC File Offset: 0x000010DC
		public static LocalizedString GetPamError(string reason)
		{
			return new LocalizedString("GetPamError", "Ex533813", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F0C File Offset: 0x0000110C
		public static LocalizedString ChangeActiveServerFailed(Guid dbId, string newServer, string reason)
		{
			return new LocalizedString("ChangeActiveServerFailed", "Ex21120F", false, true, ThirdPartyReplication.ResourceManager, new object[]
			{
				dbId,
				newServer,
				reason
			});
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F48 File Offset: 0x00001148
		public static LocalizedString NotAuthorizedError
		{
			get
			{
				return new LocalizedString("NotAuthorizedError", "Ex7EFE98", false, true, ThirdPartyReplication.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F66 File Offset: 0x00001166
		public static LocalizedString GetLocalizedString(ThirdPartyReplication.IDs key)
		{
			return new LocalizedString(ThirdPartyReplication.stringIDs[(uint)key], ThirdPartyReplication.ResourceManager, new object[0]);
		}

		// Token: 0x04000010 RID: 16
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000011 RID: 17
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.ThirdPartyReplication.Strings", typeof(ThirdPartyReplication).GetTypeInfo().Assembly);

		// Token: 0x02000012 RID: 18
		public enum IDs : uint
		{
			// Token: 0x04000013 RID: 19
			NoPAMDesignated = 3969839167U,
			// Token: 0x04000014 RID: 20
			NotAuthorizedError = 3499394288U
		}

		// Token: 0x02000013 RID: 19
		private enum ParamIDs
		{
			// Token: 0x04000016 RID: 22
			TPRBaseError,
			// Token: 0x04000017 RID: 23
			NoCopyOnServer,
			// Token: 0x04000018 RID: 24
			FailedCommunication,
			// Token: 0x04000019 RID: 25
			ImmediateDismountMailboxDatabaseFailed,
			// Token: 0x0400001A RID: 26
			OnlyPAMError,
			// Token: 0x0400001B RID: 27
			NoSuchDatabase,
			// Token: 0x0400001C RID: 28
			GetPamError,
			// Token: 0x0400001D RID: 29
			ChangeActiveServerFailed
		}
	}
}
