using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class ConditionException : LocalizedException
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00018764 File Offset: 0x00016964
		public ConditionException(Condition faultingCondition) : base(Strings.ExceptionCondition(faultingCondition.Role.FailureDescription, faultingCondition))
		{
			this.faultingCondition = faultingCondition;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00018784 File Offset: 0x00016984
		protected ConditionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faultingCondition = (Condition)info.GetValue("faultingCondition", typeof(Condition));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000187AE File Offset: 0x000169AE
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faultingCondition", this.faultingCondition);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000187C9 File Offset: 0x000169C9
		public Condition FaultingCondition
		{
			get
			{
				return this.faultingCondition;
			}
		}

		// Token: 0x0400016A RID: 362
		private Condition faultingCondition;
	}
}
