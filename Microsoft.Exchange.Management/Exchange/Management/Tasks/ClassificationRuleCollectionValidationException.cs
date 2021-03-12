using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FD8 RID: 4056
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionValidationException : LocalizedException
	{
		// Token: 0x0600AE07 RID: 44551 RVA: 0x002927F1 File Offset: 0x002909F1
		public ClassificationRuleCollectionValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE08 RID: 44552 RVA: 0x002927FA File Offset: 0x002909FA
		public ClassificationRuleCollectionValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE09 RID: 44553 RVA: 0x00292804 File Offset: 0x00290A04
		protected ClassificationRuleCollectionValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE0A RID: 44554 RVA: 0x0029280E File Offset: 0x00290A0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
