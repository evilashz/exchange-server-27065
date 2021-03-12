using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F2C RID: 3884
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class AuditEvent : IAuditEvent
	{
		// Token: 0x0600858F RID: 34191 RVA: 0x00248C40 File Offset: 0x00246E40
		public AuditEvent(MailboxSession session, MailboxAuditOperations operation, COWSettings settings, OperationResult result, LogonType logonType, bool externalAccess)
		{
			EnumValidator.ThrowIfInvalid<MailboxAuditOperations>(operation);
			EnumValidator.ThrowIfInvalid<OperationResult>(result, "result");
			EnumValidator.ThrowIfInvalid<LogonType>(logonType, "logonType");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(settings, "settings");
			this.MailboxSession = session;
			this.AuditOperation = operation;
			this.COWSettings = settings;
			this.OperationSucceeded = result;
			this.LogonType = logonType;
			this.ExternalAccess = externalAccess;
			this.CreationTime = DateTime.UtcNow;
			this.RecordId = CombGuidGenerator.NewGuid(this.CreationTime);
			this.OrganizationId = (string.IsNullOrEmpty(session.OrganizationId.ToString()) ? "First Org" : session.OrganizationId.ToString());
			this.MailboxGuid = session.MailboxGuid;
			this.OperationName = operation.ToString();
			this.LogonTypeName = logonType.ToString();
		}

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x06008590 RID: 34192 RVA: 0x00248D29 File Offset: 0x00246F29
		// (set) Token: 0x06008591 RID: 34193 RVA: 0x00248D31 File Offset: 0x00246F31
		internal MailboxSession MailboxSession { get; private set; }

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x06008592 RID: 34194 RVA: 0x00248D3A File Offset: 0x00246F3A
		// (set) Token: 0x06008593 RID: 34195 RVA: 0x00248D42 File Offset: 0x00246F42
		internal MailboxAuditOperations AuditOperation { get; private set; }

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x06008594 RID: 34196 RVA: 0x00248D4B File Offset: 0x00246F4B
		// (set) Token: 0x06008595 RID: 34197 RVA: 0x00248D53 File Offset: 0x00246F53
		private protected COWSettings COWSettings { protected get; private set; }

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x06008596 RID: 34198 RVA: 0x00248D5C File Offset: 0x00246F5C
		// (set) Token: 0x06008597 RID: 34199 RVA: 0x00248D64 File Offset: 0x00246F64
		internal LogonType LogonType { get; private set; }

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x06008598 RID: 34200 RVA: 0x00248D6D File Offset: 0x00246F6D
		// (set) Token: 0x06008599 RID: 34201 RVA: 0x00248D75 File Offset: 0x00246F75
		public DateTime CreationTime { get; private set; }

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x0600859A RID: 34202 RVA: 0x00248D7E File Offset: 0x00246F7E
		// (set) Token: 0x0600859B RID: 34203 RVA: 0x00248D86 File Offset: 0x00246F86
		public Guid RecordId { get; private set; }

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x0600859C RID: 34204 RVA: 0x00248D8F File Offset: 0x00246F8F
		// (set) Token: 0x0600859D RID: 34205 RVA: 0x00248D97 File Offset: 0x00246F97
		public string OrganizationId { get; private set; }

		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x0600859E RID: 34206 RVA: 0x00248DA0 File Offset: 0x00246FA0
		// (set) Token: 0x0600859F RID: 34207 RVA: 0x00248DA8 File Offset: 0x00246FA8
		public Guid MailboxGuid { get; private set; }

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x060085A0 RID: 34208 RVA: 0x00248DB1 File Offset: 0x00246FB1
		// (set) Token: 0x060085A1 RID: 34209 RVA: 0x00248DB9 File Offset: 0x00246FB9
		public string OperationName { get; private set; }

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x060085A2 RID: 34210 RVA: 0x00248DC2 File Offset: 0x00246FC2
		// (set) Token: 0x060085A3 RID: 34211 RVA: 0x00248DCA File Offset: 0x00246FCA
		public string LogonTypeName { get; private set; }

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x060085A4 RID: 34212 RVA: 0x00248DD3 File Offset: 0x00246FD3
		// (set) Token: 0x060085A5 RID: 34213 RVA: 0x00248DDB File Offset: 0x00246FDB
		public OperationResult OperationSucceeded { get; private set; }

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x060085A6 RID: 34214 RVA: 0x00248DE4 File Offset: 0x00246FE4
		// (set) Token: 0x060085A7 RID: 34215 RVA: 0x00248DEC File Offset: 0x00246FEC
		public bool ExternalAccess { get; private set; }

		// Token: 0x060085A8 RID: 34216 RVA: 0x00248DF5 File Offset: 0x00246FF5
		public IAuditLogRecord GetLogRecord()
		{
			return new AuditEvent.AuditLogRecord(this);
		}

		// Token: 0x060085A9 RID: 34217 RVA: 0x002494E4 File Offset: 0x002476E4
		protected virtual IEnumerable<KeyValuePair<string, string>> InternalGetEventDetails()
		{
			yield return new KeyValuePair<string, string>("Operation", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.AuditOperation
			}));
			yield return new KeyValuePair<string, string>("OperationResult", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.OperationSucceeded
			}));
			yield return new KeyValuePair<string, string>("LogonType", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.AuditScopeFromLogonType(this.LogonType)
			}));
			yield return new KeyValuePair<string, string>("ExternalAccess", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.ExternalAccess
			}));
			yield return new KeyValuePair<string, string>("UtcTime", this.CreationTime.ToString("s"));
			yield return new KeyValuePair<string, string>("InternalLogonType", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.MailboxSession.LogonType
			}));
			yield return new KeyValuePair<string, string>("MailboxGuid", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid
			}));
			yield return new KeyValuePair<string, string>("MailboxOwnerUPN", this.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			if (this.MailboxSession.MailboxOwner.Sid != null)
			{
				yield return new KeyValuePair<string, string>("MailboxOwnerSid", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					this.MailboxSession.MailboxOwner.Sid
				}));
				if (this.MailboxSession.MailboxOwner.MasterAccountSid != null)
				{
					yield return new KeyValuePair<string, string>("MailboxOwnerMasterAccountSid", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
					{
						this.MailboxSession.MailboxOwner.MasterAccountSid
					}));
				}
			}
			IdentityPair pair = this.GetUserIdentityPair();
			if (pair.LogonUserSid != null)
			{
				yield return new KeyValuePair<string, string>("LogonUserSid", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					pair.LogonUserSid
				}));
			}
			if (pair.LogonUserDisplayName != null)
			{
				yield return new KeyValuePair<string, string>("LogonUserDisplayName", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					pair.LogonUserDisplayName
				}));
			}
			string clientInfoString = (this.MailboxSession.RemoteClientSessionInfo == null) ? this.MailboxSession.ClientInfoString : this.MailboxSession.RemoteClientSessionInfo.ClientInfoString;
			if (!string.IsNullOrEmpty(clientInfoString))
			{
				yield return new KeyValuePair<string, string>("ClientInfoString", clientInfoString);
			}
			yield return new KeyValuePair<string, string>("ClientIPAddress", string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.MailboxSession.ClientIPAddress
			}));
			if (!string.IsNullOrEmpty(this.MailboxSession.ClientMachineName))
			{
				yield return new KeyValuePair<string, string>("ClientMachineName", this.MailboxSession.ClientMachineName);
			}
			if (!string.IsNullOrEmpty(this.MailboxSession.ClientProcessName))
			{
				yield return new KeyValuePair<string, string>("ClientProcessName", this.MailboxSession.ClientProcessName);
			}
			if (this.MailboxSession.ClientVersion != 0L)
			{
				yield return new KeyValuePair<string, string>("ClientVersion", AuditEvent.GetVersionString(this.MailboxSession.ClientVersion));
			}
			yield return new KeyValuePair<string, string>("OriginatingServer", string.Format(CultureInfo.InvariantCulture, "{0} ({1})\r\n", new object[]
			{
				AuditEvent.MachineName,
				"15.00.1497.010"
			}));
			yield break;
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x00249504 File Offset: 0x00247704
		private IdentityPair GetUserIdentityPair()
		{
			IdentityPair result = default(IdentityPair);
			ClientSessionInfo remoteClientSessionInfo = this.MailboxSession.RemoteClientSessionInfo;
			if (remoteClientSessionInfo != null)
			{
				result.LogonUserSid = remoteClientSessionInfo.LogonUserSid;
				result.LogonUserDisplayName = remoteClientSessionInfo.LogonUserDisplayName;
			}
			else
			{
				result = IdentityHelper.GetIdentityPair(this.MailboxSession);
				if (result.LogonUserDisplayName == null && this.LogonType == LogonType.Owner && this.MailboxSession.MailboxOwner.MailboxInfo.IsArchive)
				{
					result.LogonUserDisplayName = AuditEvent.ResolveMailboxOwnerName(this.MailboxSession.MailboxOwner);
				}
			}
			return result;
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x00249590 File Offset: 0x00247790
		private static string ResolveMailboxOwnerName(IExchangePrincipal owner)
		{
			string result;
			if (!string.IsNullOrEmpty(owner.MailboxInfo.DisplayName))
			{
				result = owner.MailboxInfo.DisplayName;
			}
			else if (!string.IsNullOrEmpty(owner.Alias))
			{
				result = owner.Alias;
			}
			else
			{
				result = ((owner.ObjectId == null) ? string.Empty : owner.ObjectId.ToString());
			}
			return result;
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x002495F4 File Offset: 0x002477F4
		protected string GetCurrentFolderPathName()
		{
			string text = null;
			Folder currentFolder = this.COWSettings.GetCurrentFolder(this.MailboxSession);
			if (currentFolder != null)
			{
				text = (currentFolder.TryGetProperty(FolderSchema.FolderPathName) as string);
				if (text != null)
				{
					text = text.Replace(COWSettings.StoreIdSeparator, '\\');
				}
			}
			return text;
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x0024963C File Offset: 0x0024783C
		private static string GetVersionString(long versionNumber)
		{
			ushort num = (ushort)(versionNumber >> 48);
			ushort num2 = (ushort)((versionNumber & 281470681743360L) >> 32);
			ushort num3 = (ushort)((versionNumber & (long)((ulong)-65536)) >> 16);
			ushort num4 = (ushort)(versionNumber & 65535L);
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", new object[]
			{
				num,
				num2,
				num3,
				num4
			});
		}

		// Token: 0x060085AE RID: 34222 RVA: 0x002496B8 File Offset: 0x002478B8
		private AuditScopes AuditScopeFromLogonType(LogonType logonType)
		{
			switch (logonType)
			{
			case LogonType.Owner:
				return AuditScopes.Owner;
			case LogonType.Admin:
				return AuditScopes.Admin;
			case LogonType.Delegated:
				return AuditScopes.Delegate;
			default:
				throw new ArgumentOutOfRangeException("logonType");
			}
		}

		// Token: 0x04005969 RID: 22889
		internal const int InitialCapacityEstimate = 1024;

		// Token: 0x0400596A RID: 22890
		private static readonly string MachineName = Environment.MachineName;

		// Token: 0x02000F2D RID: 3885
		private class AuditLogRecord : IAuditLogRecord
		{
			// Token: 0x060085B0 RID: 34224 RVA: 0x002496F8 File Offset: 0x002478F8
			public AuditLogRecord(AuditEvent eventData)
			{
				this.UserId = eventData.GetUserIdentityPair().LogonUserSid;
				this.CreationTime = eventData.CreationTime;
				this.Operation = eventData.AuditOperation.ToString();
				this.eventDetails = new List<KeyValuePair<string, string>>(eventData.InternalGetEventDetails());
			}

			// Token: 0x17002371 RID: 9073
			// (get) Token: 0x060085B1 RID: 34225 RVA: 0x00249752 File Offset: 0x00247952
			public AuditLogRecordType RecordType
			{
				get
				{
					return AuditLogRecordType.MailboxAudit;
				}
			}

			// Token: 0x17002372 RID: 9074
			// (get) Token: 0x060085B2 RID: 34226 RVA: 0x00249755 File Offset: 0x00247955
			// (set) Token: 0x060085B3 RID: 34227 RVA: 0x0024975D File Offset: 0x0024795D
			public DateTime CreationTime { get; private set; }

			// Token: 0x17002373 RID: 9075
			// (get) Token: 0x060085B4 RID: 34228 RVA: 0x00249766 File Offset: 0x00247966
			// (set) Token: 0x060085B5 RID: 34229 RVA: 0x0024976E File Offset: 0x0024796E
			public string Operation { get; private set; }

			// Token: 0x17002374 RID: 9076
			// (get) Token: 0x060085B6 RID: 34230 RVA: 0x00249777 File Offset: 0x00247977
			public string ObjectId
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002375 RID: 9077
			// (get) Token: 0x060085B7 RID: 34231 RVA: 0x0024977A File Offset: 0x0024797A
			// (set) Token: 0x060085B8 RID: 34232 RVA: 0x00249782 File Offset: 0x00247982
			public string UserId { get; private set; }

			// Token: 0x060085B9 RID: 34233 RVA: 0x0024978B File Offset: 0x0024798B
			public IEnumerable<KeyValuePair<string, string>> GetDetails()
			{
				return this.eventDetails;
			}

			// Token: 0x04005977 RID: 22903
			private List<KeyValuePair<string, string>> eventDetails;
		}
	}
}
