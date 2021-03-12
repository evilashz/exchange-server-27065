using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000A1 RID: 161
	internal sealed class JetConversionColumn : ConversionColumn, IJetColumn, IColumn
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0002003C File Offset: 0x0001E23C
		internal JetConversionColumn(string name, Type type, int size, int maxLength, Table table, Func<object, object> conversionFunction, string functionName, Column argumentColumn) : base(name, type, size, maxLength, table, conversionFunction, functionName, argumentColumn)
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0002005C File Offset: 0x0001E25C
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			object value = this.GetValue(cursor);
			return JetColumnValueHelper.GetAsByteArray(value, this);
		}
	}
}
