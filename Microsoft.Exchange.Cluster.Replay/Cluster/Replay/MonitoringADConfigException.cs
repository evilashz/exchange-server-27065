using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004DD RID: 1245
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringADConfigException : LocalizedException
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x000C309A File Offset: 0x000C129A
		public MonitoringADConfigException(string errorMsg) : base(ReplayStrings.MonitoringADConfigException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000C30AF File Offset: 0x000C12AF
		public MonitoringADConfigException(string errorMsg, Exception innerException) : base(ReplayStrings.MonitoringADConfigException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000C30C5 File Offset: 0x000C12C5
		protected MonitoringADConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000C30EF File Offset: 0x000C12EF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000C310A File Offset: 0x000C130A
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400156F RID: 5487
		private readonly string errorMsg;
	}
}
