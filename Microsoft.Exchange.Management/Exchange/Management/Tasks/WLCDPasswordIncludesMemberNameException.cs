using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E65 RID: 3685
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPasswordIncludesMemberNameException : WLCDMemberException
	{
		// Token: 0x0600A6E2 RID: 42722 RVA: 0x00287DD8 File Offset: 0x00285FD8
		public WLCDPasswordIncludesMemberNameException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6E3 RID: 42723 RVA: 0x00287DE1 File Offset: 0x00285FE1
		public WLCDPasswordIncludesMemberNameException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6E4 RID: 42724 RVA: 0x00287DEB File Offset: 0x00285FEB
		protected WLCDPasswordIncludesMemberNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6E5 RID: 42725 RVA: 0x00287DF5 File Offset: 0x00285FF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
