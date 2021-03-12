using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200041F RID: 1055
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmMountBlockedDbMountedBeforeWithMissingEdbException : AmServerException
	{
		// Token: 0x06002A12 RID: 10770 RVA: 0x000BACD2 File Offset: 0x000B8ED2
		public AmMountBlockedDbMountedBeforeWithMissingEdbException(string dbName, string edbFilePath) : base(ReplayStrings.AmMountBlockedDbMountedBeforeWithMissingEdbException(dbName, edbFilePath))
		{
			this.dbName = dbName;
			this.edbFilePath = edbFilePath;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000BACF4 File Offset: 0x000B8EF4
		public AmMountBlockedDbMountedBeforeWithMissingEdbException(string dbName, string edbFilePath, Exception innerException) : base(ReplayStrings.AmMountBlockedDbMountedBeforeWithMissingEdbException(dbName, edbFilePath), innerException)
		{
			this.dbName = dbName;
			this.edbFilePath = edbFilePath;
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000BAD18 File Offset: 0x000B8F18
		protected AmMountBlockedDbMountedBeforeWithMissingEdbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.edbFilePath = (string)info.GetValue("edbFilePath", typeof(string));
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000BAD6D File Offset: 0x000B8F6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("edbFilePath", this.edbFilePath);
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x000BAD99 File Offset: 0x000B8F99
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000BADA1 File Offset: 0x000B8FA1
		public string EdbFilePath
		{
			get
			{
				return this.edbFilePath;
			}
		}

		// Token: 0x0400143D RID: 5181
		private readonly string dbName;

		// Token: 0x0400143E RID: 5182
		private readonly string edbFilePath;
	}
}
