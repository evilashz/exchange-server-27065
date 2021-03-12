using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation.Runspaces;
using System.Reflection;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200016E RID: 366
	internal class MrsPSHandler : DisposeTrackableBase
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x00020100 File Offset: 0x0001E300
		public MrsPSHandler(string prefix)
		{
			MrsPSHandler.CheckPSRunspaceInitialized();
			this.mrsCommandInteractionHandler = new MrsPSHandler.MrsCommandInteractionHandler(prefix);
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				this.MonadConnection = new MonadConnection("timeout=30", this.mrsCommandInteractionHandler);
				disposeGuard.Add<MonadConnection>(this.MonadConnection);
				this.MonadConnection.Open();
				disposeGuard.Success();
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00020184 File Offset: 0x0001E384
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x0002018C File Offset: 0x0001E38C
		public MonadConnection MonadConnection { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00020195 File Offset: 0x0001E395
		public List<ReportEntry> ReportEntries
		{
			get
			{
				return this.mrsCommandInteractionHandler.ReportEntries;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000201A2 File Offset: 0x0001E3A2
		public List<Exception> ExceptionsReported
		{
			get
			{
				return this.mrsCommandInteractionHandler.ExceptionsReported;
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000201AF File Offset: 0x0001E3AF
		public MonadCommand GetCommand(MrsCmdlet mrsCmdlet)
		{
			return new MonadCommand(MrsPSHandler.cmdletToString[mrsCmdlet], this.MonadConnection);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000201C8 File Offset: 0x0001E3C8
		private static void CheckPSRunspaceInitialized()
		{
			if (!MrsPSHandler.psRunspaceInitialized)
			{
				lock (MrsPSHandler.psLocker)
				{
					if (!MrsPSHandler.psRunspaceInitialized)
					{
						List<CmdletConfigurationEntry> list = new List<CmdletConfigurationEntry>();
						foreach (KeyValuePair<string, MrsPSHandler.MrsCmdletInfo[]> keyValuePair in MrsPSHandler.cmdlets)
						{
							string text = Path.Combine(ConfigurationContext.Setup.BinPath, keyValuePair.Key);
							if (File.Exists(text))
							{
								Assembly assembly = Assembly.LoadFrom(text);
								foreach (MrsPSHandler.MrsCmdletInfo mrsCmdletInfo in keyValuePair.Value)
								{
									list.Add(new CmdletConfigurationEntry(mrsCmdletInfo.Cmdlet, assembly.GetType(mrsCmdletInfo.ClassName, true, true), "Microsoft.Exchange.ServerStatus-Help.xml"));
									MrsPSHandler.cmdletToString.Add(mrsCmdletInfo.MrsCmdlet, mrsCmdletInfo.Cmdlet);
								}
							}
						}
						MonadRunspaceConfiguration.AddArray(list.ToArray());
						MrsPSHandler.psRunspaceInitialized = true;
					}
				}
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000202F4 File Offset: 0x0001E4F4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.MonadConnection != null)
			{
				this.MonadConnection.Dispose();
				this.MonadConnection = null;
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00020313 File Offset: 0x0001E513
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MrsPSHandler>(this);
		}

		// Token: 0x040007FC RID: 2044
		private static readonly Dictionary<string, MrsPSHandler.MrsCmdletInfo[]> cmdlets = new Dictionary<string, MrsPSHandler.MrsCmdletInfo[]>
		{
			{
				"Microsoft.Exchange.Management.dll",
				new MrsPSHandler.MrsCmdletInfo[]
				{
					new MrsPSHandler.MrsCmdletInfo(MrsCmdlet.UpdateMovedMailbox, "Update-MovedMailbox", "Microsoft.Exchange.Management.RecipientTasks.UpdateMovedMailbox"),
					new MrsPSHandler.MrsCmdletInfo(MrsCmdlet.SetOrganization, "Set-Organization", "Microsoft.Exchange.Management.Deployment.SetOrganization"),
					new MrsPSHandler.MrsCmdletInfo(MrsCmdlet.GetPublicFolderMoveRequest, "Get-PublicFolderMoveRequest", "Microsoft.Exchange.Management.RecipientTasks.GetPublicFolderMoveRequest"),
					new MrsPSHandler.MrsCmdletInfo(MrsCmdlet.GetMoveRequest, "Get-MoveRequest", "Microsoft.Exchange.Management.RecipientTasks.GetMoveRequest")
				}
			},
			{
				"Microsoft.Exchange.Management.Recipient.dll",
				new MrsPSHandler.MrsCmdletInfo[]
				{
					new MrsPSHandler.MrsCmdletInfo(MrsCmdlet.SetConsumerMailbox, "Set-ConsumerMailbox", "Microsoft.Exchange.Management.RecipientTasks.SetConsumerMailbox"),
					new MrsPSHandler.MrsCmdletInfo(MrsCmdlet.GetMailbox, "Get-Mailbox", "Microsoft.Exchange.Management.RecipientTasks.GetMailbox")
				}
			}
		};

		// Token: 0x040007FD RID: 2045
		private static readonly object psLocker = new object();

		// Token: 0x040007FE RID: 2046
		private static Dictionary<MrsCmdlet, string> cmdletToString = new Dictionary<MrsCmdlet, string>();

		// Token: 0x040007FF RID: 2047
		private static bool psRunspaceInitialized = false;

		// Token: 0x04000800 RID: 2048
		private readonly MrsPSHandler.MrsCommandInteractionHandler mrsCommandInteractionHandler;

		// Token: 0x0200016F RID: 367
		private class MrsCmdletInfo
		{
			// Token: 0x06000E23 RID: 3619 RVA: 0x000203E7 File Offset: 0x0001E5E7
			public MrsCmdletInfo(MrsCmdlet mrsCmdlet, string cmdlet, string className)
			{
				this.MrsCmdlet = mrsCmdlet;
				this.Cmdlet = cmdlet;
				this.ClassName = className;
			}

			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00020404 File Offset: 0x0001E604
			// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0002040C File Offset: 0x0001E60C
			public string Cmdlet { get; private set; }

			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00020415 File Offset: 0x0001E615
			// (set) Token: 0x06000E27 RID: 3623 RVA: 0x0002041D File Offset: 0x0001E61D
			public string ClassName { get; private set; }

			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00020426 File Offset: 0x0001E626
			// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0002042E File Offset: 0x0001E62E
			public MrsCmdlet MrsCmdlet { get; private set; }
		}

		// Token: 0x02000170 RID: 368
		private class MrsCommandInteractionHandler : CommandInteractionHandler
		{
			// Token: 0x06000E2A RID: 3626 RVA: 0x00020437 File Offset: 0x0001E637
			public MrsCommandInteractionHandler(string prefix)
			{
				this.prefix = prefix;
				this.ReportEntries = new List<ReportEntry>();
				this.ExceptionsReported = new List<Exception>();
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002045C File Offset: 0x0001E65C
			// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00020464 File Offset: 0x0001E664
			public List<ReportEntry> ReportEntries { get; private set; }

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002046D File Offset: 0x0001E66D
			// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00020475 File Offset: 0x0001E675
			public List<Exception> ExceptionsReported { get; private set; }

			// Token: 0x06000E2F RID: 3631 RVA: 0x00020480 File Offset: 0x0001E680
			public override void ReportVerboseOutput(string message)
			{
				base.ReportVerboseOutput(message);
				MrsTracer.Common.Debug("{0}", new object[]
				{
					string.Format("{0} - {1}", this.prefix, message)
				});
				ReportEntry reportEntry = new ReportEntry(LocalizedString.Empty, ReportEntryType.Debug);
				reportEntry.DebugData = message;
				this.ReportEntries.Add(reportEntry);
			}

			// Token: 0x06000E30 RID: 3632 RVA: 0x000204E0 File Offset: 0x0001E6E0
			public override void ReportWarning(WarningReportEventArgs e)
			{
				base.ReportWarning(e);
				MrsTracer.Common.Warning("{0}", new object[]
				{
					string.Format("{0} - {1}", this.prefix, e.WarningMessage)
				});
				this.ReportEntries.Add(new ReportEntry(new LocalizedString(e.WarningMessage)));
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x00020540 File Offset: 0x0001E740
			public override void ReportException(Exception ex)
			{
				base.ReportException(ex);
				this.ExceptionsReported.Add(ex);
				LocalizedString localizedString = CommonUtils.FullExceptionMessage(ex);
				MrsTracer.Common.Error("{0}", new object[]
				{
					string.Format("{0} - {1}", this.prefix, localizedString)
				});
				this.ReportEntries.Add(new ReportEntry(localizedString, ReportEntryType.Error, ex, ReportEntryFlags.None));
			}

			// Token: 0x06000E32 RID: 3634 RVA: 0x000205AC File Offset: 0x0001E7AC
			public override void ReportErrors(ErrorReportEventArgs e)
			{
				base.ReportErrors(e);
				this.ExceptionsReported.Add(e.ErrorRecord.Exception);
				LocalizedString localizedString = CommonUtils.FullExceptionMessage(e.ErrorRecord.Exception);
				MrsTracer.Common.Error("{0}", new object[]
				{
					string.Format("{0} - {1}", this.prefix, localizedString)
				});
				this.ReportEntries.Add(new ReportEntry(localizedString, ReportEntryType.Error, e.ErrorRecord.Exception, ReportEntryFlags.None));
			}

			// Token: 0x06000E33 RID: 3635 RVA: 0x00020638 File Offset: 0x0001E838
			public override ConfirmationChoice ShowConfirmationDialog(string message, ConfirmationChoice defaultChoice)
			{
				MrsTracer.Common.Warning("{0}", new object[]
				{
					string.Format("{0} - Automatically Confirmed - {1}", this.prefix, message)
				});
				this.ReportEntries.Add(new ReportEntry(new LocalizedString(message)));
				return ConfirmationChoice.Yes;
			}

			// Token: 0x04000805 RID: 2053
			private readonly string prefix;
		}
	}
}
