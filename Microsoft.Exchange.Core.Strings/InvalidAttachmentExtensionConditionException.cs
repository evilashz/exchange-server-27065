using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAttachmentExtensionConditionException : LocalizedException
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		public InvalidAttachmentExtensionConditionException(int ruleId) : base(CoreStrings.InvalidAttachmentExtensionCondition(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public InvalidAttachmentExtensionConditionException(int ruleId, Exception innerException) : base(CoreStrings.InvalidAttachmentExtensionCondition(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		protected InvalidAttachmentExtensionConditionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C620 File Offset: 0x0000A820
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000C63B File Offset: 0x0000A83B
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x0400034A RID: 842
		private readonly int ruleId;
	}
}
