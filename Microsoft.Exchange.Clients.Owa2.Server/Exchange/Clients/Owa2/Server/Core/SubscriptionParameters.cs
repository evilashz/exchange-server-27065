using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B9 RID: 441
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscriptionParameters
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0003CAC5 File Offset: 0x0003ACC5
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x0003CACD File Offset: 0x0003ACCD
		public NotificationType NotificationType { get; set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0003CAE8 File Offset: 0x0003ACE8
		[DataMember(Name = "NotificationType")]
		public string NotificationTypeString
		{
			get
			{
				return this.NotificationType.ToString();
			}
			set
			{
				this.NotificationType = (NotificationType)Enum.Parse(typeof(NotificationType), value);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003CB05 File Offset: 0x0003AD05
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0003CB0D File Offset: 0x0003AD0D
		public ViewFilter Filter { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003CB16 File Offset: 0x0003AD16
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0003CB23 File Offset: 0x0003AD23
		[DataMember(Name = "Filter", IsRequired = false)]
		public string FilterString
		{
			get
			{
				return EnumUtilities.ToString<ViewFilter>(this.Filter);
			}
			set
			{
				this.Filter = EnumUtilities.Parse<ViewFilter>(value);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0003CB31 File Offset: 0x0003AD31
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0003CB39 File Offset: 0x0003AD39
		[DataMember]
		public string CallId { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003CB42 File Offset: 0x0003AD42
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0003CB4A File Offset: 0x0003AD4A
		[DataMember]
		public bool IsConversation { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0003CB53 File Offset: 0x0003AD53
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0003CB5B File Offset: 0x0003AD5B
		[DataMember]
		public string FolderId { get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0003CB64 File Offset: 0x0003AD64
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0003CB6C File Offset: 0x0003AD6C
		[DataMember]
		public SortResults[] SortBy { get; set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0003CB75 File Offset: 0x0003AD75
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0003CB7D File Offset: 0x0003AD7D
		[DataMember]
		public string ChannelId { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0003CB86 File Offset: 0x0003AD86
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x0003CB8E File Offset: 0x0003AD8E
		[DataMember]
		public bool InferenceEnabled { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0003CB97 File Offset: 0x0003AD97
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x0003CB9F File Offset: 0x0003AD9F
		[DataMember]
		public string FromFilter { get; set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0003CBA8 File Offset: 0x0003ADA8
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		[DataMember]
		public string ConversationShapeName { get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0003CBB9 File Offset: 0x0003ADB9
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0003CBC1 File Offset: 0x0003ADC1
		public ClutterFilter ClutterFilter { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0003CBD7 File Offset: 0x0003ADD7
		[DataMember(Name = "ClutterFilter", IsRequired = false)]
		public string ClutterFilterString
		{
			get
			{
				return EnumUtilities.ToString<ClutterFilter>(this.ClutterFilter);
			}
			set
			{
				this.ClutterFilter = EnumUtilities.Parse<ClutterFilter>(value);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0003CBE5 File Offset: 0x0003ADE5
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0003CBED File Offset: 0x0003ADED
		[DataMember]
		public string MailboxId { get; set; }
	}
}
