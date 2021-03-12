using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000323 RID: 803
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StoreIntegFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002565 RID: 9573 RVA: 0x00051687 File Offset: 0x0004F887
		public StoreIntegFailedTransientException(int error) : base(MrsStrings.StoreIntegError(error))
		{
			this.error = error;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0005169C File Offset: 0x0004F89C
		public StoreIntegFailedTransientException(int error, Exception innerException) : base(MrsStrings.StoreIntegError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000516B2 File Offset: 0x0004F8B2
		protected StoreIntegFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (int)info.GetValue("error", typeof(int));
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000516DC File Offset: 0x0004F8DC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000516F7 File Offset: 0x0004F8F7
		public int Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001022 RID: 4130
		private readonly int error;
	}
}
