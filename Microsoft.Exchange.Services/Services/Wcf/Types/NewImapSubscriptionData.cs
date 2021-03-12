using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC4 RID: 2756
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewImapSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x00107CC1 File Offset: 0x00105EC1
		// (set) Token: 0x06004E5F RID: 20063 RVA: 0x00107CC9 File Offset: 0x00105EC9
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

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x00107CDD File Offset: 0x00105EDD
		// (set) Token: 0x06004E61 RID: 20065 RVA: 0x00107CE5 File Offset: 0x00105EE5
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

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x00107CF9 File Offset: 0x00105EF9
		// (set) Token: 0x06004E63 RID: 20067 RVA: 0x00107D01 File Offset: 0x00105F01
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

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00107D15 File Offset: 0x00105F15
		// (set) Token: 0x06004E65 RID: 20069 RVA: 0x00107D22 File Offset: 0x00105F22
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

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x00107D30 File Offset: 0x00105F30
		// (set) Token: 0x06004E67 RID: 20071 RVA: 0x00107D38 File Offset: 0x00105F38
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

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x00107D4C File Offset: 0x00105F4C
		// (set) Token: 0x06004E69 RID: 20073 RVA: 0x00107D54 File Offset: 0x00105F54
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

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x00107D68 File Offset: 0x00105F68
		// (set) Token: 0x06004E6B RID: 20075 RVA: 0x00107D70 File Offset: 0x00105F70
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

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x00107D84 File Offset: 0x00105F84
		// (set) Token: 0x06004E6D RID: 20077 RVA: 0x00107D91 File Offset: 0x00105F91
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

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x00107D9F File Offset: 0x00105F9F
		// (set) Token: 0x06004E6F RID: 20079 RVA: 0x00107DA7 File Offset: 0x00105FA7
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

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x00107DBB File Offset: 0x00105FBB
		// (set) Token: 0x06004E71 RID: 20081 RVA: 0x00107DC3 File Offset: 0x00105FC3
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

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x00107DD7 File Offset: 0x00105FD7
		// (set) Token: 0x06004E73 RID: 20083 RVA: 0x00107DDF File Offset: 0x00105FDF
		[DataMember]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				base.TrackPropertyChanged("Name");
			}
		}

		// Token: 0x04002C08 RID: 11272
		private string displayName;

		// Token: 0x04002C09 RID: 11273
		private string emailAddress;

		// Token: 0x04002C0A RID: 11274
		private IMAPAuthenticationMechanism incomingAuth;

		// Token: 0x04002C0B RID: 11275
		private string incomingPassword;

		// Token: 0x04002C0C RID: 11276
		private int incomingPort;

		// Token: 0x04002C0D RID: 11277
		private IMAPSecurityMechanism incomingSecurity;

		// Token: 0x04002C0E RID: 11278
		private string incomingServer;

		// Token: 0x04002C0F RID: 11279
		private string incomingUserName;

		// Token: 0x04002C10 RID: 11280
		private string name;
	}
}
