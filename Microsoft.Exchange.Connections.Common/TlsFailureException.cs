using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000040 RID: 64
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TlsFailureException : ConnectionsTransientException
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00003D18 File Offset: 0x00001F18
		public TlsFailureException(string msg) : base(CXStrings.TlsError(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003D2D File Offset: 0x00001F2D
		public TlsFailureException(string msg, Exception innerException) : base(CXStrings.TlsError(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003D43 File Offset: 0x00001F43
		protected TlsFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003D6D File Offset: 0x00001F6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003D88 File Offset: 0x00001F88
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000D8 RID: 216
		private readonly string msg;
	}
}
