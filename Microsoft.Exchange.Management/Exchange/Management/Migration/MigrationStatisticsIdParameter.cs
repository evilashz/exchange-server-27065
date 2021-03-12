using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E4 RID: 1252
	[Serializable]
	public class MigrationStatisticsIdParameter : IIdentityParameter
	{
		// Token: 0x06002BB1 RID: 11185 RVA: 0x000AE85D File Offset: 0x000ACA5D
		public MigrationStatisticsIdParameter()
		{
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000AE865 File Offset: 0x000ACA65
		public MigrationStatisticsIdParameter(INamedIdentity namedIdentity)
		{
			if (namedIdentity == null)
			{
				throw new ArgumentNullException("namedIdentity");
			}
			this.OrganizationIdentifier = new OrganizationIdParameter(namedIdentity.Identity);
			this.RawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000AE898 File Offset: 0x000ACA98
		public MigrationStatisticsIdParameter(MigrationStatisticsId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.Id = identity;
			this.RawIdentity = identity.ToString();
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000AE8C1 File Offset: 0x000ACAC1
		public MigrationStatisticsIdParameter(MigrationStatistics statistics) : this(statistics.Identity)
		{
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000AE8CF File Offset: 0x000ACACF
		public MigrationStatisticsIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.OrganizationIdentifier = new OrganizationIdParameter(identity);
			this.RawIdentity = identity;
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000AE8F8 File Offset: 0x000ACAF8
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x000AE900 File Offset: 0x000ACB00
		public MigrationStatisticsId Id { get; internal set; }

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000AE909 File Offset: 0x000ACB09
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x000AE911 File Offset: 0x000ACB11
		public OrganizationIdParameter OrganizationIdentifier { get; private set; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000AE91A File Offset: 0x000ACB1A
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x000AE922 File Offset: 0x000ACB22
		public string RawIdentity { get; private set; }

		// Token: 0x06002BBC RID: 11196 RVA: 0x000AEAE0 File Offset: 0x000ACCE0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			if (this.Id == null)
			{
				throw new ArgumentNullException("this.Id");
			}
			IConfigurable[] array = session.Find<T>(null, this.Id, false, null);
			for (int i = 0; i < array.Length; i++)
			{
				T instance = (T)((object)array[i]);
				yield return instance;
			}
			yield break;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000AEB04 File Offset: 0x000ACD04
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.MigrationNotFound(this.RawIdentity));
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000AEB28 File Offset: 0x000ACD28
		public void Initialize(ObjectId objectId)
		{
			MigrationStatisticsId migrationStatisticsId = objectId as MigrationStatisticsId;
			if (migrationStatisticsId == null)
			{
				throw new ArgumentException("objectId");
			}
			this.Id = migrationStatisticsId;
			this.RawIdentity = migrationStatisticsId.ToString();
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000AEB5D File Offset: 0x000ACD5D
		public override string ToString()
		{
			return this.RawIdentity;
		}
	}
}
