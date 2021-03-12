using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000398 RID: 920
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SetBrokenWatsonException : TransientException
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x000B59EA File Offset: 0x000B3BEA
		public SetBrokenWatsonException(string errMsg) : base(ReplayStrings.SetBrokenWatsonException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000B59FF File Offset: 0x000B3BFF
		public SetBrokenWatsonException(string errMsg, Exception innerException) : base(ReplayStrings.SetBrokenWatsonException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000B5A15 File Offset: 0x000B3C15
		protected SetBrokenWatsonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000B5A3F File Offset: 0x000B3C3F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000B5A5A File Offset: 0x000B3C5A
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001389 RID: 5001
		private readonly string errMsg;
	}
}
