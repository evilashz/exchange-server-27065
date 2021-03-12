using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200046E RID: 1134
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMoveOperationNotSupportedStandaloneException : AmDbActionException
	{
		// Token: 0x06002BBC RID: 11196 RVA: 0x000BDEA8 File Offset: 0x000BC0A8
		public AmDbMoveOperationNotSupportedStandaloneException(string dbName) : base(ReplayStrings.AmDbMoveOperationNotSupportedStandaloneException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000BDEC2 File Offset: 0x000BC0C2
		public AmDbMoveOperationNotSupportedStandaloneException(string dbName, Exception innerException) : base(ReplayStrings.AmDbMoveOperationNotSupportedStandaloneException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000BDEDD File Offset: 0x000BC0DD
		protected AmDbMoveOperationNotSupportedStandaloneException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000BDF07 File Offset: 0x000BC107
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000BDF22 File Offset: 0x000BC122
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014AB RID: 5291
		private readonly string dbName;
	}
}
