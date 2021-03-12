using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000306 RID: 774
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060024D6 RID: 9430 RVA: 0x00050905 File Offset: 0x0004EB05
		public RelinquishJobTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0005090E File Offset: 0x0004EB0E
		public RelinquishJobTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00050918 File Offset: 0x0004EB18
		protected RelinquishJobTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00050922 File Offset: 0x0004EB22
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
