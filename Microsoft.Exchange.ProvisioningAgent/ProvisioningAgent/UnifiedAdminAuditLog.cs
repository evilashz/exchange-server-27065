using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema;
using Microsoft.Office.Compliance.Audit.Schema.Admin;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedAdminAuditLog : IAuditLog, IDisposable
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000055C5 File Offset: 0x000037C5
		public void Dispose()
		{
			this.logger.Dispose();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000055D2 File Offset: 0x000037D2
		public UnifiedAdminAuditLog(string organizationId, string organizationName)
		{
			this.organizationId = organizationId;
			this.organizationName = organizationName;
			this.logger = new UnifiedAuditLogger();
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000055F3 File Offset: 0x000037F3
		public UnifiedAuditLogger LoggerForTest
		{
			get
			{
				return this.logger;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000055FB File Offset: 0x000037FB
		public DateTime EstimatedLogStartTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005602 File Offset: 0x00003802
		public DateTime EstimatedLogEndTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005609 File Offset: 0x00003809
		public bool IsAsynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000560C File Offset: 0x0000380C
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005614 File Offset: 0x00003814
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			AuditRecord record = this.CreateAuditRecordObject(auditRecord);
			return this.logger.WriteAuditRecord(record);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005638 File Offset: 0x00003838
		public AuditRecord CreateAuditRecordObject(IAuditLogRecord auditRecord)
		{
			AuditRecord auditRecord2;
			Action<string, string> action = this.CreateAuditRecordObject(auditRecord, out auditRecord2);
			auditRecord2.Initialize();
			auditRecord2.OrganizationId = this.organizationId;
			auditRecord2.OrganizationName = (string.IsNullOrEmpty(this.organizationName) ? "First Org" : this.organizationName);
			auditRecord2.ObjectId = auditRecord.ObjectId;
			auditRecord2.Operation = auditRecord.Operation;
			auditRecord2.UserId = auditRecord.UserId;
			auditRecord2.CreationTime = auditRecord.CreationTime;
			foreach (KeyValuePair<string, string> keyValuePair in auditRecord.GetDetails())
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				action(key, value);
			}
			return auditRecord2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000570C File Offset: 0x0000390C
		private Action<string, string> CreateAuditRecordObject(IAuditLogRecord auditRecord, out AuditRecord record)
		{
			if (auditRecord.RecordType != AuditLogRecordType.AdminAudit)
			{
				throw new ArgumentException(string.Format("Invalid audit record type {0}.", auditRecord.RecordType), "auditRecord");
			}
			return this.CreateAdminAuditRecordObject(out record);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005778 File Offset: 0x00003978
		private Action<string, string> CreateAdminAuditRecordObject(out AuditRecord record)
		{
			ExchangeAdminAuditRecord adminAuditRecord = new ExchangeAdminAuditRecord();
			record = adminAuditRecord;
			Dictionary<string, ModifiedProperty> modifiedProperties = new Dictionary<string, ModifiedProperty>();
			return delegate(string field, string val)
			{
				Action<ExchangeAdminAuditRecord, string, Dictionary<string, ModifiedProperty>> action;
				if (UnifiedAdminAuditLog.AdminRecordSetters.TryGetValue(field, out action))
				{
					action(adminAuditRecord, val, modifiedProperties);
				}
			};
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000057B8 File Offset: 0x000039B8
		private static NameValuePair ParseValuePair(string multiValue)
		{
			int num = multiValue.IndexOf('=');
			if (num <= 0)
			{
				return new NameValuePair
				{
					Name = string.Empty,
					Value = multiValue
				};
			}
			string name = multiValue.Substring(0, num).TrimEnd(new char[0]);
			string value = multiValue.Substring(num + 1).TrimStart(new char[0]);
			return new NameValuePair
			{
				Name = name,
				Value = value
			};
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005830 File Offset: 0x00003A30
		private static List<NameValuePair> AddParameter(List<NameValuePair> values, string paramValue)
		{
			if (values == null)
			{
				values = new List<NameValuePair>();
			}
			NameValuePair item = UnifiedAdminAuditLog.ParseValuePair(paramValue);
			values.Add(item);
			return values;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005858 File Offset: 0x00003A58
		private static List<ModifiedProperty> AddModifiedProperty(List<ModifiedProperty> values, string propertyValuePair, bool isNewValue, Dictionary<string, ModifiedProperty> context)
		{
			if (values == null)
			{
				values = new List<ModifiedProperty>();
			}
			NameValuePair nameValuePair = UnifiedAdminAuditLog.ParseValuePair(propertyValuePair);
			string name = nameValuePair.Name;
			ModifiedProperty modifiedProperty;
			if (!context.TryGetValue(name, out modifiedProperty))
			{
				modifiedProperty = new ModifiedProperty
				{
					Name = name
				};
				context[name] = modifiedProperty;
				values.Add(modifiedProperty);
			}
			if (isNewValue)
			{
				modifiedProperty.NewValue = nameValuePair.Value;
			}
			else
			{
				modifiedProperty.OldValue = nameValuePair.Value;
			}
			return values;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005940 File Offset: 0x00003B40
		// Note: this type is marked as 'beforefieldinit'.
		static UnifiedAdminAuditLog()
		{
			Dictionary<string, Action<ExchangeAdminAuditRecord, string, Dictionary<string, ModifiedProperty>>> dictionary = new Dictionary<string, Action<ExchangeAdminAuditRecord, string, Dictionary<string, ModifiedProperty>>>();
			dictionary.Add("Error", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.Error = val;
			});
			dictionary.Add("ExternalAccess", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.ExternalAccess = bool.Parse(val);
			});
			dictionary.Add("Modified Object Resolved Name", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.ModifiedObjectResolvedName = val;
			});
			dictionary.Add("OriginatingServer", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.OriginatingServer = val;
			});
			dictionary.Add("Succeeded", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.Succeeded = new bool?(bool.Parse(val));
			});
			dictionary.Add("Parameter", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.Parameters = UnifiedAdminAuditLog.AddParameter(record.Parameters, val);
			});
			dictionary.Add("Property Modified", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.ModifiedProperties = UnifiedAdminAuditLog.AddModifiedProperty(record.ModifiedProperties, val, true, context);
			});
			dictionary.Add("Property Original", delegate(ExchangeAdminAuditRecord record, string val, Dictionary<string, ModifiedProperty> context)
			{
				record.ModifiedProperties = UnifiedAdminAuditLog.AddModifiedProperty(record.ModifiedProperties, val, false, context);
			});
			UnifiedAdminAuditLog.AdminRecordSetters = dictionary;
		}

		// Token: 0x0400006F RID: 111
		private readonly string organizationId;

		// Token: 0x04000070 RID: 112
		private readonly string organizationName;

		// Token: 0x04000071 RID: 113
		private readonly UnifiedAuditLogger logger;

		// Token: 0x04000072 RID: 114
		private static readonly Dictionary<string, Action<ExchangeAdminAuditRecord, string, Dictionary<string, ModifiedProperty>>> AdminRecordSetters;
	}
}
