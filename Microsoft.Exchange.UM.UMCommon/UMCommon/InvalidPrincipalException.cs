using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A8 RID: 424
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPrincipalException : LocalizedException
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x00035106 File Offset: 0x00033306
		public InvalidPrincipalException() : base(Strings.InvalidPrincipalException)
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00035113 File Offset: 0x00033313
		public InvalidPrincipalException(Exception innerException) : base(Strings.InvalidPrincipalException, innerException)
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00035121 File Offset: 0x00033321
		protected InvalidPrincipalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003512B File Offset: 0x0003332B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
