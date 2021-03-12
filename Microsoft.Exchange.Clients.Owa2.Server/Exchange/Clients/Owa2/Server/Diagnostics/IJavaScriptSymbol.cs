using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000425 RID: 1061
	public interface IJavaScriptSymbol
	{
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600243C RID: 9276
		int ScriptStartLine { get; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600243D RID: 9277
		int ScriptStartColumn { get; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600243E RID: 9278
		int ScriptEndLine { get; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600243F RID: 9279
		int ScriptEndColumn { get; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002440 RID: 9280
		// (set) Token: 0x06002441 RID: 9281
		uint SourceFileId { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002442 RID: 9282
		// (set) Token: 0x06002443 RID: 9283
		int ParentSymbolIndex { get; set; }
	}
}
