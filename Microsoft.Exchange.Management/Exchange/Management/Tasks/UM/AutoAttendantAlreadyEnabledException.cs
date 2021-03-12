using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011CC RID: 4556
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoAttendantAlreadyEnabledException : LocalizedException
	{
		// Token: 0x0600B8E1 RID: 47329 RVA: 0x002A52EC File Offset: 0x002A34EC
		public AutoAttendantAlreadyEnabledException() : base(Strings.AAAlreadyEnabled)
		{
		}

		// Token: 0x0600B8E2 RID: 47330 RVA: 0x002A52F9 File Offset: 0x002A34F9
		public AutoAttendantAlreadyEnabledException(Exception innerException) : base(Strings.AAAlreadyEnabled, innerException)
		{
		}

		// Token: 0x0600B8E3 RID: 47331 RVA: 0x002A5307 File Offset: 0x002A3507
		protected AutoAttendantAlreadyEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8E4 RID: 47332 RVA: 0x002A5311 File Offset: 0x002A3511
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
