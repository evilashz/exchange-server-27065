using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C6 RID: 4294
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotUpdateCertificateWhenFederationNotProvisionedException : FederationException
	{
		// Token: 0x0600B2D6 RID: 45782 RVA: 0x0029A6D0 File Offset: 0x002988D0
		public CannotUpdateCertificateWhenFederationNotProvisionedException() : base(Strings.ErrorCannotUpdateCertificateWhenFederationNotProvisioned)
		{
		}

		// Token: 0x0600B2D7 RID: 45783 RVA: 0x0029A6DD File Offset: 0x002988DD
		public CannotUpdateCertificateWhenFederationNotProvisionedException(Exception innerException) : base(Strings.ErrorCannotUpdateCertificateWhenFederationNotProvisioned, innerException)
		{
		}

		// Token: 0x0600B2D8 RID: 45784 RVA: 0x0029A6EB File Offset: 0x002988EB
		protected CannotUpdateCertificateWhenFederationNotProvisionedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B2D9 RID: 45785 RVA: 0x0029A6F5 File Offset: 0x002988F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
