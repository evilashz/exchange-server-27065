using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200001C RID: 28
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ChangeActiveServerException : ThirdPartyReplicationException
	{
		// Token: 0x06000081 RID: 129 RVA: 0x000033DE File Offset: 0x000015DE
		public ChangeActiveServerException(Guid dbId, string newServer, string reason) : base(ThirdPartyReplication.ChangeActiveServerFailed(dbId, newServer, reason))
		{
			this.dbId = dbId;
			this.newServer = newServer;
			this.reason = reason;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003408 File Offset: 0x00001608
		public ChangeActiveServerException(Guid dbId, string newServer, string reason, Exception innerException) : base(ThirdPartyReplication.ChangeActiveServerFailed(dbId, newServer, reason), innerException)
		{
			this.dbId = dbId;
			this.newServer = newServer;
			this.reason = reason;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003434 File Offset: 0x00001634
		protected ChangeActiveServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbId = (Guid)info.GetValue("dbId", typeof(Guid));
			this.newServer = (string)info.GetValue("newServer", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000034AC File Offset: 0x000016AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbId", this.dbId);
			info.AddValue("newServer", this.newServer);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000034F9 File Offset: 0x000016F9
		public Guid DbId
		{
			get
			{
				return this.dbId;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003501 File Offset: 0x00001701
		public string NewServer
		{
			get
			{
				return this.newServer;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003509 File Offset: 0x00001709
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000027 RID: 39
		private readonly Guid dbId;

		// Token: 0x04000028 RID: 40
		private readonly string newServer;

		// Token: 0x04000029 RID: 41
		private readonly string reason;
	}
}
