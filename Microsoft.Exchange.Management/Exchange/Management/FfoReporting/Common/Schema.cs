using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003DA RID: 986
	internal static class Schema
	{
		// Token: 0x04001BD2 RID: 7122
		internal const int MinPageRange = 1;

		// Token: 0x04001BD3 RID: 7123
		internal const int MaxPageRange = 1000;

		// Token: 0x04001BD4 RID: 7124
		internal const int MinPageSizeRange = 1;

		// Token: 0x04001BD5 RID: 7125
		internal const int MaxPageSizeRange = 5000;

		// Token: 0x020003DB RID: 987
		[Flags]
		public enum Actions
		{
			// Token: 0x04001BD7 RID: 7127
			AddBccRecipient = 1,
			// Token: 0x04001BD8 RID: 7128
			AddCcRecipient = 2,
			// Token: 0x04001BD9 RID: 7129
			AddManagerAsRecipient = 4,
			// Token: 0x04001BDA RID: 7130
			AddToRecipient = 8,
			// Token: 0x04001BDB RID: 7131
			ApplyClassification = 16,
			// Token: 0x04001BDC RID: 7132
			ApplyHtmlDisclaimer = 32,
			// Token: 0x04001BDD RID: 7133
			DeleteMessage = 64,
			// Token: 0x04001BDE RID: 7134
			GenerateIncidentReport = 256,
			// Token: 0x04001BDF RID: 7135
			ModerateMessageByManager = 512,
			// Token: 0x04001BE0 RID: 7136
			ModerateMessageByUser = 1024,
			// Token: 0x04001BE1 RID: 7137
			NotifySender = 4096,
			// Token: 0x04001BE2 RID: 7138
			PrependSubject = 8192,
			// Token: 0x04001BE3 RID: 7139
			Quarantine = 16384,
			// Token: 0x04001BE4 RID: 7140
			RedirectMessage = 32768,
			// Token: 0x04001BE5 RID: 7141
			RejectMessage = 65536,
			// Token: 0x04001BE6 RID: 7142
			RemoveMessageHeader = 131072,
			// Token: 0x04001BE7 RID: 7143
			RequireTLS = 262144,
			// Token: 0x04001BE8 RID: 7144
			RightsProtectMessage = 524288,
			// Token: 0x04001BE9 RID: 7145
			RouteMessageUsingConnector = 1048576,
			// Token: 0x04001BEA RID: 7146
			SetAuditSeverityHigh = 2097152,
			// Token: 0x04001BEB RID: 7147
			SetAuditSeverityLow = 4194304,
			// Token: 0x04001BEC RID: 7148
			SetAuditSeverityMedium = 8388608,
			// Token: 0x04001BED RID: 7149
			SetMessageHeader = 16777216,
			// Token: 0x04001BEE RID: 7150
			SetSpamConfidenceLevel = 33554432,
			// Token: 0x04001BEF RID: 7151
			StopRuleProcessing = 67108864
		}

		// Token: 0x020003DC RID: 988
		[Flags]
		public enum EventTypes
		{
			// Token: 0x04001BF1 RID: 7153
			DLPActionHits = 1,
			// Token: 0x04001BF2 RID: 7154
			DLPMessages = 2,
			// Token: 0x04001BF3 RID: 7155
			DLPPolicyFalsePositive = 4,
			// Token: 0x04001BF4 RID: 7156
			DLPPolicyHits = 8,
			// Token: 0x04001BF5 RID: 7157
			DLPPolicyOverride = 16,
			// Token: 0x04001BF6 RID: 7158
			DLPRuleHits = 32,
			// Token: 0x04001BF7 RID: 7159
			GoodMail = 64,
			// Token: 0x04001BF8 RID: 7160
			Malware = 128,
			// Token: 0x04001BF9 RID: 7161
			SpamContentFiltered = 256,
			// Token: 0x04001BFA RID: 7162
			SpamEnvelopeBlock = 512,
			// Token: 0x04001BFB RID: 7163
			SpamIPBlock = 1024,
			// Token: 0x04001BFC RID: 7164
			TopMailUser = 2048,
			// Token: 0x04001BFD RID: 7165
			TopMalware = 4096,
			// Token: 0x04001BFE RID: 7166
			TopMalwareUser = 8192,
			// Token: 0x04001BFF RID: 7167
			TopSpamUser = 16384,
			// Token: 0x04001C00 RID: 7168
			TransportRuleActionHits = 32768,
			// Token: 0x04001C01 RID: 7169
			TransportRuleHits = 65536,
			// Token: 0x04001C02 RID: 7170
			TransportRuleMessages = 131072,
			// Token: 0x04001C03 RID: 7171
			SpamDBEBFilter = 262144
		}

		// Token: 0x020003DD RID: 989
		[Flags]
		public enum SummarizeByValues
		{
			// Token: 0x04001C05 RID: 7173
			Action = 1,
			// Token: 0x04001C06 RID: 7174
			DlpPolicy = 2,
			// Token: 0x04001C07 RID: 7175
			Domain = 4,
			// Token: 0x04001C08 RID: 7176
			EventType = 8,
			// Token: 0x04001C09 RID: 7177
			TransportRule = 16
		}

		// Token: 0x020003DE RID: 990
		[Flags]
		internal enum DirectionValues
		{
			// Token: 0x04001C0B RID: 7179
			Inbound = 1,
			// Token: 0x04001C0C RID: 7180
			Outbound = 2
		}

		// Token: 0x020003DF RID: 991
		internal enum AggregateByValues
		{
			// Token: 0x04001C0E RID: 7182
			Hour,
			// Token: 0x04001C0F RID: 7183
			Day,
			// Token: 0x04001C10 RID: 7184
			Summary
		}

		// Token: 0x020003E0 RID: 992
		[Flags]
		internal enum DeliveryStatusValues
		{
			// Token: 0x04001C12 RID: 7186
			None = 1,
			// Token: 0x04001C13 RID: 7187
			Delivered = 2,
			// Token: 0x04001C14 RID: 7188
			Pending = 4,
			// Token: 0x04001C15 RID: 7189
			Failed = 8,
			// Token: 0x04001C16 RID: 7190
			Expanded = 16
		}

		// Token: 0x020003E1 RID: 993
		[Flags]
		public enum Source
		{
			// Token: 0x04001C18 RID: 7192
			EXO = 1,
			// Token: 0x04001C19 RID: 7193
			SPO = 2,
			// Token: 0x04001C1A RID: 7194
			ODB = 4
		}

		// Token: 0x020003E2 RID: 994
		internal static class DalTypes
		{
			// Token: 0x04001C1B RID: 7195
			internal const string HygieneDataAssemblyName = "Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C1C RID: 7196
			internal const string DefaultDataSessionTypeName = "Microsoft.Exchange.Hygiene.Data.MessageTrace.MessageTraceSession";

			// Token: 0x04001C1D RID: 7197
			internal const string AsyncQueueDataSessionTypeName = "Microsoft.Exchange.Hygiene.Data.AsyncQueue.AsyncQueueSession";

			// Token: 0x04001C1E RID: 7198
			internal const string DefaultDataSessionMethodName = "FindReportObject";

			// Token: 0x04001C1F RID: 7199
			internal const string FindMigrationReportMethodName = "FindMigrationReport";

			// Token: 0x04001C20 RID: 7200
			internal const string SchemaTypeName = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.Schema, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C21 RID: 7201
			internal const string TrafficReport = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.TrafficReport, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C22 RID: 7202
			internal const string TopTrafficReport = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.TopTrafficReport, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C23 RID: 7203
			internal const string PolicyTrafficReport = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C24 RID: 7204
			internal const string MessageDetailReport = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MessageDetailReport, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C25 RID: 7205
			internal const string MessageTrace = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MessageTrace, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C26 RID: 7206
			internal const string MessageTraceDetail = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MessageTraceDetail, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C27 RID: 7207
			internal const string DLPMessageDetail = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.DLPMessageDetail, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C28 RID: 7208
			internal const string DLPReportDetail = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.DLPUnifiedDetail, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C29 RID: 7209
			internal const string DLPPolicyReport = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C2A RID: 7210
			internal const string MalwareMessageDetail = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MalwareMessageDetail, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C2B RID: 7211
			internal const string PolicyMessageDetail = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyMessageDetail, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C2C RID: 7212
			internal const string SpamMessageDetail = "Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.SpamMessageDetail, Microsoft.Exchange.Hygiene.Data";

			// Token: 0x04001C2D RID: 7213
			internal const string AsyncQueueReport = "Microsoft.Exchange.Hygiene.Data.AsyncQueue.AsyncQueueReport, Microsoft.Exchange.Hygiene.Data";
		}

		// Token: 0x020003E3 RID: 995
		internal static class Utilities
		{
			// Token: 0x06002325 RID: 8997 RVA: 0x0008EA09 File Offset: 0x0008CC09
			internal static int ToQueryDate(DateTime date)
			{
				return int.Parse(string.Format("{0}{1:D2}{2:D2}", date.Year, date.Month, date.Day));
			}

			// Token: 0x06002326 RID: 8998 RVA: 0x0008EA3E File Offset: 0x0008CC3E
			internal static int ToQueryHour(DateTime date)
			{
				return date.Hour;
			}

			// Token: 0x06002327 RID: 8999 RVA: 0x0008EA47 File Offset: 0x0008CC47
			internal static DateTime FromQueryDate(int date, int hour)
			{
				if (date == 0 && hour == 0)
				{
					return DateTime.MinValue;
				}
				return new DateTime(date / 10000, date % 10000 / 100, date % 100, hour, 0, 0);
			}

			// Token: 0x06002328 RID: 9000 RVA: 0x0008EA74 File Offset: 0x0008CC74
			internal static PropertyDefinition GetSchemaPropertyDefinition(string name)
			{
				Type type = Type.GetType("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.Schema, Microsoft.Exchange.Hygiene.Data");
				FieldInfo field = type.GetField(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
				return (PropertyDefinition)field.GetValue(null);
			}

			// Token: 0x06002329 RID: 9001 RVA: 0x0008EAA4 File Offset: 0x0008CCA4
			internal static DataTable CreateDataTable(IEnumerable values)
			{
				DataTable dataTable = new DataTable("TrafficTypeFilterTableType");
				dataTable.Columns.Add("TrafficTypeFilter");
				foreach (object obj in values)
				{
					dataTable.Rows.Add(new object[]
					{
						obj.ToString()
					});
				}
				return dataTable;
			}

			// Token: 0x0600232A RID: 9002 RVA: 0x0008EB2C File Offset: 0x0008CD2C
			internal static DataTable CreateDataTable(Enum values)
			{
				DataTable dataTable = new DataTable("TrafficTypeFilterTableType");
				dataTable.Columns.Add("TrafficTypeFilter");
				foreach (string text in values.ToString().Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					dataTable.Rows.Add(new object[]
					{
						text.Trim()
					});
				}
				return dataTable;
			}

			// Token: 0x0600232B RID: 9003 RVA: 0x0008EBA8 File Offset: 0x0008CDA8
			internal static IList<Tuple<PropertyInfo, TAttribute>> GetProperties<TAttribute>(Type type)
			{
				List<Tuple<PropertyInfo, TAttribute>> list = new List<Tuple<PropertyInfo, TAttribute>>();
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				foreach (PropertyInfo propertyInfo in properties)
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(TAttribute), true);
					for (int j = 0; j < customAttributes.Length; j++)
					{
						TAttribute item = (TAttribute)((object)customAttributes[j]);
						list.Add(Tuple.Create<PropertyInfo, TAttribute>(propertyInfo, item));
					}
				}
				return list;
			}

			// Token: 0x0600232C RID: 9004 RVA: 0x0008EC20 File Offset: 0x0008CE20
			internal static object Invoke(MethodInfo method, object instance, params object[] args)
			{
				try
				{
					if (method != null)
					{
						return method.Invoke(instance, args);
					}
				}
				catch (TargetInvocationException ex)
				{
					throw ex.GetBaseException();
				}
				return null;
			}

			// Token: 0x0600232D RID: 9005 RVA: 0x0008EC60 File Offset: 0x0008CE60
			internal static bool ValidateEmailAddress(string address, out bool isWildCard)
			{
				isWildCard = false;
				if (string.IsNullOrEmpty(address))
				{
					return false;
				}
				if (address[0] == '@')
				{
					address = string.Format("*{0}", address);
				}
				SmtpAddress smtpAddress = new SmtpAddress(address);
				bool isValidAddress = smtpAddress.IsValidAddress;
				isWildCard = (isValidAddress && smtpAddress.Local.Contains('*'));
				return isValidAddress;
			}

			// Token: 0x0600232E RID: 9006 RVA: 0x0008ECBC File Offset: 0x0008CEBC
			internal static string GenerateDetailedError(Exception e)
			{
				StringBuilder stringBuilder = new StringBuilder("Exception Details: \n");
				while (e != null)
				{
					Type type = e.GetType();
					if (type.IsGenericType && typeof(FaultException<>) == type.GetGenericTypeDefinition())
					{
						PropertyInfo property = type.GetProperty("Detail", BindingFlags.Instance | BindingFlags.Public);
						if (property != null)
						{
							object value = property.GetValue(e, null);
							if (value != null)
							{
								stringBuilder.AppendFormat("Fault Detail: {0}\n", value.ToString());
							}
						}
					}
					stringBuilder.AppendFormat("Message: {0}\n", e.Message);
					stringBuilder.AppendFormat("Call Stack: {0}\n", e.StackTrace);
					stringBuilder.AppendFormat("Additional Data:\n", new object[0]);
					foreach (object obj in e.Data)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						stringBuilder.AppendFormat("Key = {0}, Value = {1}", dictionaryEntry.Key, dictionaryEntry.Value);
					}
					e = e.InnerException;
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600232F RID: 9007 RVA: 0x0008EDE8 File Offset: 0x0008CFE8
			internal static void ValidateParameters(Task task, Func<IConfigDataProvider> getConfigDataProviderFunction, IEnumerable<CmdletValidator.ValidatorTypes> validationTypes)
			{
				foreach (Tuple<PropertyInfo, CmdletValidator> tuple in Schema.Utilities.GetProperties<CmdletValidator>(task.GetType()))
				{
					if (validationTypes.Contains(tuple.Item2.ValidatorType))
					{
						tuple.Item2.Validate(new CmdletValidator.CmdletValidatorArgs(tuple.Item1, task, getConfigDataProviderFunction));
					}
				}
			}

			// Token: 0x06002330 RID: 9008 RVA: 0x0008EE60 File Offset: 0x0008D060
			internal static bool HasDlpRole(Task task)
			{
				return !DatacenterRegistry.IsForefrontForOffice() && (task.ExchangeRunspaceConfig == null || task.ExchangeRunspaceConfig.HasRoleOfType(RoleType.DataLossPrevention) || task.ExchangeRunspaceConfig.HasRoleOfType(RoleType.PersonallyIdentifiableInformation));
			}

			// Token: 0x06002331 RID: 9009 RVA: 0x0008EE94 File Offset: 0x0008D094
			internal static IList<string> GetEventTypes(Task task)
			{
				List<string> list = Enum.GetNames(typeof(Schema.EventTypes)).ToList<string>();
				if (!Schema.Utilities.HasDlpRole(task))
				{
					Schema.Utilities.RemoveDlpEventTypes(list);
				}
				return list;
			}

			// Token: 0x06002332 RID: 9010 RVA: 0x0008EEC8 File Offset: 0x0008D0C8
			internal static IList<string> GetSources(Task task)
			{
				return Enum.GetNames(typeof(Schema.Source)).ToList<string>();
			}

			// Token: 0x06002333 RID: 9011 RVA: 0x0008EF08 File Offset: 0x0008D108
			internal static void RemoveDlpEventTypes(List<string> eventTypes)
			{
				HashSet<string> dlpValues = new HashSet<string>
				{
					Schema.EventTypes.DLPActionHits.ToString().ToLower(),
					Schema.EventTypes.DLPMessages.ToString().ToLower(),
					Schema.EventTypes.DLPPolicyFalsePositive.ToString().ToLower(),
					Schema.EventTypes.DLPPolicyHits.ToString().ToLower(),
					Schema.EventTypes.DLPPolicyOverride.ToString().ToLower(),
					Schema.EventTypes.DLPRuleHits.ToString().ToLower()
				};
				eventTypes.RemoveAll((string value) => dlpValues.Contains(value.ToLower()));
			}

			// Token: 0x06002334 RID: 9012 RVA: 0x0008EFC8 File Offset: 0x0008D1C8
			internal static void CheckDates(DateTime? startDate, DateTime? endDate, Schema.Utilities.NotifyNeedDefaultDatesDelegate needDefaultDateAction, Schema.Utilities.ValidateDatesDelegate validateDatesAction)
			{
				if (startDate != null && endDate != null)
				{
					if (validateDatesAction != null)
					{
						validateDatesAction(startDate.Value, endDate.Value);
						return;
					}
				}
				else
				{
					if (startDate != null || endDate != null)
					{
						LocalizedString message = (startDate == null) ? Strings.RequiredStartDateParameter : Strings.RequiredEndDateParameter;
						throw new Microsoft.Exchange.Management.ReportingTask.InvalidExpressionException(message);
					}
					if (needDefaultDateAction != null)
					{
						needDefaultDateAction();
						return;
					}
				}
			}

			// Token: 0x06002335 RID: 9013 RVA: 0x0008F03D File Offset: 0x0008D23D
			internal static void VerifyDateRange(DateTime startDate, DateTime endDate)
			{
				if (endDate < startDate)
				{
					throw new Microsoft.Exchange.Management.ReportingTask.InvalidExpressionException(Strings.InvalidEndDate);
				}
			}

			// Token: 0x06002336 RID: 9014 RVA: 0x0008F054 File Offset: 0x0008D254
			internal static string[] Split(Enum enumeration)
			{
				return enumeration.ToString().Split(new char[]
				{
					',',
					' '
				}, StringSplitOptions.RemoveEmptyEntries);
			}

			// Token: 0x06002337 RID: 9015 RVA: 0x0008F080 File Offset: 0x0008D280
			internal static void AddRange<T>(IList<T> property, IEnumerable<T> values)
			{
				foreach (T item in values)
				{
					property.Add(item);
				}
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x0008F0C8 File Offset: 0x0008D2C8
			internal static void Redact(object targetOfRedaction)
			{
				Schema.Utilities.Redact(targetOfRedaction, Schema.Utilities.GetProperties<RedactAttribute>(targetOfRedaction.GetType()));
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x0008F0DC File Offset: 0x0008D2DC
			internal static void Redact(object targetOfRedaction, IList<Tuple<PropertyInfo, RedactAttribute>> redactionAttributes)
			{
				foreach (Tuple<PropertyInfo, RedactAttribute> tuple in redactionAttributes)
				{
					tuple.Item2.Redact(tuple.Item1, targetOfRedaction);
				}
			}

			// Token: 0x0600233A RID: 9018 RVA: 0x0008F130 File Offset: 0x0008D330
			internal static string GetOrganizationName(OrganizationIdParameter organization)
			{
				string result = string.Empty;
				if (organization != null)
				{
					result = ((organization.InternalADObjectId != null) ? organization.InternalADObjectId.Name : organization.RawIdentity);
				}
				return result;
			}

			// Token: 0x020003E4 RID: 996
			// (Invoke) Token: 0x0600233C RID: 9020
			internal delegate void NotifyNeedDefaultDatesDelegate();

			// Token: 0x020003E5 RID: 997
			// (Invoke) Token: 0x06002340 RID: 9024
			internal delegate void ValidateDatesDelegate(DateTime startDate, DateTime endDate);
		}
	}
}
