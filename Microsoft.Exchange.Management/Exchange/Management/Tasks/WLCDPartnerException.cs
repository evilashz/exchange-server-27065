using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E55 RID: 3669
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPartnerException : RecipientTaskException
	{
		// Token: 0x0600A6A2 RID: 42658 RVA: 0x00287B68 File Offset: 0x00285D68
		public WLCDPartnerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6A3 RID: 42659 RVA: 0x00287B71 File Offset: 0x00285D71
		public WLCDPartnerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6A4 RID: 42660 RVA: 0x00287B7B File Offset: 0x00285D7B
		protected WLCDPartnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6A5 RID: 42661 RVA: 0x00287B85 File Offset: 0x00285D85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
