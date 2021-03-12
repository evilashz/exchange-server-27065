using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B7 RID: 1719
	[DataContract]
	public class PeopleIdentity : INamedIdentity
	{
		// Token: 0x06004929 RID: 18729 RVA: 0x000DFBB4 File Offset: 0x000DDDB4
		public PeopleIdentity(string displayName, string legacyDN, string sMTPAddress, int addressOrigin, string routingType, int recipientFlag)
		{
			this.DisplayName = displayName;
			this.Address = legacyDN;
			this.SMTPAddress = sMTPAddress;
			this.RoutingType = routingType;
			this.AddressOrigin = addressOrigin;
			this.RecipientFlag = recipientFlag;
			this.IgnoreDisplayNameInIdentity = false;
		}

		// Token: 0x170027CB RID: 10187
		// (get) Token: 0x0600492A RID: 18730 RVA: 0x000DFBF0 File Offset: 0x000DDDF0
		// (set) Token: 0x0600492B RID: 18731 RVA: 0x000DFBF8 File Offset: 0x000DDDF8
		[DataMember]
		public string DisplayName { get; private set; }

		// Token: 0x170027CC RID: 10188
		// (get) Token: 0x0600492C RID: 18732 RVA: 0x000DFC01 File Offset: 0x000DDE01
		// (set) Token: 0x0600492D RID: 18733 RVA: 0x000DFC09 File Offset: 0x000DDE09
		[DataMember]
		public string Address { get; internal set; }

		// Token: 0x170027CD RID: 10189
		// (get) Token: 0x0600492E RID: 18734 RVA: 0x000DFC12 File Offset: 0x000DDE12
		// (set) Token: 0x0600492F RID: 18735 RVA: 0x000DFC1A File Offset: 0x000DDE1A
		[DataMember]
		public string SMTPAddress { get; private set; }

		// Token: 0x170027CE RID: 10190
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x000DFC23 File Offset: 0x000DDE23
		// (set) Token: 0x06004931 RID: 18737 RVA: 0x000DFC2B File Offset: 0x000DDE2B
		[DataMember]
		public string RoutingType { get; private set; }

		// Token: 0x170027CF RID: 10191
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x000DFC34 File Offset: 0x000DDE34
		// (set) Token: 0x06004933 RID: 18739 RVA: 0x000DFC3C File Offset: 0x000DDE3C
		[DataMember]
		public int AddressOrigin { get; private set; }

		// Token: 0x170027D0 RID: 10192
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x000DFC45 File Offset: 0x000DDE45
		// (set) Token: 0x06004935 RID: 18741 RVA: 0x000DFC4D File Offset: 0x000DDE4D
		[DataMember]
		public int RecipientFlag { get; private set; }

		// Token: 0x170027D1 RID: 10193
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x000DFC56 File Offset: 0x000DDE56
		// (set) Token: 0x06004937 RID: 18743 RVA: 0x000DFC5E File Offset: 0x000DDE5E
		public bool IgnoreDisplayNameInIdentity { get; set; }

		// Token: 0x170027D2 RID: 10194
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x000DFC68 File Offset: 0x000DDE68
		string INamedIdentity.Identity
		{
			get
			{
				if (!(this.RoutingType == "SMTP"))
				{
					return this.Address;
				}
				if (!this.IgnoreDisplayNameInIdentity)
				{
					return string.Concat(new string[]
					{
						"\"",
						this.DisplayName,
						"\"<",
						this.SMTPAddress,
						">"
					});
				}
				return this.SMTPAddress;
			}
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x000DFCD4 File Offset: 0x000DDED4
		internal static PeopleIdentity FromIdParameter(object value)
		{
			if (value is PeopleIdentity)
			{
				return (PeopleIdentity)value;
			}
			return null;
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x000DFCF0 File Offset: 0x000DDEF0
		internal static PeopleIdentity[] FromIdParameters(object value)
		{
			if (value is PeopleIdentity[])
			{
				return (PeopleIdentity[])value;
			}
			if (value is string[])
			{
				return (from v in (string[])value
				select PeopleIdentity.FromIdParameter(v)).ToArray<PeopleIdentity>();
			}
			return null;
		}

		// Token: 0x0400313E RID: 12606
		public const string ExchangeRoutingType = "EX";

		// Token: 0x0400313F RID: 12607
		public const string SmtpRoutingType = "SMTP";
	}
}
