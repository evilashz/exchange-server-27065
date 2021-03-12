using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SqlQueryFailedException : LocalizedException
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00008F2A File Offset: 0x0000712A
		public SqlQueryFailedException(string sprocName) : base(MigrationMonitorStrings.ErrorSqlQueryFailed(sprocName))
		{
			this.sprocName = sprocName;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008F3F File Offset: 0x0000713F
		public SqlQueryFailedException(string sprocName, Exception innerException) : base(MigrationMonitorStrings.ErrorSqlQueryFailed(sprocName), innerException)
		{
			this.sprocName = sprocName;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008F55 File Offset: 0x00007155
		protected SqlQueryFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sprocName = (string)info.GetValue("sprocName", typeof(string));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008F7F File Offset: 0x0000717F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sprocName", this.sprocName);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008F9A File Offset: 0x0000719A
		public string SprocName
		{
			get
			{
				return this.sprocName;
			}
		}

		// Token: 0x0400015C RID: 348
		private readonly string sprocName;
	}
}
