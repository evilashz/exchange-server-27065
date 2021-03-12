﻿using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F9 RID: 505
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoGarbageCollector
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x0004D9B5 File Offset: 0x0004BBB5
		public PhotoGarbageCollector(PhotosConfiguration configuration, IPerformanceDataLogger perfLogger, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
			this.perfLogger = perfLogger;
			this.configuration = configuration;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004D9F4 File Offset: 0x0004BBF4
		public void Collect(DateTime startOfCollectionUtc)
		{
			try
			{
				using (new StopwatchPerformanceTracker("GarbageCollection", this.perfLogger))
				{
					using (PhotoGarbageCollectionStatsTracker photoGarbageCollectionStatsTracker = new PhotoGarbageCollectionStatsTracker("GarbageCollection", this.perfLogger))
					{
						foreach (FileInfo file in new PhotoFileEnumerator(this.configuration, this.tracer).Enumerate())
						{
							photoGarbageCollectionStatsTracker.Account(file);
							if (this.IsGarbage(file, startOfCollectionUtc))
							{
								this.Delete(file);
								photoGarbageCollectionStatsTracker.AccountDeleted(file);
							}
						}
					}
				}
			}
			catch (DirectoryNotFoundException arg)
			{
				this.tracer.TraceDebug<DirectoryNotFoundException>((long)this.GetHashCode(), "Garbage collector: directory doesn't exist.  Exception: {0}", arg);
			}
			catch (IOException arg2)
			{
				this.tracer.TraceError<IOException>((long)this.GetHashCode(), "Garbage collector: I/O exception enumerating photo files.  Exception: {0}", arg2);
			}
			catch (UnauthorizedAccessException arg3)
			{
				this.tracer.TraceError<UnauthorizedAccessException>((long)this.GetHashCode(), "Garbage collector: unable to enumerate files because access was not authorized.  Exception: {0}", arg3);
			}
			catch (SecurityException arg4)
			{
				this.tracer.TraceError<SecurityException>((long)this.GetHashCode(), "Garbage collector: unable to enumerate files because of a security exception.  Exception: {0}", arg4);
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004DB70 File Offset: 0x0004BD70
		private bool IsGarbage(FileInfo file, DateTime startOfCollectionUtc)
		{
			if (file == null)
			{
				return false;
			}
			if (startOfCollectionUtc.Subtract(file.LastAccessTimeUtc) < this.configuration.LastAccessGarbageThreshold)
			{
				return false;
			}
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Garbage collector: file {0} was last accessed too long ago.  It's garbage.", file.FullName);
			return true;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
		private void Delete(FileInfo file)
		{
			try
			{
				this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Garbage collector: deleting file {0}", file.FullName);
				File.Delete(file.FullName);
			}
			catch (FileNotFoundException arg)
			{
				this.tracer.TraceError<FileNotFoundException>((long)this.GetHashCode(), "Garbage collector: unable to delete file because file doesn't exist.  Exception: {0}", arg);
			}
			catch (DirectoryNotFoundException arg2)
			{
				this.tracer.TraceError<DirectoryNotFoundException>((long)this.GetHashCode(), "Garbage collector: unable to delete file because directory doesn't exist.  Exception: {0}", arg2);
			}
			catch (NotSupportedException arg3)
			{
				this.tracer.TraceError<NotSupportedException>((long)this.GetHashCode(), "Garbage collector: unable to delete file because operation is not supported.  Exception: {0}", arg3);
			}
			catch (PathTooLongException arg4)
			{
				this.tracer.TraceError<PathTooLongException>((long)this.GetHashCode(), "Garbage collector: unable to delete file because path is too long.  Exception: {0}", arg4);
			}
			catch (UnauthorizedAccessException arg5)
			{
				this.tracer.TraceError<UnauthorizedAccessException>((long)this.GetHashCode(), "Garbage collector: unable to delete file because of insufficient permissions or file is read-only.  Exception: {0}", arg5);
			}
			catch (IOException arg6)
			{
				this.tracer.TraceError<IOException>((long)this.GetHashCode(), "Garbage collector: I/O exception enumerating photo files.  Exception: {0}", arg6);
			}
		}

		// Token: 0x040009E0 RID: 2528
		private readonly PhotosConfiguration configuration;

		// Token: 0x040009E1 RID: 2529
		private readonly IPerformanceDataLogger perfLogger;

		// Token: 0x040009E2 RID: 2530
		private readonly ITracer tracer;
	}
}
