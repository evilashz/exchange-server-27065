using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FastDocumentTimeoutException : OperationFailedException
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000F212 File Offset: 0x0000D412
		public FastDocumentTimeoutException(string msg) : base(Strings.FastCannotProcessDocument(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F227 File Offset: 0x0000D427
		public FastDocumentTimeoutException(string msg, Exception innerException) : base(Strings.FastCannotProcessDocument(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F23D File Offset: 0x0000D43D
		protected FastDocumentTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F267 File Offset: 0x0000D467
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000F282 File Offset: 0x0000D482
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000145 RID: 325
		private readonly string msg;
	}
}
