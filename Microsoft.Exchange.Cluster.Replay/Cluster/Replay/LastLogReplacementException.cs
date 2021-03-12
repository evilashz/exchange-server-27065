using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A7 RID: 1191
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementException : LocalizedException
	{
		// Token: 0x06002CFD RID: 11517 RVA: 0x000C0699 File Offset: 0x000BE899
		public LastLogReplacementException(string msg) : base(ReplayStrings.LastLogReplacementException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000C06AE File Offset: 0x000BE8AE
		public LastLogReplacementException(string msg, Exception innerException) : base(ReplayStrings.LastLogReplacementException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000C06C4 File Offset: 0x000BE8C4
		protected LastLogReplacementException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000C06EE File Offset: 0x000BE8EE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000C0709 File Offset: 0x000BE909
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001508 RID: 5384
		private readonly string msg;
	}
}
