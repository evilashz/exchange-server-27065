using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000015 RID: 21
	internal class FastQueryView : DisposeTrackableBase
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00005484 File Offset: 0x00003684
		public FastQueryView(ResponseFactory factory, Folder folder, SortBy[] sortBys, PropertyDefinition[] propDefs)
		{
			this.factory = factory;
			this.view = folder.ItemQuery(ItemQueryType.RetrieveFromIndex, null, sortBys, propDefs);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000054A4 File Offset: 0x000036A4
		public QueryResult TableView
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000054AC File Offset: 0x000036AC
		protected override void InternalDispose(bool isDisposing)
		{
			if (this.view != null)
			{
				try
				{
					this.view.Dispose();
				}
				catch (LocalizedException ex)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.factory.Session.SessionId, "Exception caught while disposing fastQueryView. {0}", ex.ToString());
				}
				this.view = null;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005510 File Offset: 0x00003710
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastQueryView>(this);
		}

		// Token: 0x04000094 RID: 148
		private QueryResult view;

		// Token: 0x04000095 RID: 149
		private ResponseFactory factory;
	}
}
