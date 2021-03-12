using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x020010AE RID: 4270
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FederationMetadataException : FederationException
	{
		// Token: 0x0600B25A RID: 45658 RVA: 0x002999FA File Offset: 0x00297BFA
		public FederationMetadataException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B25B RID: 45659 RVA: 0x00299A03 File Offset: 0x00297C03
		public FederationMetadataException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B25C RID: 45660 RVA: 0x00299A0D File Offset: 0x00297C0D
		protected FederationMetadataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B25D RID: 45661 RVA: 0x00299A17 File Offset: 0x00297C17
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
