using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200007D RID: 125
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADRelatedUnknownErrorException : LocalizedException
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x000166F8 File Offset: 0x000148F8
		public ADRelatedUnknownErrorException() : base(Strings.ADRelatedUnknownError)
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00016705 File Offset: 0x00014905
		public ADRelatedUnknownErrorException(Exception innerException) : base(Strings.ADRelatedUnknownError, innerException)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00016713 File Offset: 0x00014913
		protected ADRelatedUnknownErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001671D File Offset: 0x0001491D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
