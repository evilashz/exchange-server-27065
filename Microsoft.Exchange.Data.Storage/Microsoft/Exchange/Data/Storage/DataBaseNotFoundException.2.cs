using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200012F RID: 303
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataBaseNotFoundException : StorageTransientException
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x0006AFE9 File Offset: 0x000691E9
		public DataBaseNotFoundException(Guid databaseGuid) : base(ServerStrings.DataBaseNotFoundError(databaseGuid))
		{
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0006AFFE File Offset: 0x000691FE
		public DataBaseNotFoundException(Guid databaseGuid, Exception innerException) : base(ServerStrings.DataBaseNotFoundError(databaseGuid), innerException)
		{
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0006B014 File Offset: 0x00069214
		protected DataBaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseGuid = (Guid)info.GetValue("databaseGuid", typeof(Guid));
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0006B03E File Offset: 0x0006923E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseGuid", this.databaseGuid);
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0006B05E File Offset: 0x0006925E
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x040009C4 RID: 2500
		private readonly Guid databaseGuid;
	}
}
