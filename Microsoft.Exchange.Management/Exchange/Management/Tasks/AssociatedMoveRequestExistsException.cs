using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF3 RID: 3827
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssociatedMoveRequestExistsException : LocalizedException
	{
		// Token: 0x0600A9A8 RID: 43432 RVA: 0x0028C1F7 File Offset: 0x0028A3F7
		public AssociatedMoveRequestExistsException() : base(Strings.ErrorAssociatedMoveRequestExists)
		{
		}

		// Token: 0x0600A9A9 RID: 43433 RVA: 0x0028C204 File Offset: 0x0028A404
		public AssociatedMoveRequestExistsException(Exception innerException) : base(Strings.ErrorAssociatedMoveRequestExists, innerException)
		{
		}

		// Token: 0x0600A9AA RID: 43434 RVA: 0x0028C212 File Offset: 0x0028A412
		protected AssociatedMoveRequestExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A9AB RID: 43435 RVA: 0x0028C21C File Offset: 0x0028A41C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
