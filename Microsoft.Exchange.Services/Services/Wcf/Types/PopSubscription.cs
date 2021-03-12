using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ACF RID: 2767
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PopSubscription : Subscription
	{
		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x0010819C File Offset: 0x0010639C
		// (set) Token: 0x06004EC0 RID: 20160 RVA: 0x001081A4 File Offset: 0x001063A4
		[IgnoreDataMember]
		public AuthenticationMechanism IncomingAuth { get; set; }

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x001081AD File Offset: 0x001063AD
		// (set) Token: 0x06004EC2 RID: 20162 RVA: 0x001081BA File Offset: 0x001063BA
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

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x001081C8 File Offset: 0x001063C8
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x001081D0 File Offset: 0x001063D0
		[DataMember]
		public int IncomingPort { get; set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x001081D9 File Offset: 0x001063D9
		// (set) Token: 0x06004EC6 RID: 20166 RVA: 0x001081E1 File Offset: 0x001063E1
		[IgnoreDataMember]
		public SecurityMechanism IncomingSecurity { get; set; }

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x001081EA File Offset: 0x001063EA
		// (set) Token: 0x06004EC8 RID: 20168 RVA: 0x001081F7 File Offset: 0x001063F7
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

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x00108205 File Offset: 0x00106405
		// (set) Token: 0x06004ECA RID: 20170 RVA: 0x0010820D File Offset: 0x0010640D
		[DataMember]
		public string IncomingServer { get; set; }

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x00108216 File Offset: 0x00106416
		// (set) Token: 0x06004ECC RID: 20172 RVA: 0x0010821E File Offset: 0x0010641E
		[DataMember]
		public string IncomingUserName { get; set; }

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x00108227 File Offset: 0x00106427
		// (set) Token: 0x06004ECE RID: 20174 RVA: 0x0010822F File Offset: 0x0010642F
		[DataMember]
		public bool LeaveOnServer { get; set; }
	}
}
