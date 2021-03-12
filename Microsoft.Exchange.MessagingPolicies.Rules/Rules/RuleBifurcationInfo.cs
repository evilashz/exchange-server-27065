using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000026 RID: 38
	internal sealed class RuleBifurcationInfo
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000078F6 File Offset: 0x00005AF6
		public List<string> FromRecipients
		{
			get
			{
				return this.fromRecipients;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000078FE File Offset: 0x00005AFE
		public List<string> FromLists
		{
			get
			{
				return this.fromLists;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00007906 File Offset: 0x00005B06
		public List<string> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000790E File Offset: 0x00005B0E
		public List<string> Lists
		{
			get
			{
				return this.lists;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007916 File Offset: 0x00005B16
		public List<string> Managers
		{
			get
			{
				return this.managers;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000791E File Offset: 0x00005B1E
		public List<string> ADAttributes
		{
			get
			{
				return this.adAttributes;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007926 File Offset: 0x00005B26
		public List<string> ADAttributesForTextMatch
		{
			get
			{
				return this.adAttributesForTextMatch;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000792E File Offset: 0x00005B2E
		public List<string> RecipientAddressContainsWords
		{
			get
			{
				return this.recipientAddressContainsWords;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007936 File Offset: 0x00005B36
		public List<string> RecipientDomainIs
		{
			get
			{
				return this.recipientDomainIs;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000793E File Offset: 0x00005B3E
		public List<string> RecipientMatchesPatterns
		{
			get
			{
				return this.recipientMatchesPatterns;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007946 File Offset: 0x00005B46
		public List<string> RecipientMatchesRegexPatterns
		{
			get
			{
				return this.recipientMatchesRegexPatterns;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000794E File Offset: 0x00005B4E
		public List<string> RecipientAttributeContains
		{
			get
			{
				return this.recipientAttributeContains;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007956 File Offset: 0x00005B56
		public List<string> RecipientAttributeMatches
		{
			get
			{
				return this.recipientAttributeMatches;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000795E File Offset: 0x00005B5E
		public List<string> RecipientAttributeMatchesRegex
		{
			get
			{
				return this.recipientAttributeMatchesRegex;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007966 File Offset: 0x00005B66
		public List<string> RecipientInSenderList
		{
			get
			{
				return this.recipientInSenderList;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000796E File Offset: 0x00005B6E
		public List<string> SenderInRecipientList
		{
			get
			{
				return this.senderInRecipientList;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007976 File Offset: 0x00005B76
		public List<string> Patterns
		{
			get
			{
				return this.patterns;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000797E File Offset: 0x00005B7E
		public List<string> Partners
		{
			get
			{
				return this.partners;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007986 File Offset: 0x00005B86
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000798E File Offset: 0x00005B8E
		public string ManagementRelationship
		{
			get
			{
				return this.managementRelationship;
			}
			set
			{
				this.managementRelationship = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007997 File Offset: 0x00005B97
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000799F File Offset: 0x00005B9F
		public string ADAttributeValue
		{
			get
			{
				return this.adAttributeValue;
			}
			set
			{
				this.adAttributeValue = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000079A8 File Offset: 0x00005BA8
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000079B0 File Offset: 0x00005BB0
		public bool ExternalRecipients
		{
			get
			{
				return this.externalRecipients;
			}
			set
			{
				this.externalRecipients = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000079B9 File Offset: 0x00005BB9
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000079C1 File Offset: 0x00005BC1
		public bool InternalRecipients
		{
			get
			{
				return this.internalRecipients;
			}
			set
			{
				this.internalRecipients = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000079CA File Offset: 0x00005BCA
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000079D2 File Offset: 0x00005BD2
		public bool ExternalPartnerRecipients
		{
			get
			{
				return this.externalPartnerRecipients;
			}
			set
			{
				this.externalPartnerRecipients = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000079DB File Offset: 0x00005BDB
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000079E3 File Offset: 0x00005BE3
		public bool ExternalNonPartnerRecipients
		{
			get
			{
				return this.externalNonPartnerRecipients;
			}
			set
			{
				this.externalNonPartnerRecipients = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000079EC File Offset: 0x00005BEC
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000079F4 File Offset: 0x00005BF4
		public bool IsSenderEvaluation
		{
			get
			{
				return this.isSenderEvaluation;
			}
			set
			{
				this.isSenderEvaluation = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000079FD File Offset: 0x00005BFD
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007A05 File Offset: 0x00005C05
		public bool CheckADAttributeEquality
		{
			get
			{
				return this.checkADAttributeEquality;
			}
			set
			{
				this.checkADAttributeEquality = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007A0E File Offset: 0x00005C0E
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007A16 File Offset: 0x00005C16
		public bool Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007A1F File Offset: 0x00005C1F
		public Version MinimumVersion
		{
			get
			{
				if (this.RecipientDomainIs.Any<string>())
				{
					return RuleBifurcationInfo.RecipientDomainIsVersion;
				}
				if (this.recipientMatchesRegexPatterns.Any<string>() || this.recipientAttributeMatchesRegex.Any<string>())
				{
					return Rule.BaseVersion15;
				}
				return Rule.BaseVersion;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007A5C File Offset: 0x00005C5C
		public int GetEstimatedSize()
		{
			int num = 18;
			this.AddStringListPropertySize(this.fromRecipients, ref num);
			this.AddStringListPropertySize(this.fromLists, ref num);
			this.AddStringListPropertySize(this.recipients, ref num);
			this.AddStringListPropertySize(this.managers, ref num);
			this.AddStringListPropertySize(this.adAttributes, ref num);
			this.AddStringListPropertySize(this.adAttributesForTextMatch, ref num);
			this.AddStringListPropertySize(this.lists, ref num);
			this.AddStringListPropertySize(this.recipientAddressContainsWords, ref num);
			this.AddStringListPropertySize(this.recipientDomainIs, ref num);
			this.AddStringListPropertySize(this.recipientMatchesPatterns, ref num);
			this.AddStringListPropertySize(this.recipientMatchesRegexPatterns, ref num);
			this.AddStringListPropertySize(this.recipientAttributeContains, ref num);
			this.AddStringListPropertySize(this.recipientAttributeMatches, ref num);
			this.AddStringListPropertySize(this.recipientAttributeMatchesRegex, ref num);
			this.AddStringListPropertySize(this.senderInRecipientList, ref num);
			this.AddStringListPropertySize(this.recipientInSenderList, ref num);
			this.AddStringListPropertySize(this.patterns, ref num);
			this.AddStringListPropertySize(this.partners, ref num);
			num += 7;
			if (!string.IsNullOrEmpty(this.managementRelationship))
			{
				num += this.managementRelationship.Length * 2;
				num += 18;
			}
			if (!string.IsNullOrEmpty(this.adAttributeValue))
			{
				num += this.adAttributeValue.Length * 2;
				num += 18;
			}
			return num;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public void AddStringListPropertySize(List<string> property, ref int size)
		{
			if (property != null)
			{
				size += 18;
				foreach (string text in property)
				{
					size += text.Length * 2;
					size += 18;
				}
			}
		}

		// Token: 0x0400010F RID: 271
		public static readonly Version RecipientDomainIsVersion = new Version("15.00.0005.02");

		// Token: 0x04000110 RID: 272
		private readonly List<string> fromRecipients = new List<string>();

		// Token: 0x04000111 RID: 273
		private readonly List<string> fromLists = new List<string>();

		// Token: 0x04000112 RID: 274
		private readonly List<string> recipients = new List<string>();

		// Token: 0x04000113 RID: 275
		private readonly List<string> managers = new List<string>();

		// Token: 0x04000114 RID: 276
		private readonly List<string> adAttributes = new List<string>();

		// Token: 0x04000115 RID: 277
		private readonly List<string> adAttributesForTextMatch = new List<string>();

		// Token: 0x04000116 RID: 278
		private readonly List<string> lists = new List<string>();

		// Token: 0x04000117 RID: 279
		private readonly List<string> recipientAddressContainsWords = new List<string>();

		// Token: 0x04000118 RID: 280
		private readonly List<string> recipientDomainIs = new List<string>();

		// Token: 0x04000119 RID: 281
		private readonly List<string> recipientMatchesPatterns = new List<string>();

		// Token: 0x0400011A RID: 282
		private readonly List<string> recipientMatchesRegexPatterns = new List<string>();

		// Token: 0x0400011B RID: 283
		private readonly List<string> recipientAttributeContains = new List<string>();

		// Token: 0x0400011C RID: 284
		private readonly List<string> recipientAttributeMatches = new List<string>();

		// Token: 0x0400011D RID: 285
		private readonly List<string> recipientAttributeMatchesRegex = new List<string>();

		// Token: 0x0400011E RID: 286
		private readonly List<string> senderInRecipientList = new List<string>();

		// Token: 0x0400011F RID: 287
		private readonly List<string> recipientInSenderList = new List<string>();

		// Token: 0x04000120 RID: 288
		private readonly List<string> patterns = new List<string>();

		// Token: 0x04000121 RID: 289
		private readonly List<string> partners = new List<string>();

		// Token: 0x04000122 RID: 290
		private bool externalRecipients;

		// Token: 0x04000123 RID: 291
		private bool internalRecipients;

		// Token: 0x04000124 RID: 292
		private bool externalPartnerRecipients;

		// Token: 0x04000125 RID: 293
		private bool externalNonPartnerRecipients;

		// Token: 0x04000126 RID: 294
		private bool exception;

		// Token: 0x04000127 RID: 295
		private bool isSenderEvaluation;

		// Token: 0x04000128 RID: 296
		private bool checkADAttributeEquality;

		// Token: 0x04000129 RID: 297
		private string managementRelationship;

		// Token: 0x0400012A RID: 298
		private string adAttributeValue;
	}
}
