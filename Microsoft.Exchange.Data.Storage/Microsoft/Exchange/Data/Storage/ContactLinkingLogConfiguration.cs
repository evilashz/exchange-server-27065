using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004B1 RID: 1201
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactLinkingLogConfiguration : ILogConfiguration
	{
		// Token: 0x0600355B RID: 13659 RVA: 0x000D75FC File Offset: 0x000D57FC
		private ContactLinkingLogConfiguration()
		{
			this.prefix = "ContactLinking_" + ApplicationName.Current.UniqueId + "_";
			this.directoryPath = ((ContactLinkingLogConfiguration.DirectoryPath.Value != null) ? ContactLinkingLogConfiguration.DirectoryPath.Value : Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\ContactLinkingLogs\\"));
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x000D765B File Offset: 0x000D585B
		public static ContactLinkingLogConfiguration Default
		{
			get
			{
				if (ContactLinkingLogConfiguration.defaultInstance == null)
				{
					ContactLinkingLogConfiguration.defaultInstance = new ContactLinkingLogConfiguration();
				}
				return ContactLinkingLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x0600355D RID: 13661 RVA: 0x000D7673 File Offset: 0x000D5873
		public bool IsLoggingEnabled
		{
			get
			{
				return ContactLinkingLogConfiguration.Enabled.Value;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000D767F File Offset: 0x000D587F
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x000D7682 File Offset: 0x000D5882
		public string LogPath
		{
			get
			{
				return this.directoryPath;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000D768A File Offset: 0x000D588A
		public string LogPrefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000D7692 File Offset: 0x000D5892
		public string LogComponent
		{
			get
			{
				return "ContactLinkingLog";
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000D7699 File Offset: 0x000D5899
		public string LogType
		{
			get
			{
				return "Contact Linking Log";
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000D76A0 File Offset: 0x000D58A0
		public long MaxLogDirectorySizeInBytes
		{
			get
			{
				return (long)ContactLinkingLogConfiguration.MaxDirectorySize.Value.ToBytes();
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000D76C0 File Offset: 0x000D58C0
		public long MaxLogFileSizeInBytes
		{
			get
			{
				return (long)ContactLinkingLogConfiguration.MaxFileSize.Value.ToBytes();
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000D76DF File Offset: 0x000D58DF
		public TimeSpan MaxLogAge
		{
			get
			{
				return ContactLinkingLogConfiguration.MaxAge.Value;
			}
		}

		// Token: 0x04001C54 RID: 7252
		private const string Type = "Contact Linking Log";

		// Token: 0x04001C55 RID: 7253
		private const string Component = "ContactLinkingLog";

		// Token: 0x04001C56 RID: 7254
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001C57 RID: 7255
		private static readonly BoolAppSettingsEntry Enabled = new BoolAppSettingsEntry("ContactLinkingLogEnabled", true, ContactLinkingLogConfiguration.Tracer);

		// Token: 0x04001C58 RID: 7256
		private static readonly StringAppSettingsEntry DirectoryPath = new StringAppSettingsEntry("ContactLinkingLogPath", null, ContactLinkingLogConfiguration.Tracer);

		// Token: 0x04001C59 RID: 7257
		private static readonly TimeSpanAppSettingsEntry MaxAge = new TimeSpanAppSettingsEntry("ContactLinkingLogMaxAge", TimeSpanUnit.Minutes, TimeSpan.FromDays(30.0), ContactLinkingLogConfiguration.Tracer);

		// Token: 0x04001C5A RID: 7258
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxDirectorySize = new ByteQuantifiedSizeAppSettingsEntry("ContactLinkingLogMaxDirectorySize", ByteQuantifiedSize.FromMB(250UL), ContactLinkingLogConfiguration.Tracer);

		// Token: 0x04001C5B RID: 7259
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxFileSize = new ByteQuantifiedSizeAppSettingsEntry("ContactLinkingLogMaxFileSize", ByteQuantifiedSize.FromMB(10UL), ContactLinkingLogConfiguration.Tracer);

		// Token: 0x04001C5C RID: 7260
		private readonly string prefix;

		// Token: 0x04001C5D RID: 7261
		private readonly string directoryPath;

		// Token: 0x04001C5E RID: 7262
		private static ContactLinkingLogConfiguration defaultInstance;
	}
}
