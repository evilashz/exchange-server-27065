using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FEA RID: 4074
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionInvalidObjectException : LocalizedException
	{
		// Token: 0x0600AE55 RID: 44629 RVA: 0x00292CB9 File Offset: 0x00290EB9
		public ClassificationRuleCollectionInvalidObjectException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE56 RID: 44630 RVA: 0x00292CC2 File Offset: 0x00290EC2
		public ClassificationRuleCollectionInvalidObjectException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE57 RID: 44631 RVA: 0x00292CCC File Offset: 0x00290ECC
		protected ClassificationRuleCollectionInvalidObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE58 RID: 44632 RVA: 0x00292CD6 File Offset: 0x00290ED6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
