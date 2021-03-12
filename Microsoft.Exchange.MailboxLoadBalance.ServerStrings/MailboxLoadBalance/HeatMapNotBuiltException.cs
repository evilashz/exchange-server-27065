using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000020 RID: 32
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class HeatMapNotBuiltException : MailboxLoadBalanceTransientException
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000031C8 File Offset: 0x000013C8
		public HeatMapNotBuiltException() : base(MigrationWorkflowServiceStrings.ErrorHeatMapNotBuilt)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000031D5 File Offset: 0x000013D5
		public HeatMapNotBuiltException(Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorHeatMapNotBuilt, innerException)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000031E3 File Offset: 0x000013E3
		protected HeatMapNotBuiltException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000031ED File Offset: 0x000013ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
