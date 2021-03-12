using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000095 RID: 149
	public abstract class VirtualColumn : Column, IEquatable<VirtualColumn>
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0001D2BE File Offset: 0x0001B4BE
		protected VirtualColumn(VirtualColumnId virtualColumnId, string name, Type type, bool nullable, Visibility visibility, int maxLength, int size, Table table) : base(name, type, nullable, visibility, maxLength, size, table)
		{
			this.virtualColumnId = virtualColumnId;
			base.CacheHashCode();
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001D2DF File Offset: 0x0001B4DF
		public VirtualColumnId VirtualColumnId
		{
			get
			{
				return this.virtualColumnId;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails && base.Table != null)
			{
				sb.Append(base.Table.Name);
				sb.Append(".");
			}
			sb.Append(this.Name);
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails && base.Table != null)
			{
				sb.AppendFormat("(vid={0})", this.virtualColumnId);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001D35F File Offset: 0x0001B55F
		public bool Equals(VirtualColumn other)
		{
			return object.ReferenceEquals(this, other) || (!(other == null) && (this.VirtualColumnId == other.VirtualColumnId && base.Table == other.Table));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001D39B File Offset: 0x0001B59B
		protected internal override bool ActualColumnEquals(Column other)
		{
			return this.Equals(other as VirtualColumn);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001D3A9 File Offset: 0x0001B5A9
		protected override int GetSize(ITWIR context)
		{
			return 0;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		protected override object GetValue(ITWIR context)
		{
			return null;
		}

		// Token: 0x04000255 RID: 597
		private VirtualColumnId virtualColumnId;
	}
}
