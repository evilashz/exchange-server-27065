using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A5C RID: 2652
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoSuitableServerFoundException : ADTransientException
	{
		// Token: 0x06007EC9 RID: 32457 RVA: 0x001A39B6 File Offset: 0x001A1BB6
		public NoSuitableServerFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x001A39BF File Offset: 0x001A1BBF
		public NoSuitableServerFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x001A39C9 File Offset: 0x001A1BC9
		protected NoSuitableServerFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x001A39D3 File Offset: 0x001A1BD3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
