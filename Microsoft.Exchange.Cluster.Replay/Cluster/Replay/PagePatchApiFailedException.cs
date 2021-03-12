using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000498 RID: 1176
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchApiFailedException : LocalizedException
	{
		// Token: 0x06002CAA RID: 11434 RVA: 0x000BFCA2 File Offset: 0x000BDEA2
		public PagePatchApiFailedException(string msg) : base(ReplayStrings.PagePatchApiFailedException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000BFCB7 File Offset: 0x000BDEB7
		public PagePatchApiFailedException(string msg, Exception innerException) : base(ReplayStrings.PagePatchApiFailedException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000BFCCD File Offset: 0x000BDECD
		protected PagePatchApiFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000BFCF7 File Offset: 0x000BDEF7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000BFD12 File Offset: 0x000BDF12
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040014F1 RID: 5361
		private readonly string msg;
	}
}
