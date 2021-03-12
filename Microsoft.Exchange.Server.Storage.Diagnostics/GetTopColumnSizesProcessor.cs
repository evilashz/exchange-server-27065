using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000039 RID: 57
	internal sealed class GetTopColumnSizesProcessor : GetTopColumnSizesProcessorBase
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000D247 File Offset: 0x0000B447
		private GetTopColumnSizesProcessor(IList<Column> arguments) : base(arguments, false)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000D251 File Offset: 0x0000B451
		public static ProcessorCollection.QueryableProcessor Info
		{
			get
			{
				return ProcessorCollection.QueryableProcessor.Create("GetTopColumnSizes", "Column identifier(s)", "Columns describing the largest values present based on uncompressed (logical) column size", "GetTopColumnSizes(Column1, Column2, ..., ColumnN)", new Func<IList<Column>, Processor>(GetTopColumnSizesProcessor.Create));
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000D278 File Offset: 0x0000B478
		private static Processor Create(IList<Column> arguments)
		{
			if (arguments.Count < 1)
			{
				throw new DiagnosticQueryException(DiagnosticQueryStrings.ProcessorEmptyArguments());
			}
			return new GetTopColumnSizesProcessor(arguments);
		}
	}
}
