using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200046D RID: 1133
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMoveSkippedSinceMasterChanged : AmDbActionException
	{
		// Token: 0x06002BB7 RID: 11191 RVA: 0x000BDE26 File Offset: 0x000BC026
		public AmDbMoveSkippedSinceMasterChanged(string dbName) : base(ReplayStrings.AmDbMoveSkippedSinceMasterChanged(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000BDE40 File Offset: 0x000BC040
		public AmDbMoveSkippedSinceMasterChanged(string dbName, Exception innerException) : base(ReplayStrings.AmDbMoveSkippedSinceMasterChanged(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000BDE5B File Offset: 0x000BC05B
		protected AmDbMoveSkippedSinceMasterChanged(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000BDE85 File Offset: 0x000BC085
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000BDEA0 File Offset: 0x000BC0A0
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014AA RID: 5290
		private readonly string dbName;
	}
}
