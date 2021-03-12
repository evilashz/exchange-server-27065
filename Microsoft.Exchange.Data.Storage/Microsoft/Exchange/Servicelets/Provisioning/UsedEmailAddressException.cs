using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200013B RID: 315
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UsedEmailAddressException : ProcessingException
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x0006B9C4 File Offset: 0x00069BC4
		public UsedEmailAddressException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0006B9CD File Offset: 0x00069BCD
		public UsedEmailAddressException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0006B9D7 File Offset: 0x00069BD7
		protected UsedEmailAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0006B9E1 File Offset: 0x00069BE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
