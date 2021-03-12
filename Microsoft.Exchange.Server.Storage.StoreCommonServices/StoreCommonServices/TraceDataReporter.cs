using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000125 RID: 293
	internal abstract class TraceDataReporter<TContainer>
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x000398DA File Offset: 0x00037ADA
		protected TraceDataReporter(StoreDatabase database, IBinaryLogger logger, TContainer dataContainer)
		{
			this.logger = logger;
			this.database = database;
			this.dataContainer = dataContainer;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000398F7 File Offset: 0x00037AF7
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x000398FF File Offset: 0x00037AFF
		public IBinaryLogger Logger
		{
			get
			{
				return this.logger;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00039907 File Offset: 0x00037B07
		public TContainer DataContainer
		{
			get
			{
				return this.dataContainer;
			}
		}

		// Token: 0x0400064C RID: 1612
		private readonly StoreDatabase database;

		// Token: 0x0400064D RID: 1613
		private readonly IBinaryLogger logger;

		// Token: 0x0400064E RID: 1614
		private readonly TContainer dataContainer;
	}
}
