using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E32 RID: 3634
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ResetAutoBlockedDevicesException : LocalizedException
	{
		// Token: 0x0600A603 RID: 42499 RVA: 0x00286FFD File Offset: 0x002851FD
		public ResetAutoBlockedDevicesException(string recipient) : base(Strings.ResetAutoBlockedDevicesException(recipient))
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A604 RID: 42500 RVA: 0x00287012 File Offset: 0x00285212
		public ResetAutoBlockedDevicesException(string recipient, Exception innerException) : base(Strings.ResetAutoBlockedDevicesException(recipient), innerException)
		{
			this.recipient = recipient;
		}

		// Token: 0x0600A605 RID: 42501 RVA: 0x00287028 File Offset: 0x00285228
		protected ResetAutoBlockedDevicesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
		}

		// Token: 0x0600A606 RID: 42502 RVA: 0x00287052 File Offset: 0x00285252
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
		}

		// Token: 0x17003658 RID: 13912
		// (get) Token: 0x0600A607 RID: 42503 RVA: 0x0028706D File Offset: 0x0028526D
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x04005FBE RID: 24510
		private readonly string recipient;
	}
}
