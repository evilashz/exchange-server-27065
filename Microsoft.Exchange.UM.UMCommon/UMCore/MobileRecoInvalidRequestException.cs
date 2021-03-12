using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000210 RID: 528
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MobileRecoInvalidRequestException : LocalizedException
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x0003985C File Offset: 0x00037A5C
		public MobileRecoInvalidRequestException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00039865 File Offset: 0x00037A65
		public MobileRecoInvalidRequestException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003986F File Offset: 0x00037A6F
		protected MobileRecoInvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00039879 File Offset: 0x00037A79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
