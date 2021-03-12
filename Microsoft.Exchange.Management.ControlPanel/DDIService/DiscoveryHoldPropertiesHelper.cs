using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020003BB RID: 955
	public static class DiscoveryHoldPropertiesHelper
	{
		// Token: 0x060031F0 RID: 12784 RVA: 0x0009A4BC File Offset: 0x000986BC
		public static void GetObjectForNewPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			dataRow["StartDate"] = ExDateTime.Now.ToUserDateTimeGeneralFormatString();
			dataRow["EndDate"] = ExDateTime.Now.AddDays(1.0).ToUserDateTimeGeneralFormatString();
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0009A520 File Offset: 0x00098720
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["StartDate"] != DBNull.Value)
			{
				ExDateTime? exDateTime = (ExDateTime?)dataRow["StartDate"];
				dataRow["StartDateEnabled"] = (exDateTime != null);
				dataRow["SearchStartDate"] = ((exDateTime != null) ? exDateTime.Value.ToUserDateTimeGeneralFormatString() : ExDateTime.Now.ToUserDateTimeGeneralFormatString());
			}
			else
			{
				dataRow["StartDateEnabled"] = false;
				dataRow["SearchStartDate"] = ExDateTime.Now.ToUserDateTimeGeneralFormatString();
			}
			if (dataRow["EndDate"] != DBNull.Value)
			{
				ExDateTime? exDateTime2 = (ExDateTime?)dataRow["EndDate"];
				dataRow["EndDateEnabled"] = (exDateTime2 != null);
				dataRow["SearchEndDate"] = ((exDateTime2 != null) ? exDateTime2.Value.ToUserDateTimeGeneralFormatString() : ExDateTime.Now.AddDays(1.0).ToUserExDateTime().ToUserDateTimeGeneralFormatString());
			}
			else
			{
				dataRow["EndDateEnabled"] = false;
				dataRow["SearchEndDate"] = ExDateTime.Now.AddDays(1.0).ToUserExDateTime().Date.ToUserDateTimeGeneralFormatString();
			}
			if (dataRow["ItemHoldPeriod"] != DBNull.Value)
			{
				Unlimited<EnhancedTimeSpan> unlimited = (Unlimited<EnhancedTimeSpan>)dataRow["ItemHoldPeriod"];
				dataRow["HoldIndefinitely"] = unlimited.IsUnlimited;
				dataRow["ItemHoldPeriodDays"] = (unlimited.IsUnlimited ? null : unlimited.Value.Days.ToString());
			}
			DiscoveryHoldPropertiesHelper.GetValuesForListRow(dataRow);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x0009A70C File Offset: 0x0009890C
		public static void NewObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			if (!dataRow["SearchAllMailboxes"].IsNullValue() && dataRow["SearchAllMailboxes"].IsTrue())
			{
				dataRow["SourceMailboxes"] = null;
				dataRow["AllSourceMailboxes"] = true;
			}
			if (!dataRow["SearchAllPublicFolders"].IsNullValue() && dataRow["SearchAllPublicFolders"].IsTrue())
			{
				dataRow["PublicFolderSources"] = null;
				dataRow["AllPublicFolderSources"] = true;
			}
			if (!dataRow["HoldIndefinitely"].IsNullValue() && dataRow["HoldIndefinitely"].IsTrue())
			{
				dataRow["ItemHoldPeriod"] = Unlimited<EnhancedTimeSpan>.UnlimitedValue;
				list.Add("ItemHoldPeriod");
			}
			if (!dataRow["SearchContent"].IsNullValue())
			{
				bool flag = (bool)dataRow["SearchContent"];
				if (flag)
				{
					if (!string.IsNullOrEmpty((string)dataRow["SenderList"]))
					{
						dataRow["Senders"] = ((string)dataRow["SenderList"]).ToArrayOfStrings();
						list.Add("Senders");
					}
					if (!string.IsNullOrEmpty((string)dataRow["RecipientList"]))
					{
						dataRow["Recipients"] = ((string)dataRow["RecipientList"]).ToArrayOfStrings();
						list.Add("Recipients");
					}
					if (!string.IsNullOrEmpty((string)dataRow["MessageTypeList"]))
					{
						dataRow["MessageTypes"] = ((string)dataRow["MessageTypeList"]).ToArrayOfStrings();
						list.Add("MessageTypes");
					}
				}
			}
			store.SetModifiedColumns(list);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x0009A8FC File Offset: 0x00098AFC
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			if (!dataRow["SearchAllMailboxes"].IsNullValue() && dataRow["SearchAllMailboxes"].IsTrue())
			{
				dataRow["SourceMailboxes"] = null;
				list.Add("SourceMailboxes");
			}
			if (!dataRow["SearchAllPublicFolders"].IsNullValue() && dataRow["SearchAllPublicFolders"].IsTrue())
			{
				dataRow["PublicFolderSources"] = null;
				list.Add("PublicFolderSources");
				dataRow["AllPublicFolderSources"] = true;
				list.Add("AllPublicFolderSources");
			}
			if (!dataRow["HoldIndefinitely"].IsNullValue() && dataRow["HoldIndefinitely"].IsTrue())
			{
				dataRow["ItemHoldPeriod"] = Unlimited<EnhancedTimeSpan>.UnlimitedValue;
				list.Add("ItemHoldPeriod");
			}
			else if (!dataRow["ItemHoldPeriodDays"].IsNullValue())
			{
				dataRow["ItemHoldPeriod"] = Unlimited<EnhancedTimeSpan>.Parse((string)dataRow["ItemHoldPeriodDays"]);
				list.Add("ItemHoldPeriod");
			}
			if (!dataRow["StartDateEnabled"].IsNullValue())
			{
				if (dataRow["StartDateEnabled"].IsTrue())
				{
					DiscoveryHoldPropertiesHelper.SetDate(dataRow, list, "StartDate", (string)dataRow["SearchStartDate"]);
				}
				else
				{
					dataRow["StartDate"] = null;
					list.Add("StartDate");
				}
			}
			else if (!dataRow["SearchStartDate"].IsNullValue())
			{
				DiscoveryHoldPropertiesHelper.SetDate(dataRow, list, "StartDate", (string)dataRow["SearchStartDate"]);
			}
			if (!dataRow["EndDateEnabled"].IsNullValue())
			{
				if (dataRow["EndDateEnabled"].IsTrue())
				{
					DiscoveryHoldPropertiesHelper.SetDate(dataRow, list, "EndDate", (string)dataRow["SearchEndDate"]);
				}
				else
				{
					dataRow["EndDate"] = null;
					list.Add("EndDate");
				}
			}
			else if (!dataRow["SearchEndDate"].IsNullValue())
			{
				DiscoveryHoldPropertiesHelper.SetDate(dataRow, list, "EndDate", (string)dataRow["SearchEndDate"]);
			}
			DiscoveryHoldPropertiesHelper.SetChangedListProperty(dataRow, list, "SenderList", "Senders");
			DiscoveryHoldPropertiesHelper.SetChangedListProperty(dataRow, list, "RecipientList", "Recipients");
			DiscoveryHoldPropertiesHelper.SetChangedListProperty(dataRow, list, "MessageTypeList", "MessageTypes");
			if (!dataRow["SearchContent"].IsNullValue() && dataRow["SearchContent"].IsFalse())
			{
				dataRow["SearchQuery"] = DBNull.Value;
				list.Add("SearchQuery");
				dataRow["Senders"] = DBNull.Value;
				list.Add("Senders");
				dataRow["Recipients"] = DBNull.Value;
				list.Add("Recipients");
				dataRow["MessageTypes"] = DBNull.Value;
				list.Add("MessageTypes");
				dataRow["StartDate"] = DBNull.Value;
				list.Add("StartDate");
				dataRow["EndDate"] = DBNull.Value;
				list.Add("EndDate");
			}
			if (RbacPrincipal.Current.IsInRole("Set-MailboxSearch?EstimateOnly&ExcludeDuplicateMessages&LogLevel"))
			{
				dataRow["EstimateOnly"] = true;
				list.Add("EstimateOnly");
				dataRow["ExcludeDuplicateMessages"] = false;
				list.Add("ExcludeDuplicateMessages");
				dataRow["LogLevel"] = LoggingLevel.Suppress;
				list.Add("LogLevel");
			}
			store.SetModifiedColumns(list);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x0009ACC4 File Offset: 0x00098EC4
		public static void GetObjectForCopySearchPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			dataRow["EnableFullLogging"] = ((LoggingLevel)dataRow["LogLevel"] == LoggingLevel.Full);
			dataRow["SendMeEmailOnComplete"] = ((MultiValuedProperty<ADObjectId>)dataRow["StatusMailRecipients"]).Contains(RbacPrincipal.Current.ExecutingUserId);
			DiscoveryHoldPropertiesHelper.GetValuesForListRow(dataRow);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x0009AD44 File Offset: 0x00098F44
		public static void SetObjectForCopySearchPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			dataRow["EstimateOnly"] = false;
			list.Add("EstimateOnly");
			if (!dataRow["EnableFullLogging"].IsNullValue())
			{
				if (dataRow["EnableFullLogging"].IsTrue())
				{
					dataRow["LogLevel"] = LoggingLevel.Full;
				}
				else
				{
					dataRow["LogLevel"] = LoggingLevel.Basic;
				}
				list.Add("LogLevel");
			}
			if (!dataRow["LogLevel"].IsNullValue() && (LoggingLevel)dataRow["LogLevel"] == LoggingLevel.Suppress)
			{
				dataRow["LogLevel"] = LoggingLevel.Basic;
				list.Add("LogLevel");
			}
			if (!dataRow["SendMeEmailOnComplete"].IsNullValue())
			{
				if (dataRow["SendMeEmailOnComplete"].IsTrue())
				{
					dataRow["StatusMailRecipients"] = RbacPrincipal.Current.ExecutingUserId;
				}
				else
				{
					dataRow["StatusMailRecipients"] = null;
				}
				list.Add("StatusMailRecipients");
			}
			store.SetModifiedColumns(list);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x0009AE7A File Offset: 0x0009907A
		public static Identity GetIdentity(object identity, object name)
		{
			return new Identity(identity as string, name as string);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x0009AE90 File Offset: 0x00099090
		public static string GetCommaSeparatedUserList(object userObjects)
		{
			string result = string.Empty;
			MultiValuedProperty<string> multiValuedProperty = userObjects as MultiValuedProperty<string>;
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				result = string.Join(", ", multiValuedProperty.ToArray<string>());
			}
			return result;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x0009AED8 File Offset: 0x000990D8
		public static string GetCommaSeparatedMessageTypeList(object messageTypeObjects)
		{
			string result = "[]";
			MultiValuedProperty<KindKeyword> multiValuedProperty = messageTypeObjects as MultiValuedProperty<KindKeyword>;
			if (multiValuedProperty != null)
			{
				result = (from messageType in multiValuedProperty
				select messageType.ToString()).ToArray<string>().ToJsonString(null);
			}
			return result;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x0009AF28 File Offset: 0x00099128
		public static Identity GetTargetMailboxIdentity(object targetMailboxIdObject)
		{
			ADObjectId adobjectId = targetMailboxIdObject as ADObjectId;
			Identity result = null;
			if (adobjectId != null)
			{
				RecipientObjectResolverRow recipientObjectResolverRow = RecipientObjectResolver.Instance.ResolveObjects(new ADObjectId[]
				{
					adobjectId
				}).FirstOrDefault<RecipientObjectResolverRow>();
				if (recipientObjectResolverRow == null)
				{
					result = adobjectId.ToIdentity();
				}
				else
				{
					result = recipientObjectResolverRow.Identity;
				}
			}
			return result;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x0009AF74 File Offset: 0x00099174
		public static object GetPublicFolderId(object publicFolderObject)
		{
			PublicFolderIdParameter publicFolderIdParameter = publicFolderObject as PublicFolderIdParameter;
			if (publicFolderIdParameter != null)
			{
				return publicFolderIdParameter.ToString();
			}
			return publicFolderObject;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x0009AF94 File Offset: 0x00099194
		public static ExDateTime? GetUtcExDateTime(string dateString)
		{
			DateTime dateTime;
			if (DateTime.TryParse(dateString, out dateTime))
			{
				ExDateTime? exDateTime = dateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture).ToEcpExDateTime();
				if (exDateTime != null)
				{
					return new ExDateTime?(exDateTime.Value.ToUtc());
				}
			}
			return null;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x0009AFE9 File Offset: 0x000991E9
		public static bool IsObjectVersionOriginal(SearchObjectVersion version)
		{
			return version == SearchObjectVersion.Original;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x0009AFF0 File Offset: 0x000991F0
		internal static void GetValuesForListRow(DataRow row)
		{
			ExDateTime? exDateTimeValue = row["LastModifiedTime"].IsNullValue() ? null : ((ExDateTime?)row["LastModifiedTime"]);
			SearchState searchState = (SearchState)row["Status"];
			bool flag = (bool)row["EstimateOnly"];
			row["HoldStatusDescription"] = DiscoveryHoldPropertiesHelper.GetHoldStatusDescription((bool)row["InPlaceHoldEnabled"]);
			row["LastModifiedTimeDisplay"] = exDateTimeValue.ToUserDateTimeString();
			row["LastModifiedUTCDateTime"] = ((exDateTimeValue != null) ? exDateTimeValue.Value.UniversalTime : DateTime.MinValue);
			row["CreatedByDisplayName"] = DiscoveryHoldPropertiesHelper.GetCreatedByUserDisplayName((string)row["CreatedBy"]);
			row["IsEstimateOnly"] = flag;
			row["IsStartable"] = DiscoveryHoldPropertiesHelper.IsStartable(flag, searchState);
			row["IsPreviewable"] = (searchState != SearchState.EstimateFailed && searchState != SearchState.Failed);
			row["IsStoppable"] = (searchState == SearchState.InProgress || SearchState.EstimateInProgress == searchState);
			row["IsResumable"] = (SearchState.PartiallySucceeded == searchState);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x0009B142 File Offset: 0x00099342
		internal static string GetHoldStatusDescription(bool inPlaceHoldEnabled)
		{
			if (RbacPrincipal.Current.IsInRole("LegalHold"))
			{
				return inPlaceHoldEnabled ? Strings.DiscoveryHoldHoldStatusYes : Strings.DiscoveryHoldHoldStatusNo;
			}
			return string.Empty;
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x0009B170 File Offset: 0x00099370
		internal static string GetCreatedByUserDisplayName(string rawName)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(rawName))
			{
				result = rawName;
				int num = rawName.IndexOf("\\");
				if (num > -1 && num < rawName.Length - 1)
				{
					result = rawName.Substring(num + 1);
				}
				else
				{
					try
					{
						SecurityIdentifier sid = new SecurityIdentifier(rawName);
						SecurityPrincipalIdParameter securityPrincipalIdParameter = new SecurityPrincipalIdParameter(sid);
						IEnumerable<SecurityPrincipalIdParameter> sidPrincipalId = new SecurityPrincipalIdParameter[]
						{
							securityPrincipalIdParameter
						}.AsEnumerable<SecurityPrincipalIdParameter>();
						List<AcePermissionRecipientRow> list = RecipientObjectResolver.Instance.ResolveSecurityPrincipalId(sidPrincipalId).ToList<AcePermissionRecipientRow>();
						if (list.Count > 0)
						{
							result = list[0].DisplayName;
						}
					}
					catch (ArgumentException ex)
					{
						DDIHelper.Trace("Created by value is not a valid SID: " + ex.Message);
					}
				}
			}
			return result;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x0009B234 File Offset: 0x00099434
		internal static bool IsStartable(bool estimateOnly, SearchState searchStatus)
		{
			if (estimateOnly)
			{
				return SearchState.EstimateInProgress != searchStatus && SearchState.EstimateStopping != searchStatus;
			}
			return searchStatus != SearchState.InProgress && SearchState.Stopping != searchStatus;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x0009B254 File Offset: 0x00099454
		private static void SetDate(DataRow row, List<string> modifiedColumns, string propertyName, string dateString)
		{
			ExDateTime? utcExDateTime = DiscoveryHoldPropertiesHelper.GetUtcExDateTime(dateString);
			if (utcExDateTime != null)
			{
				row[propertyName] = utcExDateTime.Value.ToUtc();
				modifiedColumns.Add(propertyName);
				return;
			}
			DDIHelper.Trace(string.Format("{0} was not set because an expected date {1} resulted in null.", propertyName, dateString));
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x0009B2A8 File Offset: 0x000994A8
		private static void SetChangedListProperty(DataRow row, List<string> modifiedColumns, string displayColumnName, string taskColumnName)
		{
			if (!row[displayColumnName].IsNullValue())
			{
				if (!string.IsNullOrEmpty((string)row[displayColumnName]))
				{
					row[taskColumnName] = ((string)row[displayColumnName]).ToArrayOfStrings();
				}
				else
				{
					row[taskColumnName] = null;
				}
				modifiedColumns.Add(taskColumnName);
			}
		}

		// Token: 0x04002438 RID: 9272
		internal const string StartDateColumnName = "StartDate";

		// Token: 0x04002439 RID: 9273
		internal const string EndDateColumnName = "EndDate";

		// Token: 0x0400243A RID: 9274
		internal const string StartDateEnabledColumnName = "StartDateEnabled";

		// Token: 0x0400243B RID: 9275
		internal const string EndDateEnabledColumnName = "EndDateEnabled";

		// Token: 0x0400243C RID: 9276
		internal const string SearchStartDateColumnName = "SearchStartDate";

		// Token: 0x0400243D RID: 9277
		internal const string SearchEndDateColumnName = "SearchEndDate";

		// Token: 0x0400243E RID: 9278
		internal const string ItemHoldPeriodColumnName = "ItemHoldPeriod";

		// Token: 0x0400243F RID: 9279
		internal const string HoldIndefinitelyColumnName = "HoldIndefinitely";

		// Token: 0x04002440 RID: 9280
		internal const string ItemHoldPeriodDaysColumnName = "ItemHoldPeriodDays";

		// Token: 0x04002441 RID: 9281
		internal const string SearchContentColumnName = "SearchContent";

		// Token: 0x04002442 RID: 9282
		internal const string SearchAllMailboxesColumnName = "SearchAllMailboxes";

		// Token: 0x04002443 RID: 9283
		internal const string AllSourceMailboxesColumnName = "AllSourceMailboxes";

		// Token: 0x04002444 RID: 9284
		internal const string SourceMailboxesColumnName = "SourceMailboxes";

		// Token: 0x04002445 RID: 9285
		internal const string SearchAllPublicFoldersColumnName = "SearchAllPublicFolders";

		// Token: 0x04002446 RID: 9286
		internal const string AllPublicFolderSourcesColumnName = "AllPublicFolderSources";

		// Token: 0x04002447 RID: 9287
		internal const string PublicFolderSourcesColumnName = "PublicFolderSources";

		// Token: 0x04002448 RID: 9288
		internal const string SenderListColumnName = "SenderList";

		// Token: 0x04002449 RID: 9289
		internal const string SendersColumnName = "Senders";

		// Token: 0x0400244A RID: 9290
		internal const string RecipientListColumnName = "RecipientList";

		// Token: 0x0400244B RID: 9291
		internal const string RecipientsColumnName = "Recipients";

		// Token: 0x0400244C RID: 9292
		internal const string MessageTypeListColumnName = "MessageTypeList";

		// Token: 0x0400244D RID: 9293
		internal const string MessageTypesColumnName = "MessageTypes";

		// Token: 0x0400244E RID: 9294
		internal const string SearchQueryColumnName = "SearchQuery";

		// Token: 0x0400244F RID: 9295
		internal const string EstimateOnlyColumnName = "EstimateOnly";

		// Token: 0x04002450 RID: 9296
		internal const string ExcludeDuplicateMessagesColumnName = "ExcludeDuplicateMessages";

		// Token: 0x04002451 RID: 9297
		internal const string LogLevelColumnName = "LogLevel";

		// Token: 0x04002452 RID: 9298
		internal const string EnableFullLoggingColumnName = "EnableFullLogging";

		// Token: 0x04002453 RID: 9299
		internal const string SendMeEmailOnCompleteColumnName = "SendMeEmailOnComplete";

		// Token: 0x04002454 RID: 9300
		internal const string StatusMailRecipientsColumnName = "StatusMailRecipients";

		// Token: 0x04002455 RID: 9301
		internal const string LastModifiedTimeColumnName = "LastModifiedTime";

		// Token: 0x04002456 RID: 9302
		internal const string StatusColumnName = "Status";

		// Token: 0x04002457 RID: 9303
		internal const string HoldStatusDescriptionColumnName = "HoldStatusDescription";

		// Token: 0x04002458 RID: 9304
		internal const string InPlaceHoldEnabledColumnName = "InPlaceHoldEnabled";

		// Token: 0x04002459 RID: 9305
		internal const string LastModifiedTimeDisplayColumnName = "LastModifiedTimeDisplay";

		// Token: 0x0400245A RID: 9306
		internal const string LastModifiedUTCDateTimeColumnName = "LastModifiedUTCDateTime";

		// Token: 0x0400245B RID: 9307
		internal const string CreatedByDisplayNameColumnName = "CreatedByDisplayName";

		// Token: 0x0400245C RID: 9308
		internal const string CreatedByColumnName = "CreatedBy";

		// Token: 0x0400245D RID: 9309
		internal const string IsEstimateOnlyColumnName = "IsEstimateOnly";

		// Token: 0x0400245E RID: 9310
		internal const string IsStartableColumnName = "IsStartable";

		// Token: 0x0400245F RID: 9311
		internal const string IsPreviewableColumnName = "IsPreviewable";

		// Token: 0x04002460 RID: 9312
		internal const string IsStoppableColumnName = "IsStoppable";

		// Token: 0x04002461 RID: 9313
		internal const string IsResumableColumnName = "IsResumable";
	}
}
