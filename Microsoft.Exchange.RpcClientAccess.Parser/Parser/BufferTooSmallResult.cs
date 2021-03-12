using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000225 RID: 549
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BufferTooSmallResult : Result
	{
		// Token: 0x06000C05 RID: 3077 RVA: 0x0002693D File Offset: 0x00024B3D
		internal BufferTooSmallResult(ushort sizeNeeded, ArraySegment<byte> unexecutedBuffer, Encoding string8Encoding) : base(RopId.BufferTooSmall)
		{
			this.SizeNeeded = sizeNeeded;
			this.UnexecutedBuffer = unexecutedBuffer;
			base.String8Encoding = string8Encoding;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002695F File Offset: 0x00024B5F
		internal static int HeaderSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00026962 File Offset: 0x00024B62
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002696A File Offset: 0x00024B6A
		internal ushort SizeNeeded { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00026973 File Offset: 0x00024B73
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0002697B File Offset: 0x00024B7B
		internal ArraySegment<byte> UnexecutedBuffer { get; private set; }

		// Token: 0x06000C0B RID: 3083 RVA: 0x00026984 File Offset: 0x00024B84
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.SizeNeeded);
			writer.WriteBytesSegment(this.UnexecutedBuffer);
		}
	}
}
