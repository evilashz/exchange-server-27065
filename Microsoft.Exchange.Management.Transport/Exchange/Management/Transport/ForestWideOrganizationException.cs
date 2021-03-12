using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000175 RID: 373
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ForestWideOrganizationException : LocalizedException
	{
		// Token: 0x06000F45 RID: 3909 RVA: 0x00036115 File Offset: 0x00034315
		public ForestWideOrganizationException() : base(Strings.ErrorNeedOrganizationId)
		{
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00036122 File Offset: 0x00034322
		public ForestWideOrganizationException(Exception innerException) : base(Strings.ErrorNeedOrganizationId, innerException)
		{
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00036130 File Offset: 0x00034330
		protected ForestWideOrganizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003613A File Offset: 0x0003433A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
