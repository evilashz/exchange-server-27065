using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200045B RID: 1115
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmOperationInvalidForStandaloneRoleException : AmCommonException
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x000BD506 File Offset: 0x000BB706
		public AmOperationInvalidForStandaloneRoleException() : base(ReplayStrings.AmOperationInvalidForStandaloneRoleException)
		{
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000BD518 File Offset: 0x000BB718
		public AmOperationInvalidForStandaloneRoleException(Exception innerException) : base(ReplayStrings.AmOperationInvalidForStandaloneRoleException, innerException)
		{
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000BD52B File Offset: 0x000BB72B
		protected AmOperationInvalidForStandaloneRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000BD535 File Offset: 0x000BB735
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
