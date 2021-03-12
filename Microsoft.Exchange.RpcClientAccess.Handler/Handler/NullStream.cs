using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000088 RID: 136
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NullStream : Stream
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00026EC6 File Offset: 0x000250C6
		internal NullStream(Logon logon) : base(logon)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00026ECF File Offset: 0x000250CF
		public override void Commit()
		{
			base.CheckDisposed();
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00026ED7 File Offset: 0x000250D7
		public override uint GetSize()
		{
			base.CheckDisposed();
			return 0U;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00026EE0 File Offset: 0x000250E0
		public override ArraySegment<byte> Read(ushort requestedSize)
		{
			base.CheckDisposed();
			return Array<byte>.EmptySegment;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00026EED File Offset: 0x000250ED
		public override long Seek(StreamSeekOrigin streamSeekOrigin, long offset)
		{
			base.CheckDisposed();
			return 0L;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00026EF7 File Offset: 0x000250F7
		public override void SetSize(ulong size)
		{
			base.CheckDisposed();
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00026EFF File Offset: 0x000250FF
		public override ushort Write(ArraySegment<byte> data)
		{
			base.CheckDisposed();
			return (ushort)data.Count;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00026F0F File Offset: 0x0002510F
		public override ulong CopyToStream(Stream destinationStream, ulong bytesToCopy)
		{
			base.CheckDisposed();
			return 0UL;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00026F19 File Offset: 0x00025119
		public override void CheckCanWrite()
		{
			base.CheckDisposed();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00026F21 File Offset: 0x00025121
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NullStream>(this);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00026F29 File Offset: 0x00025129
		protected override void InternalDispose()
		{
			base.InternalDispose();
		}
	}
}
