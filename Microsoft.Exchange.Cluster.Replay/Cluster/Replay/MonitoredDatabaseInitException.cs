using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B3 RID: 1203
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoredDatabaseInitException : TransientException
	{
		// Token: 0x06002D49 RID: 11593 RVA: 0x000C11A4 File Offset: 0x000BF3A4
		public MonitoredDatabaseInitException(string dbName, string error) : base(ReplayStrings.MonitoredDatabaseInitFailure(dbName, error))
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000C11C1 File Offset: 0x000BF3C1
		public MonitoredDatabaseInitException(string dbName, string error, Exception innerException) : base(ReplayStrings.MonitoredDatabaseInitFailure(dbName, error), innerException)
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000C11E0 File Offset: 0x000BF3E0
		protected MonitoredDatabaseInitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000C1235 File Offset: 0x000BF435
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000C1261 File Offset: 0x000BF461
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000C1269 File Offset: 0x000BF469
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001524 RID: 5412
		private readonly string dbName;

		// Token: 0x04001525 RID: 5413
		private readonly string error;
	}
}
