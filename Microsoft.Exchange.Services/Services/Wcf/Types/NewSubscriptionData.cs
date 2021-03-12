using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ACC RID: 2764
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x0010805E File Offset: 0x0010625E
		// (set) Token: 0x06004EA7 RID: 20135 RVA: 0x00108066 File Offset: 0x00106266
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

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x0010807A File Offset: 0x0010627A
		// (set) Token: 0x06004EA9 RID: 20137 RVA: 0x00108082 File Offset: 0x00106282
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

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x00108096 File Offset: 0x00106296
		// (set) Token: 0x06004EAB RID: 20139 RVA: 0x0010809E File Offset: 0x0010629E
		[DataMember]
		public bool Force
		{
			get
			{
				return this.force;
			}
			set
			{
				this.force = value;
				base.TrackPropertyChanged("Force");
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06004EAC RID: 20140 RVA: 0x001080B2 File Offset: 0x001062B2
		// (set) Token: 0x06004EAD RID: 20141 RVA: 0x001080BA File Offset: 0x001062BA
		[DataMember]
		public bool Hotmail
		{
			get
			{
				return this.hotmail;
			}
			set
			{
				this.hotmail = value;
				base.TrackPropertyChanged("Hotmail");
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x001080CE File Offset: 0x001062CE
		// (set) Token: 0x06004EAF RID: 20143 RVA: 0x001080D6 File Offset: 0x001062D6
		[DataMember]
		public bool Imap
		{
			get
			{
				return this.imap;
			}
			set
			{
				this.imap = value;
				base.TrackPropertyChanged("Imap");
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x001080EA File Offset: 0x001062EA
		// (set) Token: 0x06004EB1 RID: 20145 RVA: 0x001080F2 File Offset: 0x001062F2
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

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x00108106 File Offset: 0x00106306
		// (set) Token: 0x06004EB3 RID: 20147 RVA: 0x0010810E File Offset: 0x0010630E
		[DataMember]
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
				base.TrackPropertyChanged("Password");
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x00108122 File Offset: 0x00106322
		// (set) Token: 0x06004EB5 RID: 20149 RVA: 0x0010812A File Offset: 0x0010632A
		[DataMember]
		public bool Pop
		{
			get
			{
				return this.pop;
			}
			set
			{
				this.pop = value;
				base.TrackPropertyChanged("Pop");
			}
		}

		// Token: 0x04002C21 RID: 11297
		private string displayName;

		// Token: 0x04002C22 RID: 11298
		private string emailAddress;

		// Token: 0x04002C23 RID: 11299
		private bool force;

		// Token: 0x04002C24 RID: 11300
		private bool hotmail;

		// Token: 0x04002C25 RID: 11301
		private bool imap;

		// Token: 0x04002C26 RID: 11302
		private string name;

		// Token: 0x04002C27 RID: 11303
		private string password;

		// Token: 0x04002C28 RID: 11304
		private bool pop;
	}
}
