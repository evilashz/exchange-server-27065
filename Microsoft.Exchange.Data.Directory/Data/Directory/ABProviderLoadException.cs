using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A67 RID: 2663
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ABProviderLoadException : ABOperationException
	{
		// Token: 0x06007EF9 RID: 32505 RVA: 0x001A3CA6 File Offset: 0x001A1EA6
		public ABProviderLoadException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x001A3CAF File Offset: 0x001A1EAF
		public ABProviderLoadException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x001A3CB9 File Offset: 0x001A1EB9
		protected ABProviderLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x001A3CC3 File Offset: 0x001A1EC3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
