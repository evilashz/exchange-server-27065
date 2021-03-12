using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AEB RID: 2795
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PartnerApplicationNotFoundException : LocalizedException
	{
		// Token: 0x06008145 RID: 33093 RVA: 0x001A64E9 File Offset: 0x001A46E9
		public PartnerApplicationNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x001A64F2 File Offset: 0x001A46F2
		public PartnerApplicationNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x001A64FC File Offset: 0x001A46FC
		protected PartnerApplicationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x001A6506 File Offset: 0x001A4706
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
