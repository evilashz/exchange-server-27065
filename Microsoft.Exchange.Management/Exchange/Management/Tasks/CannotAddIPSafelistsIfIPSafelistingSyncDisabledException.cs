using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200101B RID: 4123
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotAddIPSafelistsIfIPSafelistingSyncDisabledException : LocalizedException
	{
		// Token: 0x0600AF44 RID: 44868 RVA: 0x0029427A File Offset: 0x0029247A
		public CannotAddIPSafelistsIfIPSafelistingSyncDisabledException() : base(Strings.CannotAddIPSafelistsIfIPSafelistingSyncDisabledId)
		{
		}

		// Token: 0x0600AF45 RID: 44869 RVA: 0x00294287 File Offset: 0x00292487
		public CannotAddIPSafelistsIfIPSafelistingSyncDisabledException(Exception innerException) : base(Strings.CannotAddIPSafelistsIfIPSafelistingSyncDisabledId, innerException)
		{
		}

		// Token: 0x0600AF46 RID: 44870 RVA: 0x00294295 File Offset: 0x00292495
		protected CannotAddIPSafelistsIfIPSafelistingSyncDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF47 RID: 44871 RVA: 0x0029429F File Offset: 0x0029249F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
