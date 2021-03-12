using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyConversionStream : Stream
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0001AEF0 File Offset: 0x000190F0
		internal BodyConversionStream(Func<PropertyTag, BodyHelper, Stream> conversionStreamFactory, Logon logon, StreamSource streamSource, PropertyTag propertyTag, bool seekToEnd, BodyHelper bodyHelper) : base(logon)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(streamSource, "streamSource");
				Util.ThrowOnNullArgument(conversionStreamFactory, "conversionStreamFactory");
				this.conversionStreamFactory = conversionStreamFactory;
				this.streamSource = streamSource;
				this.propertyTag = propertyTag;
				this.seekToEnd = seekToEnd;
				this.bodyHelper = bodyHelper;
				disposeGuard.Success();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0001AF7C File Offset: 0x0001917C
		internal long CurrentPosition
		{
			get
			{
				if (this.seekToEnd)
				{
					return this.StreamLength;
				}
				return this.currentPosition;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0001AF93 File Offset: 0x00019193
		internal long StreamLength
		{
			get
			{
				if (this.streamLength == null)
				{
					this.streamLength = new long?(this.ConversionStream.Length);
				}
				return this.streamLength.Value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0001AFC3 File Offset: 0x000191C3
		private Stream ConversionStream
		{
			get
			{
				if (this.conversionStream == null)
				{
					this.conversionStream = this.conversionStreamFactory(this.propertyTag, this.bodyHelper);
				}
				return this.conversionStream;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override void Commit()
		{
			base.CheckDisposed();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001AFF8 File Offset: 0x000191F8
		public override uint GetSize()
		{
			base.CheckDisposed();
			return (uint)this.StreamLength;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001B007 File Offset: 0x00019207
		public override ArraySegment<byte> Read(ushort requestedSize)
		{
			base.CheckDisposed();
			this.SeekToEndIfNeeded();
			return this.FillBuffer(new ArraySegment<byte>(new byte[(int)requestedSize]));
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001B028 File Offset: 0x00019228
		public override long Seek(StreamSeekOrigin streamSeekOrigin, long offset)
		{
			base.CheckDisposed();
			long num = this.CurrentPosition;
			long num2;
			long result;
			switch (streamSeekOrigin)
			{
			case StreamSeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new RopExecutionException("Cannot seek backwards past beginning of stream", (ErrorCode)2147680281U);
				}
				if (this.CurrentPosition > offset)
				{
					this.ResetConversionStream();
				}
				else
				{
					this.SeekToEndIfNeeded();
					if (offset > this.StreamLength)
					{
						offset = this.StreamLength;
					}
				}
				num2 = offset - this.CurrentPosition;
				result = offset;
				break;
			case StreamSeekOrigin.Current:
				if (offset < 0L)
				{
					if (this.CurrentPosition + offset < 0L)
					{
						throw new RopExecutionException("Cannot seek backwards past beginning of stream", (ErrorCode)2147680281U);
					}
					offset = this.CurrentPosition + offset;
					this.ResetConversionStream();
					num2 = offset;
					result = offset;
				}
				else
				{
					this.SeekToEndIfNeeded();
					if (this.CurrentPosition + offset > this.StreamLength)
					{
						num2 = this.StreamLength - this.CurrentPosition;
						result = this.StreamLength;
					}
					else
					{
						num2 = offset;
						result = this.CurrentPosition + offset;
					}
				}
				break;
			case StreamSeekOrigin.End:
				if (offset > 0L)
				{
					this.SeekToEndIfNeeded();
					num2 = this.StreamLength - this.CurrentPosition;
					result = this.StreamLength;
				}
				else
				{
					if (this.StreamLength + offset < 0L)
					{
						throw new RopExecutionException("Cannot seek backwards past beginning of stream", (ErrorCode)2147680281U);
					}
					if (this.StreamLength + offset < this.CurrentPosition)
					{
						offset = this.StreamLength + offset;
						this.ResetConversionStream();
						num2 = offset;
						result = offset;
					}
					else
					{
						this.SeekToEndIfNeeded();
						num2 = this.StreamLength + offset - this.CurrentPosition;
						result = this.CurrentPosition + num2;
					}
				}
				break;
			default:
				throw new RopExecutionException("Unknown seek origin cannot seek in a conversion stream", (ErrorCode)2147680261U);
			}
			if (num2 + this.CurrentPosition > this.StreamLength)
			{
				throw new RopExecutionException("Cannot seek beyond end of conversion stream", (ErrorCode)2147680281U);
			}
			if (num2 > 0L)
			{
				this.AdvanceStream(num2);
			}
			return result;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001B1E7 File Offset: 0x000193E7
		public override void SetSize(ulong size)
		{
			base.CheckDisposed();
			throw new RopExecutionException("Cannot change the size of a conversion stream", (ErrorCode)2147680261U);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001B1FE File Offset: 0x000193FE
		public override ushort Write(ArraySegment<byte> data)
		{
			base.CheckDisposed();
			throw new RopExecutionException("Cannot write to a conversion stream", (ErrorCode)2147680261U);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001B218 File Offset: 0x00019418
		public override ulong CopyToStream(Stream destinationStream, ulong bytesToCopy)
		{
			base.CheckDisposed();
			this.SeekToEndIfNeeded();
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

		// Token: 0x06000382 RID: 898 RVA: 0x0001B279 File Offset: 0x00019479
		public override void CheckCanWrite()
		{
			base.CheckDisposed();
			throw new RopExecutionException("Cannot write to a conversion stream", (ErrorCode)2147680261U);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001B290 File Offset: 0x00019490
		internal override void OnAccess()
		{
			base.OnAccess();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001B298 File Offset: 0x00019498
		private void ResetConversionStream()
		{
			Util.DisposeIfPresent(this.conversionStream);
			this.conversionStream = null;
			this.currentPosition = 0L;
			this.seekToEnd = false;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001B2BC File Offset: 0x000194BC
		private void AdvanceStream(long bytesToAdvance)
		{
			if (bytesToAdvance > 0L)
			{
				int num = (int)Math.Min(65535L, bytesToAdvance);
				byte[] array = new byte[num];
				while (bytesToAdvance > 0L)
				{
					ArraySegment<byte> buffer = new ArraySegment<byte>(array);
					buffer = this.FillBuffer(buffer);
					if (buffer.Count == 0)
					{
						return;
					}
					bytesToAdvance -= (long)buffer.Count;
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001B310 File Offset: 0x00019510
		private void SeekToEndIfNeeded()
		{
			if (this.seekToEnd)
			{
				this.AdvanceStream(this.StreamLength);
			}
			this.seekToEnd = false;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001B330 File Offset: 0x00019530
		private ArraySegment<byte> FillBuffer(ArraySegment<byte> buffer)
		{
			int num = 0;
			int count = buffer.Count;
			int num2;
			do
			{
				num2 = this.ConversionStream.Read(buffer.Array, buffer.Offset + num, count - num);
				num += num2;
			}
			while (num2 > 0 && num < count);
			this.currentPosition += (long)num;
			return new ArraySegment<byte>(buffer.Array, buffer.Offset, num);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001B397 File Offset: 0x00019597
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BodyConversionStream>(this);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001B3A0 File Offset: 0x000195A0
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.streamSource);
			if (this.conversionStream != null)
			{
				try
				{
					this.conversionStream.Dispose();
				}
				finally
				{
					this.conversionStream = null;
				}
			}
			base.InternalDispose();
		}

		// Token: 0x04000128 RID: 296
		private const ulong DefaultCopyToStreamSegmentSize = 65535UL;

		// Token: 0x04000129 RID: 297
		private readonly StreamSource streamSource;

		// Token: 0x0400012A RID: 298
		private readonly PropertyTag propertyTag;

		// Token: 0x0400012B RID: 299
		private readonly BodyHelper bodyHelper;

		// Token: 0x0400012C RID: 300
		private readonly Func<PropertyTag, BodyHelper, Stream> conversionStreamFactory;

		// Token: 0x0400012D RID: 301
		private Stream conversionStream;

		// Token: 0x0400012E RID: 302
		private long currentPosition;

		// Token: 0x0400012F RID: 303
		private long? streamLength = null;

		// Token: 0x04000130 RID: 304
		private bool seekToEnd;
	}
}
