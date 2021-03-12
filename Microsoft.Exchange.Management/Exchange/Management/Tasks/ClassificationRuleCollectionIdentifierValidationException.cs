using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FDC RID: 4060
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionIdentifierValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE1A RID: 44570 RVA: 0x0029297E File Offset: 0x00290B7E
		public ClassificationRuleCollectionIdentifierValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE1B RID: 44571 RVA: 0x00292987 File Offset: 0x00290B87
		public ClassificationRuleCollectionIdentifierValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE1C RID: 44572 RVA: 0x00292991 File Offset: 0x00290B91
		protected ClassificationRuleCollectionIdentifierValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE1D RID: 44573 RVA: 0x0029299B File Offset: 0x00290B9B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
