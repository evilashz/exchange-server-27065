using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AAE RID: 2734
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LocalServerException : LocalizedException
	{
		// Token: 0x0600802A RID: 32810 RVA: 0x001A4E9C File Offset: 0x001A309C
		public LocalServerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x001A4EA5 File Offset: 0x001A30A5
		public LocalServerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x001A4EAF File Offset: 0x001A30AF
		protected LocalServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x001A4EB9 File Offset: 0x001A30B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
