using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NotificationWaitParams : BaseObject
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00007360 File Offset: 0x00005560
		public NotificationWaitParams(WorkBuffer workBuffer)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				using (BufferReader bufferReader = Reader.CreateBufferReader(workBuffer.ArraySegment))
				{
					this.Flags = (int)bufferReader.ReadUInt32();
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000073D4 File Offset: 0x000055D4
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000073DC File Offset: 0x000055DC
		public int Flags { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000073E5 File Offset: 0x000055E5
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000073ED File Offset: 0x000055ED
		public uint StatusCode { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000073F6 File Offset: 0x000055F6
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000073FE File Offset: 0x000055FE
		public int ErrorCode { get; private set; }

		// Token: 0x06000126 RID: 294 RVA: 0x00007407 File Offset: 0x00005607
		public void SetFailedResponse(uint statusCode)
		{
			base.CheckDisposed();
			this.StatusCode = statusCode;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007416 File Offset: 0x00005616
		public void SetSuccessResponse(int ec, int flags)
		{
			base.CheckDisposed();
			this.StatusCode = 0U;
			this.ErrorCode = ec;
			this.Flags = flags;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007434 File Offset: 0x00005634
		public WorkBuffer[] Serialize()
		{
			base.CheckDisposed();
			WorkBuffer workBuffer = null;
			WorkBuffer[] result;
			try
			{
				workBuffer = new WorkBuffer(256);
				using (BufferWriter bufferWriter = new BufferWriter(workBuffer.ArraySegment))
				{
					bufferWriter.WriteInt32((int)this.StatusCode);
					if (this.StatusCode == 0U)
					{
						bufferWriter.WriteInt32(this.ErrorCode);
						bufferWriter.WriteInt32(this.Flags);
					}
					workBuffer.Count = (int)bufferWriter.Position;
				}
				WorkBuffer[] array = new WorkBuffer[]
				{
					workBuffer
				};
				workBuffer = null;
				result = array;
			}
			finally
			{
				Util.DisposeIfPresent(workBuffer);
			}
			return result;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000074E4 File Offset: 0x000056E4
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationWaitParams>(this);
		}

		// Token: 0x04000099 RID: 153
		private const int BaseResponseSize = 256;
	}
}
