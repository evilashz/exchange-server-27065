using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000086 RID: 134
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ArchiveProcessorFactory
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000259A4 File Offset: 0x00023BA4
		internal static bool UseEwsForSameServerDiffDbArchive
		{
			get
			{
				if (ArchiveProcessorFactory.useEWSForSameServerDiffDbArchive == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ArchiveProcessorFactory.UseEWSForSameServerDiffDbArchiveRegKey);
					int value;
					if (obj != null && int.TryParse(obj.ToString(), out value))
					{
						ArchiveProcessorFactory.useEWSForSameServerDiffDbArchive = new bool?(value != 0);
					}
					else
					{
						ArchiveProcessorFactory.useEWSForSameServerDiffDbArchive = new bool?(false);
					}
				}
				return ArchiveProcessorFactory.useEWSForSameServerDiffDbArchive.Value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00025A08 File Offset: 0x00023C08
		internal static bool UseXtcMoveToArchive
		{
			get
			{
				if (ArchiveProcessorFactory.useXTCMoveToArchive == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.UseXtcMoveToArchive);
					if (obj is int)
					{
						int num = (int)obj;
						ArchiveProcessorFactory.useXTCMoveToArchive = new bool?(num != 0);
					}
					else
					{
						ArchiveProcessorFactory.useXTCMoveToArchive = new bool?(false);
					}
				}
				return ArchiveProcessorFactory.useXTCMoveToArchive.Value;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00025A68 File Offset: 0x00023C68
		public static IArchiveProcessor Create(ElcGlobals.ArchiveLocation archiveLocation, MailboxSession mailboxSession, ADUser adUser, StatisticsLogEntry statisticsLogEntry, bool isTestMode)
		{
			if (adUser.IsInactiveMailbox)
			{
				return null;
			}
			bool flag = archiveLocation == ElcGlobals.ArchiveLocation.SameServer;
			if (flag)
			{
				if (ArchiveProcessorFactory.UseEwsForSameServerDiffDbArchive)
				{
					return new CrossServerArchiveProcessor(mailboxSession, adUser, statisticsLogEntry, isTestMode);
				}
				return new LocalArchiveProcessor(mailboxSession, statisticsLogEntry);
			}
			else if (archiveLocation == ElcGlobals.ArchiveLocation.RemoteArchive || mailboxSession.MailboxOwner.GetArchiveMailbox().IsRemote)
			{
				if (isTestMode)
				{
					ArchiveProcessorFactory.useXTCMoveToArchive = null;
				}
				if (ArchiveProcessorFactory.UseXtcMoveToArchive)
				{
					return new LocalArchiveProcessor(mailboxSession, statisticsLogEntry);
				}
				return new CloudArchiveProcessor(mailboxSession, adUser, statisticsLogEntry, isTestMode);
			}
			else
			{
				if (!ElcGlobals.Configuration.GetConfig<bool>("DisableElcRemoteArchive") && ArchiveProcessorFactory.IsElcRemoteArchiveFeatureEnabled(adUser))
				{
					return new CrossServerArchiveProcessor(mailboxSession, adUser, statisticsLogEntry, isTestMode);
				}
				return new RemoteArchiveProcessor(mailboxSession, statisticsLogEntry);
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00025B0C File Offset: 0x00023D0C
		private static bool IsElcRemoteArchiveFeatureEnabled(ADUser adUser)
		{
			if (adUser != null)
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(adUser.GetContext(null), null, null);
				return snapshot.MailboxAssistants.ElcRemoteArchive.Enabled;
			}
			ArchiveProcessorFactory.Tracer.TraceWarning(0L, "ElcRemoteArchive feature is disabled because AD user is null, hence cannot get per user variant snapshot");
			return false;
		}

		// Token: 0x040003C7 RID: 967
		private static readonly Trace Tracer = ExTraceGlobals.ELCTracer;

		// Token: 0x040003C8 RID: 968
		private static readonly string UseEWSForSameServerDiffDbArchiveRegKey = "UseEWSForSameServerDiffDbArchive";

		// Token: 0x040003C9 RID: 969
		private static bool? useEWSForSameServerDiffDbArchive;

		// Token: 0x040003CA RID: 970
		private static bool? useXTCMoveToArchive;
	}
}
