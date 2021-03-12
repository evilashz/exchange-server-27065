using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E5C RID: 3676
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMemberNameInUseException : WLCDMemberException
	{
		// Token: 0x0600A6BE RID: 42686 RVA: 0x00287C79 File Offset: 0x00285E79
		public WLCDMemberNameInUseException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6BF RID: 42687 RVA: 0x00287C82 File Offset: 0x00285E82
		public WLCDMemberNameInUseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6C0 RID: 42688 RVA: 0x00287C8C File Offset: 0x00285E8C
		protected WLCDMemberNameInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6C1 RID: 42689 RVA: 0x00287C96 File Offset: 0x00285E96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
