using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F01 RID: 3841
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecoveryMailboxDatabaseNotMonitoredException : LocalizedException
	{
		// Token: 0x0600A9EE RID: 43502 RVA: 0x0028C892 File Offset: 0x0028AA92
		public RecoveryMailboxDatabaseNotMonitoredException(string databaseId) : base(Strings.RecoveryMailboxDatabaseNotMonitored(databaseId))
		{
			this.databaseId = databaseId;
		}

		// Token: 0x0600A9EF RID: 43503 RVA: 0x0028C8A7 File Offset: 0x0028AAA7
		public RecoveryMailboxDatabaseNotMonitoredException(string databaseId, Exception innerException) : base(Strings.RecoveryMailboxDatabaseNotMonitored(databaseId), innerException)
		{
			this.databaseId = databaseId;
		}

		// Token: 0x0600A9F0 RID: 43504 RVA: 0x0028C8BD File Offset: 0x0028AABD
		protected RecoveryMailboxDatabaseNotMonitoredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseId = (string)info.GetValue("databaseId", typeof(string));
		}

		// Token: 0x0600A9F1 RID: 43505 RVA: 0x0028C8E7 File Offset: 0x0028AAE7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseId", this.databaseId);
		}

		// Token: 0x17003707 RID: 14087
		// (get) Token: 0x0600A9F2 RID: 43506 RVA: 0x0028C902 File Offset: 0x0028AB02
		public string DatabaseId
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x0400606D RID: 24685
		private readonly string databaseId;
	}
}
