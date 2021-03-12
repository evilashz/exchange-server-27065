using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E89 RID: 3721
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseExcludedFromProvisioningException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A781 RID: 42881 RVA: 0x0028883C File Offset: 0x00286A3C
		public DatabaseExcludedFromProvisioningException(string db) : base(Strings.ErrorDatabaseExcludedFromProvisioning(db))
		{
			this.db = db;
		}

		// Token: 0x0600A782 RID: 42882 RVA: 0x00288851 File Offset: 0x00286A51
		public DatabaseExcludedFromProvisioningException(string db, Exception innerException) : base(Strings.ErrorDatabaseExcludedFromProvisioning(db), innerException)
		{
			this.db = db;
		}

		// Token: 0x0600A783 RID: 42883 RVA: 0x00288867 File Offset: 0x00286A67
		protected DatabaseExcludedFromProvisioningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.db = (string)info.GetValue("db", typeof(string));
		}

		// Token: 0x0600A784 RID: 42884 RVA: 0x00288891 File Offset: 0x00286A91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("db", this.db);
		}

		// Token: 0x1700367A RID: 13946
		// (get) Token: 0x0600A785 RID: 42885 RVA: 0x002888AC File Offset: 0x00286AAC
		public string Db
		{
			get
			{
				return this.db;
			}
		}

		// Token: 0x04005FE0 RID: 24544
		private readonly string db;
	}
}
