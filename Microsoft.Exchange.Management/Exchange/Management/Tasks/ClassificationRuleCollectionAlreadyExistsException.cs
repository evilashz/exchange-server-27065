using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE2 RID: 4066
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionAlreadyExistsException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE32 RID: 44594 RVA: 0x00292A68 File Offset: 0x00290C68
		public ClassificationRuleCollectionAlreadyExistsException() : base(Strings.ClassificationRuleCollectionAlreadyExists)
		{
		}

		// Token: 0x0600AE33 RID: 44595 RVA: 0x00292A75 File Offset: 0x00290C75
		public ClassificationRuleCollectionAlreadyExistsException(Exception innerException) : base(Strings.ClassificationRuleCollectionAlreadyExists, innerException)
		{
		}

		// Token: 0x0600AE34 RID: 44596 RVA: 0x00292A83 File Offset: 0x00290C83
		protected ClassificationRuleCollectionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE35 RID: 44597 RVA: 0x00292A8D File Offset: 0x00290C8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
