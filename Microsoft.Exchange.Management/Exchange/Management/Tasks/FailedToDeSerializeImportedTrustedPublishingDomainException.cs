using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E3 RID: 4323
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDeSerializeImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B35C RID: 45916 RVA: 0x0029B18B File Offset: 0x0029938B
		public FailedToDeSerializeImportedTrustedPublishingDomainException(Exception ex) : base(Strings.FailedToDeSerializeImportedTrustedPublishingDomain(ex))
		{
			this.ex = ex;
		}

		// Token: 0x0600B35D RID: 45917 RVA: 0x0029B1A0 File Offset: 0x002993A0
		public FailedToDeSerializeImportedTrustedPublishingDomainException(Exception ex, Exception innerException) : base(Strings.FailedToDeSerializeImportedTrustedPublishingDomain(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x0600B35E RID: 45918 RVA: 0x0029B1B6 File Offset: 0x002993B6
		protected FailedToDeSerializeImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B35F RID: 45919 RVA: 0x0029B1E0 File Offset: 0x002993E0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x170038ED RID: 14573
		// (get) Token: 0x0600B360 RID: 45920 RVA: 0x0029B1FB File Offset: 0x002993FB
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006253 RID: 25171
		private readonly Exception ex;
	}
}
