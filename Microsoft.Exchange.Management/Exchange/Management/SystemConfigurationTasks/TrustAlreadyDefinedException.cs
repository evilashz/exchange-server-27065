using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010AF RID: 4271
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrustAlreadyDefinedException : FederationException
	{
		// Token: 0x0600B25E RID: 45662 RVA: 0x00299A21 File Offset: 0x00297C21
		public TrustAlreadyDefinedException() : base(Strings.ErrorTrustAlreadyDefined)
		{
		}

		// Token: 0x0600B25F RID: 45663 RVA: 0x00299A2E File Offset: 0x00297C2E
		public TrustAlreadyDefinedException(Exception innerException) : base(Strings.ErrorTrustAlreadyDefined, innerException)
		{
		}

		// Token: 0x0600B260 RID: 45664 RVA: 0x00299A3C File Offset: 0x00297C3C
		protected TrustAlreadyDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B261 RID: 45665 RVA: 0x00299A46 File Offset: 0x00297C46
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
