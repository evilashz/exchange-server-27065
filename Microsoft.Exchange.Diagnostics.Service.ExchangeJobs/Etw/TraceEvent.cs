using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw
{
	// Token: 0x0200001F RID: 31
	public abstract class TraceEvent
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00005BFC File Offset: 0x00003DFC
		internal unsafe TraceEvent(Guid providerGuid, string providerName, EtwTraceNativeComponents.EVENT_RECORD* rawData)
		{
			this.providerGuid = providerGuid;
			this.providerName = providerName;
			this.eventRecord = rawData;
			this.userData = rawData->UserData;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00005C25 File Offset: 0x00003E25
		public unsafe ushort EventID
		{
			get
			{
				return this.eventRecord->EventHeader.Id;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00005C37 File Offset: 0x00003E37
		public Guid ProviderGuid
		{
			get
			{
				return this.providerGuid;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00005C3F File Offset: 0x00003E3F
		public string ProviderName
		{
			get
			{
				if (this.providerName == null)
				{
					this.providerName = "UnknownProvider";
				}
				return this.providerName;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005C5A File Offset: 0x00003E5A
		public unsafe ushort ID
		{
			get
			{
				return this.eventRecord->EventHeader.Id;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00005C6C File Offset: 0x00003E6C
		public unsafe byte Opcode
		{
			get
			{
				return this.eventRecord->EventHeader.Opcode;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005C7E File Offset: 0x00003E7E
		public unsafe int Version
		{
			get
			{
				return (int)this.eventRecord->EventHeader.Version;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005C90 File Offset: 0x00003E90
		public unsafe int ThreadID
		{
			get
			{
				return this.eventRecord->EventHeader.ThreadId;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005CB0 File Offset: 0x00003EB0
		public unsafe virtual int ProcessID
		{
			get
			{
				return this.eventRecord->EventHeader.ProcessId;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005CCF File Offset: 0x00003ECF
		public unsafe int PointerSize
		{
			get
			{
				if ((this.eventRecord->EventHeader.Flags & 64) == 0)
				{
					return 4;
				}
				return 8;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005CE9 File Offset: 0x00003EE9
		public unsafe Guid ActivityID
		{
			get
			{
				return this.eventRecord->EventHeader.ActivityId;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005CFB File Offset: 0x00003EFB
		public unsafe int EventDataLength
		{
			get
			{
				return (int)this.eventRecord->UserDataLength;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00005D08 File Offset: 0x00003F08
		internal IntPtr DataStart
		{
			get
			{
				return this.userData;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005D10 File Offset: 0x00003F10
		internal unsafe bool IsClassicProvider
		{
			get
			{
				return (this.eventRecord->EventHeader.Flags & 256) != 0;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005D2E File Offset: 0x00003F2E
		internal byte[] EventData()
		{
			return this.EventData(null, 0, 0, this.EventDataLength);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005D40 File Offset: 0x00003F40
		internal unsafe byte[] EventData(byte[] targetBuffer, int targetStartIndex, int sourceStartIndex, int length)
		{
			if (targetBuffer == null)
			{
				targetBuffer = new byte[length + targetStartIndex];
			}
			if (sourceStartIndex + length > this.EventDataLength)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr source = (IntPtr)((void*)((byte*)this.DataStart.ToPointer() + sourceStartIndex));
			if (length > 0)
			{
				Marshal.Copy(source, targetBuffer, targetStartIndex, length);
			}
			return targetBuffer;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005D93 File Offset: 0x00003F93
		internal int HostSizePtr(int numPointers)
		{
			return this.PointerSize * numPointers;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005D9D File Offset: 0x00003F9D
		internal int GetByteAt(int offset)
		{
			return (int)TraceEvent.RawReaderUtils.ReadByte(this.DataStart, offset);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005DAB File Offset: 0x00003FAB
		internal int GetInt32At(int offset)
		{
			return TraceEvent.RawReaderUtils.ReadInt32(this.DataStart, offset);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005DB9 File Offset: 0x00003FB9
		internal long GetInt64At(int offset)
		{
			return TraceEvent.RawReaderUtils.ReadInt64(this.DataStart, offset);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005DC7 File Offset: 0x00003FC7
		internal long GetIntPtrAt(int offset)
		{
			if (this.PointerSize == 4)
			{
				return (long)((ulong)this.GetInt32At(offset));
			}
			return this.GetInt64At(offset);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005DE2 File Offset: 0x00003FE2
		internal virtual void Parse()
		{
		}

		// Token: 0x040000FA RID: 250
		private readonly Guid providerGuid;

		// Token: 0x040000FB RID: 251
		private unsafe readonly EtwTraceNativeComponents.EVENT_RECORD* eventRecord;

		// Token: 0x040000FC RID: 252
		private readonly IntPtr userData;

		// Token: 0x040000FD RID: 253
		private string providerName;

		// Token: 0x02000020 RID: 32
		internal sealed class RawReaderUtils
		{
			// Token: 0x0600008D RID: 141 RVA: 0x00005DE4 File Offset: 0x00003FE4
			internal unsafe static long ReadInt64(IntPtr pointer, int offset)
			{
				return *(long*)((byte*)pointer.ToPointer() + offset);
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00005DF0 File Offset: 0x00003FF0
			internal unsafe static int ReadInt32(IntPtr pointer, int offset)
			{
				return *(int*)((byte*)pointer.ToPointer() + offset);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00005DFC File Offset: 0x00003FFC
			internal unsafe static short ReadInt16(IntPtr pointer, int offset)
			{
				return *(short*)((byte*)pointer.ToPointer() + offset);
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00005E08 File Offset: 0x00004008
			internal unsafe static byte ReadByte(IntPtr pointer, int offset)
			{
				return ((byte*)pointer.ToPointer())[offset];
			}
		}
	}
}
