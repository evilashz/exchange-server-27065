using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000018 RID: 24
	internal static class ExchangeServer
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00005248 File Offset: 0x00003448
		internal static Server TryGetServer(string nonLocalizedAssistantName)
		{
			Server server = null;
			Exception exception = null;
			try
			{
				Util.CatchMeIfYouCan(delegate
				{
					try
					{
						server = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 48, "TryGetServer", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\common\\ExchangeServer.cs").FindLocalServer();
					}
					catch (LocalServerNotFoundException)
					{
						throw new MissingObjectException(Strings.descMissingServerConfig);
					}
					catch (DataValidationException exception2)
					{
						exception = exception2;
					}
					catch (ComputerNameNotCurrentlyAvailableException exception3)
					{
						exception = exception3;
					}
				}, nonLocalizedAssistantName);
			}
			catch (AIException exception)
			{
				AIException exception4;
				exception = exception4;
			}
			if (exception != null)
			{
				ExchangeServer.Tracer.TraceError<Exception>(0L, "Could not get Server config object from AD. {0}", exception);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidServerADObject, "ExchangeServer", new object[]
				{
					exception
				});
			}
			ExchangeServer.TracerPfd.TracePfd<int>(3L, "PFD IWS {0} Get the server config object from AD", 30103);
			return server;
		}

		// Token: 0x040000EE RID: 238
		private static readonly Trace Tracer = ExTraceGlobals.ExchangeServerTracer;

		// Token: 0x040000EF RID: 239
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
