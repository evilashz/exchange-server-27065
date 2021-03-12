using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x02000117 RID: 279
	internal class MeterableJetDataSource : IMeterableJetDataSource
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		public MeterableJetDataSource(DataSource dataSource)
		{
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.dataSource = dataSource;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002E1BA File Offset: 0x0002C3BA
		public string DatabasePath
		{
			get
			{
				return this.dataSource.DatabasePath;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
		public long GetAvailableFreeSpace()
		{
			ulong availableFreeSpace = this.dataSource.GetAvailableFreeSpace();
			if (availableFreeSpace > 9223372036854775807UL)
			{
				throw new OverflowException("The data source returned available free space value larger than long.MaxValue");
			}
			return (long)availableFreeSpace;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		public long GetDatabaseFileSize()
		{
			long result;
			try
			{
				if (this.databaseFileInfo == null)
				{
					this.databaseFileInfo = new FileInfo(this.DatabasePath);
				}
				this.databaseFileInfo.Refresh();
				result = this.databaseFileInfo.Length;
			}
			catch (IOException)
			{
				this.databaseFileInfo = null;
				result = 0L;
			}
			return result;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002E25C File Offset: 0x0002C45C
		public long GetCurrentVersionBuckets()
		{
			return this.dataSource.GetCurrentVersionBuckets();
		}

		// Token: 0x04000560 RID: 1376
		private readonly DataSource dataSource;

		// Token: 0x04000561 RID: 1377
		private FileInfo databaseFileInfo;
	}
}
