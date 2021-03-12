using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE2 RID: 2786
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetPopSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x00108722 File Offset: 0x00106922
		// (set) Token: 0x06004F39 RID: 20281 RVA: 0x0010872A File Offset: 0x0010692A
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

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x0010873E File Offset: 0x0010693E
		// (set) Token: 0x06004F3B RID: 20283 RVA: 0x00108746 File Offset: 0x00106946
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

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x0010875A File Offset: 0x0010695A
		// (set) Token: 0x06004F3D RID: 20285 RVA: 0x00108762 File Offset: 0x00106962
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

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06004F3E RID: 20286 RVA: 0x00108776 File Offset: 0x00106976
		// (set) Token: 0x06004F3F RID: 20287 RVA: 0x0010877E File Offset: 0x0010697E
		[IgnoreDataMember]
		public AuthenticationMechanism IncomingAuth
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

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x00108792 File Offset: 0x00106992
		// (set) Token: 0x06004F41 RID: 20289 RVA: 0x0010879F File Offset: 0x0010699F
		[DataMember(Name = "IncomingAuth", IsRequired = false, EmitDefaultValue = false)]
		public string IncomingAuthString
		{
			get
			{
				return EnumUtilities.ToString<AuthenticationMechanism>(this.IncomingAuth);
			}
			set
			{
				this.IncomingAuth = EnumUtilities.Parse<AuthenticationMechanism>(value);
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x001087AD File Offset: 0x001069AD
		// (set) Token: 0x06004F43 RID: 20291 RVA: 0x001087B5 File Offset: 0x001069B5
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

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x001087C9 File Offset: 0x001069C9
		// (set) Token: 0x06004F45 RID: 20293 RVA: 0x001087D1 File Offset: 0x001069D1
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

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x001087E5 File Offset: 0x001069E5
		// (set) Token: 0x06004F47 RID: 20295 RVA: 0x001087ED File Offset: 0x001069ED
		[IgnoreDataMember]
		public SecurityMechanism IncomingSecurity
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

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x00108801 File Offset: 0x00106A01
		// (set) Token: 0x06004F49 RID: 20297 RVA: 0x0010880E File Offset: 0x00106A0E
		[DataMember(Name = "IncomingSecurity", IsRequired = false, EmitDefaultValue = false)]
		public string IncomingSecurityString
		{
			get
			{
				return EnumUtilities.ToString<SecurityMechanism>(this.IncomingSecurity);
			}
			set
			{
				this.IncomingSecurity = EnumUtilities.Parse<SecurityMechanism>(value);
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x0010881C File Offset: 0x00106A1C
		// (set) Token: 0x06004F4B RID: 20299 RVA: 0x00108824 File Offset: 0x00106A24
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

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x00108838 File Offset: 0x00106A38
		// (set) Token: 0x06004F4D RID: 20301 RVA: 0x00108840 File Offset: 0x00106A40
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

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004F4E RID: 20302 RVA: 0x00108854 File Offset: 0x00106A54
		// (set) Token: 0x06004F4F RID: 20303 RVA: 0x0010885C File Offset: 0x00106A5C
		[DataMember]
		public bool LeaveOnServer
		{
			get
			{
				return this.leaveOnServer;
			}
			set
			{
				this.leaveOnServer = value;
				base.TrackPropertyChanged("LeaveOnServer");
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x00108870 File Offset: 0x00106A70
		// (set) Token: 0x06004F51 RID: 20305 RVA: 0x00108878 File Offset: 0x00106A78
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

		// Token: 0x04002C56 RID: 11350
		private string displayName;

		// Token: 0x04002C57 RID: 11351
		private string emailAddress;

		// Token: 0x04002C58 RID: 11352
		private Identity identity;

		// Token: 0x04002C59 RID: 11353
		private AuthenticationMechanism incomingAuth;

		// Token: 0x04002C5A RID: 11354
		private string incomingPassword;

		// Token: 0x04002C5B RID: 11355
		private int incomingPort;

		// Token: 0x04002C5C RID: 11356
		private SecurityMechanism incomingSecurity;

		// Token: 0x04002C5D RID: 11357
		private string incomingServer;

		// Token: 0x04002C5E RID: 11358
		private string incomingUserName;

		// Token: 0x04002C5F RID: 11359
		private bool leaveOnServer;

		// Token: 0x04002C60 RID: 11360
		private bool resendVerification;
	}
}
