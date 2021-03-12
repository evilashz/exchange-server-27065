using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema.Mailbox;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F27 RID: 3879
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AuditEventRecordAdapter : IAuditEvent
	{
		// Token: 0x0600855C RID: 34140 RVA: 0x00247678 File Offset: 0x00245878
		protected AuditEventRecordAdapter(ExchangeMailboxAuditBaseRecord record, string displayOrganizationId)
		{
			this.record = record;
			Util.ThrowOnNullArgument(record, "record");
			MailboxAuditOperations mailboxAuditOperations;
			this.AuditOperation = (Enum.TryParse<MailboxAuditOperations>(record.Operation, out mailboxAuditOperations) ? mailboxAuditOperations : MailboxAuditOperations.None);
			this.Result = ((record.OperationResult == null) ? OperationResult.Succeeded : ((record.OperationResult == 2) ? OperationResult.PartiallySucceeded : OperationResult.Failed));
			this.OrganizationId = displayOrganizationId;
			this.MailboxGuid = record.MailboxGuid;
			this.OperationName = record.Operation;
			this.LogonTypeName = record.LogonType.ToString();
			this.OperationSucceeded = ((record.OperationResult == null) ? OperationResult.Succeeded : ((record.OperationResult == 2) ? OperationResult.PartiallySucceeded : OperationResult.Failed));
			this.ExternalAccess = record.ExternalAccess;
			this.CreationTime = record.CreationTime;
			this.RecordId = record.Id;
		}

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x0600855D RID: 34141 RVA: 0x0024774B File Offset: 0x0024594B
		// (set) Token: 0x0600855E RID: 34142 RVA: 0x00247753 File Offset: 0x00245953
		internal MailboxAuditOperations AuditOperation { get; private set; }

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x0600855F RID: 34143 RVA: 0x0024775C File Offset: 0x0024595C
		// (set) Token: 0x06008560 RID: 34144 RVA: 0x00247764 File Offset: 0x00245964
		internal OperationResult Result { get; private set; }

		// Token: 0x17002353 RID: 9043
		// (get) Token: 0x06008561 RID: 34145 RVA: 0x0024776D File Offset: 0x0024596D
		// (set) Token: 0x06008562 RID: 34146 RVA: 0x00247775 File Offset: 0x00245975
		public DateTime CreationTime { get; private set; }

		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x06008563 RID: 34147 RVA: 0x0024777E File Offset: 0x0024597E
		// (set) Token: 0x06008564 RID: 34148 RVA: 0x00247786 File Offset: 0x00245986
		public Guid RecordId { get; private set; }

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x06008565 RID: 34149 RVA: 0x0024778F File Offset: 0x0024598F
		// (set) Token: 0x06008566 RID: 34150 RVA: 0x00247797 File Offset: 0x00245997
		public string OrganizationId { get; private set; }

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x06008567 RID: 34151 RVA: 0x002477A0 File Offset: 0x002459A0
		// (set) Token: 0x06008568 RID: 34152 RVA: 0x002477A8 File Offset: 0x002459A8
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x06008569 RID: 34153 RVA: 0x002477B1 File Offset: 0x002459B1
		// (set) Token: 0x0600856A RID: 34154 RVA: 0x002477B9 File Offset: 0x002459B9
		public string OperationName { get; private set; }

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x0600856B RID: 34155 RVA: 0x002477C2 File Offset: 0x002459C2
		// (set) Token: 0x0600856C RID: 34156 RVA: 0x002477CA File Offset: 0x002459CA
		public string LogonTypeName { get; private set; }

		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x0600856D RID: 34157 RVA: 0x002477D3 File Offset: 0x002459D3
		// (set) Token: 0x0600856E RID: 34158 RVA: 0x002477DB File Offset: 0x002459DB
		public OperationResult OperationSucceeded { get; private set; }

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x0600856F RID: 34159 RVA: 0x002477E4 File Offset: 0x002459E4
		// (set) Token: 0x06008570 RID: 34160 RVA: 0x002477EC File Offset: 0x002459EC
		public bool ExternalAccess { get; private set; }

		// Token: 0x06008571 RID: 34161 RVA: 0x002477F5 File Offset: 0x002459F5
		public IAuditLogRecord GetLogRecord()
		{
			return new AuditEventRecordAdapter.AuditLogRecord(this);
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x00247D6C File Offset: 0x00245F6C
		protected virtual IEnumerable<KeyValuePair<string, string>> InternalGetEventDetails()
		{
			yield return this.MakePair("Operation", this.AuditOperation);
			yield return this.MakePair("OperationResult", this.Result);
			yield return this.MakePair("LogonType", this.record.LogonType);
			yield return this.MakePair("ExternalAccess", this.ExternalAccess);
			yield return this.MakePair("UtcTime", this.CreationTime.ToString("s"));
			yield return this.MakePair("InternalLogonType", AuditEventRecordAdapter.GetInternalLogonType(this.record.InternalLogonType));
			yield return this.MakePair("MailboxGuid", this.record.MailboxGuid);
			yield return this.MakePair("MailboxOwnerUPN", this.record.MailboxOwnerUPN);
			KeyValuePair<string, string> p;
			if (!string.IsNullOrEmpty(this.record.MailboxOwnerSid))
			{
				yield return this.MakePair("MailboxOwnerSid", this.record.MailboxOwnerSid);
				if (this.TryMakePair("MailboxOwnerMasterAccountSid", this.record.MailboxOwnerMasterAccountSid, out p))
				{
					yield return p;
				}
			}
			if (this.TryMakePair("LogonUserSid", this.record.LogonUserSid, out p))
			{
				yield return p;
			}
			if (this.TryMakePair("LogonUserDisplayName", this.record.LogonUserDisplayName, out p))
			{
				yield return p;
			}
			if (this.TryMakePair("ClientInfoString", this.record.ClientInfoString, out p))
			{
				yield return p;
			}
			yield return this.MakePair("ClientIPAddress", this.record.ClientIPAddress);
			if (this.TryMakePair("ClientMachineName", this.record.ClientMachineName, out p))
			{
				yield return p;
			}
			if (this.TryMakePair("ClientProcessName", this.record.ClientProcessName, out p))
			{
				yield return p;
			}
			if (this.TryMakePair("ClientVersion", this.record.ClientVersion, out p))
			{
				yield return p;
			}
			if (this.TryMakePair("OriginatingServer", this.record.OriginatingServer, out p))
			{
				yield return p;
			}
			yield break;
		}

		// Token: 0x06008573 RID: 34163 RVA: 0x00247D89 File Offset: 0x00245F89
		protected KeyValuePair<string, string> MakePair(string name, string value)
		{
			return new KeyValuePair<string, string>(name, value);
		}

		// Token: 0x06008574 RID: 34164 RVA: 0x00247D94 File Offset: 0x00245F94
		protected KeyValuePair<string, string> MakePair(string name, object value)
		{
			return new KeyValuePair<string, string>(name, string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value
			}));
		}

		// Token: 0x06008575 RID: 34165 RVA: 0x00247DC4 File Offset: 0x00245FC4
		protected bool TryMakePair(string name, string value, out KeyValuePair<string, string> pair)
		{
			bool flag = !string.IsNullOrWhiteSpace(value);
			pair = (flag ? new KeyValuePair<string, string>(name, value) : default(KeyValuePair<string, string>));
			return flag;
		}

		// Token: 0x06008576 RID: 34166 RVA: 0x00247DF8 File Offset: 0x00245FF8
		protected bool TryMakePair<T>(string name, T? value, out KeyValuePair<string, string> pair) where T : struct
		{
			bool flag = value != null;
			pair = (flag ? new KeyValuePair<string, string>(name, string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value.Value
			})) : default(KeyValuePair<string, string>));
			return flag;
		}

		// Token: 0x06008577 RID: 34167 RVA: 0x00247E50 File Offset: 0x00246050
		protected bool TryMakePair<T>(string name, T value, out KeyValuePair<string, string> pair) where T : class
		{
			bool flag = value != null;
			pair = (flag ? new KeyValuePair<string, string>(name, string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value
			})) : default(KeyValuePair<string, string>));
			return flag;
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x00247EA8 File Offset: 0x002460A8
		private static string GetInternalLogonType(LogonType logonType)
		{
			if (logonType == 2)
			{
				return "Delegated";
			}
			return logonType.ToString();
		}

		// Token: 0x04005957 RID: 22871
		internal const int InitialCapacityEstimate = 1024;

		// Token: 0x04005958 RID: 22872
		private readonly ExchangeMailboxAuditBaseRecord record;

		// Token: 0x02000F29 RID: 3881
		private class AuditLogRecord : IAuditLogRecord
		{
			// Token: 0x0600857F RID: 34175 RVA: 0x00247ECC File Offset: 0x002460CC
			public AuditLogRecord(AuditEventRecordAdapter eventData)
			{
				this.UserId = eventData.record.LogonUserSid;
				this.CreationTime = eventData.CreationTime;
				this.Operation = eventData.AuditOperation.ToString();
				this.eventDetails = new List<KeyValuePair<string, string>>(eventData.InternalGetEventDetails());
			}

			// Token: 0x17002360 RID: 9056
			// (get) Token: 0x06008580 RID: 34176 RVA: 0x00247F23 File Offset: 0x00246123
			public AuditLogRecordType RecordType
			{
				get
				{
					return AuditLogRecordType.MailboxAudit;
				}
			}

			// Token: 0x17002361 RID: 9057
			// (get) Token: 0x06008581 RID: 34177 RVA: 0x00247F26 File Offset: 0x00246126
			// (set) Token: 0x06008582 RID: 34178 RVA: 0x00247F2E File Offset: 0x0024612E
			public DateTime CreationTime { get; private set; }

			// Token: 0x17002362 RID: 9058
			// (get) Token: 0x06008583 RID: 34179 RVA: 0x00247F37 File Offset: 0x00246137
			// (set) Token: 0x06008584 RID: 34180 RVA: 0x00247F3F File Offset: 0x0024613F
			public string Operation { get; private set; }

			// Token: 0x17002363 RID: 9059
			// (get) Token: 0x06008585 RID: 34181 RVA: 0x00247F48 File Offset: 0x00246148
			public string ObjectId
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002364 RID: 9060
			// (get) Token: 0x06008586 RID: 34182 RVA: 0x00247F4B File Offset: 0x0024614B
			// (set) Token: 0x06008587 RID: 34183 RVA: 0x00247F53 File Offset: 0x00246153
			public string UserId { get; private set; }

			// Token: 0x06008588 RID: 34184 RVA: 0x00247F5C File Offset: 0x0024615C
			public IEnumerable<KeyValuePair<string, string>> GetDetails()
			{
				return this.eventDetails;
			}

			// Token: 0x04005963 RID: 22883
			private readonly List<KeyValuePair<string, string>> eventDetails;
		}
	}
}
