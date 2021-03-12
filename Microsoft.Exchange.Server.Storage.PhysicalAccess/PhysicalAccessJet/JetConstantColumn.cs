using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000A0 RID: 160
	internal sealed class JetConstantColumn : ConstantColumn, IJetColumn, IColumn
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x0002000E File Offset: 0x0001E20E
		internal JetConstantColumn(string name, Type type, Visibility visibility, int size, int maxLength, object value) : base(name, type, visibility, size, maxLength, value)
		{
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00020020 File Offset: 0x0001E220
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			object value = this.GetValue(cursor);
			return JetColumnValueHelper.GetAsByteArray(value, this);
		}
	}
}
