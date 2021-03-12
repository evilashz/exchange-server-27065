using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F2E RID: 3886
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToSetRequestedDatabaseSchemaVersionException : LocalizedException
	{
		// Token: 0x0600AADB RID: 43739 RVA: 0x0028E1F0 File Offset: 0x0028C3F0
		public FailedToSetRequestedDatabaseSchemaVersionException(string database) : base(Strings.FailedToSetRequestedDatabaseSchemaVersion(database))
		{
			this.database = database;
		}

		// Token: 0x0600AADC RID: 43740 RVA: 0x0028E205 File Offset: 0x0028C405
		public FailedToSetRequestedDatabaseSchemaVersionException(string database, Exception innerException) : base(Strings.FailedToSetRequestedDatabaseSchemaVersion(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x0600AADD RID: 43741 RVA: 0x0028E21B File Offset: 0x0028C41B
		protected FailedToSetRequestedDatabaseSchemaVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x0600AADE RID: 43742 RVA: 0x0028E245 File Offset: 0x0028C445
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x17003740 RID: 14144
		// (get) Token: 0x0600AADF RID: 43743 RVA: 0x0028E260 File Offset: 0x0028C460
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x040060A6 RID: 24742
		private readonly string database;
	}
}
