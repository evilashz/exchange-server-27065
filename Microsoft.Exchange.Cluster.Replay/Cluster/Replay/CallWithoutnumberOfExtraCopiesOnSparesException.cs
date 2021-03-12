using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000516 RID: 1302
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CallWithoutnumberOfExtraCopiesOnSparesException : DatabaseCopyLayoutException
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000C5B5B File Offset: 0x000C3D5B
		public CallWithoutnumberOfExtraCopiesOnSparesException(string errMsg) : base(ReplayStrings.CallWithoutnumberOfExtraCopiesOnSparesException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000C5B75 File Offset: 0x000C3D75
		public CallWithoutnumberOfExtraCopiesOnSparesException(string errMsg, Exception innerException) : base(ReplayStrings.CallWithoutnumberOfExtraCopiesOnSparesException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000C5B90 File Offset: 0x000C3D90
		protected CallWithoutnumberOfExtraCopiesOnSparesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000C5BBA File Offset: 0x000C3DBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000C5BD5 File Offset: 0x000C3DD5
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040015D3 RID: 5587
		private readonly string errMsg;
	}
}
