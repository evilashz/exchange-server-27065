using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003DB RID: 987
	[DataContract]
	public abstract class BaseMailboxSearchParameters : SetObjectProperties
	{
		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x0009E1FB File Offset: 0x0009C3FB
		// (set) Token: 0x060032E7 RID: 13031 RVA: 0x0009E20D File Offset: 0x0009C40D
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base[SearchObjectBaseSchema.Name];
			}
			set
			{
				base[SearchObjectBaseSchema.Name] = value;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x0009E21B File Offset: 0x0009C41B
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x0009E232 File Offset: 0x0009C432
		[DataMember]
		public string Recipients
		{
			get
			{
				return base[SearchObjectSchema.Recipients].StringArrayJoin(",");
			}
			set
			{
				base[SearchObjectSchema.Recipients] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x0009E245 File Offset: 0x0009C445
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x0009E261 File Offset: 0x0009C461
		[DataMember]
		public bool SearchDumpster
		{
			get
			{
				return (bool)(base[SearchObjectSchema.SearchDumpster] ?? false);
			}
			set
			{
				base[SearchObjectSchema.SearchDumpster] = value;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x0009E274 File Offset: 0x0009C474
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x0009E286 File Offset: 0x0009C486
		[DataMember]
		public string SearchQuery
		{
			get
			{
				return (string)base[SearchObjectSchema.SearchQuery];
			}
			set
			{
				base[SearchObjectSchema.SearchQuery] = value;
			}
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x0009E294 File Offset: 0x0009C494
		// (set) Token: 0x060032EF RID: 13039 RVA: 0x0009E2B0 File Offset: 0x0009C4B0
		[DataMember]
		public bool IncludeUnsearchableItems
		{
			get
			{
				return (bool)(base[SearchObjectSchema.IncludeUnsearchableItems] ?? false);
			}
			set
			{
				base[SearchObjectSchema.IncludeUnsearchableItems] = value;
			}
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x0009E2C3 File Offset: 0x0009C4C3
		// (set) Token: 0x060032F1 RID: 13041 RVA: 0x0009E2DA File Offset: 0x0009C4DA
		[DataMember]
		public string Senders
		{
			get
			{
				return base[SearchObjectSchema.Senders].StringArrayJoin(",");
			}
			set
			{
				base[SearchObjectSchema.Senders] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x0009E2ED File Offset: 0x0009C4ED
		// (set) Token: 0x060032F3 RID: 13043 RVA: 0x0009E300 File Offset: 0x0009C500
		[DataMember]
		public bool SendMeEmailOnComplete
		{
			get
			{
				return base[SearchObjectSchema.StatusMailRecipients] != null;
			}
			set
			{
				base[SearchObjectSchema.StatusMailRecipients] = (value ? RbacPrincipal.Current.ExecutingUserId : null);
			}
		}

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x0009E31D File Offset: 0x0009C51D
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x0009E32F File Offset: 0x0009C52F
		[DataMember]
		public Identity TargetMailbox
		{
			get
			{
				return Identity.FromIdParameter(base[SearchObjectSchema.TargetMailbox]);
			}
			set
			{
				base[SearchObjectSchema.TargetMailbox] = value.ToIdParameter();
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x0009E342 File Offset: 0x0009C542
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x0009E35A File Offset: 0x0009C55A
		[DataMember]
		public bool EnableFullLogging
		{
			get
			{
				return LoggingLevel.Full.Equals(base[SearchObjectSchema.LogLevel]);
			}
			set
			{
				base[SearchObjectSchema.LogLevel] = (value ? LoggingLevel.Full : LoggingLevel.Basic);
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x0009E373 File Offset: 0x0009C573
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x0009E38F File Offset: 0x0009C58F
		[DataMember]
		public bool EstimateOnly
		{
			get
			{
				return (bool)(base[SearchObjectSchema.EstimateOnly] ?? false);
			}
			set
			{
				base[SearchObjectSchema.EstimateOnly] = value;
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x0009E3A2 File Offset: 0x0009C5A2
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x0009E3BE File Offset: 0x0009C5BE
		[DataMember]
		public bool ExcludeDuplicateMessages
		{
			get
			{
				return (bool)(base[SearchObjectSchema.ExcludeDuplicateMessages] ?? true);
			}
			set
			{
				base[SearchObjectSchema.ExcludeDuplicateMessages] = value;
			}
		}
	}
}
