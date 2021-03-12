using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF1 RID: 3825
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssociatedUserMailboxExistException : LocalizedException
	{
		// Token: 0x0600A9A0 RID: 43424 RVA: 0x0028C199 File Offset: 0x0028A399
		public AssociatedUserMailboxExistException() : base(Strings.ErrorAssociatedUserMailboxExist)
		{
		}

		// Token: 0x0600A9A1 RID: 43425 RVA: 0x0028C1A6 File Offset: 0x0028A3A6
		public AssociatedUserMailboxExistException(Exception innerException) : base(Strings.ErrorAssociatedUserMailboxExist, innerException)
		{
		}

		// Token: 0x0600A9A2 RID: 43426 RVA: 0x0028C1B4 File Offset: 0x0028A3B4
		protected AssociatedUserMailboxExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A9A3 RID: 43427 RVA: 0x0028C1BE File Offset: 0x0028A3BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
