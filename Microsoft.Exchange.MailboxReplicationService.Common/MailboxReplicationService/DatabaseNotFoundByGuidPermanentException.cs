using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002FE RID: 766
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseNotFoundByGuidPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024AF RID: 9391 RVA: 0x0005056C File Offset: 0x0004E76C
		public DatabaseNotFoundByGuidPermanentException(Guid dbGuid) : base(MrsStrings.MailboxDatabaseNotFoundByGuid(dbGuid))
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00050581 File Offset: 0x0004E781
		public DatabaseNotFoundByGuidPermanentException(Guid dbGuid, Exception innerException) : base(MrsStrings.MailboxDatabaseNotFoundByGuid(dbGuid), innerException)
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x00050597 File Offset: 0x0004E797
		protected DatabaseNotFoundByGuidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbGuid = (Guid)info.GetValue("dbGuid", typeof(Guid));
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000505C1 File Offset: 0x0004E7C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbGuid", this.dbGuid);
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x000505E1 File Offset: 0x0004E7E1
		public Guid DbGuid
		{
			get
			{
				return this.dbGuid;
			}
		}

		// Token: 0x04001000 RID: 4096
		private readonly Guid dbGuid;
	}
}
