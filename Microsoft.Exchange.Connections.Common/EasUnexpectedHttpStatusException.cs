using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200005C RID: 92
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasUnexpectedHttpStatusException : ConnectionsPermanentException
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00004BA6 File Offset: 0x00002DA6
		public EasUnexpectedHttpStatusException(string msg) : base(CXStrings.EasUnexpectedHttpStatusMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004BBB File Offset: 0x00002DBB
		public EasUnexpectedHttpStatusException(string msg, Exception innerException) : base(CXStrings.EasUnexpectedHttpStatusMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004BD1 File Offset: 0x00002DD1
		protected EasUnexpectedHttpStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004BFB File Offset: 0x00002DFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004C16 File Offset: 0x00002E16
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000F6 RID: 246
		private readonly string msg;
	}
}
