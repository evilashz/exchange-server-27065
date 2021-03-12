using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ADObjectWrapperFactory
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000030B0 File Offset: 0x000012B0
		public static IADObjectCommon CreateWrapper(ADObject adObject)
		{
			if (adObject == null)
			{
				return null;
			}
			if (adObject is Database)
			{
				return ADObjectWrapperFactory.CreateWrapper((Database)adObject);
			}
			if (adObject is MiniDatabase)
			{
				return ADObjectWrapperFactory.CreateWrapper((MiniDatabase)adObject);
			}
			if (adObject is DatabaseCopy)
			{
				return ADObjectWrapperFactory.CreateWrapper((DatabaseCopy)adObject);
			}
			if (adObject is ADComputer)
			{
				return ADObjectWrapperFactory.CreateWrapper((ADComputer)adObject);
			}
			if (adObject is Server)
			{
				return ADObjectWrapperFactory.CreateWrapper((Server)adObject);
			}
			if (adObject is MiniServer)
			{
				return ADObjectWrapperFactory.CreateWrapper((MiniServer)adObject);
			}
			if (adObject is DatabaseAvailabilityGroup)
			{
				return ADObjectWrapperFactory.CreateWrapper((DatabaseAvailabilityGroup)adObject);
			}
			if (adObject is ClientAccessArray)
			{
				return ADObjectWrapperFactory.CreateWrapper((ClientAccessArray)adObject);
			}
			if (adObject is MiniClientAccessServerOrArray)
			{
				return ADObjectWrapperFactory.CreateWrapper((MiniClientAccessServerOrArray)adObject);
			}
			if (adObject is ADSite)
			{
				return ADObjectWrapperFactory.CreateWrapper((ADSite)adObject);
			}
			ExAssert.RetailAssert(false, "Type '{0}' is not supported by CreateWrapper", new object[]
			{
				adObject.GetType()
			});
			return null;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000031A7 File Offset: 0x000013A7
		public static ADDatabaseWrapper CreateWrapper(Database database)
		{
			return ADDatabaseWrapper.CreateWrapper(database);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031AF File Offset: 0x000013AF
		public static ADDatabaseWrapper CreateWrapper(MiniDatabase database)
		{
			return ADDatabaseWrapper.CreateWrapper(database);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000031B7 File Offset: 0x000013B7
		public static ADDatabaseCopyWrapper CreateWrapper(DatabaseCopy databaseCopy)
		{
			return ADDatabaseCopyWrapper.CreateWrapper(databaseCopy);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000031BF File Offset: 0x000013BF
		public static ADComputerWrapper CreateWrapper(ADComputer adComputer)
		{
			return ADComputerWrapper.CreateWrapper(adComputer);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000031C7 File Offset: 0x000013C7
		public static ADServerWrapper CreateWrapper(Server server)
		{
			return ADServerWrapper.CreateWrapper(server);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000031CF File Offset: 0x000013CF
		public static ADServerWrapper CreateWrapper(MiniServer server)
		{
			return ADServerWrapper.CreateWrapper(server);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031D7 File Offset: 0x000013D7
		public static ADDatabaseAvailabilityGroupWrapper CreateWrapper(DatabaseAvailabilityGroup dag)
		{
			return ADDatabaseAvailabilityGroupWrapper.CreateWrapper(dag);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031DF File Offset: 0x000013DF
		public static ADClientAccessArrayWrapper CreateWrapper(ClientAccessArray caArray)
		{
			return ADClientAccessArrayWrapper.CreateWrapper(caArray);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031E7 File Offset: 0x000013E7
		public static ADMiniClientAccessServerOrArrayWrapper CreateWrapper(MiniClientAccessServerOrArray caServerOrArray)
		{
			return ADMiniClientAccessServerOrArrayWrapper.CreateWrapper(caServerOrArray);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031EF File Offset: 0x000013EF
		public static ADSiteWrapper CreateWrapper(ADSite adSite)
		{
			return ADSiteWrapper.CreateWrapper(adSite);
		}
	}
}
