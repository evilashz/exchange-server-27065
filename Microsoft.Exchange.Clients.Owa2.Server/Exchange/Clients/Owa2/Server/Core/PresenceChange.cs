using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200014C RID: 332
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PresenceChange
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string SipUri { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0002E0F9 File Offset: 0x0002C2F9
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0002E101 File Offset: 0x0002C301
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string UserName { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002E10A File Offset: 0x0002C30A
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0002E112 File Offset: 0x0002C312
		[IgnoreDataMember]
		public InstantMessagePresenceType Presence { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002E11B File Offset: 0x0002C31B
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0002E12D File Offset: 0x0002C32D
		[DataMember(Name = "Presence", Order = 3)]
		public string PresenceString
		{
			get
			{
				return this.Presence.ToString();
			}
			set
			{
				this.Presence = InstantMessageUtilities.ParseEnumValue<InstantMessagePresenceType>(value, InstantMessagePresenceType.None);
			}
		}
	}
}
