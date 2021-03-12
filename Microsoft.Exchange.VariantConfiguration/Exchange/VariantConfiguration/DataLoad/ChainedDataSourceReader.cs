using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.VariantConfiguration.DataLoad
{
	// Token: 0x02000006 RID: 6
	internal class ChainedDataSourceReader : IDataSourceReader
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000256C File Offset: 0x0000076C
		public ChainedDataSourceReader()
		{
			this.dataSourceReaders = new LinkedList<IDataSourceReader>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002580 File Offset: 0x00000780
		public Func<TextReader> GetContentReader(string dataSource)
		{
			if (string.IsNullOrEmpty(dataSource))
			{
				throw new ArgumentNullException("dataSource");
			}
			foreach (IDataSourceReader dataSourceReader in this.dataSourceReaders)
			{
				if (dataSourceReader.CanGetContentReader(dataSource))
				{
					return dataSourceReader.GetContentReader(dataSource);
				}
			}
			throw new ArgumentException(string.Format("Unable to get content reader for data source '{0}'", dataSource));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002604 File Offset: 0x00000804
		public bool CanGetContentReader(string dataSource)
		{
			foreach (IDataSourceReader dataSourceReader in this.dataSourceReaders)
			{
				if (dataSourceReader.CanGetContentReader(dataSource))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002660 File Offset: 0x00000860
		public void AddDataSourceReader(IDataSourceReader dataSourceReader)
		{
			if (dataSourceReader == null)
			{
				throw new ArgumentNullException("dataSourceReader");
			}
			this.dataSourceReaders.AddLast(dataSourceReader);
		}

		// Token: 0x0400000B RID: 11
		private readonly LinkedList<IDataSourceReader> dataSourceReaders;
	}
}
