using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000AA RID: 170
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IMAPJobSubscriptionSettings : JobSubscriptionSettingsBase
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00028372 File Offset: 0x00026572
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0002837A File Offset: 0x0002657A
		public string[] ExcludedFolders { get; private set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00028383 File Offset: 0x00026583
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002838A File Offset: 0x0002658A
		public override void WriteToBatch(MigrationBatch batch)
		{
			batch.ExcludedFolders = this.ExcludedFolders;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002839D File Offset: 0x0002659D
		public override void WriteExtendedProperties(PersistableDictionary dictionary)
		{
			dictionary.SetMultiValuedProperty("ExcludedFolders", this.ExcludedFolders);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000283B8 File Offset: 0x000265B8
		public override bool ReadExtendedProperties(PersistableDictionary dictionary)
		{
			MultiValuedProperty<string> multiValuedStringProperty = dictionary.GetMultiValuedStringProperty("ExcludedFolders");
			if (multiValuedStringProperty != null)
			{
				this.ExcludedFolders = multiValuedStringProperty.ToArray();
				return true;
			}
			return false;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000283E3 File Offset: 0x000265E3
		protected override void InitalizeFromBatch(MigrationBatch batch)
		{
			this.ExcludedFolders = ((batch.ExcludedFolders == null) ? null : batch.ExcludedFolders.ToArray());
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028401 File Offset: 0x00026601
		protected override void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument)
		{
			parent.Add(new XElement("ExcludedFolders", string.Join(",", this.ExcludedFolders ?? new string[0])));
		}
	}
}
