using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000AF RID: 175
	internal sealed class JetMappedPropertyColumn : MappedPropertyColumn, IJetColumn, IColumn
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x00025C66 File Offset: 0x00023E66
		internal JetMappedPropertyColumn(Column actualColumn, StorePropTag propTag) : base(actualColumn, propTag)
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00025C70 File Offset: 0x00023E70
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			return cursor.GetColumnValueAsBytes(this.ActualColumn);
		}
	}
}
