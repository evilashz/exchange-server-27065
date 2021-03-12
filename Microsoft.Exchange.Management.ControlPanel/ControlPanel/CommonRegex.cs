using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000547 RID: 1351
	public static class CommonRegex
	{
		// Token: 0x06003F8C RID: 16268 RVA: 0x000BFCE4 File Offset: 0x000BDEE4
		static CommonRegex()
		{
			CommonRegex.DsnCode = CommonRegex.CreateRegex("DsnCode", "^[2|4|5]\\.[0-9]\\.[0-9]$");
		}

		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000BFD88 File Offset: 0x000BDF88
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000BFD8F File Offset: 0x000BDF8F
		public static Regex Domain { get; private set; } = CommonRegex.CreateRegex("Domain", "^[-a-zA-Z0-9_.]+$");

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000BFD97 File Offset: 0x000BDF97
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000BFD9E File Offset: 0x000BDF9E
		public static Regex DsnCode { get; private set; }

		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000BFDA6 File Offset: 0x000BDFA6
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x000BFDAD File Offset: 0x000BDFAD
		public static Regex EmailOrDomain { get; private set; } = CommonRegex.CreateRegex("EmailOrDomain", "^@?[a-zA-Z0-9-_]+(\\.[a-z-A-Z0-9-_]+)+$|^[a-zA-Z0-9-_\\.]+@[a-zA-Z0-9-_]+(\\.[a-z-A-Z0-9-_]+)+$");

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000BFDB5 File Offset: 0x000BDFB5
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x000BFDBC File Offset: 0x000BDFBC
		public static Regex IpAddress { get; private set; } = CommonRegex.CreateRegex("IpAddress", "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])([.]([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000BFDC4 File Offset: 0x000BDFC4
		// (set) Token: 0x06003F96 RID: 16278 RVA: 0x000BFDCB File Offset: 0x000BDFCB
		public static Regex UMNumberingPlanFormat { get; private set; } = CommonRegex.CreateRegex("UMNumberingPlanFormat", "^[+]?[x0-9]+$");

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x000BFDD3 File Offset: 0x000BDFD3
		// (set) Token: 0x06003F98 RID: 16280 RVA: 0x000BFDDA File Offset: 0x000BDFDA
		public static Regex E164 { get; private set; } = CommonRegex.CreateRegex("E164", "^[+](([0-9]){1,15})$");

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x000BFDE2 File Offset: 0x000BDFE2
		// (set) Token: 0x06003F9A RID: 16282 RVA: 0x000BFDE9 File Offset: 0x000BDFE9
		public static Regex Url { get; private set; } = CommonRegex.CreateRegex("Url", "^(ht|f)tp(s?)\\:\\/\\/[0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*(:(0-9)*)*(\\/?)([a-zA-Z0-9\\-\\.\\?\\,\\'\\/\\\\\\+&amp;%\\$#_]*)?$");

		// Token: 0x06003F9B RID: 16283 RVA: 0x000BFDF1 File Offset: 0x000BDFF1
		public static Regex NumbersOfSpecificLength(int minLength, int maxLength)
		{
			if (minLength < 1 || minLength > maxLength)
			{
				throw new ArgumentException("Must be greater than zero, and less than the maxLength", "minLength");
			}
			return new Regex(string.Format("^([0-9]){{{0},{1}}}$", minLength, maxLength));
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000BFE26 File Offset: 0x000BE026
		internal static Regex GetRegexExpressionById(string key)
		{
			return CommonRegex.keyMapping[key];
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000BFE34 File Offset: 0x000BE034
		private static Regex CreateRegex(string key, string value)
		{
			Regex regex = new Regex(value);
			CommonRegex.keyMapping.Add(key, regex);
			return regex;
		}

		// Token: 0x0400292C RID: 10540
		private const string DomainRegex = "^[-a-zA-Z0-9_.]+$";

		// Token: 0x0400292D RID: 10541
		private const string EmailOrDomainRegex = "^@?[a-zA-Z0-9-_]+(\\.[a-z-A-Z0-9-_]+)+$|^[a-zA-Z0-9-_\\.]+@[a-zA-Z0-9-_]+(\\.[a-z-A-Z0-9-_]+)+$";

		// Token: 0x0400292E RID: 10542
		private const string IpAddressRegex = "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])([.]([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

		// Token: 0x0400292F RID: 10543
		private const string UMNumberingPlanRegex = "^[+]?[x0-9]+$";

		// Token: 0x04002930 RID: 10544
		private const string E164Regex = "^[+](([0-9]){1,15})$";

		// Token: 0x04002931 RID: 10545
		private const string NumbersOfSpecificLengthFormat = "^([0-9]){{{0},{1}}}$";

		// Token: 0x04002932 RID: 10546
		private const string DsnCodeRegex = "^[2|4|5]\\.[0-9]\\.[0-9]$";

		// Token: 0x04002933 RID: 10547
		private const string UrlRegex = "^(ht|f)tp(s?)\\:\\/\\/[0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*(:(0-9)*)*(\\/?)([a-zA-Z0-9\\-\\.\\?\\,\\'\\/\\\\\\+&amp;%\\$#_]*)?$";

		// Token: 0x04002934 RID: 10548
		private static readonly Dictionary<string, Regex> keyMapping = new Dictionary<string, Regex>(5);
	}
}
