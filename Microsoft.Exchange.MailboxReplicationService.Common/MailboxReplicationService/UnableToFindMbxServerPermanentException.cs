using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E7 RID: 743
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToFindMbxServerPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002445 RID: 9285 RVA: 0x0004FD04 File Offset: 0x0004DF04
		public UnableToFindMbxServerPermanentException(string server) : base(MrsStrings.UnableToFindMbxServer(server))
		{
			this.server = server;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x0004FD19 File Offset: 0x0004DF19
		public UnableToFindMbxServerPermanentException(string server, Exception innerException) : base(MrsStrings.UnableToFindMbxServer(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x0004FD2F File Offset: 0x0004DF2F
		protected UnableToFindMbxServerPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0004FD59 File Offset: 0x0004DF59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x0004FD74 File Offset: 0x0004DF74
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04000FF2 RID: 4082
		private readonly string server;
	}
}
