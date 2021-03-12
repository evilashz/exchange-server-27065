using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	internal class NotFilter : QueryFilter
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000710B File Offset: 0x0000530B
		public NotFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			this.filter = filter;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007128 File Offset: 0x00005328
		public QueryFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007130 File Offset: 0x00005330
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(!(");
			this.filter.ToString(sb);
			sb.Append("))");
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007158 File Offset: 0x00005358
		public override bool Equals(object obj)
		{
			NotFilter notFilter = obj as NotFilter;
			return notFilter != null && notFilter.GetType() == base.GetType() && notFilter.filter.Equals(this.filter);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007195 File Offset: 0x00005395
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.filter.GetHashCode();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000071B0 File Offset: 0x000053B0
		public override string PropertyName
		{
			get
			{
				if (this.filter is SinglePropertyFilter)
				{
					SinglePropertyFilter singlePropertyFilter = this.filter as SinglePropertyFilter;
					return QueryFilter.ConvertPropertyName(singlePropertyFilter.Property.Name);
				}
				if (this.filter is CompositeFilter)
				{
					CompositeFilter compositeFilter = this.filter as CompositeFilter;
					return compositeFilter.PropertyName;
				}
				return base.PropertyName;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000720D File Offset: 0x0000540D
		public override IEnumerable<string> Keywords()
		{
			return this.filter.Keywords();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000721A File Offset: 0x0000541A
		public override QueryFilter CloneWithPropertyReplacement(IDictionary<PropertyDefinition, PropertyDefinition> propertyMap)
		{
			return new NotFilter(this.filter.CloneWithPropertyReplacement(propertyMap));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000722D File Offset: 0x0000542D
		internal override IEnumerable<PropertyDefinition> FilterProperties()
		{
			return this.Filter.FilterProperties();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000723C File Offset: 0x0000543C
		protected override int GetSize()
		{
			int size = base.GetSize();
			return size + this.filter.Size;
		}

		// Token: 0x04000090 RID: 144
		private readonly QueryFilter filter;
	}
}
