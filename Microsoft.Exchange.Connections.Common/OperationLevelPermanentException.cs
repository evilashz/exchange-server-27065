using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000035 RID: 53
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OperationLevelPermanentException : ConnectionsPermanentException
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00003899 File Offset: 0x00001A99
		public OperationLevelPermanentException(string msg) : base(CXStrings.OperationLevelPermanentExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000038AE File Offset: 0x00001AAE
		public OperationLevelPermanentException(string msg, Exception innerException) : base(CXStrings.OperationLevelPermanentExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000038C4 File Offset: 0x00001AC4
		protected OperationLevelPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000038EE File Offset: 0x00001AEE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003909 File Offset: 0x00001B09
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000D0 RID: 208
		private readonly string msg;
	}
}
