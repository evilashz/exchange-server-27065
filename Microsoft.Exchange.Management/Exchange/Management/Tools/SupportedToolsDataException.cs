using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x0200110C RID: 4364
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SupportedToolsDataException : LocalizedException
	{
		// Token: 0x0600B42E RID: 46126 RVA: 0x0029C6ED File Offset: 0x0029A8ED
		public SupportedToolsDataException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B42F RID: 46127 RVA: 0x0029C6F6 File Offset: 0x0029A8F6
		public SupportedToolsDataException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B430 RID: 46128 RVA: 0x0029C700 File Offset: 0x0029A900
		protected SupportedToolsDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B431 RID: 46129 RVA: 0x0029C70A File Offset: 0x0029A90A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
