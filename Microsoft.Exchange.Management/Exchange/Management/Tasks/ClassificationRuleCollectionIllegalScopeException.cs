using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE9 RID: 4073
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionIllegalScopeException : LocalizedException
	{
		// Token: 0x0600AE51 RID: 44625 RVA: 0x00292C92 File Offset: 0x00290E92
		public ClassificationRuleCollectionIllegalScopeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AE52 RID: 44626 RVA: 0x00292C9B File Offset: 0x00290E9B
		public ClassificationRuleCollectionIllegalScopeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AE53 RID: 44627 RVA: 0x00292CA5 File Offset: 0x00290EA5
		protected ClassificationRuleCollectionIllegalScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE54 RID: 44628 RVA: 0x00292CAF File Offset: 0x00290EAF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
