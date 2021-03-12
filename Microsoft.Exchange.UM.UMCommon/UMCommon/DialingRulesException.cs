using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A5 RID: 421
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DialingRulesException : LocalizedException
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x00034FE7 File Offset: 0x000331E7
		public DialingRulesException() : base(Strings.DialingRulesException)
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00034FF4 File Offset: 0x000331F4
		public DialingRulesException(Exception innerException) : base(Strings.DialingRulesException, innerException)
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00035002 File Offset: 0x00033202
		protected DialingRulesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003500C File Offset: 0x0003320C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
