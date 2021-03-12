using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B56 RID: 2902
	[Cmdlet("Export", "TransportRuleCollection", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ExportTransportRuleCollection : ExportRuleCollectionTaskBase
	{
		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x06006941 RID: 26945 RVA: 0x001B21C8 File Offset: 0x001B03C8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportTransportRuleCollection;
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x001B21CF File Offset: 0x001B03CF
		public ExportTransportRuleCollection()
		{
			this.supportedPredicates = TransportRulePredicate.GetAvailablePredicateMappings();
			this.supportedActions = TransportRuleAction.GetAvailableActionMappings();
			base.RuleCollectionName = Utils.RuleCollectionNameFromRole();
			this.Format = RuleCollectionFormat.RuleCollectionXML;
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x06006943 RID: 26947 RVA: 0x001B21FF File Offset: 0x001B03FF
		// (set) Token: 0x06006944 RID: 26948 RVA: 0x001B2216 File Offset: 0x001B0416
		[Parameter(Mandatory = false)]
		public RuleCollectionFormat Format
		{
			get
			{
				return (RuleCollectionFormat)base.Fields["Format"];
			}
			set
			{
				base.Fields["Format"] = value;
			}
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x001B2230 File Offset: 0x001B0430
		protected override void InternalProcessRecord()
		{
			if (base.NeedSuppressingPiiData)
			{
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
			}
			switch (this.Format)
			{
			case RuleCollectionFormat.RuleCollectionXML:
				this.WriteFormattedRules();
				return;
			case RuleCollectionFormat.InternalXML:
				this.WriteRawRules();
				return;
			default:
				return;
			}
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x001B2274 File Offset: 0x001B0474
		private void WriteFormattedRules()
		{
			ADRuleStorageManager ruleStorageManager = base.RuleStorageManager;
			ruleStorageManager.LoadRuleCollection();
			IEnumerable<TransportRuleHandle> ruleHandles = ruleStorageManager.GetRuleHandles();
			List<Rule> list = new List<Rule>();
			foreach (TransportRuleHandle transportRuleHandle in ruleHandles)
			{
				string text = null;
				try
				{
					Rule rule = Rule.CreateFromInternalRule(this.supportedPredicates, this.supportedActions, transportRuleHandle.Rule, transportRuleHandle.AdRule.Priority, transportRuleHandle.AdRule);
					if (base.NeedSuppressingPiiData)
					{
						rule.SuppressPiiData(Utils.GetSessionPiiMap(base.ExchangeRunspaceConfig));
					}
					list.Add(rule);
				}
				catch (ArgumentException ex)
				{
					text = ex.Message;
				}
				catch (InvalidOperationException ex2)
				{
					text = ex2.Message;
				}
				catch (ParserException ex3)
				{
					text = ex3.Message;
				}
				catch (RulesValidationException ex4)
				{
					text = ex4.Message;
				}
				if (text != null)
				{
					base.WriteWarning(Strings.ErrorObjectHasValidationErrorsWithId(transportRuleHandle.AdRule.Identity.ToString()) + " " + text);
				}
			}
			this.WriteResult(new BinaryFileDataObject
			{
				FileData = PowershellTransportRuleSerializer.Serialize(list)
			});
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x001B23E0 File Offset: 0x001B05E0
		private void WriteRawRules()
		{
			ADRuleStorageManager ruleStorageManager = base.RuleStorageManager;
			using (Stream stream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					ruleStorageManager.LoadRuleCollectionWithoutParsing();
					ruleStorageManager.WriteRawRulesToStream(streamWriter);
					if (base.NeedSuppressingPiiData)
					{
						stream.Seek(0L, SeekOrigin.Begin);
						StreamReader streamReader = new StreamReader(stream);
						string value = SuppressingPiiData.Redact(streamReader.ReadToEnd());
						stream.SetLength(0L);
						streamWriter.Write(value);
						streamWriter.Flush();
					}
					stream.Seek(0L, SeekOrigin.Begin);
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						BinaryFileDataObject dataObject = new BinaryFileDataObject
						{
							FileData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length)
						};
						this.WriteResult(dataObject);
					}
				}
			}
		}

		// Token: 0x040036B7 RID: 14007
		private const string RuleCollectionFormatParameter = "Format";

		// Token: 0x040036B8 RID: 14008
		private readonly TypeMapping[] supportedPredicates;

		// Token: 0x040036B9 RID: 14009
		private readonly TypeMapping[] supportedActions;
	}
}
