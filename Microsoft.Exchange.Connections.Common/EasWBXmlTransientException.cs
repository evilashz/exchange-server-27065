using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000057 RID: 87
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasWBXmlTransientException : ConnectionsTransientException
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x000048F5 File Offset: 0x00002AF5
		public EasWBXmlTransientException(string msg) : base(CXStrings.EasWBXmlExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000490A File Offset: 0x00002B0A
		public EasWBXmlTransientException(string msg, Exception innerException) : base(CXStrings.EasWBXmlExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004920 File Offset: 0x00002B20
		protected EasWBXmlTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000494A File Offset: 0x00002B4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004965 File Offset: 0x00002B65
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000F0 RID: 240
		private readonly string msg;
	}
}
