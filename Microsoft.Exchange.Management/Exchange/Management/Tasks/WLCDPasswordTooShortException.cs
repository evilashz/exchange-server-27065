using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E63 RID: 3683
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPasswordTooShortException : WLCDMemberException
	{
		// Token: 0x0600A6DA RID: 42714 RVA: 0x00287D8A File Offset: 0x00285F8A
		public WLCDPasswordTooShortException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6DB RID: 42715 RVA: 0x00287D93 File Offset: 0x00285F93
		public WLCDPasswordTooShortException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6DC RID: 42716 RVA: 0x00287D9D File Offset: 0x00285F9D
		protected WLCDPasswordTooShortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6DD RID: 42717 RVA: 0x00287DA7 File Offset: 0x00285FA7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
