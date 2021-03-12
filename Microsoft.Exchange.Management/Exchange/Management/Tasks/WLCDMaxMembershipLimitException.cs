using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E61 RID: 3681
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMaxMembershipLimitException : WLCDMemberException
	{
		// Token: 0x0600A6D2 RID: 42706 RVA: 0x00287D3C File Offset: 0x00285F3C
		public WLCDMaxMembershipLimitException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6D3 RID: 42707 RVA: 0x00287D45 File Offset: 0x00285F45
		public WLCDMaxMembershipLimitException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6D4 RID: 42708 RVA: 0x00287D4F File Offset: 0x00285F4F
		protected WLCDMaxMembershipLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6D5 RID: 42709 RVA: 0x00287D59 File Offset: 0x00285F59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
