using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A57 RID: 2647
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RusTransientException : DataSourceTransientException
	{
		// Token: 0x06007EB4 RID: 32436 RVA: 0x001A38A2 File Offset: 0x001A1AA2
		public RusTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x001A38AB File Offset: 0x001A1AAB
		public RusTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x001A38B5 File Offset: 0x001A1AB5
		protected RusTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x001A38BF File Offset: 0x001A1ABF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
