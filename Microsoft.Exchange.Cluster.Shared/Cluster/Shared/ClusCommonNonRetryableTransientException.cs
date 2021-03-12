using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000CF RID: 207
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusCommonNonRetryableTransientException : ClusCommonTransientException
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x0001BC1A File Offset: 0x00019E1A
		public ClusCommonNonRetryableTransientException(string msg) : base(Strings.ClusCommonNonRetryableTransientException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001BC34 File Offset: 0x00019E34
		public ClusCommonNonRetryableTransientException(string msg, Exception innerException) : base(Strings.ClusCommonNonRetryableTransientException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001BC4F File Offset: 0x00019E4F
		protected ClusCommonNonRetryableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001BC79 File Offset: 0x00019E79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001BC94 File Offset: 0x00019E94
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000721 RID: 1825
		private readonly string msg;
	}
}
