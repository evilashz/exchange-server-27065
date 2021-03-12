using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E66 RID: 3686
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPasswordIncludesInvalidCharsException : WLCDMemberException
	{
		// Token: 0x0600A6E6 RID: 42726 RVA: 0x00287DFF File Offset: 0x00285FFF
		public WLCDPasswordIncludesInvalidCharsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6E7 RID: 42727 RVA: 0x00287E08 File Offset: 0x00286008
		public WLCDPasswordIncludesInvalidCharsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6E8 RID: 42728 RVA: 0x00287E12 File Offset: 0x00286012
		protected WLCDPasswordIncludesInvalidCharsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6E9 RID: 42729 RVA: 0x00287E1C File Offset: 0x0028601C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
