using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003FF RID: 1023
	internal interface IEhloOptions
	{
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06002EA4 RID: 11940
		// (set) Token: 0x06002EA5 RID: 11941
		int Flags { get; set; }

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06002EA6 RID: 11942
		// (set) Token: 0x06002EA7 RID: 11943
		string AdvertisedFQDN { get; set; }

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002EA8 RID: 11944
		// (set) Token: 0x06002EA9 RID: 11945
		IPAddress AdvertisedIPAddress { get; set; }

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002EAA RID: 11946
		// (set) Token: 0x06002EAB RID: 11947
		bool BinaryMime { get; set; }

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002EAC RID: 11948
		// (set) Token: 0x06002EAD RID: 11949
		bool EightBitMime { get; set; }

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002EAE RID: 11950
		// (set) Token: 0x06002EAF RID: 11951
		bool EnhancedStatusCodes { get; set; }

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06002EB0 RID: 11952
		// (set) Token: 0x06002EB1 RID: 11953
		bool Dsn { get; set; }

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06002EB2 RID: 11954
		// (set) Token: 0x06002EB3 RID: 11955
		SizeMode Size { get; set; }

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06002EB4 RID: 11956
		// (set) Token: 0x06002EB5 RID: 11957
		long MaxSize { get; set; }

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002EB6 RID: 11958
		ICollection<string> AuthenticationMechanisms { get; }

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06002EB7 RID: 11959
		// (set) Token: 0x06002EB8 RID: 11960
		bool XAdrc { get; set; }

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06002EB9 RID: 11961
		// (set) Token: 0x06002EBA RID: 11962
		bool XExprops { get; set; }

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06002EBB RID: 11963
		Version XExpropsVersion { get; }

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06002EBC RID: 11964
		// (set) Token: 0x06002EBD RID: 11965
		bool XFastIndex { get; set; }

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06002EBE RID: 11966
		// (set) Token: 0x06002EBF RID: 11967
		bool StartTLS { get; set; }

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06002EC0 RID: 11968
		// (set) Token: 0x06002EC1 RID: 11969
		bool AnonymousTLS { get; set; }

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002EC2 RID: 11970
		ICollection<string> ExchangeAuthArgs { get; }

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06002EC3 RID: 11971
		// (set) Token: 0x06002EC4 RID: 11972
		bool Pipelining { get; set; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06002EC5 RID: 11973
		// (set) Token: 0x06002EC6 RID: 11974
		bool Chunking { get; set; }

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06002EC7 RID: 11975
		// (set) Token: 0x06002EC8 RID: 11976
		bool XMsgId { get; set; }

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06002EC9 RID: 11977
		// (set) Token: 0x06002ECA RID: 11978
		bool Xexch50 { get; set; }

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06002ECB RID: 11979
		// (set) Token: 0x06002ECC RID: 11980
		bool XLongAddr { get; set; }

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06002ECD RID: 11981
		// (set) Token: 0x06002ECE RID: 11982
		bool XOrar { get; set; }

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002ECF RID: 11983
		// (set) Token: 0x06002ED0 RID: 11984
		bool XRDst { get; set; }

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002ED1 RID: 11985
		// (set) Token: 0x06002ED2 RID: 11986
		bool XShadow { get; set; }

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06002ED3 RID: 11987
		// (set) Token: 0x06002ED4 RID: 11988
		bool XShadowRequest { get; set; }

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06002ED5 RID: 11989
		// (set) Token: 0x06002ED6 RID: 11990
		bool XOorg { get; set; }

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06002ED7 RID: 11991
		// (set) Token: 0x06002ED8 RID: 11992
		bool XProxy { get; set; }

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002ED9 RID: 11993
		// (set) Token: 0x06002EDA RID: 11994
		bool XProxyFrom { get; set; }

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002EDB RID: 11995
		// (set) Token: 0x06002EDC RID: 11996
		bool XProxyTo { get; set; }

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06002EDD RID: 11997
		// (set) Token: 0x06002EDE RID: 11998
		bool XRsetProxyTo { get; set; }

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06002EDF RID: 11999
		// (set) Token: 0x06002EE0 RID: 12000
		bool XSessionMdbGuid { get; set; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06002EE1 RID: 12001
		// (set) Token: 0x06002EE2 RID: 12002
		bool XAttr { get; set; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06002EE3 RID: 12003
		// (set) Token: 0x06002EE4 RID: 12004
		bool XSysProbe { get; set; }

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06002EE5 RID: 12005
		// (set) Token: 0x06002EE6 RID: 12006
		bool XOrigFrom { get; set; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06002EE7 RID: 12007
		ICollection<string> ExtendedCommands { get; }

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06002EE8 RID: 12008
		// (set) Token: 0x06002EE9 RID: 12009
		bool SmtpUtf8 { get; set; }

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06002EEA RID: 12010
		// (set) Token: 0x06002EEB RID: 12011
		bool XSessionType { get; set; }

		// Token: 0x06002EEC RID: 12012
		bool AreAnyAuthMechanismsSupported();

		// Token: 0x06002EED RID: 12013
		SmtpResponse CreateSmtpResponse(AdrcSmtpMessageContextBlob adrcSmtpMessageContextBlobInstance, ExtendedPropertiesSmtpMessageContextBlob extendedPropertiesSmtpMessageContextBlobInstance, FastIndexSmtpMessageContextBlob fastIndexSmtpMessageContextBlobInstance);

		// Token: 0x06002EEE RID: 12014
		void SetFlags(EhloOptionsFlags flagsToSet, bool value);

		// Token: 0x06002EEF RID: 12015
		void AddAuthenticationMechanism(string mechanism, bool enabled);

		// Token: 0x06002EF0 RID: 12016
		void ParseResponse(SmtpResponse response, IPAddress remoteIPAddress);

		// Token: 0x06002EF1 RID: 12017
		void ParseResponse(SmtpResponse ehloResponse, IPAddress remoteIPAddress, int linesToSkip);

		// Token: 0x06002EF2 RID: 12018
		void ParseHeloResponse(SmtpResponse heloResponse);

		// Token: 0x06002EF3 RID: 12019
		bool MatchForClientProxySession(IEhloOptions other, out string nonCriticalNonMatchingOptions, out string criticalNonMatchingOptions);

		// Token: 0x06002EF4 RID: 12020
		bool MatchForInboundProxySession(IEhloOptions other, bool proxyingBdat, out string nonMatchingOptions, out string criticalNonMatchingOptions);
	}
}
