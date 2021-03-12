using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.GlobalConfig
{
	// Token: 0x02000136 RID: 310
	internal class GlobalSystemConfigSession
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x00025F3A File Offset: 0x0002413A
		internal GlobalSystemConfigSession()
		{
			this.webStoreDataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Spam);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00025F53 File Offset: 0x00024153
		internal IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.webStoreDataProvider.Find<T>(filter, rootId, deepSearch, sortBy).ToArray<IConfigurable>();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00025F6A File Offset: 0x0002416A
		internal void Save(IConfigurable configurable)
		{
			this.webStoreDataProvider.Save(configurable);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00025F78 File Offset: 0x00024178
		internal void Delete(IConfigurable configurable)
		{
			if (configurable is DataCenterSettings)
			{
				throw new ArgumentException(string.Format("Delete operation is not supported for the type {0}.", configurable.GetType().ToString()));
			}
			this.webStoreDataProvider.Delete(configurable);
		}

		// Token: 0x04000600 RID: 1536
		private IConfigDataProvider webStoreDataProvider;
	}
}
