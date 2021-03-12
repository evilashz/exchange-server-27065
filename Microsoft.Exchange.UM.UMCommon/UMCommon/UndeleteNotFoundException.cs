using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B9 RID: 441
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UndeleteNotFoundException : LocalizedException
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x000358FA File Offset: 0x00033AFA
		public UndeleteNotFoundException() : base(Strings.UndeleteNotFound)
		{
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00035907 File Offset: 0x00033B07
		public UndeleteNotFoundException(Exception innerException) : base(Strings.UndeleteNotFound, innerException)
		{
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00035915 File Offset: 0x00033B15
		protected UndeleteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0003591F File Offset: 0x00033B1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
