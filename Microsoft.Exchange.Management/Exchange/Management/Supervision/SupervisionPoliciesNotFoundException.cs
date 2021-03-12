using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x02000E7E RID: 3710
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SupervisionPoliciesNotFoundException : LocalizedException
	{
		// Token: 0x0600A749 RID: 42825 RVA: 0x002882A5 File Offset: 0x002864A5
		public SupervisionPoliciesNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A74A RID: 42826 RVA: 0x002882AE File Offset: 0x002864AE
		public SupervisionPoliciesNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A74B RID: 42827 RVA: 0x002882B8 File Offset: 0x002864B8
		protected SupervisionPoliciesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A74C RID: 42828 RVA: 0x002882C2 File Offset: 0x002864C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
