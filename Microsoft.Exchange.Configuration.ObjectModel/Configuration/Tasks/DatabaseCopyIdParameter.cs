using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public class DatabaseCopyIdParameter : ADIdParameter
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0001FE85 File Offset: 0x0001E085
		public DatabaseCopyIdParameter(DatabaseCopy databaseCopy) : base(databaseCopy.Id)
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001FE93 File Offset: 0x0001E093
		public DatabaseCopyIdParameter(MailboxServerIdParameter mailboxServerId) : base("*\\" + mailboxServerId.ToString())
		{
			this.Initialize(base.RawIdentity);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001FEB7 File Offset: 0x0001E0B7
		public DatabaseCopyIdParameter(MailboxServer server) : base("*\\" + server.Identity)
		{
			this.Initialize(base.RawIdentity);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001FEDB File Offset: 0x0001E0DB
		public DatabaseCopyIdParameter(Database database) : base(database.Name + "\\*")
		{
			this.Initialize(base.RawIdentity);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001FEFF File Offset: 0x0001E0FF
		public DatabaseCopyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
			this.Initialize(base.RawIdentity);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001FF14 File Offset: 0x0001E114
		public DatabaseCopyIdParameter()
		{
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001FF1C File Offset: 0x0001E11C
		public DatabaseCopyIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001FF25 File Offset: 0x0001E125
		protected DatabaseCopyIdParameter(string identity) : base(identity)
		{
			this.Initialize(base.RawIdentity);
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001FF3A File Offset: 0x0001E13A
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0001FF42 File Offset: 0x0001E142
		internal bool AllowInvalid { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001FF4B File Offset: 0x0001E14B
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001FF53 File Offset: 0x0001E153
		internal bool AllowLegacy
		{
			get
			{
				return this.allowLegacy;
			}
			set
			{
				this.allowLegacy = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0001FF5C File Offset: 0x0001E15C
		internal string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001FF64 File Offset: 0x0001E164
		internal string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001FF6C File Offset: 0x0001E16C
		protected ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.Mailbox;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001FF6F File Offset: 0x0001E16F
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return this.additionalQueryFilter;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001FF77 File Offset: 0x0001E177
		public static DatabaseCopyIdParameter Parse(string identity)
		{
			return new DatabaseCopyIdParameter(identity);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001FF80 File Offset: 0x0001E180
		public override string ToString()
		{
			string result;
			if (base.InternalADObjectId == null)
			{
				result = this.DatabaseName + '\\' + this.ServerName;
			}
			else
			{
				result = base.ToString();
			}
			return result;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001FFBA File Offset: 0x0001E1BA
		internal static DatabaseCopyIdParameter TestHookCreateDatabaseCopyIdParameter(string identity)
		{
			return new DatabaseCopyIdParameter(identity);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001FFC2 File Offset: 0x0001E1C2
		internal void SetAdditionalQueryFilter(QueryFilter newFilter)
		{
			this.additionalQueryFilter = newFilter;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00020298 File Offset: 0x0001E498
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			var func = null;
			var func2 = null;
			var func3 = null;
			Func<DatabaseCopy, int> func4 = null;
			if (!typeof(DatabaseCopy).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			new List<T>();
			notFoundReason = null;
			if (base.InternalADObjectId != null)
			{
				return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			IEnumerable<DatabaseCopy> enumerable;
			if (this.DatabaseName.Equals("*"))
			{
				enumerable = from dbCopy in base.PerformPrimarySearch<DatabaseCopy>(base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, this.ServerName), rootId, session, true, optionalData)
				where dbCopy.IsValidDatabaseCopy(this.AllowInvalid)
				select dbCopy;
			}
			else
			{
				IEnumerable<MiniDatabase> source = this.PerformSearch<MiniDatabase>(base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, this.DatabaseName), null, session, true);
				if (func == null)
				{
					func = ((MiniDatabase db) => new
					{
						db = db,
						tmpRootId = (db.Id ?? new ADObjectId(db.Name))
					});
				}
				var source2 = source.Select(func);
				var collectionSelector = <>h__TransparentIdentifier0 => this.PerformPrimarySearch<DatabaseCopy>(this.CreateWildcardOrEqualFilter(ADObjectSchema.Name, this.ServerName), <>h__TransparentIdentifier0.tmpRootId, session, true, optionalData);
				if (func2 == null)
				{
					func2 = ((<>h__TransparentIdentifier0, DatabaseCopy dbCopy) => new
					{
						<>h__TransparentIdentifier0,
						dbCopy
					});
				}
				var source3 = from <>h__TransparentIdentifier1 in source2.SelectMany(collectionSelector, func2)
				where <>h__TransparentIdentifier1.dbCopy.IsValidDatabaseCopy(this.AllowInvalid)
				select <>h__TransparentIdentifier1;
				if (func3 == null)
				{
					func3 = (<>h__TransparentIdentifier1 => <>h__TransparentIdentifier1.dbCopy);
				}
				enumerable = source3.Select(func3);
			}
			if (!this.DatabaseName.Contains("*"))
			{
				IEnumerable<DatabaseCopy> source4 = enumerable;
				if (func4 == null)
				{
					func4 = ((DatabaseCopy dbCopy) => dbCopy.ActivationPreferenceInternal);
				}
				enumerable = source4.OrderBy(func4);
			}
			return (IEnumerable<T>)enumerable;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00020464 File Offset: 0x0001E664
		protected void Initialize(string identity)
		{
			if (base.InternalADObjectId != null)
			{
				if (!(base.InternalADObjectId.Rdn != null))
				{
					Guid objectGuid = base.InternalADObjectId.ObjectGuid;
				}
				return;
			}
			string[] array = identity.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 2)
			{
				this.databaseName = array[0];
				this.serverName = DatabaseCopyIdParameter.GetServerNameFromServerShortNameOrFqdn(array[1]);
			}
			else if (array.Length == 1)
			{
				this.databaseName = array[0];
				this.serverName = "*";
			}
			if (array.Length > 2)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
			if (string.IsNullOrEmpty(this.DatabaseName))
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00020528 File Offset: 0x0001E728
		private static string GetServerNameFromServerShortNameOrFqdn(string serverName)
		{
			string text = serverName;
			if (text.Contains("."))
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 412, "GetServerNameFromServerShortNameOrFqdn", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\DatabaseCopyIdParameter.cs");
				topologyConfigurationSession.UseConfigNC = false;
				topologyConfigurationSession.UseGlobalCatalog = true;
				ADComputer adcomputer = topologyConfigurationSession.FindComputerByHostName(text);
				if (adcomputer == null)
				{
					throw new ArgumentException(Strings.ErrorInvalidServerName(text), "Identity");
				}
				text = ((ADObjectId)adcomputer.Identity).Name;
			}
			return text;
		}

		// Token: 0x04000269 RID: 617
		private string databaseName;

		// Token: 0x0400026A RID: 618
		private string serverName;

		// Token: 0x0400026B RID: 619
		private bool allowLegacy;

		// Token: 0x0400026C RID: 620
		private QueryFilter additionalQueryFilter;
	}
}
