using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E60 RID: 3680
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDUnmanagedMemberNotExistsException : WLCDMemberException
	{
		// Token: 0x0600A6CE RID: 42702 RVA: 0x00287D15 File Offset: 0x00285F15
		public WLCDUnmanagedMemberNotExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6CF RID: 42703 RVA: 0x00287D1E File Offset: 0x00285F1E
		public WLCDUnmanagedMemberNotExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6D0 RID: 42704 RVA: 0x00287D28 File Offset: 0x00285F28
		protected WLCDUnmanagedMemberNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6D1 RID: 42705 RVA: 0x00287D32 File Offset: 0x00285F32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
