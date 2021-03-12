using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010ED RID: 4333
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCspForCryptoModeInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B38D RID: 45965 RVA: 0x0029B61F File Offset: 0x0029981F
		public InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(string cspName, int cryptoMode) : base(Strings.InvalidCspForCryptoModeInImportedTrustedPublishingDomain(cspName, cryptoMode))
		{
			this.cspName = cspName;
			this.cryptoMode = cryptoMode;
		}

		// Token: 0x0600B38E RID: 45966 RVA: 0x0029B63C File Offset: 0x0029983C
		public InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(string cspName, int cryptoMode, Exception innerException) : base(Strings.InvalidCspForCryptoModeInImportedTrustedPublishingDomain(cspName, cryptoMode), innerException)
		{
			this.cspName = cspName;
			this.cryptoMode = cryptoMode;
		}

		// Token: 0x0600B38F RID: 45967 RVA: 0x0029B65C File Offset: 0x0029985C
		protected InvalidCspForCryptoModeInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cspName = (string)info.GetValue("cspName", typeof(string));
			this.cryptoMode = (int)info.GetValue("cryptoMode", typeof(int));
		}

		// Token: 0x0600B390 RID: 45968 RVA: 0x0029B6B1 File Offset: 0x002998B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cspName", this.cspName);
			info.AddValue("cryptoMode", this.cryptoMode);
		}

		// Token: 0x170038F6 RID: 14582
		// (get) Token: 0x0600B391 RID: 45969 RVA: 0x0029B6DD File Offset: 0x002998DD
		public string CspName
		{
			get
			{
				return this.cspName;
			}
		}

		// Token: 0x170038F7 RID: 14583
		// (get) Token: 0x0600B392 RID: 45970 RVA: 0x0029B6E5 File Offset: 0x002998E5
		public int CryptoMode
		{
			get
			{
				return this.cryptoMode;
			}
		}

		// Token: 0x0400625C RID: 25180
		private readonly string cspName;

		// Token: 0x0400625D RID: 25181
		private readonly int cryptoMode;
	}
}
