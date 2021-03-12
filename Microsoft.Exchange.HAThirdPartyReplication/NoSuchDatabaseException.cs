using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000019 RID: 25
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoSuchDatabaseException : ThirdPartyReplicationException
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003146 File Offset: 0x00001346
		public NoSuchDatabaseException(Guid dbId) : base(ThirdPartyReplication.NoSuchDatabase(dbId))
		{
			this.dbId = dbId;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003160 File Offset: 0x00001360
		public NoSuchDatabaseException(Guid dbId, Exception innerException) : base(ThirdPartyReplication.NoSuchDatabase(dbId), innerException)
		{
			this.dbId = dbId;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000317B File Offset: 0x0000137B
		protected NoSuchDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbId = (Guid)info.GetValue("dbId", typeof(Guid));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000031A5 File Offset: 0x000013A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbId", this.dbId);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000031C5 File Offset: 0x000013C5
		public Guid DbId
		{
			get
			{
				return this.dbId;
			}
		}

		// Token: 0x04000021 RID: 33
		private readonly Guid dbId;
	}
}
