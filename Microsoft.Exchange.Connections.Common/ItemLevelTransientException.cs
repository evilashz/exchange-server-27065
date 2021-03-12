using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000036 RID: 54
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ItemLevelTransientException : ConnectionsTransientException
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00003911 File Offset: 0x00001B11
		public ItemLevelTransientException(string msg) : base(CXStrings.ItemLevelTransientExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003926 File Offset: 0x00001B26
		public ItemLevelTransientException(string msg, Exception innerException) : base(CXStrings.ItemLevelTransientExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000393C File Offset: 0x00001B3C
		protected ItemLevelTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003966 File Offset: 0x00001B66
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003981 File Offset: 0x00001B81
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly string msg;
	}
}
