using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DDA RID: 3546
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveDefaultSiteMailboxProvisioningPolicyException : LocalizedException
	{
		// Token: 0x0600A42F RID: 42031 RVA: 0x00283CD1 File Offset: 0x00281ED1
		public CannotRemoveDefaultSiteMailboxProvisioningPolicyException() : base(Strings.CannotRemoveDefaultSiteMailboxProvisioningPolicyException)
		{
		}

		// Token: 0x0600A430 RID: 42032 RVA: 0x00283CDE File Offset: 0x00281EDE
		public CannotRemoveDefaultSiteMailboxProvisioningPolicyException(Exception innerException) : base(Strings.CannotRemoveDefaultSiteMailboxProvisioningPolicyException, innerException)
		{
		}

		// Token: 0x0600A431 RID: 42033 RVA: 0x00283CEC File Offset: 0x00281EEC
		protected CannotRemoveDefaultSiteMailboxProvisioningPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A432 RID: 42034 RVA: 0x00283CF6 File Offset: 0x00281EF6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
