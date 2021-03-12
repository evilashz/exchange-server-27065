using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200101D RID: 4125
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetBothEhfAndFfoRoutingException : LocalizedException
	{
		// Token: 0x0600AF4C RID: 44876 RVA: 0x002942D8 File Offset: 0x002924D8
		public CannotSetBothEhfAndFfoRoutingException() : base(Strings.CannotSetBothEhfAndFfoRoutingId)
		{
		}

		// Token: 0x0600AF4D RID: 44877 RVA: 0x002942E5 File Offset: 0x002924E5
		public CannotSetBothEhfAndFfoRoutingException(Exception innerException) : base(Strings.CannotSetBothEhfAndFfoRoutingId, innerException)
		{
		}

		// Token: 0x0600AF4E RID: 44878 RVA: 0x002942F3 File Offset: 0x002924F3
		protected CannotSetBothEhfAndFfoRoutingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF4F RID: 44879 RVA: 0x002942FD File Offset: 0x002924FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
