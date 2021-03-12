using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002B1 RID: 689
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SupportedVersionListFormatException : InvalidOperationException
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x0005C762 File Offset: 0x0005A962
		public SupportedVersionListFormatException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0005C770 File Offset: 0x0005A970
		public SupportedVersionListFormatException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0005C77F File Offset: 0x0005A97F
		protected SupportedVersionListFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0005C789 File Offset: 0x0005A989
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
