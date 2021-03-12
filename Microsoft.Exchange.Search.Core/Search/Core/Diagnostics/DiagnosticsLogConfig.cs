using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000095 RID: 149
	internal class DiagnosticsLogConfig : Config, IDiagnosticsLogConfig, IConfig
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000D170 File Offset: 0x0000B370
		public DiagnosticsLogConfig(DiagnosticsLogConfig.LogDefaults logDefaults)
		{
			this.DefaultEventLogComponentGuid = logDefaults.EventLogComponentGuid;
			this.DefaultServiceName = logDefaults.ServiceName;
			this.DefaultLogTypeName = logDefaults.LogTypeName;
			this.DefaultLogFilePath = logDefaults.LogFilePath;
			this.DefaultLogFilePrefix = logDefaults.LogFilePrefix;
			this.DefaultLogComponent = logDefaults.LogComponent;
			this.ConfigurableLoad();
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public Guid EventLogComponentGuid { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000D1F5 File Offset: 0x0000B3F5
		public bool IsEnabled { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000D1FE File Offset: 0x0000B3FE
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000D206 File Offset: 0x0000B406
		public string ServiceName { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000D20F File Offset: 0x0000B40F
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000D217 File Offset: 0x0000B417
		public string LogTypeName { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000D220 File Offset: 0x0000B420
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000D228 File Offset: 0x0000B428
		public string LogFilePath { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000D231 File Offset: 0x0000B431
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000D239 File Offset: 0x0000B439
		public string LogFilePrefix { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000D242 File Offset: 0x0000B442
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000D24A File Offset: 0x0000B44A
		public string LogComponent { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000D253 File Offset: 0x0000B453
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000D25B File Offset: 0x0000B45B
		public int MaxAge { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000D264 File Offset: 0x0000B464
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000D26C File Offset: 0x0000B46C
		public int MaxDirectorySize { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000D275 File Offset: 0x0000B475
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000D27D File Offset: 0x0000B47D
		public int MaxFileSize { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000D286 File Offset: 0x0000B486
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000D28E File Offset: 0x0000B48E
		public bool ApplyHourPrecision { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000D297 File Offset: 0x0000B497
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000D29F File Offset: 0x0000B49F
		public bool IncludeExtendedLogging { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		public int ExtendedLoggingSize { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000D2B9 File Offset: 0x0000B4B9
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		public DiagnosticsLoggingTag DiagnosticsLoggingTag { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000D2CA File Offset: 0x0000B4CA
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000D2D2 File Offset: 0x0000B4D2
		private Guid DefaultEventLogComponentGuid
		{
			get
			{
				return this.defaultEventLogComponentGuid;
			}
			set
			{
				this.defaultEventLogComponentGuid = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000D2DB File Offset: 0x0000B4DB
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
		private string DefaultServiceName
		{
			get
			{
				return this.defaultServiceName;
			}
			set
			{
				this.defaultServiceName = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		private string DefaultLogTypeName
		{
			get
			{
				return this.defaultLogTypeName;
			}
			set
			{
				this.defaultLogTypeName = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000D305 File Offset: 0x0000B505
		private string DefaultLogFilePath
		{
			get
			{
				return this.defaultLogFilePath;
			}
			set
			{
				this.defaultLogFilePath = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000D30E File Offset: 0x0000B50E
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000D316 File Offset: 0x0000B516
		private string DefaultLogFilePrefix
		{
			get
			{
				return this.defaultLogFilePrefix;
			}
			set
			{
				this.defaultLogFilePrefix = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000D31F File Offset: 0x0000B51F
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000D327 File Offset: 0x0000B527
		private string DefaultLogComponent
		{
			get
			{
				return this.defaultLogComponent;
			}
			set
			{
				this.defaultLogComponent = value;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000D330 File Offset: 0x0000B530
		public override void Load()
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D334 File Offset: 0x0000B534
		private void ConfigurableLoad()
		{
			this.EventLogComponentGuid = this.DefaultEventLogComponentGuid;
			if (this.EventLogComponentGuid == Guid.Empty)
			{
				throw new ArgumentNullException("eventLogComponentGuid must be set.");
			}
			this.ServiceName = this.DefaultServiceName;
			Util.ThrowOnNullOrEmptyArgument(this.ServiceName, "serviceName must be set.");
			this.LogTypeName = base.ReadString("logTypeName", this.DefaultLogTypeName);
			Util.ThrowOnNullOrEmptyArgument(this.LogTypeName, "logTypeName config key must be set.");
			this.LogFilePath = base.ReadString("logFilePath", this.DefaultLogFilePath);
			Util.ThrowOnNullOrEmptyArgument(this.LogFilePath, "logFilePath config key must be set.");
			this.LogFilePrefix = base.ReadString("logFilePrefix", this.DefaultLogFilePrefix);
			Util.ThrowOnNullOrEmptyArgument(this.LogFilePrefix, "logFilePrefix config key must be set.");
			this.LogComponent = base.ReadString("logComponent", this.DefaultLogComponent);
			Util.ThrowOnNullOrEmptyArgument(this.LogComponent, "logComponent config key must be set.");
			this.IsEnabled = base.ReadBool("fileLoggingEnabled", true);
			this.MaxAge = base.ReadInt("ageQuota", 1440);
			this.MaxDirectorySize = base.ReadInt("maxDirectorySize", 1048576);
			this.MaxFileSize = base.ReadInt("maxFileSize", 10240);
			this.ApplyHourPrecision = base.ReadBool("applyHourPrecision", true);
			this.IncludeExtendedLogging = base.ReadBool("includeExtendedLogging", true);
			this.DiagnosticsLoggingTag = (DiagnosticsLoggingTag)base.ReadInt("diagnosticsLoggingTag", 7);
			this.ExtendedLoggingSize = base.ReadInt("extendedLoggingSize", 16);
			if (this.ExtendedLoggingSize != 8 && this.ExtendedLoggingSize != 16 && this.ExtendedLoggingSize != 32 && this.ExtendedLoggingSize != 64 && this.ExtendedLoggingSize != 128 && this.ExtendedLoggingSize != 256)
			{
				throw new ArgumentException("Extended logging size must be (8 | 16 | 32 | 64 | 128 | 256).", "extendedLoggingSize");
			}
		}

		// Token: 0x040001C8 RID: 456
		internal const int DefaultExtendedLoggingSize = 16;

		// Token: 0x040001C9 RID: 457
		private const bool DefaultFileLoggingEnabled = true;

		// Token: 0x040001CA RID: 458
		private const int DefaultMaxAge = 1440;

		// Token: 0x040001CB RID: 459
		private const int DefaultMaxDirectorySize = 1048576;

		// Token: 0x040001CC RID: 460
		private const int DefaultMaxFileSize = 10240;

		// Token: 0x040001CD RID: 461
		private const bool DefaultApplyHourPrecision = true;

		// Token: 0x040001CE RID: 462
		private const bool DefaultIncludeExtendedLogging = true;

		// Token: 0x040001CF RID: 463
		private const int DefaultDiagnosticsLoggingTag = 7;

		// Token: 0x040001D0 RID: 464
		private Guid defaultEventLogComponentGuid = Guid.Empty;

		// Token: 0x040001D1 RID: 465
		private string defaultServiceName;

		// Token: 0x040001D2 RID: 466
		private string defaultLogTypeName;

		// Token: 0x040001D3 RID: 467
		private string defaultLogFilePath;

		// Token: 0x040001D4 RID: 468
		private string defaultLogFilePrefix;

		// Token: 0x040001D5 RID: 469
		private string defaultLogComponent;

		// Token: 0x02000096 RID: 150
		internal class LogDefaults
		{
			// Token: 0x0600042E RID: 1070 RVA: 0x0000D510 File Offset: 0x0000B710
			internal LogDefaults(Guid eventLogComponentGuid, string serviceName, string logTypeName, string logFilePath, string logFilePrefix, string logComponent)
			{
				this.EventLogComponentGuid = eventLogComponentGuid;
				this.ServiceName = serviceName;
				this.LogTypeName = logTypeName;
				this.LogFilePath = logFilePath;
				this.LogFilePrefix = logFilePrefix;
				this.LogComponent = logComponent;
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000D545 File Offset: 0x0000B745
			// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000D54D File Offset: 0x0000B74D
			internal Guid EventLogComponentGuid { get; set; }

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000D556 File Offset: 0x0000B756
			// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000D55E File Offset: 0x0000B75E
			internal string ServiceName { get; set; }

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000D567 File Offset: 0x0000B767
			// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000D56F File Offset: 0x0000B76F
			internal string LogTypeName { get; set; }

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000D578 File Offset: 0x0000B778
			// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000D580 File Offset: 0x0000B780
			internal string LogFilePath { get; set; }

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000D589 File Offset: 0x0000B789
			// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000D591 File Offset: 0x0000B791
			internal string LogFilePrefix { get; set; }

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000D59A File Offset: 0x0000B79A
			// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000D5A2 File Offset: 0x0000B7A2
			internal string LogComponent { get; set; }
		}
	}
}
