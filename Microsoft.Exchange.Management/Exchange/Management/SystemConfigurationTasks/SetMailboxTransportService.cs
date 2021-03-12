using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009CC RID: 2508
	[Cmdlet("Set", "MailboxTransportService", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxTransportService : SetSystemConfigurationObjectTask<MailboxTransportServerIdParameter, MailboxTransportServerPresentationObject, MailboxTransportServer>
	{
		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06005982 RID: 22914 RVA: 0x00177E77 File Offset: 0x00176077
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailboxTransportService(this.Identity.ToString());
			}
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x06005983 RID: 22915 RVA: 0x00177E89 File Offset: 0x00176089
		// (set) Token: 0x06005984 RID: 22916 RVA: 0x00177EA0 File Offset: 0x001760A0
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxSubmissionAgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["MailboxSubmissionAgentLogMaxAge"];
			}
			set
			{
				base.Fields["MailboxSubmissionAgentLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x00177EB8 File Offset: 0x001760B8
		// (set) Token: 0x06005986 RID: 22918 RVA: 0x00177ECF File Offset: 0x001760CF
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["MailboxSubmissionAgentLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["MailboxSubmissionAgentLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x00177EE7 File Offset: 0x001760E7
		// (set) Token: 0x06005988 RID: 22920 RVA: 0x00177EFE File Offset: 0x001760FE
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["MailboxSubmissionAgentLogMaxFileSize"];
			}
			set
			{
				base.Fields["MailboxSubmissionAgentLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x00177F16 File Offset: 0x00176116
		// (set) Token: 0x0600598A RID: 22922 RVA: 0x00177F2D File Offset: 0x0017612D
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MailboxSubmissionAgentLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["MailboxSubmissionAgentLogPath"];
			}
			set
			{
				base.Fields["MailboxSubmissionAgentLogPath"] = value;
			}
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x00177F40 File Offset: 0x00176140
		// (set) Token: 0x0600598C RID: 22924 RVA: 0x00177F57 File Offset: 0x00176157
		[Parameter(Mandatory = false)]
		public bool MailboxSubmissionAgentLogEnabled
		{
			get
			{
				return (bool)base.Fields["MailboxSubmissionAgentLogEnabled"];
			}
			set
			{
				base.Fields["MailboxSubmissionAgentLogEnabled"] = value;
			}
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x0600598D RID: 22925 RVA: 0x00177F6F File Offset: 0x0017616F
		// (set) Token: 0x0600598E RID: 22926 RVA: 0x00177F86 File Offset: 0x00176186
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxDeliveryAgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["MailboxDeliveryAgentLogMaxAge"];
			}
			set
			{
				base.Fields["MailboxDeliveryAgentLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x00177F9E File Offset: 0x0017619E
		// (set) Token: 0x06005990 RID: 22928 RVA: 0x00177FB5 File Offset: 0x001761B5
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["MailboxDeliveryAgentLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["MailboxDeliveryAgentLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x00177FCD File Offset: 0x001761CD
		// (set) Token: 0x06005992 RID: 22930 RVA: 0x00177FE4 File Offset: 0x001761E4
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["MailboxDeliveryAgentLogMaxFileSize"];
			}
			set
			{
				base.Fields["MailboxDeliveryAgentLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x00177FFC File Offset: 0x001761FC
		// (set) Token: 0x06005994 RID: 22932 RVA: 0x00178013 File Offset: 0x00176213
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MailboxDeliveryAgentLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["MailboxDeliveryAgentLogPath"];
			}
			set
			{
				base.Fields["MailboxDeliveryAgentLogPath"] = value;
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06005995 RID: 22933 RVA: 0x00178026 File Offset: 0x00176226
		// (set) Token: 0x06005996 RID: 22934 RVA: 0x0017803D File Offset: 0x0017623D
		[Parameter(Mandatory = false)]
		public bool MailboxDeliveryAgentLogEnabled
		{
			get
			{
				return (bool)base.Fields["MailboxDeliveryAgentLogEnabled"];
			}
			set
			{
				base.Fields["MailboxDeliveryAgentLogEnabled"] = value;
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06005997 RID: 22935 RVA: 0x00178055 File Offset: 0x00176255
		// (set) Token: 0x06005998 RID: 22936 RVA: 0x0017806C File Offset: 0x0017626C
		[Parameter(Mandatory = false)]
		public bool MailboxDeliveryThrottlingLogEnabled
		{
			get
			{
				return (bool)base.Fields["MailboxDeliveryThrottlingLogEnabled"];
			}
			set
			{
				base.Fields["MailboxDeliveryThrottlingLogEnabled"] = value;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x00178084 File Offset: 0x00176284
		// (set) Token: 0x0600599A RID: 22938 RVA: 0x0017809B File Offset: 0x0017629B
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxDeliveryThrottlingLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["MailboxDeliveryThrottlingLogMaxAge"];
			}
			set
			{
				base.Fields["MailboxDeliveryThrottlingLogMaxAge"] = value;
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x0600599B RID: 22939 RVA: 0x001780B3 File Offset: 0x001762B3
		// (set) Token: 0x0600599C RID: 22940 RVA: 0x001780CA File Offset: 0x001762CA
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["MailboxDeliveryThrottlingLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["MailboxDeliveryThrottlingLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x001780E2 File Offset: 0x001762E2
		// (set) Token: 0x0600599E RID: 22942 RVA: 0x001780F9 File Offset: 0x001762F9
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["MailboxDeliveryThrottlingLogMaxFileSize"];
			}
			set
			{
				base.Fields["MailboxDeliveryThrottlingLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x00178111 File Offset: 0x00176311
		// (set) Token: 0x060059A0 RID: 22944 RVA: 0x00178128 File Offset: 0x00176328
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MailboxDeliveryThrottlingLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["MailboxDeliveryThrottlingLogPath"];
			}
			set
			{
				base.Fields["MailboxDeliveryThrottlingLogPath"] = value;
			}
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0017813C File Offset: 0x0017633C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified("MailboxSubmissionAgentLogMaxAge"))
			{
				this.DataObject.MailboxSubmissionAgentLogMaxAge = this.MailboxSubmissionAgentLogMaxAge;
			}
			if (base.Fields.IsModified("MailboxSubmissionAgentLogMaxDirectorySize"))
			{
				this.DataObject.MailboxSubmissionAgentLogMaxDirectorySize = this.MailboxSubmissionAgentLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("MailboxSubmissionAgentLogMaxFileSize"))
			{
				this.DataObject.MailboxSubmissionAgentLogMaxFileSize = this.MailboxSubmissionAgentLogMaxFileSize;
			}
			if (base.Fields.IsModified("MailboxSubmissionAgentLogPath"))
			{
				this.DataObject.MailboxSubmissionAgentLogPath = this.MailboxSubmissionAgentLogPath;
			}
			if (base.Fields.IsModified("MailboxSubmissionAgentLogEnabled"))
			{
				this.DataObject.MailboxSubmissionAgentLogEnabled = this.MailboxSubmissionAgentLogEnabled;
			}
			if (base.Fields.IsModified("MailboxDeliveryAgentLogMaxAge"))
			{
				this.DataObject.MailboxDeliveryAgentLogMaxAge = this.MailboxDeliveryAgentLogMaxAge;
			}
			if (base.Fields.IsModified("MailboxDeliveryAgentLogMaxDirectorySize"))
			{
				this.DataObject.MailboxDeliveryAgentLogMaxDirectorySize = this.MailboxDeliveryAgentLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("MailboxDeliveryAgentLogMaxFileSize"))
			{
				this.DataObject.MailboxDeliveryAgentLogMaxFileSize = this.MailboxDeliveryAgentLogMaxFileSize;
			}
			if (base.Fields.IsModified("MailboxDeliveryAgentLogPath"))
			{
				this.DataObject.MailboxDeliveryAgentLogPath = this.MailboxDeliveryAgentLogPath;
			}
			if (base.Fields.IsModified("MailboxDeliveryAgentLogEnabled"))
			{
				this.DataObject.MailboxDeliveryAgentLogEnabled = this.MailboxDeliveryAgentLogEnabled;
			}
			if (base.Fields.IsModified("MailboxDeliveryThrottlingLogEnabled"))
			{
				this.DataObject.MailboxDeliveryThrottlingLogEnabled = this.MailboxDeliveryThrottlingLogEnabled;
			}
			if (base.Fields.IsModified("MailboxDeliveryThrottlingLogMaxAge"))
			{
				this.DataObject.MailboxDeliveryThrottlingLogMaxAge = this.MailboxDeliveryThrottlingLogMaxAge;
			}
			if (base.Fields.IsModified("MailboxDeliveryThrottlingLogMaxDirectorySize"))
			{
				this.DataObject.MailboxDeliveryThrottlingLogMaxDirectorySize = this.MailboxDeliveryThrottlingLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("MailboxDeliveryThrottlingLogMaxFileSize"))
			{
				this.DataObject.MailboxDeliveryThrottlingLogMaxFileSize = this.MailboxDeliveryThrottlingLogMaxFileSize;
			}
			if (base.Fields.IsModified("MailboxDeliveryThrottlingLogPath"))
			{
				this.DataObject.MailboxDeliveryThrottlingLogPath = this.MailboxDeliveryThrottlingLogPath;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x00178368 File Offset: 0x00176568
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Instance.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServerNameModified), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.Identity != null)
			{
				this.Identity = MailboxTransportServerIdParameter.CreateIdentity(this.Identity);
			}
			base.InternalValidate();
			if (!this.DataObject.IsMailboxServer)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTaskRunningLocationMailboxOnly), ErrorCategory.InvalidOperation, null);
			}
			if (base.HasErrors)
			{
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400333C RID: 13116
		private const string MailboxSubmissionAgentLogMaxAgeKey = "MailboxSubmissionAgentLogMaxAge";

		// Token: 0x0400333D RID: 13117
		private const string MailboxSubmissionAgentLogMaxDirectorySizeKey = "MailboxSubmissionAgentLogMaxDirectorySize";

		// Token: 0x0400333E RID: 13118
		private const string MailboxSubmissionAgentLogMaxFileSizeKey = "MailboxSubmissionAgentLogMaxFileSize";

		// Token: 0x0400333F RID: 13119
		private const string MailboxSubmissionAgentLogPathKey = "MailboxSubmissionAgentLogPath";

		// Token: 0x04003340 RID: 13120
		private const string MailboxSubmissionAgentLogEnabledKey = "MailboxSubmissionAgentLogEnabled";

		// Token: 0x04003341 RID: 13121
		private const string MailboxDeliveryAgentLogMaxAgeKey = "MailboxDeliveryAgentLogMaxAge";

		// Token: 0x04003342 RID: 13122
		private const string MailboxDeliveryAgentLogMaxDirectorySizeKey = "MailboxDeliveryAgentLogMaxDirectorySize";

		// Token: 0x04003343 RID: 13123
		private const string MailboxDeliveryAgentLogMaxFileSizeKey = "MailboxDeliveryAgentLogMaxFileSize";

		// Token: 0x04003344 RID: 13124
		private const string MailboxDeliveryAgentLogPathKey = "MailboxDeliveryAgentLogPath";

		// Token: 0x04003345 RID: 13125
		private const string MailboxDeliveryAgentLogEnabledKey = "MailboxDeliveryAgentLogEnabled";

		// Token: 0x04003346 RID: 13126
		private const string MailboxDeliveryThrottlingLogEnabledKey = "MailboxDeliveryThrottlingLogEnabled";

		// Token: 0x04003347 RID: 13127
		private const string MailboxDeliveryThrottlingLogMaxAgeKey = "MailboxDeliveryThrottlingLogMaxAge";

		// Token: 0x04003348 RID: 13128
		private const string MailboxDeliveryThrottlingLogMaxDirectorySizeKey = "MailboxDeliveryThrottlingLogMaxDirectorySize";

		// Token: 0x04003349 RID: 13129
		private const string MailboxDeliveryThrottlingLogMaxFileSizeKey = "MailboxDeliveryThrottlingLogMaxFileSize";

		// Token: 0x0400334A RID: 13130
		private const string MailboxDeliveryThrottlingLogPathKey = "MailboxDeliveryThrottlingLogPath";
	}
}
