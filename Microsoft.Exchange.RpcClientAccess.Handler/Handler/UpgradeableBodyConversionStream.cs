using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000090 RID: 144
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UpgradeableBodyConversionStream : Stream
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00028810 File Offset: 0x00026A10
		internal UpgradeableBodyConversionStream(Logon logon, StreamSource streamSource, PropertyTag propertyTag, bool isAppend, Encoding string8Encoding, BodyHelper bodyHelper, Func<Logon, StreamSource, PropertyTag, bool, BodyHelper, BodyConversionStream> bodyConversionStreamFactory, Func<Logon, StreamSource, PropertyTag, Encoding, PropertyStream> propertyStreamFactory) : base(logon)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(streamSource, "streamSource");
				if (!propertyTag.IsBodyProperty())
				{
					throw new ArgumentException("PropertyTag must be a body property.");
				}
				this.streamSource = streamSource;
				this.propertyTag = propertyTag;
				this.isAppend = isAppend;
				this.string8Encoding = string8Encoding;
				this.bodyHelper = bodyHelper;
				this.bodyConversionStreamFactory = bodyConversionStreamFactory;
				this.propertyStreamFactory = propertyStreamFactory;
				disposeGuard.Success();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000288B4 File Offset: 0x00026AB4
		private BodyConversionStream BodyConversionStream
		{
			get
			{
				if (this.UsePropertyStream)
				{
					throw new InvalidOperationException("Should have detected propertyStream exists and not have called BodyConversionStream");
				}
				if (this.bodyConversionStream == null)
				{
					this.bodyConversionStream = this.bodyConversionStreamFactory(base.LogonObject, this.streamSource, this.propertyTag, this.isAppend, this.bodyHelper);
				}
				return this.bodyConversionStream;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00028914 File Offset: 0x00026B14
		private PropertyStream PropertyStream
		{
			get
			{
				if (this.propertyStream == null)
				{
					long streamLength = this.BodyConversionStream.StreamLength;
					long currentPosition = this.BodyConversionStream.CurrentPosition;
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						PropertyStream propertyStream = this.propertyStreamFactory(base.LogonObject, this.streamSource, this.propertyTag, this.string8Encoding);
						disposeGuard.Add<PropertyStream>(propertyStream);
						if (streamLength > 0L)
						{
							if (currentPosition > 0L)
							{
								this.BodyConversionStream.Seek(StreamSeekOrigin.Begin, 0L);
							}
							this.BodyConversionStream.CopyToStream(propertyStream, (ulong)streamLength);
							propertyStream.Seek(StreamSeekOrigin.Begin, currentPosition);
						}
						this.propertyStream = propertyStream;
						disposeGuard.Success();
					}
					this.bodyHelper.OnBeforeWrite(this.propertyTag);
					Util.DisposeIfPresent(this.bodyConversionStream);
					this.bodyConversionStream = null;
				}
				return this.propertyStream;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00028A08 File Offset: 0x00026C08
		private bool UsePropertyStream
		{
			get
			{
				return this.propertyStream != null;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00028A16 File Offset: 0x00026C16
		public override void Commit()
		{
			base.CheckDisposed();
			if (this.UsePropertyStream)
			{
				this.PropertyStream.Commit();
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00028A34 File Offset: 0x00026C34
		public override uint GetSize()
		{
			base.CheckDisposed();
			if (this.UsePropertyStream)
			{
				return this.PropertyStream.GetSize();
			}
			if (this.bodyConversionStreamSize == null)
			{
				this.bodyConversionStreamSize = new uint?(this.BodyConversionStream.GetSize());
			}
			return this.bodyConversionStreamSize.Value;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00028A89 File Offset: 0x00026C89
		public override ArraySegment<byte> Read(ushort requestedSize)
		{
			base.CheckDisposed();
			if (this.UsePropertyStream)
			{
				return this.PropertyStream.Read(requestedSize);
			}
			return this.BodyConversionStream.Read(requestedSize);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00028AB4 File Offset: 0x00026CB4
		public override long Seek(StreamSeekOrigin streamSeekOrigin, long offset)
		{
			base.CheckDisposed();
			if (this.UsePropertyStream)
			{
				return this.PropertyStream.Seek(streamSeekOrigin, offset);
			}
			if (streamSeekOrigin == StreamSeekOrigin.Begin && offset == 0L)
			{
				Util.DisposeIfPresent(this.bodyConversionStream);
				this.bodyConversionStream = null;
				return 0L;
			}
			if (offset > 0L && ((streamSeekOrigin == StreamSeekOrigin.Begin && offset > this.BodyConversionStream.StreamLength) || (streamSeekOrigin == StreamSeekOrigin.Current && this.BodyConversionStream.CurrentPosition > this.BodyConversionStream.StreamLength - offset) || streamSeekOrigin == StreamSeekOrigin.End))
			{
				return this.PropertyStream.Seek(streamSeekOrigin, offset);
			}
			return this.BodyConversionStream.Seek(streamSeekOrigin, offset);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00028B4D File Offset: 0x00026D4D
		public override void SetSize(ulong size)
		{
			base.CheckDisposed();
			this.PropertyStream.SetSize(size);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00028B61 File Offset: 0x00026D61
		public override ushort Write(ArraySegment<byte> data)
		{
			base.CheckDisposed();
			return this.PropertyStream.Write(data);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00028B75 File Offset: 0x00026D75
		public override ulong CopyToStream(Stream destinationStream, ulong bytesToCopy)
		{
			base.CheckDisposed();
			if (this.UsePropertyStream)
			{
				return this.PropertyStream.CopyToStream(destinationStream, bytesToCopy);
			}
			return this.BodyConversionStream.CopyToStream(destinationStream, bytesToCopy);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00028BA0 File Offset: 0x00026DA0
		public override void CheckCanWrite()
		{
			base.CheckDisposed();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00028BA8 File Offset: 0x00026DA8
		internal override void OnAccess()
		{
			if (this.UsePropertyStream)
			{
				this.PropertyStream.OnAccess();
			}
			else if (this.bodyConversionStream != null)
			{
				this.bodyConversionStream.OnAccess();
			}
			base.OnAccess();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00028BD8 File Offset: 0x00026DD8
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UpgradeableBodyConversionStream>(this);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00028BE0 File Offset: 0x00026DE0
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.streamSource);
			if (this.bodyConversionStream != null)
			{
				try
				{
					this.bodyConversionStream.Dispose();
				}
				finally
				{
					this.bodyConversionStream = null;
				}
			}
			if (this.propertyStream != null)
			{
				try
				{
					this.propertyStream.Dispose();
				}
				finally
				{
					this.propertyStream = null;
				}
			}
			base.InternalDispose();
		}

		// Token: 0x04000269 RID: 617
		private const ulong DefaultCopyToStreamSegmentSize = 65535UL;

		// Token: 0x0400026A RID: 618
		private readonly StreamSource streamSource;

		// Token: 0x0400026B RID: 619
		private readonly PropertyTag propertyTag;

		// Token: 0x0400026C RID: 620
		private readonly bool isAppend;

		// Token: 0x0400026D RID: 621
		private readonly Encoding string8Encoding;

		// Token: 0x0400026E RID: 622
		private readonly BodyHelper bodyHelper;

		// Token: 0x0400026F RID: 623
		private readonly Func<Logon, StreamSource, PropertyTag, bool, BodyHelper, BodyConversionStream> bodyConversionStreamFactory;

		// Token: 0x04000270 RID: 624
		private readonly Func<Logon, StreamSource, PropertyTag, Encoding, PropertyStream> propertyStreamFactory;

		// Token: 0x04000271 RID: 625
		private BodyConversionStream bodyConversionStream;

		// Token: 0x04000272 RID: 626
		private uint? bodyConversionStreamSize = null;

		// Token: 0x04000273 RID: 627
		private PropertyStream propertyStream;
	}
}
