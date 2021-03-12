using System;
using System.IO;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000010 RID: 16
	internal class PSTPropertyStream : Stream
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00006374 File Offset: 0x00004574
		public PSTPropertyStream(bool isRead, IProperty pstProperty)
		{
			this.position = 0;
			this.overflowBuffer = new byte[0];
			this.overflowPosition = 0;
			this.propTag = new PropertyTag(pstProperty.PropTag);
			if (isRead)
			{
				this.pstStreamReader = pstProperty.OpenStreamReader();
				this.length = (long)this.pstStreamReader.Length;
				return;
			}
			this.pstStreamWriter = pstProperty.OpenStreamWriter();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000063E0 File Offset: 0x000045E0
		public PropertyTag PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000063E8 File Offset: 0x000045E8
		public override bool CanRead
		{
			get
			{
				return this.pstStreamReader != null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000063F6 File Offset: 0x000045F6
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000063F9 File Offset: 0x000045F9
		public override bool CanWrite
		{
			get
			{
				return this.pstStreamWriter != null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006407 File Offset: 0x00004607
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000640F File Offset: 0x0000460F
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00006418 File Offset: 0x00004618
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				if (value != 0L)
				{
					throw new NotImplementedException("PSTPropertyStream does not implement 'public override long Position'");
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000642A File Offset: 0x0000462A
		public override void Close()
		{
			if (this.pstStreamReader != null)
			{
				this.pstStreamReader.Close();
				this.pstStreamReader = null;
				return;
			}
			if (this.pstStreamWriter != null)
			{
				this.pstStreamWriter.Close();
				this.pstStreamWriter = null;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006461 File Offset: 0x00004661
		public override void Flush()
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006464 File Offset: 0x00004664
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count < 0 || offset < 0)
			{
				throw new UnableToStreamPSTPropPermanentException(this.propTag, offset, count, this.Length);
			}
			int i = count;
			while (i > 0)
			{
				if (this.overflowBuffer.Length - this.overflowPosition <= i)
				{
					if (this.overflowBuffer.Length - this.overflowPosition > 0)
					{
						Array.Copy(this.overflowBuffer, this.overflowPosition, buffer, offset + count - i, this.overflowBuffer.Length - this.overflowPosition);
						this.position += this.overflowBuffer.Length - this.overflowPosition;
						i -= this.overflowBuffer.Length - this.overflowPosition;
					}
					if (this.pstStreamReader.IsEnd)
					{
						if (i != 0)
						{
							throw new UnableToStreamPSTPropPermanentException(this.propTag, offset, i, this.Length);
						}
						break;
					}
					else
					{
						this.overflowBuffer = this.pstStreamReader.Read();
						this.overflowPosition = 0;
					}
				}
				else
				{
					Array.Copy(this.overflowBuffer, this.overflowPosition, buffer, offset + count - i, i);
					this.overflowPosition += i;
					this.position += i;
					i = 0;
				}
			}
			return count - i;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006597 File Offset: 0x00004797
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException("PSTPropertyStream does not implement 'public override long Seek'");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000065A3 File Offset: 0x000047A3
		public override void SetLength(long value)
		{
			throw new NotImplementedException("PSTPropertyStream does not implement 'public override void SetLength'");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000065B0 File Offset: 0x000047B0
		public override void Write(byte[] buffer, int offset, int count)
		{
			byte[] array = new byte[count];
			Array.Copy(buffer, offset, array, 0, count);
			this.pstStreamWriter.Write(array);
			this.position += count;
			this.length += (long)count;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000065F7 File Offset: 0x000047F7
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x0400003B RID: 59
		private readonly PropertyTag propTag;

		// Token: 0x0400003C RID: 60
		private IPropertyReader pstStreamReader;

		// Token: 0x0400003D RID: 61
		private IPropertyWriter pstStreamWriter;

		// Token: 0x0400003E RID: 62
		private long length;

		// Token: 0x0400003F RID: 63
		private byte[] overflowBuffer;

		// Token: 0x04000040 RID: 64
		private int overflowPosition;

		// Token: 0x04000041 RID: 65
		private int position;
	}
}
