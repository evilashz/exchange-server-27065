using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxOrDatabaseNotSpecifiedException : LocalizedException
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003EC8 File Offset: 0x000020C8
		public MailboxOrDatabaseNotSpecifiedException() : base(Strings.descMailboxOrDatabaseNotSpecified)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003ED5 File Offset: 0x000020D5
		public MailboxOrDatabaseNotSpecifiedException(Exception innerException) : base(Strings.descMailboxOrDatabaseNotSpecified, innerException)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003EE3 File Offset: 0x000020E3
		protected MailboxOrDatabaseNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003EED File Offset: 0x000020ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
