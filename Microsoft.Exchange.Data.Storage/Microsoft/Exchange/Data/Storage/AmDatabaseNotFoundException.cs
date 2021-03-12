using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000CF RID: 207
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDatabaseNotFoundException : AmServerException
	{
		// Token: 0x06001288 RID: 4744 RVA: 0x00067CF5 File Offset: 0x00065EF5
		public AmDatabaseNotFoundException(Guid dbGuid) : base(ServerStrings.AmDatabaseNotFoundException(dbGuid))
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00067D0F File Offset: 0x00065F0F
		public AmDatabaseNotFoundException(Guid dbGuid, Exception innerException) : base(ServerStrings.AmDatabaseNotFoundException(dbGuid), innerException)
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00067D2A File Offset: 0x00065F2A
		protected AmDatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbGuid = (Guid)info.GetValue("dbGuid", typeof(Guid));
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00067D54 File Offset: 0x00065F54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbGuid", this.dbGuid);
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00067D74 File Offset: 0x00065F74
		public Guid DbGuid
		{
			get
			{
				return this.dbGuid;
			}
		}

		// Token: 0x0400095E RID: 2398
		private readonly Guid dbGuid;
	}
}
