using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A3 RID: 1443
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseValidationNullRefException : LocalizedException
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x000DDF41 File Offset: 0x000DC141
		public DatabaseValidationNullRefException(string database) : base(Strings.DatabaseValidationNullRef(database))
		{
			this.database = database;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000DDF56 File Offset: 0x000DC156
		public DatabaseValidationNullRefException(string database, Exception innerException) : base(Strings.DatabaseValidationNullRef(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000DDF6C File Offset: 0x000DC16C
		protected DatabaseValidationNullRefException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000DDF96 File Offset: 0x000DC196
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x000DDFB1 File Offset: 0x000DC1B1
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x04001C78 RID: 7288
		private readonly string database;
	}
}
