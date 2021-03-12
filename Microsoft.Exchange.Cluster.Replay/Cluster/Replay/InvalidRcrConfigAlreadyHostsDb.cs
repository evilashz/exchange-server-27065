using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A2 RID: 930
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRcrConfigAlreadyHostsDb : TransientException
	{
		// Token: 0x06002777 RID: 10103 RVA: 0x000B5FB2 File Offset: 0x000B41B2
		public InvalidRcrConfigAlreadyHostsDb(string nodeName, string dbName) : base(ReplayStrings.InvalidRcrConfigAlreadyHostsDb(nodeName, dbName))
		{
			this.nodeName = nodeName;
			this.dbName = dbName;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000B5FCF File Offset: 0x000B41CF
		public InvalidRcrConfigAlreadyHostsDb(string nodeName, string dbName, Exception innerException) : base(ReplayStrings.InvalidRcrConfigAlreadyHostsDb(nodeName, dbName), innerException)
		{
			this.nodeName = nodeName;
			this.dbName = dbName;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000B5FF0 File Offset: 0x000B41F0
		protected InvalidRcrConfigAlreadyHostsDb(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000B6045 File Offset: 0x000B4245
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x000B6071 File Offset: 0x000B4271
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000B6079 File Offset: 0x000B4279
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x04001396 RID: 5014
		private readonly string nodeName;

		// Token: 0x04001397 RID: 5015
		private readonly string dbName;
	}
}
