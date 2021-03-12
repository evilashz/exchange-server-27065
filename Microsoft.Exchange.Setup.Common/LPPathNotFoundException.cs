using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000081 RID: 129
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LPPathNotFoundException : LocalizedException
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x000167FD File Offset: 0x000149FD
		public LPPathNotFoundException() : base(Strings.LanguagePackPathNotFoundError)
		{
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001680A File Offset: 0x00014A0A
		public LPPathNotFoundException(Exception innerException) : base(Strings.LanguagePackPathNotFoundError, innerException)
		{
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00016818 File Offset: 0x00014A18
		protected LPPathNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00016822 File Offset: 0x00014A22
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
