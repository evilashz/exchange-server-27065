using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FDE RID: 4062
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionLocalizationInfoValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE22 RID: 44578 RVA: 0x002929CC File Offset: 0x00290BCC
		public ClassificationRuleCollectionLocalizationInfoValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE23 RID: 44579 RVA: 0x002929D5 File Offset: 0x00290BD5
		public ClassificationRuleCollectionLocalizationInfoValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE24 RID: 44580 RVA: 0x002929DF File Offset: 0x00290BDF
		protected ClassificationRuleCollectionLocalizationInfoValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE25 RID: 44581 RVA: 0x002929E9 File Offset: 0x00290BE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
