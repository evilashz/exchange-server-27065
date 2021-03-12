using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADTopologyConfigurationSessionWrapper : IADToplogyConfigurationSession
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00004714 File Offset: 0x00002914
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00004721 File Offset: 0x00002921
		public bool UseConfigNC
		{
			get
			{
				return this.m_session.UseConfigNC;
			}
			set
			{
				this.m_session.UseConfigNC = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000472F File Offset: 0x0000292F
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000473C File Offset: 0x0000293C
		public bool UseGlobalCatalog
		{
			get
			{
				return this.m_session.UseGlobalCatalog;
			}
			set
			{
				this.m_session.UseGlobalCatalog = value;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000474A File Offset: 0x0000294A
		public static ADTopologyConfigurationSessionWrapper CreateWrapper(ITopologyConfigurationSession session)
		{
			ExAssert.RetailAssert(session != null, "'session' instance to wrap must not be null!");
			return new ADTopologyConfigurationSessionWrapper(session);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004763 File Offset: 0x00002963
		private ADTopologyConfigurationSessionWrapper(ITopologyConfigurationSession session)
		{
			ExAssert.RetailAssert(session != null, "'session' instance to wrap must not be null!");
			this.m_session = session;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004784 File Offset: 0x00002984
		public IADServer FindServerByName(string serverShortName)
		{
			Server server = this.m_session.FindServerByName(serverShortName);
			return ADObjectWrapperFactory.CreateWrapper(server);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000047A4 File Offset: 0x000029A4
		public IADDatabaseAvailabilityGroup FindDagByServer(IADServer server)
		{
			if (server != null)
			{
				ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
				if (databaseAvailabilityGroup != null && !databaseAvailabilityGroup.IsDeleted)
				{
					DatabaseAvailabilityGroup dag = this.m_session.Read<DatabaseAvailabilityGroup>(databaseAvailabilityGroup);
					return ADObjectWrapperFactory.CreateWrapper(dag);
				}
			}
			return null;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000047DC File Offset: 0x000029DC
		public IADComputer FindComputerByHostName(string hostName)
		{
			ADComputer adComputer = this.m_session.FindComputerByHostName(hostName);
			return ADObjectWrapperFactory.CreateWrapper(adComputer);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000049E0 File Offset: 0x00002BE0
		public IEnumerable<IADDatabase> GetAllDatabases(IADServer server)
		{
			IEnumerable<DatabaseCopy> copies = this.GetAllRealDatabaseCopies(server);
			foreach (DatabaseCopy copy in copies)
			{
				ADObjectId dbId = ((ADObjectId)copy.Identity).Parent;
				IADDatabase database = this.ReadADObject<IADDatabase>(dbId);
				if (database != null)
				{
					yield return database;
				}
			}
			yield break;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004A0C File Offset: 0x00002C0C
		public IEnumerable<IADDatabaseCopy> GetAllDatabaseCopies(IADServer server)
		{
			IEnumerable<DatabaseCopy> allRealDatabaseCopies = this.GetAllRealDatabaseCopies(server);
			return from copy in allRealDatabaseCopies
			select ADObjectWrapperFactory.CreateWrapper(copy);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00004A48 File Offset: 0x00002C48
		private IEnumerable<DatabaseCopy> GetAllRealDatabaseCopies(IADServer server)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, server.Name);
			return this.m_session.FindPaged<DatabaseCopy>(null, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00004A7C File Offset: 0x00002C7C
		public IADDatabase FindDatabaseByGuid(Guid dbGuid)
		{
			if (dbGuid == Guid.Empty)
			{
				throw new ArgumentException("dbGuid cannot be Empty.");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, dbGuid);
			IADDatabase[] array = this.Find<IADDatabase>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length <= 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public TADWrapperObject ReadADObject<TADWrapperObject>(ADObjectId objectId) where TADWrapperObject : class, IADObjectCommon
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, objectId);
			TADWrapperObject[] array = this.Find<TADWrapperObject>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length <= 0)
			{
				return default(TADWrapperObject);
			}
			return array[0];
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00004B10 File Offset: 0x00002D10
		public TADWrapperObject[] Find<TADWrapperObject>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults) where TADWrapperObject : class, IADObjectCommon
		{
			if (typeof(TADWrapperObject).Equals(typeof(IADDatabase)))
			{
				IADDatabase[] array = (IADDatabase[])this.FindInternal<TADWrapperObject, MiniDatabase>(rootId, scope, filter, sortBy, maxResults, ADDatabaseWrapper.PropertiesNeededForDatabase);
				if (array != null)
				{
					foreach (IADDatabase iaddatabase in array)
					{
						((ADDatabaseWrapper)iaddatabase).FinishConstructionFromMiniDatabase(this.m_session);
					}
				}
				return (TADWrapperObject[])array;
			}
			if (typeof(TADWrapperObject).Equals(typeof(IADDatabaseCopy)))
			{
				return this.FindInternal<TADWrapperObject, DatabaseCopy>(rootId, scope, filter, sortBy, maxResults, null);
			}
			if (typeof(TADWrapperObject).Equals(typeof(IADServer)))
			{
				return this.FindInternal<TADWrapperObject, MiniServer>(rootId, scope, filter, sortBy, maxResults, ADServerWrapper.PropertiesNeededForServer);
			}
			if (typeof(TADWrapperObject).Equals(typeof(IADDatabaseAvailabilityGroup)))
			{
				return this.FindInternal<TADWrapperObject, DatabaseAvailabilityGroup>(rootId, scope, filter, sortBy, maxResults, null);
			}
			if (typeof(TADWrapperObject).Equals(typeof(IADClientAccessArray)))
			{
				return this.FindInternal<TADWrapperObject, ClientAccessArray>(rootId, scope, QueryFilter.AndTogether(new QueryFilter[]
				{
					filter,
					ClientAccessArray.PriorTo15ExchangeObjectVersionFilter
				}), sortBy, maxResults, null);
			}
			ExAssert.RetailAssert(false, "Unhandled type '{0}' !", new object[]
			{
				typeof(TADWrapperObject).FullName
			});
			return null;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00004C84 File Offset: 0x00002E84
		private TADWrapperObject[] FindInternal<TADWrapperObject, TADObject>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties = null) where TADWrapperObject : class, IADObjectCommon where TADObject : ADObject, new()
		{
			TADObject[] array = this.m_session.Find<TADObject>(rootId, scope, filter, sortBy, maxResults, properties);
			if (array == null)
			{
				return null;
			}
			return (from o in array
			select (TADWrapperObject)((object)ADObjectWrapperFactory.CreateWrapper(o))).ToArray<TADWrapperObject>();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public IADServer ReadMiniServer(ADObjectId entryId)
		{
			MiniServer server = this.m_session.ReadMiniServer(entryId, ADServerWrapper.PropertiesNeededForServer);
			return ADObjectWrapperFactory.CreateWrapper(server);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00004CEC File Offset: 0x00002EEC
		public IADServer FindMiniServerByName(string serverName)
		{
			MiniServer server = this.m_session.FindMiniServerByName(serverName, ADServerWrapper.PropertiesNeededForServer);
			return ADObjectWrapperFactory.CreateWrapper(server);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004D14 File Offset: 0x00002F14
		public bool TryFindByExchangeLegacyDN(string legacyExchangeDN, out IADMiniClientAccessServerOrArray adMiniClientAccessServerOrArray)
		{
			MiniClientAccessServerOrArray caServerOrArray = null;
			bool result = this.m_session.TryFindByExchangeLegacyDN(legacyExchangeDN, ADMiniClientAccessServerOrArrayWrapper.PropertiesNeededForCas, out caServerOrArray);
			adMiniClientAccessServerOrArray = ADObjectWrapperFactory.CreateWrapper(caServerOrArray);
			return result;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004D40 File Offset: 0x00002F40
		public IADMiniClientAccessServerOrArray ReadMiniClientAccessServerOrArray(ADObjectId entryId)
		{
			MiniClientAccessServerOrArray caServerOrArray = this.m_session.ReadMiniClientAccessServerOrArray(entryId, ADMiniClientAccessServerOrArrayWrapper.PropertiesNeededForCas);
			return ADObjectWrapperFactory.CreateWrapper(caServerOrArray);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004D68 File Offset: 0x00002F68
		public IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn)
		{
			MiniClientAccessServerOrArray caServerOrArray = this.m_session.FindMiniClientAccessServerOrArrayByFqdn(serverFqdn, ADMiniClientAccessServerOrArrayWrapper.PropertiesNeededForCas);
			return ADObjectWrapperFactory.CreateWrapper(caServerOrArray);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004D90 File Offset: 0x00002F90
		public IADSite GetLocalSite()
		{
			ADSite localSite = this.m_session.GetLocalSite();
			return ADObjectWrapperFactory.CreateWrapper(localSite);
		}

		// Token: 0x0400008D RID: 141
		private ITopologyConfigurationSession m_session;
	}
}
