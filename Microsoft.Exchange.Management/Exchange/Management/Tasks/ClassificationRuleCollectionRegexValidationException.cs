using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FD9 RID: 4057
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionRegexValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE0B RID: 44555 RVA: 0x00292818 File Offset: 0x00290A18
		public ClassificationRuleCollectionRegexValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE0C RID: 44556 RVA: 0x00292821 File Offset: 0x00290A21
		public ClassificationRuleCollectionRegexValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE0D RID: 44557 RVA: 0x0029282B File Offset: 0x00290A2B
		protected ClassificationRuleCollectionRegexValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE0E RID: 44558 RVA: 0x00292835 File Offset: 0x00290A35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
