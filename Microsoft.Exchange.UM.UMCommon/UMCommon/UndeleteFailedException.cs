using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001BA RID: 442
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UndeleteFailedException : LocalizedException
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x00035929 File Offset: 0x00033B29
		public UndeleteFailedException() : base(Strings.UndeleteFailed)
		{
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00035936 File Offset: 0x00033B36
		public UndeleteFailedException(Exception innerException) : base(Strings.UndeleteFailed, innerException)
		{
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00035944 File Offset: 0x00033B44
		protected UndeleteFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003594E File Offset: 0x00033B4E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
