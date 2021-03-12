using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A92 RID: 2706
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrgContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FAA RID: 32682 RVA: 0x001A44EE File Offset: 0x001A26EE
		public OrgContainerNotFoundException() : base(DirectoryStrings.OrgContainerNotFoundException)
		{
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x001A44FB File Offset: 0x001A26FB
		public OrgContainerNotFoundException(Exception innerException) : base(DirectoryStrings.OrgContainerNotFoundException, innerException)
		{
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x001A4509 File Offset: 0x001A2709
		protected OrgContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x001A4513 File Offset: 0x001A2713
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
