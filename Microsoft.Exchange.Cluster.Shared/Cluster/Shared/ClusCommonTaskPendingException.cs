using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D1 RID: 209
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusCommonTaskPendingException : ClusCommonTransientException
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0001BD1E File Offset: 0x00019F1E
		public ClusCommonTaskPendingException(string msg) : base(Strings.ClusCommonTaskPendingException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001BD38 File Offset: 0x00019F38
		public ClusCommonTaskPendingException(string msg, Exception innerException) : base(Strings.ClusCommonTaskPendingException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001BD53 File Offset: 0x00019F53
		protected ClusCommonTaskPendingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001BD7D File Offset: 0x00019F7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001BD98 File Offset: 0x00019F98
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000723 RID: 1827
		private readonly string msg;
	}
}
