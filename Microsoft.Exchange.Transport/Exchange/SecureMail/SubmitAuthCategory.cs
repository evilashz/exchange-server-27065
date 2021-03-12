using System;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x0200051D RID: 1309
	internal class SubmitAuthCategory
	{
		// Token: 0x06003D35 RID: 15669 RVA: 0x000FFAD8 File Offset: 0x000FDCD8
		private SubmitAuthCategory(string name, bool recordDomain)
		{
			this.name = name;
			this.logFormat = "{0:x2}" + name.Substring(0, 1) + (recordDomain ? ":{1}" : ":");
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000FFB0E File Offset: 0x000FDD0E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000FFB16 File Offset: 0x000FDD16
		public bool IsAnonymous
		{
			get
			{
				return this == SubmitAuthCategory.Anonymous;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000FFB20 File Offset: 0x000FDD20
		public bool IsPartner
		{
			get
			{
				return this == SubmitAuthCategory.Partner;
			}
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000FFB2A File Offset: 0x000FDD2A
		public static string FormatExisting(MultilevelAuthMechanism mechanism, string category)
		{
			if (string.IsNullOrEmpty(category))
			{
				category = SubmitAuthCategory.Other.Name;
			}
			return string.Format("{0:x2}{1}:", (int)mechanism, category.Substring(0, 1));
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000FFB58 File Offset: 0x000FDD58
		public bool Matches(string optCategoryName)
		{
			return string.Equals(optCategoryName, this.Name, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000FFB67 File Offset: 0x000FDD67
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000FFB6F File Offset: 0x000FDD6F
		public string FormatLog(MultilevelAuthMechanism mechanism, string authDomain)
		{
			if (!MultilevelAuth.MayWriteAuthDomain(mechanism))
			{
				authDomain = null;
			}
			return string.Format(this.logFormat, (int)mechanism, authDomain);
		}

		// Token: 0x04001F21 RID: 7969
		public static readonly SubmitAuthCategory Anonymous = new SubmitAuthCategory("Anonymous", false);

		// Token: 0x04001F22 RID: 7970
		public static readonly SubmitAuthCategory Other = new SubmitAuthCategory("Other", true);

		// Token: 0x04001F23 RID: 7971
		public static readonly SubmitAuthCategory Internal = new SubmitAuthCategory("Internal", true);

		// Token: 0x04001F24 RID: 7972
		public static readonly SubmitAuthCategory External = new SubmitAuthCategory("External", true);

		// Token: 0x04001F25 RID: 7973
		public static readonly SubmitAuthCategory Partner = new SubmitAuthCategory("Partner", true);

		// Token: 0x04001F26 RID: 7974
		private string name;

		// Token: 0x04001F27 RID: 7975
		private string logFormat;
	}
}
