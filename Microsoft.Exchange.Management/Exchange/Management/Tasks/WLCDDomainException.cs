using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E57 RID: 3671
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDDomainException : WLCDPartnerException
	{
		// Token: 0x0600A6AA RID: 42666 RVA: 0x00287BB6 File Offset: 0x00285DB6
		public WLCDDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6AB RID: 42667 RVA: 0x00287BBF File Offset: 0x00285DBF
		public WLCDDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6AC RID: 42668 RVA: 0x00287BC9 File Offset: 0x00285DC9
		protected WLCDDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6AD RID: 42669 RVA: 0x00287BD3 File Offset: 0x00285DD3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
