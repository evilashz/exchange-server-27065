using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x0200003E RID: 62
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NoLoadContraint : IAllocationConstraint
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00007DEF File Offset: 0x00005FEF
		public NoLoadContraint(LoadContainer container)
		{
			this.container = container;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007DFE File Offset: 0x00005FFE
		public ConstraintValidationResult Accept(LoadEntity entity)
		{
			return new ConstraintValidationResult(this, false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007E07 File Offset: 0x00006007
		public void ValidateAccepted(LoadEntity entity)
		{
			throw new ContainerCannotReceiveLoadException(this.container.Name);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007E19 File Offset: 0x00006019
		public IAllocationConstraint CloneForContainer(LoadContainer container)
		{
			return new NoLoadContraint(container);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007E21 File Offset: 0x00006021
		public override string ToString()
		{
			return string.Format("REJECT", new object[0]);
		}

		// Token: 0x040000A5 RID: 165
		[DataMember]
		private readonly LoadContainer container;
	}
}
