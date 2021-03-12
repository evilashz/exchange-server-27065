using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200009B RID: 155
	public sealed class ColumnInfo
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x00010680 File Offset: 0x0000E880
		internal ColumnInfo(string name, JET_COLUMNID columnid, JET_coltyp coltyp, JET_CP cp, int maxLength, byte[] defaultValue, ColumndefGrbit grbit)
		{
			this.Name = name;
			this.Columnid = columnid;
			this.Coltyp = coltyp;
			this.Cp = cp;
			this.MaxLength = maxLength;
			this.defaultValue = ((defaultValue == null) ? null : new ReadOnlyCollection<byte>(defaultValue));
			this.Grbit = grbit;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x000106D4 File Offset: 0x0000E8D4
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x000106DC File Offset: 0x0000E8DC
		public string Name { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000106E5 File Offset: 0x0000E8E5
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x000106ED File Offset: 0x0000E8ED
		public JET_COLUMNID Columnid { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x000106F6 File Offset: 0x0000E8F6
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x000106FE File Offset: 0x0000E8FE
		public JET_coltyp Coltyp { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00010707 File Offset: 0x0000E907
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001070F File Offset: 0x0000E90F
		public JET_CP Cp { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00010718 File Offset: 0x0000E918
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00010720 File Offset: 0x0000E920
		public int MaxLength { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00010729 File Offset: 0x0000E929
		public IList<byte> DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00010731 File Offset: 0x0000E931
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00010739 File Offset: 0x0000E939
		public ColumndefGrbit Grbit { get; private set; }

		// Token: 0x0600071F RID: 1823 RVA: 0x00010742 File Offset: 0x0000E942
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000319 RID: 793
		private readonly ReadOnlyCollection<byte> defaultValue;
	}
}
