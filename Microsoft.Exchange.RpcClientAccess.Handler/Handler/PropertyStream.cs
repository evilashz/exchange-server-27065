using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200008A RID: 138
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PropertyStream : Stream
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x000278A8 File Offset: 0x00025AA8
		internal PropertyStream(Stream propertyStream, PropertyType propertyType, Logon logon, StreamSource streamSource) : base(logon)
		{
			Util.ThrowOnNullArgument(streamSource, "streamSource");
			this.propertyType = propertyType;
			this.streamSource = streamSource;
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (propertyStream == null)
				{
					throw new ArgumentNullException("propertyStream");
				}
				this.systemStream = propertyStream;
				disposeGuard.Success();
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002791C File Offset: 0x00025B1C
		public override void Commit()
		{
			base.CheckDisposed();
			this.systemStream.Flush();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002792F File Offset: 0x00025B2F
		public override uint GetSize()
		{
			base.CheckDisposed();
			return (uint)this.systemStream.Length;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00027944 File Offset: 0x00025B44
		public override ArraySegment<byte> Read(ushort requestedSize)
		{
			base.CheckDisposed();
			byte[] array = new byte[(int)requestedSize];
			int num = 0;
			int num2;
			do
			{
				num2 = this.systemStream.Read(array, num, (int)requestedSize - num);
				num += num2;
			}
			while (num2 > 0 && num < (int)requestedSize);
			return new ArraySegment<byte>(array, 0, num);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00027987 File Offset: 0x00025B87
		public override long Seek(StreamSeekOrigin streamSeekOrigin, long offset)
		{
			base.CheckDisposed();
			return this.systemStream.Seek(offset, PropertyStream.ToStreamSeekOrigin(streamSeekOrigin));
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000279A4 File Offset: 0x00025BA4
		public override void SetSize(ulong size)
		{
			base.CheckDisposed();
			if (!this.systemStream.CanWrite)
			{
				throw new RopExecutionException("Cannot change the size of a readonly stream", (ErrorCode)2147680261U);
			}
			if (size > 2147483647UL)
			{
				throw new RopExecutionException(string.Format("Requested size for stream is too big: {0}.", size), (ErrorCode)2147746565U);
			}
			this.systemStream.SetLength((long)size);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00027A04 File Offset: 0x00025C04
		public override ushort Write(ArraySegment<byte> data)
		{
			base.CheckDisposed();
			this.CheckCanWrite();
			if (data.Count > 65535)
			{
				throw new RopExecutionException(string.Format("Requested amount of data to write to stream is too big: {0}.", data.Count), (ErrorCode)2147746565U);
			}
			if (this.systemStream.Position + (long)data.Count > 2147483647L)
			{
				throw new RopExecutionException("Size of the stream after writing the requested data is too big.", (ErrorCode)2147746565U);
			}
			this.systemStream.Write(data.Array, data.Offset, data.Count);
			if (this.propertyType == PropertyType.Binary)
			{
				this.systemStream.Flush();
				this.streamChanged = false;
			}
			else
			{
				this.streamChanged = true;
			}
			return (ushort)data.Count;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override ulong CopyToStream(Stream destinationStream, ulong bytesToCopy)
		{
			base.CheckDisposed();
			destinationStream.CheckCanWrite();
			ulong num = 0UL;
			while (bytesToCopy > num)
			{
				ushort requestedSize = (ushort)Math.Min(bytesToCopy - num, 65535UL);
				ArraySegment<byte> data = this.Read(requestedSize);
				if (data.Count == 0)
				{
					break;
				}
				if (this != destinationStream)
				{
					destinationStream.Write(data);
				}
				num += (ulong)((long)data.Count);
			}
			return num;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00027B23 File Offset: 0x00025D23
		public override void CheckCanWrite()
		{
			base.CheckDisposed();
			if (!this.systemStream.CanWrite)
			{
				throw new RopExecutionException("Cannot write to a readonly stream", (ErrorCode)2147680261U);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00027B48 File Offset: 0x00025D48
		internal override void OnAccess()
		{
			base.OnAccess();
			this.streamSource.OnAccess();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00027B5B File Offset: 0x00025D5B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PropertyStream>(this);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00027B64 File Offset: 0x00025D64
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.streamSource);
			if (this.systemStream != null)
			{
				try
				{
					if (this.streamChanged)
					{
						this.streamChanged = false;
						this.systemStream.Flush();
					}
					this.systemStream.Dispose();
				}
				finally
				{
					this.systemStream = null;
				}
			}
			base.InternalDispose();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00027BCC File Offset: 0x00025DCC
		private static SeekOrigin ToStreamSeekOrigin(StreamSeekOrigin origin)
		{
			switch (origin)
			{
			case StreamSeekOrigin.Begin:
				return SeekOrigin.Begin;
			case StreamSeekOrigin.Current:
				return SeekOrigin.Current;
			case StreamSeekOrigin.End:
				return SeekOrigin.End;
			default:
				throw new RopExecutionException(string.Format("Invalid StreamSeekOrigin value: {0}", origin), (ErrorCode)2147680343U);
			}
		}

		// Token: 0x04000250 RID: 592
		private const ulong DefaultCopyToStreamSegmentSize = 65535UL;

		// Token: 0x04000251 RID: 593
		private readonly StreamSource streamSource;

		// Token: 0x04000252 RID: 594
		private readonly PropertyType propertyType;

		// Token: 0x04000253 RID: 595
		private Stream systemStream;

		// Token: 0x04000254 RID: 596
		private bool streamChanged;
	}
}
