using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200004B RID: 75
	internal class ElcReporting
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x000100CF File Offset: 0x0000E2CF
		internal ElcReporting(MailboxSession Session)
		{
			this.session = Session;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000100E0 File Offset: 0x0000E2E0
		internal bool CheckReportingStatus()
		{
			int num = 0;
			object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.ELCReportEnabed);
			return obj != null && int.TryParse(obj.ToString(), out num) && num == 1;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00010118 File Offset: 0x0000E318
		internal void SendReportEmail(TagExpirationExecutor tagExpirationExecutor)
		{
			try
			{
				if (tagExpirationExecutor.ELCReport.Count != 0)
				{
					this.CompleteReport(tagExpirationExecutor, RetentionActionType.DeleteAndAllowRecovery);
					this.CompleteReport(tagExpirationExecutor, RetentionActionType.PermanentlyDelete);
					this.CompleteReport(tagExpirationExecutor, RetentionActionType.MoveToArchive);
					using (MessageItem messageItem = MessageItem.Create(this.session, this.session.GetDefaultFolderId(DefaultFolderType.Inbox)))
					{
						Participant msexchangeAccount = tagExpirationExecutor.ElcAssistant.GetMSExchangeAccount(this.session.MailboxOwner.MailboxInfo.OrganizationId);
						messageItem.Sender = msexchangeAccount;
						messageItem.From = msexchangeAccount;
						messageItem.Recipients.Add(new Participant(this.session.MailboxOwner));
						messageItem.IsDraft = false;
						if (this.session.MailboxOwner.MailboxInfo.IsArchive)
						{
							messageItem.Subject = Strings.ElcEmailSubjectArchive + " " + DateTime.UtcNow.ToString("d");
						}
						else
						{
							messageItem.Subject = Strings.ElcEmailSubject + " " + DateTime.UtcNow.ToString("d");
						}
						StringBuilder stringBuilder = new StringBuilder();
						Dictionary<string, int> dictionary = new Dictionary<string, int>();
						foreach (KeyValuePair<RetentionActionType, List<List<object>>> keyValuePair in tagExpirationExecutor.ELCReport)
						{
							switch (keyValuePair.Key)
							{
							case RetentionActionType.DeleteAndAllowRecovery:
								stringBuilder.Append(string.Format("<b>{0} ({1}: {2})</b><br><br>", Strings.ElcEmailSoftDeletedItems, Strings.ElcEmailColumnTotal, keyValuePair.Value.Count));
								break;
							case RetentionActionType.PermanentlyDelete:
								stringBuilder.Append(string.Format("<b>{0} ({1}: {2})</b><br><br>", Strings.ElcEmailHardDeletedItems, Strings.ElcEmailColumnTotal, keyValuePair.Value.Count));
								break;
							case RetentionActionType.MoveToArchive:
								stringBuilder.Append(string.Format("<b>{0} ({1}: {2})</b><br><br>", Strings.ElcEmailArchivedItems, Strings.ElcEmailColumnTotal, keyValuePair.Value.Count));
								break;
							}
							if (keyValuePair.Value.Count != 0)
							{
								stringBuilder.Append(string.Format("<table cellpadding=8 cellspacing=1 border=1 rules=rows frame=below width=90%><tr><td align=left><b>{0}</b></td><td align=left><b>{1}</b></td><td align=left><b>{2}</b></td><td align=left><b>{3}</b></td><td align=left><b>{4}</b></td><td align=left><b>{5}</b></td><td align=left><b>{6}</b></td><td align=left><b>{7}</b></td></tr>", new object[]
								{
									Strings.ElcEmailSubjectColumn,
									Strings.ElcEmailFromColumn,
									Strings.ElcEmailFolderColumn,
									Strings.ElcEmailPolicyTagColumn,
									Strings.ElcEmailAdditionalTagColumn,
									Strings.ElcEmailReceivedColumn,
									Strings.ElcEmailModifiedColumn,
									Strings.ElcEmailItemsAffected
								}));
								foreach (List<object> list in keyValuePair.Value)
								{
									string text = list[3].ToString();
									if (!dictionary.ContainsKey(text))
									{
										dictionary.Add(text, (int)list[8]);
									}
									else
									{
										dictionary[text] += (int)list[8];
									}
									stringBuilder.Append("<tr>");
									for (int i = 1; i < list.Count; i++)
									{
										text = list[i].ToString();
										if (i == 1)
										{
											stringBuilder.Append(string.Format("<td width=20%>{0}</td>", text));
										}
										else if (i == 4 || i == 5)
										{
											stringBuilder.Append(string.Format("<td width=15%>{0}</td>", text));
										}
										else if (i == 8)
										{
											stringBuilder.Append(string.Format("<td align=center width=2%>{0}</td>", text));
										}
										else
										{
											stringBuilder.Append(string.Format("<td width=" + (48 / (list.Count - 5)).ToString() + "%>{0}</td>", text));
										}
									}
									stringBuilder.Append("</tr>");
								}
								stringBuilder.Append("</table>");
							}
							stringBuilder.Append("<br><br><br>");
						}
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.Append(string.Format("<b>{0}</b><br><br>", Strings.ElcEmailFolderReport));
						stringBuilder2.Append("<table border=0 width=" + ((dictionary.Count > 5) ? 90 : (dictionary.Count * 15)).ToString() + "%>");
						string text2 = "";
						int num = 0;
						foreach (KeyValuePair<string, int> keyValuePair2 in dictionary)
						{
							if (num != 0 && num % 6 == 0)
							{
								stringBuilder2.Append("<tr>" + text2 + "</tr>");
								text2 = "";
							}
							object obj = text2;
							text2 = string.Concat(new object[]
							{
								obj,
								"<td align=left width=15%>",
								keyValuePair2.Key,
								" <b>(",
								keyValuePair2.Value,
								")</b></td>"
							});
							num++;
						}
						stringBuilder2.Append("<tr>" + text2 + "</tr>");
						stringBuilder2.Append("</table></br></br></br></br>");
						StringBuilder stringBuilder3 = new StringBuilder();
						stringBuilder3.Append(string.Format("<html><body><font face=\"helvetica\"><h3>{0}</h3><br>", Strings.ElcEmailIntro));
						if (tagExpirationExecutor.elcReportOverflow)
						{
							stringBuilder3.Append(string.Format("<b>{0}</b><br></br><br>", Strings.ElcEmailReportOverflow(ElcGlobals.ReportItemLimit.ToString())));
						}
						stringBuilder3.Append(stringBuilder2.ToString());
						stringBuilder3.Append(stringBuilder.ToString());
						stringBuilder3.Append(string.Format("</br></br><b>{0}</b><br><br>", Strings.ElcEmailToMeCcMe));
						stringBuilder3.Append(string.Format("<b>{0}</b><br><br>", Strings.ElcEmailConversationExplanation));
						using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
						{
							textWriter.Write(stringBuilder3.ToString());
						}
						messageItem.SendWithoutSavingMessage();
						goto IL_659;
					}
				}
				if (!tagExpirationExecutor.isReportEnabled)
				{
					ElcReporting.Tracer.TraceDebug<ElcReporting>((long)this.GetHashCode(), "{0}: ELC Reporting is switched off", this);
				}
				IL_659:;
			}
			catch (StoragePermanentException ex)
			{
				ElcReporting.Tracer.TraceError<ElcReporting>((long)this.GetHashCode(), "{0}: Permanent Storage Exception while sending the report on policies: " + ex.Message, this);
			}
			catch (StorageTransientException ex2)
			{
				ElcReporting.Tracer.TraceError<ElcReporting>((long)this.GetHashCode(), "{0}: Transient Storage Exception while sending the report on policies: " + ex2.Message, this);
			}
			catch (DataSourceOperationException ex3)
			{
				ElcReporting.Tracer.TraceError<ElcReporting>((long)this.GetHashCode(), "{0}: Object not found exception while retrieving the MS Exchange account: " + ex3.Message, this);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_MSExchangeSystemAccountRetrieval, null, new object[]
				{
					Process.GetCurrentProcess().Id,
					ex3.Message
				});
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000108F8 File Offset: 0x0000EAF8
		private void CompleteReport(TagExpirationExecutor tagExpirationExecutor, RetentionActionType retention)
		{
			if (!tagExpirationExecutor.ELCReport.ContainsKey(retention))
			{
				tagExpirationExecutor.ELCReport.Add(retention, new List<List<object>>());
			}
		}

		// Token: 0x04000248 RID: 584
		private MailboxSession session;

		// Token: 0x04000249 RID: 585
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.ElcReportingTracer;
	}
}
