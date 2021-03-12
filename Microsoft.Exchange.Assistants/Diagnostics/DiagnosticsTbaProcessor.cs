using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x020000A0 RID: 160
	internal class DiagnosticsTbaProcessor : DiagnosticsProcessorBase
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00019388 File Offset: 0x00017588
		public DiagnosticsTbaProcessor(DiagnosticsArgument arguments) : base(arguments)
		{
			this.dbProcessor = new DiagnosticsDatabaseProcessor(base.Arguments);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000193A4 File Offset: 0x000175A4
		public XElement Process(TimeBasedAssistantControllerWrapper[] assistantControllers)
		{
			ArgumentValidator.ThrowIfNull("assistantControllers", assistantControllers);
			IEnumerable<TimeBasedAssistantControllerWrapper> tbasToProcess;
			try
			{
				tbasToProcess = this.GetTbasToProcess(assistantControllers);
			}
			catch (UnknownAssistantException exception)
			{
				return DiagnosticsFormatter.FormatErrorElement(exception);
			}
			if (tbasToProcess == null)
			{
				return DiagnosticsFormatter.FormatErrorElement("Could not find any TBA to provide diagnostics for.");
			}
			XElement xelement = DiagnosticsFormatter.FormatRootElement();
			foreach (TimeBasedAssistantControllerWrapper tba in tbasToProcess)
			{
				List<TimeBasedDatabaseDriver> databaseDriversToProcess;
				try
				{
					databaseDriversToProcess = this.dbProcessor.GetDatabaseDriversToProcess(tba);
				}
				catch (UnknownDatabaseException exception2)
				{
					return DiagnosticsFormatter.FormatErrorElement(exception2);
				}
				if (databaseDriversToProcess == null)
				{
					return DiagnosticsFormatter.FormatErrorElement("Could not retrive the list of database drivers to process (null value).");
				}
				if (databaseDriversToProcess.Count > 0)
				{
					XElement content = this.ProcessDiagnosticsForOneTba(tba, databaseDriversToProcess);
					xelement.Add(content);
				}
			}
			return xelement;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001948C File Offset: 0x0001768C
		private IEnumerable<TimeBasedAssistantControllerWrapper> GetTbasToProcess(IEnumerable<TimeBasedAssistantControllerWrapper> assistantControllers)
		{
			ArgumentValidator.ThrowIfNull("assistantControllers", assistantControllers);
			bool flag = base.Arguments.HasArgument("assistant");
			if (flag)
			{
				string argument = base.Arguments.GetArgument<string>("assistant");
				return new TimeBasedAssistantControllerWrapper[]
				{
					this.GetTbaByName(argument, assistantControllers)
				};
			}
			return assistantControllers;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000194E0 File Offset: 0x000176E0
		private TimeBasedAssistantControllerWrapper GetTbaByName(string tbaName, IEnumerable<TimeBasedAssistantControllerWrapper> assistantControllers)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("tbaName", tbaName);
			ArgumentValidator.ThrowIfNull("assistantControllers", assistantControllers);
			foreach (TimeBasedAssistantControllerWrapper timeBasedAssistantControllerWrapper in assistantControllers)
			{
				if (timeBasedAssistantControllerWrapper.Id.Equals(tbaName))
				{
					return timeBasedAssistantControllerWrapper;
				}
			}
			throw new UnknownAssistantException(tbaName);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00019554 File Offset: 0x00017754
		private XElement ProcessDiagnosticsForOneTba(TimeBasedAssistantControllerWrapper tba, IEnumerable<TimeBasedDatabaseDriver> drivers)
		{
			ArgumentValidator.ThrowIfNull("tba", tba);
			ArgumentValidator.ThrowIfNull("drivers", drivers);
			string id = tba.Id;
			TimeSpan workCycle = tba.Controller.TimeBasedAssistantType.WorkCycle;
			TimeSpan workCycleCheckpoint = tba.Controller.TimeBasedAssistantType.WorkCycleCheckpoint;
			XElement xelement = DiagnosticsFormatter.FormatAssistantRoot(id);
			XElement content = DiagnosticsFormatter.FormatWorkcycleInfoElement(workCycle);
			XElement content2 = DiagnosticsFormatter.FormatWorkcycleCheckpointInfoElement(workCycleCheckpoint);
			XElement xelement2 = DiagnosticsFormatter.FormatDatabasesRoot();
			foreach (TimeBasedDatabaseDriver driver in drivers)
			{
				XElement content3 = this.dbProcessor.ProcessDiagnosticsForOneDatabase(driver);
				xelement2.Add(content3);
			}
			xelement.Add(content);
			xelement.Add(content2);
			xelement.Add(xelement2);
			return xelement;
		}

		// Token: 0x040002E9 RID: 745
		private readonly DiagnosticsDatabaseProcessor dbProcessor;
	}
}
