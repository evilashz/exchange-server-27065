using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000157 RID: 343
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationItemLastUpdatedInTheFutureTransientException : MigrationTransientException
	{
		// Token: 0x0600161A RID: 5658 RVA: 0x0006EE32 File Offset: 0x0006D032
		public MigrationItemLastUpdatedInTheFutureTransientException(string time) : base(Strings.MigrationItemLastUpdatedInTheFuture(time))
		{
			this.time = time;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0006EE47 File Offset: 0x0006D047
		public MigrationItemLastUpdatedInTheFutureTransientException(string time, Exception innerException) : base(Strings.MigrationItemLastUpdatedInTheFuture(time), innerException)
		{
			this.time = time;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0006EE5D File Offset: 0x0006D05D
		protected MigrationItemLastUpdatedInTheFutureTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.time = (string)info.GetValue("time", typeof(string));
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0006EE87 File Offset: 0x0006D087
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("time", this.time);
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0006EEA2 File Offset: 0x0006D0A2
		public string Time
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x04000AE6 RID: 2790
		private readonly string time;
	}
}
