using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC9 RID: 2761
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewPopSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x00107EB2 File Offset: 0x001060B2
		// (set) Token: 0x06004E86 RID: 20102 RVA: 0x00107EBA File Offset: 0x001060BA
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

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x00107ECE File Offset: 0x001060CE
		// (set) Token: 0x06004E88 RID: 20104 RVA: 0x00107ED6 File Offset: 0x001060D6
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

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x00107EEA File Offset: 0x001060EA
		// (set) Token: 0x06004E8A RID: 20106 RVA: 0x00107EF2 File Offset: 0x001060F2
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

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x00107F06 File Offset: 0x00106106
		// (set) Token: 0x06004E8C RID: 20108 RVA: 0x00107F13 File Offset: 0x00106113
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

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x00107F21 File Offset: 0x00106121
		// (set) Token: 0x06004E8E RID: 20110 RVA: 0x00107F29 File Offset: 0x00106129
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

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x00107F3D File Offset: 0x0010613D
		// (set) Token: 0x06004E90 RID: 20112 RVA: 0x00107F45 File Offset: 0x00106145
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

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x00107F59 File Offset: 0x00106159
		// (set) Token: 0x06004E92 RID: 20114 RVA: 0x00107F61 File Offset: 0x00106161
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

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x00107F75 File Offset: 0x00106175
		// (set) Token: 0x06004E94 RID: 20116 RVA: 0x00107F82 File Offset: 0x00106182
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

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x00107F90 File Offset: 0x00106190
		// (set) Token: 0x06004E96 RID: 20118 RVA: 0x00107F98 File Offset: 0x00106198
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

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x00107FAC File Offset: 0x001061AC
		// (set) Token: 0x06004E98 RID: 20120 RVA: 0x00107FB4 File Offset: 0x001061B4
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

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x00107FC8 File Offset: 0x001061C8
		// (set) Token: 0x06004E9A RID: 20122 RVA: 0x00107FD0 File Offset: 0x001061D0
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

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06004E9B RID: 20123 RVA: 0x00107FE4 File Offset: 0x001061E4
		// (set) Token: 0x06004E9C RID: 20124 RVA: 0x00107FEC File Offset: 0x001061EC
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

		// Token: 0x04002C15 RID: 11285
		private string displayName;

		// Token: 0x04002C16 RID: 11286
		private string emailAddress;

		// Token: 0x04002C17 RID: 11287
		private AuthenticationMechanism incomingAuth;

		// Token: 0x04002C18 RID: 11288
		private string incomingPassword;

		// Token: 0x04002C19 RID: 11289
		private int incomingPort;

		// Token: 0x04002C1A RID: 11290
		private SecurityMechanism incomingSecurity;

		// Token: 0x04002C1B RID: 11291
		private string incomingServer;

		// Token: 0x04002C1C RID: 11292
		private string incomingUserName;

		// Token: 0x04002C1D RID: 11293
		private bool leaveOnServer;

		// Token: 0x04002C1E RID: 11294
		private string name;
	}
}
