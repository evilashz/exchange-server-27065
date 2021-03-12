using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE0 RID: 4064
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionVersionValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE2A RID: 44586 RVA: 0x00292A1A File Offset: 0x00290C1A
		public ClassificationRuleCollectionVersionValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE2B RID: 44587 RVA: 0x00292A23 File Offset: 0x00290C23
		public ClassificationRuleCollectionVersionValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE2C RID: 44588 RVA: 0x00292A2D File Offset: 0x00290C2D
		protected ClassificationRuleCollectionVersionValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE2D RID: 44589 RVA: 0x00292A37 File Offset: 0x00290C37
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
