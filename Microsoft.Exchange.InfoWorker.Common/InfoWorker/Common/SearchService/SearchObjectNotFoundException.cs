using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000247 RID: 583
	[Serializable]
	internal class SearchObjectNotFoundException : ObjectNotFoundException
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x0004CD77 File Offset: 0x0004AF77
		internal SearchObjectNotFoundException(LocalizedString message) : base(message)
		{
		}
	}
}
