using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E5B RID: 3675
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMemberNameUnavailableException : WLCDMemberException
	{
		// Token: 0x0600A6BA RID: 42682 RVA: 0x00287C52 File Offset: 0x00285E52
		public WLCDMemberNameUnavailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6BB RID: 42683 RVA: 0x00287C5B File Offset: 0x00285E5B
		public WLCDMemberNameUnavailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6BC RID: 42684 RVA: 0x00287C65 File Offset: 0x00285E65
		protected WLCDMemberNameUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6BD RID: 42685 RVA: 0x00287C6F File Offset: 0x00285E6F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
