using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A6 RID: 422
	internal sealed class GroupMetricsMailboxFileStore : DirectoryMailboxFileStore
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0006166A File Offset: 0x0005F86A
		private GroupMetricsMailboxFileStore(OrganizationId orgId, Guid mbxGuid) : base(orgId, mbxGuid, "GroupMetrics")
		{
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0006167C File Offset: 0x0005F87C
		public static GroupMetricsMailboxFileStore FromMailboxGuid(OrganizationId orgId, Guid mbxGuid)
		{
			GroupMetricsMailboxFileStore result = null;
			try
			{
				result = new GroupMetricsMailboxFileStore(orgId, mbxGuid);
			}
			catch (Exception ex)
			{
				UmGlobals.ExEvent.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToAccessOrganizationMailbox, null, new object[]
				{
					orgId,
					mbxGuid,
					CommonUtil.ToEventLogString(ex)
				});
				if (!GroupMetricsMailboxFileStore.IsExpectedException(ex))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000616E4 File Offset: 0x0005F8E4
		public void UploadCookies(IList<string> filePathList)
		{
			ValidateArgument.NotNull(filePathList, "filePathList");
			if (filePathList.Count == 0)
			{
				GroupMetricsMailboxFileStore.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.UploadCookie - skipping upload due to empty cookie path list");
				return;
			}
			GroupMetricsMailboxFileStore.Tracer.TraceDebug<int>((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.UploadCookie - {0} cookies to be uploaded", filePathList.Count);
			using (MailboxSession mailboxSession = this.TryGetMailboxSession())
			{
				if (mailboxSession == null)
				{
					GroupMetricsMailboxFileStore.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.UploadCookie - skipping upload due to null session");
				}
				foreach (string text in filePathList)
				{
					try
					{
						GroupMetricsMailboxFileStore.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.UploadCookie - uploading cookie {0}", text);
						base.UploadFile(text, text, mailboxSession);
					}
					catch (Exception ex)
					{
						UmGlobals.ExEvent.LogEvent(InfoWorkerEventLogConstants.Tuple_UploadGroupMetricsCookieFailed, null, new object[]
						{
							base.OrgId,
							text,
							CommonUtil.ToEventLogString(ex)
						});
						if (!GroupMetricsMailboxFileStore.IsExpectedException(ex))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00061814 File Offset: 0x0005FA14
		public bool DownloadCookies(ICollection<string> pathCollection)
		{
			ValidateArgument.NotNull(pathCollection, "pathCollection");
			if (pathCollection.Count == 0)
			{
				GroupMetricsMailboxFileStore.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.DownloadCookies - skipping download due to empty cookie path collection");
				return false;
			}
			bool result = true;
			using (MailboxSession mailboxSession = this.TryGetMailboxSession())
			{
				if (mailboxSession == null)
				{
					GroupMetricsMailboxFileStore.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.DownloadCookies - skipping download due to null session");
					return false;
				}
				foreach (string text in pathCollection)
				{
					try
					{
						string text2 = base.DownloadLatestFile(text, DateTime.MinValue, mailboxSession);
						if (text2 != null)
						{
							GroupMetricsMailboxFileStore.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.DownloadCookie - Copying downloaded cookie '{0}' to '{1}'", text2, text);
							string directoryName = Path.GetDirectoryName(text);
							Directory.CreateDirectory(directoryName);
							try
							{
								File.Copy(text2, text, true);
								continue;
							}
							finally
							{
								base.DeleteFolder(Path.GetDirectoryName(text2));
							}
						}
						result = false;
					}
					catch (Exception ex)
					{
						UmGlobals.ExEvent.LogEvent(InfoWorkerEventLogConstants.Tuple_DownloadGroupMetricsCookieFailed, null, new object[]
						{
							base.OrgId,
							text,
							CommonUtil.ToEventLogString(ex)
						});
						if (!GroupMetricsMailboxFileStore.IsExpectedException(ex))
						{
							throw;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00061984 File Offset: 0x0005FB84
		public void DeleteCookie(string path)
		{
			using (MailboxSession mailboxSession = this.TryGetMailboxSession())
			{
				if (mailboxSession == null)
				{
					GroupMetricsMailboxFileStore.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMetricsMailboxFileStore.DeleteCookie - skipping due to null session");
				}
				else
				{
					try
					{
						base.DeleteFileFromMailbox(path, mailboxSession);
					}
					catch (Exception ex)
					{
						UmGlobals.ExEvent.LogEvent(InfoWorkerEventLogConstants.Tuple_DeleteGroupMetricsCookieFailed, null, new object[]
						{
							base.OrgId,
							path,
							CommonUtil.ToEventLogString(ex)
						});
						if (!GroupMetricsMailboxFileStore.IsExpectedException(ex))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00061A24 File Offset: 0x0005FC24
		private static bool IsExpectedException(Exception e)
		{
			return e is IOException || e is UnauthorizedAccessException || e is LocalizedException;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00061A44 File Offset: 0x0005FC44
		private MailboxSession TryGetMailboxSession()
		{
			MailboxSession result = null;
			try
			{
				result = DirectoryMailboxFileStore.GetMailboxSession(base.OrgId, base.MailboxGuid);
			}
			catch (Exception ex)
			{
				UmGlobals.ExEvent.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToAccessOrganizationMailbox, null, new object[]
				{
					base.OrgId,
					base.MailboxGuid,
					CommonUtil.ToEventLogString(ex)
				});
				if (!GroupMetricsMailboxFileStore.IsExpectedException(ex))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x04000A76 RID: 2678
		internal const string FolderName = "GroupMetrics";

		// Token: 0x04000A77 RID: 2679
		private static readonly Trace Tracer = GroupMetricsUtility.Tracer;
	}
}
