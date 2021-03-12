using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000037 RID: 55
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ItemLevelPermanentException : ConnectionsTransientException
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00003989 File Offset: 0x00001B89
		public ItemLevelPermanentException(string msg) : base(CXStrings.ItemLevelPermanentExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000399E File Offset: 0x00001B9E
		public ItemLevelPermanentException(string msg, Exception innerException) : base(CXStrings.ItemLevelPermanentExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000039B4 File Offset: 0x00001BB4
		protected ItemLevelPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000039DE File Offset: 0x00001BDE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000039F9 File Offset: 0x00001BF9
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000D2 RID: 210
		private readonly string msg;
	}
}
