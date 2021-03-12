using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F2 RID: 498
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoFileEnumerator
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x0004D2DC File Offset: 0x0004B4DC
		public PhotoFileEnumerator(PhotosConfiguration configuration, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
			this.configuration = configuration;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004D308 File Offset: 0x0004B508
		public IEnumerable<FileInfo> Enumerate()
		{
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Photo file enumerator: enumerating at {0}", this.configuration.PhotosRootDirectoryFullPath);
			if (!Directory.Exists(this.configuration.PhotosRootDirectoryFullPath))
			{
				return Array<FileInfo>.Empty;
			}
			return new DirectoryInfo(this.configuration.PhotosRootDirectoryFullPath).EnumerateFiles("*.jpg", SearchOption.AllDirectories);
		}

		// Token: 0x040009B3 RID: 2483
		private const string PhotoFilePattern = "*.jpg";

		// Token: 0x040009B4 RID: 2484
		private readonly ITracer tracer;

		// Token: 0x040009B5 RID: 2485
		private readonly PhotosConfiguration configuration;
	}
}
