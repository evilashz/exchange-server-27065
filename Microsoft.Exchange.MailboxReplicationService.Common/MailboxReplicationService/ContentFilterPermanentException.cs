using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200029D RID: 669
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ContentFilterPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022D3 RID: 8915 RVA: 0x0004D92D File Offset: 0x0004BB2D
		public ContentFilterPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0004D936 File Offset: 0x0004BB36
		public ContentFilterPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0004D940 File Offset: 0x0004BB40
		protected ContentFilterPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x0004D94A File Offset: 0x0004BB4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
