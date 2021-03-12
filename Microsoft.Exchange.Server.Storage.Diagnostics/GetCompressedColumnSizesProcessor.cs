using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000036 RID: 54
	internal sealed class GetCompressedColumnSizesProcessor : GetColumnSizesProcessorBase
	{
		// Token: 0x0600019D RID: 413 RVA: 0x0000CF02 File Offset: 0x0000B102
		private GetCompressedColumnSizesProcessor(IList<Column> arguments) : base(arguments, true)
		{
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public static ProcessorCollection.QueryableProcessor Info
		{
			get
			{
				return ProcessorCollection.QueryableProcessor.Create("GetCompressedColumnSizes", "Column identifier(s)", "Compressed (physical) size information for the value of each column", "GetCompressedColumnSizes(Column1, Column2, ..., ColumnN)", new Func<IList<Column>, Processor>(GetCompressedColumnSizesProcessor.Create));
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000CF33 File Offset: 0x0000B133
		private static Processor Create(IList<Column> arguments)
		{
			if (arguments.Count < 1)
			{
				throw new DiagnosticQueryException(DiagnosticQueryStrings.ProcessorEmptyArguments());
			}
			return new GetCompressedColumnSizesProcessor(arguments);
		}
	}
}
