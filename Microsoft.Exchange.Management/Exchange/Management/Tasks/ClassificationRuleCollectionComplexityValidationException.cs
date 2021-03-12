using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FDF RID: 4063
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionComplexityValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE26 RID: 44582 RVA: 0x002929F3 File Offset: 0x00290BF3
		public ClassificationRuleCollectionComplexityValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE27 RID: 44583 RVA: 0x002929FC File Offset: 0x00290BFC
		public ClassificationRuleCollectionComplexityValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE28 RID: 44584 RVA: 0x00292A06 File Offset: 0x00290C06
		protected ClassificationRuleCollectionComplexityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE29 RID: 44585 RVA: 0x00292A10 File Offset: 0x00290C10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
