using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FDA RID: 4058
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionKeywordValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE0F RID: 44559 RVA: 0x0029283F File Offset: 0x00290A3F
		public ClassificationRuleCollectionKeywordValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE10 RID: 44560 RVA: 0x00292848 File Offset: 0x00290A48
		public ClassificationRuleCollectionKeywordValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE11 RID: 44561 RVA: 0x00292852 File Offset: 0x00290A52
		protected ClassificationRuleCollectionKeywordValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE12 RID: 44562 RVA: 0x0029285C File Offset: 0x00290A5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
