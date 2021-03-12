using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000041 RID: 65
	internal static class ProtocolLog
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00011997 File Offset: 0x0000FB97
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0001199E File Offset: 0x0000FB9E
		internal static bool Enabled { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000119A6 File Offset: 0x0000FBA6
		internal static string DefaultLogFilePath
		{
			get
			{
				return ProtocolLog.defaultLogFilePath;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000119AD File Offset: 0x0000FBAD
		internal static LogSchema Schema
		{
			get
			{
				if (ProtocolLog.schema == null)
				{
					ProtocolLog.schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", ProtocolLog.defaultLogTypeName, ProtocolLog.GetColumnArray());
				}
				return ProtocolLog.schema;
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000119DC File Offset: 0x0000FBDC
		internal static void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriond, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision)
		{
			ProtocolLog.log = new Log(ProtocolLog.defaultLogFilePrefix, new LogHeaderFormatter(ProtocolLog.Schema), ProtocolLog.defaultLogComponent);
			ProtocolLog.log.Configure(logFilePath, maxRetentionPeriond, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision);
			ProtocolLog.Enabled = true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011A29 File Offset: 0x0000FC29
		internal static void SetDefaults(string logFilePath, string logTypeName, string logFilePrefix, string logComponent)
		{
			ProtocolLog.defaultLogFilePath = logFilePath;
			ProtocolLog.defaultLogTypeName = logTypeName;
			ProtocolLog.defaultLogFilePrefix = logFilePrefix;
			ProtocolLog.defaultLogComponent = logComponent;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011A43 File Offset: 0x0000FC43
		internal static void Shutdown()
		{
			if (ProtocolLog.log != null)
			{
				ProtocolLog.log.Close();
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011A58 File Offset: 0x0000FC58
		internal static ProtocolLogSession CreateSession(int sessionId, string clientAddress, string serverAddress, string protocolSequence)
		{
			ProtocolLogSession protocolLogSession = new ProtocolLogSession(new LogRowFormatter(ProtocolLog.Schema));
			protocolLogSession[ProtocolLog.Field.SessionId] = sessionId;
			if (!string.IsNullOrEmpty(clientAddress))
			{
				protocolLogSession[ProtocolLog.Field.ClientIp] = clientAddress;
			}
			if (!string.IsNullOrEmpty(serverAddress))
			{
				protocolLogSession[ProtocolLog.Field.ServerIp] = string.Intern(serverAddress);
			}
			if (!string.IsNullOrEmpty(protocolSequence))
			{
				protocolLogSession[ProtocolLog.Field.Protocol] = string.Intern(protocolSequence);
			}
			return protocolLogSession;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011ABD File Offset: 0x0000FCBD
		internal static void Append(LogRowFormatter row)
		{
			if (ProtocolLog.Enabled)
			{
				ProtocolLog.log.Append(row, 0);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		internal static void LogProtocolFailure(string operation, IList<string> requestIds, IList<string> cookies, string message, string userName, string protocolSequence, string clientAddress, string organization, Exception exception)
		{
			ProtocolLogSession protocolLogSession = ProtocolLog.CreateSession(0, clientAddress, null, protocolSequence);
			if (!string.IsNullOrEmpty(organization))
			{
				protocolLogSession[ProtocolLog.Field.OrganizationInfo] = organization;
			}
			string text = string.Empty;
			if (cookies != null && cookies.Count > 0)
			{
				text = string.Join("|", cookies);
			}
			string text2 = string.Empty;
			if (requestIds != null && requestIds.Count > 0)
			{
				text2 = string.Join("|", requestIds);
			}
			string arg = string.Empty;
			if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
			{
				arg = string.Format(" [{0}{1}{2}]", text, string.IsNullOrEmpty(text) ? string.Empty : ";", text2);
			}
			string arg2 = string.Empty;
			if (!string.IsNullOrEmpty(userName))
			{
				arg2 = string.Format(" [{0}]", userName);
			}
			protocolLogSession.AppendProtocolFailure(operation, string.Format("{0}{1}{2}", message, arg2, arg), exception.LogMessage(true));
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011BAD File Offset: 0x0000FDAD
		internal static string LogMessage(this Exception exception, bool wantDetails = true)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (wantDetails)
			{
				return exception.ToString();
			}
			return exception.GetType().ToString() + ": " + exception.Message;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		private static string[] GetColumnArray()
		{
			string[] array = new string[ProtocolLog.Fields.Length];
			for (int i = 0; i < ProtocolLog.Fields.Length; i++)
			{
				array[i] = ProtocolLog.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x04000189 RID: 393
		private const string LogTypeName = "AddressBook Protocol Logs";

		// Token: 0x0400018A RID: 394
		private const string LogFilePrefix = "AddressBook_";

		// Token: 0x0400018B RID: 395
		private const string LogComponent = "AddressBookProtocolLogs";

		// Token: 0x0400018C RID: 396
		internal static readonly ProtocolLog.FieldInfo[] Fields = new ProtocolLog.FieldInfo[]
		{
			new ProtocolLog.FieldInfo(ProtocolLog.Field.DateTime, "date-time"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.SessionId, "session-id"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.SequenceNumber, "seq-number"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientName, "client-name"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.OrganizationInfo, "organization-info"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientIp, "client-ip"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ServerIp, "server-ip"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Protocol, "protocol"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Operation, "operation"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.RpcStatus, "rpc-status"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ProcessingTime, "processing-time"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.OperationSpecific, "operation-specific"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Failures, "failures"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Authentication, "authentication"),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Delay, "delay")
		};

		// Token: 0x0400018D RID: 397
		private static LogSchema schema;

		// Token: 0x0400018E RID: 398
		private static Log log;

		// Token: 0x0400018F RID: 399
		private static string defaultLogFilePath = string.Format("{0}Logging\\AddressBook Service\\", ExchangeSetupContext.InstallPath);

		// Token: 0x04000190 RID: 400
		private static string defaultLogTypeName = "AddressBook Protocol Logs";

		// Token: 0x04000191 RID: 401
		private static string defaultLogFilePrefix = "AddressBook_";

		// Token: 0x04000192 RID: 402
		private static string defaultLogComponent = "AddressBookProtocolLogs";

		// Token: 0x02000042 RID: 66
		internal enum Field
		{
			// Token: 0x04000195 RID: 405
			DateTime,
			// Token: 0x04000196 RID: 406
			SessionId,
			// Token: 0x04000197 RID: 407
			SequenceNumber,
			// Token: 0x04000198 RID: 408
			ClientName,
			// Token: 0x04000199 RID: 409
			OrganizationInfo,
			// Token: 0x0400019A RID: 410
			ClientIp,
			// Token: 0x0400019B RID: 411
			ServerIp,
			// Token: 0x0400019C RID: 412
			Protocol,
			// Token: 0x0400019D RID: 413
			Operation,
			// Token: 0x0400019E RID: 414
			RpcStatus,
			// Token: 0x0400019F RID: 415
			ProcessingTime,
			// Token: 0x040001A0 RID: 416
			OperationSpecific,
			// Token: 0x040001A1 RID: 417
			Failures,
			// Token: 0x040001A2 RID: 418
			Authentication,
			// Token: 0x040001A3 RID: 419
			Delay
		}

		// Token: 0x02000043 RID: 67
		internal struct FieldInfo
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x00011DDA File Offset: 0x0000FFDA
			public FieldInfo(ProtocolLog.Field field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x040001A4 RID: 420
			internal readonly ProtocolLog.Field Field;

			// Token: 0x040001A5 RID: 421
			internal readonly string ColumnName;
		}
	}
}
