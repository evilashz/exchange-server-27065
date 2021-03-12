using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000E86 RID: 3718
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationUpgradeException : LocalizedException
	{
		// Token: 0x0600A771 RID: 42865 RVA: 0x00288675 File Offset: 0x00286875
		public OrganizationUpgradeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A772 RID: 42866 RVA: 0x0028867E File Offset: 0x0028687E
		public OrganizationUpgradeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A773 RID: 42867 RVA: 0x00288688 File Offset: 0x00286888
		protected OrganizationUpgradeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A774 RID: 42868 RVA: 0x00288692 File Offset: 0x00286892
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
