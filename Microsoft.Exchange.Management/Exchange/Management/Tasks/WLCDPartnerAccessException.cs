using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E56 RID: 3670
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPartnerAccessException : WLCDPartnerException
	{
		// Token: 0x0600A6A6 RID: 42662 RVA: 0x00287B8F File Offset: 0x00285D8F
		public WLCDPartnerAccessException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6A7 RID: 42663 RVA: 0x00287B98 File Offset: 0x00285D98
		public WLCDPartnerAccessException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6A8 RID: 42664 RVA: 0x00287BA2 File Offset: 0x00285DA2
		protected WLCDPartnerAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6A9 RID: 42665 RVA: 0x00287BAC File Offset: 0x00285DAC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
