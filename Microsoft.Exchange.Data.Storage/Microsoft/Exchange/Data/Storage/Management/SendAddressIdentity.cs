using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A51 RID: 2641
	[Serializable]
	public class SendAddressIdentity : ObjectId, IEquatable<SendAddressIdentity>
	{
		// Token: 0x06006086 RID: 24710 RVA: 0x00197135 File Offset: 0x00195335
		public SendAddressIdentity()
		{
			this.isUniqueIdentity = false;
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x00197144 File Offset: 0x00195344
		public SendAddressIdentity(string mailboxIdParameterString, string addressId)
		{
			if (mailboxIdParameterString == null)
			{
				throw new ArgumentNullException("userId");
			}
			if (mailboxIdParameterString.Length == 0)
			{
				throw new ArgumentException("mailboxIdParameterString cannot be empty.", "mailboxIdParameterString");
			}
			if (addressId == null)
			{
				throw new ArgumentNullException("addressId");
			}
			this.isUniqueIdentity = true;
			this.mailboxIdParameterString = mailboxIdParameterString;
			this.addressId = addressId;
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x001971A0 File Offset: 0x001953A0
		public SendAddressIdentity(string stringIdentity)
		{
			if (stringIdentity == null)
			{
				throw new ArgumentNullException("stringIdentity");
			}
			if (stringIdentity.Length == 0)
			{
				throw new ArgumentException("stringIdentity was set to empty.", "stringIdentity");
			}
			int num = stringIdentity.LastIndexOf('\\');
			if (num <= 0)
			{
				throw new ArgumentException(ServerStrings.InvalidSendAddressIdentity, "id");
			}
			this.isUniqueIdentity = true;
			this.mailboxIdParameterString = stringIdentity.Substring(0, num);
			this.addressId = ((num == stringIdentity.Length - 1) ? string.Empty : stringIdentity.Substring(num + 1));
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x00197231 File Offset: 0x00195431
		public bool IsUniqueIdentity
		{
			get
			{
				return this.isUniqueIdentity;
			}
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x0600608A RID: 24714 RVA: 0x00197239 File Offset: 0x00195439
		public string MailboxIdParameterString
		{
			get
			{
				return this.mailboxIdParameterString;
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x00197241 File Offset: 0x00195441
		public string AddressId
		{
			get
			{
				return this.addressId;
			}
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x00197249 File Offset: 0x00195449
		public bool Equals(SendAddressIdentity other)
		{
			return other != null && this.mailboxIdParameterString.Equals(other.mailboxIdParameterString) && this.addressId.Equals(other.addressId);
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x00197274 File Offset: 0x00195474
		public override byte[] GetBytes()
		{
			return null;
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x00197278 File Offset: 0x00195478
		public override string ToString()
		{
			if (this.mailboxIdParameterString == null || this.addressId == null)
			{
				return string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				this.mailboxIdParameterString,
				'\\',
				this.addressId
			});
		}

		// Token: 0x040036EF RID: 14063
		private const char Separator = '\\';

		// Token: 0x040036F0 RID: 14064
		private string mailboxIdParameterString;

		// Token: 0x040036F1 RID: 14065
		private string addressId;

		// Token: 0x040036F2 RID: 14066
		private bool isUniqueIdentity;
	}
}
