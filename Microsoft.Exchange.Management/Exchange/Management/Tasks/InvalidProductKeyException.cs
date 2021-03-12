using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001007 RID: 4103
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidProductKeyException : LocalizedException
	{
		// Token: 0x0600AEE5 RID: 44773 RVA: 0x00293A6D File Offset: 0x00291C6D
		public InvalidProductKeyException() : base(Strings.InvalidProductKey)
		{
		}

		// Token: 0x0600AEE6 RID: 44774 RVA: 0x00293A7A File Offset: 0x00291C7A
		public InvalidProductKeyException(Exception innerException) : base(Strings.InvalidProductKey, innerException)
		{
		}

		// Token: 0x0600AEE7 RID: 44775 RVA: 0x00293A88 File Offset: 0x00291C88
		protected InvalidProductKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEE8 RID: 44776 RVA: 0x00293A92 File Offset: 0x00291C92
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
