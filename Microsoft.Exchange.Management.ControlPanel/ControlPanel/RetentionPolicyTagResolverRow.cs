using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000268 RID: 616
	[DataContract]
	public class RetentionPolicyTagResolverRow : AdObjectResolverRow
	{
		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000824DB File Offset: 0x000806DB
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x000824E3 File Offset: 0x000806E3
		internal ElcContentSettings ContentSettings
		{
			get
			{
				return this.contentSettings;
			}
			set
			{
				this.contentSettings = value;
			}
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000824EC File Offset: 0x000806EC
		public RetentionPolicyTagResolverRow(ADRawEntry aDRawEntry) : base(aDRawEntry)
		{
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000824F5 File Offset: 0x000806F5
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0008250C File Offset: 0x0008070C
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base.ADRawEntry[ADObjectSchema.Name];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x00082513 File Offset: 0x00080713
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x0008252F File Offset: 0x0008072F
		[DataMember]
		public string Type
		{
			get
			{
				return RetentionUtils.GetLocalizedType((ElcFolderType)base.ADRawEntry[RetentionPolicyTagSchema.Type]);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x00082538 File Offset: 0x00080738
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x000825C3 File Offset: 0x000807C3
		[DataMember]
		public string RetentionPeriodDays
		{
			get
			{
				bool flag = this.contentSettings != null && this.contentSettings.RetentionEnabled;
				EnhancedTimeSpan? enhancedTimeSpan = (this.contentSettings != null) ? this.contentSettings.AgeLimitForRetention : null;
				if (enhancedTimeSpan != null && flag)
				{
					int days = enhancedTimeSpan.Value.Days;
					return string.Format((days > 1) ? Strings.RPTDays : Strings.RPTDay, days);
				}
				return Strings.Unlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000825CC File Offset: 0x000807CC
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x00082630 File Offset: 0x00080830
		[DataMember]
		public int RetentionDays
		{
			get
			{
				bool flag = this.contentSettings != null && this.contentSettings.RetentionEnabled;
				EnhancedTimeSpan? enhancedTimeSpan = (this.contentSettings != null) ? this.contentSettings.AgeLimitForRetention : null;
				if (enhancedTimeSpan != null && flag)
				{
					return enhancedTimeSpan.Value.Days;
				}
				return int.MaxValue;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x00082638 File Offset: 0x00080838
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x00082662 File Offset: 0x00080862
		[DataMember]
		public string RetentionPolicyActionType
		{
			get
			{
				RetentionActionType retentionActionType = (this.contentSettings != null) ? this.contentSettings.RetentionAction : RetentionActionType.DeleteAndAllowRecovery;
				return RetentionUtils.GetLocalizedRetentionActionType(retentionActionType);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040020D1 RID: 8401
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ADObjectSchema.Name,
			RetentionPolicyTagSchema.Type
		}.ToArray();

		// Token: 0x040020D2 RID: 8402
		private ElcContentSettings contentSettings;
	}
}
