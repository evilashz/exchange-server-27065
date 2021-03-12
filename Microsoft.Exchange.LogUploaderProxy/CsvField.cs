using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200000F RID: 15
	public class CsvField
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002804 File Offset: 0x00000A04
		public CsvField(string name, Type type, bool isIndexed, Version buildAdded, NormalizeColumnDataMethod normalizeMethod)
		{
			NormalizeColumnDataMethod normalizeMethod2 = (string c) => normalizeMethod(c);
			this.csvFieldImpl = new CsvField(name, type, isIndexed, buildAdded, normalizeMethod2);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000284A File Offset: 0x00000A4A
		public CsvField(string name, Type type, Version buildAdded)
		{
			this.csvFieldImpl = new CsvField(name, type, buildAdded);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002860 File Offset: 0x00000A60
		public CsvField(string name, Type type, bool isMandatory, Version buildAdded)
		{
			this.csvFieldImpl = new CsvField(name, type, isMandatory, buildAdded);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002890 File Offset: 0x00000A90
		public CsvField(string name, Type type, bool isIndexed, NormalizeColumnDataMethod normalizeMethod)
		{
			NormalizeColumnDataMethod normalizeMethod2 = (string c) => normalizeMethod(c);
			this.csvFieldImpl = new CsvField(name, type, isIndexed, normalizeMethod2);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000028D4 File Offset: 0x00000AD4
		public CsvField(string name, Type type)
		{
			this.csvFieldImpl = new CsvField(name, type);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000028E9 File Offset: 0x00000AE9
		public string Name
		{
			get
			{
				return this.csvFieldImpl.Name;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000028F6 File Offset: 0x00000AF6
		public Type Type
		{
			get
			{
				return this.csvFieldImpl.Type;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002903 File Offset: 0x00000B03
		public bool IsIndexed
		{
			get
			{
				return this.csvFieldImpl.IsIndexed;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002910 File Offset: 0x00000B10
		public bool IsMandatory
		{
			get
			{
				return this.csvFieldImpl.IsMandatory;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000291D File Offset: 0x00000B1D
		public Version BuildAdded
		{
			get
			{
				return this.csvFieldImpl.BuildAdded;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000293D File Offset: 0x00000B3D
		public NormalizeColumnDataMethod NormalizeMethod
		{
			get
			{
				return (string c) => this.csvFieldImpl.NormalizeMethod(c);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000294B File Offset: 0x00000B4B
		internal CsvField CsvFieldImpl
		{
			get
			{
				return this.csvFieldImpl;
			}
		}

		// Token: 0x0400002B RID: 43
		private CsvField csvFieldImpl;
	}
}
