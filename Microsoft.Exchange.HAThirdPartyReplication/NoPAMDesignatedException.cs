using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoPAMDesignatedException : ThirdPartyReplicationException
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000310D File Offset: 0x0000130D
		public NoPAMDesignatedException() : base(ThirdPartyReplication.NoPAMDesignated)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000311F File Offset: 0x0000131F
		public NoPAMDesignatedException(Exception innerException) : base(ThirdPartyReplication.NoPAMDesignated, innerException)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003132 File Offset: 0x00001332
		protected NoPAMDesignatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000313C File Offset: 0x0000133C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
