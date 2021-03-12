using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001EF RID: 495
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidMailboxPolicyException : LocalizedException
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00038D26 File Offset: 0x00036F26
		public InvalidMailboxPolicyException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00038D2F File Offset: 0x00036F2F
		public InvalidMailboxPolicyException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00038D39 File Offset: 0x00036F39
		protected InvalidMailboxPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00038D43 File Offset: 0x00036F43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
