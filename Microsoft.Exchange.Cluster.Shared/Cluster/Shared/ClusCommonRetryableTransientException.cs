using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000CE RID: 206
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusCommonRetryableTransientException : ClusCommonTransientException
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0001BB98 File Offset: 0x00019D98
		public ClusCommonRetryableTransientException(string msg) : base(Strings.ClusCommonRetryableTransientException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001BBB2 File Offset: 0x00019DB2
		public ClusCommonRetryableTransientException(string msg, Exception innerException) : base(Strings.ClusCommonRetryableTransientException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001BBCD File Offset: 0x00019DCD
		protected ClusCommonRetryableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001BBF7 File Offset: 0x00019DF7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001BC12 File Offset: 0x00019E12
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000720 RID: 1824
		private readonly string msg;
	}
}
