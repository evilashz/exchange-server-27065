using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000259 RID: 601
	internal struct SubscriptionCacheEntry : IComparable<SubscriptionCacheEntry>
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x0007B774 File Offset: 0x00079974
		public SubscriptionCacheEntry(Guid id, string address, string displayName, byte[] instanceKey, CultureInfo culture)
		{
			this.id = id;
			this.address = address;
			this.displayName = displayName;
			this.instanceKey = instanceKey;
			this.culture = culture;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0007B79B File Offset: 0x0007999B
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x0007B7A3 File Offset: 0x000799A3
		public Guid Id
		{
			get
			{
				return this.id;
			}
			internal set
			{
				this.id = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0007B7AC File Offset: 0x000799AC
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x0007B7B4 File Offset: 0x000799B4
		public string Address
		{
			get
			{
				return this.address;
			}
			internal set
			{
				this.address = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0007B7BD File Offset: 0x000799BD
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x0007B7C5 File Offset: 0x000799C5
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			internal set
			{
				this.displayName = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0007B7CE File Offset: 0x000799CE
		public byte[] InstanceKey
		{
			get
			{
				return this.instanceKey;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0007B7D6 File Offset: 0x000799D6
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x0007B7DE File Offset: 0x000799DE
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			internal set
			{
				this.culture = value;
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0007B7E7 File Offset: 0x000799E7
		public int CompareTo(SubscriptionCacheEntry other)
		{
			return this.CompareCultureSensitiveStrings(this.address, other.address);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0007B7FC File Offset: 0x000799FC
		public bool DisplayNameMatch(SubscriptionCacheEntry other)
		{
			return 0 == this.CompareCultureSensitiveStrings(this.displayName, other.displayName);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0007B814 File Offset: 0x00079A14
		public bool MatchOnInstanceKey(byte[] otherInstanceKey)
		{
			if (this.instanceKey == null || otherInstanceKey == null)
			{
				return false;
			}
			for (int i = 0; i < this.instanceKey.Length; i++)
			{
				if (this.instanceKey[i] != otherInstanceKey[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0007B854 File Offset: 0x00079A54
		public void RenderToJavascript(TextWriter writer)
		{
			RecipientInfoCacheEntry entry = new RecipientInfoCacheEntry(this.displayName, this.address, this.address, null, "SMTP", AddressOrigin.OneOff, 0, null, EmailAddressIndex.None, null, null);
			AutoCompleteCacheEntry.RenderEntryJavascript(writer, entry);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0007B88C File Offset: 0x00079A8C
		private int CompareCultureSensitiveStrings(string source, string target)
		{
			CultureInfo cultureInfo = (this.culture == null) ? CultureInfo.InvariantCulture : this.culture;
			return cultureInfo.CompareInfo.Compare(source, target, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x04000DCE RID: 3534
		private Guid id;

		// Token: 0x04000DCF RID: 3535
		private string address;

		// Token: 0x04000DD0 RID: 3536
		private string displayName;

		// Token: 0x04000DD1 RID: 3537
		private byte[] instanceKey;

		// Token: 0x04000DD2 RID: 3538
		private CultureInfo culture;
	}
}
