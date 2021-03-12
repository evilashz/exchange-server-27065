using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000034 RID: 52
	internal abstract class GetColumnSizesProcessorBase : Processor
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000CD88 File Offset: 0x0000AF88
		protected GetColumnSizesProcessorBase(IList<Column> arguments, bool compressedSize) : base(GetColumnSizesProcessorBase.GetArgumentColumns(arguments, compressedSize))
		{
			this.generated = new List<Processor.ColumnDefinition>(arguments.Count);
			foreach (Column column in base.Arguments.Values)
			{
				this.generated.Add(new Processor.ColumnDefinition(column.Name, typeof(int), Visibility.Public));
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000CE14 File Offset: 0x0000B014
		public override IEnumerable<Processor.ColumnDefinition> GetGeneratedColumns()
		{
			return this.generated;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public override object GetValue(SimpleQueryOperator qop, Reader reader, Column column)
		{
			Column column2 = null;
			if (!base.Arguments.TryGetValue(column.Name, out column2))
			{
				throw new DiagnosticQueryException(DiagnosticQueryStrings.ProcessorColumnNotFound(column.Name));
			}
			return reader.GetValue(column2);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000CE58 File Offset: 0x0000B058
		private static IList<Column> GetArgumentColumns(IList<Column> arguments, bool compressedSize)
		{
			Column[] array = new Column[arguments.Count];
			string str = compressedSize ? "_CompressedSize" : "_Size";
			for (int i = 0; i < arguments.Count; i++)
			{
				array[i] = Factory.CreateSizeOfColumn(arguments[i].Name + str, arguments[i], compressedSize);
			}
			return array;
		}

		// Token: 0x04000111 RID: 273
		private readonly IList<Processor.ColumnDefinition> generated;
	}
}
