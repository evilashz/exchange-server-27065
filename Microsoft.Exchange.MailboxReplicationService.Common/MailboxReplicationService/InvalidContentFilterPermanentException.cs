using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002CB RID: 715
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidContentFilterPermanentException : ContentFilterPermanentException
	{
		// Token: 0x060023BB RID: 9147 RVA: 0x0004EFF0 File Offset: 0x0004D1F0
		public InvalidContentFilterPermanentException(string msg) : base(MrsStrings.ContentFilterIsInvalid(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0004F005 File Offset: 0x0004D205
		public InvalidContentFilterPermanentException(string msg, Exception innerException) : base(MrsStrings.ContentFilterIsInvalid(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0004F01B File Offset: 0x0004D21B
		protected InvalidContentFilterPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x0004F045 File Offset: 0x0004D245
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x0004F060 File Offset: 0x0004D260
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000FD8 RID: 4056
		private readonly string msg;
	}
}
