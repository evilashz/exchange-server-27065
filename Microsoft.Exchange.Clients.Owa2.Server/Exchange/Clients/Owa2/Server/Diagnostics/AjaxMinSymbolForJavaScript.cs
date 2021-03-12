using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000426 RID: 1062
	public struct AjaxMinSymbolForJavaScript : IJavaScriptSymbol
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x00083366 File Offset: 0x00081566
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x0008336E File Offset: 0x0008156E
		public int ScriptStartLine { get; set; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x00083377 File Offset: 0x00081577
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x0008337F File Offset: 0x0008157F
		public int ScriptStartColumn { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x00083388 File Offset: 0x00081588
		// (set) Token: 0x06002449 RID: 9289 RVA: 0x00083390 File Offset: 0x00081590
		public int ScriptEndLine { get; set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x00083399 File Offset: 0x00081599
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x000833A1 File Offset: 0x000815A1
		public int ScriptEndColumn { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000833AA File Offset: 0x000815AA
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x000833B2 File Offset: 0x000815B2
		public int SourceStartLine { get; set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000833BB File Offset: 0x000815BB
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x000833C3 File Offset: 0x000815C3
		public int SourceStartColumn { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000833CC File Offset: 0x000815CC
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x000833D4 File Offset: 0x000815D4
		public int SourceEndLine { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x000833DD File Offset: 0x000815DD
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x000833E5 File Offset: 0x000815E5
		public int SourceEndColumn { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x000833EE File Offset: 0x000815EE
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x000833F6 File Offset: 0x000815F6
		public uint SourceFileId { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x000833FF File Offset: 0x000815FF
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x00083407 File Offset: 0x00081607
		public int ParentSymbolIndex { get; set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x00083410 File Offset: 0x00081610
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x00083418 File Offset: 0x00081618
		public int FunctionNameIndex { get; set; }
	}
}
