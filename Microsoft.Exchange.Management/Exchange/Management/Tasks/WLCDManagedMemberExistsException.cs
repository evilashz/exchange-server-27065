using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E5D RID: 3677
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDManagedMemberExistsException : WLCDMemberException
	{
		// Token: 0x0600A6C2 RID: 42690 RVA: 0x00287CA0 File Offset: 0x00285EA0
		public WLCDManagedMemberExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6C3 RID: 42691 RVA: 0x00287CA9 File Offset: 0x00285EA9
		public WLCDManagedMemberExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6C4 RID: 42692 RVA: 0x00287CB3 File Offset: 0x00285EB3
		protected WLCDManagedMemberExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6C5 RID: 42693 RVA: 0x00287CBD File Offset: 0x00285EBD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
