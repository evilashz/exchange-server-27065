using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000DD7 RID: 3543
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFqdnException : LocalizedException
	{
		// Token: 0x0600A421 RID: 42017 RVA: 0x00283BA5 File Offset: 0x00281DA5
		public InvalidFqdnException() : base(Strings.InvalidFqdnFromAD)
		{
		}

		// Token: 0x0600A422 RID: 42018 RVA: 0x00283BB2 File Offset: 0x00281DB2
		public InvalidFqdnException(Exception innerException) : base(Strings.InvalidFqdnFromAD, innerException)
		{
		}

		// Token: 0x0600A423 RID: 42019 RVA: 0x00283BC0 File Offset: 0x00281DC0
		protected InvalidFqdnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A424 RID: 42020 RVA: 0x00283BCA File Offset: 0x00281DCA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
