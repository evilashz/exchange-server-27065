using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class StreamAccessor
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00007642 File Offset: 0x00005842
		internal StreamAccessor(Stream stream)
		{
			this.internalStream = stream;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00007651 File Offset: 0x00005851
		internal virtual bool CanSeek
		{
			get
			{
				return this.internalStream.CanSeek;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000227 RID: 551
		internal abstract long Length { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000228 RID: 552
		// (set) Token: 0x06000229 RID: 553
		internal abstract long Position { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000765E File Offset: 0x0000585E
		protected Stream InternalStream
		{
			get
			{
				return this.internalStream;
			}
		}

		// Token: 0x0600022B RID: 555
		internal abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x0600022C RID: 556
		internal abstract int Read(byte[] buffer, int offset, int count);

		// Token: 0x040003E1 RID: 993
		private readonly Stream internalStream;
	}
}
