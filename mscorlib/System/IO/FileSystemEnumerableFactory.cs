using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200018D RID: 397
	internal static class FileSystemEnumerableFactory
	{
		// Token: 0x0600188B RID: 6283 RVA: 0x00050254 File Offset: 0x0004E454
		internal static IEnumerable<string> CreateFileNameIterator(string path, string originalUserPath, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
		{
			SearchResultHandler<string> resultHandler = new StringResultHandler(includeFiles, includeDirs);
			return new FileSystemEnumerableIterator<string>(path, originalUserPath, searchPattern, searchOption, resultHandler, checkHost);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00050278 File Offset: 0x0004E478
		internal static IEnumerable<FileInfo> CreateFileInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<FileInfo> resultHandler = new FileInfoResultHandler();
			return new FileSystemEnumerableIterator<FileInfo>(path, originalUserPath, searchPattern, searchOption, resultHandler, true);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00050298 File Offset: 0x0004E498
		internal static IEnumerable<DirectoryInfo> CreateDirectoryInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<DirectoryInfo> resultHandler = new DirectoryInfoResultHandler();
			return new FileSystemEnumerableIterator<DirectoryInfo>(path, originalUserPath, searchPattern, searchOption, resultHandler, true);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000502B8 File Offset: 0x0004E4B8
		internal static IEnumerable<FileSystemInfo> CreateFileSystemInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<FileSystemInfo> resultHandler = new FileSystemInfoResultHandler();
			return new FileSystemEnumerableIterator<FileSystemInfo>(path, originalUserPath, searchPattern, searchOption, resultHandler, true);
		}
	}
}
