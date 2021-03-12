using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000DF RID: 223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbAcllErrorNoReplicaInstance : AmServerException
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x000684B1 File Offset: 0x000666B1
		public AmDbAcllErrorNoReplicaInstance(string database, string server) : base(ServerStrings.AmDbAcllErrorNoReplicaInstance(database, server))
		{
			this.database = database;
			this.server = server;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000684D3 File Offset: 0x000666D3
		public AmDbAcllErrorNoReplicaInstance(string database, string server, Exception innerException) : base(ServerStrings.AmDbAcllErrorNoReplicaInstance(database, server), innerException)
		{
			this.database = database;
			this.server = server;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000684F8 File Offset: 0x000666F8
		protected AmDbAcllErrorNoReplicaInstance(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0006854D File Offset: 0x0006674D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00068579 File Offset: 0x00066779
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00068581 File Offset: 0x00066781
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0400096C RID: 2412
		private readonly string database;

		// Token: 0x0400096D RID: 2413
		private readonly string server;
	}
}
