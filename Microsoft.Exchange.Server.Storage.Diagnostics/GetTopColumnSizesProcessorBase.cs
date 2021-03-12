using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000038 RID: 56
	internal abstract class GetTopColumnSizesProcessorBase : GetTopSizesProcessor
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000D15D File Offset: 0x0000B35D
		protected GetTopColumnSizesProcessorBase(IList<Column> arguments, bool compressedSize) : base(compressedSize ? "CompressedColumn" : "Column", ConfigurationSchema.ProcessorNumberOfColumnResults.Value, GetTopColumnSizesProcessorBase.GetArgumentColumns(arguments, compressedSize))
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000D188 File Offset: 0x0000B388
		public override List<Tuple<string, int>> GetData(SimpleQueryOperator qop, Reader reader)
		{
			List<Tuple<string, int>> list = new List<Tuple<string, int>>(100);
			if (reader != null)
			{
				foreach (Column column in base.Arguments.Values)
				{
					int item = (int)reader.GetValue(column);
					list.Add(new Tuple<string, int>(column.Name, item));
				}
			}
			return list;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000D200 File Offset: 0x0000B400
		private static IList<Column> GetArgumentColumns(IList<Column> arguments, bool compressedSize)
		{
			Column[] array = new Column[arguments.Count];
			for (int i = 0; i < arguments.Count; i++)
			{
				array[i] = Factory.CreateSizeOfColumn(arguments[i].Name, arguments[i], compressedSize);
			}
			return array;
		}
	}
}
