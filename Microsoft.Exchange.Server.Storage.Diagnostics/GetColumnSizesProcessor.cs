using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000035 RID: 53
	internal sealed class GetColumnSizesProcessor : GetColumnSizesProcessorBase
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000CEB5 File Offset: 0x0000B0B5
		private GetColumnSizesProcessor(IList<Column> arguments) : base(arguments, false)
		{
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000CEBF File Offset: 0x0000B0BF
		public static ProcessorCollection.QueryableProcessor Info
		{
			get
			{
				return ProcessorCollection.QueryableProcessor.Create("GetColumnSizes", "Column identifier(s)", "Uncompressed (logical) size information for the value of each column", "GetColumnSizes(Column1, Column2, ..., ColumnN)", new Func<IList<Column>, Processor>(GetColumnSizesProcessor.Create));
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000CEE6 File Offset: 0x0000B0E6
		private static Processor Create(IList<Column> arguments)
		{
			if (arguments.Count < 1)
			{
				throw new DiagnosticQueryException(DiagnosticQueryStrings.ProcessorEmptyArguments());
			}
			return new GetColumnSizesProcessor(arguments);
		}
	}
}
