using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DFB RID: 3579
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RootDomainNotFoundException : LocalizedException
	{
		// Token: 0x0600A4E3 RID: 42211 RVA: 0x0028517C File Offset: 0x0028337C
		public RootDomainNotFoundException() : base(Strings.RootDomainNotFoundException)
		{
		}

		// Token: 0x0600A4E4 RID: 42212 RVA: 0x00285189 File Offset: 0x00283389
		public RootDomainNotFoundException(Exception innerException) : base(Strings.RootDomainNotFoundException, innerException)
		{
		}

		// Token: 0x0600A4E5 RID: 42213 RVA: 0x00285197 File Offset: 0x00283397
		protected RootDomainNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4E6 RID: 42214 RVA: 0x002851A1 File Offset: 0x002833A1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
