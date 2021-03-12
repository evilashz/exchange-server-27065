using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011CB RID: 4555
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoAttendantAlreadyDisabledException : LocalizedException
	{
		// Token: 0x0600B8DD RID: 47325 RVA: 0x002A52BD File Offset: 0x002A34BD
		public AutoAttendantAlreadyDisabledException() : base(Strings.AAAlreadyDisabled)
		{
		}

		// Token: 0x0600B8DE RID: 47326 RVA: 0x002A52CA File Offset: 0x002A34CA
		public AutoAttendantAlreadyDisabledException(Exception innerException) : base(Strings.AAAlreadyDisabled, innerException)
		{
		}

		// Token: 0x0600B8DF RID: 47327 RVA: 0x002A52D8 File Offset: 0x002A34D8
		protected AutoAttendantAlreadyDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8E0 RID: 47328 RVA: 0x002A52E2 File Offset: 0x002A34E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
