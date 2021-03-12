using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D0 RID: 1232
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayTestStoreConnectivityTimedoutException : LocalizedException
	{
		// Token: 0x06002DF0 RID: 11760 RVA: 0x000C26E5 File Offset: 0x000C08E5
		public ReplayTestStoreConnectivityTimedoutException(string operationName, string errorMsg) : base(ReplayStrings.ReplayTestStoreConnectivityTimedoutException(operationName, errorMsg))
		{
			this.operationName = operationName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000C2702 File Offset: 0x000C0902
		public ReplayTestStoreConnectivityTimedoutException(string operationName, string errorMsg, Exception innerException) : base(ReplayStrings.ReplayTestStoreConnectivityTimedoutException(operationName, errorMsg), innerException)
		{
			this.operationName = operationName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000C2720 File Offset: 0x000C0920
		protected ReplayTestStoreConnectivityTimedoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000C2775 File Offset: 0x000C0975
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000C27A1 File Offset: 0x000C09A1
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000C27A9 File Offset: 0x000C09A9
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001557 RID: 5463
		private readonly string operationName;

		// Token: 0x04001558 RID: 5464
		private readonly string errorMsg;
	}
}
