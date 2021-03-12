using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B3 RID: 179
	internal sealed class JetPropertyColumn : PropertyColumn, IJetColumn, IColumn
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x000260CC File Offset: 0x000242CC
		public JetPropertyColumn(string name, Type type, int size, int maxLength, Table table, StorePropTag propTag, Func<IRowAccess, IRowPropertyBag> rowPropBagCreator, Column[] dependOn) : base(name, type, size, maxLength, table, propTag, rowPropBagCreator, dependOn)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000260EC File Offset: 0x000242EC
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			object value = this.GetValue(cursor);
			return JetColumnValueHelper.GetAsByteArray(value, this);
		}
	}
}
