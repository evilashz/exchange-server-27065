using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A6E RID: 2670
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADRemoveContainerException : ADOperationException
	{
		// Token: 0x06007F15 RID: 32533 RVA: 0x001A3DB7 File Offset: 0x001A1FB7
		public ADRemoveContainerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x001A3DC0 File Offset: 0x001A1FC0
		public ADRemoveContainerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x001A3DCA File Offset: 0x001A1FCA
		protected ADRemoveContainerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x001A3DD4 File Offset: 0x001A1FD4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
