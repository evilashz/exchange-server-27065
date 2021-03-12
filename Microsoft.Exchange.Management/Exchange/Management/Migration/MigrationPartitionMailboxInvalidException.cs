using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200110F RID: 4367
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationPartitionMailboxInvalidException : LocalizedException
	{
		// Token: 0x0600B43B RID: 46139 RVA: 0x0029C7BB File Offset: 0x0029A9BB
		public MigrationPartitionMailboxInvalidException(string mailbox) : base(Strings.MigrationPartitionMailboxInvalid(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600B43C RID: 46140 RVA: 0x0029C7D0 File Offset: 0x0029A9D0
		public MigrationPartitionMailboxInvalidException(string mailbox, Exception innerException) : base(Strings.MigrationPartitionMailboxInvalid(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600B43D RID: 46141 RVA: 0x0029C7E6 File Offset: 0x0029A9E6
		protected MigrationPartitionMailboxInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x0600B43E RID: 46142 RVA: 0x0029C810 File Offset: 0x0029AA10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x1700391C RID: 14620
		// (get) Token: 0x0600B43F RID: 46143 RVA: 0x0029C82B File Offset: 0x0029AA2B
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x04006282 RID: 25218
		private readonly string mailbox;
	}
}
