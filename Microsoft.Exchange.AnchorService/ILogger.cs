using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILogger : IDisposeTrackable, IDisposable
	{
		// Token: 0x060000C9 RID: 201
		void LogEvent(MigrationEventType eventType, params string[] args);

		// Token: 0x060000CA RID: 202
		void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args);

		// Token: 0x060000CB RID: 203
		void LogEvent(MigrationEventType eventType, Exception ex, params string[] args);

		// Token: 0x060000CC RID: 204
		void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args);

		// Token: 0x060000CD RID: 205
		void LogTerseEvent(MigrationEventType eventType, params string[] args);

		// Token: 0x060000CE RID: 206
		void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args);

		// Token: 0x060000CF RID: 207
		void LogTerseEvent(MigrationEventType eventType, Exception ex, params string[] args);

		// Token: 0x060000D0 RID: 208
		void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args);

		// Token: 0x060000D1 RID: 209
		void LogError(Exception exception, string formatString, params object[] formatArgs);

		// Token: 0x060000D2 RID: 210
		void LogVerbose(string formatString, params object[] formatArgs);

		// Token: 0x060000D3 RID: 211
		void LogWarning(string formatString, params object[] formatArgs);

		// Token: 0x060000D4 RID: 212
		void LogInformation(string formatString, params object[] formatArgs);

		// Token: 0x060000D5 RID: 213
		void Log(MigrationEventType eventType, Exception exception, string format, params object[] args);

		// Token: 0x060000D6 RID: 214
		void Log(MigrationEventType eventType, string format, params object[] args);

		// Token: 0x060000D7 RID: 215
		void Log(string source, MigrationEventType eventType, object context, string format, params object[] args);
	}
}
