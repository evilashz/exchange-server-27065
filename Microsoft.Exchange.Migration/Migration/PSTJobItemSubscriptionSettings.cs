using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSTJobItemSubscriptionSettings : JobItemSubscriptionSettingsBase
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00029500 File Offset: 0x00027700
		internal PSTJobItemSubscriptionSettings()
		{
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00029508 File Offset: 0x00027708
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00029510 File Offset: 0x00027710
		public string PstFilePath { get; private set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00029519 File Offset: 0x00027719
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00029521 File Offset: 0x00027721
		public bool? PrimaryOnly { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0002952A File Offset: 0x0002772A
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00029532 File Offset: 0x00027732
		public bool? ArchiveOnly { get; private set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002953B File Offset: 0x0002773B
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00029543 File Offset: 0x00027743
		public string SourceRootFolder { get; private set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002954C File Offset: 0x0002774C
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00029554 File Offset: 0x00027754
		public string TargetRootFolder { get; private set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0002955D File Offset: 0x0002775D
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return PSTJobItemSubscriptionSettings.PSTJobItemSubscriptionSettingsPropertyDefinitions;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00029564 File Offset: 0x00027764
		protected override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && string.IsNullOrEmpty(this.PstFilePath) && this.PrimaryOnly == null && this.ArchiveOnly == null && string.IsNullOrEmpty(this.SourceRootFolder) && string.IsNullOrEmpty(this.TargetRootFolder);
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000295C0 File Offset: 0x000277C0
		internal static PSTJobItemSubscriptionSettings CreateFromProperties(string pstFilePath)
		{
			return new PSTJobItemSubscriptionSettings
			{
				PstFilePath = pstFilePath,
				PrimaryOnly = new bool?(true),
				LastModifiedTime = ExDateTime.UtcNow
			};
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000295F4 File Offset: 0x000277F4
		public override JobItemSubscriptionSettingsBase Clone()
		{
			return new PSTJobItemSubscriptionSettings
			{
				PstFilePath = this.PstFilePath,
				PrimaryOnly = this.PrimaryOnly,
				ArchiveOnly = this.ArchiveOnly,
				SourceRootFolder = this.SourceRootFolder,
				TargetRootFolder = this.TargetRootFolder,
				LastModifiedTime = base.LastModifiedTime
			};
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00029650 File Offset: 0x00027850
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			if (!string.IsNullOrEmpty(this.PstFilePath))
			{
				message[MigrationBatchMessageSchema.MigrationPSTFilePath] = this.PstFilePath;
			}
			if (!string.IsNullOrEmpty(this.SourceRootFolder))
			{
				message[MigrationBatchMessageSchema.MigrationSourceRootFolder] = this.SourceRootFolder;
			}
			if (!string.IsNullOrEmpty(this.TargetRootFolder))
			{
				message[MigrationBatchMessageSchema.MigrationTargetRootFolder] = this.TargetRootFolder;
			}
			MigrationHelper.WriteOrDeleteNullableProperty<bool?>(message, MigrationBatchMessageSchema.MigrationJobPrimaryOnly, this.PrimaryOnly);
			MigrationHelper.WriteOrDeleteNullableProperty<bool?>(message, MigrationBatchMessageSchema.MigrationJobArchiveOnly, this.ArchiveOnly);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000296E4 File Offset: 0x000278E4
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.PstFilePath = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationPSTFilePath, null);
			this.PrimaryOnly = message.GetValueOrDefault<bool?>(MigrationBatchMessageSchema.MigrationJobPrimaryOnly, null);
			this.ArchiveOnly = message.GetValueOrDefault<bool?>(MigrationBatchMessageSchema.MigrationJobArchiveOnly, null);
			JobSubscriptionSettingsBase.ValidatePrimaryArchiveExclusivity(this.PrimaryOnly, this.ArchiveOnly);
			this.SourceRootFolder = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationSourceRootFolder, null);
			this.TargetRootFolder = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationTargetRootFolder, null);
			return base.ReadFromMessageItem(message);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00029774 File Offset: 0x00027974
		public override void UpdateFromDataRow(IMigrationDataRow request)
		{
			bool flag = false;
			PSTMigrationDataRow pstmigrationDataRow = request as PSTMigrationDataRow;
			if (pstmigrationDataRow == null)
			{
				throw new ArgumentException("expected an PSTMigrationDataRow", "request");
			}
			if (!string.Equals(this.PstFilePath, pstmigrationDataRow.PSTFilePath))
			{
				this.PstFilePath = pstmigrationDataRow.PSTFilePath;
				flag = true;
			}
			bool flag2 = false;
			bool flag3 = false;
			if (pstmigrationDataRow.TargetMailboxType == MigrationMailboxType.PrimaryAndArchive)
			{
				flag2 = true;
				flag3 = true;
			}
			else if (pstmigrationDataRow.TargetMailboxType == MigrationMailboxType.PrimaryOnly)
			{
				flag2 = true;
			}
			else
			{
				flag3 = true;
			}
			if (!object.Equals(this.PrimaryOnly, flag2) || !object.Equals(this.ArchiveOnly, flag3))
			{
				this.PrimaryOnly = new bool?(flag2);
				this.ArchiveOnly = new bool?(flag3);
				JobSubscriptionSettingsBase.ValidatePrimaryArchiveExclusivity(this.PrimaryOnly, this.ArchiveOnly);
				flag = true;
			}
			if (!string.Equals(this.SourceRootFolder, pstmigrationDataRow.SourceRootFolder))
			{
				this.SourceRootFolder = pstmigrationDataRow.SourceRootFolder;
				flag = true;
			}
			if (!string.Equals(this.TargetRootFolder, pstmigrationDataRow.TargetRootFolder))
			{
				this.TargetRootFolder = pstmigrationDataRow.TargetRootFolder;
				flag = true;
			}
			if (flag || base.LastModifiedTime == ExDateTime.MinValue)
			{
				base.LastModifiedTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002989C File Offset: 0x00027A9C
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			string content = string.IsNullOrEmpty(this.PstFilePath) ? string.Empty : this.PstFilePath;
			string content2 = string.IsNullOrEmpty(this.SourceRootFolder) ? string.Empty : this.SourceRootFolder;
			string content3 = string.IsNullOrEmpty(this.TargetRootFolder) ? string.Empty : this.TargetRootFolder;
			parent.Add(new object[]
			{
				new XElement("PSTFilePath", content),
				new XElement("PrimaryOnly", this.PrimaryOnly),
				new XElement("ArchiveOnly", this.ArchiveOnly),
				new XElement("SourceRootFolder", content2),
				new XElement("TargetRootFolder", content3)
			});
		}

		// Token: 0x040003D8 RID: 984
		public static readonly PropertyDefinition[] PSTJobItemSubscriptionSettingsPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationPSTFilePath,
				MigrationBatchMessageSchema.MigrationJobPrimaryOnly,
				MigrationBatchMessageSchema.MigrationJobArchiveOnly,
				MigrationBatchMessageSchema.MigrationSourceRootFolder,
				MigrationBatchMessageSchema.MigrationTargetRootFolder
			},
			SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions
		});
	}
}
