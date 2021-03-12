using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000782 RID: 1922
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarDiagnosticAnalyzer
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x00115A1D File Offset: 0x00113C1D
		internal CalendarDiagnosticAnalyzer(ExchangePrincipal principal, AnalysisDetailLevel detailLevel)
		{
			this.principal = principal;
			this.detailLevel = detailLevel;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x00115A38 File Offset: 0x00113C38
		internal IEnumerable<CalendarLogAnalysis> AnalyzeLogs(IEnumerable<CalendarLog> calendarLogs)
		{
			CalendarLog calendarLog = calendarLogs.FirstOrDefault<CalendarLog>();
			IEnumerable<CalendarLogAnalysis> enumerable;
			if (calendarLog != null && calendarLog.IsFileLink)
			{
				enumerable = this.LoadMsgLogs(calendarLogs);
			}
			else
			{
				enumerable = this.LoadMailboxLogs(calendarLogs);
			}
			string[] array = null;
			if (!this.VerifyItemCohesion(enumerable, out array))
			{
				throw new InvalidLogCollectionException();
			}
			enumerable.OrderBy((CalendarLogAnalysis f) => f, CalendarLogAnalysis.GetComparer());
			return this.PerformAnalysis(enumerable);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x00115AAC File Offset: 0x00113CAC
		private bool VerifyItemCohesion(IEnumerable<CalendarLogAnalysis> logs, out string[] ids)
		{
			List<string> list = new List<string>();
			foreach (CalendarLogAnalysis calendarLogAnalysis in logs)
			{
				if (!list.Contains(calendarLogAnalysis.InternalIdentity.CleanGlobalObjectId))
				{
					list.Add(calendarLogAnalysis.InternalIdentity.CleanGlobalObjectId);
				}
			}
			ids = list.ToArray();
			return list.Count<string>() == 1;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x00115B28 File Offset: 0x00113D28
		private IEnumerable<CalendarLogAnalysis> PerformAnalysis(IEnumerable<CalendarLogAnalysis> logs)
		{
			IEnumerable<AnalysisRule> analysisRules = AnalysisRule.GetAnalysisRules();
			LinkedList<CalendarLogAnalysis> linkedList = new LinkedList<CalendarLogAnalysis>(logs);
			foreach (AnalysisRule analysisRule in analysisRules)
			{
				analysisRule.Analyze(linkedList);
			}
			return linkedList.ToList<CalendarLogAnalysis>();
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x00115B84 File Offset: 0x00113D84
		private IEnumerable<CalendarLogAnalysis> LoadMailboxLogs(IEnumerable<CalendarLog> logs)
		{
			if (this.principal == null)
			{
				throw new InvalidOperationException("The Analyzer was not provided with session objects during construction and cannot connect to the specified mailbox");
			}
			List<CalendarLogAnalysis> list = new List<CalendarLogAnalysis>();
			using (MailboxSession mailboxSession = StoreTasksHelper.OpenMailboxSession(this.principal, "Get-CalendarDiagnosticLogs"))
			{
				foreach (CalendarLog calendarLog in logs)
				{
					CalendarLogId calendarLogId = calendarLog.Identity as CalendarLogId;
					if (calendarLogId != null)
					{
						UriHandler uriHandler = new UriHandler(calendarLogId.Uri);
						if (uriHandler.IsValidLink && !uriHandler.IsFileLink)
						{
							CalendarLogAnalysis calendarLogAnalysis = this.LoadFromMailbox(calendarLogId, uriHandler, mailboxSession);
							if (calendarLogAnalysis != null)
							{
								list.Add(calendarLogAnalysis);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x00115C58 File Offset: 0x00113E58
		private IEnumerable<CalendarLogAnalysis> LoadMsgLogs(IEnumerable<CalendarLog> logs)
		{
			List<CalendarLogAnalysis> list = new List<CalendarLogAnalysis>();
			foreach (CalendarLog calendarLog in logs)
			{
				CalendarLogId calendarLogId = calendarLog.Identity as CalendarLogId;
				if (calendarLogId != null)
				{
					UriHandler uriHandler = new UriHandler(calendarLogId.Uri);
					if (uriHandler.IsValidLink && uriHandler.IsFileLink)
					{
						CalendarLogAnalysis calendarLogAnalysis = this.LoadFromFile(calendarLogId, uriHandler);
						if (calendarLogAnalysis != null)
						{
							list.Add(calendarLogAnalysis);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x00115CEC File Offset: 0x00113EEC
		private CalendarLogAnalysis LoadFromFile(CalendarLogId id, UriHandler handler)
		{
			FileInfo fileInfo = new FileInfo(handler.Uri.LocalPath);
			if (fileInfo.Exists)
			{
				using (MessageItem messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties))
				{
					using (FileStream fileStream = fileInfo.OpenRead())
					{
						ItemConversion.ConvertMsgStorageToItem(fileStream, messageItem, new InboundConversionOptions(new EmptyRecipientCache(), null));
						IEnumerable<PropertyDefinition> displayProperties = AnalysisDetailLevels.GetDisplayProperties(this.detailLevel);
						return new CalendarLogAnalysis(id, messageItem, displayProperties);
					}
				}
			}
			throw new ArgumentException("Item argument cannot be resolved.", "item");
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x00115D90 File Offset: 0x00113F90
		private CalendarLogAnalysis LoadFromMailbox(CalendarLogId id, UriHandler handler, MailboxSession session)
		{
			StoreObjectId storeId = StoreObjectId.Deserialize(handler.Id);
			CalendarLogAnalysis result;
			using (Item item = Item.Bind(session, storeId))
			{
				IEnumerable<PropertyDefinition> displayProperties = AnalysisDetailLevels.GetDisplayProperties(this.detailLevel);
				item.Load(displayProperties.ToArray<PropertyDefinition>());
				result = new CalendarLogAnalysis(id, item, displayProperties);
			}
			return result;
		}

		// Token: 0x04002A1B RID: 10779
		private ExchangePrincipal principal;

		// Token: 0x04002A1C RID: 10780
		private AnalysisDetailLevel detailLevel;
	}
}
