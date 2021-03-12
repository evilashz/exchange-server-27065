using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E59 RID: 3673
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDUnknownException : WLCDPartnerException
	{
		// Token: 0x0600A6B2 RID: 42674 RVA: 0x00287C04 File Offset: 0x00285E04
		public WLCDUnknownException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6B3 RID: 42675 RVA: 0x00287C0D File Offset: 0x00285E0D
		public WLCDUnknownException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6B4 RID: 42676 RVA: 0x00287C17 File Offset: 0x00285E17
		protected WLCDUnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6B5 RID: 42677 RVA: 0x00287C21 File Offset: 0x00285E21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
