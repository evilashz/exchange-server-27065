using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SqlServerTimeoutException : LocalizedException
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00008EB2 File Offset: 0x000070B2
		public SqlServerTimeoutException(string sprocName) : base(MigrationMonitorStrings.ErrorSqlServerTimeout(sprocName))
		{
			this.sprocName = sprocName;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008EC7 File Offset: 0x000070C7
		public SqlServerTimeoutException(string sprocName, Exception innerException) : base(MigrationMonitorStrings.ErrorSqlServerTimeout(sprocName), innerException)
		{
			this.sprocName = sprocName;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008EDD File Offset: 0x000070DD
		protected SqlServerTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sprocName = (string)info.GetValue("sprocName", typeof(string));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008F07 File Offset: 0x00007107
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sprocName", this.sprocName);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008F22 File Offset: 0x00007122
		public string SprocName
		{
			get
			{
				return this.sprocName;
			}
		}

		// Token: 0x0400015B RID: 347
		private readonly string sprocName;
	}
}
