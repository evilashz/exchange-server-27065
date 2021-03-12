using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E5A RID: 3674
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDInvalidMemberNameException : WLCDMemberException
	{
		// Token: 0x0600A6B6 RID: 42678 RVA: 0x00287C2B File Offset: 0x00285E2B
		public WLCDInvalidMemberNameException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6B7 RID: 42679 RVA: 0x00287C34 File Offset: 0x00285E34
		public WLCDInvalidMemberNameException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6B8 RID: 42680 RVA: 0x00287C3E File Offset: 0x00285E3E
		protected WLCDInvalidMemberNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6B9 RID: 42681 RVA: 0x00287C48 File Offset: 0x00285E48
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
