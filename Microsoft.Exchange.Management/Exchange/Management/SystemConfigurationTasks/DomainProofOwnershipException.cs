using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C7 RID: 4295
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainProofOwnershipException : FederationException
	{
		// Token: 0x0600B2DA RID: 45786 RVA: 0x0029A6FF File Offset: 0x002988FF
		public DomainProofOwnershipException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B2DB RID: 45787 RVA: 0x0029A708 File Offset: 0x00298908
		public DomainProofOwnershipException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B2DC RID: 45788 RVA: 0x0029A712 File Offset: 0x00298912
		protected DomainProofOwnershipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B2DD RID: 45789 RVA: 0x0029A71C File Offset: 0x0029891C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
