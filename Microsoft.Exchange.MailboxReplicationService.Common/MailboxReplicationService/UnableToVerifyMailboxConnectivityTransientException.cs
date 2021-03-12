using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002CE RID: 718
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToVerifyMailboxConnectivityTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060023C8 RID: 9160 RVA: 0x0004F0C6 File Offset: 0x0004D2C6
		public UnableToVerifyMailboxConnectivityTransientException(LocalizedString mailboxId) : base(MrsStrings.UnableToVerifyMailboxConnectivity(mailboxId))
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x0004F0DB File Offset: 0x0004D2DB
		public UnableToVerifyMailboxConnectivityTransientException(LocalizedString mailboxId, Exception innerException) : base(MrsStrings.UnableToVerifyMailboxConnectivity(mailboxId), innerException)
		{
			this.mailboxId = mailboxId;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0004F0F1 File Offset: 0x0004D2F1
		protected UnableToVerifyMailboxConnectivityTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxId = (LocalizedString)info.GetValue("mailboxId", typeof(LocalizedString));
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0004F11B File Offset: 0x0004D31B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxId", this.mailboxId);
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x0004F13B File Offset: 0x0004D33B
		public LocalizedString MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x04000FD9 RID: 4057
		private readonly LocalizedString mailboxId;
	}
}
