using System;
using System.IO;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.External
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class Streams
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00007666 File Offset: 0x00005866
		internal static Stream CreateSuppressCloseWrapper(Stream baseStream)
		{
			return new SuppressCloseStream(baseStream);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000766E File Offset: 0x0000586E
		internal static void ConfigureTempStorage(int defaultMaximumSize, int defaultBlockSize, string defaultPath, Func<int, byte[]> defaultAcquireBuffer, Action<byte[]> defaultReleaseBuffer)
		{
			TemporaryDataStorage.Configure(defaultMaximumSize, defaultBlockSize, defaultPath, defaultAcquireBuffer, defaultReleaseBuffer);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000767B File Offset: 0x0000587B
		internal static string GetCtsTempPath()
		{
			return TemporaryDataStorage.GetTempPath();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007682 File Offset: 0x00005882
		internal static Stream CreateTemporaryStorageStream()
		{
			return ApplicationServices.Provider.CreateTemporaryStorage();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007690 File Offset: 0x00005890
		internal static Stream CloneTemporaryStorageStream(Stream originalStream)
		{
			ICloneableStream cloneableStream = originalStream as ICloneableStream;
			if (cloneableStream == null)
			{
				throw new ArgumentException("This stream cannot be cloned", "originalStream");
			}
			return cloneableStream.Clone();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000076BD File Offset: 0x000058BD
		internal static Stream CreateTemporaryStorageStream(Func<int, byte[]> acquireBuffer, Action<byte[]> releaseBuffer)
		{
			return DefaultApplicationServices.CreateTemporaryStorage(acquireBuffer, releaseBuffer);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000076C6 File Offset: 0x000058C6
		internal static Stream CreateAutoPositionedStream(Stream baseStream)
		{
			return new AutoPositionReadOnlyStream(baseStream, true);
		}
	}
}
