using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200026D RID: 621
	[DataContract]
	public class ViewRetentionPolicyTagRow : OptionalRetentionPolicyTagRow
	{
		// Token: 0x06002982 RID: 10626 RVA: 0x00082B9D File Offset: 0x00080D9D
		public ViewRetentionPolicyTagRow(PresentationRetentionPolicyTag rpt) : base(rpt)
		{
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x00082BA6 File Offset: 0x00080DA6
		// (set) Token: 0x06002984 RID: 10628 RVA: 0x00082BB3 File Offset: 0x00080DB3
		[DataMember]
		public string AppliesTo
		{
			get
			{
				return base.RetentionPolicyTag.MessageClassDisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x00082BBC File Offset: 0x00080DBC
		// (set) Token: 0x06002986 RID: 10630 RVA: 0x00082BF8 File Offset: 0x00080DF8
		[DataMember]
		public string RetentionPolicyTagTypeDescription
		{
			get
			{
				string result = string.Empty;
				if (!base.OptionalTag)
				{
					if (base.DefaultTag)
					{
						result = OwaOptionStrings.DefaultRetentionTagDescription;
					}
					else
					{
						result = OwaOptionStrings.RetentionTypeRequiredDescription;
					}
				}
				return result;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x00082C00 File Offset: 0x00080E00
		// (set) Token: 0x06002988 RID: 10632 RVA: 0x00082C2C File Offset: 0x00080E2C
		[DataMember]
		public string DescriptionLabel
		{
			get
			{
				string description = base.Description;
				if (!string.IsNullOrEmpty(description))
				{
					return OwaOptionStrings.ViewRPTDescriptionLabel;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x00082C34 File Offset: 0x00080E34
		// (set) Token: 0x0600298A RID: 10634 RVA: 0x00082C74 File Offset: 0x00080E74
		[DataMember]
		public string RetentionPeriodDetail
		{
			get
			{
				if (base.RetentionPolicyTag.AgeLimitForRetention != null && base.RetentionPolicyTag.RetentionEnabled)
				{
					return base.RetentionPeriod;
				}
				return OwaOptionStrings.RPTNone;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
