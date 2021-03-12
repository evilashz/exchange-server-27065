using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000141 RID: 321
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NotificationPayloadBase
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002C684 File Offset: 0x0002A884
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0002C68C File Offset: 0x0002A88C
		public string SubscriptionId
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0002C695 File Offset: 0x0002A895
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x0002C6A7 File Offset: 0x0002A8A7
		[DataMember(Name = "EventType", EmitDefaultValue = false)]
		public string EventTypeString
		{
			get
			{
				return this.EventType.ToString();
			}
			set
			{
				this.EventType = (QueryNotificationType)Enum.Parse(typeof(QueryNotificationType), value);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x0002C6CC File Offset: 0x0002A8CC
		[IgnoreDataMember]
		internal QueryNotificationType EventType { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002C6D5 File Offset: 0x0002A8D5
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0002C6DD File Offset: 0x0002A8DD
		[IgnoreDataMember]
		internal NotificationLocation Source { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002C6E6 File Offset: 0x0002A8E6
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0002C6EE File Offset: 0x0002A8EE
		[IgnoreDataMember]
		internal DateTime? CreatedTime { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002C6F7 File Offset: 0x0002A8F7
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0002C6FF File Offset: 0x0002A8FF
		[IgnoreDataMember]
		internal DateTime? ReceivedTime { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002C708 File Offset: 0x0002A908
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0002C710 File Offset: 0x0002A910
		[IgnoreDataMember]
		internal DateTime? QueuedTime { get; set; }

		// Token: 0x0400073A RID: 1850
		[DataMember(EmitDefaultValue = false)]
		private string id = "notification";
	}
}
