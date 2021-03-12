using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ADA RID: 2778
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetImapSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06004F06 RID: 20230 RVA: 0x001084B7 File Offset: 0x001066B7
		// (set) Token: 0x06004F07 RID: 20231 RVA: 0x001084BF File Offset: 0x001066BF
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				base.TrackPropertyChanged("DisplayName");
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x001084D3 File Offset: 0x001066D3
		// (set) Token: 0x06004F09 RID: 20233 RVA: 0x001084DB File Offset: 0x001066DB
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
				base.TrackPropertyChanged("EmailAddress");
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06004F0A RID: 20234 RVA: 0x001084EF File Offset: 0x001066EF
		// (set) Token: 0x06004F0B RID: 20235 RVA: 0x001084F7 File Offset: 0x001066F7
		[DataMember]
		public Identity Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
				base.TrackPropertyChanged("Identity");
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06004F0C RID: 20236 RVA: 0x0010850B File Offset: 0x0010670B
		// (set) Token: 0x06004F0D RID: 20237 RVA: 0x00108513 File Offset: 0x00106713
		[IgnoreDataMember]
		public IMAPAuthenticationMechanism IncomingAuth
		{
			get
			{
				return this.incomingAuth;
			}
			set
			{
				this.incomingAuth = value;
				base.TrackPropertyChanged("IncomingAuth");
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x00108527 File Offset: 0x00106727
		// (set) Token: 0x06004F0F RID: 20239 RVA: 0x00108534 File Offset: 0x00106734
		[DataMember(Name = "IncomingAuth", IsRequired = false, EmitDefaultValue = false)]
		public string IncomingAuthString
		{
			get
			{
				return EnumUtilities.ToString<IMAPAuthenticationMechanism>(this.IncomingAuth);
			}
			set
			{
				this.IncomingAuth = EnumUtilities.Parse<IMAPAuthenticationMechanism>(value);
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x00108542 File Offset: 0x00106742
		// (set) Token: 0x06004F11 RID: 20241 RVA: 0x0010854A File Offset: 0x0010674A
		[DataMember]
		public string IncomingPassword
		{
			get
			{
				return this.incomingPassword;
			}
			set
			{
				this.incomingPassword = value;
				base.TrackPropertyChanged("IncomingPassword");
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0010855E File Offset: 0x0010675E
		// (set) Token: 0x06004F13 RID: 20243 RVA: 0x00108566 File Offset: 0x00106766
		[DataMember]
		public int IncomingPort
		{
			get
			{
				return this.incomingPort;
			}
			set
			{
				this.incomingPort = value;
				base.TrackPropertyChanged("IncomingPort");
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x0010857A File Offset: 0x0010677A
		// (set) Token: 0x06004F15 RID: 20245 RVA: 0x00108582 File Offset: 0x00106782
		[IgnoreDataMember]
		public IMAPSecurityMechanism IncomingSecurity
		{
			get
			{
				return this.incomingSecurity;
			}
			set
			{
				this.incomingSecurity = value;
				base.TrackPropertyChanged("IncomingSecurity");
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x00108596 File Offset: 0x00106796
		// (set) Token: 0x06004F17 RID: 20247 RVA: 0x001085A3 File Offset: 0x001067A3
		[DataMember(Name = "IncomingSecurity", IsRequired = false, EmitDefaultValue = false)]
		public string IncomingSecurityString
		{
			get
			{
				return EnumUtilities.ToString<IMAPSecurityMechanism>(this.IncomingSecurity);
			}
			set
			{
				this.IncomingSecurity = EnumUtilities.Parse<IMAPSecurityMechanism>(value);
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x001085B1 File Offset: 0x001067B1
		// (set) Token: 0x06004F19 RID: 20249 RVA: 0x001085B9 File Offset: 0x001067B9
		[DataMember]
		public string IncomingServer
		{
			get
			{
				return this.incomingServer;
			}
			set
			{
				this.incomingServer = value;
				base.TrackPropertyChanged("IncomingServer");
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x001085CD File Offset: 0x001067CD
		// (set) Token: 0x06004F1B RID: 20251 RVA: 0x001085D5 File Offset: 0x001067D5
		[DataMember]
		public string IncomingUserName
		{
			get
			{
				return this.incomingUserName;
			}
			set
			{
				this.incomingUserName = value;
				base.TrackPropertyChanged("IncomingUserName");
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x001085E9 File Offset: 0x001067E9
		// (set) Token: 0x06004F1D RID: 20253 RVA: 0x001085F1 File Offset: 0x001067F1
		[DataMember]
		public bool ResendVerification
		{
			get
			{
				return this.resendVerification;
			}
			set
			{
				this.resendVerification = value;
				base.TrackPropertyChanged("ResendVerification");
			}
		}

		// Token: 0x04002C46 RID: 11334
		private string displayName;

		// Token: 0x04002C47 RID: 11335
		private string emailAddress;

		// Token: 0x04002C48 RID: 11336
		private Identity identity;

		// Token: 0x04002C49 RID: 11337
		private IMAPAuthenticationMechanism incomingAuth;

		// Token: 0x04002C4A RID: 11338
		private string incomingPassword;

		// Token: 0x04002C4B RID: 11339
		private int incomingPort;

		// Token: 0x04002C4C RID: 11340
		private IMAPSecurityMechanism incomingSecurity;

		// Token: 0x04002C4D RID: 11341
		private string incomingServer;

		// Token: 0x04002C4E RID: 11342
		private string incomingUserName;

		// Token: 0x04002C4F RID: 11343
		private bool resendVerification;
	}
}
