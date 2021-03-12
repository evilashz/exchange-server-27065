using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E1 RID: 1249
	[Serializable]
	public class MigrationConfigIdParameter : IIdentityParameter
	{
		// Token: 0x06002B85 RID: 11141 RVA: 0x000ADF1C File Offset: 0x000AC11C
		public MigrationConfigIdParameter()
		{
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000ADF24 File Offset: 0x000AC124
		public MigrationConfigIdParameter(INamedIdentity namedIdentity)
		{
			if (namedIdentity == null)
			{
				throw new ArgumentNullException("namedIdentity");
			}
			this.OrganizationIdentifier = new OrganizationIdParameter(namedIdentity.Identity);
			this.RawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000ADF57 File Offset: 0x000AC157
		public MigrationConfigIdParameter(MigrationConfigId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.Id = identity;
			this.RawIdentity = identity.ToString();
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000ADF80 File Offset: 0x000AC180
		public MigrationConfigIdParameter(MigrationConfig config) : this(config.Identity)
		{
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000ADF8E File Offset: 0x000AC18E
		public MigrationConfigIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.OrganizationIdentifier = new OrganizationIdParameter(identity);
			this.RawIdentity = identity;
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000ADFB7 File Offset: 0x000AC1B7
		// (set) Token: 0x06002B8B RID: 11147 RVA: 0x000ADFBF File Offset: 0x000AC1BF
		public MigrationConfigId Id { get; internal set; }

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000ADFC8 File Offset: 0x000AC1C8
		// (set) Token: 0x06002B8D RID: 11149 RVA: 0x000ADFD0 File Offset: 0x000AC1D0
		public OrganizationIdParameter OrganizationIdentifier { get; private set; }

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000ADFD9 File Offset: 0x000AC1D9
		// (set) Token: 0x06002B8F RID: 11151 RVA: 0x000ADFE1 File Offset: 0x000AC1E1
		public string RawIdentity { get; private set; }

		// Token: 0x06002B90 RID: 11152 RVA: 0x000AE1A0 File Offset: 0x000AC3A0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			if (this.Id == null)
			{
				throw new ArgumentException("this.Id");
			}
			IConfigurable[] array = session.Find<T>(null, this.Id, false, null);
			for (int i = 0; i < array.Length; i++)
			{
				T instance = (T)((object)array[i]);
				yield return instance;
			}
			yield break;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000AE1C4 File Offset: 0x000AC3C4
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.MigrationNotFound(this.RawIdentity));
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000AE1E8 File Offset: 0x000AC3E8
		public void Initialize(ObjectId objectId)
		{
			MigrationConfigId migrationConfigId = objectId as MigrationConfigId;
			if (migrationConfigId == null)
			{
				throw new ArgumentException("objectId");
			}
			this.Id = migrationConfigId;
			this.RawIdentity = migrationConfigId.ToString();
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000AE21D File Offset: 0x000AC41D
		public override string ToString()
		{
			return this.RawIdentity;
		}
	}
}
