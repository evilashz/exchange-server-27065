using System;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x0200003E RID: 62
	internal sealed class DsnRecipientInfo
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		public DsnRecipientInfo(string displayName, string address, string addressType, string enhancedStatusCode, string statusText) : this(displayName, address, addressType, enhancedStatusCode, statusText, null, null, null, null, null)
		{
			if (!EnhancedStatusCodeImpl.IsValid(enhancedStatusCode))
			{
				throw new ArgumentException("Invalid enhanced status code");
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		internal DsnRecipientInfo(string displayName, string address, string addressType, string enhancedStatusCode, string statusText, string orecip, string explanation, string specificExplanation, string decodedAddress, string[] dsnParamTexts) : this(displayName, address, addressType, enhancedStatusCode, statusText, orecip, explanation, specificExplanation, decodedAddress, dsnParamTexts, false, null)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000FF04 File Offset: 0x0000E104
		internal DsnRecipientInfo(string displayName, string address, string addressType, string enhancedStatusCode, string statusText, string orecip, string explanation, string specificExplanation, string decodedAddress, string[] dsnParamTexts, bool overwriteDefault, string dsnSource) : this(displayName, address, addressType, enhancedStatusCode, statusText, orecip, explanation, specificExplanation, decodedAddress, dsnParamTexts, overwriteDefault, dsnSource, 0, null, null, null)
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000FF30 File Offset: 0x0000E130
		internal DsnRecipientInfo(string displayName, string address, string addressType, string enhancedStatusCode, string statusText, string orecip, string explanation, string specificExplanation, string decodedAddress, string[] dsnParamTexts, bool overwriteDefault, string dsnSource, int retryCount, string lastTransientErrorDetails, string lastPermanentErrorDetails, string receivingServerDetails)
		{
			this.displayName = (string.IsNullOrEmpty(displayName) ? string.Empty : displayName);
			this.address = (string.IsNullOrEmpty(address) ? string.Empty : address);
			this.addressType = (string.IsNullOrEmpty(addressType) ? string.Empty : addressType);
			this.enhancedStatusCode = (string.IsNullOrEmpty(enhancedStatusCode) ? string.Empty : enhancedStatusCode);
			this.statusText = (string.IsNullOrEmpty(statusText) ? string.Empty : statusText);
			this.orecip = (string.IsNullOrEmpty(orecip) ? string.Empty : orecip);
			this.dsnHumanReadableExplanation = (string.IsNullOrEmpty(explanation) ? string.Empty : explanation);
			this.specificExplanation = (specificExplanation ?? string.Empty);
			this.decodedAddress = (string.IsNullOrEmpty(decodedAddress) ? string.Empty : decodedAddress);
			this.dsnParamTexts = dsnParamTexts;
			this.overwriteDefault = overwriteDefault;
			this.dsnSource = dsnSource;
			this.retryCount = retryCount;
			this.lastTransientErrorDetails = (string.IsNullOrEmpty(lastTransientErrorDetails) ? string.Empty : lastTransientErrorDetails);
			this.lastPermanentErrorDetails = (string.IsNullOrEmpty(lastPermanentErrorDetails) ? string.Empty : lastPermanentErrorDetails);
			this.receivingServerDetails = (string.IsNullOrEmpty(receivingServerDetails) ? string.Empty : receivingServerDetails);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00010076 File Offset: 0x0000E276
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0001007E File Offset: 0x0000E27E
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00010087 File Offset: 0x0000E287
		internal string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0001008F File Offset: 0x0000E28F
		internal string AddressType
		{
			get
			{
				return this.addressType;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00010097 File Offset: 0x0000E297
		internal string EnhancedStatusCode
		{
			get
			{
				return this.enhancedStatusCode;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0001009F File Offset: 0x0000E29F
		internal string StatusText
		{
			get
			{
				return this.statusText;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000100A7 File Offset: 0x0000E2A7
		// (set) Token: 0x0600028F RID: 655 RVA: 0x000100AF File Offset: 0x0000E2AF
		internal string DecodedAddress
		{
			get
			{
				return this.decodedAddress;
			}
			set
			{
				this.decodedAddress = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000100B8 File Offset: 0x0000E2B8
		internal string ORecip
		{
			get
			{
				return this.orecip;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000100C0 File Offset: 0x0000E2C0
		// (set) Token: 0x06000292 RID: 658 RVA: 0x000100C8 File Offset: 0x0000E2C8
		internal string DsnHumanReadableExplanation
		{
			get
			{
				return this.dsnHumanReadableExplanation;
			}
			set
			{
				this.dsnHumanReadableExplanation = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000100D1 File Offset: 0x0000E2D1
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000100D9 File Offset: 0x0000E2D9
		internal string DsnRecipientExplanation
		{
			get
			{
				return this.specificExplanation;
			}
			set
			{
				this.specificExplanation = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000100E2 File Offset: 0x0000E2E2
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000100EA File Offset: 0x0000E2EA
		internal bool IsCustomizedDsn
		{
			get
			{
				return this.isCustomizedDsn;
			}
			set
			{
				this.isCustomizedDsn = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000100F3 File Offset: 0x0000E2F3
		internal string[] DsnParamTexts
		{
			get
			{
				return this.dsnParamTexts;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000100FB File Offset: 0x0000E2FB
		internal bool OverwriteDefault
		{
			get
			{
				return this.overwriteDefault;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00010103 File Offset: 0x0000E303
		internal string DsnSource
		{
			get
			{
				return this.dsnSource;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0001010B File Offset: 0x0000E30B
		internal int RetryCount
		{
			get
			{
				return this.retryCount;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00010113 File Offset: 0x0000E313
		internal string LastTransientErrorDetails
		{
			get
			{
				return this.lastTransientErrorDetails;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001011B File Offset: 0x0000E31B
		internal string LastPermanentErrorDetails
		{
			get
			{
				return this.lastPermanentErrorDetails;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00010123 File Offset: 0x0000E323
		internal string ReceivingServerDetails
		{
			get
			{
				return this.receivingServerDetails;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001012B File Offset: 0x0000E32B
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00010133 File Offset: 0x0000E333
		public string NdrEnhancedText
		{
			get
			{
				return this.ndrEnhancedText;
			}
			set
			{
				this.ndrEnhancedText = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001013C File Offset: 0x0000E33C
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00010144 File Offset: 0x0000E344
		public EmailRecipientCollection ModeratedRecipients
		{
			get
			{
				return this.moderatedRecipients;
			}
			set
			{
				this.moderatedRecipients = value;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0001014D File Offset: 0x0000E34D
		internal string GetDiagnostic()
		{
			return this.addressType + "#" + this.orecip;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010168 File Offset: 0x0000E368
		public DsnRecipientInfo NewCloneWithDifferentRecipient(string recipientAddress)
		{
			return new DsnRecipientInfo(string.Empty, recipientAddress, this.AddressType, this.EnhancedStatusCode, this.StatusText, this.ORecip, this.DsnHumanReadableExplanation, this.DsnRecipientExplanation, string.Empty, (string[])this.DsnParamTexts.Clone(), this.OverwriteDefault, this.DsnSource);
		}

		// Token: 0x040002EC RID: 748
		private readonly string dsnSource;

		// Token: 0x040002ED RID: 749
		private string displayName;

		// Token: 0x040002EE RID: 750
		private readonly string addressType;

		// Token: 0x040002EF RID: 751
		private readonly string address;

		// Token: 0x040002F0 RID: 752
		private string decodedAddress;

		// Token: 0x040002F1 RID: 753
		private readonly string enhancedStatusCode;

		// Token: 0x040002F2 RID: 754
		private readonly string statusText;

		// Token: 0x040002F3 RID: 755
		private readonly string orecip;

		// Token: 0x040002F4 RID: 756
		private string dsnHumanReadableExplanation;

		// Token: 0x040002F5 RID: 757
		private readonly string[] dsnParamTexts;

		// Token: 0x040002F6 RID: 758
		private readonly bool overwriteDefault;

		// Token: 0x040002F7 RID: 759
		private readonly int retryCount;

		// Token: 0x040002F8 RID: 760
		private readonly string lastTransientErrorDetails;

		// Token: 0x040002F9 RID: 761
		private readonly string lastPermanentErrorDetails;

		// Token: 0x040002FA RID: 762
		private readonly string receivingServerDetails;

		// Token: 0x040002FB RID: 763
		private bool isCustomizedDsn;

		// Token: 0x040002FC RID: 764
		private string ndrEnhancedText;

		// Token: 0x040002FD RID: 765
		private EmailRecipientCollection moderatedRecipients;

		// Token: 0x040002FE RID: 766
		private string specificExplanation;
	}
}
