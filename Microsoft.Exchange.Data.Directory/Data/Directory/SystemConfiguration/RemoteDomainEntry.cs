using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000562 RID: 1378
	internal class RemoteDomainEntry : RemoteDomain, DomainMatchMap<RemoteDomainEntry>.IDomainEntry
	{
		// Token: 0x06003DE9 RID: 15849 RVA: 0x000EB920 File Offset: 0x000E9B20
		public RemoteDomainEntry(DomainContentConfig domain)
		{
			this.domain = domain.DomainName;
			this.ByteEncoderTypeFor7BitCharsets = domain.ByteEncoderTypeFor7BitCharsets;
			this.CharacterSet = domain.CharacterSet;
			this.NonMimeCharacterSet = domain.NonMimeCharacterSet;
			this.AllowedOOFType = domain.AllowedOOFType;
			this.ContentType = domain.ContentType;
			this.DisplaySenderName = domain.DisplaySenderName;
			this.PreferredInternetCodePageForShiftJis = domain.PreferredInternetCodePageForShiftJis;
			this.RequiredCharsetCoverage = domain.RequiredCharsetCoverage;
			this.TNEFEnabled = domain.TNEFEnabled;
			this.LineWrapSize = domain.LineWrapSize;
			this.OofSettings = domain.GetOOFSettings();
			this.Flags = (int)domain[EdgeDomainContentConfigSchema.Flags];
			this.UseSimpleDisplayName = domain.UseSimpleDisplayName;
			this.MessageCountThreshold = domain.MessageCountThreshold;
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000EB9F1 File Offset: 0x000E9BF1
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000EB9F9 File Offset: 0x000E9BF9
		public bool TrustedMailOutboundEnabled
		{
			get
			{
				return (this.Flags & 1) != 0;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000EBA09 File Offset: 0x000E9C09
		public bool TrustedMailInboundEnabled
		{
			get
			{
				return (this.Flags & 2) != 0;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000EBA19 File Offset: 0x000E9C19
		public bool AutoReplyEnabled
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.AutoReply) != AcceptMessageType.Default;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06003DEE RID: 15854 RVA: 0x000EBA29 File Offset: 0x000E9C29
		public bool AutoForwardEnabled
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.AutoForward) != AcceptMessageType.Default;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x000EBA39 File Offset: 0x000E9C39
		public bool DeliveryReportEnabled
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.DR) != AcceptMessageType.Default;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000EBA49 File Offset: 0x000E9C49
		public bool NDREnabled
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.NDR) != AcceptMessageType.Default;
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000EBA5A File Offset: 0x000E9C5A
		public bool MFNEnabled
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.MFN) != AcceptMessageType.Default;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06003DF2 RID: 15858 RVA: 0x000EBA6E File Offset: 0x000E9C6E
		public bool NDRDiagnosticInfoEnabled
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.NDRDiagnosticInfoDisabled) == AcceptMessageType.Default;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000EBA7F File Offset: 0x000E9C7F
		public override string NameSpecification
		{
			get
			{
				return this.DomainName.ToString();
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x000EBA8C File Offset: 0x000E9C8C
		public override string NonMimeCharset
		{
			get
			{
				return this.NonMimeCharacterSet;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000EBA94 File Offset: 0x000E9C94
		public override bool IsInternal
		{
			get
			{
				return (this.OofSettings & AcceptMessageType.InternalDomain) != AcceptMessageType.Default;
			}
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000EBAA5 File Offset: 0x000E9CA5
		public static int GetLenghAfterNullCheck(string s)
		{
			if (s == null)
			{
				return 0;
			}
			return s.Length;
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000EBAB2 File Offset: 0x000E9CB2
		public override string ToString()
		{
			return this.NameSpecification;
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000EBABA File Offset: 0x000E9CBA
		public int EstimateSize
		{
			get
			{
				if (this.DomainName == null)
				{
					return RemoteDomainEntry.GetLenghAfterNullCheck(this.NameSpecification) * 2 + RemoteDomainEntry.GetLenghAfterNullCheck(this.NonMimeCharacterSet) * 2 + 2 + 20;
				}
				return RemoteDomainEntry.GetLenghAfterNullCheck(this.DomainName.Domain) * 2 + 1;
			}
		}

		// Token: 0x040029F9 RID: 10745
		public readonly string CharacterSet;

		// Token: 0x040029FA RID: 10746
		public readonly string NonMimeCharacterSet;

		// Token: 0x040029FB RID: 10747
		public readonly ByteEncoderTypeFor7BitCharsetsEnum ByteEncoderTypeFor7BitCharsets;

		// Token: 0x040029FC RID: 10748
		public readonly AllowedOOFType AllowedOOFType;

		// Token: 0x040029FD RID: 10749
		public readonly ContentType ContentType;

		// Token: 0x040029FE RID: 10750
		public readonly bool DisplaySenderName;

		// Token: 0x040029FF RID: 10751
		public readonly PreferredInternetCodePageForShiftJisEnum PreferredInternetCodePageForShiftJis;

		// Token: 0x04002A00 RID: 10752
		public readonly int? RequiredCharsetCoverage;

		// Token: 0x04002A01 RID: 10753
		public readonly bool? TNEFEnabled;

		// Token: 0x04002A02 RID: 10754
		public readonly Unlimited<int> LineWrapSize;

		// Token: 0x04002A03 RID: 10755
		public readonly AcceptMessageType OofSettings;

		// Token: 0x04002A04 RID: 10756
		private readonly int Flags;

		// Token: 0x04002A05 RID: 10757
		public readonly bool UseSimpleDisplayName;

		// Token: 0x04002A06 RID: 10758
		public readonly int MessageCountThreshold;

		// Token: 0x04002A07 RID: 10759
		private readonly SmtpDomainWithSubdomains domain;
	}
}
