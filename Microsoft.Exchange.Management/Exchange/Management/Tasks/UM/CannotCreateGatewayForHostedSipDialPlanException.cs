using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011EA RID: 4586
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotCreateGatewayForHostedSipDialPlanException : LocalizedException
	{
		// Token: 0x0600B971 RID: 47473 RVA: 0x002A5F7C File Offset: 0x002A417C
		public CannotCreateGatewayForHostedSipDialPlanException() : base(Strings.CannotCreateGatewayForHostedSipDialPlan)
		{
		}

		// Token: 0x0600B972 RID: 47474 RVA: 0x002A5F89 File Offset: 0x002A4189
		public CannotCreateGatewayForHostedSipDialPlanException(Exception innerException) : base(Strings.CannotCreateGatewayForHostedSipDialPlan, innerException)
		{
		}

		// Token: 0x0600B973 RID: 47475 RVA: 0x002A5F97 File Offset: 0x002A4197
		protected CannotCreateGatewayForHostedSipDialPlanException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B974 RID: 47476 RVA: 0x002A5FA1 File Offset: 0x002A41A1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
