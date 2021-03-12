using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.ImportContacts
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ImportContactsCsvSchema : CsvSchema
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00008494 File Offset: 0x00006694
		public ImportContactsCsvSchema(Dictionary<string, ImportContactProperties> columnsMapping, CultureInfo culture) : base(2000, null, null, null)
		{
			SyncUtilities.ThrowIfArgumentNull("columnsMapping", columnsMapping);
			SyncUtilities.ThrowIfArgumentNull("culture", culture);
			this.columnsMapping = columnsMapping;
			this.culture = culture;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000084C8 File Offset: 0x000066C8
		public Dictionary<string, ImportContactProperties> ColumnsMapping
		{
			get
			{
				return this.columnsMapping;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000084D0 File Offset: 0x000066D0
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000084D8 File Offset: 0x000066D8
		public static CsvRow? ReadCSVHeader(Stream sourceStream, Encoding encoding)
		{
			Stream stream = Streams.CreateSuppressCloseWrapper(sourceStream);
			CsvRow? result;
			using (StreamReader streamReader = new StreamReader(stream, encoding))
			{
				using (CsvReader csvReader = new CsvReader(streamReader, true))
				{
					if (ImportContactsCsvSchema.IsEmptyHeader(csvReader.Headers))
					{
						result = null;
					}
					else
					{
						CsvColumnMap columnMap = new CsvColumnMap(csvReader.Headers, false);
						result = new CsvRow?(new CsvRow(0, csvReader.Headers, columnMap));
					}
				}
			}
			return result;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008570 File Offset: 0x00006770
		protected override CsvRow CreateCsvRow(int index, IList<string> data, CsvColumnMap columnMap)
		{
			int num = columnMap.Count - data.Count;
			if (num > 0)
			{
				List<string> list = new List<string>(data);
				for (int i = 0; i < num; i++)
				{
					list.Add(string.Empty);
				}
				return base.CreateCsvRow(index, list, columnMap);
			}
			if (num == -1 && data[data.Count - 1] == string.Empty)
			{
				List<string> list2 = new List<string>(data);
				list2.RemoveAt(data.Count - 1);
				return base.CreateCsvRow(index, list2, columnMap);
			}
			return base.CreateCsvRow(index, data, columnMap);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000085FE File Offset: 0x000067FE
		private static bool IsEmptyHeader(string[] header)
		{
			return header == null || header.Length <= 0 || (header.Length == 1 && header[0].Trim() == string.Empty);
		}

		// Token: 0x04000129 RID: 297
		public const int InternalMaximumRowCount = 2000;

		// Token: 0x0400012A RID: 298
		private Dictionary<string, ImportContactProperties> columnsMapping;

		// Token: 0x0400012B RID: 299
		private CultureInfo culture;
	}
}
