using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200038D RID: 909
	[OwaEventStruct("FVLVF")]
	internal class FolderVirtualListViewFilter
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x000C5014 File Offset: 0x000C3214
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000C501C File Offset: 0x000C321C
		public bool IsCurrentVersion
		{
			get
			{
				return this.version == 3;
			}
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000C5028 File Offset: 0x000C3228
		public static FolderVirtualListViewFilter ParseFromPropertyValue(object propertyValue)
		{
			string[] array = propertyValue as string[];
			if (array == null || array.Length < 2)
			{
				return null;
			}
			if (string.IsNullOrEmpty(array[0]))
			{
				return null;
			}
			FolderVirtualListViewFilter folderVirtualListViewFilter = new FolderVirtualListViewFilter();
			try
			{
				folderVirtualListViewFilter.SourceFolderId = OwaStoreObjectId.CreateFromString(array[0]);
			}
			catch (OwaInvalidIdFormatException)
			{
				return null;
			}
			int num = 0;
			folderVirtualListViewFilter.version = 0;
			if (!string.IsNullOrEmpty(array[1]))
			{
				string[] array2 = array[1].Split(new char[]
				{
					':'
				});
				if (array2.Length < 1 || !int.TryParse(array2[0], out num))
				{
					num = 0;
				}
				if (array2.Length < 2 || !int.TryParse(array2[1], out folderVirtualListViewFilter.version))
				{
					folderVirtualListViewFilter.version = 0;
				}
			}
			folderVirtualListViewFilter.SendToMe = ((num & 1) != 0);
			folderVirtualListViewFilter.CcToMe = ((num & 2) != 0);
			folderVirtualListViewFilter.IsUnread = ((num & 4) != 0);
			folderVirtualListViewFilter.IsHighImportance = ((num & 16) != 0);
			folderVirtualListViewFilter.HasAttachments = ((num & 32) != 0);
			folderVirtualListViewFilter.IsFlag = ((num & 8) != 0);
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 2; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					if (array[i].StartsWith(";"))
					{
						if (num2 == 3 && string.IsNullOrEmpty(folderVirtualListViewFilter.Categories))
						{
							folderVirtualListViewFilter.Categories = stringBuilder.ToString();
						}
						string value = array[i].Substring(";".Length);
						if ("sTo".Equals(value, StringComparison.OrdinalIgnoreCase))
						{
							num2 = 1;
						}
						else if ("sFrm".Equals(value, StringComparison.OrdinalIgnoreCase))
						{
							num2 = 2;
						}
						else if ("sCat".Equals(value, StringComparison.OrdinalIgnoreCase))
						{
							num2 = 3;
						}
						else
						{
							num2 = 0;
						}
					}
					else
					{
						switch (num2)
						{
						case 1:
							if (string.IsNullOrEmpty(folderVirtualListViewFilter.To))
							{
								folderVirtualListViewFilter.To = array[i];
							}
							break;
						case 2:
							if (string.IsNullOrEmpty(folderVirtualListViewFilter.From))
							{
								folderVirtualListViewFilter.From = array[i];
							}
							break;
						case 3:
							if (!flag)
							{
								stringBuilder.Append(',');
							}
							flag = false;
							stringBuilder.Append(array[i]);
							break;
						}
					}
				}
			}
			if (num2 == 3 && string.IsNullOrEmpty(folderVirtualListViewFilter.Categories))
			{
				folderVirtualListViewFilter.Categories = stringBuilder.ToString();
			}
			return folderVirtualListViewFilter;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000C5288 File Offset: 0x000C3488
		public int GetBooleanFlags()
		{
			int num = 0;
			if (this.SendToMe)
			{
				num |= 1;
			}
			if (this.CcToMe)
			{
				num |= 2;
			}
			if (this.IsUnread)
			{
				num |= 4;
			}
			if (this.IsFlag)
			{
				num |= 8;
			}
			if (this.IsHighImportance)
			{
				num |= 16;
			}
			if (this.HasAttachments)
			{
				num |= 32;
			}
			return num;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000C52E4 File Offset: 0x000C34E4
		public string[] GetPropertyValueToSave()
		{
			List<string> list = new List<string>();
			list.Add(this.SourceFolderId.ToBase64String());
			list.Add(this.GetBooleanFlags().ToString() + ':' + this.version);
			if (!string.IsNullOrEmpty(this.Categories))
			{
				string[] array = this.Categories.Split(new char[]
				{
					','
				});
				Array.Sort<string>(array);
				if (array.Length > 0)
				{
					list.Add(";sCat");
					foreach (string item in array)
					{
						list.Add(item);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.To))
			{
				list.Add(";sTo");
				list.Add(this.To);
			}
			if (!string.IsNullOrEmpty(this.From))
			{
				list.Add(";sFrm");
				list.Add(this.From);
			}
			return list.ToArray();
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000C53E4 File Offset: 0x000C35E4
		public override int GetHashCode()
		{
			return this.SourceFolderId.GetHashCode() ^ this.SendToMe.GetHashCode() ^ this.CcToMe.GetHashCode() ^ this.IsUnread.GetHashCode() ^ (string.IsNullOrEmpty(this.Categories) ? 0 : this.Categories.GetHashCode()) ^ (string.IsNullOrEmpty(this.To) ? 0 : this.To.GetHashCode()) ^ (string.IsNullOrEmpty(this.From) ? 0 : this.From.GetHashCode()) ^ this.IsFlag.GetHashCode() ^ this.IsHighImportance.GetHashCode() ^ this.HasAttachments.GetHashCode() ^ this.version.GetHashCode();
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000C54A4 File Offset: 0x000C36A4
		public override bool Equals(object obj)
		{
			FolderVirtualListViewFilter folderVirtualListViewFilter = obj as FolderVirtualListViewFilter;
			return folderVirtualListViewFilter != null && this.EqualsIgnoreVersion(folderVirtualListViewFilter) && this.version == folderVirtualListViewFilter.version;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000C54D8 File Offset: 0x000C36D8
		public bool EqualsIgnoreVersion(FolderVirtualListViewFilter filter)
		{
			return this.SourceFolderId.Equals(filter.SourceFolderId) && this.SendToMe == filter.SendToMe && this.CcToMe == filter.CcToMe && this.IsUnread == filter.IsUnread && FolderVirtualListViewFilter.CheckCategoryEquals(this.Categories, filter.Categories) && FolderVirtualListViewFilter.CheckStringEquals(this.From, filter.From) && FolderVirtualListViewFilter.CheckStringEquals(this.To, filter.To) && this.IsFlag == filter.IsFlag && this.IsHighImportance == filter.IsHighImportance && this.HasAttachments == filter.HasAttachments;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000C558D File Offset: 0x000C378D
		private static bool CheckStringEquals(string str1, string str2)
		{
			return (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2)) || (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2) && string.Equals(str1, str2, StringComparison.InvariantCulture));
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000C55BC File Offset: 0x000C37BC
		private static bool CheckCategoryEquals(string catString1, string catString2)
		{
			if (string.IsNullOrEmpty(catString1) && string.IsNullOrEmpty(catString2))
			{
				return true;
			}
			if (string.IsNullOrEmpty(catString1) || string.IsNullOrEmpty(catString2))
			{
				return false;
			}
			string[] array = catString1.Split(new char[]
			{
				','
			});
			string[] array2 = catString2.Split(new char[]
			{
				','
			});
			if (array.Length != array2.Length)
			{
				return false;
			}
			Array.Sort<string>(array);
			Array.Sort<string>(array2);
			int num = 0;
			while (num < array.Length && num < array2.Length)
			{
				if (array[num] != array2[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000C5654 File Offset: 0x000C3854
		public string[] GetCategories()
		{
			if (string.IsNullOrEmpty(this.Categories))
			{
				return null;
			}
			string[] array = this.Categories.Split(new char[]
			{
				','
			});
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000C5690 File Offset: 0x000C3890
		public string ToDescription()
		{
			LocalizedStrings.GetNonEncoded(538423429);
			LocalizedStrings.GetNonEncoded(742303293);
			List<string> list = new List<string>(10);
			if (this.HasAttachments)
			{
				list.Add(LocalizedStrings.GetNonEncoded(-2070993236));
			}
			if (this.IsHighImportance)
			{
				list.Add(LocalizedStrings.GetNonEncoded(-1382849860));
			}
			if (this.IsFlag)
			{
				list.Add(LocalizedStrings.GetNonEncoded(1398003256));
			}
			if (!string.IsNullOrEmpty(this.To))
			{
				string item = string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(-1733525344), new object[]
				{
					this.To
				});
				list.Add(item);
			}
			if (!string.IsNullOrEmpty(this.From))
			{
				string item2 = string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(1286003943), new object[]
				{
					this.From
				});
				list.Add(item2);
			}
			if (!string.IsNullOrEmpty(this.Categories))
			{
				string text = null;
				string[] array = this.Categories.Split(new char[]
				{
					','
				});
				int num = 0;
				while (num < array.Length && num < 3)
				{
					if (text == null)
					{
						text = array[num];
					}
					else
					{
						text = string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(538423429), new object[]
						{
							text,
							array[num]
						});
					}
					num++;
				}
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
			}
			if (this.IsUnread)
			{
				list.Add(LocalizedStrings.GetNonEncoded(-1020805457));
			}
			if (this.CcToMe)
			{
				list.Add(LocalizedStrings.GetNonEncoded(954766149));
			}
			if (this.SendToMe)
			{
				list.Add(LocalizedStrings.GetNonEncoded(226051813));
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			string text2 = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				string format = (i == 1) ? LocalizedStrings.GetNonEncoded(742303293) : LocalizedStrings.GetNonEncoded(538423429);
				text2 = string.Format(CultureInfo.CurrentCulture, format, new object[]
				{
					list[i],
					text2
				});
			}
			return text2;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000C58C5 File Offset: 0x000C3AC5
		public bool IsValidFilter()
		{
			return this.GetBooleanFlags() != 0 || !string.IsNullOrEmpty(this.To) || !string.IsNullOrEmpty(this.From) || !string.IsNullOrEmpty(this.Categories);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000C58FC File Offset: 0x000C3AFC
		private static QueryFilter BuildTextFilter(string keyWord, MatchOptions matchOptions, params PropertyDefinition[] searchProperties)
		{
			QueryFilter[] array = new TextFilter[searchProperties.Length];
			for (int i = 0; i < searchProperties.Length; i++)
			{
				array[i] = new TextFilter(searchProperties[i], keyWord, matchOptions, MatchFlags.IgnoreCase);
			}
			if (array.Length <= 1)
			{
				return array[0];
			}
			return new OrFilter(array);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000C5940 File Offset: 0x000C3B40
		private QueryFilter GetQueryFilter()
		{
			List<QueryFilter> list = new List<QueryFilter>(10);
			if (this.SendToMe)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageToMe, true));
			}
			if (this.CcToMe)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.MessageCcMe, true));
			}
			if (this.IsUnread)
			{
				list.Add(new BitMaskFilter(MessageItemSchema.Flags, 1UL, false));
			}
			if (!string.IsNullOrEmpty(this.Categories))
			{
				string[] array = this.Categories.Split(new char[]
				{
					','
				});
				List<QueryFilter> list2 = new List<QueryFilter>(array.Length);
				foreach (string propertyValue in array)
				{
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Categories, propertyValue));
				}
				if (list2.Count > 0)
				{
					list.Add((list2.Count > 1) ? new OrFilter(list2.ToArray()) : list2[0]);
				}
			}
			if (!string.IsNullOrEmpty(this.To))
			{
				list.Add(FolderVirtualListViewFilter.BuildTextFilter(this.To, MatchOptions.SubString, new PropertyDefinition[]
				{
					ItemSchema.DisplayTo
				}));
			}
			if (!string.IsNullOrEmpty(this.From))
			{
				list.Add(FolderVirtualListViewFilter.BuildTextFilter(this.From, MatchOptions.FullString, FolderVirtualListViewFilter.fromProperties));
			}
			if (this.IsFlag)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.FlagStatus, FlagStatus.Flagged));
			}
			if (this.IsHighImportance)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Importance, Importance.High));
			}
			if (this.HasAttachments)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.HasAttachment, true));
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new AndFilter(list.ToArray());
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000C5B13 File Offset: 0x000C3D13
		public void UpgradeFilter(SearchFolder folder, PropertyDefinition[] propertiesToLoad)
		{
			if (this.IsCurrentVersion)
			{
				throw new OwaInvalidOperationException("Can't upgrade a filter in current version");
			}
			this.version = 3;
			folder[ViewStateProperties.FilteredViewLabel] = this.GetPropertyValueToSave();
			folder.Save();
			folder.Load(FolderList.FolderTreeQueryProperties);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000C5B54 File Offset: 0x000C3D54
		public void ApplyFilter(SearchFolder folder, PropertyDefinition[] propertiesToLoad)
		{
			if (!this.IsCurrentVersion)
			{
				throw new OwaInvalidOperationException("Can't apply a filter in different version");
			}
			int num = 0;
			SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(this.GetQueryFilter(), new StoreId[]
			{
				this.SourceFolderId.StoreObjectId
			});
			searchFolderCriteria.DeepTraversal = false;
			SearchPerformanceData searchPerformanceData = new SearchPerformanceData();
			string text = this.ToDescription();
			searchPerformanceData.StartSearch(string.IsNullOrEmpty(text) ? "No Search String" : text);
			IAsyncResult asyncResult = folder.BeginApplyContinuousSearch(searchFolderCriteria, null, null);
			Stopwatch watch = Utilities.StartWatch();
			bool flag = asyncResult.AsyncWaitHandle.WaitOne(5000, false);
			searchPerformanceData.Complete(!flag, true);
			if (flag)
			{
				folder.EndApplyContinuousSearch(asyncResult);
			}
			else
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "FolderVirtualListViewEventHandler.GetFilteredView. Search for filtered view timed out.");
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.SearchesTimedOut.Increment();
				}
			}
			OwaContext.Current.SearchPerformanceData = searchPerformanceData;
			Utilities.StopWatch(watch, "FolderVirtualListViewEventHandler.GetFilteredView (Wait for filter to complete)");
			object obj = folder.TryGetProperty(FolderSchema.ExtendedFolderFlags);
			if (!(obj is PropertyError))
			{
				num = (int)obj;
			}
			folder[FolderSchema.ExtendedFolderFlags] = (num | 4194304);
			folder.Save();
			folder.Load(propertiesToLoad);
		}

		// Token: 0x0400183A RID: 6202
		private const int MaximumFilterTime = 5;

		// Token: 0x0400183B RID: 6203
		public const char CategorySplitChar = ',';

		// Token: 0x0400183C RID: 6204
		private const string ParameterNamePrefix = ";";

		// Token: 0x0400183D RID: 6205
		private const char SplitCharBetweenFlagAndVersion = ':';

		// Token: 0x0400183E RID: 6206
		public const string StructNamespace = "FVLVF";

		// Token: 0x0400183F RID: 6207
		public const string FolderId = "fId";

		// Token: 0x04001840 RID: 6208
		public const string SendToMeName = "fStm";

		// Token: 0x04001841 RID: 6209
		public const string CcToMeName = "fCtm";

		// Token: 0x04001842 RID: 6210
		public const string IsUnreadName = "fUR";

		// Token: 0x04001843 RID: 6211
		public const string CategoriesName = "sCat";

		// Token: 0x04001844 RID: 6212
		public const string ToName = "sTo";

		// Token: 0x04001845 RID: 6213
		public const string FromName = "sFrm";

		// Token: 0x04001846 RID: 6214
		public const string IsFlagName = "fFlg";

		// Token: 0x04001847 RID: 6215
		public const string IsHighImportanceName = "fHI";

		// Token: 0x04001848 RID: 6216
		public const string HasAttachmentsName = "fAttch";

		// Token: 0x04001849 RID: 6217
		private const int SendToMeFlag = 1;

		// Token: 0x0400184A RID: 6218
		private const int CcToMeFlag = 2;

		// Token: 0x0400184B RID: 6219
		private const int IsUnreadFlag = 4;

		// Token: 0x0400184C RID: 6220
		private const int IsFlagFlag = 8;

		// Token: 0x0400184D RID: 6221
		private const int IsHighImportanceFlag = 16;

		// Token: 0x0400184E RID: 6222
		private const int HasAttachmentsFlag = 32;

		// Token: 0x0400184F RID: 6223
		private const int CurrentFilterConditionVersion = 3;

		// Token: 0x04001850 RID: 6224
		private static readonly PropertyDefinition[] fromProperties = new PropertyDefinition[]
		{
			ItemSchema.SentRepresentingDisplayName,
			ItemSchema.SentRepresentingEmailAddress,
			MessageItemSchema.SenderDisplayName,
			MessageItemSchema.SenderEmailAddress
		};

		// Token: 0x04001851 RID: 6225
		[OwaEventField("fId")]
		public OwaStoreObjectId SourceFolderId;

		// Token: 0x04001852 RID: 6226
		[OwaEventField("fStm", true, false)]
		public bool SendToMe;

		// Token: 0x04001853 RID: 6227
		[OwaEventField("fCtm", true, false)]
		public bool CcToMe;

		// Token: 0x04001854 RID: 6228
		[OwaEventField("fUR", true, false)]
		public bool IsUnread;

		// Token: 0x04001855 RID: 6229
		[OwaEventField("sCat", true, null)]
		public string Categories;

		// Token: 0x04001856 RID: 6230
		[OwaEventField("sTo", true, null)]
		public string To;

		// Token: 0x04001857 RID: 6231
		[OwaEventField("sFrm", true, null)]
		public string From;

		// Token: 0x04001858 RID: 6232
		[OwaEventField("fFlg", true, false)]
		public bool IsFlag;

		// Token: 0x04001859 RID: 6233
		[OwaEventField("fHI", true, false)]
		public bool IsHighImportance;

		// Token: 0x0400185A RID: 6234
		[OwaEventField("fAttch", true, false)]
		public bool HasAttachments;

		// Token: 0x0400185B RID: 6235
		private int version = 3;
	}
}
