using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E27 RID: 3623
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangePrincipalNotFoundException : LocalizedException
	{
		// Token: 0x0600A5C9 RID: 42441 RVA: 0x002869DD File Offset: 0x00284BDD
		public ExchangePrincipalNotFoundException(string recipient) : base(Strings.ExchangePrincipalNotFoundException(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5CA RID: 42442 RVA: 0x002869F2 File Offset: 0x00284BF2
		public ExchangePrincipalNotFoundException(string recipient, Exception innerException) : base(Strings.ExchangePrincipalNotFoundException(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5CB RID: 42443 RVA: 0x00286A08 File Offset: 0x00284C08
		protected ExchangePrincipalNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600A5CC RID: 42444 RVA: 0x00286A32 File Offset: 0x00284C32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x1700364A RID: 13898
		// (get) Token: 0x0600A5CD RID: 42445 RVA: 0x00286A4D File Offset: 0x00284C4D
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04005FB0 RID: 24496
		private readonly string recipient;
	}
}
