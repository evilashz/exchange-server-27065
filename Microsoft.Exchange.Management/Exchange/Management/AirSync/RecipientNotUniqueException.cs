using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E29 RID: 3625
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientNotUniqueException : LocalizedException
	{
		// Token: 0x0600A5D3 RID: 42451 RVA: 0x00286ACD File Offset: 0x00284CCD
		public RecipientNotUniqueException(string recipient) : base(Strings.RecipientNotUniqueException(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5D4 RID: 42452 RVA: 0x00286AE2 File Offset: 0x00284CE2
		public RecipientNotUniqueException(string recipient, Exception innerException) : base(Strings.RecipientNotUniqueException(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A5D5 RID: 42453 RVA: 0x00286AF8 File Offset: 0x00284CF8
		protected RecipientNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600A5D6 RID: 42454 RVA: 0x00286B22 File Offset: 0x00284D22
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x1700364C RID: 13900
		// (get) Token: 0x0600A5D7 RID: 42455 RVA: 0x00286B3D File Offset: 0x00284D3D
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04005FB2 RID: 24498
		private readonly string recipient;
	}
}
