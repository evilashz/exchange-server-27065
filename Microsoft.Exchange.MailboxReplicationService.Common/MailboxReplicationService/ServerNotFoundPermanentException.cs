using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200031C RID: 796
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002543 RID: 9539 RVA: 0x0005137A File Offset: 0x0004F57A
		public ServerNotFoundPermanentException(string serverLegDN) : base(MrsStrings.ServerNotFound(serverLegDN))
		{
			this.serverLegDN = serverLegDN;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x0005138F File Offset: 0x0004F58F
		public ServerNotFoundPermanentException(string serverLegDN, Exception innerException) : base(MrsStrings.ServerNotFound(serverLegDN), innerException)
		{
			this.serverLegDN = serverLegDN;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000513A5 File Offset: 0x0004F5A5
		protected ServerNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverLegDN = (string)info.GetValue("serverLegDN", typeof(string));
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000513CF File Offset: 0x0004F5CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverLegDN", this.serverLegDN);
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000513EA File Offset: 0x0004F5EA
		public string ServerLegDN
		{
			get
			{
				return this.serverLegDN;
			}
		}

		// Token: 0x0400101C RID: 4124
		private readonly string serverLegDN;
	}
}
