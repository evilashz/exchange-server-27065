using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RedirectionLogicException : LocalizedException
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003E4D File Offset: 0x0000204D
		public RedirectionLogicException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003E56 File Offset: 0x00002056
		public RedirectionLogicException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003E60 File Offset: 0x00002060
		protected RedirectionLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003E6A File Offset: 0x0000206A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
