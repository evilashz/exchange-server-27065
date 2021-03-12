using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E0 RID: 1248
	[Serializable]
	public class MigrationBatchIdParameter : IIdentityParameter
	{
		// Token: 0x06002B75 RID: 11125 RVA: 0x000ADC03 File Offset: 0x000ABE03
		public MigrationBatchIdParameter() : this(MigrationBatchId.Any)
		{
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000ADC10 File Offset: 0x000ABE10
		public MigrationBatchIdParameter(INamedIdentity namedIdentity)
		{
			if (namedIdentity == null)
			{
				throw new ArgumentNullException("namedIdentity");
			}
			this.MigrationBatchId = MigrationBatchIdParameter.MigrationBatchIdFromString(namedIdentity.Identity);
			this.RawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000ADC43 File Offset: 0x000ABE43
		public MigrationBatchIdParameter(MigrationBatchId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationBatchId = identity;
			this.RawIdentity = this.MigrationBatchId.ToString();
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000ADC71 File Offset: 0x000ABE71
		public MigrationBatchIdParameter(MigrationBatch batch) : this(batch.Identity)
		{
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000ADC7F File Offset: 0x000ABE7F
		public MigrationBatchIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationBatchId = MigrationBatchIdParameter.MigrationBatchIdFromString(identity);
			this.RawIdentity = identity;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000ADCA8 File Offset: 0x000ABEA8
		public MigrationBatchIdParameter(Guid jobId) : this(new MigrationBatchId(jobId))
		{
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000ADCB6 File Offset: 0x000ABEB6
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x000ADCBE File Offset: 0x000ABEBE
		public MigrationBatchId MigrationBatchId
		{
			get
			{
				return this.migrationBatchId;
			}
			private set
			{
				this.migrationBatchId = value;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000ADCC7 File Offset: 0x000ABEC7
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000ADCCF File Offset: 0x000ABECF
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

		// Token: 0x06002B7F RID: 11135 RVA: 0x000ADCD8 File Offset: 0x000ABED8
		public static MigrationBatchIdParameter Parse(string identity)
		{
			return new MigrationBatchIdParameter(identity);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000ADE7C File Offset: 0x000AC07C
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			IConfigurable[] array = session.Find<T>(null, this.MigrationBatchId, false, null);
			for (int i = 0; i < array.Length; i++)
			{
				T instance = (T)((object)array[i]);
				yield return instance;
			}
			yield break;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000ADEA0 File Offset: 0x000AC0A0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.MigrationJobNotFound(this.RawIdentity));
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000ADEC4 File Offset: 0x000AC0C4
		public void Initialize(ObjectId objectId)
		{
			MigrationBatchId migrationBatchId = objectId as MigrationBatchId;
			if (migrationBatchId == null)
			{
				throw new ArgumentException("objectId");
			}
			this.MigrationBatchId = migrationBatchId;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000ADEED File Offset: 0x000AC0ED
		public override string ToString()
		{
			return this.RawIdentity;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000ADEF8 File Offset: 0x000AC0F8
		private static MigrationBatchId MigrationBatchIdFromString(string identity)
		{
			Guid jobId;
			if (!GuidHelper.TryParseGuid(identity, out jobId))
			{
				return new MigrationBatchId(identity);
			}
			return new MigrationBatchId(jobId);
		}

		// Token: 0x04002019 RID: 8217
		public const string MigrationMailboxName = "Migration.8f3e7716-2011-43e4-96b1-aba62d229136";

		// Token: 0x0400201A RID: 8218
		private MigrationBatchId migrationBatchId;

		// Token: 0x0400201B RID: 8219
		private string rawIdentity;
	}
}
