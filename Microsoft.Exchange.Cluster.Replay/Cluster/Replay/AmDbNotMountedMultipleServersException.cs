using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000488 RID: 1160
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbNotMountedMultipleServersException : AmBcsSelectionException
	{
		// Token: 0x06002C53 RID: 11347 RVA: 0x000BF29E File Offset: 0x000BD49E
		public AmDbNotMountedMultipleServersException(string dbName, string detailedMsg) : base(ReplayStrings.AmDbNotMountedMultipleServersException(dbName, detailedMsg))
		{
			this.dbName = dbName;
			this.detailedMsg = detailedMsg;
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000BF2C0 File Offset: 0x000BD4C0
		public AmDbNotMountedMultipleServersException(string dbName, string detailedMsg, Exception innerException) : base(ReplayStrings.AmDbNotMountedMultipleServersException(dbName, detailedMsg), innerException)
		{
			this.dbName = dbName;
			this.detailedMsg = detailedMsg;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000BF2E4 File Offset: 0x000BD4E4
		protected AmDbNotMountedMultipleServersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.detailedMsg = (string)info.GetValue("detailedMsg", typeof(string));
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000BF339 File Offset: 0x000BD539
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("detailedMsg", this.detailedMsg);
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000BF365 File Offset: 0x000BD565
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000BF36D File Offset: 0x000BD56D
		public string DetailedMsg
		{
			get
			{
				return this.detailedMsg;
			}
		}

		// Token: 0x040014DA RID: 5338
		private readonly string dbName;

		// Token: 0x040014DB RID: 5339
		private readonly string detailedMsg;
	}
}
