using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000043 RID: 67
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3MirroredAccountNotPossibleException : LocalizedException
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00005ED0 File Offset: 0x000040D0
		public Pop3MirroredAccountNotPossibleException() : base(Strings.Pop3MirroredAccountNotPossibleException)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005EDD File Offset: 0x000040DD
		public Pop3MirroredAccountNotPossibleException(Exception innerException) : base(Strings.Pop3MirroredAccountNotPossibleException, innerException)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005EEB File Offset: 0x000040EB
		protected Pop3MirroredAccountNotPossibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005EF5 File Offset: 0x000040F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
