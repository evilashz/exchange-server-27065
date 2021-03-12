using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E9 RID: 4585
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotCreateHuntGroupForHostedSipDialPlanException : LocalizedException
	{
		// Token: 0x0600B96D RID: 47469 RVA: 0x002A5F4D File Offset: 0x002A414D
		public CannotCreateHuntGroupForHostedSipDialPlanException() : base(Strings.CannotCreateHuntGroupForHostedSipDialPlan)
		{
		}

		// Token: 0x0600B96E RID: 47470 RVA: 0x002A5F5A File Offset: 0x002A415A
		public CannotCreateHuntGroupForHostedSipDialPlanException(Exception innerException) : base(Strings.CannotCreateHuntGroupForHostedSipDialPlan, innerException)
		{
		}

		// Token: 0x0600B96F RID: 47471 RVA: 0x002A5F68 File Offset: 0x002A4168
		protected CannotCreateHuntGroupForHostedSipDialPlanException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B970 RID: 47472 RVA: 0x002A5F72 File Offset: 0x002A4172
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
