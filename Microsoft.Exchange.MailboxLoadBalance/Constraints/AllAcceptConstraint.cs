using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class AllAcceptConstraint : IAllocationConstraint
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000079CB File Offset: 0x00005BCB
		public AllAcceptConstraint(params IAllocationConstraint[] constraints)
		{
			if (constraints == null)
			{
				throw new ArgumentNullException("constraints");
			}
			this.constraints = constraints;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000079E8 File Offset: 0x00005BE8
		public IAllocationConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000079F0 File Offset: 0x00005BF0
		public void ValidateAccepted(LoadEntity entity)
		{
			foreach (IAllocationConstraint allocationConstraint in this.constraints)
			{
				allocationConstraint.ValidateAccepted(entity);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007A20 File Offset: 0x00005C20
		public ConstraintValidationResult Accept(LoadEntity entity)
		{
			foreach (IAllocationConstraint allocationConstraint in this.constraints)
			{
				ConstraintValidationResult constraintValidationResult = allocationConstraint.Accept(entity);
				if (!constraintValidationResult.Accepted)
				{
					return constraintValidationResult;
				}
			}
			return new ConstraintValidationResult(this, true);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007A80 File Offset: 0x00005C80
		public IAllocationConstraint CloneForContainer(LoadContainer container)
		{
			IAllocationConstraint[] array = (from x in this.constraints
			select x.CloneForContainer(container)).ToArray<IAllocationConstraint>();
			return new AllAcceptConstraint(array);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007ABD File Offset: 0x00005CBD
		public override string ToString()
		{
			return string.Format("AND({0}).", string.Join<object>(",", this.constraints.Cast<object>()));
		}

		// Token: 0x0400009D RID: 157
		[DataMember]
		private readonly IAllocationConstraint[] constraints;
	}
}
