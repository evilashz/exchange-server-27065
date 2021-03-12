using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConnectParams : BaseObject
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000296C File Offset: 0x00000B6C
		public ConnectParams(WorkBuffer workBuffer)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				short[] array = new short[3];
				using (BufferReader bufferReader = Reader.CreateBufferReader(workBuffer.ArraySegment))
				{
					this.UserDn = bufferReader.ReadAsciiString(StringFlags.IncludeNull | StringFlags.Sized16);
					this.Flags = (int)bufferReader.ReadUInt32();
					this.ConnectionModulus = (int)bufferReader.ReadUInt32();
					this.CodePage = (int)bufferReader.ReadUInt32();
					this.StringLcid = (int)bufferReader.ReadUInt32();
					this.SortLcid = (int)bufferReader.ReadUInt32();
					array[0] = (short)bufferReader.ReadUInt16();
					array[1] = (short)bufferReader.ReadUInt16();
					array[2] = (short)bufferReader.ReadUInt16();
					short[] array2 = new short[4];
					MapiVersionConversion.Normalize(array, array2);
					this.ClientVersion = array2;
					int num = (int)bufferReader.ReadUInt32();
					if (num > EmsmdbConstants.MaxExtendedAuxBufferSize)
					{
						throw ProtocolException.FromResponseCode((LID)41952, string.Format("Maximum AUX output size too large; maximum={0}.", EmsmdbConstants.MaxExtendedAuxBufferSize), ResponseCode.InvalidPayload, null);
					}
					num = Math.Min(num, EmsmdbConstants.MaxExtendedAuxBufferSize - 4);
					int count = (int)bufferReader.ReadUInt32();
					this.SegmentExtendedAuxIn = bufferReader.ReadArraySegment((uint)count);
					this.responseAuxOutput = new WorkBuffer(num + 4);
					this.SegmentExtendedAuxOut = new ArraySegment<byte>(this.responseAuxOutput.ArraySegment.Array, this.responseAuxOutput.ArraySegment.Offset + 4, this.responseAuxOutput.ArraySegment.Count - 4);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002B2C File Offset: 0x00000D2C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002B34 File Offset: 0x00000D34
		public string UserDn { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002B3D File Offset: 0x00000D3D
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002B45 File Offset: 0x00000D45
		public int Flags { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002B4E File Offset: 0x00000D4E
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002B56 File Offset: 0x00000D56
		public int ConnectionModulus { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002B5F File Offset: 0x00000D5F
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002B67 File Offset: 0x00000D67
		public int CodePage { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002B70 File Offset: 0x00000D70
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002B78 File Offset: 0x00000D78
		public int StringLcid { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002B81 File Offset: 0x00000D81
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002B89 File Offset: 0x00000D89
		public int SortLcid { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002B92 File Offset: 0x00000D92
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002B9A File Offset: 0x00000D9A
		public short[] ClientVersion { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002BA3 File Offset: 0x00000DA3
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002BAB File Offset: 0x00000DAB
		public ArraySegment<byte> SegmentExtendedAuxIn { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002BB4 File Offset: 0x00000DB4
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002BBC File Offset: 0x00000DBC
		public ArraySegment<byte> SegmentExtendedAuxOut { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002BC5 File Offset: 0x00000DC5
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002BCD File Offset: 0x00000DCD
		public uint StatusCode { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002BD6 File Offset: 0x00000DD6
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002BDE File Offset: 0x00000DDE
		public int ErrorCode { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002BE7 File Offset: 0x00000DE7
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002BEF File Offset: 0x00000DEF
		public TimeSpan PollsMax { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002BF8 File Offset: 0x00000DF8
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002C00 File Offset: 0x00000E00
		public int RetryCount { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002C09 File Offset: 0x00000E09
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002C11 File Offset: 0x00000E11
		public TimeSpan RetryDelay { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002C1A File Offset: 0x00000E1A
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002C22 File Offset: 0x00000E22
		public string DnPrefix { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C2B File Offset: 0x00000E2B
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002C33 File Offset: 0x00000E33
		public string DisplayName { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002C44 File Offset: 0x00000E44
		public short[] ServerVersion { get; private set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00002C4D File Offset: 0x00000E4D
		public void SetFailedResponse(uint statusCode)
		{
			base.CheckDisposed();
			this.StatusCode = statusCode;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void SetSuccessResponse(int ec, TimeSpan pollsMax, int retryCount, TimeSpan retryDelay, string dnPrefix, string displayName, short[] serverVersion, ArraySegment<byte> segmentExtendedAuxOut)
		{
			base.CheckDisposed();
			this.StatusCode = 0U;
			this.ErrorCode = ec;
			this.PollsMax = pollsMax;
			this.RetryCount = retryCount;
			this.RetryDelay = retryDelay;
			this.DnPrefix = dnPrefix;
			this.DisplayName = displayName;
			this.ServerVersion = serverVersion;
			this.SegmentExtendedAuxOut = segmentExtendedAuxOut;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CB4 File Offset: 0x00000EB4
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
					short[] array2 = new short[3];
					MapiVersionConversion.Legacy(this.ServerVersion, array2, 4000);
					using (BufferWriter bufferWriter2 = new BufferWriter(this.responseAuxOutput.ArraySegment))
					{
						bufferWriter2.WriteInt32(this.SegmentExtendedAuxOut.Count);
					}
					this.responseAuxOutput.Count = this.SegmentExtendedAuxOut.Count + 4;
					workBuffer = new WorkBuffer((this.DnPrefix.Length + this.DisplayName.Length) * 2 + 256);
					using (BufferWriter bufferWriter3 = new BufferWriter(workBuffer.ArraySegment))
					{
						bufferWriter3.WriteInt32((int)this.StatusCode);
						bufferWriter3.WriteInt32(this.ErrorCode);
						bufferWriter3.WriteInt32((int)this.PollsMax.TotalMilliseconds);
						bufferWriter3.WriteInt32(this.RetryCount);
						bufferWriter3.WriteInt32((int)this.RetryDelay.TotalMilliseconds);
						bufferWriter3.WriteAsciiString(this.DnPrefix, StringFlags.IncludeNull | StringFlags.Sized16);
						bufferWriter3.WriteAsciiString(this.DisplayName, StringFlags.IncludeNull | StringFlags.Sized16);
						bufferWriter3.WriteInt16(array2[0]);
						bufferWriter3.WriteInt16(array2[1]);
						bufferWriter3.WriteInt16(array2[2]);
						workBuffer.Count = (int)bufferWriter3.Position;
					}
					array = new WorkBuffer[]
					{
						workBuffer,
						this.responseAuxOutput
					};
					workBuffer = null;
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

		// Token: 0x0600002B RID: 43 RVA: 0x00002F04 File Offset: 0x00001104
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.responseAuxOutput);
			base.InternalDispose();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F17 File Offset: 0x00001117
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectParams>(this);
		}

		// Token: 0x04000033 RID: 51
		private const int BaseResponseSize = 256;

		// Token: 0x04000034 RID: 52
		private WorkBuffer responseAuxOutput;
	}
}
