using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001B8 RID: 440
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DeleteFailedException : LocalizedException
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x000358CB File Offset: 0x00033ACB
		public DeleteFailedException() : base(Strings.DeleteFailed)
		{
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000358D8 File Offset: 0x00033AD8
		public DeleteFailedException(Exception innerException) : base(Strings.DeleteFailed, innerException)
		{
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000358E6 File Offset: 0x00033AE6
		protected DeleteFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000358F0 File Offset: 0x00033AF0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
