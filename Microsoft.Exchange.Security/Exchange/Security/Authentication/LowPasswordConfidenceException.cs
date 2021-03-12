using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LowPasswordConfidenceException : LocalizedException
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00006512 File Offset: 0x00004712
		public LowPasswordConfidenceException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000651B File Offset: 0x0000471B
		public LowPasswordConfidenceException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006525 File Offset: 0x00004725
		protected LowPasswordConfidenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000652F File Offset: 0x0000472F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
