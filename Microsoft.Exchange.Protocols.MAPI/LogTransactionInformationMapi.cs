using System;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200000A RID: 10
	internal class LogTransactionInformationMapi : ILogTransactionInformation
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002E51 File Offset: 0x00001051
		public LogTransactionInformationMapi()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E59 File Offset: 0x00001059
		public LogTransactionInformationMapi(RopId ropId)
		{
			this.ropId = ropId;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002E68 File Offset: 0x00001068
		public RopId RopId
		{
			get
			{
				return this.ropId;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E70 File Offset: 0x00001070
		public override string ToString()
		{
			return string.Format("MAPI RPC:\nIdentifier: {0}\n", this.ropId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E87 File Offset: 0x00001087
		public byte Type()
		{
			return 4;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E8C File Offset: 0x0000108C
		public int Serialize(byte[] buffer, int offset)
		{
			int num = offset;
			if (buffer != null)
			{
				buffer[offset] = this.Type();
			}
			offset++;
			if (buffer != null)
			{
				buffer[offset] = (byte)this.ropId;
			}
			offset++;
			return offset - num;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002EC0 File Offset: 0x000010C0
		public void Parse(byte[] buffer, ref int offset)
		{
			byte b = buffer[offset++];
			this.ropId = (RopId)buffer[offset++];
		}

		// Token: 0x04000041 RID: 65
		private RopId ropId;
	}
}
