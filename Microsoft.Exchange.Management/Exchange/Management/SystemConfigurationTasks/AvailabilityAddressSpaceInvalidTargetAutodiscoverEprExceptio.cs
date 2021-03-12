using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001175 RID: 4469
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AvailabilityAddressSpaceInvalidTargetAutodiscoverEprException : LocalizedException
	{
		// Token: 0x0600B633 RID: 46643 RVA: 0x0029F6C8 File Offset: 0x0029D8C8
		public AvailabilityAddressSpaceInvalidTargetAutodiscoverEprException() : base(Strings.AvailabilityAddressSpaceInvalidTargetAutodiscoverEpr)
		{
		}

		// Token: 0x0600B634 RID: 46644 RVA: 0x0029F6D5 File Offset: 0x0029D8D5
		public AvailabilityAddressSpaceInvalidTargetAutodiscoverEprException(Exception innerException) : base(Strings.AvailabilityAddressSpaceInvalidTargetAutodiscoverEpr, innerException)
		{
		}

		// Token: 0x0600B635 RID: 46645 RVA: 0x0029F6E3 File Offset: 0x0029D8E3
		protected AvailabilityAddressSpaceInvalidTargetAutodiscoverEprException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B636 RID: 46646 RVA: 0x0029F6ED File Offset: 0x0029D8ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
