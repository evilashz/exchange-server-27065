using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000080 RID: 128
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToReadLCIDFromRegistryException : LocalizedException
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x000167CE File Offset: 0x000149CE
		public FailedToReadLCIDFromRegistryException() : base(Strings.FailedToReadLCIDFromRegistryError)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000167DB File Offset: 0x000149DB
		public FailedToReadLCIDFromRegistryException(Exception innerException) : base(Strings.FailedToReadLCIDFromRegistryError, innerException)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000167E9 File Offset: 0x000149E9
		protected FailedToReadLCIDFromRegistryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000167F3 File Offset: 0x000149F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
