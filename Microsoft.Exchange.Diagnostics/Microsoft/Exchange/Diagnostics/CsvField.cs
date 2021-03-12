using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001AF RID: 431
	public class CsvField
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002C0AB File Offset: 0x0002A2AB
		public CsvField(string name, Type type, bool isIndexed, Version buildAdded, NormalizeColumnDataMethod normalizeMethod) : this(name, type, isIndexed, normalizeMethod)
		{
			this.buildAdded = buildAdded;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
		public CsvField(string name, Type type, Version buildAdded) : this(name, type)
		{
			this.buildAdded = buildAdded;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002C0D1 File Offset: 0x0002A2D1
		public CsvField(string name, Type type, bool isMandatory, Version buildAdded) : this(name, type)
		{
			this.isMandatory = isMandatory;
			this.buildAdded = buildAdded;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002C0EA File Offset: 0x0002A2EA
		public CsvField(string name, Type type, bool isIndexed, NormalizeColumnDataMethod normalizeMethod) : this(name, type)
		{
			this.isIndexed = isIndexed;
			this.normalizeMethod = normalizeMethod;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002C103 File Offset: 0x0002A303
		public CsvField(string name, Type type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002C119 File Offset: 0x0002A319
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0002C121 File Offset: 0x0002A321
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002C129 File Offset: 0x0002A329
		public bool IsIndexed
		{
			get
			{
				return this.isIndexed;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002C131 File Offset: 0x0002A331
		public bool IsMandatory
		{
			get
			{
				return this.isMandatory;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002C139 File Offset: 0x0002A339
		public Version BuildAdded
		{
			get
			{
				return this.buildAdded;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002C141 File Offset: 0x0002A341
		public NormalizeColumnDataMethod NormalizeMethod
		{
			get
			{
				return this.normalizeMethod;
			}
		}

		// Token: 0x040008B9 RID: 2233
		private readonly string name;

		// Token: 0x040008BA RID: 2234
		private readonly Type type;

		// Token: 0x040008BB RID: 2235
		private readonly Version buildAdded;

		// Token: 0x040008BC RID: 2236
		private readonly bool isIndexed;

		// Token: 0x040008BD RID: 2237
		private readonly bool isMandatory;

		// Token: 0x040008BE RID: 2238
		private readonly NormalizeColumnDataMethod normalizeMethod;
	}
}
