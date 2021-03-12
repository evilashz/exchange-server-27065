using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000047 RID: 71
	public class QueryableProperty
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000DF5B File Offset: 0x0000C15B
		private QueryableProperty(string tag, string name, string type, object value)
		{
			this.tag = tag;
			this.name = name;
			this.type = type;
			this.value = value;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000DF80 File Offset: 0x0000C180
		public string PropertyTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000DF88 File Offset: 0x0000C188
		public string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000DF90 File Offset: 0x0000C190
		public string PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000DF98 File Offset: 0x0000C198
		public object PropertyValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public static QueryableProperty Create(string tag, string name, string type, object value)
		{
			return new QueryableProperty(tag, name, type, value);
		}

		// Token: 0x04000138 RID: 312
		private readonly string tag;

		// Token: 0x04000139 RID: 313
		private readonly string name;

		// Token: 0x0400013A RID: 314
		private readonly string type;

		// Token: 0x0400013B RID: 315
		private readonly object value;
	}
}
