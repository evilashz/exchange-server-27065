using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000202 RID: 514
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestLogFactory
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0004E2D7 File Offset: 0x0004C4D7
		public PhotoRequestLogFactory(PhotosConfiguration configuration, string build)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNullOrEmpty("build", build);
			this.build = build;
			this.configuration = configuration;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004E304 File Offset: 0x0004C504
		public PhotoRequestLog Create()
		{
			if (PhotoRequestLogFactory.logInstance == null)
			{
				lock (PhotoRequestLogFactory.SyncLock)
				{
					if (PhotoRequestLogFactory.logInstance == null)
					{
						PhotoRequestLogFactory.logInstance = new PhotoRequestLog(this.configuration, this.GetLogFilenamePrefix(), this.build);
					}
				}
			}
			return PhotoRequestLogFactory.logInstance;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004E36C File Offset: 0x0004C56C
		private string GetLogFilenamePrefix()
		{
			return ApplicationName.Current.Name;
		}

		// Token: 0x04000A51 RID: 2641
		private static readonly object SyncLock = new object();

		// Token: 0x04000A52 RID: 2642
		private static PhotoRequestLog logInstance;

		// Token: 0x04000A53 RID: 2643
		private readonly PhotosConfiguration configuration;

		// Token: 0x04000A54 RID: 2644
		private readonly string build;
	}
}
