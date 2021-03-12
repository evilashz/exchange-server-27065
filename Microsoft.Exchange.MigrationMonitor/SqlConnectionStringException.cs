using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SqlConnectionStringException : LocalizedException
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00009414 File Offset: 0x00007614
		public SqlConnectionStringException(string connection) : base(MigrationMonitorStrings.ErrorSqlConnectionString(connection))
		{
			this.connection = connection;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009429 File Offset: 0x00007629
		public SqlConnectionStringException(string connection, Exception innerException) : base(MigrationMonitorStrings.ErrorSqlConnectionString(connection), innerException)
		{
			this.connection = connection;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000943F File Offset: 0x0000763F
		protected SqlConnectionStringException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.connection = (string)info.GetValue("connection", typeof(string));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009469 File Offset: 0x00007669
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("connection", this.connection);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00009484 File Offset: 0x00007684
		public string Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x04000161 RID: 353
		private readonly string connection;
	}
}
