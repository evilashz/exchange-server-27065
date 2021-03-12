using System;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A0 RID: 416
	public class AlignUnit : IComparable<AlignUnit>
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00041228 File Offset: 0x0003F428
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x00041230 File Offset: 0x0003F430
		public int ColumnSpan { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00041239 File Offset: 0x0003F439
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x00041241 File Offset: 0x0003F441
		public int RowSpan { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0004124A File Offset: 0x0003F44A
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00041252 File Offset: 0x0003F452
		public Control Control { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0004125B File Offset: 0x0003F45B
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x00041263 File Offset: 0x0003F463
		public Padding ResultMargin { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0004126C File Offset: 0x0003F46C
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x00041274 File Offset: 0x0003F474
		public Padding CompareMargin { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0004127D File Offset: 0x0003F47D
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00041285 File Offset: 0x0003F485
		public Padding InlinedMargin { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0004128E File Offset: 0x0003F48E
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00041296 File Offset: 0x0003F496
		public int Row { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0004129F File Offset: 0x0003F49F
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x000412A7 File Offset: 0x0003F4A7
		public int Column { get; set; }

		// Token: 0x06001089 RID: 4233 RVA: 0x000412B0 File Offset: 0x0003F4B0
		public AlignUnit(Control ctrl, int rowSpan, int columnSpan, int row, int column)
		{
			this.ColumnSpan = columnSpan;
			this.RowSpan = rowSpan;
			this.Row = row;
			this.Column = column;
			this.Control = ctrl;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000412E0 File Offset: 0x0003F4E0
		public int CompareTo(AlignUnit unit)
		{
			if (this.Row == unit.Row)
			{
				return this.Column.CompareTo(unit.Column);
			}
			return this.Row.CompareTo(unit.Row);
		}
	}
}
