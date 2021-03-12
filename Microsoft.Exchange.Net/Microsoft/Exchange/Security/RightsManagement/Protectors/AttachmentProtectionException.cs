using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020000D0 RID: 208
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AttachmentProtectionException : LocalizedException
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00013E2A File Offset: 0x0001202A
		public AttachmentProtectionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013E33 File Offset: 0x00012033
		public AttachmentProtectionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00013E3D File Offset: 0x0001203D
		protected AttachmentProtectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013E47 File Offset: 0x00012047
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
