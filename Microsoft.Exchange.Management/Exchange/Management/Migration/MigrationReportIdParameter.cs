using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E3 RID: 1251
	[Serializable]
	public class MigrationReportIdParameter : IIdentityParameter
	{
		// Token: 0x06002BA5 RID: 11173 RVA: 0x000AE596 File Offset: 0x000AC796
		public MigrationReportIdParameter() : this(StoreObjectId.DummyId.ToString())
		{
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000AE5A8 File Offset: 0x000AC7A8
		public MigrationReportIdParameter(INamedIdentity namedIdentity)
		{
			if (namedIdentity == null)
			{
				throw new ArgumentNullException("namedIdentity");
			}
			this.MigrationReportId = new MigrationReportId(namedIdentity.Identity);
			this.RawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000AE5DB File Offset: 0x000AC7DB
		public MigrationReportIdParameter(MigrationReportId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationReportId = identity;
			this.RawIdentity = identity.ToString();
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000AE604 File Offset: 0x000AC804
		public MigrationReportIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationReportId = new MigrationReportId(identity);
			this.RawIdentity = identity;
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000AE62D File Offset: 0x000AC82D
		// (set) Token: 0x06002BAA RID: 11178 RVA: 0x000AE635 File Offset: 0x000AC835
		public MigrationReportId MigrationReportId
		{
			get
			{
				return this.migrationReportId;
			}
			private set
			{
				this.migrationReportId = value;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000AE63E File Offset: 0x000AC83E
		// (set) Token: 0x06002BAC RID: 11180 RVA: 0x000AE646 File Offset: 0x000AC846
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
			private set
			{
				this.rawIdentity = value;
			}
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000AE7EC File Offset: 0x000AC9EC
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			IConfigurable[] array = session.Find<T>(null, this.MigrationReportId, false, null);
			for (int i = 0; i < array.Length; i++)
			{
				T instance = (T)((object)array[i]);
				yield return instance;
			}
			yield break;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000AE810 File Offset: 0x000ACA10
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.MigrationReportNotFound);
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000AE82C File Offset: 0x000ACA2C
		public void Initialize(ObjectId objectId)
		{
			MigrationReportId migrationReportId = objectId as MigrationReportId;
			if (migrationReportId == null)
			{
				throw new ArgumentException("objectId");
			}
			this.MigrationReportId = migrationReportId;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000AE855 File Offset: 0x000ACA55
		public override string ToString()
		{
			return this.RawIdentity;
		}

		// Token: 0x04002024 RID: 8228
		private MigrationReportId migrationReportId;

		// Token: 0x04002025 RID: 8229
		private string rawIdentity;
	}
}
