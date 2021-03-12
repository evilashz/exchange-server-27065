using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001B6 RID: 438
	internal class FailureObjectLog
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0002C594 File Offset: 0x0002A794
		public FailureObjectLog(ObjectLogConfiguration logConfig)
		{
			this.failureLogger = new ObjectLog<FailureObjectLog.FailureObjectLogEntry>(new FailureObjectLog.FailureObjectLogSchema(), logConfig);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002C5AD File Offset: 0x0002A7AD
		public static LogSchema GetLogSchema()
		{
			return ObjectLog<FailureObjectLog.FailureObjectLogEntry>.GetLogSchema(new FailureObjectLog.FailureObjectLogSchema());
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002C5BC File Offset: 0x0002A7BC
		public void LogFailureEvent(IFailureObjectLoggable failureObject, Exception failureException)
		{
			if (failureException != null && failureObject != null)
			{
				FailureObjectLog.FailureObjectLogEntry failureObjectLogEntry = new FailureObjectLog.FailureObjectLogEntry(failureObject, this.ExtractExceptionString(failureException), this.ComputeFailureHash(failureException), failureException.GetType().Name, default(Guid), 0);
				this.failureLogger.LogObject(failureObjectLogEntry);
				while (failureException.InnerException != null)
				{
					failureException = failureException.InnerException;
					failureObjectLogEntry = new FailureObjectLog.FailureObjectLogEntry(failureObject, this.ExtractExceptionString(failureException), this.ComputeFailureHash(failureException), failureException.GetType().Name, failureObjectLogEntry.FailureGuid, failureObjectLogEntry.FailureLevel + 1);
					this.failureLogger.LogObject(failureObjectLogEntry);
				}
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002C658 File Offset: 0x0002A858
		public virtual string ComputeFailureHash(Exception failureException)
		{
			string result;
			WatsonExceptionReport.TryStringHashFromStackTrace(failureException, false, out result);
			return result;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002C670 File Offset: 0x0002A870
		public virtual string ExtractExceptionString(Exception failureException)
		{
			return failureException.ToString();
		}

		// Token: 0x04000907 RID: 2311
		private ObjectLog<FailureObjectLog.FailureObjectLogEntry> failureLogger;

		// Token: 0x020001B7 RID: 439
		private class FailureObjectLogEntry
		{
			// Token: 0x06000C1C RID: 3100 RVA: 0x0002C678 File Offset: 0x0002A878
			public FailureObjectLogEntry(IFailureObjectLoggable failureObject, string ex, string failureHash, string exType, Guid failureGuid = default(Guid), int failureLevel = 0)
			{
				this.FailureObject = failureObject;
				this.FailureException = ex;
				this.FailureExceptionHash = failureHash;
				this.FailureExceptionType = exType;
				this.FailureGuid = ((failureGuid == default(Guid)) ? Guid.NewGuid() : failureGuid);
				this.FailureLevel = failureLevel;
			}

			// Token: 0x04000908 RID: 2312
			public readonly IFailureObjectLoggable FailureObject;

			// Token: 0x04000909 RID: 2313
			public readonly Guid FailureGuid;

			// Token: 0x0400090A RID: 2314
			public readonly int FailureLevel;

			// Token: 0x0400090B RID: 2315
			public readonly string FailureException;

			// Token: 0x0400090C RID: 2316
			public readonly string FailureExceptionHash;

			// Token: 0x0400090D RID: 2317
			public readonly string FailureExceptionType;
		}

		// Token: 0x020001B9 RID: 441
		private class FailureObjectLogSchema : ObjectLogSchema
		{
			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002C705 File Offset: 0x0002A905
			public override string LogType
			{
				get
				{
					return "Failure Object Log";
				}
			}

			// Token: 0x04000910 RID: 2320
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> FailureGuid = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("FailureGuid", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureGuid);

			// Token: 0x04000911 RID: 2321
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> FailureLevel = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("FailureLevel", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureLevel);

			// Token: 0x04000912 RID: 2322
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> ApplicationVersion = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("ApplicationVersion", (FailureObjectLog.FailureObjectLogEntry d) => ExWatson.RealApplicationVersion.ToString());

			// Token: 0x04000913 RID: 2323
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> ObjectGuid = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("ObjectGuid", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureObject.ObjectGuid);

			// Token: 0x04000914 RID: 2324
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> ObjectType = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("ObjectType", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureObject.ObjectType);

			// Token: 0x04000915 RID: 2325
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> Flags = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("Flags", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureObject.Flags);

			// Token: 0x04000916 RID: 2326
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> FailureContext = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("FailureContext", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureObject.FailureContext);

			// Token: 0x04000917 RID: 2327
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> ExceptionFailureType = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("ExceptionFailureType", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureExceptionType);

			// Token: 0x04000918 RID: 2328
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> ExceptionHash = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("ExceptionHash", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureExceptionHash);

			// Token: 0x04000919 RID: 2329
			public static readonly ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry> ExceptionMessage = new ObjectLogSimplePropertyDefinition<FailureObjectLog.FailureObjectLogEntry>("ExceptionMessage", (FailureObjectLog.FailureObjectLogEntry d) => d.FailureException);
		}
	}
}
