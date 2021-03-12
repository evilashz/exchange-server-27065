using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000336 RID: 822
	[Serializable]
	internal sealed class LiveDomainServicesException : FederationException
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0007C388 File Offset: 0x0007A588
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0007C390 File Offset: 0x0007A590
		public DomainError? DomainError { get; set; }

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0007C399 File Offset: 0x0007A599
		public LiveDomainServicesException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0007C3A6 File Offset: 0x0007A5A6
		public LiveDomainServicesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0007C3B0 File Offset: 0x0007A5B0
		public LiveDomainServicesException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0007C3B9 File Offset: 0x0007A5B9
		public LiveDomainServicesException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0007C3C3 File Offset: 0x0007A5C3
		public LiveDomainServicesException(DomainError domainError, LocalizedString message) : base(message)
		{
			this.DomainError = new DomainError?(domainError);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0007C3D8 File Offset: 0x0007A5D8
		public LiveDomainServicesException(DomainError domainError, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.DomainError = new DomainError?(domainError);
		}
	}
}
