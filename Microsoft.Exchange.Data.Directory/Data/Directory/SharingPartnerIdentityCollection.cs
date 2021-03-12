using System;
using System.Globalization;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	internal sealed class SharingPartnerIdentityCollection : IEquatable<SharingPartnerIdentityCollection>
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0005596F File Offset: 0x00053B6F
		internal MultiValuedProperty<string> InnerCollection
		{
			get
			{
				return this.innerCollection;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00055977 File Offset: 0x00053B77
		private StringHasher StringHasher
		{
			get
			{
				if (this.stringHasher == null)
				{
					this.stringHasher = new StringHasher();
				}
				return this.stringHasher;
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00055992 File Offset: 0x00053B92
		internal SharingPartnerIdentityCollection(MultiValuedProperty<string> mvp)
		{
			if (mvp == null)
			{
				throw new ArgumentNullException("mvp");
			}
			this.innerCollection = mvp;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000559AF File Offset: 0x00053BAF
		public bool Changed
		{
			get
			{
				return this.InnerCollection.Changed;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x000559BC File Offset: 0x00053BBC
		public int Count
		{
			get
			{
				return this.InnerCollection.Count;
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000559C9 File Offset: 0x00053BC9
		public bool Contains(string externalId)
		{
			return this.InnerCollection.Contains(this.GetSharingPartnerIdentity(externalId));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000559DD File Offset: 0x00053BDD
		public void Add(string externalId)
		{
			this.InnerCollection.Add(this.GetSharingPartnerIdentity(externalId));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000559F1 File Offset: 0x00053BF1
		public bool Remove(string externalId)
		{
			return this.InnerCollection.Remove(this.GetSharingPartnerIdentity(externalId));
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00055A08 File Offset: 0x00053C08
		private string GetSharingPartnerIdentity(string externalId)
		{
			string input = externalId.Trim();
			return this.StringHasher.GetHash(input).ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00055A35 File Offset: 0x00053C35
		public bool Equals(SharingPartnerIdentityCollection other)
		{
			return other != null && this.InnerCollection.Equals(other.InnerCollection);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00055A4D File Offset: 0x00053C4D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SharingPartnerIdentityCollection);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00055A5B File Offset: 0x00053C5B
		public override int GetHashCode()
		{
			return this.InnerCollection.GetHashCode();
		}

		// Token: 0x04000A34 RID: 2612
		private readonly MultiValuedProperty<string> innerCollection;

		// Token: 0x04000A35 RID: 2613
		private StringHasher stringHasher;
	}
}
