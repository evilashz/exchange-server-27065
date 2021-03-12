using System;
using System.IO;
using Microsoft.Exchange.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001F9 RID: 505
	public sealed class CRCScratchPad : IDisposable
	{
		// Token: 0x0600137F RID: 4991 RVA: 0x00071BA8 File Offset: 0x0006FDA8
		public CRCScratchPad()
		{
			this.memoryStream = new MemoryStream();
			this.memoryWriter = new BinaryWriter(this.memoryStream);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00071BCC File Offset: 0x0006FDCC
		internal void ComputeCRCAndWrite(IWriteToBinaryWriter data, BinaryWriter writer, ref uint crc32)
		{
			this.memoryStream.Seek(0L, SeekOrigin.Begin);
			data.WriteTo(this.memoryWriter);
			int count = (int)this.memoryStream.Position;
			crc32 = OABCRC.ComputeCRC(crc32, this.memoryStream.GetBuffer(), 0, count);
			writer.Write(this.memoryStream.GetBuffer(), 0, count);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00071C2C File Offset: 0x0006FE2C
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.memoryWriter != null)
				{
					this.memoryWriter.Dispose();
					this.memoryWriter = null;
				}
				if (this.memoryStream != null)
				{
					this.memoryStream.Dispose();
					this.memoryStream = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x04000BE4 RID: 3044
		private MemoryStream memoryStream;

		// Token: 0x04000BE5 RID: 3045
		private BinaryWriter memoryWriter;

		// Token: 0x04000BE6 RID: 3046
		private bool disposed;
	}
}
