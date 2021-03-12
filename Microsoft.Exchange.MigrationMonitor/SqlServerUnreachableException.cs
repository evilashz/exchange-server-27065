using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SqlServerUnreachableException : LocalizedException
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000910A File Offset: 0x0000730A
		public SqlServerUnreachableException(string connectionString) : base(MigrationMonitorStrings.ErrorSqlServerUnreachableException(connectionString))
		{
			this.connectionString = connectionString;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000911F File Offset: 0x0000731F
		public SqlServerUnreachableException(string connectionString, Exception innerException) : base(MigrationMonitorStrings.ErrorSqlServerUnreachableException(connectionString), innerException)
		{
			this.connectionString = connectionString;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009135 File Offset: 0x00007335
		protected SqlServerUnreachableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.connectionString = (string)info.GetValue("connectionString", typeof(string));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000915F File Offset: 0x0000735F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("connectionString", this.connectionString);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000917A File Offset: 0x0000737A
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
		}

		// Token: 0x04000160 RID: 352
		private readonly string connectionString;
	}
}
