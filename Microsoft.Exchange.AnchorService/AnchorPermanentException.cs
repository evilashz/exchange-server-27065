using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200003F RID: 63
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AnchorPermanentException : LocalizedException
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00009502 File Offset: 0x00007702
		public AnchorPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000950B File Offset: 0x0000770B
		public AnchorPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009515 File Offset: 0x00007715
		protected AnchorPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000951F File Offset: 0x0000771F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
