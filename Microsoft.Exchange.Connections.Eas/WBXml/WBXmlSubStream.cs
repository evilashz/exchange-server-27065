using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class WBXmlSubStream : StreamAccessor
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000781C File Offset: 0x00005A1C
		internal WBXmlSubStream(Stream stream, long startPosition, long length) : base(stream)
		{
			this.startPosition = startPosition;
			this.length = length;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007833 File Offset: 0x00005A33
		internal override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000783B File Offset: 0x00005A3B
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000784F File Offset: 0x00005A4F
		internal override long Position
		{
			get
			{
				return base.InternalStream.Position - this.startPosition;
			}
			set
			{
				if (value > this.length)
				{
					base.InternalStream.Position = this.startPosition + this.length;
					return;
				}
				base.InternalStream.Position = this.startPosition + value;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007888 File Offset: 0x00005A88
		internal override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			if (origin == SeekOrigin.Begin)
			{
				num = this.startPosition + offset;
			}
			else if (origin == SeekOrigin.Current)
			{
				num = base.InternalStream.Position + offset;
			}
			else
			{
				if (origin != SeekOrigin.End)
				{
					throw new ArgumentException();
				}
				num = this.length + offset;
			}
			if (num < this.startPosition)
			{
				num = this.startPosition;
			}
			if (num > this.startPosition + this.length)
			{
				num = this.startPosition + this.length;
			}
			return base.InternalStream.Seek(num, SeekOrigin.Begin);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000790C File Offset: 0x00005B0C
		internal override int Read(byte[] buffer, int offset, int count)
		{
			if (base.InternalStream.Position + (long)count > this.startPosition + this.length)
			{
				count = (int)(this.startPosition + this.length - base.InternalStream.Position);
			}
			if (count > 0)
			{
				return base.InternalStream.Read(buffer, offset, count);
			}
			return 0;
		}

		// Token: 0x040003FC RID: 1020
		private readonly long startPosition;

		// Token: 0x040003FD RID: 1021
		private readonly long length;
	}
}
