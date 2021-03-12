using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E26 RID: 3622
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientNotFoundException : LocalizedException
	{
		// Token: 0x0600A5C4 RID: 42436 RVA: 0x00286965 File Offset: 0x00284B65
		public RecipientNotFoundException(string recipient) : base(Strings.RecipientNotFoundException(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5C5 RID: 42437 RVA: 0x0028697A File Offset: 0x00284B7A
		public RecipientNotFoundException(string recipient, Exception innerException) : base(Strings.RecipientNotFoundException(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5C6 RID: 42438 RVA: 0x00286990 File Offset: 0x00284B90
		protected RecipientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600A5C7 RID: 42439 RVA: 0x002869BA File Offset: 0x00284BBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17003649 RID: 13897
		// (get) Token: 0x0600A5C8 RID: 42440 RVA: 0x002869D5 File Offset: 0x00284BD5
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04005FAF RID: 24495
		private readonly string recipient;
	}
}
