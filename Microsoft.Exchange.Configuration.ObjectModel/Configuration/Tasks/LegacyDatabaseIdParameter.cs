using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	public class LegacyDatabaseIdParameter : ServerBasedIdParameter
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x00021DF4 File Offset: 0x0001FFF4
		public LegacyDatabaseIdParameter(Database database) : base(database.Id)
		{
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00021E02 File Offset: 0x00020002
		public LegacyDatabaseIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00021E0B File Offset: 0x0002000B
		public LegacyDatabaseIdParameter()
		{
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00021E13 File Offset: 0x00020013
		public LegacyDatabaseIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00021E1C File Offset: 0x0002001C
		protected LegacyDatabaseIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00021E25 File Offset: 0x00020025
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.Mailbox;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00021E28 File Offset: 0x00020028
		public static LegacyDatabaseIdParameter Parse(string identity)
		{
			return new LegacyDatabaseIdParameter(identity);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00021E30 File Offset: 0x00020030
		public override string ToString()
		{
			if (base.InternalADObjectId == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (!string.IsNullOrEmpty(base.ServerName))
				{
					stringBuilder.Append(base.ServerName);
					stringBuilder.Append('\\');
				}
				if (!string.IsNullOrEmpty(this.storageGroupName))
				{
					stringBuilder.Append(this.storageGroupName);
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(base.CommonName);
				return stringBuilder.ToString();
			}
			return base.ToString();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00021EAB File Offset: 0x000200AB
		internal override void Initialize(ObjectId objectId)
		{
			base.Initialize(objectId);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00021EB4 File Offset: 0x000200B4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			IEnumerable<T> enumerable = null;
			EnumerableWrapper<T> enumerableWrapper = null;
			notFoundReason = null;
			if (string.IsNullOrEmpty(this.storageGroupName))
			{
				enumerableWrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			}
			if (!string.IsNullOrEmpty(base.CommonName) && (enumerableWrapper == null || !enumerableWrapper.HasElements()))
			{
				string serverName = this.storageGroupName;
				ServerIdParameter serverIdParameter = base.ServerId;
				if (string.IsNullOrEmpty(this.storageGroupName))
				{
					serverName = base.ServerName;
					serverIdParameter = new ServerIdParameter();
				}
				ADObjectId[] matchingIdentities = serverIdParameter.GetMatchingIdentities((IConfigDataProvider)session);
				for (int i = 0; i < matchingIdentities.Length; i++)
				{
					if (ServerIdParameter.HasRole(matchingIdentities[i], this.RoleRestriction, (IConfigDataProvider)session) || (base.AllowLegacy && !ServerIdParameter.HasRole(matchingIdentities[i], ServerRole.All, (IConfigDataProvider)session)))
					{
						if (string.IsNullOrEmpty(this.storageGroupName))
						{
							rootId = matchingIdentities[i].GetChildId("InformationStore").GetChildId(serverName);
							enumerable = base.PerformPrimarySearch<T>(base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, base.CommonName), rootId, session, true, optionalData);
						}
						else
						{
							List<T> list = new List<T>();
							IEnumerable<StorageGroup> enumerable2 = base.PerformSearch<StorageGroup>(base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, this.storageGroupName), matchingIdentities[i], session, true);
							foreach (StorageGroup storageGroup in enumerable2)
							{
								enumerable = base.PerformPrimarySearch<T>(base.CreateWildcardOrEqualFilter(ADObjectSchema.Name, base.CommonName), storageGroup.Id, session, true, optionalData);
								list.AddRange(enumerable);
							}
							enumerable = list;
						}
					}
				}
			}
			else
			{
				enumerable = enumerableWrapper;
			}
			if (enumerable == null)
			{
				enumerable = new List<T>();
			}
			return enumerable;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002207C File Offset: 0x0002027C
		protected override void Initialize(string identity)
		{
			if (base.InternalADObjectId != null && base.InternalADObjectId.Rdn != null)
			{
				return;
			}
			string[] array = identity.Split(new char[]
			{
				'\\'
			});
			if (array.Length > 3)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
			if (array.Length == 3)
			{
				string identity2 = array[0] + '\\' + array[1];
				base.Initialize(identity2);
				this.storageGroupName = base.CommonName;
				base.CommonName = array[2];
			}
			else
			{
				base.Initialize(identity);
			}
			if (!string.IsNullOrEmpty(base.ServerName) && !string.IsNullOrEmpty(this.storageGroupName) && base.ServerId == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
		}

		// Token: 0x0400027E RID: 638
		private string storageGroupName;
	}
}
