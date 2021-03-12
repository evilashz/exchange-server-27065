using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D9 RID: 729
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindDCHavingUmmUpdateTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x0004F45D File Offset: 0x0004D65D
		public CouldNotFindDCHavingUmmUpdateTransientException(Guid expectedDb, string recipient) : base(MrsStrings.CouldNotFindDcHavingUmmUpdateError(expectedDb, recipient))
		{
			this.expectedDb = expectedDb;
			this.recipient = recipient;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x0004F47A File Offset: 0x0004D67A
		public CouldNotFindDCHavingUmmUpdateTransientException(Guid expectedDb, string recipient, Exception innerException) : base(MrsStrings.CouldNotFindDcHavingUmmUpdateError(expectedDb, recipient), innerException)
		{
			this.expectedDb = expectedDb;
			this.recipient = recipient;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0004F498 File Offset: 0x0004D698
		protected CouldNotFindDCHavingUmmUpdateTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.expectedDb = (Guid)info.GetValue("expectedDb", typeof(Guid));
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0004F4ED File Offset: 0x0004D6ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("expectedDb", this.expectedDb);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x0004F51E File Offset: 0x0004D71E
		public Guid ExpectedDb
		{
			get
			{
				return this.expectedDb;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x0004F526 File Offset: 0x0004D726
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04000FDE RID: 4062
		private readonly Guid expectedDb;

		// Token: 0x04000FDF RID: 4063
		private readonly string recipient;
	}
}
