using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200003F RID: 63
	internal class RegexMatch : IMatch
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000E548 File Offset: 0x0000C748
		internal RegexMatch(string pattern, CaseSensitivityMode caseSensitivityMode, MatchRegexOptions options, TimeSpan matchTimeout)
		{
			this.pattern = pattern;
			this.options = options;
			this.caseSensitivityMode = caseSensitivityMode;
			this.useCache = options.HasFlag(MatchRegexOptions.Cached);
			this.regex = this.CreateRegex(pattern, caseSensitivityMode, options, matchTimeout);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		internal RegexMatch(RegexMatch original)
		{
			this.pattern = original.pattern;
			this.options = original.options;
			this.caseSensitivityMode = original.caseSensitivityMode;
			this.useCache = original.useCache;
			if (this.options.HasFlag(MatchRegexOptions.LazyOptimize))
			{
				this.options &= ~MatchRegexOptions.LazyOptimize;
				this.regex = this.CreateRegex(this.pattern, this.caseSensitivityMode, this.options, original.regex.MatchTimeout);
				return;
			}
			this.regex = original.regex;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000E654 File Offset: 0x0000C854
		internal long Identifier
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E65C File Offset: 0x0000C85C
		public bool IsMatch(TextScanContext data)
		{
			if (this.useCache)
			{
				bool result;
				if (!data.TryGetCachedResult(this.id, out result))
				{
					result = this.ExecuteRegex(data);
					data.SetCachedResult(this.id, result);
				}
				return result;
			}
			return this.ExecuteRegex(data);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		private Regex CreateRegex(string pattern, CaseSensitivityMode caseSensitivityMode, MatchRegexOptions options, TimeSpan matchTimeout)
		{
			Regex regex = new Regex(pattern, this.GetRegexOptions(caseSensitivityMode, options), matchTimeout);
			if (options.HasFlag(MatchRegexOptions.Primed) && !options.HasFlag(MatchRegexOptions.LazyOptimize))
			{
				regex.IsMatch("Received: from server.domain.domain.domain.domain (1.2.3.4) by\nserver.domain.domain.domain (1.1.1.1) with Microsoft\nSMTP Server (TLS) id 1.1.1.1 via Mailbox Transport; 1 Jan 2000\n11:08:40 +0000\nX-ExtLoop1: 1\nd=\"pdf'?scan'208,217\";a=\"139187562\"\nWed, 01 Jan 2000 12:26:15 +0000\nFrom: test@microsoft.com\nTo: abc.microsoft.com\nSubject: test subject\nDate: Mon, 1 Jan 2000 01:00:00 +0000\nMessage-ID: <5F15F3000D504c3689DF5FF6E0FFB429@BGSMSX102.domain.domain>\nAccept-Language: en-US\nContent-Language: en-US\nX-MS-Has-Attach: yes\nX-MS-TNEF-Correlator:\nx-originating-ip: [1.2.1.1]\nContent-Type: multipart/mixed;\nboundary=\"BA698262-28A9-4de8-9A78-FA9632EDEEBF\"\nMIME-Version: 1.0\nReturn-Path: test@abc.com\nX-OrganizationHeadersPreserved: DF-H14-01.exchange.corp.microsoft.com\nX-Forefront-Antispam-Report: CIP:1.1.1.2;IPV:NLI;EFV:NLI;SFV:SZE;SFS:;DIR:OUT;LANG:en;\nX-CrossPremisesHeadersPromoted: domain\nX-CrossPremisesHeadersFiltered: domain\nX-MS-Exchange-Organization-Network-Message-Id: 3BDE8C5D-F9E2-4959-9959-13294825ACEC\nX-MS-Exchange-Organization-AVStamp-Service: 1.0\nX-MS-Exchange-Organization-SCL: 0\nX-MS-Exchange-Organization-AuthSource:\nX-MS-Exchange-Organization-AuthAs: Anonymous\nX-OriginatorOrg: domain.domain.domain\nmail from: test@test.com\nrcpt to: test@test.com\nsubject: test message\nReceived: from ABCDEFG.xyz.microsoft.com (1.1.1.1) by\nabcdefghijklmnopqrstuvwxyz\nABCDEFGHIJKLMNOPQRSTUVWXYZ\n1234567890\n-=[];',./_+{}:\"<>?\n\\aaaaaaaaabbbbbbbbbcccccccc000000000011111111122222222---------      ::::::::\n`~!@#$%^&*()_+|\t \n{975FF651-E46D-49d3-8DBD-A65513698885}{F1FFF2FC-EA90-4dc5-BF46-8F796065AF35}\nReceived: from SN2FFOFD003.ffo.gbl (157.55.158.24) by\n BL2SR01CA105.outlook.office365.com (10.255.109.150) with Microsoft SMTP\n Server (TLS) id 15.0.805.1 via Frontend Transport; Fri, 4 Oct 2013 22:46:44\n +0000\nReceived: from hybrid.exchange.microsoft.com (131.107.1.17) by\n SN2FFOFD003.mail.o365filtering.com (10.111.201.40) with Microsoft SMTP Server\n (TLS) id 15.0.795.3 via Frontend Transport; Fri, 4 Oct 2013 22:46:43 +0000\nReceived: from mail121-db9-R.bigfish.com (157.54.51.113) by mail.microsoft.com\n (157.54.80.67) with Microsoft SMTP Server (TLS) id 14.3.136.1; Fri, 4 Oct\n 2013 22:45:31 +0000");
				regex.IsMatch(pattern);
			}
			return regex;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E6FA File Offset: 0x0000C8FA
		private bool ExecuteRegex(TextScanContext context)
		{
			return this.regex.IsMatch((CaseSensitivityMode.InsensitiveUsingNormalization == this.caseSensitivityMode) ? context.NormalizedData : context.Data);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E720 File Offset: 0x0000C920
		private RegexOptions GetRegexOptions(CaseSensitivityMode caseSensitivityMode, MatchRegexOptions options)
		{
			RegexOptions regexOptions = RegexOptions.None;
			if (CaseSensitivityMode.Insensitive == caseSensitivityMode)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			if (options.HasFlag(MatchRegexOptions.Compiled) && !options.HasFlag(MatchRegexOptions.LazyOptimize))
			{
				regexOptions |= RegexOptions.Compiled;
			}
			if (options.HasFlag(MatchRegexOptions.ExplicitCaptures))
			{
				regexOptions |= RegexOptions.ExplicitCapture;
			}
			if (options.HasFlag(MatchRegexOptions.CultureInvariant))
			{
				regexOptions |= RegexOptions.CultureInvariant;
			}
			return regexOptions;
		}

		// Token: 0x0400014B RID: 331
		private const string RegexPrimingText = "Received: from server.domain.domain.domain.domain (1.2.3.4) by\nserver.domain.domain.domain (1.1.1.1) with Microsoft\nSMTP Server (TLS) id 1.1.1.1 via Mailbox Transport; 1 Jan 2000\n11:08:40 +0000\nX-ExtLoop1: 1\nd=\"pdf'?scan'208,217\";a=\"139187562\"\nWed, 01 Jan 2000 12:26:15 +0000\nFrom: test@microsoft.com\nTo: abc.microsoft.com\nSubject: test subject\nDate: Mon, 1 Jan 2000 01:00:00 +0000\nMessage-ID: <5F15F3000D504c3689DF5FF6E0FFB429@BGSMSX102.domain.domain>\nAccept-Language: en-US\nContent-Language: en-US\nX-MS-Has-Attach: yes\nX-MS-TNEF-Correlator:\nx-originating-ip: [1.2.1.1]\nContent-Type: multipart/mixed;\nboundary=\"BA698262-28A9-4de8-9A78-FA9632EDEEBF\"\nMIME-Version: 1.0\nReturn-Path: test@abc.com\nX-OrganizationHeadersPreserved: DF-H14-01.exchange.corp.microsoft.com\nX-Forefront-Antispam-Report: CIP:1.1.1.2;IPV:NLI;EFV:NLI;SFV:SZE;SFS:;DIR:OUT;LANG:en;\nX-CrossPremisesHeadersPromoted: domain\nX-CrossPremisesHeadersFiltered: domain\nX-MS-Exchange-Organization-Network-Message-Id: 3BDE8C5D-F9E2-4959-9959-13294825ACEC\nX-MS-Exchange-Organization-AVStamp-Service: 1.0\nX-MS-Exchange-Organization-SCL: 0\nX-MS-Exchange-Organization-AuthSource:\nX-MS-Exchange-Organization-AuthAs: Anonymous\nX-OriginatorOrg: domain.domain.domain\nmail from: test@test.com\nrcpt to: test@test.com\nsubject: test message\nReceived: from ABCDEFG.xyz.microsoft.com (1.1.1.1) by\nabcdefghijklmnopqrstuvwxyz\nABCDEFGHIJKLMNOPQRSTUVWXYZ\n1234567890\n-=[];',./_+{}:\"<>?\n\\aaaaaaaaabbbbbbbbbcccccccc000000000011111111122222222---------      ::::::::\n`~!@#$%^&*()_+|\t \n{975FF651-E46D-49d3-8DBD-A65513698885}{F1FFF2FC-EA90-4dc5-BF46-8F796065AF35}\nReceived: from SN2FFOFD003.ffo.gbl (157.55.158.24) by\n BL2SR01CA105.outlook.office365.com (10.255.109.150) with Microsoft SMTP\n Server (TLS) id 15.0.805.1 via Frontend Transport; Fri, 4 Oct 2013 22:46:44\n +0000\nReceived: from hybrid.exchange.microsoft.com (131.107.1.17) by\n SN2FFOFD003.mail.o365filtering.com (10.111.201.40) with Microsoft SMTP Server\n (TLS) id 15.0.795.3 via Frontend Transport; Fri, 4 Oct 2013 22:46:43 +0000\nReceived: from mail121-db9-R.bigfish.com (157.54.51.113) by mail.microsoft.com\n (157.54.80.67) with Microsoft SMTP Server (TLS) id 14.3.136.1; Fri, 4 Oct\n 2013 22:45:31 +0000";

		// Token: 0x0400014C RID: 332
		private readonly long id = IDGenerator.GetNextID();

		// Token: 0x0400014D RID: 333
		private readonly bool useCache;

		// Token: 0x0400014E RID: 334
		private readonly CaseSensitivityMode caseSensitivityMode;

		// Token: 0x0400014F RID: 335
		private readonly string pattern;

		// Token: 0x04000150 RID: 336
		private readonly MatchRegexOptions options;

		// Token: 0x04000151 RID: 337
		private Regex regex;
	}
}
