using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A4 RID: 1444
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToGetDatabaseStateException : LocalizedException
	{
		// Token: 0x060026CD RID: 9933 RVA: 0x000DDFB9 File Offset: 0x000DC1B9
		public UnableToGetDatabaseStateException(string database) : base(Strings.UnableToGetDatabaseState(database))
		{
			this.database = database;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000DDFCE File Offset: 0x000DC1CE
		public UnableToGetDatabaseStateException(string database, Exception innerException) : base(Strings.UnableToGetDatabaseState(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000DDFE4 File Offset: 0x000DC1E4
		protected UnableToGetDatabaseStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000DE00E File Offset: 0x000DC20E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000DE029 File Offset: 0x000DC229
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x04001C79 RID: 7289
		private readonly string database;
	}
}
