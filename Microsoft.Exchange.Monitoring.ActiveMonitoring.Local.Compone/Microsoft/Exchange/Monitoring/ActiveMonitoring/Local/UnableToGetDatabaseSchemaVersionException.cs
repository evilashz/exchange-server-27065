using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A5 RID: 1445
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToGetDatabaseSchemaVersionException : LocalizedException
	{
		// Token: 0x060026D2 RID: 9938 RVA: 0x000DE031 File Offset: 0x000DC231
		public UnableToGetDatabaseSchemaVersionException(string database) : base(Strings.UnableToGetDatabaseSchemaVersion(database))
		{
			this.database = database;
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000DE046 File Offset: 0x000DC246
		public UnableToGetDatabaseSchemaVersionException(string database, Exception innerException) : base(Strings.UnableToGetDatabaseSchemaVersion(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000DE05C File Offset: 0x000DC25C
		protected UnableToGetDatabaseSchemaVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000DE086 File Offset: 0x000DC286
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000DE0A1 File Offset: 0x000DC2A1
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x04001C7A RID: 7290
		private readonly string database;
	}
}
