using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A76 RID: 2678
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADScopeException : ADOperationException
	{
		// Token: 0x06007F35 RID: 32565 RVA: 0x001A3EEF File Offset: 0x001A20EF
		public ADScopeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F36 RID: 32566 RVA: 0x001A3EF8 File Offset: 0x001A20F8
		public ADScopeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x001A3F02 File Offset: 0x001A2102
		protected ADScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x001A3F0C File Offset: 0x001A210C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
