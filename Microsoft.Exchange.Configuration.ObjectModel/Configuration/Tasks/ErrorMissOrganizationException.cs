using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002CF RID: 719
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorMissOrganizationException : LocalizedException
	{
		// Token: 0x06001976 RID: 6518 RVA: 0x0005D32D File Offset: 0x0005B52D
		public ErrorMissOrganizationException() : base(Strings.ErrorMissOrganization)
		{
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0005D33A File Offset: 0x0005B53A
		public ErrorMissOrganizationException(Exception innerException) : base(Strings.ErrorMissOrganization, innerException)
		{
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0005D348 File Offset: 0x0005B548
		protected ErrorMissOrganizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005D352 File Offset: 0x0005B552
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
