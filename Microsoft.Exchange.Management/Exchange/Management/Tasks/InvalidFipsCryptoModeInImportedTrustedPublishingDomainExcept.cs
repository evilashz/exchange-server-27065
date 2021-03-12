using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010EE RID: 4334
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFipsCryptoModeInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B393 RID: 45971 RVA: 0x0029B6ED File Offset: 0x002998ED
		public InvalidFipsCryptoModeInImportedTrustedPublishingDomainException(int cryptoMode) : base(Strings.invalidFipsCryptoModeInImportedTrustedPublishingDomain(cryptoMode))
		{
			this.cryptoMode = cryptoMode;
		}

		// Token: 0x0600B394 RID: 45972 RVA: 0x0029B702 File Offset: 0x00299902
		public InvalidFipsCryptoModeInImportedTrustedPublishingDomainException(int cryptoMode, Exception innerException) : base(Strings.invalidFipsCryptoModeInImportedTrustedPublishingDomain(cryptoMode), innerException)
		{
			this.cryptoMode = cryptoMode;
		}

		// Token: 0x0600B395 RID: 45973 RVA: 0x0029B718 File Offset: 0x00299918
		protected InvalidFipsCryptoModeInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cryptoMode = (int)info.GetValue("cryptoMode", typeof(int));
		}

		// Token: 0x0600B396 RID: 45974 RVA: 0x0029B742 File Offset: 0x00299942
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cryptoMode", this.cryptoMode);
		}

		// Token: 0x170038F8 RID: 14584
		// (get) Token: 0x0600B397 RID: 45975 RVA: 0x0029B75D File Offset: 0x0029995D
		public int CryptoMode
		{
			get
			{
				return this.cryptoMode;
			}
		}

		// Token: 0x0400625E RID: 25182
		private readonly int cryptoMode;
	}
}
