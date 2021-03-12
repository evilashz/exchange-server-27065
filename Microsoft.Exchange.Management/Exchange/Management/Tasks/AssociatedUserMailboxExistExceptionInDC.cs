using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF2 RID: 3826
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssociatedUserMailboxExistExceptionInDC : LocalizedException
	{
		// Token: 0x0600A9A4 RID: 43428 RVA: 0x0028C1C8 File Offset: 0x0028A3C8
		public AssociatedUserMailboxExistExceptionInDC() : base(Strings.ErrorAssociatedUserMailboxExistInDC)
		{
		}

		// Token: 0x0600A9A5 RID: 43429 RVA: 0x0028C1D5 File Offset: 0x0028A3D5
		public AssociatedUserMailboxExistExceptionInDC(Exception innerException) : base(Strings.ErrorAssociatedUserMailboxExistInDC, innerException)
		{
		}

		// Token: 0x0600A9A6 RID: 43430 RVA: 0x0028C1E3 File Offset: 0x0028A3E3
		protected AssociatedUserMailboxExistExceptionInDC(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A9A7 RID: 43431 RVA: 0x0028C1ED File Offset: 0x0028A3ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
