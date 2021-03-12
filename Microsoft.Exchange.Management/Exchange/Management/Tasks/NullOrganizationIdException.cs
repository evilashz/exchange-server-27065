using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200110D RID: 4365
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullOrganizationIdException : LocalizedException
	{
		// Token: 0x0600B432 RID: 46130 RVA: 0x0029C714 File Offset: 0x0029A914
		public NullOrganizationIdException() : base(Strings.NullCurrentOrganizationIdException)
		{
		}

		// Token: 0x0600B433 RID: 46131 RVA: 0x0029C721 File Offset: 0x0029A921
		public NullOrganizationIdException(Exception innerException) : base(Strings.NullCurrentOrganizationIdException, innerException)
		{
		}

		// Token: 0x0600B434 RID: 46132 RVA: 0x0029C72F File Offset: 0x0029A92F
		protected NullOrganizationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B435 RID: 46133 RVA: 0x0029C739 File Offset: 0x0029A939
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
