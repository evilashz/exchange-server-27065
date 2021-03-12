using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public sealed class AdminAuditLogEvent : ConfigurableObject
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00006DDF File Offset: 0x00004FDF
		public AdminAuditLogEvent() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006E5C File Offset: 0x0000505C
		public AdminAuditLogEvent(AdminAuditLogEventId identity, string eventLog)
		{
			AdminAuditLogEvent.<>c__DisplayClass4 CS$<>8__locals1 = new AdminAuditLogEvent.<>c__DisplayClass4();
			CS$<>8__locals1.eventLog = eventLog;
			this..ctor();
			CS$<>8__locals1.<>4__this = this;
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (CS$<>8__locals1.eventLog == null)
			{
				throw new ArgumentNullException("eventLog");
			}
			this.propertyBag[SimpleProviderObjectSchema.Identity] = identity;
			Dictionary<string, AdminAuditLogModifiedProperty> modifiedProperties = new Dictionary<string, AdminAuditLogModifiedProperty>(StringComparer.OrdinalIgnoreCase);
			List<PropertyParseSchema> list = new List<PropertyParseSchema>(AdminAuditLogEvent.AdminAuditLogRecordParseSchema);
			list.Add(new PropertyParseSchema("Parameter", AdminAuditLogSchema.CmdletParameters, (string line) => CS$<>8__locals1.<>4__this.ParseCmdletParameter(line, CS$<>8__locals1.eventLog)));
			list.Add(new PropertyParseSchema("Property Modified", AdminAuditLogSchema.ModifiedProperties, (string line) => CS$<>8__locals1.<>4__this.ParseModifiedProperty(line, modifiedProperties, true, CS$<>8__locals1.eventLog)));
			list.Add(new PropertyParseSchema("Property Original", AdminAuditLogSchema.ModifiedProperties, (string line) => CS$<>8__locals1.<>4__this.ParseModifiedProperty(line, modifiedProperties, false, CS$<>8__locals1.eventLog)));
			AuditLogParseSerialize.ParseAdminAuditLogRecord(this, list, CS$<>8__locals1.eventLog);
			if (!string.IsNullOrEmpty(this.ObjectModified) && string.IsNullOrEmpty(this.ModifiedObjectResolvedName))
			{
				this.ModifiedObjectResolvedName = this.ObjectModified;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006F7A File Offset: 0x0000517A
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00006F91 File Offset: 0x00005191
		public string ObjectModified
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.ObjectModified] as string;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.ObjectModified] = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006FA4 File Offset: 0x000051A4
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000705C File Offset: 0x0000525C
		internal string SearchObject
		{
			get
			{
				string text = this.propertyBag[AdminAuditLogSchema.CmdletName] as string;
				if (AdminAuditLogHelper.IsDiscoverySearchModifierCmdlet(text))
				{
					return this.ObjectModified;
				}
				if (string.Compare(text, "new-mailboxsearch", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					MultiValuedProperty<AdminAuditLogCmdletParameter> multiValuedProperty = this.propertyBag[AdminAuditLogSchema.CmdletParameters] as MultiValuedProperty<AdminAuditLogCmdletParameter>;
					if (multiValuedProperty != null)
					{
						foreach (AdminAuditLogCmdletParameter adminAuditLogCmdletParameter in multiValuedProperty)
						{
							if (string.Compare(adminAuditLogCmdletParameter.Name, "name", StringComparison.InvariantCultureIgnoreCase) == 0)
							{
								return adminAuditLogCmdletParameter.Value;
							}
						}
					}
				}
				return string.Empty;
			}
			private set
			{
				this.propertyBag[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000706F File Offset: 0x0000526F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00007086 File Offset: 0x00005286
		internal string ModifiedObjectResolvedName
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.ModifiedObjectResolvedName] as string;
			}
			set
			{
				this.propertyBag[AdminAuditLogSchema.ModifiedObjectResolvedName] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007099 File Offset: 0x00005299
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000070B0 File Offset: 0x000052B0
		public string CmdletName
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.CmdletName] as string;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.CmdletName] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000070C3 File Offset: 0x000052C3
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000070DA File Offset: 0x000052DA
		public MultiValuedProperty<AdminAuditLogCmdletParameter> CmdletParameters
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.CmdletParameters] as MultiValuedProperty<AdminAuditLogCmdletParameter>;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.CmdletParameters] = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000070ED File Offset: 0x000052ED
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00007104 File Offset: 0x00005304
		public MultiValuedProperty<AdminAuditLogModifiedProperty> ModifiedProperties
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.ModifiedProperties] as MultiValuedProperty<AdminAuditLogModifiedProperty>;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.ModifiedProperties] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007117 File Offset: 0x00005317
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000712E File Offset: 0x0000532E
		public string Caller
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.Caller] as string;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.Caller] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007141 File Offset: 0x00005341
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000715D File Offset: 0x0000535D
		public bool? ExternalAccess
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.ExternalAccess] as bool?;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.ExternalAccess] = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007175 File Offset: 0x00005375
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00007191 File Offset: 0x00005391
		public bool? Succeeded
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.Succeeded] as bool?;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.Succeeded] = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000071A9 File Offset: 0x000053A9
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000071C0 File Offset: 0x000053C0
		public string Error
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.Error] as string;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.Error] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000071D3 File Offset: 0x000053D3
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000071EF File Offset: 0x000053EF
		public DateTime? RunDate
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.RunDate] as DateTime?;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.RunDate] = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007207 File Offset: 0x00005407
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000721E File Offset: 0x0000541E
		public string OriginatingServer
		{
			get
			{
				return this.propertyBag[AdminAuditLogSchema.OriginatingServer] as string;
			}
			internal set
			{
				this.propertyBag[AdminAuditLogSchema.OriginatingServer] = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007231 File Offset: 0x00005431
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AdminAuditLogEvent.schema;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007250 File Offset: 0x00005450
		private static PropertyParseSchema[] AdminAuditLogRecordParseSchema
		{
			get
			{
				if (AdminAuditLogEvent.adminAuditLogRecordParseSchema == null)
				{
					PropertyParseSchema[] array = new PropertyParseSchema[9];
					array[0] = new PropertyParseSchema("Cmdlet Name", AdminAuditLogSchema.CmdletName, null);
					array[1] = new PropertyParseSchema("Object Modified", AdminAuditLogSchema.ObjectModified, null);
					array[2] = new PropertyParseSchema("Modified Object Resolved Name", AdminAuditLogSchema.ModifiedObjectResolvedName, null);
					array[3] = new PropertyParseSchema("Caller", AdminAuditLogSchema.Caller, null);
					array[4] = new PropertyParseSchema("ExternalAccess", AdminAuditLogSchema.ExternalAccess, (string line) => AuditLogParseSerialize.ParseBoolean(line));
					array[5] = new PropertyParseSchema("Succeeded", AdminAuditLogSchema.Succeeded, (string line) => AuditLogParseSerialize.ParseBoolean(line));
					array[6] = new PropertyParseSchema("Run Date", AdminAuditLogSchema.RunDate, (string line) => AdminAuditLogEvent.FixAndParseRunDate(line));
					array[7] = new PropertyParseSchema("Error", AdminAuditLogSchema.Error, null);
					array[8] = new PropertyParseSchema("OriginatingServer", AdminAuditLogSchema.OriginatingServer, null);
					AdminAuditLogEvent.adminAuditLogRecordParseSchema = array;
				}
				return AdminAuditLogEvent.adminAuditLogRecordParseSchema;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000737C File Offset: 0x0000557C
		private object ParseCmdletParameter(string line, string eventLog)
		{
			try
			{
				AdminAuditLogCmdletParameter item = AdminAuditLogCmdletParameter.Parse(line);
				this.CmdletParameters.Add(item);
			}
			catch (ArgumentException)
			{
				TaskLogger.LogWarning(Strings.WarningInvalidParameterOrModifiedPropertyInAdminAuditLog(eventLog));
			}
			return null;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000073C0 File Offset: 0x000055C0
		private object ParseModifiedProperty(string line, Dictionary<string, AdminAuditLogModifiedProperty> modifiedProperties, bool newValue, string eventLog)
		{
			try
			{
				AdminAuditLogModifiedProperty adminAuditLogModifiedProperty = AdminAuditLogModifiedProperty.Parse(line, newValue);
				if (modifiedProperties.ContainsKey(adminAuditLogModifiedProperty.Name))
				{
					if (newValue)
					{
						if (modifiedProperties[adminAuditLogModifiedProperty.Name].NewValue != null)
						{
							TaskLogger.LogWarning(Strings.WarningDuplicatedPropertyModifiedEntry(adminAuditLogModifiedProperty.Name));
						}
						modifiedProperties[adminAuditLogModifiedProperty.Name].NewValue = adminAuditLogModifiedProperty.NewValue;
					}
					else
					{
						if (modifiedProperties[adminAuditLogModifiedProperty.Name].OldValue != null)
						{
							TaskLogger.LogWarning(Strings.WarningDuplicatedPropertyOriginalEntry(adminAuditLogModifiedProperty.Name));
						}
						modifiedProperties[adminAuditLogModifiedProperty.Name].OldValue = adminAuditLogModifiedProperty.OldValue;
					}
				}
				else
				{
					this.ModifiedProperties.Add(adminAuditLogModifiedProperty);
					modifiedProperties[adminAuditLogModifiedProperty.Name] = adminAuditLogModifiedProperty;
				}
			}
			catch (ArgumentException)
			{
				TaskLogger.LogWarning(Strings.WarningInvalidParameterOrModifiedPropertyInAdminAuditLog(eventLog));
			}
			return null;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000749C File Offset: 0x0000569C
		private static object FixAndParseRunDate(string line)
		{
			if (line.EndsWith("UTC", StringComparison.OrdinalIgnoreCase))
			{
				line = line.Substring(0, line.Length - 3);
			}
			ExDateTime exDateTime;
			if (ExDateTime.TryParse(line, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out exDateTime))
			{
				return exDateTime.UniversalTime.ToLocalTime();
			}
			return null;
		}

		// Token: 0x04000110 RID: 272
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance<AdminAuditLogSchema>();

		// Token: 0x04000111 RID: 273
		private static PropertyParseSchema[] adminAuditLogRecordParseSchema = null;
	}
}
