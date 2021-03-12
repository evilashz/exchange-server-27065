using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200017D RID: 381
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidAccessScopeIsPredicateException : InvalidComplianceRulePredicateException
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x000363F8 File Offset: 0x000345F8
		public InvalidAccessScopeIsPredicateException() : base(Strings.InvalidAccessScopeIsPredicate)
		{
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00036405 File Offset: 0x00034605
		public InvalidAccessScopeIsPredicateException(Exception innerException) : base(Strings.InvalidAccessScopeIsPredicate, innerException)
		{
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00036413 File Offset: 0x00034613
		protected InvalidAccessScopeIsPredicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003641D File Offset: 0x0003461D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
