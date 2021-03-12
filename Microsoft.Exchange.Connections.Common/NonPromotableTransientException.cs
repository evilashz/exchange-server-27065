using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NonPromotableTransientException : ConnectionsTransientException
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000037A9 File Offset: 0x000019A9
		public NonPromotableTransientException(string msg) : base(CXStrings.NonPromotableTransientExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000037BE File Offset: 0x000019BE
		public NonPromotableTransientException(string msg, Exception innerException) : base(CXStrings.NonPromotableTransientExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000037D4 File Offset: 0x000019D4
		protected NonPromotableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000037FE File Offset: 0x000019FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003819 File Offset: 0x00001A19
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000CE RID: 206
		private readonly string msg;
	}
}
