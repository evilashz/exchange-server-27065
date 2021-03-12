using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthMetadataBuilderException : LocalizedException
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000343B File Offset: 0x0000163B
		public AuthMetadataBuilderException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003444 File Offset: 0x00001644
		public AuthMetadataBuilderException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000344E File Offset: 0x0000164E
		protected AuthMetadataBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003458 File Offset: 0x00001658
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
