using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000030 RID: 48
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FastPermanentDocumentException : OperationFailedException
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000F19A File Offset: 0x0000D39A
		public FastPermanentDocumentException(string msg) : base(Strings.FastCannotProcessDocument(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000F1AF File Offset: 0x0000D3AF
		public FastPermanentDocumentException(string msg, Exception innerException) : base(Strings.FastCannotProcessDocument(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F1C5 File Offset: 0x0000D3C5
		protected FastPermanentDocumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F1EF File Offset: 0x0000D3EF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000F20A File Offset: 0x0000D40A
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000144 RID: 324
		private readonly string msg;
	}
}
