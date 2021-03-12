using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000383 RID: 899
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasMoveFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600274B RID: 10059 RVA: 0x000546D2 File Offset: 0x000528D2
		public EasMoveFailedTransientException(string errorMessage) : base(MrsStrings.EasMoveFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000546E7 File Offset: 0x000528E7
		public EasMoveFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasMoveFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000546FD File Offset: 0x000528FD
		protected EasMoveFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00054727 File Offset: 0x00052927
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x00054742 File Offset: 0x00052942
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001088 RID: 4232
		private readonly string errorMessage;
	}
}
