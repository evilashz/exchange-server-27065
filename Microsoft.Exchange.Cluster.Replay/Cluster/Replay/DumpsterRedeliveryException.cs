using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200051C RID: 1308
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DumpsterRedeliveryException : LocalizedException
	{
		// Token: 0x06002FA3 RID: 12195 RVA: 0x000C5EBF File Offset: 0x000C40BF
		public DumpsterRedeliveryException(string errorMsg) : base(ReplayStrings.DumpsterRedeliveryException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000C5ED4 File Offset: 0x000C40D4
		public DumpsterRedeliveryException(string errorMsg, Exception innerException) : base(ReplayStrings.DumpsterRedeliveryException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000C5EEA File Offset: 0x000C40EA
		protected DumpsterRedeliveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000C5F14 File Offset: 0x000C4114
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000C5F2F File Offset: 0x000C412F
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x040015DA RID: 5594
		private readonly string errorMsg;
	}
}
