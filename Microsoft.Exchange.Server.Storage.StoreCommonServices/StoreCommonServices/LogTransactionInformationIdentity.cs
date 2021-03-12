using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000098 RID: 152
	internal class LogTransactionInformationIdentity : ILogTransactionInformation
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0001E4AB File Offset: 0x0001C6AB
		public LogTransactionInformationIdentity()
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001E4BB File Offset: 0x0001C6BB
		public LogTransactionInformationIdentity(int mailboxNumber, ClientType clientType)
		{
			this.mailboxNumber = mailboxNumber;
			this.clientType = clientType;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001E4D9 File Offset: 0x0001C6D9
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001E4E1 File Offset: 0x0001C6E1
		public ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001E4E9 File Offset: 0x0001C6E9
		public override string ToString()
		{
			return string.Format("Identity block:\nmailbox number: {0}\nclientType: {1}\n", this.mailboxNumber, this.clientType);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001E50B File Offset: 0x0001C70B
		public byte Type()
		{
			return 2;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001E510 File Offset: 0x0001C710
		public int Serialize(byte[] buffer, int offset)
		{
			int num = offset;
			if (buffer != null)
			{
				buffer[offset] = this.Type();
			}
			offset++;
			offset += SerializedValue.SerializeInt32(this.mailboxNumber, buffer, offset);
			if (buffer != null)
			{
				buffer[offset] = (byte)this.clientType;
			}
			offset++;
			return offset - num;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001E558 File Offset: 0x0001C758
		public void Parse(byte[] buffer, ref int offset)
		{
			byte b = buffer[offset++];
			this.mailboxNumber = SerializedValue.ParseInt32(buffer, ref offset);
			this.clientType = (ClientType)buffer[offset++];
		}

		// Token: 0x040003EA RID: 1002
		private int mailboxNumber;

		// Token: 0x040003EB RID: 1003
		private ClientType clientType = ClientType.MaxValue;
	}
}
