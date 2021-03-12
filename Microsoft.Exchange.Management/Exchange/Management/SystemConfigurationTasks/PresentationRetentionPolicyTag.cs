using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200031C RID: 796
	[Serializable]
	public class PresentationRetentionPolicyTag : RetentionPolicyTag
	{
		// Token: 0x06001AE6 RID: 6886 RVA: 0x000777A0 File Offset: 0x000759A0
		public PresentationRetentionPolicyTag(RetentionPolicyTag retentionPolicyTag)
		{
			this.propertyBag = retentionPolicyTag.propertyBag;
			this.m_Session = retentionPolicyTag.m_Session;
			this.contentSettings = retentionPolicyTag.GetELCContentSettings().FirstOrDefault<ElcContentSettings>();
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000777D1 File Offset: 0x000759D1
		public PresentationRetentionPolicyTag() : this(new RetentionPolicyTag(), new ElcContentSettings())
		{
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000777E3 File Offset: 0x000759E3
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.contentSettings != null)
			{
				errors.AddRange(this.contentSettings.Validate());
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00077805 File Offset: 0x00075A05
		public PresentationRetentionPolicyTag(RetentionPolicyTag retentionPolicyTag, ElcContentSettings contentSettings)
		{
			this.propertyBag = retentionPolicyTag.propertyBag;
			this.m_Session = retentionPolicyTag.m_Session;
			this.contentSettings = contentSettings;
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0007782C File Offset: 0x00075A2C
		public string MessageClassDisplayName
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (string)this.contentSettings[ElcContentSettingsSchema.MessageClassDisplayName];
				}
				return null;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0007784D File Offset: 0x00075A4D
		public string MessageClass
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (string)this.contentSettings[ElcContentSettingsSchema.MessageClass];
				}
				return null;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0007786E File Offset: 0x00075A6E
		public string Description
		{
			get
			{
				if (this.contentSettings != null)
				{
					return this.contentSettings.Description;
				}
				return null;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00077885 File Offset: 0x00075A85
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x000778A6 File Offset: 0x00075AA6
		public bool RetentionEnabled
		{
			get
			{
				return this.contentSettings != null && (bool)this.contentSettings[ElcContentSettingsSchema.RetentionEnabled];
			}
			set
			{
				if (this.contentSettings != null)
				{
					this.contentSettings.RetentionEnabled = value;
				}
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000778BC File Offset: 0x00075ABC
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x000778DD File Offset: 0x00075ADD
		public RetentionActionType RetentionAction
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (RetentionActionType)this.contentSettings[ElcContentSettingsSchema.RetentionAction];
				}
				return RetentionActionType.DeleteAndAllowRecovery;
			}
			set
			{
				if (this.contentSettings != null)
				{
					this.contentSettings.RetentionAction = value;
				}
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000778F4 File Offset: 0x00075AF4
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00077928 File Offset: 0x00075B28
		public EnhancedTimeSpan? AgeLimitForRetention
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (EnhancedTimeSpan?)this.contentSettings[ElcContentSettingsSchema.AgeLimitForRetention];
				}
				return null;
			}
			set
			{
				if (this.contentSettings != null)
				{
					this.contentSettings.AgeLimitForRetention = value;
				}
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0007793E File Offset: 0x00075B3E
		public ADObjectId MoveToDestinationFolder
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (ADObjectId)this.contentSettings[ElcContentSettingsSchema.MoveToDestinationFolder];
				}
				return null;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0007795F File Offset: 0x00075B5F
		public RetentionDateType TriggerForRetention
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (RetentionDateType)this.contentSettings[ElcContentSettingsSchema.TriggerForRetention];
				}
				return RetentionDateType.WhenDelivered;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00077980 File Offset: 0x00075B80
		public JournalingFormat MessageFormatForJournaling
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (JournalingFormat)this.contentSettings[ElcContentSettingsSchema.MessageFormatForJournaling];
				}
				return JournalingFormat.UseMsg;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000779A1 File Offset: 0x00075BA1
		public bool JournalingEnabled
		{
			get
			{
				return this.contentSettings != null && (bool)this.contentSettings[ElcContentSettingsSchema.JournalingEnabled];
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000779C2 File Offset: 0x00075BC2
		public ADObjectId AddressForJournaling
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (ADObjectId)this.contentSettings[ElcContentSettingsSchema.AddressForJournaling];
				}
				return null;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x000779E3 File Offset: 0x00075BE3
		public string LabelForJournaling
		{
			get
			{
				if (this.contentSettings != null)
				{
					return (string)this.contentSettings[ElcContentSettingsSchema.LabelForJournaling];
				}
				return null;
			}
		}

		// Token: 0x04000BB4 RID: 2996
		private ElcContentSettings contentSettings;
	}
}
