using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000105 RID: 261
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ApnsFeedbackExpiredTokenException : ApnsFeedbackException
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x00019D9C File Offset: 0x00017F9C
		public ApnsFeedbackExpiredTokenException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00019DA5 File Offset: 0x00017FA5
		public ApnsFeedbackExpiredTokenException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00019DAF File Offset: 0x00017FAF
		protected ApnsFeedbackExpiredTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00019DB9 File Offset: 0x00017FB9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
