using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C7 RID: 199
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DocumentValidationException : OperationFailedException
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0001313D File Offset: 0x0001133D
		public DocumentValidationException(string msg) : base(Strings.DocumentValidationFailure(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00013152 File Offset: 0x00011352
		public DocumentValidationException(string msg, Exception innerException) : base(Strings.DocumentValidationFailure(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00013168 File Offset: 0x00011368
		protected DocumentValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00013192 File Offset: 0x00011392
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000131AD File Offset: 0x000113AD
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040002CF RID: 719
		private readonly string msg;
	}
}
