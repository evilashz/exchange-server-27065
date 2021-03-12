using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200078C RID: 1932
	[Cmdlet("Get", "CalendarDiagnosticAnalysis", DefaultParameterSetName = "DefaultSet")]
	public sealed class GetCalendarDiagnosticAnalysis : Task
	{
		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x00116A24 File Offset: 0x00114C24
		// (set) Token: 0x060043E6 RID: 17382 RVA: 0x00116A45 File Offset: 0x00114C45
		[Parameter(Mandatory = true, ParameterSetName = "DefaultSet")]
		public CalendarLog[] CalendarLogs
		{
			get
			{
				return ((CalendarLog[])base.Fields["CalendarLogKey"]) ?? new CalendarLog[0];
			}
			set
			{
				base.Fields["CalendarLogKey"] = value;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x00116A58 File Offset: 0x00114C58
		// (set) Token: 0x060043E8 RID: 17384 RVA: 0x00116A6F File Offset: 0x00114C6F
		[Parameter(Mandatory = true, ParameterSetName = "LocationSet")]
		public string[] LogLocation
		{
			get
			{
				return (string[])base.Fields["CalendarLogLocationKey"];
			}
			set
			{
				base.Fields["CalendarLogLocationKey"] = value;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x00116A82 File Offset: 0x00114C82
		// (set) Token: 0x060043EA RID: 17386 RVA: 0x00116A99 File Offset: 0x00114C99
		[Parameter(Mandatory = false)]
		public string GlobalObjectId
		{
			get
			{
				return (string)base.Fields["ItemIDKey"];
			}
			set
			{
				base.Fields["ItemIDKey"] = value;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x00116AAC File Offset: 0x00114CAC
		// (set) Token: 0x060043EC RID: 17388 RVA: 0x00116ACD File Offset: 0x00114CCD
		[Parameter(Mandatory = false)]
		public AnalysisDetailLevel DetailLevel
		{
			get
			{
				return (AnalysisDetailLevel)(base.Fields["DetailLevelKey"] ?? AnalysisDetailLevel.Basic);
			}
			set
			{
				base.Fields["DetailLevelKey"] = value;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x00116AE5 File Offset: 0x00114CE5
		// (set) Token: 0x060043EE RID: 17390 RVA: 0x00116B06 File Offset: 0x00114D06
		[Parameter(Mandatory = false)]
		public OutputType OutputAs
		{
			get
			{
				return (OutputType)(base.Fields["OutputTypeKey"] ?? OutputType.CSV);
			}
			set
			{
				base.Fields["OutputTypeKey"] = value;
			}
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x00116B20 File Offset: 0x00114D20
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.LogLocation != null && this.LogLocation.Length > 0)
			{
				List<CalendarLog> list = new List<CalendarLog>();
				foreach (string identity in this.LogLocation)
				{
					list.AddRange(CalendarLog.Parse(identity));
				}
				this.CalendarLogs = list.ToArray();
			}
			else if (this.CalendarLogs.Count<CalendarLog>() == 0)
			{
				base.WriteError(new InvalidADObjectOperationException(Strings.CalendarLogsNotFound), ErrorCategory.InvalidData, null);
			}
			foreach (CalendarLog calendarLog in this.CalendarLogs)
			{
				if (calendarLog.IsFileLink != this.CalendarLogs.First<CalendarLog>().IsFileLink)
				{
					base.WriteError(new InvalidADObjectOperationException(Strings.CalendarAnalysisMixedModeNotSupported), ErrorCategory.InvalidData, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x00116C14 File Offset: 0x00114E14
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			CalendarLog calendarLog = this.CalendarLogs.FirstOrDefault<CalendarLog>();
			if (calendarLog == null)
			{
				return;
			}
			CalendarDiagnosticAnalyzer calendarDiagnosticAnalyzer;
			if (calendarLog.IsFileLink)
			{
				calendarDiagnosticAnalyzer = new CalendarDiagnosticAnalyzer(null, this.DetailLevel);
			}
			else
			{
				CalendarLogId calendarLogId = calendarLog.Identity as CalendarLogId;
				UriHandler uriHandler = new UriHandler(calendarLogId.Uri);
				string host = uriHandler.Host;
				SmtpAddress address = new SmtpAddress(uriHandler.UserName, host);
				if (!address.IsValidAddress)
				{
					base.WriteError(new InvalidADObjectOperationException(Strings.Error_InvalidAddress((string)address)), ErrorCategory.InvalidData, null);
				}
				ExchangePrincipal principal = ExchangePrincipal.FromProxyAddress(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(host), (string)address, RemotingOptions.AllowCrossSite);
				calendarDiagnosticAnalyzer = new CalendarDiagnosticAnalyzer(principal, this.DetailLevel);
			}
			try
			{
				CalendarLog[] array;
				if (!string.IsNullOrEmpty(this.GlobalObjectId))
				{
					array = (from f in this.CalendarLogs
					where f.CleanGlobalObjectId == this.GlobalObjectId
					select f).ToArray<CalendarLog>();
				}
				else
				{
					array = this.CalendarLogs;
				}
				CalendarLog[] calendarLogs = array;
				IEnumerable<CalendarLogAnalysis> logs = calendarDiagnosticAnalyzer.AnalyzeLogs(calendarLogs);
				base.WriteObject(CalendarLogAnalysisSerializer.Serialize(logs, this.OutputAs, this.DetailLevel, true));
			}
			catch (InvalidLogCollectionException)
			{
				base.WriteError(new InvalidADObjectOperationException(Strings.Error_MultipleItemsFound), ErrorCategory.InvalidData, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00116D54 File Offset: 0x00114F54
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is StorageTransientException || exception is StoragePermanentException || exception is ObjectNotFoundException || exception is IOException || exception is UnauthorizedAccessException;
		}

		// Token: 0x04002A31 RID: 10801
		private const string DetailLevelKey = "DetailLevelKey";

		// Token: 0x04002A32 RID: 10802
		private const string CalendarLogKey = "CalendarLogKey";

		// Token: 0x04002A33 RID: 10803
		private const string CalendarLogLocationKey = "CalendarLogLocationKey";

		// Token: 0x04002A34 RID: 10804
		private const string OutputTypeKey = "OutputTypeKey";

		// Token: 0x04002A35 RID: 10805
		private const string ItemIdKey = "ItemIDKey";

		// Token: 0x04002A36 RID: 10806
		private const string DefaultParameterSet = "DefaultSet";

		// Token: 0x04002A37 RID: 10807
		private const string LocationParameterSet = "LocationSet";
	}
}
