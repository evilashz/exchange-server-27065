using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x0200009A RID: 154
	internal class DiagnosticsDatabaseProcessor : DiagnosticsProcessorBase
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x000186A5 File Offset: 0x000168A5
		public DiagnosticsDatabaseProcessor(DiagnosticsArgument arguments) : base(arguments)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000186B0 File Offset: 0x000168B0
		public List<TimeBasedDatabaseDriver> GetDatabaseDriversToProcess(TimeBasedAssistantControllerWrapper tba)
		{
			ArgumentValidator.ThrowIfNull("tba", tba);
			bool flag = base.Arguments.HasArgument("database");
			if (flag)
			{
				string argument = base.Arguments.GetArgument<string>("database");
				return new List<TimeBasedDatabaseDriver>
				{
					this.GetDatabaseDriverByName(tba, argument)
				};
			}
			TimeBasedDatabaseDriver[] currentAssistantDrivers = tba.Controller.GetCurrentAssistantDrivers();
			if (currentAssistantDrivers != null)
			{
				return currentAssistantDrivers.ToList<TimeBasedDatabaseDriver>();
			}
			return new List<TimeBasedDatabaseDriver>();
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00018720 File Offset: 0x00016920
		public XElement ProcessDiagnosticsForOneDatabase(TimeBasedDatabaseDriver driver)
		{
			ArgumentValidator.ThrowIfNull("driver", driver);
			if (base.Arguments.HasArgument("summary"))
			{
				return DiagnosticsFormatter.FormatTimeBasedJobDatabaseStats(driver.DatabaseInfo.DatabaseName, driver.DatabaseInfo.Guid, driver.GetDatabaseDiagnosticsSummary());
			}
			if (base.Arguments.HasArgument("running"))
			{
				return DiagnosticsFormatter.FormatTimeBasedMailboxes(true, driver.DatabaseInfo.DatabaseName, driver.DatabaseInfo.Guid, driver.GetDatabaseDiagnosticsSummary(), driver.GetMailboxGuidList(true));
			}
			if (base.Arguments.HasArgument("queued"))
			{
				return DiagnosticsFormatter.FormatTimeBasedMailboxes(false, driver.DatabaseInfo.DatabaseName, driver.DatabaseInfo.Guid, driver.GetDatabaseDiagnosticsSummary(), driver.GetMailboxGuidList(false));
			}
			if (base.Arguments.HasArgument("history"))
			{
				return DiagnosticsFormatter.FormatWindowJobHistory(driver.DatabaseInfo.DatabaseName, driver.DatabaseInfo.Guid, driver.GetDatabaseDiagnosticsSummary(), driver.GetWindowJobHistory());
			}
			return DiagnosticsFormatter.FormatTimeBasedJobDatabaseStatsCommon(driver.DatabaseInfo.DatabaseName, driver.DatabaseInfo.Guid, driver.GetDatabaseDiagnosticsSummary());
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00018840 File Offset: 0x00016A40
		private TimeBasedDatabaseDriver GetDatabaseDriverByName(TimeBasedAssistantControllerWrapper tba, string databaseName)
		{
			ArgumentValidator.ThrowIfNull("tba", tba);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("databaseName", databaseName);
			foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in tba.Controller.GetCurrentAssistantDrivers())
			{
				if (timeBasedDatabaseDriver.DatabaseInfo.DatabaseName.Equals(databaseName))
				{
					return timeBasedDatabaseDriver;
				}
			}
			throw new UnknownDatabaseException(databaseName);
		}
	}
}
