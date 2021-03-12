using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E4 RID: 4324
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDecryptImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B361 RID: 45921 RVA: 0x0029B203 File Offset: 0x00299403
		public FailedToDecryptImportedTrustedPublishingDomainException(Exception ex) : base(Strings.FailedToDecryptImportedTrustedPublishingDomain(ex))
		{
			this.ex = ex;
		}

		// Token: 0x0600B362 RID: 45922 RVA: 0x0029B218 File Offset: 0x00299418
		public FailedToDecryptImportedTrustedPublishingDomainException(Exception ex, Exception innerException) : base(Strings.FailedToDecryptImportedTrustedPublishingDomain(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x0600B363 RID: 45923 RVA: 0x0029B22E File Offset: 0x0029942E
		protected FailedToDecryptImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B364 RID: 45924 RVA: 0x0029B258 File Offset: 0x00299458
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x170038EE RID: 14574
		// (get) Token: 0x0600B365 RID: 45925 RVA: 0x0029B273 File Offset: 0x00299473
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006254 RID: 25172
		private readonly Exception ex;
	}
}
