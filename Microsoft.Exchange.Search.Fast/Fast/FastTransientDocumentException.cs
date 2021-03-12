using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200002F RID: 47
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FastTransientDocumentException : OperationFailedException
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000F122 File Offset: 0x0000D322
		public FastTransientDocumentException(string msg) : base(Strings.FastCannotProcessDocument(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000F137 File Offset: 0x0000D337
		public FastTransientDocumentException(string msg, Exception innerException) : base(Strings.FastCannotProcessDocument(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000F14D File Offset: 0x0000D34D
		protected FastTransientDocumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000F177 File Offset: 0x0000D377
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000F192 File Offset: 0x0000D392
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000143 RID: 323
		private readonly string msg;
	}
}
