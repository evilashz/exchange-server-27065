using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE6 RID: 4070
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionStorageException : LocalizedException
	{
		// Token: 0x0600AE44 RID: 44612 RVA: 0x00292BBC File Offset: 0x00290DBC
		public ClassificationRuleCollectionStorageException() : base(Strings.ClassificationRuleCollectionStorageFailure)
		{
		}

		// Token: 0x0600AE45 RID: 44613 RVA: 0x00292BC9 File Offset: 0x00290DC9
		public ClassificationRuleCollectionStorageException(Exception innerException) : base(Strings.ClassificationRuleCollectionStorageFailure, innerException)
		{
		}

		// Token: 0x0600AE46 RID: 44614 RVA: 0x00292BD7 File Offset: 0x00290DD7
		protected ClassificationRuleCollectionStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE47 RID: 44615 RVA: 0x00292BE1 File Offset: 0x00290DE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
