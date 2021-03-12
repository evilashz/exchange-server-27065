using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200101C RID: 4124
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledException : LocalizedException
	{
		// Token: 0x0600AF48 RID: 44872 RVA: 0x002942A9 File Offset: 0x002924A9
		public CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledException() : base(Strings.CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledId)
		{
		}

		// Token: 0x0600AF49 RID: 44873 RVA: 0x002942B6 File Offset: 0x002924B6
		public CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledException(Exception innerException) : base(Strings.CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledId, innerException)
		{
		}

		// Token: 0x0600AF4A RID: 44874 RVA: 0x002942C4 File Offset: 0x002924C4
		protected CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF4B RID: 44875 RVA: 0x002942CE File Offset: 0x002924CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
