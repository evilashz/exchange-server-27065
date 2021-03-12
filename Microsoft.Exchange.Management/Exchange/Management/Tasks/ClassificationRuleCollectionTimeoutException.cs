using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE8 RID: 4072
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionTimeoutException : LocalizedException
	{
		// Token: 0x0600AE4D RID: 44621 RVA: 0x00292C63 File Offset: 0x00290E63
		public ClassificationRuleCollectionTimeoutException() : base(Strings.ClassificationRuleCollectionTimeoutFailure)
		{
		}

		// Token: 0x0600AE4E RID: 44622 RVA: 0x00292C70 File Offset: 0x00290E70
		public ClassificationRuleCollectionTimeoutException(Exception innerException) : base(Strings.ClassificationRuleCollectionTimeoutFailure, innerException)
		{
		}

		// Token: 0x0600AE4F RID: 44623 RVA: 0x00292C7E File Offset: 0x00290E7E
		protected ClassificationRuleCollectionTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE50 RID: 44624 RVA: 0x00292C88 File Offset: 0x00290E88
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
