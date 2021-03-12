using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DFE RID: 3582
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MicrosoftExchangeContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600A4EF RID: 42223 RVA: 0x00285209 File Offset: 0x00283409
		public MicrosoftExchangeContainerNotFoundException() : base(Strings.MicrosoftExchangeContainerNotFoundException)
		{
		}

		// Token: 0x0600A4F0 RID: 42224 RVA: 0x00285216 File Offset: 0x00283416
		public MicrosoftExchangeContainerNotFoundException(Exception innerException) : base(Strings.MicrosoftExchangeContainerNotFoundException, innerException)
		{
		}

		// Token: 0x0600A4F1 RID: 42225 RVA: 0x00285224 File Offset: 0x00283424
		protected MicrosoftExchangeContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4F2 RID: 42226 RVA: 0x0028522E File Offset: 0x0028342E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
