using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C6 RID: 4550
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialPlanAssociatedWithPoliciesException : LocalizedException
	{
		// Token: 0x0600B8C7 RID: 47303 RVA: 0x002A5150 File Offset: 0x002A3350
		public DialPlanAssociatedWithPoliciesException() : base(Strings.DialPlanAssociatedWithPoliciesException)
		{
		}

		// Token: 0x0600B8C8 RID: 47304 RVA: 0x002A515D File Offset: 0x002A335D
		public DialPlanAssociatedWithPoliciesException(Exception innerException) : base(Strings.DialPlanAssociatedWithPoliciesException, innerException)
		{
		}

		// Token: 0x0600B8C9 RID: 47305 RVA: 0x002A516B File Offset: 0x002A336B
		protected DialPlanAssociatedWithPoliciesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8CA RID: 47306 RVA: 0x002A5175 File Offset: 0x002A3375
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
