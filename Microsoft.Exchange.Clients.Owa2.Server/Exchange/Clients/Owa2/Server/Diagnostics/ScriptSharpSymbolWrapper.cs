using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200045E RID: 1118
	internal struct ScriptSharpSymbolWrapper : IJavaScriptSymbol
	{
		// Token: 0x0600258A RID: 9610 RVA: 0x0008828C File Offset: 0x0008648C
		public ScriptSharpSymbolWrapper(ScriptSharpSymbol symbol)
		{
			this.symbol = symbol;
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x00088295 File Offset: 0x00086495
		public int ScriptStartLine
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x00088298 File Offset: 0x00086498
		public int ScriptStartColumn
		{
			get
			{
				return this.symbol.ScriptStartPosition;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000882A5 File Offset: 0x000864A5
		public int ScriptEndLine
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000882A8 File Offset: 0x000864A8
		public int ScriptEndColumn
		{
			get
			{
				return this.symbol.ScriptEndPosition;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000882B5 File Offset: 0x000864B5
		// (set) Token: 0x06002590 RID: 9616 RVA: 0x000882C2 File Offset: 0x000864C2
		public uint SourceFileId
		{
			get
			{
				return this.symbol.SourceFileId;
			}
			set
			{
				this.symbol.SourceFileId = value;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000882D0 File Offset: 0x000864D0
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x000882DD File Offset: 0x000864DD
		public int ParentSymbolIndex
		{
			get
			{
				return this.symbol.ParentSymbol;
			}
			set
			{
				this.symbol.ParentSymbol = value;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000882EB File Offset: 0x000864EB
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x000882F8 File Offset: 0x000864F8
		public int FunctionNameIndex
		{
			get
			{
				return this.symbol.FunctionNameIndex;
			}
			set
			{
				this.symbol.FunctionNameIndex = value;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x00088306 File Offset: 0x00086506
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x0008830E File Offset: 0x0008650E
		public ScriptSharpSymbol InnerSymbol
		{
			get
			{
				return this.symbol;
			}
			set
			{
				this.symbol = value;
			}
		}

		// Token: 0x040015AD RID: 5549
		private ScriptSharpSymbol symbol;
	}
}
