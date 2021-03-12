using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Log : ILog
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000022E0 File Offset: 0x000004E0
		public Log(ILogEmitter logEmitter, LogLevel logLevel = LogLevel.LogDefault)
		{
			this.LogEmitter = logEmitter;
			this.LogLevel = logLevel;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022F6 File Offset: 0x000004F6
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000022FE File Offset: 0x000004FE
		protected ILogEmitter LogEmitter { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002307 File Offset: 0x00000507
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000230F File Offset: 0x0000050F
		protected LogLevel LogLevel { get; set; }

		// Token: 0x06000030 RID: 48 RVA: 0x00002318 File Offset: 0x00000518
		public bool IsEnabled(LogLevel level)
		{
			return this.LogLevel.HasFlag(level);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002330 File Offset: 0x00000530
		public virtual void Trace(string formatString, params object[] args)
		{
			if (this.IsEnabled(LogLevel.LogTrace))
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002348 File Offset: 0x00000548
		public virtual void Debug(string formatString, params object[] args)
		{
			if (this.IsEnabled(LogLevel.LogDebug))
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002360 File Offset: 0x00000560
		public virtual void Info(string formatString, params object[] args)
		{
			if (this.IsEnabled(LogLevel.LogInfo))
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002378 File Offset: 0x00000578
		public virtual void Warn(string formatString, params object[] args)
		{
			if (this.IsEnabled(LogLevel.LogWarn))
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002391 File Offset: 0x00000591
		public virtual void Error(string formatString, params object[] args)
		{
			if (this.IsEnabled(LogLevel.LogError))
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000023AA File Offset: 0x000005AA
		public virtual void Fatal(string formatString, params object[] args)
		{
			if (this.IsEnabled(LogLevel.LogFatal))
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000023C4 File Offset: 0x000005C4
		public virtual void Fatal(Exception exception, string message = "")
		{
			if (this.IsEnabled(LogLevel.LogFatal))
			{
				string formatString = string.Format("{0}:{1}", message, exception.Message);
				this.LogEmitter.Emit(formatString, new object[0]);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000023FF File Offset: 0x000005FF
		public virtual void Assert(bool condition, string formatString, params object[] args)
		{
			if (condition)
			{
				this.LogEmitter.Emit(formatString, args);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002411 File Offset: 0x00000611
		public virtual void RetailAssert(bool condition, string formatString, params object[] args)
		{
			if (condition)
			{
				this.LogEmitter.Emit(formatString, args);
				ExAssert.RetailAssert(true, formatString, args);
			}
		}
	}
}
