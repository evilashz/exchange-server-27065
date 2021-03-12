using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x0200000E RID: 14
	internal class SearchFeatureState
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000031E3 File Offset: 0x000013E3
		internal SearchFeatureState(ISearchServiceConfig config, List<MdbInfo> mdbInfoList, IDiagnosticsSession diagnosticsSession)
		{
			this.config = config;
			this.MdbInfos = mdbInfoList;
			this.diagnosticsSession = diagnosticsSession;
			this.searchMemoryModel = new SearchMemoryModel(config, diagnosticsSession);
			this.searchMemoryModel.SearchMemoryUsage = SearchMemoryModel.GetFreshSearchMemoryUsage();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003220 File Offset: 0x00001420
		internal SearchFeatureState(SearchFeatureState searchFeatureState)
		{
			this.config = searchFeatureState.Config;
			this.MdbInfos = SearchFeatureState.GetCopiedMdbInfoList(searchFeatureState.MdbInfos);
			this.diagnosticsSession = searchFeatureState.DiagnosticsSession;
			this.searchMemoryModel = new SearchMemoryModel(searchFeatureState.SearchMemoryModel);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000326D File Offset: 0x0000146D
		internal ISearchServiceConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003275 File Offset: 0x00001475
		internal IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003285 File Offset: 0x00001485
		internal List<MdbInfo> MdbInfos { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000328E File Offset: 0x0000148E
		internal SearchMemoryModel SearchMemoryModel
		{
			get
			{
				return this.searchMemoryModel;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003298 File Offset: 0x00001498
		internal static SearchFeatureState GetCurrentState(ISearchServiceConfig config, IDiagnosticsSession diagnosticsSession, IEnumerable<MdbInfo> allMDBs)
		{
			foreach (MdbInfo mdbInfo in allMDBs)
			{
				bool flag;
				if (SearchConfig.GetRegistry(mdbInfo.Guid, "DisableInstantSearch", ref flag))
				{
					mdbInfo.IsInstantSearchEnabled = (mdbInfo.MountedOnLocalServer && !flag);
				}
				if (SearchConfig.GetRegistry(mdbInfo.Guid, "AutoSuspendCatalog", ref flag))
				{
					mdbInfo.IsCatalogSuspended = (!mdbInfo.MountedOnLocalServer && flag);
				}
			}
			return new SearchFeatureState(config, new List<MdbInfo>(allMDBs), diagnosticsSession);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003338 File Offset: 0x00001538
		internal static SearchFeatureState GetAllOnState(ISearchServiceConfig config, IDiagnosticsSession diagnosticsSession, IEnumerable<MdbInfo> allMDBs)
		{
			SearchFeatureState currentState = SearchFeatureState.GetCurrentState(config, diagnosticsSession, allMDBs);
			currentState.SearchMemoryModel.SearchMemoryOperation.MemoryUsageHighLine = long.MaxValue;
			currentState.SearchMemoryModel.SearchMemoryOperation.MemoryUsageLowLine = long.MaxValue;
			return currentState.GetNextState();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003388 File Offset: 0x00001588
		internal SearchFeatureState GetNextState()
		{
			SearchFeatureState searchFeatureState = new SearchFeatureState(this);
			SearchMemoryModel.ActionDirection actionDirection = searchFeatureState.SearchMemoryModel.GetActionDirection();
			if (actionDirection != SearchMemoryModel.ActionDirection.None)
			{
				if (actionDirection == SearchMemoryModel.ActionDirection.Degrade)
				{
					searchFeatureState.MdbInfos.Sort(new Comparison<MdbInfo>(SearchFeatureState.CompareMdbInfoInDescendingAP));
				}
				else
				{
					searchFeatureState.MdbInfos.Sort(new Comparison<MdbInfo>(SearchFeatureState.CompareMdbInfoInAscendingAP));
				}
				int count = SearchFeature.SearchFeatureTypeList.Count;
				for (int i = 0; i < count; i++)
				{
					SearchFeature.SearchFeatureType searchFeatureType = (actionDirection == SearchMemoryModel.ActionDirection.Degrade) ? SearchFeature.SearchFeatureTypeList[i] : SearchFeature.SearchFeatureTypeList[count - i - 1];
					foreach (MdbInfo mdbInfo in searchFeatureState.MdbInfos)
					{
						SearchFeature searchFeature = new SearchFeature(searchFeatureType, searchFeatureState.Config, mdbInfo);
						if (searchFeature.IsFlipAllowed())
						{
							long potentialMemoryChange = searchFeature.GetPotentialMemoryChange();
							if (searchFeatureState.SearchMemoryModel.IsBetter(potentialMemoryChange))
							{
								searchFeatureState.ToggleSettings(mdbInfo, searchFeatureType, potentialMemoryChange);
							}
						}
					}
				}
			}
			return searchFeatureState;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000034A4 File Offset: 0x000016A4
		internal void ToggleSettings(MdbInfo mdbInfo, SearchFeature.SearchFeatureType searchFeatureType, long memoryChange)
		{
			string text;
			bool flag;
			switch (searchFeatureType)
			{
			case SearchFeature.SearchFeatureType.InstantSearch:
				mdbInfo.IsInstantSearchEnabled = !mdbInfo.IsInstantSearchEnabled;
				text = "DisableInstantSearch";
				flag = !mdbInfo.IsInstantSearchEnabled;
				break;
			case SearchFeature.SearchFeatureType.PassiveCatalog:
				mdbInfo.IsCatalogSuspended = !mdbInfo.IsCatalogSuspended;
				text = "AutoSuspendCatalog";
				flag = mdbInfo.IsCatalogSuspended;
				break;
			default:
				throw new ArgumentException("Unexpected search feature type: " + searchFeatureType.ToString() + ". Accepted values: InstantSearch, PassiveCatalog, Refiners.", "searchFeatureType");
			}
			this.diagnosticsSession.TraceDebug<MdbInfo, string, bool>("Database {0} is affected. The affected registry key is {1}. Its value is set to {2}.", mdbInfo, text, flag);
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Database {0} is affected. The affected registry key is {1}. Its value is set to {2}.", new object[]
			{
				mdbInfo,
				text,
				flag
			});
			this.searchMemoryModel.ApplyMemoryChange(memoryChange, mdbInfo.ToString());
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000357C File Offset: 0x0000177C
		internal void WriteCurrentStateToRegistry()
		{
			if (this.MdbInfos.Count > 0)
			{
				List<Guid> list = new List<Guid>(this.MdbInfos.Count);
				foreach (MdbInfo mdbInfo in this.MdbInfos)
				{
					list.Add(mdbInfo.Guid);
					SearchConfig.SetRegistry(mdbInfo.Guid, "DisableInstantSearch", !mdbInfo.IsInstantSearchEnabled);
					SearchConfig.SetRegistry(mdbInfo.Guid, "AutoSuspendCatalog", mdbInfo.IsCatalogSuspended);
					this.diagnosticsSession.TraceDebug<MdbInfo, bool, bool>("Graceful Degradation affected Database {0}. DisableInstantSearch: {1}; AutoSuspendCatalog: {2}.", mdbInfo, !mdbInfo.IsInstantSearchEnabled, mdbInfo.IsCatalogSuspended);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003648 File Offset: 0x00001848
		internal bool Equals(SearchFeatureState searchFeatureState)
		{
			if (searchFeatureState.MdbInfos.Count != this.MdbInfos.Count)
			{
				return false;
			}
			for (int i = 0; i < this.MdbInfos.Count; i++)
			{
				if (this.MdbInfos[i].Guid != searchFeatureState.MdbInfos[i].Guid || this.MdbInfos[i].IsInstantSearchEnabled != searchFeatureState.MdbInfos[i].IsInstantSearchEnabled || this.MdbInfos[i].IsCatalogSuspended != searchFeatureState.MdbInfos[i].IsCatalogSuspended)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000036FC File Offset: 0x000018FC
		private static int CompareMdbInfoInDescendingAP(MdbInfo x, MdbInfo y)
		{
			if (x.ActivationPreference == y.ActivationPreference)
			{
				return y.Guid.CompareTo(x.Guid);
			}
			return y.ActivationPreference.CompareTo(x.ActivationPreference);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003740 File Offset: 0x00001940
		private static int CompareMdbInfoInAscendingAP(MdbInfo x, MdbInfo y)
		{
			return SearchFeatureState.CompareMdbInfoInDescendingAP(x, y) * -1;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000374C File Offset: 0x0000194C
		private static List<MdbInfo> GetCopiedMdbInfoList(List<MdbInfo> mdbInfoList)
		{
			if (mdbInfoList.Count == 0)
			{
				throw new ArgumentException("mdbInfoList");
			}
			List<MdbInfo> list = new List<MdbInfo>(mdbInfoList.Count);
			foreach (MdbInfo mdbInfo in mdbInfoList)
			{
				list.Add(new MdbInfo(mdbInfo));
			}
			return list;
		}

		// Token: 0x0400002D RID: 45
		private readonly ISearchServiceConfig config;

		// Token: 0x0400002E RID: 46
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400002F RID: 47
		private readonly SearchMemoryModel searchMemoryModel;
	}
}
