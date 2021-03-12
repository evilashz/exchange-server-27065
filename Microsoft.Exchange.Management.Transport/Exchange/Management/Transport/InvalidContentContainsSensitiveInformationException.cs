using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200017F RID: 383
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidContentContainsSensitiveInformationException : InvalidComplianceRulePredicateException
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x00036456 File Offset: 0x00034656
		public InvalidContentContainsSensitiveInformationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003645F File Offset: 0x0003465F
		public InvalidContentContainsSensitiveInformationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00036469 File Offset: 0x00034669
		protected InvalidContentContainsSensitiveInformationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00036473 File Offset: 0x00034673
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
