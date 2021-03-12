using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DFC RID: 3580
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LocalDomainNotFoundException : LocalizedException
	{
		// Token: 0x0600A4E7 RID: 42215 RVA: 0x002851AB File Offset: 0x002833AB
		public LocalDomainNotFoundException() : base(Strings.LocalDomainNotFoundException)
		{
		}

		// Token: 0x0600A4E8 RID: 42216 RVA: 0x002851B8 File Offset: 0x002833B8
		public LocalDomainNotFoundException(Exception innerException) : base(Strings.LocalDomainNotFoundException, innerException)
		{
		}

		// Token: 0x0600A4E9 RID: 42217 RVA: 0x002851C6 File Offset: 0x002833C6
		protected LocalDomainNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4EA RID: 42218 RVA: 0x002851D0 File Offset: 0x002833D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
