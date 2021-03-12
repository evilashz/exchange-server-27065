using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B0 RID: 4272
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoTrustConfiguredException : FederationException
	{
		// Token: 0x0600B262 RID: 45666 RVA: 0x00299A50 File Offset: 0x00297C50
		public NoTrustConfiguredException() : base(Strings.ErrorNoTrustConfigured)
		{
		}

		// Token: 0x0600B263 RID: 45667 RVA: 0x00299A5D File Offset: 0x00297C5D
		public NoTrustConfiguredException(Exception innerException) : base(Strings.ErrorNoTrustConfigured, innerException)
		{
		}

		// Token: 0x0600B264 RID: 45668 RVA: 0x00299A6B File Offset: 0x00297C6B
		protected NoTrustConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B265 RID: 45669 RVA: 0x00299A75 File Offset: 0x00297C75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
