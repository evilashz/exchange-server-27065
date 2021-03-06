using System;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000157 RID: 343
	internal class MessageSearcher : IDisposable
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000395FE File Offset: 0x000377FE
		internal Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00039608 File Offset: 0x00037808
		internal MessageSearcher(MapiStore store, SearchTestResult searchResult, MonitorHelper monitor, StopClass threadExit)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (searchResult == null)
			{
				throw new ArgumentNullException("searchResult");
			}
			this.store = store;
			this.searchResult = searchResult;
			this.databaseGuid = searchResult.DatabaseGuid;
			this.searchString = DateTime.UtcNow.Ticks.ToString();
			this.monitor = monitor;
			this.threadExit = threadExit;
			this.ReadSleepTestHook();
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00039690 File Offset: 0x00037890
		void IDisposable.Dispose()
		{
			TestSearch.TestSearchTracer.TraceDebug((long)this.GetHashCode(), "Disposing MessageSearcher");
			if (!this.disposed)
			{
				lock (this)
				{
					if (this.folderId != null)
					{
						this.parentFolder.DeleteFolder(this.folderId, DeleteFolderFlags.DeleteMessages | DeleteFolderFlags.DelSubFolders | DeleteFolderFlags.ForceHardDelete);
						this.folderId = null;
					}
					if (this.searchFolder != null)
					{
						this.searchFolder.Dispose();
						this.searchFolder = null;
					}
					if (this.testFolder != null)
					{
						this.testFolder.Dispose();
						this.testFolder = null;
					}
					if (this.parentFolder != null)
					{
						this.parentFolder.Dispose();
						this.parentFolder = null;
					}
					if (this.store != null)
					{
						this.store.Dispose();
						this.store = null;
					}
					this.disposed = true;
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00039780 File Offset: 0x00037980
		private uint CreateMsgWithAttachement(out byte[] entryId)
		{
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			uint @int;
			using (MapiMessage mapiMessage = this.testFolder.CreateMessage())
			{
				PropValue[] props = new PropValue[]
				{
					new PropValue(PropTag.Subject, string.Format("CITestSearch: {0}.", this.searchString)),
					new PropValue(PropTag.Body, string.Format("The unique search string in the body is: {0}.", this.searchString)),
					new PropValue(PropTag.MessageDeliveryTime, (DateTime)ExDateTime.Now)
				};
				this.threadExit.CheckStop();
				mapiMessage.SetProps(props);
				int num;
				using (MapiAttach mapiAttach = mapiMessage.CreateAttach(out num))
				{
					string s = string.Format("This is a test msg created by test-search task (MSExchangeSearch {0}).It will be deleted soon...", this.searchString);
					byte[] bytes = asciiencoding.GetBytes(s);
					using (MapiStream mapiStream = mapiAttach.OpenStream(PropTag.AttachDataBin, OpenPropertyFlags.Create))
					{
						mapiStream.Write(bytes, 0, bytes.Length);
						mapiStream.Flush();
						this.threadExit.CheckStop();
					}
					props = new PropValue[]
					{
						new PropValue(PropTag.AttachFileName, "CITestSearch.txt"),
						new PropValue(PropTag.AttachMethod, AttachMethods.ByValue)
					};
					mapiAttach.SetProps(props);
					mapiAttach.SaveChanges();
				}
				this.threadExit.CheckStop();
				mapiMessage.SaveChanges();
				entryId = mapiMessage.GetProp(PropTag.EntryId).GetBytes();
				@int = (uint)mapiMessage.GetProp(PropTag.DocumentId).GetInt();
			}
			return @int;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00039990 File Offset: 0x00037B90
		private void SearchMapiNotificationHandler(MapiNotification notification)
		{
			if (notification.NotificationType == AdviseFlags.SearchComplete)
			{
				this.SearchComplete.Set();
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000399AC File Offset: 0x00037BAC
		private bool FoundMessage(int docIdToLookFor)
		{
			this.AddMonitoringEvent(this.searchResult, Strings.TestSearchFindMessage(this.searchResult.Database, this.searchResult.Mailbox));
			try
			{
				using (MapiTable contentsTable = this.searchFolder.GetContentsTable(ContentsTableFlags.DeferredErrors))
				{
					this.threadExit.CheckStop();
					Restriction restriction = new Restriction.PropertyRestriction(Restriction.RelOp.Equal, PropTag.DocumentId, docIdToLookFor);
					if (contentsTable.QueryOneValue(PropTag.DocumentId, restriction) != null)
					{
						return true;
					}
				}
			}
			catch (MapiExceptionNotFound)
			{
				return false;
			}
			return false;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00039A50 File Offset: 0x00037C50
		private bool SearchSucceeded(Restriction res)
		{
			bool result;
			lock (this)
			{
				if (res == null)
				{
					throw new ArgumentNullException("res");
				}
				this.threadExit.CheckStop();
				byte[] bytes = this.searchFolder.GetProp(PropTag.EntryId).GetBytes();
				this.searchFolder.GetProp(PropTag.ContainerClass).GetString();
				this.SearchComplete.Reset();
				this.threadExit.CheckStop();
				MapiNotificationHandle mapiNotificationHandle = this.store.Advise(bytes, AdviseFlags.SearchComplete, new MapiNotificationHandler(this.SearchMapiNotificationHandler), (MapiNotificationClientFlags)0);
				try
				{
					this.threadExit.CheckStop();
					this.searchFolder.SetSearchCriteria(res, new byte[][]
					{
						this.folderId
					}, SearchCriteriaFlags.Restart | SearchCriteriaFlags.Foreground);
					this.threadExit.CheckStop();
					bool flag2 = this.SearchComplete.WaitOne(MessageSearcher.SearchCompleteTimeout, true);
					if (flag2)
					{
						this.threadExit.CheckStop();
						Thread.Sleep(this.testSearchStallInSeconds);
						return this.FoundMessage((int)this.documentId);
					}
				}
				finally
				{
					if (mapiNotificationHandle != null)
					{
						this.store.Unadvise(mapiNotificationHandle);
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00039BB8 File Offset: 0x00037DB8
		private void DeleteOldTestFolders()
		{
			using (MapiTable hierarchyTable = this.parentFolder.GetHierarchyTable())
			{
				PropValue[][] array = hierarchyTable.QueryAllRows(Restriction.Content(PropTag.DisplayName, "test-exchangesearch-folder-", ContentFlags.Prefix), new PropTag[]
				{
					PropTag.EntryId
				});
				foreach (PropValue[] array3 in array)
				{
					this.threadExit.CheckStop();
					this.parentFolder.DeleteFolder(array3[0].GetBytes(), DeleteFolderFlags.DeleteMessages | DeleteFolderFlags.DelSubFolders | DeleteFolderFlags.ForceHardDelete);
				}
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00039C54 File Offset: 0x00037E54
		internal void InitializeSearch()
		{
			lock (this)
			{
				this.threadExit.CheckStop();
				this.AddMonitoringEvent(this.searchResult, Strings.TestSearchCurrentMailbox(this.searchResult.Mailbox));
				this.AddMonitoringEvent(this.searchResult, Strings.TestSearchGetNonIpmSubTreeFolder(this.searchResult.Database, this.searchResult.Mailbox));
				this.parentFolder = this.store.GetNonIpmSubtreeFolder();
				this.threadExit.CheckStop();
				Thread.Sleep(this.testInitStallInSeconds);
				this.DeleteOldTestFolders();
				string folderName = "test-exchangesearch-folder-" + Guid.NewGuid();
				this.threadExit.CheckStop();
				this.AddMonitoringEvent(this.searchResult, Strings.TestSearchCreateFolder(this.searchResult.Database, this.searchResult.Mailbox));
				this.testFolder = this.parentFolder.CreateFolder(folderName, null, true);
				this.folderId = this.testFolder.GetProp(PropTag.EntryId).GetBytes();
				this.threadExit.CheckStop();
				this.testFolder.EmptyFolder(EmptyFolderFlags.ForceHardDelete);
				this.threadExit.CheckStop();
				this.AddMonitoringEvent(this.searchResult, Strings.TestSearchCreateMessage(this.searchResult.Database, this.searchResult.Mailbox));
				byte[] entryId;
				this.documentId = this.CreateMsgWithAttachement(out entryId);
				this.searchResult.EntryId = entryId;
				this.searchResult.DocumentId = this.documentId;
				long ticks = ExDateTime.Now.LocalTime.Ticks;
				string folderName2 = string.Format("test-{0}", ticks);
				this.threadExit.CheckStop();
				this.AddMonitoringEvent(this.searchResult, Strings.TestSearchCreateSearchFolder(this.searchResult.Database, this.searchResult.Mailbox));
				this.searchFolder = this.testFolder.CreateSearchFolder(folderName2, "", false);
				this.threadExit.CheckStop();
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00039E84 File Offset: 0x00038084
		internal bool DoSearch()
		{
			return this.SearchSucceeded(Restriction.Content(PropTag.Subject, this.searchString, ContentFlags.Prefix));
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00039EAC File Offset: 0x000380AC
		private void AddMonitoringEvent(SearchTestResult result, LocalizedString msg)
		{
			if (this.monitor != null)
			{
				this.monitor.AddMonitoringEvent(result, msg);
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00039EC4 File Offset: 0x000380C4
		private void ReadSleepTestHook()
		{
			RegistryKey registryKey2;
			RegistryKey registryKey = registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExSearch\\TestHook", RegistryKeyPermissionCheck.ReadSubTree);
			try
			{
				if (registryKey != null)
				{
					this.testInitStallInSeconds = (int)registryKey.GetValue("MessageSearcherInitStallInSeconds", 0);
					this.testSearchStallInSeconds = (int)registryKey.GetValue("MessageSearcherSearchStallInSeconds", 0);
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
		}

		// Token: 0x04000612 RID: 1554
		private const string TestFolderNamePrefix = "test-exchangesearch-folder-";

		// Token: 0x04000613 RID: 1555
		private const string TestHookKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExSearch\\TestHook";

		// Token: 0x04000614 RID: 1556
		private const string InitStallKey = "MessageSearcherInitStallInSeconds";

		// Token: 0x04000615 RID: 1557
		private const string SearchStallKey = "MessageSearcherSearchStallInSeconds";

		// Token: 0x04000616 RID: 1558
		private MapiStore store;

		// Token: 0x04000617 RID: 1559
		private SearchTestResult searchResult;

		// Token: 0x04000618 RID: 1560
		private MapiFolder parentFolder;

		// Token: 0x04000619 RID: 1561
		private MapiFolder testFolder;

		// Token: 0x0400061A RID: 1562
		private MapiFolder searchFolder;

		// Token: 0x0400061B RID: 1563
		private readonly Guid databaseGuid;

		// Token: 0x0400061C RID: 1564
		private byte[] folderId;

		// Token: 0x0400061D RID: 1565
		private uint documentId;

		// Token: 0x0400061E RID: 1566
		private readonly string searchString;

		// Token: 0x0400061F RID: 1567
		private MonitorHelper monitor;

		// Token: 0x04000620 RID: 1568
		private StopClass threadExit;

		// Token: 0x04000621 RID: 1569
		private int testInitStallInSeconds;

		// Token: 0x04000622 RID: 1570
		private int testSearchStallInSeconds;

		// Token: 0x04000623 RID: 1571
		private static TimeSpan SearchCompleteTimeout = new TimeSpan(0, 0, 2);

		// Token: 0x04000624 RID: 1572
		private ManualResetEvent SearchComplete = new ManualResetEvent(false);

		// Token: 0x04000625 RID: 1573
		private bool disposed;
	}
}
