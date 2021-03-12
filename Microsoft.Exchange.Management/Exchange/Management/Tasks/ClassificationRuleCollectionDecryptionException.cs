using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE5 RID: 4069
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionDecryptionException : LocalizedException
	{
		// Token: 0x0600AE40 RID: 44608 RVA: 0x00292B8D File Offset: 0x00290D8D
		public ClassificationRuleCollectionDecryptionException() : base(Strings.ClassificationRuleCollectionDecryptionFailure)
		{
		}

		// Token: 0x0600AE41 RID: 44609 RVA: 0x00292B9A File Offset: 0x00290D9A
		public ClassificationRuleCollectionDecryptionException(Exception innerException) : base(Strings.ClassificationRuleCollectionDecryptionFailure, innerException)
		{
		}

		// Token: 0x0600AE42 RID: 44610 RVA: 0x00292BA8 File Offset: 0x00290DA8
		protected ClassificationRuleCollectionDecryptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE43 RID: 44611 RVA: 0x00292BB2 File Offset: 0x00290DB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
