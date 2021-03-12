using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000363 RID: 867
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptRestrictionDataException : MailboxReplicationPermanentException
	{
		// Token: 0x060026AD RID: 9901 RVA: 0x00053833 File Offset: 0x00051A33
		public CorruptRestrictionDataException() : base(MrsStrings.CorruptRestrictionData)
		{
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x00053840 File Offset: 0x00051A40
		public CorruptRestrictionDataException(Exception innerException) : base(MrsStrings.CorruptRestrictionData, innerException)
		{
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0005384E File Offset: 0x00051A4E
		protected CorruptRestrictionDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x00053858 File Offset: 0x00051A58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
