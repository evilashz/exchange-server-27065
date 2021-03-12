using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D2 RID: 1234
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogInspectorFailedException : LocalizedException
	{
		// Token: 0x06002DFB RID: 11771 RVA: 0x000C2829 File Offset: 0x000C0A29
		public LogInspectorFailedException(string errorMsg) : base(ReplayStrings.LogInspectorFailed(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000C283E File Offset: 0x000C0A3E
		public LogInspectorFailedException(string errorMsg, Exception innerException) : base(ReplayStrings.LogInspectorFailed(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000C2854 File Offset: 0x000C0A54
		protected LogInspectorFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000C287E File Offset: 0x000C0A7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000C2899 File Offset: 0x000C0A99
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400155A RID: 5466
		private readonly string errorMsg;
	}
}
