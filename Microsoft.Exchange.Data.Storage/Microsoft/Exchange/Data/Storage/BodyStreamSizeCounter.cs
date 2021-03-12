using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A9 RID: 1449
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyStreamSizeCounter : StreamWrapper
	{
		// Token: 0x06003B77 RID: 15223 RVA: 0x000F4C23 File Offset: 0x000F2E23
		internal BodyStreamSizeCounter(Stream stream) : base(stream, true, StreamBase.Capabilities.Writable)
		{
			this.byteCount = 0;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000F4C35 File Offset: 0x000F2E35
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (base.InternalStream != null)
			{
				base.InternalStream.Write(buffer, offset, count);
			}
			this.byteCount += count;
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000F4C5B File Offset: 0x000F2E5B
		public override void Flush()
		{
			if (base.InternalStream != null)
			{
				base.InternalStream.Flush();
			}
		}

		// Token: 0x04001FB7 RID: 8119
		private int byteCount;
	}
}
