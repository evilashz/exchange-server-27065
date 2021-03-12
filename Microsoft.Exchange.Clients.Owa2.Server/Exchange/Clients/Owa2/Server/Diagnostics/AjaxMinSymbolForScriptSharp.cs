using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000427 RID: 1063
	public struct AjaxMinSymbolForScriptSharp : IJavaScriptSymbol
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00083421 File Offset: 0x00081621
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x00083429 File Offset: 0x00081629
		public int ScriptStartLine { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00083432 File Offset: 0x00081632
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x0008343A File Offset: 0x0008163A
		public int ScriptStartColumn { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00083443 File Offset: 0x00081643
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0008344B File Offset: 0x0008164B
		public int ScriptEndLine { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x00083454 File Offset: 0x00081654
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x0008345C File Offset: 0x0008165C
		public int ScriptEndColumn { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x00083465 File Offset: 0x00081665
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x0008346D File Offset: 0x0008166D
		public int SourceStartPosition { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x00083476 File Offset: 0x00081676
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x0008347E File Offset: 0x0008167E
		public int SourceEndPosition { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x00083487 File Offset: 0x00081687
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x0008348F File Offset: 0x0008168F
		public uint SourceFileId { get; set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x00083498 File Offset: 0x00081698
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x000834A0 File Offset: 0x000816A0
		public int ParentSymbolIndex { get; set; }
	}
}
