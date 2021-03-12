using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200049D RID: 1181
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchLegacyFileExistsException : PagePatchApiFailedException
	{
		// Token: 0x06002CC8 RID: 11464 RVA: 0x000C00C2 File Offset: 0x000BE2C2
		public PagePatchLegacyFileExistsException() : base(ReplayStrings.PagePatchLegacyFileExistsException)
		{
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000C00D4 File Offset: 0x000BE2D4
		public PagePatchLegacyFileExistsException(Exception innerException) : base(ReplayStrings.PagePatchLegacyFileExistsException, innerException)
		{
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000C00E7 File Offset: 0x000BE2E7
		protected PagePatchLegacyFileExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000C00F1 File Offset: 0x000BE2F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
