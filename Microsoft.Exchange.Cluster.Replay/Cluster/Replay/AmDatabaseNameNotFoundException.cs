using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000479 RID: 1145
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDatabaseNameNotFoundException : AmCommonException
	{
		// Token: 0x06002BF8 RID: 11256 RVA: 0x000BE5DB File Offset: 0x000BC7DB
		public AmDatabaseNameNotFoundException(string dbName) : base(ReplayStrings.AmDatabaseNameNotFoundException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000BE5F5 File Offset: 0x000BC7F5
		public AmDatabaseNameNotFoundException(string dbName, Exception innerException) : base(ReplayStrings.AmDatabaseNameNotFoundException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000BE610 File Offset: 0x000BC810
		protected AmDatabaseNameNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000BE63A File Offset: 0x000BC83A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000BE655 File Offset: 0x000BC855
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014BB RID: 5307
		private readonly string dbName;
	}
}
