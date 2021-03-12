using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000040 RID: 64
	public static class Antispam
	{
		// Token: 0x0600197F RID: 6527 RVA: 0x00050828 File Offset: 0x0004EA28
		public static void GetConnectionFilterPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			List<IPAddressEntry> list = new List<IPAddressEntry>();
			List<IPAddressEntry> list2 = new List<IPAddressEntry>();
			if (!DDIHelper.IsEmptyValue(dataRow["IPAllowList"]))
			{
				foreach (IPRange range in ((MultiValuedProperty<IPRange>)dataRow["IPAllowList"]))
				{
					list.Add(new IPAddressEntry(range));
				}
			}
			if (!DDIHelper.IsEmptyValue(dataRow["IPBlockList"]))
			{
				foreach (IPRange range2 in ((MultiValuedProperty<IPRange>)dataRow["IPBlockList"]))
				{
					list2.Add(new IPAddressEntry(range2));
				}
			}
			dataRow["IPAllowList"] = list;
			dataRow["IPBlockList"] = list2;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000509A4 File Offset: 0x0004EBA4
		public static void GetContentFilterPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow row = dataTable.Rows[0];
			Antispam.contentEmailListParameters.ForEach(delegate(string p)
			{
				if (!row[p].IsNullValue())
				{
					MultiValuedProperty<SmtpAddress> multiValuedProperty = (MultiValuedProperty<SmtpAddress>)row[p];
					row[string.Format("str{0}", p)] = multiValuedProperty.ToStringArray<SmtpAddress>().StringArrayJoin("; ");
				}
			});
			List<Identity> list = new List<Identity>();
			List<Identity> list2 = new List<Identity>();
			LanguageList languageList = new LanguageList();
			if (!row["RegionBlockList"].IsNullValue())
			{
				foreach (string text in ((MultiValuedProperty<string>)row["RegionBlockList"]))
				{
					list.Add(new Identity(text.ToString(), Antispam.GetRegionDisplayName(text)));
				}
				row["regionList"] = list.ToArray();
			}
			if (!row["LanguageBlockList"].IsNullValue())
			{
				foreach (string text2 in ((MultiValuedProperty<string>)row["LanguageBlockList"]))
				{
					list2.Add(new Identity(text2.ToString(), RtlUtil.ConvertToDecodedBidiString(languageList.GetDisplayValue(text2), RtlUtil.IsRtl)));
				}
				row["languageList"] = list2.ToArray();
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00050B98 File Offset: 0x0004ED98
		public static void GetOutboundSpamFilterPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow row = dataTable.Rows[0];
			Antispam.outboundEmailListParameters.ForEach(delegate(string p)
			{
				if (!row[p].IsNullValue())
				{
					MultiValuedProperty<SmtpAddress> multiValuedProperty = (MultiValuedProperty<SmtpAddress>)row[p];
					row[string.Format("str{0}", p)] = multiValuedProperty.ToStringArray<SmtpAddress>().StringArrayJoin("; ");
				}
			});
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00050BE4 File Offset: 0x0004EDE4
		public static void GetConnectionFilterSDOPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			List<IPAddressEntry> list = new List<IPAddressEntry>();
			List<IPAddressEntry> list2 = new List<IPAddressEntry>();
			if (!DDIHelper.IsEmptyValue(dataRow["IPAllowList"]))
			{
				foreach (IPRange range in ((MultiValuedProperty<IPRange>)dataRow["IPAllowList"]))
				{
					list.Add(new IPAddressEntry(range));
				}
			}
			if (!DDIHelper.IsEmptyValue(dataRow["IPBlockList"]))
			{
				foreach (IPRange range2 in ((MultiValuedProperty<IPRange>)dataRow["IPBlockList"]))
				{
					list2.Add(new IPAddressEntry(range2));
				}
			}
			dataRow["IPAllowList"] = list;
			dataRow["IPBlockList"] = list2;
			if (list.Count > 0)
			{
				dataRow["AllowListStatus"] = Strings.ASConfigured.ToString();
			}
			else
			{
				dataRow["AllowListStatus"] = Strings.ASNotConfigured.ToString();
			}
			if (list2.Count > 0)
			{
				dataRow["BlockListStatus"] = Strings.ASConfigured.ToString();
				return;
			}
			dataRow["BlockListStatus"] = Strings.ASNotConfigured.ToString();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00050DF0 File Offset: 0x0004EFF0
		public static void GetContentFilterSDOPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow row = dataTable.Rows[0];
			Antispam.contentEmailListParameters.ForEach(delegate(string p)
			{
				if (!row[p].IsNullValue())
				{
					MultiValuedProperty<SmtpAddress> multiValuedProperty3 = (MultiValuedProperty<SmtpAddress>)row[p];
					row[string.Format("str{0}", p)] = multiValuedProperty3.ToStringArray<SmtpAddress>().StringArrayJoin("; ");
				}
			});
			row["FalsePositiveStatus"] = ((!string.IsNullOrEmpty(row["strFalsePositiveAdditionalRecipients"].ToString())) ? Strings.ASConfigured.ToString() : Strings.ASNotConfigured.ToString());
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (Antispam.ParsedASFEIsOn(row["IncreaseScoreWithImageLinks"].ToString()))
			{
				multiValuedProperty.Add(Strings.IncreaseScoreWithImageLinksSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["IncreaseScoreWithNumericIps"].ToString()))
			{
				multiValuedProperty.Add(Strings.IncreaseScoreWithNumericIPsSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["IncreaseScoreWithRedirectToOtherPort"].ToString()))
			{
				multiValuedProperty.Add(Strings.IncreaseScoreWithRedirectToOtherPortSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["IncreaseScoreWithBizOrInfoUrls"].ToString()))
			{
				multiValuedProperty.Add(Strings.IncreaseScoreWithBizContentFilteringSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamEmptyMessages"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamEmptyMessagesSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamJavaScriptInHtml"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamJavaScriptInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamFramesInHtml"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamFramesInhtmlSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamObjectTagsInHtml"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamObjectTagsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamEmbedTagsInHtml"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamEmbedTagsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamFormTagsInHtml"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamFormTagsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamWebBugsInHtml"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamWebBugsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamSensitiveWordList"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamSensitiveWordListSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamSpfRecordHardFail"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamSpfRecordHardFailSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamFromAddressAuthFail"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamFromAddressAuthFailSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamNdrBackscatter"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamNdrBackscatterSDO);
			}
			if (Antispam.ParsedASFEIsOn(row["MarkAsSpamBulkMail"].ToString()))
			{
				multiValuedProperty.Add(Strings.MarkAsSpamBulkMailSDO);
			}
			if (multiValuedProperty.Count == 0)
			{
				multiValuedProperty.Add(Strings.ASNone);
			}
			row["ASFOnOutput"] = multiValuedProperty;
			MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
			if (Antispam.ParsedASFEIsTest(row["IncreaseScoreWithImageLinks"].ToString()))
			{
				multiValuedProperty2.Add(Strings.IncreaseScoreWithImageLinksSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["IncreaseScoreWithNumericIps"].ToString()))
			{
				multiValuedProperty2.Add(Strings.IncreaseScoreWithNumericIPsSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["IncreaseScoreWithRedirectToOtherPort"].ToString()))
			{
				multiValuedProperty2.Add(Strings.IncreaseScoreWithRedirectToOtherPortSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["IncreaseScoreWithBizOrInfoUrls"].ToString()))
			{
				multiValuedProperty2.Add(Strings.IncreaseScoreWithBizContentFilteringSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamEmptyMessages"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamEmptyMessagesSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamJavaScriptInHtml"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamJavaScriptInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamFramesInHtml"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamFramesInhtmlSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamObjectTagsInHtml"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamObjectTagsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamEmbedTagsInHtml"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamEmbedTagsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamFormTagsInHtml"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamFormTagsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamWebBugsInHtml"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamWebBugsInHtmlSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamSensitiveWordList"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamSensitiveWordListSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamSpfRecordHardFail"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamSpfRecordHardFailSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamFromAddressAuthFail"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamFromAddressAuthFailSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamNdrBackscatter"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamNdrBackscatterSDO);
			}
			if (Antispam.ParsedASFEIsTest(row["MarkAsSpamBulkMail"].ToString()))
			{
				multiValuedProperty2.Add(Strings.MarkAsSpamBulkMailSDO);
			}
			if (multiValuedProperty2.Count == 0)
			{
				multiValuedProperty2.Add(Strings.ASNone);
			}
			row["ASFTestOutput"] = multiValuedProperty2;
			string value = string.Empty;
			switch ((SpamFilteringTestModeAction)Enum.Parse(typeof(SpamFilteringTestModeAction), row["TestModeAction"].ToString(), true))
			{
			case SpamFilteringTestModeAction.None:
				value = Strings.ASNone;
				break;
			case SpamFilteringTestModeAction.AddXHeader:
				value = Strings.AddTestXHeaderSDOLabel;
				break;
			case SpamFilteringTestModeAction.BccMessage:
				value = Strings.BccMessageSDOLabel;
				break;
			}
			row["TestModeDisplay"] = value;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00051518 File Offset: 0x0004F718
		public static void PostGetForSDOActionRule(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (!DDIHelper.IsEmptyValue(dataRow["Description"]))
			{
				RuleDescription ruleDescription = (RuleDescription)dataRow["Description"];
				dataRow["RuleDescriptionIf"] = ruleDescription.RuleDescriptionIf;
				dataRow["RuleDescriptionTakeActions"] = ruleDescription.RuleDescriptionTakeActions;
				dataRow["RuleDescriptionExceptIf"] = ruleDescription.RuleDescriptionExceptIf;
				dataRow["ConditionDescriptions"] = ruleDescription.ConditionDescriptions.ToArray();
				dataRow["ActionDescriptions"] = ruleDescription.ActionDescriptions.ToArray();
				dataRow["ExceptionDescriptions"] = ruleDescription.ExceptionDescriptions.ToArray();
			}
			dataRow["CanToggleESN"] = (!DBNull.Value.Equals(dataRow["RecipientDomainIs"]) && DBNull.Value.Equals(dataRow["SentTo"]) && DBNull.Value.Equals(dataRow["SentToMemberOf"]) && DBNull.Value.Equals(dataRow["ExceptIfSentTo"]) && DBNull.Value.Equals(dataRow["ExceptIfSentToMemberOf"]) && DBNull.Value.Equals(dataRow["ExceptIfRecipientDomainIs"]));
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00051678 File Offset: 0x0004F878
		public static void PostGetListWorkflow(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Identity"] = new Identity(Guid.Empty.ToString(), "Default");
			dataRow["RuleName"] = "Default";
			dataRow["State"] = "Enabled";
			dataRow["Priority"] = int.MaxValue;
			dataTable.Rows.Add(dataRow);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00051758 File Offset: 0x0004F958
		public static void GetOutboundSpamFilterSDOPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow row = dataTable.Rows[0];
			Antispam.outboundEmailListParameters.ForEach(delegate(string p)
			{
				if (!row[p].IsNullValue())
				{
					MultiValuedProperty<SmtpAddress> multiValuedProperty = (MultiValuedProperty<SmtpAddress>)row[p];
					row[string.Format("str{0}", p)] = multiValuedProperty.ToStringArray<SmtpAddress>().StringArrayJoin("; ");
				}
			});
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00051854 File Offset: 0x0004FA54
		public static void SetContentFilterPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			List<string> modifiedColumns = new List<string>();
			Antispam.contentEmailListParameters.ForEach(delegate(string p)
			{
				string text = string.Format("str{0}", p);
				if (!DBNull.Value.Equals(row[text]))
				{
					string[] array = row[text].ToString().Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries);
					MultiValuedProperty<SmtpAddress> multiValuedProperty3 = new MultiValuedProperty<SmtpAddress>();
					foreach (string text2 in array)
					{
						multiValuedProperty3.Add(new SmtpAddress(text2.Trim()));
					}
					inputRow[text] = multiValuedProperty3;
					modifiedColumns.Add(text);
				}
			});
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (!row["regionList"].IsNullValue())
			{
				foreach (object obj in ((Array)row["regionList"]))
				{
					Identity identity = (Identity)obj;
					multiValuedProperty.Add(identity.RawIdentity);
				}
				if (multiValuedProperty.Count > 0)
				{
					inputRow["RegionBlockList"] = multiValuedProperty;
				}
				else
				{
					inputRow["RegionBlockList"] = null;
				}
			}
			else
			{
				inputRow["RegionBlockList"] = null;
			}
			modifiedColumns.Add("RegionBlockList");
			MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
			if (!row["languageList"].IsNullValue())
			{
				foreach (object obj2 in ((Array)row["languageList"]))
				{
					Identity identity2 = (Identity)obj2;
					multiValuedProperty2.Add(identity2.RawIdentity);
				}
				if (multiValuedProperty2.Count > 0)
				{
					inputRow["LanguageBlockList"] = multiValuedProperty2;
				}
				else
				{
					inputRow["LanguageBlockList"] = null;
				}
			}
			else
			{
				inputRow["LanguageBlockList"] = null;
			}
			modifiedColumns.Add("LanguageBlockList");
			if (!DBNull.Value.Equals(row["SentTo"]) || !DBNull.Value.Equals(row["SentToMemberOf"]) || !DBNull.Value.Equals(row["ExceptIfSentTo"]) || !DBNull.Value.Equals(row["ExceptIfSentToMemberOf"]) || !DBNull.Value.Equals(row["ExceptIfRecipientDomainIs"]))
			{
				inputRow["EnableEndUserSpamNotifications"] = false;
				modifiedColumns.Add("EnableEndUserSpamNotifications");
			}
			if (modifiedColumns.Count > 0)
			{
				store.SetModifiedColumns(modifiedColumns);
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00051BE4 File Offset: 0x0004FDE4
		public static void SetOutboundSpamFilterPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow row = dataTable.Rows[0];
			List<string> modifiedColumns = new List<string>();
			Antispam.outboundEmailListParameters.ForEach(delegate(string p)
			{
				string text = string.Format("str{0}", p);
				if (!DBNull.Value.Equals(row[text]))
				{
					string[] array = row[text].ToString().Split(new char[]
					{
						';'
					}, StringSplitOptions.RemoveEmptyEntries);
					MultiValuedProperty<SmtpAddress> multiValuedProperty = new MultiValuedProperty<SmtpAddress>();
					foreach (string text2 in array)
					{
						multiValuedProperty.Add(new SmtpAddress(text2.Trim()));
					}
					inputRow[text] = multiValuedProperty;
					modifiedColumns.Add(text);
				}
			});
			if (modifiedColumns.Count > 0)
			{
				store.SetModifiedColumns(modifiedColumns);
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00051C4B File Offset: 0x0004FE4B
		private static bool ParsedASFEIsOn(string asfSetting)
		{
			return (SpamFilteringOption)Enum.Parse(typeof(SpamFilteringOption), asfSetting) == SpamFilteringOption.On;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00051C65 File Offset: 0x0004FE65
		private static bool ParsedASFEIsTest(string asfSetting)
		{
			return (SpamFilteringOption)Enum.Parse(typeof(SpamFilteringOption), asfSetting) == SpamFilteringOption.Test;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00051CF0 File Offset: 0x0004FEF0
		public static void BuildRegionList(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			Array.ForEach<string>(HygieneUtils.iso3166Alpha2Codes, delegate(string code)
			{
				string text = code.ToUpper().ToString();
				string regionDisplayName = Antispam.GetRegionDisplayName(text);
				DataRow dataRow = dataTable.NewRow();
				dataRow["RegionCode"] = text;
				dataRow["RegionName"] = regionDisplayName;
				dataRow["Identity"] = new Identity(text, regionDisplayName);
				dataTable.Rows.Add(dataRow);
			});
			dataTable.EndLoadData();
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00051E04 File Offset: 0x00050004
		public static void BuildLanguageList(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			LanguageList lang = new LanguageList();
			dataTable.BeginLoadData();
			Array.ForEach<string>(HygieneUtils.antispamFilterableLanguages, delegate(string code)
			{
				string text = code.ToUpper().ToString();
				DataRow dataRow = dataTable.NewRow();
				try
				{
					dataRow["LanguageCode"] = text;
					dataRow["LanguageName"] = RtlUtil.ConvertToDecodedBidiString(lang.GetDisplayValue(code), RtlUtil.IsRtl);
					dataRow["Identity"] = new Identity(text, dataRow["LanguageName"].ToString());
				}
				catch
				{
					dataRow["LanguageCode"] = text;
					dataRow["LanguageName"] = text;
					dataRow["Identity"] = new Identity(text, text);
				}
				dataTable.Rows.Add(dataRow);
			});
			dataTable.EndLoadData();
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00051E58 File Offset: 0x00050058
		private static string GetRegionDisplayName(string countryCode)
		{
			string result;
			try
			{
				CountryInfo countryInfo = CountryInfo.Parse(countryCode);
				result = RtlUtil.ConvertToDecodedBidiString(Strings.GetLocalizedString((Strings.IDs)Enum.Parse(typeof(Strings.IDs), countryInfo.UniqueId)), RtlUtil.IsRtl);
			}
			catch
			{
				try
				{
					result = RtlUtil.ConvertToDecodedBidiString(Strings.GetLocalizedString((Strings.IDs)Enum.Parse(typeof(Strings.IDs), string.Format("Region_{0}", countryCode))), RtlUtil.IsRtl);
				}
				catch
				{
					result = countryCode;
				}
			}
			return result;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00051EF8 File Offset: 0x000500F8
		public static bool IsDefaultPolicyIdentity(object identity)
		{
			return identity is Identity && string.Compare(((Identity)identity).RawIdentity, Guid.Empty.ToString(), true) == 0;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00051F36 File Offset: 0x00050136
		public static PeopleIdentity[] ConvertToPeopleIdentity(object identity)
		{
			if (identity is RecipientIdParameter[])
			{
				return Identity.ConvertToPeopleIdentity((RecipientIdParameter[])identity);
			}
			return null;
		}

		// Token: 0x04001AC1 RID: 6849
		private static readonly List<string> emailListParameters = new List<string>
		{
			"FalsePositiveAdditionalRecipients",
			"RedirectToRecipients",
			"TestModeBccToRecipients",
			"BccSuspiciousOutboundAdditionalRecipients",
			"NotifyOutboundSpamRecipients"
		};

		// Token: 0x04001AC2 RID: 6850
		private static readonly List<string> contentEmailListParameters = new List<string>
		{
			"FalsePositiveAdditionalRecipients",
			"RedirectToRecipients",
			"TestModeBccToRecipients"
		};

		// Token: 0x04001AC3 RID: 6851
		private static readonly List<string> outboundEmailListParameters = new List<string>
		{
			"BccSuspiciousOutboundAdditionalRecipients",
			"NotifyOutboundSpamRecipients"
		};
	}
}
