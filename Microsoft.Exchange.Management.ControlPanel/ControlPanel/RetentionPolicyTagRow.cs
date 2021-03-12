using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200026B RID: 619
	[DataContract]
	public class RetentionPolicyTagRow : RetentionPolicyTagBaseRow
	{
		// Token: 0x0600297B RID: 10619 RVA: 0x00082AFE File Offset: 0x00080CFE
		public RetentionPolicyTagRow(PresentationRetentionPolicyTag rpt) : base(rpt)
		{
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x00082B07 File Offset: 0x00080D07
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x00082B22 File Offset: 0x00080D22
		[DataMember]
		public string RetentionPolicyTagType
		{
			get
			{
				return base.OptionalTag ? OwaOptionStrings.RetentionTypeOptional : OwaOptionStrings.RetentionTypeRequired;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x00082B29 File Offset: 0x00080D29
		protected override string GetLocalizedRetentionActionType()
		{
			if (base.DefaultTag)
			{
				return (base.RetentionPolicyTag.RetentionAction == RetentionActionType.MoveToArchive) ? OwaOptionStrings.RetentionActionTypeDefaultArchive : OwaOptionStrings.RetentionActionTypeDefaultDelete;
			}
			return base.GetLocalizedRetentionActionType();
		}
	}
}
