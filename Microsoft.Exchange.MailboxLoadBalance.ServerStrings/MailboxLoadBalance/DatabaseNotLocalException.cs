using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseNotLocalException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public DatabaseNotLocalException(string databaseName, string edbPath) : base(MigrationWorkflowServiceStrings.ErrorDatabaseNotLocal(databaseName, edbPath))
		{
			this.databaseName = databaseName;
			this.edbPath = edbPath;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002CBD File Offset: 0x00000EBD
		public DatabaseNotLocalException(string databaseName, string edbPath, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorDatabaseNotLocal(databaseName, edbPath), innerException)
		{
			this.databaseName = databaseName;
			this.edbPath = edbPath;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002CDC File Offset: 0x00000EDC
		protected DatabaseNotLocalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.edbPath = (string)info.GetValue("edbPath", typeof(string));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D31 File Offset: 0x00000F31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("edbPath", this.edbPath);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002D5D File Offset: 0x00000F5D
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002D65 File Offset: 0x00000F65
		public string EdbPath
		{
			get
			{
				return this.edbPath;
			}
		}

		// Token: 0x0400002D RID: 45
		private readonly string databaseName;

		// Token: 0x0400002E RID: 46
		private readonly string edbPath;
	}
}
