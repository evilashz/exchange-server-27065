using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DisconnectParams : BaseObject
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002F20 File Offset: 0x00001120
		public DisconnectParams(WorkBuffer workBuffer)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (workBuffer.ArraySegment.Count > 0)
				{
					throw ProtocolException.FromResponseCode((LID)42528, "Disconnect body should be empty.", ResponseCode.InvalidPayload, null);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002F88 File Offset: 0x00001188
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002F90 File Offset: 0x00001190
		public uint StatusCode { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002F99 File Offset: 0x00001199
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002FA1 File Offset: 0x000011A1
		public int ErrorCode { get; private set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002FAA File Offset: 0x000011AA
		public void SetFailedResponse(uint statusCode)
		{
			base.CheckDisposed();
			this.StatusCode = statusCode;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002FB9 File Offset: 0x000011B9
		public void SetSuccessResponse(int ec)
		{
			base.CheckDisposed();
			this.StatusCode = 0U;
			this.ErrorCode = ec;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FD0 File Offset: 0x000011D0
		public WorkBuffer[] Serialize()
		{
			base.CheckDisposed();
			WorkBuffer workBuffer = null;
			WorkBuffer[] result;
			try
			{
				workBuffer = new WorkBuffer(16);
				using (BufferWriter bufferWriter = new BufferWriter(workBuffer.ArraySegment))
				{
					bufferWriter.WriteInt32((int)this.StatusCode);
					if (this.StatusCode == 0U)
					{
						bufferWriter.WriteInt32(this.ErrorCode);
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

		// Token: 0x06000035 RID: 53 RVA: 0x00003070 File Offset: 0x00001270
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DisconnectParams>(this);
		}

		// Token: 0x04000046 RID: 70
		private const int BaseResponseSize = 16;
	}
}
