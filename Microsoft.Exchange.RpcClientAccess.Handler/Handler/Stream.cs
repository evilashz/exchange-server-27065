using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class Stream : ServerObject
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0001AED4 File Offset: 0x000190D4
		internal Stream(Logon logon) : base(logon)
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001AEDD File Offset: 0x000190DD
		public static Stream Create(Stream propertyStream, PropertyType propertyType, Logon logon, StreamSource streamSource)
		{
			return new PropertyStream(propertyStream, propertyType, logon, streamSource);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001AEE8 File Offset: 0x000190E8
		public static Stream CreateNull(Logon logon)
		{
			return new NullStream(logon);
		}

		// Token: 0x0600036F RID: 879
		public abstract void Commit();

		// Token: 0x06000370 RID: 880
		public abstract uint GetSize();

		// Token: 0x06000371 RID: 881
		public abstract ArraySegment<byte> Read(ushort requestedSize);

		// Token: 0x06000372 RID: 882
		public abstract long Seek(StreamSeekOrigin streamSeekOrigin, long offset);

		// Token: 0x06000373 RID: 883
		public abstract void SetSize(ulong size);

		// Token: 0x06000374 RID: 884
		public abstract ushort Write(ArraySegment<byte> data);

		// Token: 0x06000375 RID: 885
		public abstract ulong CopyToStream(Stream destinationStream, ulong bytesToCopy);

		// Token: 0x06000376 RID: 886
		public abstract void CheckCanWrite();
	}
}
