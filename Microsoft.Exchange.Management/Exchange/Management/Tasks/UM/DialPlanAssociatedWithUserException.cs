using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C4 RID: 4548
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanAssociatedWithUserException : LocalizedException
	{
		// Token: 0x0600B8BE RID: 47294 RVA: 0x002A50A9 File Offset: 0x002A32A9
		public DialPlanAssociatedWithUserException() : base(Strings.DialPlanAssociatedWithUserException)
		{
		}

		// Token: 0x0600B8BF RID: 47295 RVA: 0x002A50B6 File Offset: 0x002A32B6
		public DialPlanAssociatedWithUserException(Exception innerException) : base(Strings.DialPlanAssociatedWithUserException, innerException)
		{
		}

		// Token: 0x0600B8C0 RID: 47296 RVA: 0x002A50C4 File Offset: 0x002A32C4
		protected DialPlanAssociatedWithUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8C1 RID: 47297 RVA: 0x002A50CE File Offset: 0x002A32CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
