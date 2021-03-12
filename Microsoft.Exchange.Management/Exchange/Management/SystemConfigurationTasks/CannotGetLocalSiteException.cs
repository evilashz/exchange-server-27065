using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010AD RID: 4269
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotGetLocalSiteException : FederationException
	{
		// Token: 0x0600B256 RID: 45654 RVA: 0x002999CB File Offset: 0x00297BCB
		public CannotGetLocalSiteException() : base(Strings.ErrorCannotGetLocalSite)
		{
		}

		// Token: 0x0600B257 RID: 45655 RVA: 0x002999D8 File Offset: 0x00297BD8
		public CannotGetLocalSiteException(Exception innerException) : base(Strings.ErrorCannotGetLocalSite, innerException)
		{
		}

		// Token: 0x0600B258 RID: 45656 RVA: 0x002999E6 File Offset: 0x00297BE6
		protected CannotGetLocalSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B259 RID: 45657 RVA: 0x002999F0 File Offset: 0x00297BF0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
