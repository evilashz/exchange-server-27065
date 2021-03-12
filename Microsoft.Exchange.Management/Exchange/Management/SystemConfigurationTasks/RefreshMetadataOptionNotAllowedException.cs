using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B7 RID: 4279
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RefreshMetadataOptionNotAllowedException : FederationException
	{
		// Token: 0x0600B282 RID: 45698 RVA: 0x00299CBD File Offset: 0x00297EBD
		public RefreshMetadataOptionNotAllowedException() : base(Strings.ErrorRefreshMetadataOptionNotAllowed)
		{
		}

		// Token: 0x0600B283 RID: 45699 RVA: 0x00299CCA File Offset: 0x00297ECA
		public RefreshMetadataOptionNotAllowedException(Exception innerException) : base(Strings.ErrorRefreshMetadataOptionNotAllowed, innerException)
		{
		}

		// Token: 0x0600B284 RID: 45700 RVA: 0x00299CD8 File Offset: 0x00297ED8
		protected RefreshMetadataOptionNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B285 RID: 45701 RVA: 0x00299CE2 File Offset: 0x00297EE2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
