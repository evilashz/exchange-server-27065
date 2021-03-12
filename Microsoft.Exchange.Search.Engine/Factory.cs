using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000005 RID: 5
	internal class Factory
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002B2E File Offset: 0x00000D2E
		protected Factory()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B36 File Offset: 0x00000D36
		internal static Hookable<Factory> Instance
		{
			get
			{
				return Factory.instance;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002B3D File Offset: 0x00000D3D
		internal static Factory Current
		{
			get
			{
				return Factory.instance.Value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B49 File Offset: 0x00000D49
		internal virtual IExecutable CreateFeedingController(ISearchServiceConfig config, MdbInfo mdbInfo, IIndexStatusStore indexStatusStore, IIndexManager indexManager, IDocumentTracker tracker)
		{
			return new SearchFeedingController(config, mdbInfo, indexStatusStore, indexManager, tracker);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B57 File Offset: 0x00000D57
		internal virtual ISearchServiceConfig CreateSearchServiceConfig()
		{
			return new FlightingSearchConfig();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B5E File Offset: 0x00000D5E
		internal virtual ISearchServiceConfig CreateSearchServiceConfig(Guid mdbGuid)
		{
			return new FlightingSearchConfig(mdbGuid);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B66 File Offset: 0x00000D66
		internal virtual IDocumentTracker CreateDocumentTracker()
		{
			return new DocumentTracker();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B6D File Offset: 0x00000D6D
		internal virtual IIndexStatusStore CreateIndexStatusStore()
		{
			return IndexStatusStore.Instance;
		}

		// Token: 0x04000016 RID: 22
		private static readonly Hookable<Factory> instance = Hookable<Factory>.Create(true, new Factory());
	}
}
