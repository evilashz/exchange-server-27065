using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200020F RID: 527
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("New", "ExchangeUpgradeBucket", SupportsShouldProcess = true)]
	public sealed class NewExchangeUpgradeBucket : NewSystemConfigurationObjectTask<ExchangeUpgradeBucket>
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004E9B9 File Offset: 0x0004CBB9
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x0004E9D0 File Offset: 0x0004CBD0
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxMailboxes
		{
			get
			{
				return (Unlimited<int>)base.Fields[ExchangeUpgradeBucketSchema.MaxMailboxes];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.MaxMailboxes] = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004E9E8 File Offset: 0x0004CBE8
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x0004E9FF File Offset: 0x0004CBFF
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)base.Fields[ExchangeUpgradeBucketSchema.Description];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.Description] = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004EA12 File Offset: 0x0004CC12
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x0004EA29 File Offset: 0x0004CC29
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields[ExchangeUpgradeBucketSchema.Enabled];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.Enabled] = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0004EA41 File Offset: 0x0004CC41
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x0004EA58 File Offset: 0x0004CC58
		[ValidateRange(1, 999)]
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)base.Fields[ExchangeUpgradeBucketSchema.Priority];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.Priority] = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004EA70 File Offset: 0x0004CC70
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0004EA87 File Offset: 0x0004CC87
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string SourceVersion
		{
			get
			{
				return (string)base.Fields[ExchangeUpgradeBucketSchema.SourceVersion];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.SourceVersion] = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004EA9A File Offset: 0x0004CC9A
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0004EAB1 File Offset: 0x0004CCB1
		[Parameter(Mandatory = false)]
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)base.Fields[ExchangeUpgradeBucketSchema.StartDate];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.StartDate] = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004EAC9 File Offset: 0x0004CCC9
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004EAE0 File Offset: 0x0004CCE0
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string TargetVersion
		{
			get
			{
				return (string)base.Fields[ExchangeUpgradeBucketSchema.TargetVersion];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.TargetVersion] = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004EAF3 File Offset: 0x0004CCF3
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0004EB0A File Offset: 0x0004CD0A
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 4)]
		public OrganizationUpgradeStage[] DisabledUpgradeStages
		{
			get
			{
				return (OrganizationUpgradeStage[])base.Fields[ExchangeUpgradeBucketSchema.DisabledUpgradeStages];
			}
			set
			{
				base.Fields[ExchangeUpgradeBucketSchema.DisabledUpgradeStages] = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004EB1D File Offset: 0x0004CD1D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewExchangeUpgradeBucket(this.DataObject.Name);
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004EB2F File Offset: 0x0004CD2F
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is ArgumentException;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004EB48 File Offset: 0x0004CD48
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ExchangeUpgradeBucket exchangeUpgradeBucket = (ExchangeUpgradeBucket)base.PrepareDataObject();
			exchangeUpgradeBucket.SetId(this.ConfigurationSession, base.Name);
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.Enabled))
			{
				exchangeUpgradeBucket.Enabled = this.Enabled;
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.MaxMailboxes))
			{
				exchangeUpgradeBucket.MaxMailboxes = this.MaxMailboxes;
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.Description))
			{
				exchangeUpgradeBucket.Description = this.Description;
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.Priority))
			{
				exchangeUpgradeBucket.Priority = this.Priority;
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.SourceVersion))
			{
				exchangeUpgradeBucket.SourceVersion = NewExchangeUpgradeBucket.TranslateKnownVersion(this.SourceVersion);
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.StartDate))
			{
				exchangeUpgradeBucket.StartDate = this.StartDate;
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.TargetVersion))
			{
				exchangeUpgradeBucket.TargetVersion = NewExchangeUpgradeBucket.TranslateKnownVersion(this.TargetVersion);
			}
			if (base.Fields.IsChanged(ExchangeUpgradeBucketSchema.DisabledUpgradeStages))
			{
				exchangeUpgradeBucket.DisabledUpgradeStages = this.DisabledUpgradeStages;
			}
			TaskLogger.LogExit();
			return exchangeUpgradeBucket;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004EC78 File Offset: 0x0004CE78
		private static string TranslateKnownVersion(string sourceVersion)
		{
			if ("R5".Equals(sourceVersion, StringComparison.OrdinalIgnoreCase))
			{
				return "14.1.225.*";
			}
			if ("R6".Equals(sourceVersion, StringComparison.OrdinalIgnoreCase))
			{
				return "14.16.*";
			}
			return sourceVersion;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004ECA3 File Offset: 0x0004CEA3
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			UpgradeBucketTaskHelper.ValidateSourceAndTargetVersions(this.DataObject.SourceVersion, this.DataObject.TargetVersion, new Task.ErrorLoggerDelegate(base.WriteError));
			TaskLogger.LogExit();
		}
	}
}
