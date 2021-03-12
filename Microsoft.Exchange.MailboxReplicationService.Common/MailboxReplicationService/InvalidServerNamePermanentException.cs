using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002BD RID: 701
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidServerNamePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002376 RID: 9078 RVA: 0x0004E995 File Offset: 0x0004CB95
		public InvalidServerNamePermanentException(string serverName) : base(MrsStrings.InvalidServerName(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0004E9AA File Offset: 0x0004CBAA
		public InvalidServerNamePermanentException(string serverName, Exception innerException) : base(MrsStrings.InvalidServerName(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0004E9C0 File Offset: 0x0004CBC0
		protected InvalidServerNamePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0004E9EA File Offset: 0x0004CBEA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x0004EA05 File Offset: 0x0004CC05
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000FCB RID: 4043
		private readonly string serverName;
	}
}
