using System;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000178 RID: 376
	internal class PooledLdapConnection : LdapConnection
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x0004CAD9 File Offset: 0x0004ACD9
		public PooledLdapConnection(ADServerInfo serverInfo, ADServerRole role, bool isNotify) : this(serverInfo, role, isNotify, null)
		{
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0004CAE8 File Offset: 0x0004ACE8
		public PooledLdapConnection(ADServerInfo serverInfo, ADServerRole role, bool isNotify, NetworkCredential networkCredential) : base(serverInfo.GetLdapDirectoryIdentifier(), networkCredential)
		{
			this.references = 1;
			this.serverInfo = serverInfo;
			Interlocked.Increment(ref PooledLdapConnection.totalCount);
			base.SessionOptions.AutoReconnect = !isNotify;
			base.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
			base.SessionOptions.PingKeepAliveTimeout = TimeSpan.FromSeconds(30.0);
			if (Environment.OSVersion.Version >= new Version(5, 2))
			{
				base.SessionOptions.SendTimeout = TimeSpan.FromSeconds(30.0);
			}
			base.SessionOptions.Signing = true;
			base.SessionOptions.Sealing = ConnectionPoolManager.LdapEncryptionEnabled;
			base.SessionOptions.ProtocolVersion = 3;
			base.AuthType = serverInfo.AuthType;
			this.isUp = true;
			this.isBind = false;
			this.isFatalError = false;
			this.isNotify = isNotify;
			this.retryCount = 0;
			this.timeMarkedDown = 0;
			this.role = role;
			this.timeCreated = Environment.TickCount;
			this.isSetupContext = this.IsSetupContext();
			ExTraceGlobals.ConnectionTracer.TraceDebug<string, string>((long)this.GetHashCode(), " LdapConnection to {0} is CREATED, encryption is {1}", this.ADServerInfo.FqdnPlusPort, base.SessionOptions.Sealing ? "ON" : "OFF");
			if (this.serverInfo.Port == 3268)
			{
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessOpenConnectionsGC, UpdateType.Add, 1U);
			}
			else
			{
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessOpenConnectionsDC, UpdateType.Add, 1U);
			}
			if (networkCredential == null)
			{
				try
				{
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						this.isLocalSystemOrNetworkService = (current.User.Equals(Globals.LocalSystemSid) || current.User.Equals(Globals.NetworkServiceSid));
						this.userIdentity = current.Name;
					}
				}
				catch (SystemException)
				{
					this.isLocalSystemOrNetworkService = false;
					this.userIdentity = null;
				}
				return;
			}
			this.isLocalSystemOrNetworkService = false;
			if (string.IsNullOrEmpty(networkCredential.Domain))
			{
				this.userIdentity = networkCredential.UserName;
				return;
			}
			this.userIdentity = string.Format("{0}\\{1}", networkCredential.Domain, networkCredential.UserName);
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0004CD1C File Offset: 0x0004AF1C
		internal ADServerRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0004CD24 File Offset: 0x0004AF24
		internal bool IsNotify
		{
			get
			{
				return this.isNotify;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0004CD2C File Offset: 0x0004AF2C
		internal bool IsUp
		{
			get
			{
				return this.isUp;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0004CD34 File Offset: 0x0004AF34
		internal bool IsFatalError
		{
			get
			{
				return this.isFatalError;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0004CD3C File Offset: 0x0004AF3C
		internal string ServerName
		{
			get
			{
				return this.serverInfo.Fqdn;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0004CD49 File Offset: 0x0004AF49
		internal int Port
		{
			get
			{
				return this.serverInfo.Port;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0004CD56 File Offset: 0x0004AF56
		internal ADServerInfo ADServerInfo
		{
			get
			{
				return this.serverInfo;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0004CD5E File Offset: 0x0004AF5E
		internal int OutstandingRequestCount
		{
			get
			{
				return this.outstandingRequestCount;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0004CD66 File Offset: 0x0004AF66
		private string UnqualifiedServerName
		{
			get
			{
				if (this.unqualifiedServerName == null)
				{
					this.unqualifiedServerName = MachineName.GetNodeNameFromFqdn(this.ServerName);
				}
				return this.unqualifiedServerName;
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0004CD88 File Offset: 0x0004AF88
		internal static ADErrorRecord AnalyzeExtendedErrorInfo(string extendedErrorInfo)
		{
			ADErrorRecord aderrorRecord = new ADErrorRecord();
			if (string.IsNullOrEmpty(extendedErrorInfo))
			{
				return aderrorRecord;
			}
			int num = 0;
			int num2 = 0;
			string text = null;
			string[] array = extendedErrorInfo.Split(new char[]
			{
				':'
			}, 2);
			if (array.Length > 1)
			{
				string s = array[0];
				if (int.TryParse(s, NumberStyles.AllowHexSpecifier, null, out num) && num != 0)
				{
					text = DirectoryStrings.ErrorAdditionalInfo(new Win32Exception(num).Message);
					int num3 = num;
					if (num3 == 8373)
					{
						text = DirectoryStrings.AppendLocalizedStrings(text, DirectoryStrings.ErrorReplicationLatency);
					}
				}
				else
				{
					num = 0;
				}
			}
			array = extendedErrorInfo.Split(new string[]
			{
				", data "
			}, StringSplitOptions.None);
			if (array.Length == 2)
			{
				string s2 = array[1].Trim();
				if (int.TryParse(s2, out num2))
				{
					if (num2 > 0)
					{
						num2 = 0;
					}
				}
				else
				{
					num2 = 0;
				}
			}
			string text2 = DirectoryStrings.ErrorADResponse(extendedErrorInfo);
			if (!string.IsNullOrEmpty(text))
			{
				text2 = DirectoryStrings.AppendLocalizedStrings(text, text2);
			}
			aderrorRecord.NativeError = num;
			aderrorRecord.Message = text2;
			aderrorRecord.JetError = num2;
			return aderrorRecord;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0004CEA4 File Offset: 0x0004B0A4
		internal bool IsResultCode(DirectoryException de, ResultCode resultCode)
		{
			DirectoryOperationException ex = de as DirectoryOperationException;
			return ex != null && ex.Response.ResultCode == resultCode;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0004CECC File Offset: 0x0004B0CC
		private static ADErrorRecord AnalyzeDirectoryError(DirectoryException de, bool isErrorFromNotification, bool isLocalSystemOrNetworkService, bool isBind)
		{
			DirectoryOperationException ex = de as DirectoryOperationException;
			LdapException ex2 = de as LdapException;
			string text = de.Message;
			LdapError ldapError;
			if (ex == null)
			{
				ldapError = (LdapError)ex2.ErrorCode;
			}
			else if (ex.Response == null)
			{
				ldapError = LdapError.ServerDown;
			}
			else
			{
				ldapError = (LdapError)ex.Response.ResultCode;
				text = ex.Response.ErrorMessage;
			}
			Microsoft.Exchange.Diagnostics.Trace directoryExceptionTracer = ExTraceGlobals.DirectoryExceptionTracer;
			long id = 0L;
			string formatString = "Caught {0} with {1}(0x{2}), message={3}";
			object[] array = new object[4];
			array[0] = de.GetType();
			array[1] = (int)ldapError;
			object[] array2 = array;
			int num = 2;
			int num2 = (int)ldapError;
			array2[num] = num2.ToString("X");
			array[3] = text;
			directoryExceptionTracer.TraceDebug(id, formatString, array);
			ADErrorRecord aderrorRecord = PooledLdapConnection.AnalyzeLdapError(ldapError, text, isErrorFromNotification, isLocalSystemOrNetworkService, isBind);
			aderrorRecord.InnerException = de;
			return aderrorRecord;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004CF7C File Offset: 0x0004B17C
		private static ActivityOperationType GetActivityOperation(LdapOperation value)
		{
			switch (value)
			{
			case LdapOperation.Read:
				return ActivityOperationType.ADRead;
			case LdapOperation.Search:
				return ActivityOperationType.ADSearch;
			case LdapOperation.Write:
				return ActivityOperationType.ADWrite;
			default:
				throw new ArgumentOutOfRangeException("value");
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0004CFB0 File Offset: 0x0004B1B0
		private static ADErrorRecord AnalyzeLdapError(LdapError ldapError, string message, bool isErrorFromNotification, bool isLocalSystemOrNetworkService, bool isBind)
		{
			bool isDownError = false;
			bool flag = false;
			bool isSearchError = false;
			bool isTimeoutError = false;
			HandlingType handlingType = HandlingType.Throw;
			ADErrorRecord aderrorRecord = PooledLdapConnection.AnalyzeExtendedErrorInfo(message);
			aderrorRecord.LdapError = ldapError;
			switch (ldapError)
			{
			case LdapError.OperationsError:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.RetryOnce;
				goto IL_3BB;
			case LdapError.ProtocolError:
				isDownError = true;
				flag = false;
				handlingType = HandlingType.Retry;
				goto IL_3BB;
			case LdapError.TimelimitExceeded:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.RetryOnce;
				aderrorRecord.IsServerSideTimeoutError = true;
				goto IL_3BB;
			case LdapError.SizelimitExceeded:
			case LdapError.CompareFalse:
			case LdapError.CompareTrue:
			case LdapError.StrongAuthRequired:
			case LdapError.ReferralV2:
			case LdapError.Referral:
			case LdapError.ConfidentialityRequired:
			case LdapError.SaslBindInProgress:
			case LdapError.NoSuchAttribute:
			case LdapError.UndefinedType:
			case LdapError.InappropriateMatching:
			case LdapError.ConstraintViolation:
			case LdapError.InvalidSyntax:
			case LdapError.NoSuchObject:
			case LdapError.AliasProblem:
			case LdapError.InvalidDnSyntax:
			case LdapError.IsLeaf:
			case LdapError.AliasDerefProblem:
			case LdapError.InappropriateAuth:
			case LdapError.InsufficientRights:
			case LdapError.UnwillingToPerform:
			case LdapError.LoopDetect:
			case LdapError.SortControlMissing:
			case LdapError.OffsetRangeError:
			case LdapError.NamingViolation:
			case LdapError.ObjectClassViolation:
			case LdapError.NotAllowedOnNonleaf:
			case LdapError.NotAllowedOnRdn:
			case LdapError.NoObjectClassMods:
			case LdapError.ResultsTooLarge:
			case LdapError.AffectsMultipleDsas:
			case LdapError.DecodingError:
			case LdapError.AuthUnknown:
			case LdapError.FilterError:
			case LdapError.UserCancelled:
			case LdapError.ParamError:
			case LdapError.NotSupported:
			case LdapError.ControlNotFound:
			case LdapError.MoreResultsToReturn:
			case LdapError.ClientLoop:
			case LdapError.ReferralLimitExceeded:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.Throw;
				isSearchError = true;
				goto IL_3BB;
			case LdapError.AuthMethodNotSupported:
			case LdapError.ConnectError:
			case LdapError.SendTimeOut:
				isDownError = true;
				flag = true;
				handlingType = HandlingType.Retry;
				goto IL_3BB;
			case LdapError.AdminLimitExceeded:
			{
				int jetError = aderrorRecord.JetError;
				if (jetError == -1112 || jetError == -1026)
				{
					isDownError = false;
					flag = false;
					handlingType = HandlingType.Throw;
					goto IL_3BB;
				}
				isDownError = false;
				flag = false;
				if (8397 == aderrorRecord.NativeError)
				{
					handlingType = HandlingType.Throw;
					goto IL_3BB;
				}
				handlingType = HandlingType.Retry;
				goto IL_3BB;
			}
			case LdapError.UnavailableCritExtension:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.RetryOnce;
				if (aderrorRecord.NativeError == 87)
				{
					aderrorRecord.Message = DirectoryStrings.AppendLocalizedStrings(DirectoryStrings.ErrorRejectedCookie.ToString(), aderrorRecord.Message);
					goto IL_3BB;
				}
				if (aderrorRecord.NativeError == 8431)
				{
					handlingType = HandlingType.Throw;
					goto IL_3BB;
				}
				goto IL_3BB;
			case (LdapError)15:
			case (LdapError)22:
			case (LdapError)23:
			case (LdapError)24:
			case (LdapError)25:
			case (LdapError)26:
			case (LdapError)27:
			case (LdapError)28:
			case (LdapError)29:
			case (LdapError)30:
			case (LdapError)31:
			case (LdapError)37:
			case (LdapError)38:
			case (LdapError)39:
			case (LdapError)40:
			case (LdapError)41:
			case (LdapError)42:
			case (LdapError)43:
			case (LdapError)44:
			case (LdapError)45:
			case (LdapError)46:
			case (LdapError)47:
			case (LdapError)55:
			case (LdapError)56:
			case (LdapError)57:
			case (LdapError)58:
			case (LdapError)59:
			case (LdapError)62:
			case (LdapError)63:
			case (LdapError)72:
			case (LdapError)73:
			case (LdapError)74:
			case (LdapError)75:
			case LdapError.VirtualListViewError:
			case (LdapError)77:
			case (LdapError)78:
			case (LdapError)79:
			case (LdapError)98:
			case (LdapError)99:
			case (LdapError)100:
			case (LdapError)101:
			case (LdapError)102:
			case (LdapError)103:
			case (LdapError)104:
			case (LdapError)105:
			case (LdapError)106:
			case (LdapError)107:
			case (LdapError)108:
			case (LdapError)109:
			case (LdapError)110:
			case (LdapError)111:
				goto IL_3BB;
			case LdapError.AttributeOrValueExists:
			case LdapError.AlreadyExists:
				flag = false;
				isDownError = false;
				handlingType = HandlingType.Throw;
				aderrorRecord.IsModificationError = true;
				goto IL_3BB;
			case LdapError.InvalidCredentials:
				flag = false;
				isDownError = false;
				handlingType = (isLocalSystemOrNetworkService ? HandlingType.Retry : HandlingType.Throw);
				goto IL_3BB;
			case LdapError.Busy:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.Retry;
				goto IL_3BB;
			case LdapError.Unavailable:
				if (aderrorRecord.NativeError == 8417)
				{
					isDownError = false;
					flag = false;
					handlingType = HandlingType.RetryOnce;
					goto IL_3BB;
				}
				break;
			case LdapError.Other:
			{
				int nativeError = aderrorRecord.NativeError;
				if (nativeError <= 8)
				{
					if (nativeError == 0)
					{
						isDownError = false;
						flag = true;
						handlingType = HandlingType.RetryOnce;
						goto IL_3BB;
					}
					if (nativeError != 8)
					{
						goto IL_2AC;
					}
				}
				else if (nativeError != 14 && nativeError != 170)
				{
					goto IL_2AC;
				}
				isDownError = true;
				flag = false;
				handlingType = HandlingType.Retry;
				goto IL_3BB;
				IL_2AC:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.Throw;
				goto IL_3BB;
			}
			case LdapError.ServerDown:
				break;
			case LdapError.LocalError:
				if (isBind)
				{
					isDownError = true;
					flag = true;
					handlingType = HandlingType.RetryOnce;
					goto IL_3BB;
				}
				isDownError = false;
				flag = true;
				handlingType = HandlingType.Retry;
				goto IL_3BB;
			case LdapError.EncodingError:
			case LdapError.NoMemory:
				isDownError = false;
				flag = true;
				handlingType = HandlingType.Retry;
				goto IL_3BB;
			case LdapError.Timeout:
				if (isErrorFromNotification)
				{
					isDownError = false;
					flag = true;
				}
				else
				{
					isDownError = true;
					flag = true;
				}
				handlingType = HandlingType.RetryOnce;
				isTimeoutError = true;
				goto IL_3BB;
			case LdapError.NoResultsReturned:
				isDownError = false;
				flag = false;
				handlingType = HandlingType.Throw;
				if (isErrorFromNotification)
				{
					ldapError = LdapError.Success;
					goto IL_3BB;
				}
				goto IL_3BB;
			default:
				goto IL_3BB;
			}
			isDownError = true;
			flag = false;
			handlingType = HandlingType.Retry;
			IL_3BB:
			aderrorRecord.HandlingType = handlingType;
			aderrorRecord.IsDownError = isDownError;
			aderrorRecord.IsFatalError = flag;
			aderrorRecord.IsSearchError = isSearchError;
			aderrorRecord.IsTimeoutError = isTimeoutError;
			return aderrorRecord;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0004D3A3 File Offset: 0x0004B5A3
		internal ADErrorRecord AnalyzeDirectoryError(DirectoryException de)
		{
			return this.AnalyzeDirectoryError(de, false);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0004D3B0 File Offset: 0x0004B5B0
		internal ADErrorRecord AnalyzeDirectoryError(DirectoryException de, bool usingOptimisticTimeout)
		{
			ADErrorRecord aderrorRecord = PooledLdapConnection.AnalyzeDirectoryError(de, this.IsNotify || usingOptimisticTimeout, this.isLocalSystemOrNetworkService, this.isBind);
			if (aderrorRecord.LdapError == LdapError.InvalidCredentials)
			{
				aderrorRecord.InnerException = this.GetInvalidCredentialException(aderrorRecord.InnerException);
			}
			if (aderrorRecord.IsSearchError)
			{
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateSearchFailures, UpdateType.Update, 1U);
			}
			if (aderrorRecord.IsTimeoutError)
			{
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateTimeouts, UpdateType.Update, 1U);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateTimeouts, UpdateType.Update, 1U);
			}
			if (aderrorRecord.IsServerSideTimeoutError)
			{
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateTimelimitExceeded, UpdateType.Update, 1U);
			}
			if (aderrorRecord.IsModificationError)
			{
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateModificationError, UpdateType.Update, 1U);
			}
			if (aderrorRecord.IsDownError && this.IsNotify)
			{
				aderrorRecord.IsDownError = false;
				aderrorRecord.IsFatalError = true;
				ExTraceGlobals.ADNotificationsTracer.TraceWarning((long)this.GetHashCode(), "Changing notify connection from DOWN to FATAL");
			}
			else if (aderrorRecord.IsDownError)
			{
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateDisconnects, UpdateType.Update, 1U);
				this.MarkDown(aderrorRecord.LdapError, aderrorRecord.Message);
			}
			else
			{
				this.UnMarkDown();
			}
			bool flag = aderrorRecord.IsTimeoutError && this.IsNotify;
			if (aderrorRecord.IsFatalError)
			{
				if (!flag)
				{
					ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateFatalErrors, UpdateType.Update, 1U);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_FATAL_ERROR, this.ServerName, new object[]
					{
						this.ServerName,
						((int)aderrorRecord.LdapError).ToString("X"),
						aderrorRecord.LdapError
					});
				}
				this.MarkFatal();
			}
			return aderrorRecord;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004D550 File Offset: 0x0004B750
		private Exception GetInvalidCredentialException(Exception innerException)
		{
			Exception result;
			if (string.IsNullOrEmpty(this.userIdentity))
			{
				result = new ADInvalidCredentialException(DirectoryStrings.ExceptionInvalidCredentialsFailedToGetIdentity(this.ServerName), innerException);
			}
			else if (this.isLocalSystemOrNetworkService)
			{
				result = new ADInvalidServiceCredentialException(DirectoryStrings.ExceptionInvalidCredentials(this.ServerName, this.userIdentity), innerException);
			}
			else
			{
				result = new ADInvalidCredentialException(DirectoryStrings.ExceptionInvalidCredentials(this.ServerName, this.userIdentity), innerException);
			}
			return result;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004D5BB File Offset: 0x0004B7BB
		internal void MarkFatal()
		{
			ExTraceGlobals.ConnectionTracer.TraceWarning<string>((long)this.GetHashCode(), "Marking LdapConnection to {0} as FATAL", this.ServerName);
			this.isFatalError = true;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0004D5E0 File Offset: 0x0004B7E0
		private void MarkDown(LdapError ldapError, string message)
		{
			if (!this.IsUp)
			{
				ExTraceGlobals.ConnectionDetailsTracer.TraceError<string>((long)this.GetHashCode(), "MarkDown: LdapConnection to {0} is already down", this.ServerName);
				return;
			}
			if (this.retryCount != 0)
			{
				ExTraceGlobals.ConnectionTracer.TraceWarning<string, int>((long)this.GetHashCode(), "MarkDown: LdapConnection to {0} failed to retry, error {1}", this.ServerName, (int)ldapError);
				this.isUp = false;
				return;
			}
			ExTraceGlobals.ConnectionTracer.TraceWarning<string, int, string>((long)this.GetHashCode(), "MarkDown: Marking DOWN LdapConnection to {0}, error {1}, message {2}", this.ServerName, (int)ldapError, message);
			ExEventLog.EventTuple tuple_DSC_EVENT_DC_DOWN = DirectoryEventLogConstants.Tuple_DSC_EVENT_DC_DOWN;
			string serverName = this.ServerName;
			object[] array = new object[4];
			array[0] = this.ServerName;
			object[] array2 = array;
			int num = 1;
			int num2 = (int)ldapError;
			array2[num] = num2.ToString("X");
			array[2] = message;
			array[3] = ldapError;
			Globals.LogEvent(tuple_DSC_EVENT_DC_DOWN, serverName, array);
			if (string.IsNullOrEmpty(this.ServerName))
			{
				return;
			}
			try
			{
				ADRunspaceServerSettingsProvider.GetInstance().ReportServerDown(this.serverInfo.PartitionFqdn, this.ServerName, this.role);
				TopologyProvider.GetInstance().ReportServerDown(this.serverInfo.PartitionFqdn, this.ServerName, this.role);
			}
			finally
			{
				Interlocked.Exchange(ref this.timeMarkedDown, Environment.TickCount);
				this.isUp = false;
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0004D718 File Offset: 0x0004B918
		private void UnMarkDown()
		{
			int num = this.retryCount;
			if (this.IsUp && num != 0 && num == Interlocked.CompareExchange(ref this.retryCount, 0, num))
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "UnMarkDown: Marking UP LdapConnection to {0}", this.ServerName);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DC_UP, this.ServerName, new object[]
				{
					this.ServerName,
					this.serverInfo.Port
				});
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0004D798 File Offset: 0x0004B998
		internal void RetryDownConnection()
		{
			if (this.IsUp)
			{
				return;
			}
			int num = this.timeMarkedDown;
			int num2 = this.retryCount;
			int tickCount = Environment.TickCount;
			if (tickCount - num < 30000 << num2)
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "RetryDownConnection: LdapConnection to {0} has been DOWN for {1} seconds, retried {2} times. Not retrying now.", this.ServerName, (tickCount - num) / 1000, num2);
				return;
			}
			if (num != Interlocked.CompareExchange(ref this.timeMarkedDown, Environment.TickCount, num))
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "RetryDownConnection: LdapConnection to {0} is being retried by another thread.", this.ServerName);
				return;
			}
			num2 = Interlocked.Increment(ref this.retryCount);
			if (5 <= num2)
			{
				ExTraceGlobals.ConnectionTracer.TraceWarning<string, int, int>((long)this.GetHashCode(), "RetryDownConnection: LdapConnection to {0} has been retried too many times ({1}). It has been down for {2} seconds.", this.ServerName, num2, (Environment.TickCount - num) / 1000);
				this.MarkFatal();
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateFatalErrors, UpdateType.Update, 1U);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_FATAL_ERROR, this.ServerName, new object[]
				{
					this.ServerName,
					"80070103",
					"ERROR_NO_MORE_ITEMS"
				});
				return;
			}
			ExTraceGlobals.ConnectionTracer.TraceDebug<string, int>((long)this.GetHashCode(), "RetryDownConnection:Retrying LdapConnection to {0}. Retry count is {1}.", this.ServerName, num2);
			this.isUp = true;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		internal PooledLdapConnection BorrowFromPool()
		{
			int arg = Interlocked.Increment(ref this.references);
			ExTraceGlobals.ConnectionDetailsTracer.TraceDebug<int>((long)this.GetHashCode(), "Connection borrowed from pool, count incremented to {0}", arg);
			return this;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0004D908 File Offset: 0x0004BB08
		internal void ReturnToPool()
		{
			int num = Interlocked.Decrement(ref this.references);
			ExTraceGlobals.ConnectionDetailsTracer.TraceDebug<int>((long)this.GetHashCode(), "Connection returned to pool, count decremented to {0}", num);
			if (num == 0)
			{
				base.Dispose();
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004D944 File Offset: 0x0004BB44
		internal void GetLoggingDataFromDirectoryRequest(DirectoryRequest request, out LocalizedString requestType, out string distinguishedName, out QueryScope scope, out string filter)
		{
			requestType = LocalizedString.Empty;
			distinguishedName = string.Empty;
			scope = QueryScope.Base;
			filter = string.Empty;
			AddRequest addRequest = request as AddRequest;
			ModifyDNRequest modifyDNRequest = request as ModifyDNRequest;
			ModifyRequest modifyRequest = request as ModifyRequest;
			DeleteRequest deleteRequest = request as DeleteRequest;
			SearchRequest searchRequest = request as SearchRequest;
			if (addRequest != null)
			{
				distinguishedName = addRequest.DistinguishedName;
				requestType = PooledLdapConnection.LdapAddLocalized;
				return;
			}
			if (modifyDNRequest != null)
			{
				distinguishedName = modifyDNRequest.DistinguishedName;
				requestType = PooledLdapConnection.LdapModifyDNLocalized;
				return;
			}
			if (modifyRequest != null)
			{
				distinguishedName = modifyRequest.DistinguishedName;
				requestType = PooledLdapConnection.LdapModifyLocalized;
				return;
			}
			if (deleteRequest != null)
			{
				distinguishedName = deleteRequest.DistinguishedName;
				requestType = PooledLdapConnection.LdapDeleteLocalized;
				return;
			}
			if (searchRequest != null)
			{
				distinguishedName = searchRequest.DistinguishedName;
				requestType = PooledLdapConnection.LdapSearchLocalized;
				filter = (string)searchRequest.Filter;
				scope = (QueryScope)searchRequest.Scope;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004DA20 File Offset: 0x0004BC20
		internal DirectoryResponse SendRequest(DirectoryRequest request, LdapOperation ldapOperation, TimeSpan? clientSideSearchTimeout, IActivityScope activityScope = null, string callerInfo = null)
		{
			DirectoryResponse directoryResponse = null;
			float num = 0f;
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool flag = false;
			Interlocked.Increment(ref this.outstandingRequestCount);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessOutstandingRequests, UpdateType.Add, 1U);
			ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCOutstandingRequests, UpdateType.Add, 1U);
			SearchRequest searchRequest = request as SearchRequest;
			LocalizedString empty = LocalizedString.Empty;
			string empty2 = string.Empty;
			QueryScope queryScope = QueryScope.Base;
			string empty3 = string.Empty;
			string failure = string.Empty;
			SearchStats searchStats = null;
			try
			{
				this.InitializeStatisticTracing(request);
				try
				{
					if (!this.isSetupContext && ExEnvironment.IsTest && ((Globals.TestPassTypeValue.StartsWith("private", StringComparison.InvariantCultureIgnoreCase) && Globals.IsVirtualMachine) || Globals.ForceLdapLatency))
					{
						Thread.Sleep(70);
					}
					this.GetLoggingDataFromDirectoryRequest(request, out empty, out empty2, out queryScope, out empty3);
					if (!string.IsNullOrEmpty(empty2) && empty != DirectoryStrings.LdapSearch)
					{
						GenericSettingsContext genericSettingsContext = new GenericSettingsContext("ForestFqdn", this.serverInfo.PartitionFqdn, null);
						using (genericSettingsContext.Activate())
						{
							if (ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("IsADWriteDisabled"))
							{
								throw new ADWriteDisabledException(DirectoryStrings.ExceptionADWriteDisabled(Globals.ProcessName, this.serverInfo.PartitionFqdn));
							}
							int config = ConfigBase<AdDriverConfigSchema>.GetConfig<int>("DelayForADWriteThrottlingInMsec");
							if (config > 0)
							{
								Thread.Sleep(config);
							}
						}
					}
					directoryResponse = base.SendRequest(request, clientSideSearchTimeout ?? ConnectionPoolManager.ClientSideSearchTimeout);
					ExTraceGlobals.FaultInjectionTracer.TraceTest(3139841341U);
					flag = true;
				}
				catch (SEHException innerException)
				{
					throw new ADExternalException(DirectoryStrings.ExceptionExternalError, innerException);
				}
				catch (Exception ex)
				{
					failure = ex.Message;
					throw;
				}
				finally
				{
					stopwatch.Stop();
					num = (float)stopwatch.Elapsed.TotalMilliseconds;
					ActivityContext.AddOperation(PooledLdapConnection.GetActivityOperation(ldapOperation), this.UnqualifiedServerName, num, 1);
					if (flag && directoryResponse != null && (directoryResponse.Referral == null || directoryResponse.Referral.Length == 0))
					{
						PerformanceContext.UpdateContext(1U, (int)num);
					}
				}
				if (directoryResponse.Referral != null && directoryResponse.Referral.Length > 0 && ldapOperation != LdapOperation.Write)
				{
					string dnsSafeHost = directoryResponse.Referral[0].DnsSafeHost;
					throw new ADReferralException(DirectoryStrings.ExceptionReferral(this.ServerName, dnsSafeHost, (searchRequest == null) ? "<null>" : searchRequest.DistinguishedName, searchRequest.Filter.ToString()));
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.outstandingRequestCount);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessOutstandingRequests, UpdateType.Subtract, 1U);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCOutstandingRequests, UpdateType.Subtract, 1U);
				if (directoryResponse != null)
				{
					this.FinalizeStatisticTracing(directoryResponse, (int)num, out searchStats);
				}
				Guid activityId = Guid.Empty;
				string userEmail = null;
				if (activityScope != null && activityScope.Status == ActivityContextStatus.ActivityStarted)
				{
					activityId = activityScope.ActivityId;
					userEmail = activityScope.UserEmail;
				}
				ADProtocolLog.Instance.Append(empty, empty2, empty3, queryScope.ToString(), this.ADServerInfo.Fqdn, this.ADServerInfo.Port.ToString(), (directoryResponse != null) ? directoryResponse.ResultCode.ToString() : "Exception", (long)((int)num), failure, (searchStats != null) ? searchStats.CallTime : -1, (searchStats != null) ? searchStats.EntriesVisited : 0, (searchStats != null) ? searchStats.EntriesReturned : 0, activityId, userEmail, null, callerInfo);
			}
			this.UnMarkDown();
			if (num >= (float)(ConnectionPoolManager.LongRunningOperationThresholdSeconds * 1000))
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_LONG_RUNNING_OPERATION, null, new object[]
				{
					empty,
					(int)num,
					this.ServerName,
					empty2,
					empty3,
					queryScope
				});
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateLongRunningOperations, UpdateType.Update, 1U);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateLongRunningOperations, UpdateType.Update, 1U);
			}
			switch (ldapOperation)
			{
			case LdapOperation.Read:
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeRead, UpdateType.Add, (uint)num);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeReadBase, UpdateType.Add, 1U);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateRead, UpdateType.Add, 1U);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCTimeRead, UpdateType.Add, (uint)num);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCTimeReadBase, UpdateType.Add, 1U);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateRead, UpdateType.Add, 1U);
				break;
			case LdapOperation.Search:
			{
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearch, UpdateType.Add, (uint)num);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchBase, UpdateType.Add, 1U);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateSearch, UpdateType.Add, 1U);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCTimeSearch, UpdateType.Add, (uint)num);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCTimeSearchBase, UpdateType.Add, 1U);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateSearch, UpdateType.Add, 1U);
				ADProviderPerf.UpdateProcessTimeSearchPercentileCounter((uint)num);
				uint value = 0U;
				uint value2 = 0U;
				if (searchStats != null)
				{
					value = (uint)((searchStats.EntriesReturned != 0) ? (searchStats.EntriesVisited / searchStats.EntriesReturned) : searchStats.EntriesVisited);
					value2 = (uint)searchStats.CallTime;
				}
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessCostSearch, UpdateType.Add, value);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessCostSearchBase, UpdateType.Add, 1U);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchOnDC, UpdateType.Add, value2);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchOnDCBase, UpdateType.Add, 1U);
				break;
			}
			case LdapOperation.Write:
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeWrite, UpdateType.Add, (uint)num);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeWriteBase, UpdateType.Add, 1U);
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateWrite, UpdateType.Add, 1U);
				break;
			}
			return directoryResponse;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0004DF9C File Offset: 0x0004C19C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (Environment.HasShutdownStarted)
			{
				return;
			}
			if (this.serverInfo != null)
			{
				Interlocked.Decrement(ref PooledLdapConnection.totalCount);
				if (this.serverInfo.Port == 3268)
				{
					ADProviderPerf.UpdateProcessCounter(Counter.ProcessOpenConnectionsGC, UpdateType.Subtract, 1U);
				}
				else
				{
					ADProviderPerf.UpdateProcessCounter(Counter.ProcessOpenConnectionsDC, UpdateType.Subtract, 1U);
				}
				uint num = (uint)(Environment.TickCount - this.timeCreated);
				ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCAvgTimeConnection, UpdateType.Update, num / 1000U);
				LdapDirectoryIdentifier ldapDirectoryIdentifier = this.Directory as LdapDirectoryIdentifier;
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Disposing pooled connection to {0}", (ldapDirectoryIdentifier != null && ldapDirectoryIdentifier.Servers != null && ldapDirectoryIdentifier.Servers.Length > 0) ? ldapDirectoryIdentifier.Servers[0] : "<null>");
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_CONNECTION_CLOSED, null, new object[]
				{
					this.ServerName,
					this.serverInfo.Port
				});
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004E090 File Offset: 0x0004C290
		internal void BindWithLogging()
		{
			bool flag = false;
			string failure = string.Empty;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				base.Bind();
				flag = true;
			}
			catch (Exception ex)
			{
				failure = ex.Message;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				float num = (float)stopwatch.Elapsed.TotalMilliseconds;
				ADProtocolLog.Instance.Append("Bind", string.Empty, string.Empty, string.Empty, this.ADServerInfo.Fqdn, this.ADServerInfo.Port.ToString(), flag ? "Success" : "Exception", (long)((int)num), failure, -1, 0, 0, Guid.Empty, null, null, null);
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004E158 File Offset: 0x0004C358
		internal void BindWithRetry(int maxRetries)
		{
			int num = 0;
			for (;;)
			{
				if (string.IsNullOrEmpty(this.ServerName))
				{
					ExTraceGlobals.ConnectionTracer.TraceWarning<string>((long)this.GetHashCode(), "{0} Adding ForceRediscovery flag.", this.serverInfo.PartitionFqdn);
					base.SessionOptions.LocatorFlag = (base.SessionOptions.LocatorFlag | LocatorFlags.ForceRediscovery);
				}
				try
				{
					try
					{
						this.isBind = true;
						this.BindWithLogging();
						ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), " LdapConnection to {0} is UP", this.ServerName);
						break;
					}
					catch (DirectoryException ex)
					{
						ExTraceGlobals.ConnectionTracer.TraceWarning<Type, string>((long)this.GetHashCode(), "Caught {0} with message={1}", ex.GetType(), ex.Message);
						ADProviderPerf.UpdateDCCounter(this.ServerName, Counter.DCRateBindFailures, UpdateType.Update, 1U);
						ADErrorRecord aderrorRecord = this.AnalyzeDirectoryError(ex);
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_BIND_FAILED, "BindFailedTo" + this.ServerName, new object[]
						{
							this.ServerName,
							aderrorRecord.LdapError,
							this.serverInfo.Port,
							aderrorRecord.Message
						});
						if (aderrorRecord.HandlingType == HandlingType.Throw)
						{
							if (aderrorRecord.LdapError == LdapError.InvalidCredentials)
							{
								throw aderrorRecord.InnerException;
							}
							throw new ADOperationException(DirectoryStrings.ExceptionOneTimeBindFailed(this.ServerName, aderrorRecord.Message), aderrorRecord.InnerException);
						}
						else if (++num >= maxRetries)
						{
							LocalizedException ex2 = new ADTransientException(DirectoryStrings.ExceptionOneTimeBindFailed(this.ServerName, aderrorRecord.Message), aderrorRecord.InnerException);
							if (TopologyMode.Ldap == TopologyProvider.CurrentTopologyMode)
							{
								ex2.Data.Add(TopologyMode.Ldap.ToString(), aderrorRecord);
							}
							throw ex2;
						}
					}
					continue;
				}
				finally
				{
					this.isBind = false;
				}
				break;
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0004E338 File Offset: 0x0004C538
		internal bool TryBindWithRetry(int maxRetries)
		{
			ADErrorRecord aderrorRecord;
			return this.TryBindWithRetry(maxRetries, out aderrorRecord);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0004E350 File Offset: 0x0004C550
		internal bool TryBindWithRetry(int maxRetries, out ADErrorRecord errorRecord)
		{
			int num = 0;
			bool result;
			for (;;)
			{
				bool flag = this.isNotify;
				this.isNotify = false;
				this.isBind = true;
				try
				{
					try
					{
						this.BindWithLogging();
						ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), " LdapConnection to {0} is UP", this.ServerName);
						errorRecord = null;
						result = true;
						break;
					}
					catch (DirectoryException ex)
					{
						ExTraceGlobals.ConnectionTracer.TraceWarning<Type, string>((long)this.GetHashCode(), "Caught {0} with message={1}", ex.GetType(), ex.Message);
						errorRecord = this.AnalyzeDirectoryError(ex);
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_BIND_FAILED, "BindFailedTo" + this.ServerName, new object[]
						{
							this.ServerName,
							errorRecord.LdapError,
							this.serverInfo.Port,
							errorRecord.Message
						});
						if (++num >= maxRetries)
						{
							result = false;
							break;
						}
					}
					continue;
				}
				finally
				{
					this.isNotify = flag;
					this.isBind = false;
				}
				break;
			}
			return result;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004E46C File Offset: 0x0004C66C
		private void InitializeStatisticTracing(DirectoryRequest request)
		{
			if (request is SearchRequest && SearchStatsControl.FindSearchStatsControl(request) == null)
			{
				request.Controls.Add(new SearchStatsControl());
			}
			if (!ExTraceGlobals.ADPerformanceTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			if (ConnectionPoolManager.CallstackTraceRatio > 0)
			{
				int totalRequestCount = this.ADServerInfo.TotalRequestCount;
				if (totalRequestCount % ConnectionPoolManager.CallstackTraceRatio == 0)
				{
					StackTrace stackTrace = new StackTrace(false);
					string text = stackTrace.ToString().Replace("\r\n   ", "|");
					int num;
					for (int i = 0; i < text.Length; i += num)
					{
						num = Math.Min(3800, text.Length - i);
						ExTraceGlobals.ADPerformanceTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Callstack({0}):{1}", totalRequestCount, text.Substring(i, num));
					}
				}
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004E52C File Offset: 0x0004C72C
		private void FinalizeStatisticTracing(DirectoryResponse response, int ticksElapsed, out SearchStats searchStats)
		{
			searchStats = null;
			SearchStatsControl searchStatsControl = SearchStatsControl.FindSearchStatsControl(response);
			if (searchStatsControl == null)
			{
				return;
			}
			searchStats = SearchStats.Parse(searchStatsControl.GetValue());
			if (searchStats == null)
			{
				ExTraceGlobals.ADPerformanceTracer.TraceDebug<int>((long)this.GetHashCode(), "Last operation took {0} milliseconds. No statistic information available", ticksElapsed);
				return;
			}
			ExTraceGlobals.ADPerformanceTracer.TraceDebug((long)this.GetHashCode(), "Last operation took {0} milliseconds, visited:{1}, returned:{2}, index:{3}, filter from AD:{4}", new object[]
			{
				ticksElapsed,
				searchStats.EntriesVisited,
				searchStats.EntriesReturned,
				searchStats.Index,
				searchStats.Filter
			});
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004E5CC File Offset: 0x0004C7CC
		private bool TryGetNamingContexts(ref string configNC, ref string defaultNC, ref string rootNC, ref string schemaNC, ref string serverNameDN, out ADErrorRecord errorMessage)
		{
			SearchResponse searchResponse = null;
			try
			{
				SearchRequest request = new SearchRequest(string.Empty, "(objectclass=*)", SearchScope.Base, new string[]
				{
					"configurationNamingContext",
					"defaultNamingContext",
					"rootDomainNamingContext",
					"schemaNamingContext",
					"serverName"
				});
				searchResponse = (SearchResponse)this.SendRequest(request, LdapOperation.Search, null, null, null);
				errorMessage = null;
			}
			catch (DirectoryException ex)
			{
				errorMessage = this.AnalyzeDirectoryError(ex);
				ExTraceGlobals.ConnectionTracer.TraceWarning<Type, LdapError, string>((long)this.GetHashCode(), "GetNamingContexts: Caught {0} with error={1}, message={2}", ex.GetType(), errorMessage.LdapError, errorMessage.Message);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_READ_ROOTDSE_FAILED, "ReadRootDSEFailedOn" + this.ServerName, new object[]
				{
					this.ServerName,
					errorMessage.LdapError,
					this.serverInfo.Port,
					errorMessage.Message
				});
				return false;
			}
			if (searchResponse.Entries.Count > 0)
			{
				SearchResultEntry searchResultEntry = searchResponse.Entries[0];
				if (searchResultEntry.Attributes.Contains("configurationNamingContext"))
				{
					configNC = (string)searchResultEntry.Attributes["configurationNamingContext"].GetValues(typeof(string))[0];
				}
				if (searchResultEntry.Attributes.Contains("defaultNamingContext"))
				{
					defaultNC = (string)searchResultEntry.Attributes["defaultNamingContext"].GetValues(typeof(string))[0];
				}
				if (searchResultEntry.Attributes.Contains("rootDomainNamingContext"))
				{
					rootNC = (string)searchResultEntry.Attributes["rootDomainNamingContext"].GetValues(typeof(string))[0];
				}
				if (searchResultEntry.Attributes.Contains("schemaNamingContext"))
				{
					schemaNC = (string)searchResultEntry.Attributes["schemaNamingContext"].GetValues(typeof(string))[0];
				}
				if (searchResultEntry.Attributes.Contains("serverName"))
				{
					serverNameDN = (string)searchResultEntry.Attributes["serverName"].GetValues(typeof(string))[0];
				}
			}
			return true;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004E83C File Offset: 0x0004CA3C
		internal void SetNamingContexts()
		{
			ADErrorRecord aderrorRecord;
			if (!this.TrySetNamingContexts(out aderrorRecord))
			{
				throw new ADTransientException(DirectoryStrings.ExceptionReadingRootDSE(this.ServerName ?? "<null>", aderrorRecord.Message));
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004E874 File Offset: 0x0004CA74
		internal bool TrySetNamingContexts(out ADErrorRecord errorMessage)
		{
			if (string.IsNullOrEmpty(this.ADServerInfo.WritableNC) || string.IsNullOrEmpty(this.ADServerInfo.ConfigNC) || string.IsNullOrEmpty(this.ADServerInfo.RootDomainNC) || string.IsNullOrEmpty(this.ADServerInfo.SchemaNC) || string.IsNullOrEmpty(this.ADServerInfo.SiteName))
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				if (!this.TryGetNamingContexts(ref text, ref text2, ref text3, ref text4, ref text5, out errorMessage))
				{
					return false;
				}
				if (text2 != null && string.IsNullOrEmpty(this.ADServerInfo.WritableNC))
				{
					this.ADServerInfo.WritableNC = text2;
				}
				if (text != null && string.IsNullOrEmpty(this.ADServerInfo.ConfigNC))
				{
					this.ADServerInfo.ConfigNC = text;
				}
				if (text3 != null && string.IsNullOrEmpty(this.ADServerInfo.RootDomainNC))
				{
					this.ADServerInfo.RootDomainNC = text3;
				}
				if (text4 != null && string.IsNullOrEmpty(this.ADServerInfo.SchemaNC))
				{
					this.ADServerInfo.SchemaNC = text4;
				}
				if (!string.IsNullOrEmpty(text5) && string.IsNullOrEmpty(this.ADServerInfo.SiteName))
				{
					ADObjectId adobjectId = new ADObjectId(text5);
					if (adobjectId.Parent != null && adobjectId.Parent.Parent != null)
					{
						string name = adobjectId.Parent.Parent.Name;
						this.ADServerInfo.SiteName = name;
					}
				}
				ExTraceGlobals.ConnectionTracer.TraceDebug((long)this.GetHashCode(), "PooledLdapConnection::SetNamingContexts - WritableNC={0} ConfigNC={1} RootDomainNC={2} SchemaNC={3} SiteName={4}", new object[]
				{
					this.ADServerInfo.WritableNC,
					this.ADServerInfo.ConfigNC,
					this.ADServerInfo.RootDomainNC,
					this.ADServerInfo.SchemaNC,
					this.ADServerInfo.SiteName
				});
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004EA4C File Offset: 0x0004CC4C
		internal bool IsSetupContext()
		{
			ADDriverContext processADContext = ADSessionSettings.GetProcessADContext();
			return processADContext != null && (processADContext.Mode == ContextMode.Setup || processADContext.Mode == ContextMode.Test);
		}

		// Token: 0x04000932 RID: 2354
		private const int MaxRetryCount = 5;

		// Token: 0x04000933 RID: 2355
		private const int RetryInterval = 30000;

		// Token: 0x04000934 RID: 2356
		internal const int DefaultDCPort = 389;

		// Token: 0x04000935 RID: 2357
		internal const int DefaultGCPort = 3268;

		// Token: 0x04000936 RID: 2358
		internal const int DefaultDnsWeight = 100;

		// Token: 0x04000937 RID: 2359
		internal static readonly LocalizedString LdapAddLocalized = DirectoryStrings.LdapAdd;

		// Token: 0x04000938 RID: 2360
		internal static readonly LocalizedString LdapModifyDNLocalized = DirectoryStrings.LdapModifyDN;

		// Token: 0x04000939 RID: 2361
		internal static readonly LocalizedString LdapModifyLocalized = DirectoryStrings.LdapModify;

		// Token: 0x0400093A RID: 2362
		internal static readonly LocalizedString LdapDeleteLocalized = DirectoryStrings.LdapDelete;

		// Token: 0x0400093B RID: 2363
		internal static readonly LocalizedString LdapSearchLocalized = DirectoryStrings.LdapSearch;

		// Token: 0x0400093C RID: 2364
		private static int totalCount;

		// Token: 0x0400093D RID: 2365
		private bool isNotify;

		// Token: 0x0400093E RID: 2366
		private ADServerRole role;

		// Token: 0x0400093F RID: 2367
		private bool isUp;

		// Token: 0x04000940 RID: 2368
		private bool isBind;

		// Token: 0x04000941 RID: 2369
		private bool isFatalError;

		// Token: 0x04000942 RID: 2370
		private ADServerInfo serverInfo;

		// Token: 0x04000943 RID: 2371
		private string unqualifiedServerName;

		// Token: 0x04000944 RID: 2372
		private int references;

		// Token: 0x04000945 RID: 2373
		private int retryCount;

		// Token: 0x04000946 RID: 2374
		private int timeCreated;

		// Token: 0x04000947 RID: 2375
		private int timeMarkedDown;

		// Token: 0x04000948 RID: 2376
		private int outstandingRequestCount;

		// Token: 0x04000949 RID: 2377
		private string userIdentity;

		// Token: 0x0400094A RID: 2378
		private bool isLocalSystemOrNetworkService;

		// Token: 0x0400094B RID: 2379
		private bool isSetupContext;
	}
}
