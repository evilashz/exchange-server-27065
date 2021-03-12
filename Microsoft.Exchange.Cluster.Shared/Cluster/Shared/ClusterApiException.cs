using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000B6 RID: 182
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterApiException : ClusterException
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x0001AF77 File Offset: 0x00019177
		public ClusterApiException(string msg) : base(Strings.ClusterApiException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001AF91 File Offset: 0x00019191
		public ClusterApiException(string msg, Exception innerException) : base(Strings.ClusterApiException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001AFAC File Offset: 0x000191AC
		protected ClusterApiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001AFD6 File Offset: 0x000191D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001AFF1 File Offset: 0x000191F1
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000708 RID: 1800
		private readonly string msg;
	}
}
