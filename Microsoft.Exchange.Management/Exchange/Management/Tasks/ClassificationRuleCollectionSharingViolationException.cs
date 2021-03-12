using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE3 RID: 4067
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionSharingViolationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE36 RID: 44598 RVA: 0x00292A97 File Offset: 0x00290C97
		public ClassificationRuleCollectionSharingViolationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE37 RID: 44599 RVA: 0x00292AA0 File Offset: 0x00290CA0
		public ClassificationRuleCollectionSharingViolationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE38 RID: 44600 RVA: 0x00292AAA File Offset: 0x00290CAA
		protected ClassificationRuleCollectionSharingViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE39 RID: 44601 RVA: 0x00292AB4 File Offset: 0x00290CB4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
