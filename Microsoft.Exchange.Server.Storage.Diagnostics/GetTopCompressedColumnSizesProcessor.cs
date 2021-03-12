using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200003A RID: 58
	internal sealed class GetTopCompressedColumnSizesProcessor : GetTopColumnSizesProcessorBase
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000D294 File Offset: 0x0000B494
		private GetTopCompressedColumnSizesProcessor(IList<Column> arguments) : base(arguments, true)
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000D29E File Offset: 0x0000B49E
		public static ProcessorCollection.QueryableProcessor Info
		{
			get
			{
				return ProcessorCollection.QueryableProcessor.Create("GetTopCompressedColumnSizes", "Column identifier(s)", "Columns describing the largest values present based on compressed (physical) column size", "GetTopCompressedColumnSizes(Column1, Column2, ..., ColumnN)", new Func<IList<Column>, Processor>(GetTopCompressedColumnSizesProcessor.Create));
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000D2C5 File Offset: 0x0000B4C5
		private static Processor Create(IList<Column> arguments)
		{
			if (arguments.Count < 1)
			{
				throw new DiagnosticQueryException(DiagnosticQueryStrings.ProcessorEmptyArguments());
			}
			return new GetTopCompressedColumnSizesProcessor(arguments);
		}
	}
}
