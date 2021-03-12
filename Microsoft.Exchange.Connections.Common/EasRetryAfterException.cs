using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000059 RID: 89
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasRetryAfterException : ConnectionsTransientException
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x000049E5 File Offset: 0x00002BE5
		public EasRetryAfterException(TimeSpan delay, string msg) : base(CXStrings.EasRetryAfterExceptionMsg(delay, msg))
		{
			this.delay = delay;
			this.msg = msg;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00004A02 File Offset: 0x00002C02
		public EasRetryAfterException(TimeSpan delay, string msg, Exception innerException) : base(CXStrings.EasRetryAfterExceptionMsg(delay, msg), innerException)
		{
			this.delay = delay;
			this.msg = msg;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004A20 File Offset: 0x00002C20
		protected EasRetryAfterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.delay = (TimeSpan)info.GetValue("delay", typeof(TimeSpan));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004A75 File Offset: 0x00002C75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("delay", this.delay);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public TimeSpan Delay
		{
			get
			{
				return this.delay;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004AAE File Offset: 0x00002CAE
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000F2 RID: 242
		private readonly TimeSpan delay;

		// Token: 0x040000F3 RID: 243
		private readonly string msg;
	}
}
