using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OperationLevelTransientException : ConnectionsTransientException
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00003821 File Offset: 0x00001A21
		public OperationLevelTransientException(string msg) : base(CXStrings.OperationLevelTransientExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003836 File Offset: 0x00001A36
		public OperationLevelTransientException(string msg, Exception innerException) : base(CXStrings.OperationLevelTransientExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000384C File Offset: 0x00001A4C
		protected OperationLevelTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003876 File Offset: 0x00001A76
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003891 File Offset: 0x00001A91
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly string msg;
	}
}
