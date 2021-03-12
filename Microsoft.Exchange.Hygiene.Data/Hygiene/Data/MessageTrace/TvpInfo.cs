using System;
using System.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200017D RID: 381
	internal sealed class TvpInfo
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0002FB7A File Offset: 0x0002DD7A
		public TvpInfo(HygienePropertyDefinition tableName, DataTable tvp, HygienePropertyDefinition[] columns)
		{
			this.tableName = tableName;
			this.tvp = tvp;
			this.columns = columns;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002FB97 File Offset: 0x0002DD97
		public HygienePropertyDefinition TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0002FB9F File Offset: 0x0002DD9F
		public DataTable Tvp
		{
			get
			{
				return this.tvp;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0002FBA7 File Offset: 0x0002DDA7
		public HygienePropertyDefinition[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x04000711 RID: 1809
		private readonly HygienePropertyDefinition tableName;

		// Token: 0x04000712 RID: 1810
		private readonly DataTable tvp;

		// Token: 0x04000713 RID: 1811
		private readonly HygienePropertyDefinition[] columns;
	}
}
