using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004EF RID: 1263
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedException : LocalizedException
	{
		// Token: 0x06002E99 RID: 11929 RVA: 0x000C3AB2 File Offset: 0x000C1CB2
		public AutoReseedException(string errorMsg) : base(ReplayStrings.AutoReseedException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000C3AC7 File Offset: 0x000C1CC7
		public AutoReseedException(string errorMsg, Exception innerException) : base(ReplayStrings.AutoReseedException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000C3ADD File Offset: 0x000C1CDD
		protected AutoReseedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000C3B07 File Offset: 0x000C1D07
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000C3B22 File Offset: 0x000C1D22
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001584 RID: 5508
		private readonly string errorMsg;
	}
}
