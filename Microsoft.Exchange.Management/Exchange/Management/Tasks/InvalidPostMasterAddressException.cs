using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC4 RID: 4036
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPostMasterAddressException : LocalizedException
	{
		// Token: 0x0600ADAB RID: 44459 RVA: 0x002920CD File Offset: 0x002902CD
		public InvalidPostMasterAddressException() : base(Strings.ErrorInvalidPostMasterAddress)
		{
		}

		// Token: 0x0600ADAC RID: 44460 RVA: 0x002920DA File Offset: 0x002902DA
		public InvalidPostMasterAddressException(Exception innerException) : base(Strings.ErrorInvalidPostMasterAddress, innerException)
		{
		}

		// Token: 0x0600ADAD RID: 44461 RVA: 0x002920E8 File Offset: 0x002902E8
		protected InvalidPostMasterAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADAE RID: 44462 RVA: 0x002920F2 File Offset: 0x002902F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
