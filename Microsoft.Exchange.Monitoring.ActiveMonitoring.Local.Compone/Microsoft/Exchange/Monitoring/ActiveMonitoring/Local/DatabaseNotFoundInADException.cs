using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A7 RID: 1447
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseNotFoundInADException : RecoveryActionExceptionCommon
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x000DE0D8 File Offset: 0x000DC2D8
		public DatabaseNotFoundInADException(string databaseGuid) : base(Strings.DatabaseNotFoundInADException(databaseGuid))
		{
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000DE0F2 File Offset: 0x000DC2F2
		public DatabaseNotFoundInADException(string databaseGuid, Exception innerException) : base(Strings.DatabaseNotFoundInADException(databaseGuid), innerException)
		{
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000DE10D File Offset: 0x000DC30D
		protected DatabaseNotFoundInADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseGuid = (string)info.GetValue("databaseGuid", typeof(string));
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000DE137 File Offset: 0x000DC337
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseGuid", this.databaseGuid);
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000DE152 File Offset: 0x000DC352
		public string DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x04001C7B RID: 7291
		private readonly string databaseGuid;
	}
}
