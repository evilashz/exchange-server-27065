using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExecuteParams : BaseObject
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00006E70 File Offset: 0x00005070
		public ExecuteParams(WorkBuffer workBuffer)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				using (BufferReader bufferReader = Reader.CreateBufferReader(workBuffer.ArraySegment))
				{
					this.Flags = (int)bufferReader.ReadUInt32();
					int num = (int)bufferReader.ReadUInt32();
					if (num > EmsmdbConstants.MaxChainedExtendedRopBufferSize)
					{
						throw ProtocolException.FromResponseCode((LID)34336, "Maximum ROP output size too large.", ResponseCode.InvalidPayload, null);
					}
					num = Math.Min(num, EmsmdbConstants.MaxChainedExtendedRopBufferSize - 4);
					int count = (int)bufferReader.ReadUInt32();
					this.SegmentExtendedRopIn = bufferReader.ReadArraySegment((uint)count);
					int num2 = (int)bufferReader.ReadUInt32();
					if (num2 > EmsmdbConstants.MaxExtendedAuxBufferSize)
					{
						throw ProtocolException.FromResponseCode((LID)41952, "Maximum AUX output size too large.", ResponseCode.InvalidPayload, null);
					}
					num2 = Math.Min(num2, EmsmdbConstants.MaxExtendedAuxBufferSize - 4);
					int count2 = (int)bufferReader.ReadUInt32();
					this.SegmentExtendedAuxIn = bufferReader.ReadArraySegment((uint)count2);
					this.responseRopOutput = new WorkBuffer(num + 4);
					this.SegmentExtendedRopOut = new ArraySegment<byte>(this.responseRopOutput.ArraySegment.Array, this.responseRopOutput.ArraySegment.Offset + 4, this.responseRopOutput.ArraySegment.Count - 4);
					this.responseAuxOutput = new WorkBuffer(num2 + 4);
					this.SegmentExtendedAuxOut = new ArraySegment<byte>(this.responseAuxOutput.ArraySegment.Array, this.responseAuxOutput.ArraySegment.Offset + 4, this.responseAuxOutput.ArraySegment.Count - 4);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00007050 File Offset: 0x00005250
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00007058 File Offset: 0x00005258
		public int Flags { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00007061 File Offset: 0x00005261
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00007069 File Offset: 0x00005269
		public ArraySegment<byte> SegmentExtendedRopIn { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00007072 File Offset: 0x00005272
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000707A File Offset: 0x0000527A
		public ArraySegment<byte> SegmentExtendedAuxIn { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00007083 File Offset: 0x00005283
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000708B File Offset: 0x0000528B
		public ArraySegment<byte> SegmentExtendedRopOut { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00007094 File Offset: 0x00005294
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000709C File Offset: 0x0000529C
		public ArraySegment<byte> SegmentExtendedAuxOut { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000070A5 File Offset: 0x000052A5
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000070AD File Offset: 0x000052AD
		public uint StatusCode { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000070B6 File Offset: 0x000052B6
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000070BE File Offset: 0x000052BE
		public int ErrorCode { get; private set; }

		// Token: 0x0600011A RID: 282 RVA: 0x000070C7 File Offset: 0x000052C7
		public void SetFailedResponse(uint statusCode)
		{
			base.CheckDisposed();
			this.StatusCode = statusCode;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000070D6 File Offset: 0x000052D6
		public void SetSuccessResponse(int ec, ArraySegment<byte> segmentExtendedRopOut, ArraySegment<byte> segmentExtendedAuxOut)
		{
			base.CheckDisposed();
			this.StatusCode = 0U;
			this.ErrorCode = ec;
			this.SegmentExtendedRopOut = segmentExtendedRopOut;
			this.SegmentExtendedAuxOut = segmentExtendedAuxOut;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000070FC File Offset: 0x000052FC
		public WorkBuffer[] Serialize()
		{
			base.CheckDisposed();
			WorkBuffer workBuffer = null;
			WorkBuffer[] result;
			try
			{
				WorkBuffer[] array;
				if (this.StatusCode != 0U)
				{
					workBuffer = new WorkBuffer(256);
					using (BufferWriter bufferWriter = new BufferWriter(workBuffer.ArraySegment))
					{
						bufferWriter.WriteInt32((int)this.StatusCode);
						workBuffer.Count = (int)bufferWriter.Position;
					}
					array = new WorkBuffer[]
					{
						workBuffer
					};
					workBuffer = null;
				}
				else
				{
					using (BufferWriter bufferWriter2 = new BufferWriter(this.responseRopOutput.ArraySegment))
					{
						bufferWriter2.WriteInt32(this.SegmentExtendedRopOut.Count);
					}
					this.responseRopOutput.Count = this.SegmentExtendedRopOut.Count + 4;
					using (BufferWriter bufferWriter3 = new BufferWriter(this.responseAuxOutput.ArraySegment))
					{
						bufferWriter3.WriteInt32(this.SegmentExtendedAuxOut.Count);
					}
					this.responseAuxOutput.Count = this.SegmentExtendedAuxOut.Count + 4;
					workBuffer = new WorkBuffer(256);
					using (BufferWriter bufferWriter4 = new BufferWriter(workBuffer.ArraySegment))
					{
						bufferWriter4.WriteInt32((int)this.StatusCode);
						bufferWriter4.WriteInt32(this.ErrorCode);
						bufferWriter4.WriteInt32(0);
						bufferWriter4.WriteInt32(Environment.TickCount - this.startTick);
						workBuffer.Count = (int)bufferWriter4.Position;
					}
					array = new WorkBuffer[]
					{
						workBuffer,
						this.responseRopOutput,
						this.responseAuxOutput
					};
					workBuffer = null;
					this.responseRopOutput = null;
					this.responseAuxOutput = null;
				}
				result = array;
			}
			finally
			{
				Util.DisposeIfPresent(workBuffer);
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007338 File Offset: 0x00005538
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.responseRopOutput);
			Util.DisposeIfPresent(this.responseAuxOutput);
			base.InternalDispose();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007356 File Offset: 0x00005556
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExecuteParams>(this);
		}

		// Token: 0x0400008E RID: 142
		private const int BaseResponseSize = 256;

		// Token: 0x0400008F RID: 143
		private readonly int startTick = Environment.TickCount;

		// Token: 0x04000090 RID: 144
		private WorkBuffer responseRopOutput;

		// Token: 0x04000091 RID: 145
		private WorkBuffer responseAuxOutput;
	}
}
