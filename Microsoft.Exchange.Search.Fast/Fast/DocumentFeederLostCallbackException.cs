using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DocumentFeederLostCallbackException : OperationFailedException
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000F28A File Offset: 0x0000D48A
		public DocumentFeederLostCallbackException(string msg) : base(Strings.LostCallbackFailure(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F29F File Offset: 0x0000D49F
		public DocumentFeederLostCallbackException(string msg, Exception innerException) : base(Strings.LostCallbackFailure(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F2B5 File Offset: 0x0000D4B5
		protected DocumentFeederLostCallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000F2DF File Offset: 0x0000D4DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000F2FA File Offset: 0x0000D4FA
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000146 RID: 326
		private readonly string msg;
	}
}
