using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200001B RID: 27
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImmediateDismountMailboxDatabaseException : ThirdPartyReplicationException
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003301 File Offset: 0x00001501
		public ImmediateDismountMailboxDatabaseException(Guid dbId, string reason) : base(ThirdPartyReplication.ImmediateDismountMailboxDatabaseFailed(dbId, reason))
		{
			this.dbId = dbId;
			this.reason = reason;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003323 File Offset: 0x00001523
		public ImmediateDismountMailboxDatabaseException(Guid dbId, string reason, Exception innerException) : base(ThirdPartyReplication.ImmediateDismountMailboxDatabaseFailed(dbId, reason), innerException)
		{
			this.dbId = dbId;
			this.reason = reason;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003348 File Offset: 0x00001548
		protected ImmediateDismountMailboxDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbId = (Guid)info.GetValue("dbId", typeof(Guid));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000339D File Offset: 0x0000159D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbId", this.dbId);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033CE File Offset: 0x000015CE
		public Guid DbId
		{
			get
			{
				return this.dbId;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000033D6 File Offset: 0x000015D6
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000025 RID: 37
		private readonly Guid dbId;

		// Token: 0x04000026 RID: 38
		private readonly string reason;
	}
}
