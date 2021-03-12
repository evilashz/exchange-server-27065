using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Ceres.CoreServices.Admin;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Ceres.SearchCore.Admin;
using Microsoft.Ceres.SearchCore.Admin.Clustering;
using Microsoft.Ceres.SearchCore.Admin.Config;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Ceres.SearchCore.Services.GenerationController;
using Microsoft.Ceres.SearchCore.Services.Indexes.FastServerIndex;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200001D RID: 29
	internal class IndexManager : FastManagementClient, IIndexManager
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000A374 File Offset: 0x00008574
		protected IndexManager()
		{
			base.DiagnosticsSession.ComponentName = "IndexManager";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.IndexManagementTracer;
			this.InitializeIndexModels();
			base.ConnectManagementAgents();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public static IndexManager Instance
		{
			get
			{
				if (Interlocked.CompareExchange<Hookable<IndexManager>>(ref IndexManager.hookableInstance, null, null) == null)
				{
					lock (IndexManager.lockObject)
					{
						if (IndexManager.hookableInstance == null)
						{
							Hookable<IndexManager> hookable = Hookable<IndexManager>.Create(true, new IndexManager());
							Thread.MemoryBarrier();
							IndexManager.hookableInstance = hookable;
						}
					}
				}
				return IndexManager.hookableInstance.Value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000A424 File Offset: 0x00008624
		protected virtual IAdminServiceManagementAgent AdminService
		{
			get
			{
				return this.adminService;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000A42E File Offset: 0x0000862E
		protected virtual IIndexClusterManagementAgent ClusterService
		{
			get
			{
				return this.clusterService;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000A438 File Offset: 0x00008638
		protected virtual IIndexManagement IndexService
		{
			get
			{
				return this.indexService;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000A442 File Offset: 0x00008642
		protected override int ManagementPortOffset
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000A445 File Offset: 0x00008645
		protected NodeManagementClient NodeClient
		{
			get
			{
				return NodeManagementClient.Instance;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000A4CC File Offset: 0x000086CC
		public void TriggerMasterMerge(string indexName)
		{
			base.PerformFastOperation(delegate()
			{
				using (SystemClient systemClient = this.ConnectSystem())
				{
					IGenerationControllerAgent managementAgentClient = systemClient.GetManagementAgentClient<IGenerationControllerAgent>("AdminNode1", "AdminNode1.GenerationController");
					IFastServerAgent managementAgentClient2 = systemClient.GetManagementAgentClient<IFastServerAgent>("IndexNode1", indexName + "Single.FastServer.FSIndex");
					managementAgentClient.TriggerCheckpoint(indexName);
					managementAgentClient2.TriggerMasterMerge();
				}
			}, "Trigger MasterMerge using the IFastServerAgent");
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000A544 File Offset: 0x00008744
		public void UpdateConfiguration()
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "UpdateConfiguration called", new object[0]);
			base.PerformFastOperation(delegate()
			{
				if (!this.AdminService.IsPendingReconfiguration)
				{
					this.AdminService.UpdateConfiguration();
				}
			}, "UpdateConfiguration");
			base.PerformFastOperation(delegate()
			{
				if (this.CheckForNoPendingConfigurationUpdate())
				{
					this.NodeClient.CheckForAllNodesHealthy(true);
					base.ConnectManagementAgents();
					return;
				}
				throw new UpdateConfigurationFailedException();
			}, "Post UpdateConfiguration Checks");
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A5BC File Offset: 0x000087BC
		public void CreateCatalog(string indexName, string databasePath, bool databaseCopyActive, RefinerUsage refinersToEnable)
		{
			base.DiagnosticsSession.TraceDebug<string, string>("Add-IndexSystem - IndexName:{0}, Mode:{1}", indexName, this.IndexSystemMode(databaseCopyActive, false));
			List<string> nodeNames = null;
			if (!string.IsNullOrEmpty(databasePath) && !Directory.Exists(databasePath))
			{
				throw new DatabasePathDoesNotExist(databasePath);
			}
			base.PerformFastOperation(delegate()
			{
				nodeNames = this.NodeClient.GetNodeNamesBasedOnRole("Index");
			}, "CreateCatalog");
			if (nodeNames != null && nodeNames.Count > 0)
			{
				nodeNames.Sort();
				this.CreateIndexSystem(indexName, nodeNames[0], databasePath, databaseCopyActive, refinersToEnable);
				return;
			}
			throw new NoFASTNodesFoundException();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A690 File Offset: 0x00008890
		public void RemoveCatalog(string indexName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Remove-IndexSystem - IndexName:{0}", new object[]
			{
				indexName
			});
			base.PerformFastOperation(delegate()
			{
				this.WaitForOperationFinished(this.IndexService.DeleteIndexSystem(indexName));
			}, "DeleteIndexSystem");
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A718 File Offset: 0x00008918
		public void ResetCatalog(string indexName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Reset-IndexSystem - IndexName:{0}", new object[]
			{
				indexName
			});
			base.PerformFastOperation(delegate()
			{
				this.WaitForOperationFinished(this.IndexService.ResetIndexSystem(indexName));
			}, "ResetIndexSystem");
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A814 File Offset: 0x00008A14
		public bool EnsureCatalog(string indexName, bool databaseCopyActive, bool suspended, RefinerUsage refinersToEnable)
		{
			base.DiagnosticsSession.TraceDebug<string, string>("Attempt to Update-IndexSystem - IndexName:{0}, Mode:{1}", indexName, this.IndexSystemMode(databaseCopyActive, suspended));
			if (this.UpdateIndexSystem(indexName, delegate(IndexSystemModel model)
			{
				IndexSystemOptions options = new IndexSystemOptions(model.Options)
				{
					JournalBufferSize = FastManagementClient.Config.JournalBufferSize,
					ImportIndex = false,
					TriggerReseedOnDocsumLookupFailure = FastManagementClient.Config.TriggerReseedOnDocsumLookupFailure,
					InvalidateOnSeedingReadFailure = FastManagementClient.Config.InvalidateOnSeedingReadFailure
				};
				model.Options = options;
				model.Schema = FastIndexSystemSchema.GetIndexSystemSchema(refinersToEnable);
				this.SetIndexSystemIndex(model, databaseCopyActive);
				if (databaseCopyActive)
				{
					model.Suspended = false;
					return;
				}
				model.Suspended = suspended;
			}))
			{
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Update-IndexSystem - IndexName:{0}, Mode:{1}", new object[]
				{
					indexName,
					this.IndexSystemMode(databaseCopyActive, suspended)
				});
				return true;
			}
			return false;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		public string GetTransportIndexSystem()
		{
			foreach (string text in this.GetCatalogs())
			{
				if (FastIndexVersion.GetIndexSystemDatabaseGuid(text) != Guid.Empty)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000A928 File Offset: 0x00008B28
		public bool SuspendCatalog(string indexName)
		{
			base.DiagnosticsSession.TraceDebug<string>("Attempt to Suspend-IndexSystem - IndexName:{0}", indexName);
			if (this.UpdateIndexSystem(indexName, delegate(IndexSystemModel model)
			{
				model.Suspended = true;
			}))
			{
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Suspend-IndexSystem - IndexName:{0}", new object[]
				{
					indexName
				});
				return true;
			}
			return false;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A998 File Offset: 0x00008B98
		public bool ResumeCatalog(string indexName)
		{
			base.DiagnosticsSession.TraceDebug<string>("Attempt to Resume-IndexSystem - IndexName:{0}", indexName);
			if (this.UpdateIndexSystem(indexName, delegate(IndexSystemModel model)
			{
				model.Suspended = false;
			}))
			{
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Resume-IndexSystem - IndexName:{0}", new object[]
				{
					indexName
				});
				return true;
			}
			return false;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000AA28 File Offset: 0x00008C28
		public void FlushCatalog(string indexName)
		{
			base.DiagnosticsSession.TraceDebug<string>("Attempt to Flush-IndexSystem:{0}", indexName);
			base.PerformFastOperation(delegate()
			{
				this.WaitForOperationFinished(this.IndexService.ForceCheckpoint(indexName));
			}, "Flush-IndexSystem");
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public bool CatalogExists(string indexName)
		{
			base.DiagnosticsSession.TraceDebug<string>("IndexSystem-Exists? - IndexName:{0}", indexName);
			return this.PerformFastOperation<bool>(() => this.IndexService.IndexSystemNames.Contains(indexName), "CatalogExists");
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public CatalogState GetCatalogState(string indexName, out string seedingSource, out int? failureCode, out string failureReason)
		{
			seedingSource = null;
			failureCode = null;
			failureReason = null;
			IndexClusterMemberInfo clusterMemberInfo = this.GetClusterMemberInfo(indexName);
			CatalogState indexingState = this.GetIndexingState(indexName, clusterMemberInfo, null);
			if (indexingState == CatalogState.Seeding)
			{
				seedingSource = clusterMemberInfo.IndexingState.SeedingState.SeedingSource;
			}
			if (clusterMemberInfo != null && clusterMemberInfo.IndexingState != null && clusterMemberInfo.IndexingState.Reason != null)
			{
				failureCode = new int?(clusterMemberInfo.IndexingState.Reason.Code);
				failureReason = clusterMemberInfo.IndexingState.Reason.Reason;
			}
			return indexingState;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000AB88 File Offset: 0x00008D88
		public HashSet<string> GetCatalogs()
		{
			base.DiagnosticsSession.TraceDebug("Get-IndexSystems", new object[0]);
			return this.PerformFastOperation<HashSet<string>>(() => new HashSet<string>(this.IndexService.IndexSystemNames), "GetCatalogs");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public XElement GetIndexSystemsDiagnostics()
		{
			XElement xelement = new XElement("IndexSystems");
			foreach (string text in this.GetCatalogs())
			{
				IndexSystemModel indexSystemModel = this.IndexService.GetIndexSystemModel(text);
				IndexClusterMemberInfo clusterMemberInfo = this.GetClusterMemberInfo(text);
				CatalogState indexingState = this.GetIndexingState(text, clusterMemberInfo, indexSystemModel);
				string content = null;
				string content2 = null;
				string content3 = null;
				string content4 = null;
				int? num = null;
				string content5 = null;
				if (indexSystemModel != null)
				{
					content = this.GetRootDirectory(text);
					if (clusterMemberInfo != null)
					{
						content2 = string.Format("{0}.{1}.{2}", indexSystemModel.Name, indexSystemModel.SequenceNumber, clusterMemberInfo.Name);
						if (clusterMemberInfo.IndexingState != null && clusterMemberInfo.IndexingState.Reason != null)
						{
							num = new int?(clusterMemberInfo.IndexingState.Reason.Code);
							content5 = clusterMemberInfo.IndexingState.Reason.Reason;
						}
					}
				}
				if (indexingState == CatalogState.Seeding)
				{
					content3 = clusterMemberInfo.IndexingState.SeedingState.SeedingSource;
					content4 = clusterMemberInfo.IndexingState.SeedingState.SeedingTarget;
				}
				XElement xelement2 = new XElement("IndexSystem");
				xelement2.Add(new XElement("Name", text));
				xelement2.Add(new XElement("IndexingState", indexingState));
				xelement2.Add(new XElement("RootDirectory", content));
				xelement2.Add(new XElement("Directory", content2));
				xelement2.Add(new XElement("SeedingSource", content3));
				xelement2.Add(new XElement("SeedingTarget", content4));
				xelement2.Add(new XElement("FailureCode", num));
				xelement2.Add(new XElement("FailureReason", content5));
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IndexManager>(this);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		public string GetRootDirectory(string indexSystemName)
		{
			IndexSystemModel indexSystemModel = this.IndexService.GetIndexSystemModel(indexSystemName);
			if (indexSystemModel == null)
			{
				return null;
			}
			string path;
			if (indexSystemModel.Options.RootDirectory.StartsWith("/root/"))
			{
				path = indexSystemModel.Options.RootDirectory.Substring("/root/".Length);
			}
			else
			{
				path = indexSystemModel.Options.RootDirectory;
			}
			return Path.GetFullPath(path);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000AE4C File Offset: 0x0000904C
		public bool IsCatalogSuspended(Guid mdbGuid)
		{
			string indexSystemName = FastIndexVersion.GetIndexSystemName(mdbGuid);
			IndexSystemModel indexSystemModel = this.GetIndexSystemModel(indexSystemName);
			return indexSystemModel.Suspended;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000AE6E File Offset: 0x0000906E
		internal static IDisposable SetInstanceTestHook(IndexManager mockIndexManager)
		{
			if (IndexManager.hookableInstance == null)
			{
				IndexManager instance = IndexManager.Instance;
			}
			return IndexManager.hookableInstance.SetTestHook(mockIndexManager);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000AE98 File Offset: 0x00009098
		internal bool CheckForNoPendingConfigurationUpdate()
		{
			bool result = false;
			for (int i = 0; i < 120; i++)
			{
				if (!this.PerformFastOperation<bool>(() => this.AdminService.IsPendingReconfiguration, "CheckIsPendingReconfiguration"))
				{
					result = true;
					break;
				}
				Thread.Sleep(1000);
			}
			return result;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000AEE4 File Offset: 0x000090E4
		internal string[] GetIndexSystemModelIndexesOptions(string indexName)
		{
			IndexSystemModel indexSystemModel = this.GetIndexSystemModel(indexName);
			IIndexSystemIndex[] indexes = indexSystemModel.Indexes;
			int num = 0;
			if (num >= indexes.Length)
			{
				return null;
			}
			IIndexSystemIndex indexSystemIndex = indexes[num];
			return indexSystemIndex.Options;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000AF40 File Offset: 0x00009140
		internal IndexSystemModel GetIndexSystemModel(string indexName)
		{
			return this.PerformFastOperation<IndexSystemModel>(() => this.IndexService.GetIndexSystemModel(indexName), "GetIndexSystemModel");
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000AF7A File Offset: 0x0000917A
		protected override void InternalConnectManagementAgents(WcfManagementClient client)
		{
			this.adminService = client.GetManagementAgent<IAdminServiceManagementAgent>("AdminService");
			this.clusterService = client.GetManagementAgent<IIndexClusterManagementAgent>("IndexClusterManager");
			this.indexService = client.GetManagementAgent<IIndexManagement>("IndexController");
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000AFB8 File Offset: 0x000091B8
		private void InitializeIndexModels()
		{
			List<string> list = new List<string>(4);
			if (FastManagementClient.Config.EnableIndexPartsCache)
			{
				list.Add("disk_parts_memfs_max_total=" + FastManagementClient.Config.IndexPartsMaxCacheSize);
				list.Add("disk_parts_memfs_max_one=" + FastManagementClient.Config.MaxCacheSizePerIndexPart);
			}
			this.defaultModelIndexes = this.GetIndexSystemIndex(list);
			list.Add("query_mode=full");
			this.activeModelIndexes = this.GetIndexSystemIndex(list);
			list.Clear();
			list.Add("query_mode=simple");
			this.passiveModelIndexes = this.GetIndexSystemIndex(list);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B05C File Offset: 0x0000925C
		private IIndexSystemIndex[] GetIndexSystemIndex(List<string> additionalOptionsToAdd)
		{
			List<string> list = new List<string>(IndexManager.defaultOptions);
			if (FastManagementClient.Config.MergeOverrideFolderUpdateGroup)
			{
				list.AddRange(IndexManager.overrideOptions);
			}
			if (FastManagementClient.Config.CachePreWarmingEnabled)
			{
				list.Add("cache_distribution=" + FastManagementClient.Config.CachePreWarmingSubCacheSizes);
			}
			if (additionalOptionsToAdd != null)
			{
				list.AddRange(additionalOptionsToAdd);
			}
			if (FastManagementClient.Config.EnableSingleValueRefiners)
			{
				list.Add("support_single_value_refiners=1");
				list.Add("support_default_values_for_single_value_refiners=1");
			}
			return new IndexSystemIndex[]
			{
				new IndexSystemIndex
				{
					Name = "FSIndex",
					Type = 1,
					Options = list.ToArray()
				}
			};
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000B110 File Offset: 0x00009310
		private CatalogState GetIndexingState(string indexName, IndexClusterMemberInfo indexClusterMemberInfo, IndexSystemModel indexModel)
		{
			if (indexClusterMemberInfo == null)
			{
				if (indexModel == null)
				{
					indexModel = this.GetIndexSystemModel(indexName);
				}
				if (indexModel == null || !indexModel.Suspended)
				{
					return CatalogState.Unknown;
				}
				return CatalogState.Suspended;
			}
			else
			{
				if (indexClusterMemberInfo.IndexingState == null)
				{
					return CatalogState.Unknown;
				}
				if (indexClusterMemberInfo.IndexingState.IsInvalid)
				{
					return CatalogState.Failed;
				}
				if (indexClusterMemberInfo.IndexingState.SeedingState.SeedingSource != null)
				{
					return CatalogState.Seeding;
				}
				return CatalogState.Healthy;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000B25C File Offset: 0x0000945C
		private IndexClusterMemberInfo GetClusterMemberInfo(string indexName)
		{
			return this.PerformFastOperation<IndexClusterMemberInfo>(delegate()
			{
				string text;
				if (!this.indexClusterMap.TryGetValue(indexName, out text))
				{
					foreach (string text2 in this.ClusterService.Clusters)
					{
						if (text2.StartsWith(indexName, StringComparison.OrdinalIgnoreCase))
						{
							text = text2;
							this.indexClusterMap[indexName] = text;
							break;
						}
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				IndexClusterMemberInfo result;
				using (IEnumerator<IndexClusterMemberInfo> enumerator2 = this.ClusterService.Members(text).GetEnumerator())
				{
					result = (enumerator2.MoveNext() ? enumerator2.Current : null);
				}
				return result;
			}, "GetClusterMemberInfo");
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B39C File Offset: 0x0000959C
		private void CreateIndexSystem(string indexSystemName, string indexNodeName, string databasePath, bool databaseCopyActive, RefinerUsage refinersToEnable)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Creating index system {0} at {1}", new object[]
			{
				indexSystemName,
				databasePath ?? "default location"
			});
			IndexSystemModel model = null;
			base.PerformFastOperation(delegate()
			{
				model = this.IndexService.CreateIndexSystemModel(indexSystemName, "fastsearch");
				if (!string.IsNullOrEmpty(databasePath))
				{
					IndexSystemOptions options = new IndexSystemOptions(model.Options)
					{
						RootDirectory = "/root/" + databasePath,
						JournalBufferSize = FastManagementClient.Config.JournalBufferSize,
						ImportIndex = true,
						TriggerReseedOnDocsumLookupFailure = FastManagementClient.Config.TriggerReseedOnDocsumLookupFailure,
						InvalidateOnSeedingReadFailure = FastManagementClient.Config.InvalidateOnSeedingReadFailure
					};
					model.Options = options;
				}
				model.Schema = FastIndexSystemSchema.GetIndexSystemSchema(refinersToEnable);
				this.SetIndexSystemIndex(model, databaseCopyActive);
			}, "CreateIndexSystem");
			base.PerformFastOperation(delegate()
			{
				this.WaitForOperationFinished(this.IndexService.CreateIndexSystem(model, indexNodeName));
			}, "CreateIndexSystem");
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B444 File Offset: 0x00009644
		private void SetIndexSystemIndex(IndexSystemModel model, bool databaseCopyActive)
		{
			if (FastManagementClient.Config.PassiveCatalogEnabled)
			{
				model.Indexes = (databaseCopyActive ? this.activeModelIndexes : this.passiveModelIndexes);
				return;
			}
			model.Indexes = this.defaultModelIndexes;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B476 File Offset: 0x00009676
		private string IndexSystemMode(bool databaseCopyActive, bool suspended)
		{
			if (suspended)
			{
				return "suspended";
			}
			if (!FastManagementClient.Config.PassiveCatalogEnabled)
			{
				return "default";
			}
			if (!databaseCopyActive)
			{
				return "passive";
			}
			return "active";
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B548 File Offset: 0x00009748
		private bool UpdateIndexSystem(string indexName, Action<IndexSystemModel> action)
		{
			return this.PerformFastOperation<bool>(delegate()
			{
				IndexSystemModel indexSystemModel = this.IndexService.GetIndexSystemModel(indexName);
				if (indexSystemModel == null)
				{
					return false;
				}
				action(indexSystemModel);
				indexSystemModel.LastUpdated = 0L;
				int num = this.IndexService.UpdateIndexSystem(indexSystemModel);
				this.WaitForOperationFinished(num);
				bool boolReturnValue = this.IndexService.GetBoolReturnValue(num);
				this.DiagnosticsSession.TraceDebug<string, string, bool>("{0} index system {1}: {2}", boolReturnValue ? "Updated" : "Did not update", indexName, boolReturnValue);
				return boolReturnValue;
			}, "UpdateIndexSystem");
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B587 File Offset: 0x00009787
		private void WaitForOperationFinished(int handle)
		{
			while (!this.IndexService.IsFinished(handle))
			{
				Thread.Sleep(100);
			}
		}

		// Token: 0x040000C1 RID: 193
		internal const string SystemName = "Fsis";

		// Token: 0x040000C2 RID: 194
		internal const string IndexRole = "Index";

		// Token: 0x040000C3 RID: 195
		private const string IndexTemplate = "fastsearch";

		// Token: 0x040000C4 RID: 196
		private const string RootDirectoryPrefix = "/root/";

		// Token: 0x040000C5 RID: 197
		private const string PropertyFieldFormat = "&quot;{0}&quot;";

		// Token: 0x040000C6 RID: 198
		private const int UpdateConfigurationRetryCount = 120;

		// Token: 0x040000C7 RID: 199
		private const int ConfigurationRetryInterval = 1000;

		// Token: 0x040000C8 RID: 200
		private static readonly string[] defaultOptions = new string[]
		{
			"cachemem_size=" + FastManagementClient.Config.MemCacheSize,
			"doc_sum_index_in_memory=0",
			"indextime_gen_extids=0",
			"restart_on_latent_refinable_change=1",
			"use_context_lengths=0",
			"merges_concurrent_max=" + FastManagementClient.Config.MaxConcurrentMerges,
			"max_failed_merges=" + FastManagementClient.Config.MaxFailedMerges,
			"master_merges_concurrent_max=" + FastManagementClient.Config.MaxConcurrentMasterMerges,
			"master_merge_trigger_ratio_periodic=" + FastManagementClient.Config.MasterMergeTriggerRatioPeriodic,
			"master_merge_trigger_ratio_daily=" + FastManagementClient.Config.MasterMergeTriggerRatioDaily,
			"master_merge_trigger_ratio_weekly=" + FastManagementClient.Config.MasterMergeTriggerRatioWeekly,
			"master_merge_trigger_minute_of_day=" + FastManagementClient.Config.MasterMergeTriggerMinuteOfDay,
			"master_merge_trigger_minute_of_week=" + FastManagementClient.Config.MasterMergeTriggerMinuteOfWeek,
			"merge_levels_values_default=" + FastManagementClient.Config.MergeLevelsValuesDefault,
			"expected_doccount_mergeunit=" + FastManagementClient.Config.ExpectedDocCountMergeUnit,
			"failed_merges_before_reseed=" + FastManagementClient.Config.MaxFailedMerges,
			"failed_checkpoints_before_reseed=" + FastManagementClient.Config.FailedCheckpointsBeforeReseed,
			"failed_generations_before_reseed=" + FastManagementClient.Config.FailedGenerationsBeforeReseed
		};

		// Token: 0x040000C9 RID: 201
		private static readonly string[] overrideOptions = new string[]
		{
			"merge_override1_updategroups=FolderUpdateGroup",
			"merge_override1_levels=" + FastManagementClient.Config.OverrideFolderUpdateGroupMergeLevels,
			"merge_override1_unit=" + FastManagementClient.Config.OverrideFolderUpdateGroupMergeUnit,
			"merge_override1_master_merge_ratio=" + FastManagementClient.Config.OverrideFolderUpdateGroupMasterMergeRatio
		};

		// Token: 0x040000CA RID: 202
		private static object lockObject = new object();

		// Token: 0x040000CB RID: 203
		private static Hookable<IndexManager> hookableInstance;

		// Token: 0x040000CC RID: 204
		private volatile IAdminServiceManagementAgent adminService;

		// Token: 0x040000CD RID: 205
		private volatile IIndexClusterManagementAgent clusterService;

		// Token: 0x040000CE RID: 206
		private volatile IIndexManagement indexService;

		// Token: 0x040000CF RID: 207
		private ConcurrentDictionary<string, string> indexClusterMap = new ConcurrentDictionary<string, string>();

		// Token: 0x040000D0 RID: 208
		private IIndexSystemIndex[] defaultModelIndexes;

		// Token: 0x040000D1 RID: 209
		private IIndexSystemIndex[] activeModelIndexes;

		// Token: 0x040000D2 RID: 210
		private IIndexSystemIndex[] passiveModelIndexes;
	}
}
