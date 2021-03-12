using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000061 RID: 97
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToAddExternalUserException : LocalizedException
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00011D41 File Offset: 0x0000FF41
		public GroupMailboxFailedToAddExternalUserException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00011D4A File Offset: 0x0000FF4A
		public GroupMailboxFailedToAddExternalUserException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00011D54 File Offset: 0x0000FF54
		protected GroupMailboxFailedToAddExternalUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00011D5E File Offset: 0x0000FF5E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
