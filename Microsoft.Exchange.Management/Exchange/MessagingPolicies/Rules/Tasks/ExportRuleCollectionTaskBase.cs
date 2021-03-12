using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000A17 RID: 2583
	public abstract class ExportRuleCollectionTaskBase : GetMultitenancySystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x001865A2 File Offset: 0x001847A2
		// (set) Token: 0x06005CA8 RID: 23720 RVA: 0x001865AA File Offset: 0x001847AA
		protected string RuleCollectionName
		{
			get
			{
				return this.ruleCollectionName;
			}
			set
			{
				this.ruleCollectionName = value;
			}
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x001865B4 File Offset: 0x001847B4
		protected override void InternalProcessRecord()
		{
			ADRuleStorageManager adruleStorageManager = this.RuleStorageManager;
			if (adruleStorageManager == null)
			{
				return;
			}
			using (Stream stream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					adruleStorageManager.LoadRuleCollectionWithoutParsing();
					IEnumerable<Rule> source = adruleStorageManager.WriteToStream(streamWriter, ExportRuleCollectionTaskBase.MaxLegacyFormatVersion, null);
					stream.Seek(0L, SeekOrigin.Begin);
					BinaryReader binaryReader = new BinaryReader(stream);
					BinaryFileDataObject dataObject = new BinaryFileDataObject
					{
						FileData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length)
					};
					this.WriteResult(dataObject);
					if (source.Any<Rule>())
					{
						this.WriteWarning(Strings.ExportSkippedE15Rules(source.Count<Rule>()));
					}
				}
			}
			try
			{
				adruleStorageManager.ParseRuleCollection();
			}
			catch (ParserException ex)
			{
				this.WriteWarning(Strings.CorruptRuleCollection(ex.Message));
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x001866A4 File Offset: 0x001848A4
		// (set) Token: 0x06005CAB RID: 23723 RVA: 0x00186700 File Offset: 0x00184900
		internal ADRuleStorageManager RuleStorageManager
		{
			get
			{
				if (this.ruleStorageManager == null)
				{
					try
					{
						return new ADRuleStorageManager(this.ruleCollectionName, base.DataSession);
					}
					catch (RuleCollectionNotInAdException)
					{
						this.WriteWarning(Strings.RuleCollectionNotFoundDuringExport(this.RuleCollectionName));
						return null;
					}
				}
				return this.ruleStorageManager;
			}
			set
			{
				this.ruleStorageManager = value;
			}
		}

		// Token: 0x04003471 RID: 13425
		private static Version MaxLegacyFormatVersion = new Version("14.00.0000.00");

		// Token: 0x04003472 RID: 13426
		private string ruleCollectionName;

		// Token: 0x04003473 RID: 13427
		private ADRuleStorageManager ruleStorageManager;
	}
}
