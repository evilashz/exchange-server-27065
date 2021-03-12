using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x0200003B RID: 59
	[DataContract]
	internal class ConstraintValidationResult
	{
		// Token: 0x0600023E RID: 574 RVA: 0x00007C2E File Offset: 0x00005E2E
		public ConstraintValidationResult(IAllocationConstraint constraint, bool accepted)
		{
			this.Constraint = constraint;
			this.Accepted = accepted;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00007C44 File Offset: 0x00005E44
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007C4C File Offset: 0x00005E4C
		[DataMember]
		public IAllocationConstraint Constraint { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007C55 File Offset: 0x00005E55
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007C5D File Offset: 0x00005E5D
		[DataMember]
		public bool Accepted { get; private set; }

		// Token: 0x06000243 RID: 579 RVA: 0x00007C66 File Offset: 0x00005E66
		public static implicit operator bool(ConstraintValidationResult result)
		{
			return result != null && result.Accepted;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007C73 File Offset: 0x00005E73
		public override string ToString()
		{
			return string.Format("Constraint: '{0}' Accepted: {1}.", this.Constraint, this.Accepted);
		}
	}
}
