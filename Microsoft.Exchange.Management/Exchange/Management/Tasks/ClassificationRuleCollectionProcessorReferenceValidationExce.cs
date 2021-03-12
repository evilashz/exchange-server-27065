using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FDD RID: 4061
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionProcessorReferenceValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE1E RID: 44574 RVA: 0x002929A5 File Offset: 0x00290BA5
		public ClassificationRuleCollectionProcessorReferenceValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE1F RID: 44575 RVA: 0x002929AE File Offset: 0x00290BAE
		public ClassificationRuleCollectionProcessorReferenceValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE20 RID: 44576 RVA: 0x002929B8 File Offset: 0x00290BB8
		protected ClassificationRuleCollectionProcessorReferenceValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE21 RID: 44577 RVA: 0x002929C2 File Offset: 0x00290BC2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
