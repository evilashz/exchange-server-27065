using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EDF RID: 3807
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseConnectionTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600A93B RID: 43323 RVA: 0x0028B569 File Offset: 0x00289769
		public DatabaseConnectionTransientException(string mdb) : base(Strings.ErrorCannotConnectToMailboxDatabase(mdb))
		{
			this.mdb = mdb;
		}

		// Token: 0x0600A93C RID: 43324 RVA: 0x0028B57E File Offset: 0x0028977E
		public DatabaseConnectionTransientException(string mdb, Exception innerException) : base(Strings.ErrorCannotConnectToMailboxDatabase(mdb), innerException)
		{
			this.mdb = mdb;
		}

		// Token: 0x0600A93D RID: 43325 RVA: 0x0028B594 File Offset: 0x00289794
		protected DatabaseConnectionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdb = (string)info.GetValue("mdb", typeof(string));
		}

		// Token: 0x0600A93E RID: 43326 RVA: 0x0028B5BE File Offset: 0x002897BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdb", this.mdb);
		}

		// Token: 0x170036DC RID: 14044
		// (get) Token: 0x0600A93F RID: 43327 RVA: 0x0028B5D9 File Offset: 0x002897D9
		public string Mdb
		{
			get
			{
				return this.mdb;
			}
		}

		// Token: 0x04006042 RID: 24642
		private readonly string mdb;
	}
}
