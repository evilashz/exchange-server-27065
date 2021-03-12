using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E5F RID: 3679
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDUnmanagedMemberExistsException : WLCDMemberException
	{
		// Token: 0x0600A6CA RID: 42698 RVA: 0x00287CEE File Offset: 0x00285EEE
		public WLCDUnmanagedMemberExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6CB RID: 42699 RVA: 0x00287CF7 File Offset: 0x00285EF7
		public WLCDUnmanagedMemberExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6CC RID: 42700 RVA: 0x00287D01 File Offset: 0x00285F01
		protected WLCDUnmanagedMemberExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6CD RID: 42701 RVA: 0x00287D0B File Offset: 0x00285F0B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
