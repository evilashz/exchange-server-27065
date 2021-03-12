using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001C1 RID: 449
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SipResourceIdAndExtensionsNeededException : LocalizedException
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x00035CB7 File Offset: 0x00033EB7
		public SipResourceIdAndExtensionsNeededException() : base(Strings.SipResourceIdAndExtensionsNeeded)
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00035CC4 File Offset: 0x00033EC4
		public SipResourceIdAndExtensionsNeededException(Exception innerException) : base(Strings.SipResourceIdAndExtensionsNeeded, innerException)
		{
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00035CD2 File Offset: 0x00033ED2
		protected SipResourceIdAndExtensionsNeededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00035CDC File Offset: 0x00033EDC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
