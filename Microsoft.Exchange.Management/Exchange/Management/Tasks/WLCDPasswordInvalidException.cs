using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E67 RID: 3687
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPasswordInvalidException : WLCDMemberException
	{
		// Token: 0x0600A6EA RID: 42730 RVA: 0x00287E26 File Offset: 0x00286026
		public WLCDPasswordInvalidException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6EB RID: 42731 RVA: 0x00287E2F File Offset: 0x0028602F
		public WLCDPasswordInvalidException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6EC RID: 42732 RVA: 0x00287E39 File Offset: 0x00286039
		protected WLCDPasswordInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6ED RID: 42733 RVA: 0x00287E43 File Offset: 0x00286043
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
