using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E68 RID: 3688
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDInvalidNetIdException : WLCDMemberException
	{
		// Token: 0x0600A6EE RID: 42734 RVA: 0x00287E4D File Offset: 0x0028604D
		public WLCDInvalidNetIdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6EF RID: 42735 RVA: 0x00287E56 File Offset: 0x00286056
		public WLCDInvalidNetIdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6F0 RID: 42736 RVA: 0x00287E60 File Offset: 0x00286060
		protected WLCDInvalidNetIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6F1 RID: 42737 RVA: 0x00287E6A File Offset: 0x0028606A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
