using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000145 RID: 325
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessagePresence
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002D158 File Offset: 0x0002B358
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0002D160 File Offset: 0x0002B360
		[IgnoreDataMember]
		public InstantMessagePresenceType Presence { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002D169 File Offset: 0x0002B369
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0002D17B File Offset: 0x0002B37B
		[DataMember(Name = "Presence")]
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
