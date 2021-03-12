using System;
using System.IO;

namespace Microsoft.Exchange.VariantConfiguration.DataLoad
{
	// Token: 0x02000007 RID: 7
	internal class FilesDataSourceReader : IDataSourceReader
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000267D File Offset: 0x0000087D
		internal FilesDataSourceReader(string directory)
		{
			if (string.IsNullOrEmpty(directory))
			{
				throw new ArgumentNullException("directory");
			}
			if (!Directory.Exists(directory))
			{
				throw new ArgumentException(string.Format("Directory '{0}' does not exist.", directory));
			}
			this.directory = directory;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026D4 File Offset: 0x000008D4
		public Func<TextReader> GetContentReader(string dataSource)
		{
			if (string.IsNullOrEmpty(dataSource))
			{
				throw new ArgumentNullException("dataSource");
			}
			string filePath = Path.Combine(this.directory, dataSource);
			if (!File.Exists(filePath))
			{
				throw new ArgumentException(string.Format("'{0}' does not exist, therefore data source '{1}' cannot be read.", filePath, dataSource));
			}
			return () => new StreamReader(File.OpenRead(filePath));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000273C File Offset: 0x0000093C
		public bool CanGetContentReader(string dataSource)
		{
			return !string.IsNullOrEmpty(dataSource) && File.Exists(Path.Combine(this.directory, dataSource));
		}

		// Token: 0x0400000C RID: 12
		private readonly string directory;
	}
}
