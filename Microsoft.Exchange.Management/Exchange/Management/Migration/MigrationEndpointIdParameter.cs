using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004F2 RID: 1266
	[Serializable]
	public class MigrationEndpointIdParameter : IIdentityParameter
	{
		// Token: 0x06002D05 RID: 11525 RVA: 0x000B4861 File Offset: 0x000B2A61
		public MigrationEndpointIdParameter() : this(MigrationEndpointId.Any)
		{
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000B486E File Offset: 0x000B2A6E
		public MigrationEndpointIdParameter(string name) : this(new MigrationEndpointId(name, Guid.Empty))
		{
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000B4881 File Offset: 0x000B2A81
		public MigrationEndpointIdParameter(INamedIdentity namedId) : this(new MigrationEndpointId(namedId.Identity, Guid.Empty))
		{
			this.RawIdentity = namedId.DisplayName;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000B48A5 File Offset: 0x000B2AA5
		public MigrationEndpointIdParameter(MigrationEndpointId id)
		{
			this.MigrationEndpointId = id;
			this.RawIdentity = id.ToString();
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000B48C0 File Offset: 0x000B2AC0
		public MigrationEndpointIdParameter(Guid guid) : this(new MigrationEndpointId(string.Empty, guid))
		{
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000B48D3 File Offset: 0x000B2AD3
		public MigrationEndpointIdParameter(MigrationEndpoint connector) : this(connector.Identity)
		{
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000B48E1 File Offset: 0x000B2AE1
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x000B48E9 File Offset: 0x000B2AE9
		public string RawIdentity { get; private set; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000B48F2 File Offset: 0x000B2AF2
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x000B48FA File Offset: 0x000B2AFA
		public MigrationEndpointId MigrationEndpointId { get; private set; }

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B4AA0 File Offset: 0x000B2CA0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			IConfigurable[] array = session.Find<T>(null, this.MigrationEndpointId, false, null);
			for (int i = 0; i < array.Length; i++)
			{
				T entry = (T)((object)array[i]);
				yield return entry;
			}
			yield break;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B4AC4 File Offset: 0x000B2CC4
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.ErrorCouldNotFindMigrationEndpoint);
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000B4AE0 File Offset: 0x000B2CE0
		public void Initialize(ObjectId objectId)
		{
			MigrationEndpointId migrationEndpointId = objectId as MigrationEndpointId;
			if (migrationEndpointId == null)
			{
				throw new ArgumentException("Only MigrationEndpointId is supported.", "objectId");
			}
			this.MigrationEndpointId = migrationEndpointId;
			this.RawIdentity = this.MigrationEndpointId.ToString();
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000B4B1F File Offset: 0x000B2D1F
		public override string ToString()
		{
			return this.RawIdentity;
		}
	}
}
