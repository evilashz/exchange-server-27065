using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000010 RID: 16
	public abstract class PropertyColumn : ExtendedPropertyColumn
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000C470 File Offset: 0x0000A670
		protected PropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, Column[] dependOn) : base(name, type, PropertyColumn.GetVisibility(propTag, dependOn), size, maxLength, table, propTag)
		{
			this.rowPropBagCreator = rowPropBagCreator;
			this.dependOn = dependOn;
			base.CacheHashCode();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		public Func<IRowAccess, IRowPropertyBag> PropertyBagCreator
		{
			get
			{
				return this.rowPropBagCreator;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public Column[] DependOn
		{
			get
			{
				return this.dependOn;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		public override void EnumerateColumns(Action<Column, object> callback, object state)
		{
			callback(this, state);
			if (this.dependOn != null)
			{
				foreach (Column column in this.dependOn)
				{
					column.EnumerateColumns(callback, state);
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append(this.Name);
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails)
			{
				sb.Append(":PROP[");
				base.StorePropTag.AppendToString(sb, true);
				sb.Append("]");
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000C538 File Offset: 0x0000A738
		protected internal override bool ActualColumnEquals(Column other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			ExtendedPropertyColumn extendedPropertyColumn = other as ExtendedPropertyColumn;
			return extendedPropertyColumn != null && base.StorePropTag == extendedPropertyColumn.StorePropTag && base.Table == extendedPropertyColumn.Table;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000C584 File Offset: 0x0000A784
		protected override int CalculateHashCode()
		{
			return ((base.Table == null) ? 0 : base.Table.GetHashCode()) ^ (int)base.StorePropTag.PropTag;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		protected override int GetSize(ITWIR context)
		{
			return context.GetPropertyColumnSize(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
		protected override object GetValue(ITWIR context)
		{
			return context.GetPropertyColumnValue(this);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		private static Visibility GetVisibility(StorePropTag propTag, Column[] columns)
		{
			Visibility v = (propTag.PropInfo != null) ? propTag.PropInfo.Visibility : Visibility.Public;
			Visibility visibility;
			if (columns == null || columns.Length <= 0)
			{
				visibility = Visibility.Public;
			}
			else
			{
				visibility = VisibilityHelper.Select(from col in columns
				select col.Visibility);
			}
			Visibility v2 = visibility;
			return VisibilityHelper.Select(v, v2);
		}

		// Token: 0x0400006F RID: 111
		private readonly Func<IRowAccess, IRowPropertyBag> rowPropBagCreator;

		// Token: 0x04000070 RID: 112
		private readonly Column[] dependOn;
	}
}
