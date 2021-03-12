using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class AnyLoadConstraint : IAllocationConstraint
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00007C02 File Offset: 0x00005E02
		public ConstraintValidationResult Accept(LoadEntity entity)
		{
			return new ConstraintValidationResult(this, true);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007C0B File Offset: 0x00005E0B
		public void ValidateAccepted(LoadEntity entity)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007C0D File Offset: 0x00005E0D
		public IAllocationConstraint CloneForContainer(LoadContainer container)
		{
			return new AnyLoadConstraint();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007C14 File Offset: 0x00005E14
		public override string ToString()
		{
			return string.Format("ACCEPT", new object[0]);
		}
	}
}
