using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000067 RID: 103
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverTooManyRetriesException : MobileDriverTransientException
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public MobileDriverTooManyRetriesException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000CD51 File Offset: 0x0000AF51
		public MobileDriverTooManyRetriesException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		protected MobileDriverTooManyRetriesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000CD65 File Offset: 0x0000AF65
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
