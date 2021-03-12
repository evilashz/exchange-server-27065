using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200026A RID: 618
	[DataContract]
	public class RetentionPolicyTagBaseRow : BaseRow
	{
		// Token: 0x06002964 RID: 10596 RVA: 0x000827B2 File Offset: 0x000809B2
		public RetentionPolicyTagBaseRow(PresentationRetentionPolicyTag rpt) : base(rpt)
		{
			this.RetentionPolicyTag = rpt;
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x000827C2 File Offset: 0x000809C2
		// (set) Token: 0x06002966 RID: 10598 RVA: 0x000827CA File Offset: 0x000809CA
		public PresentationRetentionPolicyTag RetentionPolicyTag { get; set; }

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000827D4 File Offset: 0x000809D4
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x00082806 File Offset: 0x00080A06
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.RetentionPolicyTag.GetLocalizedFolderName(new CultureInfo[]
				{
					Thread.CurrentThread.CurrentUICulture
				}.AsEnumerable<CultureInfo>());
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x00082810 File Offset: 0x00080A10
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x00082860 File Offset: 0x00080A60
		[DataMember]
		public int RetentionDays
		{
			get
			{
				if (this.RetentionPolicyTag.AgeLimitForRetention == null || !this.RetentionPolicyTag.RetentionEnabled)
				{
					return int.MaxValue;
				}
				return this.RetentionPolicyTag.AgeLimitForRetention.Value.Days;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x00082868 File Offset: 0x00080A68
		// (set) Token: 0x0600296C RID: 10604 RVA: 0x000828C3 File Offset: 0x00080AC3
		[DataMember]
		public string RetentionPeriod
		{
			get
			{
				if (this.RetentionPolicyTag.AgeLimitForRetention != null && this.RetentionPolicyTag.RetentionEnabled)
				{
					return this.RetentionDuration(this.RetentionPolicyTag.AgeLimitForRetention.Value.Days);
				}
				return OwaOptionStrings.Unlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x000828CA File Offset: 0x00080ACA
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x000828E8 File Offset: 0x00080AE8
		[DataMember]
		public string RetentionAction
		{
			get
			{
				return this.GetLocalizedRetentionAction(this.RetentionPolicyTag.RetentionEnabled, this.RetentionPolicyTag.RetentionAction);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x000828EF File Offset: 0x00080AEF
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x000828F7 File Offset: 0x00080AF7
		[DataMember]
		public string RetentionPolicyActionType
		{
			get
			{
				return this.GetLocalizedRetentionActionType();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x000828FE File Offset: 0x00080AFE
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x00082906 File Offset: 0x00080B06
		[DataMember]
		public bool OptionalTag { get; set; }

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x0008290F File Offset: 0x00080B0F
		// (set) Token: 0x06002974 RID: 10612 RVA: 0x00082920 File Offset: 0x00080B20
		public bool DefaultTag
		{
			get
			{
				return this.RetentionPolicyTag.Type == ElcFolderType.All;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00082928 File Offset: 0x00080B28
		public static string GetSortProperty(string displayProperty)
		{
			string result = displayProperty;
			if (displayProperty == "RetentionPeriod")
			{
				result = "RetentionDays";
			}
			return result;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0008294C File Offset: 0x00080B4C
		public override bool Equals(object obj)
		{
			return obj != null && (object.ReferenceEquals(obj, this) || (base.GetType() == obj.GetType() && this.RetentionPolicyTag.Guid == (obj as RetentionPolicyTagBaseRow).RetentionPolicyTag.Guid));
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000829A0 File Offset: 0x00080BA0
		public override int GetHashCode()
		{
			return this.RetentionPolicyTag.Guid.GetHashCode();
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000829C6 File Offset: 0x00080BC6
		protected virtual string GetLocalizedRetentionActionType()
		{
			return (this.RetentionPolicyTag.RetentionAction == RetentionActionType.MoveToArchive) ? OwaOptionStrings.RetentionActionTypeArchive : OwaOptionStrings.RetentionActionTypeDelete;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000829E8 File Offset: 0x00080BE8
		private string RetentionDuration(int days)
		{
			if (days == 0)
			{
				return OwaOptionStrings.RPTExpireNever;
			}
			if (days <= 90)
			{
				return string.Format((days > 1) ? OwaOptionStrings.RPTDays : OwaOptionStrings.RPTDay, days);
			}
			int num = days / 365;
			int num2 = days % 365 / 30;
			string text = string.Format((num2 > 1) ? OwaOptionStrings.RPTMonths : OwaOptionStrings.RPTMonth, num2);
			string text2 = string.Format((num > 1) ? OwaOptionStrings.RPTYears : OwaOptionStrings.RPTYear, num);
			if (num != 0 && num2 != 0)
			{
				return string.Format(OwaOptionStrings.RPTYearsMonths, text2, text);
			}
			if (num != 0)
			{
				return text2;
			}
			if (num2 != 0)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00082AA8 File Offset: 0x00080CA8
		private string GetLocalizedRetentionAction(bool retentionEnabled, RetentionActionType retentionActionType)
		{
			string result = LocalizedDescriptionAttribute.FromEnum(typeof(RetentionActionType), retentionActionType);
			bool flag = retentionActionType == RetentionActionType.MoveToArchive;
			if (retentionActionType == RetentionActionType.DeleteAndAllowRecovery)
			{
				result = OwaOptionStrings.RetentionActionDeleteAndAllowRecovery;
			}
			if (!retentionEnabled)
			{
				if (flag)
				{
					result = OwaOptionStrings.RetentionActionNeverMove;
				}
				else
				{
					result = OwaOptionStrings.RetentionActionNeverDelete;
				}
			}
			return result;
		}
	}
}
