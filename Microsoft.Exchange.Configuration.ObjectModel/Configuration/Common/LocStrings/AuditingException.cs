using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Common.LocStrings
{
	// Token: 0x020002A2 RID: 674
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuditingException : LocalizedException
	{
		// Token: 0x0600189F RID: 6303 RVA: 0x0005C0DE File Offset: 0x0005A2DE
		public AuditingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005C0E7 File Offset: 0x0005A2E7
		public AuditingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0005C0F1 File Offset: 0x0005A2F1
		protected AuditingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0005C0FB File Offset: 0x0005A2FB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
