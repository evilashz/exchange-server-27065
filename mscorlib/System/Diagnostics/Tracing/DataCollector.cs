using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000410 RID: 1040
	[SecurityCritical]
	internal struct DataCollector
	{
		// Token: 0x060034C2 RID: 13506 RVA: 0x000CD3B4 File Offset: 0x000CB5B4
		internal unsafe void Enable(byte* scratch, int scratchSize, EventSource.EventData* datas, int dataCount, GCHandle* pins, int pinCount)
		{
			this.datasStart = datas;
			this.scratchEnd = scratch + scratchSize;
			this.datasEnd = datas + dataCount;
			this.pinsEnd = pins + pinCount;
			this.scratch = scratch;
			this.datas = datas;
			this.pins = pins;
			this.writingScalars = false;
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000CD413 File Offset: 0x000CB613
		internal void Disable()
		{
			this = default(DataCollector);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000CD41C File Offset: 0x000CB61C
		internal unsafe EventSource.EventData* Finish()
		{
			this.ScalarsEnd();
			return this.datas;
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000CD42C File Offset: 0x000CB62C
		internal unsafe void AddScalar(void* value, int size)
		{
			if (this.bufferNesting != 0)
			{
				int num = this.bufferPos;
				int num2;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
					num2 = 0;
				}
				while (num2 != size)
				{
					this.buffer[num] = ((byte*)value)[num2];
					num2++;
					num++;
				}
				return;
			}
			byte* ptr = this.scratch;
			byte* ptr2 = ptr + size;
			if (this.scratchEnd < ptr2)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_AddScalarOutOfRange"));
			}
			this.ScalarsBegin();
			this.scratch = ptr2;
			for (int num3 = 0; num3 != size; num3++)
			{
				ptr[num3] = ((byte*)value)[num3];
			}
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000CD4CC File Offset: 0x000CB6CC
		internal unsafe void AddBinary(string value, int size)
		{
			if (size > 65535)
			{
				size = 65534;
			}
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(size + 2);
			}
			this.AddScalar((void*)(&size), 2);
			if (size != 0)
			{
				if (this.bufferNesting == 0)
				{
					this.ScalarsEnd();
					this.PinArray(value, size);
					return;
				}
				int startIndex = this.bufferPos;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
				}
				fixed (string text = value)
				{
					void* ptr = text;
					if (ptr != null)
					{
						ptr = (void*)((byte*)ptr + RuntimeHelpers.OffsetToStringData);
					}
					Marshal.Copy((IntPtr)ptr, this.buffer, startIndex, size);
				}
			}
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000CD55D File Offset: 0x000CB75D
		internal void AddBinary(Array value, int size)
		{
			this.AddArray(value, size, 1);
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000CD568 File Offset: 0x000CB768
		internal unsafe void AddArray(Array value, int length, int itemSize)
		{
			if (length > 65535)
			{
				length = 65535;
			}
			int num = length * itemSize;
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(num + 2);
			}
			this.AddScalar((void*)(&length), 2);
			checked
			{
				if (length != 0)
				{
					if (this.bufferNesting == 0)
					{
						this.ScalarsEnd();
						this.PinArray(value, num);
						return;
					}
					int dstOffset = this.bufferPos;
					this.bufferPos += num;
					this.EnsureBuffer();
					Buffer.BlockCopy(value, 0, this.buffer, dstOffset, num);
				}
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000CD5E7 File Offset: 0x000CB7E7
		internal int BeginBufferedArray()
		{
			this.BeginBuffered();
			this.bufferPos += 2;
			return this.bufferPos;
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000CD603 File Offset: 0x000CB803
		internal void EndBufferedArray(int bookmark, int count)
		{
			this.EnsureBuffer();
			this.buffer[bookmark - 2] = (byte)count;
			this.buffer[bookmark - 1] = (byte)(count >> 8);
			this.EndBuffered();
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000CD62B File Offset: 0x000CB82B
		internal void BeginBuffered()
		{
			this.ScalarsEnd();
			this.bufferNesting++;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000CD641 File Offset: 0x000CB841
		internal void EndBuffered()
		{
			this.bufferNesting--;
			if (this.bufferNesting == 0)
			{
				this.EnsureBuffer();
				this.PinArray(this.buffer, this.bufferPos);
				this.buffer = null;
				this.bufferPos = 0;
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000CD680 File Offset: 0x000CB880
		private void EnsureBuffer()
		{
			int num = this.bufferPos;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000CD6B0 File Offset: 0x000CB8B0
		private void EnsureBuffer(int additionalSize)
		{
			int num = this.bufferPos + additionalSize;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000CD6E0 File Offset: 0x000CB8E0
		private void GrowBuffer(int required)
		{
			int num = (this.buffer == null) ? 64 : this.buffer.Length;
			do
			{
				num *= 2;
			}
			while (num < required);
			Array.Resize<byte>(ref this.buffer, num);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000CD718 File Offset: 0x000CB918
		private unsafe void PinArray(object value, int size)
		{
			GCHandle* ptr = this.pins;
			if (this.pinsEnd == ptr)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_PinArrayOutOfRange"));
			}
			EventSource.EventData* ptr2 = this.datas;
			if (this.datasEnd == ptr2)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange"));
			}
			this.pins = ptr + 1;
			this.datas = ptr2 + 1;
			*ptr = GCHandle.Alloc(value, GCHandleType.Pinned);
			ptr2->DataPointer = ptr->AddrOfPinnedObject();
			ptr2->m_Size = size;
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000CD7A4 File Offset: 0x000CB9A4
		private unsafe void ScalarsBegin()
		{
			if (!this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				if (this.datasEnd == ptr)
				{
					throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange"));
				}
				ptr->DataPointer = (IntPtr)((void*)this.scratch);
				this.writingScalars = true;
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000CD7F4 File Offset: 0x000CB9F4
		private unsafe void ScalarsEnd()
		{
			if (this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				ptr->m_Size = (this.scratch - checked((UIntPtr)ptr->m_Ptr)) / 1;
				this.datas = ptr + 1;
				this.writingScalars = false;
			}
		}

		// Token: 0x04001754 RID: 5972
		[ThreadStatic]
		internal static DataCollector ThreadInstance;

		// Token: 0x04001755 RID: 5973
		private unsafe byte* scratchEnd;

		// Token: 0x04001756 RID: 5974
		private unsafe EventSource.EventData* datasEnd;

		// Token: 0x04001757 RID: 5975
		private unsafe GCHandle* pinsEnd;

		// Token: 0x04001758 RID: 5976
		private unsafe EventSource.EventData* datasStart;

		// Token: 0x04001759 RID: 5977
		private unsafe byte* scratch;

		// Token: 0x0400175A RID: 5978
		private unsafe EventSource.EventData* datas;

		// Token: 0x0400175B RID: 5979
		private unsafe GCHandle* pins;

		// Token: 0x0400175C RID: 5980
		private byte[] buffer;

		// Token: 0x0400175D RID: 5981
		private int bufferPos;

		// Token: 0x0400175E RID: 5982
		private int bufferNesting;

		// Token: 0x0400175F RID: 5983
		private bool writingScalars;
	}
}
