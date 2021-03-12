using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F3A RID: 3898
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditingOpticsLoggerInstance : IAuditingOpticsLoggerInstance
	{
		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x060085ED RID: 34285 RVA: 0x0024B2EB File Offset: 0x002494EB
		// (set) Token: 0x060085EE RID: 34286 RVA: 0x0024B2F3 File Offset: 0x002494F3
		private Log Logger { get; set; }

		// Token: 0x060085EF RID: 34287 RVA: 0x0024B2FC File Offset: 0x002494FC
		internal static IDisposable SetActivityIdTestHook(Guid actId)
		{
			AuditingOpticsLoggerInstance.hookableActivityId = Hookable<Guid>.Create(true, Guid.Empty);
			return AuditingOpticsLoggerInstance.hookableActivityId.SetTestHook(actId);
		}

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x060085F0 RID: 34288 RVA: 0x0024B31C File Offset: 0x0024951C
		private string ServerName
		{
			get
			{
				Server localServer = LocalServerCache.LocalServer;
				if (localServer == null)
				{
					return string.Empty;
				}
				return localServer.Name;
			}
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x060085F1 RID: 34289 RVA: 0x0024B33E File Offset: 0x0024953E
		// (set) Token: 0x060085F2 RID: 34290 RVA: 0x0024B346 File Offset: 0x00249546
		internal AuditingOpticsLoggerType LoggerType { get; private set; }

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x060085F3 RID: 34291 RVA: 0x0024B34F File Offset: 0x0024954F
		// (set) Token: 0x060085F4 RID: 34292 RVA: 0x0024B357 File Offset: 0x00249557
		public bool Enabled { get; private set; }

		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x060085F5 RID: 34293 RVA: 0x0024B360 File Offset: 0x00249560
		// (set) Token: 0x060085F6 RID: 34294 RVA: 0x0024B368 File Offset: 0x00249568
		private LogSchema LogSchema { get; set; }

		// Token: 0x060085F7 RID: 34295 RVA: 0x0024B371 File Offset: 0x00249571
		internal bool IsDebugTraceEnabled()
		{
			return this.Tracer != null && this.Tracer.IsTraceEnabled(TraceType.DebugTrace);
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x060085F8 RID: 34296 RVA: 0x0024B389 File Offset: 0x00249589
		private Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AdminAuditLogTracer;
			}
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x060085F9 RID: 34297 RVA: 0x0024B390 File Offset: 0x00249590
		private string LogComponentName
		{
			get
			{
				return AuditingOpticsConstants.LoggerComponentName;
			}
		}

		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x060085FA RID: 34298 RVA: 0x0024B397 File Offset: 0x00249597
		private string LogTypeName
		{
			get
			{
				return this.LoggerType.ToString() + AuditingOpticsConstants.AuditLoggerTypeName;
			}
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x060085FB RID: 34299 RVA: 0x0024B3B3 File Offset: 0x002495B3
		private string FileNamePrefix
		{
			get
			{
				return this.LoggerType.ToString() + AuditingOpticsConstants.AuditLoggerFileNamePrefix;
			}
		}

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x060085FC RID: 34300 RVA: 0x0024B3CF File Offset: 0x002495CF
		private int TimestampField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060085FD RID: 34301 RVA: 0x0024B3D4 File Offset: 0x002495D4
		internal AuditingOpticsLoggerInstance(AuditingOpticsLoggerType loggerType)
		{
			EnumValidator.AssertValid<AuditingOpticsLoggerType>(loggerType);
			AuditingOpticsLoggerSettings auditingOpticsLoggerSettings = AuditingOpticsLoggerSettings.Load();
			if (auditingOpticsLoggerSettings.Enabled)
			{
				this.Enabled = true;
				this.LoggerType = loggerType;
				if (this.IsDebugTraceEnabled())
				{
					this.SafeTraceDebug(0L, "Start creating Auditing Optics log.", new object[0]);
				}
				this.LogSchema = new LogSchema(AuditingOpticsConstants.SoftwareName, "15.00.1497.010", this.LogTypeName, this.GetLogFields());
				LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.LogSchema);
				this.Logger = new Log(this.FileNamePrefix, headerFormatter, this.LogComponentName);
				if (this.IsDebugTraceEnabled())
				{
					this.SafeTraceDebug(0L, "Start configuring the Auditing Optics log.", new object[0]);
				}
				this.Logger.Configure(Path.Combine(auditingOpticsLoggerSettings.DirectoryPath, this.FileNamePrefix), auditingOpticsLoggerSettings.MaxAge, (long)auditingOpticsLoggerSettings.MaxDirectorySize.ToBytes(), (long)auditingOpticsLoggerSettings.MaxFileSize.ToBytes(), (int)auditingOpticsLoggerSettings.CacheSize.ToBytes(), auditingOpticsLoggerSettings.FlushInterval, auditingOpticsLoggerSettings.FlushToDisk);
				if (this.IsDebugTraceEnabled())
				{
					this.SafeTraceDebug(0L, "Auditing Optics log on server '{0}' is created and ready for use.", new object[]
					{
						this.ServerName
					});
					return;
				}
			}
			else
			{
				this.Enabled = false;
				if (this.IsDebugTraceEnabled())
				{
					this.SafeTraceDebug(0L, "The Auditing Optics log is disabled.", new object[0]);
				}
			}
		}

		// Token: 0x060085FE RID: 34302 RVA: 0x0024B52D File Offset: 0x0024972D
		private string[] GetLogFields()
		{
			return Enum.GetNames(typeof(AuditingOpticsLogFields));
		}

		// Token: 0x060085FF RID: 34303 RVA: 0x0024B540 File Offset: 0x00249740
		private string GetActivityId()
		{
			if (AuditingOpticsLoggerInstance.hookableActivityId == null)
			{
				return Guid.NewGuid().ToString("D");
			}
			return AuditingOpticsLoggerInstance.hookableActivityId.Value.ToString("D");
		}

		// Token: 0x06008600 RID: 34304 RVA: 0x0024B57E File Offset: 0x0024977E
		internal void SafeTraceDebug(long id, string message, params object[] args)
		{
			if (this.Tracer != null)
			{
				this.Tracer.TraceDebug(id, message, args);
			}
		}

		// Token: 0x06008601 RID: 34305 RVA: 0x0024B596 File Offset: 0x00249796
		internal void Stop()
		{
			if (this.Logger != null)
			{
				this.Logger.Close();
				this.Logger = null;
			}
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x0024B5B4 File Offset: 0x002497B4
		public void InternalLogRow(List<KeyValuePair<string, object>> customData)
		{
			if (!this.Enabled)
			{
				if (this.IsDebugTraceEnabled())
				{
					this.SafeTraceDebug(0L, "AuditingOpticsLogger log is disabled, skip writing to the log file.", new object[0]);
				}
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.LogSchema);
			if (this.IsDebugTraceEnabled())
			{
				string text = string.Empty;
				if (customData != null)
				{
					bool flag;
					text = LogRowFormatter.FormatCollection(customData, out flag);
				}
				if (this.IsDebugTraceEnabled())
				{
					this.SafeTraceDebug(0L, "Start writing row to audit log: ServerName='{0}', CustomData='{1}'", new object[]
					{
						this.ServerName,
						text
					});
				}
			}
			logRowFormatter[1] = this.GetActivityId();
			logRowFormatter[2] = this.ServerName;
			logRowFormatter[3] = ApplicationName.Current.Name.ToLower();
			logRowFormatter[4] = ApplicationName.Current.ProcessId;
			logRowFormatter[5] = customData;
			this.Logger.Append(logRowFormatter, 0);
			if (this.IsDebugTraceEnabled())
			{
				this.SafeTraceDebug(0L, "The above row is written to the log successfully.", new object[0]);
			}
		}

		// Token: 0x040059BA RID: 22970
		private static Hookable<Guid> hookableActivityId;
	}
}
