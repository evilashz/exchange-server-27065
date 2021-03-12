using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A6 RID: 934
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbLockConflictException : AmTransientException
	{
		// Token: 0x0600278E RID: 10126 RVA: 0x000B629C File Offset: 0x000B449C
		public AmDbLockConflictException(Guid dbGuid, string reqReason, string ownerReason) : base(ReplayStrings.AmDbLockConflict(dbGuid, reqReason, ownerReason))
		{
			this.dbGuid = dbGuid;
			this.reqReason = reqReason;
			this.ownerReason = ownerReason;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000B62C6 File Offset: 0x000B44C6
		public AmDbLockConflictException(Guid dbGuid, string reqReason, string ownerReason, Exception innerException) : base(ReplayStrings.AmDbLockConflict(dbGuid, reqReason, ownerReason), innerException)
		{
			this.dbGuid = dbGuid;
			this.reqReason = reqReason;
			this.ownerReason = ownerReason;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000B62F4 File Offset: 0x000B44F4
		protected AmDbLockConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbGuid = (Guid)info.GetValue("dbGuid", typeof(Guid));
			this.reqReason = (string)info.GetValue("reqReason", typeof(string));
			this.ownerReason = (string)info.GetValue("ownerReason", typeof(string));
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000B636C File Offset: 0x000B456C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbGuid", this.dbGuid);
			info.AddValue("reqReason", this.reqReason);
			info.AddValue("ownerReason", this.ownerReason);
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x000B63B9 File Offset: 0x000B45B9
		public Guid DbGuid
		{
			get
			{
				return this.dbGuid;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x000B63C1 File Offset: 0x000B45C1
		public string ReqReason
		{
			get
			{
				return this.reqReason;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000B63C9 File Offset: 0x000B45C9
		public string OwnerReason
		{
			get
			{
				return this.ownerReason;
			}
		}

		// Token: 0x0400139D RID: 5021
		private readonly Guid dbGuid;

		// Token: 0x0400139E RID: 5022
		private readonly string reqReason;

		// Token: 0x0400139F RID: 5023
		private readonly string ownerReason;
	}
}
