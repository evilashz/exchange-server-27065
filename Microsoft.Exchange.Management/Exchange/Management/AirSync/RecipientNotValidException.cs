using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E28 RID: 3624
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientNotValidException : LocalizedException
	{
		// Token: 0x0600A5CE RID: 42446 RVA: 0x00286A55 File Offset: 0x00284C55
		public RecipientNotValidException(string recipient) : base(Strings.RecipientNotValidException(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5CF RID: 42447 RVA: 0x00286A6A File Offset: 0x00284C6A
		public RecipientNotValidException(string recipient, Exception innerException) : base(Strings.RecipientNotValidException(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5D0 RID: 42448 RVA: 0x00286A80 File Offset: 0x00284C80
		protected RecipientNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600A5D1 RID: 42449 RVA: 0x00286AAA File Offset: 0x00284CAA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x1700364B RID: 13899
		// (get) Token: 0x0600A5D2 RID: 42450 RVA: 0x00286AC5 File Offset: 0x00284CC5
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04005FB1 RID: 24497
		private readonly string recipient;
	}
}
