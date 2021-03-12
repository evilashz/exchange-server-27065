using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000279 RID: 633
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PendingResult : Result
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0002980C File Offset: 0x00027A0C
		internal PendingResult(ushort sessionId, Encoding string8Encoding) : base(RopId.Pending)
		{
			this.SessionId = sessionId;
			base.String8Encoding = string8Encoding;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00029824 File Offset: 0x00027A24
		internal PendingResult(Reader reader) : base(reader)
		{
			this.SessionId = reader.ReadUInt16();
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00029839 File Offset: 0x00027A39
		internal static int Size
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0002983C File Offset: 0x00027A3C
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00029844 File Offset: 0x00027A44
		internal ushort SessionId { get; private set; }

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002984D File Offset: 0x00027A4D
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.SessionId);
		}
	}
}
