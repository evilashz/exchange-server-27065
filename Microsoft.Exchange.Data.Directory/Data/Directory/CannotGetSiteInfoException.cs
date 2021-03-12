using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A89 RID: 2697
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotGetSiteInfoException : ADExternalException
	{
		// Token: 0x06007F81 RID: 32641 RVA: 0x001A41EC File Offset: 0x001A23EC
		public CannotGetSiteInfoException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x001A41F5 File Offset: 0x001A23F5
		public CannotGetSiteInfoException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x001A41FF File Offset: 0x001A23FF
		protected CannotGetSiteInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x001A4209 File Offset: 0x001A2409
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
