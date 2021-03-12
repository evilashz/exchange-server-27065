using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010BB RID: 4283
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSpecifyAccountNamespaceWithoutTrustException : FederationException
	{
		// Token: 0x0600B294 RID: 45716 RVA: 0x00299E0B File Offset: 0x0029800B
		public CannotSpecifyAccountNamespaceWithoutTrustException() : base(Strings.ErrorCannotSpecifyAccountNamespaceWithoutTrust)
		{
		}

		// Token: 0x0600B295 RID: 45717 RVA: 0x00299E18 File Offset: 0x00298018
		public CannotSpecifyAccountNamespaceWithoutTrustException(Exception innerException) : base(Strings.ErrorCannotSpecifyAccountNamespaceWithoutTrust, innerException)
		{
		}

		// Token: 0x0600B296 RID: 45718 RVA: 0x00299E26 File Offset: 0x00298026
		protected CannotSpecifyAccountNamespaceWithoutTrustException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B297 RID: 45719 RVA: 0x00299E30 File Offset: 0x00298030
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
