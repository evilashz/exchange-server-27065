using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000058 RID: 88
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasWebException : ConnectionsTransientException
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000496D File Offset: 0x00002B6D
		public EasWebException(string msg) : base(CXStrings.EasWebExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00004982 File Offset: 0x00002B82
		public EasWebException(string msg, Exception innerException) : base(CXStrings.EasWebExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004998 File Offset: 0x00002B98
		protected EasWebException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000049C2 File Offset: 0x00002BC2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000049DD File Offset: 0x00002BDD
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000F1 RID: 241
		private readonly string msg;
	}
}
