using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000182 RID: 386
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidContentContentPropertyContainsWordsException : InvalidComplianceRulePredicateException
	{
		// Token: 0x06000F7F RID: 3967 RVA: 0x00036524 File Offset: 0x00034724
		public InvalidContentContentPropertyContainsWordsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003652D File Offset: 0x0003472D
		public InvalidContentContentPropertyContainsWordsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00036537 File Offset: 0x00034737
		protected InvalidContentContentPropertyContainsWordsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00036541 File Offset: 0x00034741
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
