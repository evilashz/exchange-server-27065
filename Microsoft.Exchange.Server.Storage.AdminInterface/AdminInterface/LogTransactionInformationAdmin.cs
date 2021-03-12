using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000062 RID: 98
	internal class LogTransactionInformationAdmin : ILogTransactionInformation
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public LogTransactionInformationAdmin()
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public LogTransactionInformationAdmin(AdminMethod methodId)
		{
			this.methodId = methodId;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000EEDB File Offset: 0x0000D0DB
		public AdminMethod MethodId
		{
			get
			{
				return this.methodId;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000EEE3 File Offset: 0x0000D0E3
		public override string ToString()
		{
			return string.Format("Admin RPC:\nIdentifier: {0}\n", this.methodId);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000EEFA File Offset: 0x0000D0FA
		public byte Type()
		{
			return 3;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000EF00 File Offset: 0x0000D100
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
				buffer[offset] = (byte)this.methodId;
			}
			offset++;
			return offset - num;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000EF34 File Offset: 0x0000D134
		public void Parse(byte[] buffer, ref int offset)
		{
			byte b = buffer[offset++];
			this.methodId = (AdminMethod)buffer[offset++];
		}

		// Token: 0x04000204 RID: 516
		private AdminMethod methodId;
	}
}
