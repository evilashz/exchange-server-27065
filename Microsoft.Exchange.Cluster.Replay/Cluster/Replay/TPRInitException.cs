using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A7 RID: 935
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TPRInitException : TransientException
	{
		// Token: 0x06002795 RID: 10133 RVA: 0x000B63D1 File Offset: 0x000B45D1
		public TPRInitException(string errMsg) : base(ReplayStrings.TPRInitFailure(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000B63E6 File Offset: 0x000B45E6
		public TPRInitException(string errMsg, Exception innerException) : base(ReplayStrings.TPRInitFailure(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000B63FC File Offset: 0x000B45FC
		protected TPRInitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000B6426 File Offset: 0x000B4626
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x000B6441 File Offset: 0x000B4641
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040013A0 RID: 5024
		private readonly string errMsg;
	}
}
