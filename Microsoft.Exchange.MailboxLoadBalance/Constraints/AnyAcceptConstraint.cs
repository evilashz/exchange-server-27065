using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x02000039 RID: 57
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AnyAcceptConstraint : IAllocationConstraint
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00007AEE File Offset: 0x00005CEE
		public AnyAcceptConstraint(params IAllocationConstraint[] constraints)
		{
			this.constraints = constraints;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007AFD File Offset: 0x00005CFD
		public IAllocationConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007B08 File Offset: 0x00005D08
		public void ValidateAccepted(LoadEntity entity)
		{
			if (this.Accept(entity))
			{
				return;
			}
			foreach (IAllocationConstraint allocationConstraint in this.constraints)
			{
				allocationConstraint.ValidateAccepted(entity);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007B44 File Offset: 0x00005D44
		public ConstraintValidationResult Accept(LoadEntity entity)
		{
			foreach (IAllocationConstraint allocationConstraint in this.constraints)
			{
				ConstraintValidationResult constraintValidationResult = allocationConstraint.Accept(entity);
				if (constraintValidationResult.Accepted)
				{
					return constraintValidationResult;
				}
			}
			return new ConstraintValidationResult(this, false);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public IAllocationConstraint CloneForContainer(LoadContainer container)
		{
			IAllocationConstraint[] array = (from x in this.constraints
			select x.CloneForContainer(container)).ToArray<IAllocationConstraint>();
			return new AnyAcceptConstraint(array);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007BE1 File Offset: 0x00005DE1
		public override string ToString()
		{
			return string.Format("OR({0}).", string.Join<object>(",", this.constraints.Cast<object>()));
		}

		// Token: 0x0400009E RID: 158
		[DataMember]
		private readonly IAllocationConstraint[] constraints;
	}
}
