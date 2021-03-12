using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000060 RID: 96
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotAMemberException : DataSourceOperationException
	{
		// Token: 0x0600031F RID: 799 RVA: 0x00011D1A File Offset: 0x0000FF1A
		public NotAMemberException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00011D23 File Offset: 0x0000FF23
		public NotAMemberException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00011D2D File Offset: 0x0000FF2D
		protected NotAMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011D37 File Offset: 0x0000FF37
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
