using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasWBXmlPermanentException : ConnectionsPermanentException
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000487D File Offset: 0x00002A7D
		public EasWBXmlPermanentException(string msg) : base(CXStrings.EasWBXmlPermanentExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004892 File Offset: 0x00002A92
		public EasWBXmlPermanentException(string msg, Exception innerException) : base(CXStrings.EasWBXmlPermanentExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000048A8 File Offset: 0x00002AA8
		protected EasWBXmlPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000048D2 File Offset: 0x00002AD2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000048ED File Offset: 0x00002AED
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000EF RID: 239
		private readonly string msg;
	}
}
