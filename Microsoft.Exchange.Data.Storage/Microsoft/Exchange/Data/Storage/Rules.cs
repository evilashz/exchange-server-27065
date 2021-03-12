using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Rules : IList<Rule>, ICollection<Rule>, IEnumerable<Rule>, IEnumerable
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0004E253 File Offset: 0x0004C453
		public Rules(Folder folder) : this(folder, true, false)
		{
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0004E25E File Offset: 0x0004C45E
		public Rules(Folder folder, bool includeHidden) : this(folder, true, includeHidden)
		{
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0004E269 File Offset: 0x0004C469
		public Rules(Folder folder, bool loadRules, bool includeHidden = false)
		{
			this.folder = folder;
			this.data = new List<Rule>();
			if (loadRules)
			{
				this.ParseRules(includeHidden);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0004E28D File Offset: 0x0004C48D
		public int IndexOf(Rule item)
		{
			this.CheckSaved();
			return this.data.IndexOf(item);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0004E2A1 File Offset: 0x0004C4A1
		public void Insert(int index, Rule item)
		{
			this.CheckSaved();
			this.data.Insert(index, item);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0004E2B6 File Offset: 0x0004C4B6
		public void RemoveAt(int index)
		{
			this.CheckSaved();
			this.data.RemoveAt(index);
		}

		// Token: 0x17000228 RID: 552
		public Rule this[int index]
		{
			get
			{
				this.CheckSaved();
				return this.data[index];
			}
			set
			{
				this.CheckSaved();
				this.data[index] = value;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		public void Add(Rule item)
		{
			this.CheckSaved();
			this.data.Add(item);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0004E307 File Offset: 0x0004C507
		public void Clear()
		{
			this.CheckSaved();
			if (this.data.Count > 0)
			{
				this.data.Clear();
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0004E328 File Offset: 0x0004C528
		public bool Contains(Rule item)
		{
			this.CheckSaved();
			return this.data.Contains(item);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0004E33C File Offset: 0x0004C53C
		public void CopyTo(Rule[] array, int arrayIndex)
		{
			this.CheckSaved();
			this.data.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0004E351 File Offset: 0x0004C551
		public int Count
		{
			get
			{
				this.CheckSaved();
				return this.data.Count;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0004E364 File Offset: 0x0004C564
		public bool IsReadOnly
		{
			get
			{
				this.CheckSaved();
				return false;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0004E36D File Offset: 0x0004C56D
		public bool Remove(Rule item)
		{
			this.CheckSaved();
			return this.data.Remove(item);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0004E381 File Offset: 0x0004C581
		public IEnumerator<Rule> GetEnumerator()
		{
			this.CheckSaved();
			return this.data.GetEnumerator();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004E399 File Offset: 0x0004C599
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.CheckSaved();
			return this.data.GetEnumerator();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
		public Rule FindByRuleId(RuleId id)
		{
			this.CheckSaved();
			if (id == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Rules::Bind. {0} should not be null.", "id");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("id", 1));
			}
			int indexByRuleId = this.GetIndexByRuleId(id);
			if (indexByRuleId != -1)
			{
				return this[indexByRuleId];
			}
			throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004E414 File Offset: 0x0004C614
		public void Delete(RuleId id)
		{
			this.CheckSaved();
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Rules::Delete.");
			if (id == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Rules::Delete. {0} should not be null.", "id");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("id", 1));
			}
			int indexByRuleId = this.GetIndexByRuleId(id);
			if (indexByRuleId != -1)
			{
				this.data.RemoveAt(indexByRuleId);
				return;
			}
			throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0004E490 File Offset: 0x0004C690
		public int GetIndexByRuleId(RuleId id)
		{
			this.CheckSaved();
			if (id == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Rules::Bind. {0} should not be null.", "id");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("id", 1));
			}
			if (id.RuleIndex < this.data.Count)
			{
				Rule rule = this.data[id.RuleIndex];
				if (rule.Id.Equals(id))
				{
					return id.RuleIndex;
				}
			}
			foreach (Rule rule2 in this)
			{
				if (rule2.Id.Equals(id))
				{
					return rule2.Id.RuleIndex;
				}
			}
			return -1;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0004E560 File Offset: 0x0004C760
		public bool LegacyOutlookRulesCacheExists
		{
			get
			{
				this.CheckSaved();
				ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ItemClass, "IPM.RuleOrganizer");
				SortBy[] sortColumns = new SortBy[]
				{
					new SortBy(InternalSchema.ItemClass, SortOrder.Ascending)
				};
				bool result;
				using (QueryResult queryResult = this.Folder.ItemQuery(ItemQueryType.Associated, null, sortColumns, new PropertyDefinition[]
				{
					StoreObjectSchema.EntryId,
					FolderSchema.DisplayName
				}))
				{
					result = queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
				}
				return result;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
		public virtual void Save()
		{
			this.Save(false);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0004E5FC File Offset: 0x0004C7FC
		public virtual void Save(bool includeHidden)
		{
			this.CheckSaved();
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Rules::Save.");
			List<Rule> list = new List<Rule>();
			List<Rule> list2 = new List<Rule>();
			List<Rule> list3 = new List<Rule>();
			List<Rule> list4 = new List<Rule>();
			int num = 10;
			foreach (Rule rule in this)
			{
				if (rule.IsNotSupported)
				{
					ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Rules::Save. Can't save NotSupported rule {0}", rule.Name);
					this.UpdateUnsupportedRuleSequence(rule, num);
				}
				else
				{
					if (rule.Sequence != num)
					{
						rule.Sequence = num;
					}
					if (rule.IsDirty)
					{
						rule.Save();
					}
				}
				num++;
				if (rule.IsNew)
				{
					list.Add(rule.ServerRule);
				}
				else if (rule.IsDirty)
				{
					list2.Add(rule.ServerRule);
				}
				else
				{
					list3.Add(rule.ServerRule);
				}
			}
			foreach (Rule rule2 in this.serverRules)
			{
				if ((Rules.IsInboxRule(rule2) || includeHidden) && !list2.Contains(rule2) && !list3.Contains(rule2))
				{
					list4.Add(rule2);
					rule2.Operation = RuleOperation.Delete;
				}
			}
			if (list.Count > 0)
			{
				this.RuleSaver(list.ToArray(), new Rules.RulesUpdaterDelegate(this.Folder.MapiFolder.AddRules));
			}
			if (list2.Count > 0)
			{
				this.RuleSaver(list2.ToArray(), new Rules.RulesUpdaterDelegate(this.Folder.MapiFolder.ModifyRules));
			}
			if (list4.Count > 0)
			{
				this.RuleSaver(list4.ToArray(), new Rules.RulesUpdaterDelegate(this.Folder.MapiFolder.DeleteRules));
			}
			this.RemoveOutlookRuleBlob();
			this.Nuke();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004E818 File Offset: 0x0004CA18
		public void SaveBatch()
		{
			this.CheckSaved();
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Rules::SaveBatch.");
			Rule[] array = (from rule in this
			where RuleOperation.NoOp != rule.ServerRule.Operation
			select rule.ServerRule).ToArray<Rule>();
			if (0 < array.Length)
			{
				this.RuleSaver(array, new Rules.RulesUpdaterDelegate(this.folder.MapiFolder.SaveRuleBatch));
			}
			this.RemoveOutlookRuleBlob();
			this.Nuke();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0004E8BB File Offset: 0x0004CABB
		public void Nuke()
		{
			this.serverRules = null;
			this.data.Clear();
			this.saved = true;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0004E8D6 File Offset: 0x0004CAD6
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0004E8DE File Offset: 0x0004CADE
		internal Rule[] ServerRules
		{
			get
			{
				return this.serverRules;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0004E8E8 File Offset: 0x0004CAE8
		private static bool IsInboxRule(Rule serverRule)
		{
			return (!(serverRule.Provider != "MSFT:TDX Rules") || !(serverRule.Provider != "ExchangeMailboxRules14") || serverRule.Provider.StartsWith("RuleOrganizer", StringComparison.OrdinalIgnoreCase)) && string.Compare(serverRule.Name, "#NET FOLDERS#", StringComparison.CurrentCultureIgnoreCase) != 0;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0004E944 File Offset: 0x0004CB44
		private static void RunMapiCode(LocalizedString context, Action action)
		{
			try
			{
				action();
			}
			catch (MapiRetryableException exception)
			{
				throw StorageGlobals.TranslateMapiException(context, exception, null, null, string.Empty, new object[0]);
			}
			catch (MapiPermanentException exception2)
			{
				throw StorageGlobals.TranslateMapiException(context, exception2, null, null, string.Empty, new object[0]);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0004E9CC File Offset: 0x0004CBCC
		private void ParseRules(bool includeHidden)
		{
			MapiFolder mapiFolder = this.Folder.MapiFolder;
			Rules.RunMapiCode(ServerStrings.ErrorLoadingRules, delegate
			{
				this.serverRules = mapiFolder.GetRules(new PropTag[0]);
			});
			int num = 10;
			foreach (Rule rule in this.serverRules)
			{
				if (Rules.IsInboxRule(rule) || includeHidden)
				{
					Rule rule2 = Rule.Create(this.Folder, rule);
					this.data.Add(rule2);
					rule2.Id = new RuleId(this.data.IndexOf(rule2), rule.ID);
					rule2.ClearDirty();
					if (rule2.Sequence != num)
					{
						rule2.Sequence = num;
					}
					num++;
				}
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0004EAFC File Offset: 0x0004CCFC
		private void UpdateUnsupportedRuleSequence(Rule rule, int sequence)
		{
			Rules.RunMapiCode(ServerStrings.ErrorSavingRules, delegate
			{
				if (rule.ServerRule.ExecutionSequence != sequence)
				{
					rule.ServerRule.ExecutionSequence = sequence;
					this.Folder.MapiFolder.ModifyRules(new Rule[]
					{
						rule.ServerRule
					});
				}
			});
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0004ED68 File Offset: 0x0004CF68
		private void RuleSaver(Rule[] mapiRules, Rules.RulesUpdaterDelegate rulesUpdater)
		{
			Rules.RunMapiCode(ServerStrings.ErrorSavingRules, delegate
			{
				StoreSession session = this.Folder.Session;
				object <>4__this = this;
				bool flag = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					try
					{
						rulesUpdater(mapiRules);
					}
					catch (MapiRetryableException ex)
					{
						if (!(ex is MapiExceptionNotEnoughMemory))
						{
							if (!(ex is MapiExceptionRpcOutOfMemory))
							{
								goto IL_C2;
							}
						}
						try
						{
							foreach (Rule rule in mapiRules)
							{
								rulesUpdater(new Rule[]
								{
									rule
								});
							}
							goto IL_C4;
						}
						catch (MapiExceptionNotEnoughMemory innerException)
						{
							throw new RulesTooBigException(ServerStrings.ErrorSavingRules, innerException);
						}
						catch (MapiExceptionRpcOutOfMemory innerException2)
						{
							throw new RulesTooBigException(ServerStrings.ErrorSavingRules, innerException2);
						}
						goto IL_C2;
						IL_C4:
						goto IL_C6;
						IL_C2:
						throw;
					}
					IL_C6:;
				}
				catch (MapiPermanentException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ErrorSavingRules, ex2, session, <>4__this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Rules::Save.", new object[0]),
						ex2
					});
				}
				catch (MapiRetryableException ex3)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ErrorSavingRules, ex3, session, <>4__this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Rules::Save.", new object[0]),
						ex3
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag)
							{
								session.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			});
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0004EDA6 File Offset: 0x0004CFA6
		private void CheckSaved()
		{
			if (this.saved)
			{
				throw new InvalidOperationException("The Rules collection cannot be used after a save.  Use a new Rules collection.");
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0004EDBC File Offset: 0x0004CFBC
		private void RemoveOutlookRuleBlob()
		{
			using (QueryResult queryResult = this.Folder.ItemQuery(ItemQueryType.Associated, null, Rules.OutlookRuleBlobSortByArray, Rules.OutlookRuleBlobPropertyDefinitionArray))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, Rules.OutlookRuleBlobFilter))
				{
					List<StoreObjectId> list = new List<StoreObjectId>();
					bool flag = true;
					do
					{
						object[][] rows = queryResult.GetRows(10000);
						foreach (object[] array2 in rows)
						{
							if (!StringComparer.OrdinalIgnoreCase.Equals((string)array2[1], "IPM.RuleOrganizer"))
							{
								flag = false;
								break;
							}
							list.Add(PropertyBag.CheckPropertyValue<VersionedId>(ItemSchema.Id, array2[0]).ObjectId);
						}
						flag = (flag && rows.Length > 0);
					}
					while (flag);
					this.Folder.DeleteObjects(DeleteItemFlags.HardDelete, list.ToArray());
				}
			}
		}

		// Token: 0x04000308 RID: 776
		internal const int SequenceOffset = 10;

		// Token: 0x04000309 RID: 777
		private Rule[] serverRules;

		// Token: 0x0400030A RID: 778
		private Folder folder;

		// Token: 0x0400030B RID: 779
		private readonly List<Rule> data;

		// Token: 0x0400030C RID: 780
		private bool saved;

		// Token: 0x0400030D RID: 781
		private static readonly ComparisonFilter OutlookRuleBlobFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.RuleOrganizer");

		// Token: 0x0400030E RID: 782
		private static readonly SortBy[] OutlookRuleBlobSortByArray = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x0400030F RID: 783
		private static readonly PropertyDefinition[] OutlookRuleBlobPropertyDefinitionArray = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x020000A0 RID: 160
		// (Invoke) Token: 0x06000B3B RID: 2875
		private delegate void RulesUpdaterDelegate(Rule[] mapiRules);
	}
}
