using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB7 RID: 2743
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ImapSubscription : Subscription
	{
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x00106D94 File Offset: 0x00104F94
		// (set) Token: 0x06004D44 RID: 19780 RVA: 0x00106D9C File Offset: 0x00104F9C
		[IgnoreDataMember]
		public IMAPAuthenticationMechanism IncomingAuth { get; set; }

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x00106DA5 File Offset: 0x00104FA5
		// (set) Token: 0x06004D46 RID: 19782 RVA: 0x00106DB2 File Offset: 0x00104FB2
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

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x00106DC0 File Offset: 0x00104FC0
		// (set) Token: 0x06004D48 RID: 19784 RVA: 0x00106DC8 File Offset: 0x00104FC8
		[DataMember]
		public int IncomingPort { get; set; }

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06004D49 RID: 19785 RVA: 0x00106DD1 File Offset: 0x00104FD1
		// (set) Token: 0x06004D4A RID: 19786 RVA: 0x00106DD9 File Offset: 0x00104FD9
		[IgnoreDataMember]
		public IMAPSecurityMechanism IncomingSecurity { get; set; }

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x00106DE2 File Offset: 0x00104FE2
		// (set) Token: 0x06004D4C RID: 19788 RVA: 0x00106DEF File Offset: 0x00104FEF
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

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06004D4D RID: 19789 RVA: 0x00106DFD File Offset: 0x00104FFD
		// (set) Token: 0x06004D4E RID: 19790 RVA: 0x00106E05 File Offset: 0x00105005
		[DataMember]
		public string IncomingServer { get; set; }

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004D4F RID: 19791 RVA: 0x00106E0E File Offset: 0x0010500E
		// (set) Token: 0x06004D50 RID: 19792 RVA: 0x00106E16 File Offset: 0x00105016
		[DataMember]
		public string IncomingUserName { get; set; }
	}
}
