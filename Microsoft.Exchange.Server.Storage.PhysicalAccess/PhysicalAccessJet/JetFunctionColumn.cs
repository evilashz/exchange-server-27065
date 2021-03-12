using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000A2 RID: 162
	internal sealed class JetFunctionColumn : FunctionColumn, IJetColumn, IColumn
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x00020078 File Offset: 0x0001E278
		internal JetFunctionColumn(string name, Type type, int size, int maxLength, Table table, Func<object[], object> function, string functionName, params Column[] argumentColumns) : base(name, type, size, maxLength, table, function, functionName, argumentColumns)
		{
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00020098 File Offset: 0x0001E298
		byte[] IJetColumn.GetValueAsBytes(IJetSimpleQueryOperator cursor)
		{
			object value = this.GetValue(cursor);
			return JetColumnValueHelper.GetAsByteArray(value, this);
		}
	}
}
