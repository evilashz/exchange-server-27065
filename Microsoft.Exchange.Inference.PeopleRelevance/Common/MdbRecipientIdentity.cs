using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal class MdbRecipientIdentity : IIdentity, IEquatable<IIdentity>
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000306A File Offset: 0x0000126A
		internal MdbRecipientIdentity(string smtpAddress)
		{
			if (string.IsNullOrEmpty(smtpAddress))
			{
				throw new ArgumentException("SMTP address is null or empty");
			}
			this.smtpAddress = smtpAddress.ToUpper();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003091 File Offset: 0x00001291
		internal string SmptAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003099 File Offset: 0x00001299
		public override bool Equals(object other)
		{
			return this.Equals(other as IIdentity);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000030A7 File Offset: 0x000012A7
		public virtual bool Equals(IIdentity other)
		{
			return this.Equals(other as MdbRecipientIdentity);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000030B5 File Offset: 0x000012B5
		public override int GetHashCode()
		{
			return this.SmptAddress.GetHashCode();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000030C2 File Offset: 0x000012C2
		public override string ToString()
		{
			return this.SmptAddress;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000030CA File Offset: 0x000012CA
		private bool Equals(MdbRecipientIdentity other)
		{
			return other != null && (object.ReferenceEquals(other, this) || string.Equals(this.SmptAddress, other.SmptAddress, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0400002D RID: 45
		private readonly string smtpAddress;
	}
}
