using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000240 RID: 576
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidObjectTypeForSessionException : LocalizedException
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x00047836 File Offset: 0x00045A36
		public InvalidObjectTypeForSessionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0004783F File Offset: 0x00045A3F
		public InvalidObjectTypeForSessionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00047849 File Offset: 0x00045A49
		protected InvalidObjectTypeForSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00047853 File Offset: 0x00045A53
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
