using System;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200001B RID: 27
	internal static class Streams
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000046C1 File Offset: 0x000028C1
		public static Stream CreateSuppressCloseWrapper(Stream baseStream)
		{
			return new SuppressCloseStream(baseStream);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000046C9 File Offset: 0x000028C9
		public static void ConfigureTempStorage(int defaultMaximumSize, int defaultBlockSize, string defaultPath, Func<int, byte[]> defaultAcquireBuffer, Action<byte[]> defaultReleaseBuffer)
		{
			TemporaryDataStorage.Configure(defaultMaximumSize, defaultBlockSize, defaultPath, defaultAcquireBuffer, defaultReleaseBuffer);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000046D6 File Offset: 0x000028D6
		public static string GetCtsTempPath()
		{
			return TemporaryDataStorage.GetTempPath();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000046DD File Offset: 0x000028DD
		public static Stream CreateTemporaryStorageStream()
		{
			return ApplicationServices.Provider.CreateTemporaryStorage();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000046EC File Offset: 0x000028EC
		public static Stream CloneTemporaryStorageStream(Stream originalStream)
		{
			ICloneableStream cloneableStream = originalStream as ICloneableStream;
			if (cloneableStream == null)
			{
				throw new ArgumentException("This stream cannot be cloned", "originalStream");
			}
			return cloneableStream.Clone();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004719 File Offset: 0x00002919
		public static Stream CreateTemporaryStorageStream(Func<int, byte[]> acquireBuffer, Action<byte[]> releaseBuffer)
		{
			return DefaultApplicationServices.CreateTemporaryStorage(acquireBuffer, releaseBuffer);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004722 File Offset: 0x00002922
		public static Stream CreateAutoPositionedStream(Stream baseStream)
		{
			return new AutoPositionReadOnlyStream(baseStream, true);
		}
	}
}
