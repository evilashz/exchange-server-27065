using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AddressEntryId
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal AddressEntryId(byte[] entryId)
		{
			this.entryId = entryId;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public static AddressEntryId Parse(Reader reader, Encoding string8Encoding, uint sizeEntry)
		{
			return new AddressEntryId(reader.ReadBytes(sizeEntry));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020ED File Offset: 0x000002ED
		public virtual void Serialize(Writer writer)
		{
			writer.WriteBytes(this.entryId);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FB File Offset: 0x000002FB
		public virtual void SetUnicode()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FD File Offset: 0x000002FD
		public virtual void SetString8(Encoding string8Encoding)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FF File Offset: 0x000002FF
		internal static byte[] ToBytes(BufferWriter.SerializeDelegate serialize)
		{
			return BufferWriter.Serialize(serialize);
		}

		// Token: 0x04000001 RID: 1
		private readonly byte[] entryId;
	}
}
