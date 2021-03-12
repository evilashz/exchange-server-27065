using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000059 RID: 89
	internal class ADPagedReader<TResult> : ADGenericPagedReader<TResult> where TResult : IConfigurable, new()
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00019364 File Offset: 0x00017564
		internal ADPagedReader()
		{
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001936C File Offset: 0x0001756C
		internal ADPagedReader(IDirectorySession session, ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties, bool skipCheckVirtualIndex) : base(session, rootId, scope, filter, sortBy, pageSize, properties, skipCheckVirtualIndex)
		{
			this.pageResultRequestControl = new PageResultRequestControl(base.PageSize);
			base.DirectoryControls.Add(this.pageResultRequestControl);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000193B0 File Offset: 0x000175B0
		protected override SearchResultEntryCollection GetNextResultCollection()
		{
			this.pageResultRequestControl.Cookie = base.Cookie;
			this.pageResultRequestControl.PageSize = base.PageSize;
			if (base.PagesReturned > 0)
			{
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRatePaged, UpdateType.Add, 1U);
				ADProviderPerf.UpdateDCCounter(base.PreferredServerName, Counter.DCRatePaged, UpdateType.Add, 1U);
			}
			DirectoryControl directoryControl;
			SearchResultEntryCollection nextResultCollection = base.GetNextResultCollection(typeof(PageResultResponseControl), out directoryControl);
			base.Cookie = ((directoryControl == null) ? null : ((PageResultResponseControl)directoryControl).Cookie);
			if (base.Cookie == null || base.Cookie.Length == 0 || nextResultCollection == null)
			{
				base.RetrievedAllData = new bool?(true);
			}
			else
			{
				base.RetrievedAllData = new bool?(false);
			}
			return nextResultCollection;
		}

		// Token: 0x040001A4 RID: 420
		private PageResultRequestControl pageResultRequestControl;
	}
}
