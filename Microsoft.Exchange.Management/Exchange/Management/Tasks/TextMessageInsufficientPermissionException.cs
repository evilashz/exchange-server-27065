using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FBF RID: 4031
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TextMessageInsufficientPermissionException : LocalizedException
	{
		// Token: 0x0600AD91 RID: 44433 RVA: 0x00291E16 File Offset: 0x00290016
		public TextMessageInsufficientPermissionException() : base(Strings.ErrorTextMessageInsufficientPermission)
		{
		}

		// Token: 0x0600AD92 RID: 44434 RVA: 0x00291E23 File Offset: 0x00290023
		public TextMessageInsufficientPermissionException(Exception innerException) : base(Strings.ErrorTextMessageInsufficientPermission, innerException)
		{
		}

		// Token: 0x0600AD93 RID: 44435 RVA: 0x00291E31 File Offset: 0x00290031
		protected TextMessageInsufficientPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD94 RID: 44436 RVA: 0x00291E3B File Offset: 0x0029003B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
