using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE1 RID: 4065
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionFingerprintValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE2E RID: 44590 RVA: 0x00292A41 File Offset: 0x00290C41
		public ClassificationRuleCollectionFingerprintValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE2F RID: 44591 RVA: 0x00292A4A File Offset: 0x00290C4A
		public ClassificationRuleCollectionFingerprintValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE30 RID: 44592 RVA: 0x00292A54 File Offset: 0x00290C54
		protected ClassificationRuleCollectionFingerprintValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE31 RID: 44593 RVA: 0x00292A5E File Offset: 0x00290C5E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
