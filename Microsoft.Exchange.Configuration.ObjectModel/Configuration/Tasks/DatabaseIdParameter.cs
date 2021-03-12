using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public class DatabaseIdParameter : ADIdParameter
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x000205A5 File Offset: 0x0001E7A5
		public DatabaseIdParameter(Database database) : base(database.Id)
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000205B3 File Offset: 0x0001E7B3
		public DatabaseIdParameter(DatabaseCopyIdParameter databaseCopy) : this(databaseCopy.DatabaseName)
		{
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000205C1 File Offset: 0x0001E7C1
		public DatabaseIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000205CA File Offset: 0x0001E7CA
		public DatabaseIdParameter()
		{
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000205D2 File Offset: 0x0001E7D2
		public DatabaseIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000205DB File Offset: 0x0001E7DB
		protected DatabaseIdParameter(string identity) : base(identity)
		{
			this.Initialize(identity);
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x000205EB File Offset: 0x0001E7EB
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x000205F3 File Offset: 0x0001E7F3
		internal bool AllowLegacy
		{
			get
			{
				return this.allowLegacy;
			}
			set
			{
				this.allowLegacy = value;
				if (this.legacyParameter != null)
				{
					this.legacyParameter.AllowLegacy = value;
				}
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00020610 File Offset: 0x0001E810
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00020618 File Offset: 0x0001E818
		internal bool AllowInvalid { get; set; }

		// Token: 0x06000968 RID: 2408 RVA: 0x00020621 File Offset: 0x0001E821
		public static DatabaseIdParameter Parse(string identity)
		{
			return new DatabaseIdParameter(identity);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00020629 File Offset: 0x0001E829
		public override string ToString()
		{
			if (base.InternalADObjectId == null && this.legacyParameter != null)
			{
				return this.legacyParameter.ToString();
			}
			return base.ToString();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00020668 File Offset: 0x0001E868
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (!typeof(Database).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			new List<T>();
			notFoundReason = null;
			if (rootId != null)
			{
				Server server = (Server)((IConfigDataProvider)session).Read<Server>(rootId);
				if (server != null)
				{
					if (optionalData != null && optionalData.AdditionalFilter != null)
					{
						throw new NotSupportedException("Supplying Additional Filters and a RootId is not currently supported by this IdParameter.");
					}
					return server.GetDatabases<T>(this.AllowInvalid);
				}
			}
			IEnumerable<T> objects = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			IEnumerable<T> enumerable = from tmpDb in objects
			where tmpDb.IsValid || this.AllowInvalid
			select tmpDb;
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(enumerable);
			if (!wrapper.HasElements() && this.legacyParameter != null)
			{
				wrapper = EnumerableWrapper<T>.GetWrapper(this.legacyParameter.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			}
			return wrapper;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00020750 File Offset: 0x0001E950
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
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
			if (array.Length > 2)
			{
				this.legacyParameter = LegacyDatabaseIdParameter.Parse(identity);
			}
		}

		// Token: 0x0400026E RID: 622
		private LegacyDatabaseIdParameter legacyParameter;

		// Token: 0x0400026F RID: 623
		private bool allowLegacy;
	}
}
