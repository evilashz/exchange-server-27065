using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	internal class CommentFilter : QueryFilter
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00008130 File Offset: 0x00006330
		public CommentFilter(PropertyDefinition[] properties, object[] values, QueryFilter filter)
		{
			if ((properties != null && values == null) || (properties == null && values != null))
			{
				throw new ArgumentException("Either of properties or values is Null while the other is not.");
			}
			if (properties != null && values != null && properties.Length != values.Length)
			{
				throw new ArgumentException("The lengths of properties and values do not match.");
			}
			this.properties = properties;
			this.values = values;
			this.filter = filter;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008188 File Offset: 0x00006388
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append("[");
			if (this.properties != null)
			{
				for (int i = 0; i < this.properties.Length; i++)
				{
					if (i != 0)
					{
						sb.Append(", ");
					}
					sb.Append(this.properties[i].ToString());
					sb.Append("=");
					sb.Append(this.values[i].ToString());
				}
			}
			sb.Append("], (");
			if (this.filter != null)
			{
				this.filter.ToString(sb);
			}
			sb.Append("))");
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008237 File Offset: 0x00006437
		public QueryFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000823F File Offset: 0x0000643F
		public PropertyDefinition[] Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008247 File Offset: 0x00006447
		public object[] Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040000A4 RID: 164
		private readonly PropertyDefinition[] properties;

		// Token: 0x040000A5 RID: 165
		private readonly object[] values;

		// Token: 0x040000A6 RID: 166
		private readonly QueryFilter filter;
	}
}
