using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasRequiresSyncKeyResetException : ConnectionsPermanentException
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00004AB6 File Offset: 0x00002CB6
		public EasRequiresSyncKeyResetException(string msg) : base(CXStrings.EasRequiresSyncKeyResetExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004ACB File Offset: 0x00002CCB
		public EasRequiresSyncKeyResetException(string msg, Exception innerException) : base(CXStrings.EasRequiresSyncKeyResetExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004AE1 File Offset: 0x00002CE1
		protected EasRequiresSyncKeyResetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004B0B File Offset: 0x00002D0B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004B26 File Offset: 0x00002D26
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000F4 RID: 244
		private readonly string msg;
	}
}
