using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200001B RID: 27
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConstraintCouldNotBeSatisfiedProvisioningException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002FAC File Offset: 0x000011AC
		public ConstraintCouldNotBeSatisfiedProvisioningException(string constraintExpression) : base(MigrationWorkflowServiceStrings.ErrorConstraintCouldNotBeSatisfied(constraintExpression))
		{
			this.constraintExpression = constraintExpression;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002FC1 File Offset: 0x000011C1
		public ConstraintCouldNotBeSatisfiedProvisioningException(string constraintExpression, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorConstraintCouldNotBeSatisfied(constraintExpression), innerException)
		{
			this.constraintExpression = constraintExpression;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002FD7 File Offset: 0x000011D7
		protected ConstraintCouldNotBeSatisfiedProvisioningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.constraintExpression = (string)info.GetValue("constraintExpression", typeof(string));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003001 File Offset: 0x00001201
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("constraintExpression", this.constraintExpression);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000301C File Offset: 0x0000121C
		public string ConstraintExpression
		{
			get
			{
				return this.constraintExpression;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly string constraintExpression;
	}
}
