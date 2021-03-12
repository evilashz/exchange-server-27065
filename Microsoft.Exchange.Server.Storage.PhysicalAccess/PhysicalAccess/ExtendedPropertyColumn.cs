using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200000C RID: 12
	public abstract class ExtendedPropertyColumn : Column
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000BC63 File Offset: 0x00009E63
		protected ExtendedPropertyColumn(string name, Type type, bool nullable, Visibility visibility, int size, int maxLength, Table table, StorePropTag propTag) : base(name, type, nullable, visibility, maxLength, size, table)
		{
			this.propTag = propTag;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000BC80 File Offset: 0x00009E80
		protected ExtendedPropertyColumn(string name, Type type, Visibility visibility, int size, int maxLength, Table table, StorePropTag propTag) : this(name, type, true, visibility, size, maxLength, table, propTag)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000BC9F File Offset: 0x00009E9F
		public StorePropTag StorePropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		public override void GetNameOrIdForSerialization(out string columnName, out uint columnId)
		{
			columnName = string.Empty;
			columnId = this.StorePropTag.PropTag;
		}

		// Token: 0x04000066 RID: 102
		private readonly StorePropTag propTag;
	}
}
