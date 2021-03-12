using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000311 RID: 785
	public interface IListView
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001DA0 RID: 7584
		int TotalCount { get; }

		// Token: 0x06001DA1 RID: 7585
		void Render(TextWriter writer);

		// Token: 0x06001DA2 RID: 7586
		void RenderForCompactWebPart(TextWriter writer);
	}
}
