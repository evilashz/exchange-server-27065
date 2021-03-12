using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200100C RID: 4108
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IrmLicensingIsDisabledException : LocalizedException
	{
		// Token: 0x0600AEFC RID: 44796 RVA: 0x00293C33 File Offset: 0x00291E33
		public IrmLicensingIsDisabledException() : base(Strings.IrmLicensingIsDisabled)
		{
		}

		// Token: 0x0600AEFD RID: 44797 RVA: 0x00293C40 File Offset: 0x00291E40
		public IrmLicensingIsDisabledException(Exception innerException) : base(Strings.IrmLicensingIsDisabled, innerException)
		{
		}

		// Token: 0x0600AEFE RID: 44798 RVA: 0x00293C4E File Offset: 0x00291E4E
		protected IrmLicensingIsDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEFF RID: 44799 RVA: 0x00293C58 File Offset: 0x00291E58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
