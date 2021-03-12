using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000474 RID: 1140
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbRemountSkippedSinceDatabaseWasAdminDismounted : AmDbActionException
	{
		// Token: 0x06002BE0 RID: 11232 RVA: 0x000BE39A File Offset: 0x000BC59A
		public AmDbRemountSkippedSinceDatabaseWasAdminDismounted(string dbName) : base(ReplayStrings.AmDbRemountSkippedSinceDatabaseWasAdminDismounted(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000BE3B4 File Offset: 0x000BC5B4
		public AmDbRemountSkippedSinceDatabaseWasAdminDismounted(string dbName, Exception innerException) : base(ReplayStrings.AmDbRemountSkippedSinceDatabaseWasAdminDismounted(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000BE3CF File Offset: 0x000BC5CF
		protected AmDbRemountSkippedSinceDatabaseWasAdminDismounted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000BE3F9 File Offset: 0x000BC5F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000BE414 File Offset: 0x000BC614
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014B7 RID: 5303
		private readonly string dbName;
	}
}
