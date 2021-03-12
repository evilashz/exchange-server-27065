using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A6B RID: 2667
	[Serializable]
	public sealed class PublicFolderMailboxDumpsterInfo : ConfigurableObject
	{
		// Token: 0x06006187 RID: 24967 RVA: 0x0019C283 File Offset: 0x0019A483
		private PublicFolderMailboxDumpsterInfo() : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			this.propertyBag[SimpleProviderObjectSchema.Identity] = new PublicFolderDumpsterInfoId();
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x06006188 RID: 24968 RVA: 0x0019C2B4 File Offset: 0x0019A4B4
		public string DumpsterHolderEntryId
		{
			get
			{
				return (string)this[PublicFolderDumpsterInfoSchema.DumpsterHolderEntryId];
			}
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06006189 RID: 24969 RVA: 0x0019C2C6 File Offset: 0x0019A4C6
		public int CountTotalFolders
		{
			get
			{
				return (int)this[PublicFolderDumpsterInfoSchema.CountTotalFolders];
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x0600618A RID: 24970 RVA: 0x0019C2D8 File Offset: 0x0019A4D8
		public bool HasDumpsterExtended
		{
			get
			{
				return (bool)this[PublicFolderDumpsterInfoSchema.HasDumpsterExtended];
			}
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x0600618B RID: 24971 RVA: 0x0019C2EA File Offset: 0x0019A4EA
		public int CountLegacyDumpsters
		{
			get
			{
				return (int)this[PublicFolderDumpsterInfoSchema.CountLegacyDumpsters];
			}
		}

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x0600618C RID: 24972 RVA: 0x0019C2FC File Offset: 0x0019A4FC
		public int CountContainerLevel1
		{
			get
			{
				return (int)this[PublicFolderDumpsterInfoSchema.CountContainerLevel1];
			}
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x0600618D RID: 24973 RVA: 0x0019C30E File Offset: 0x0019A50E
		public int CountContainerLevel2
		{
			get
			{
				return (int)this[PublicFolderDumpsterInfoSchema.CountContainerLevel2];
			}
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x0600618E RID: 24974 RVA: 0x0019C320 File Offset: 0x0019A520
		public int CountDumpsters
		{
			get
			{
				return (int)this[PublicFolderDumpsterInfoSchema.CountDumpsters];
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x0600618F RID: 24975 RVA: 0x0019C332 File Offset: 0x0019A532
		public int CountDeletedFolders
		{
			get
			{
				return (int)this[PublicFolderDumpsterInfoSchema.CountDeletedFolders];
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06006190 RID: 24976 RVA: 0x0019C344 File Offset: 0x0019A544
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PublicFolderMailboxDumpsterInfo.schema;
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06006191 RID: 24977 RVA: 0x0019C34B File Offset: 0x0019A54B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x0019C354 File Offset: 0x0019A554
		internal static PublicFolderMailboxDumpsterInfo LoadInfo(PublicFolderSession session, Action<LocalizedString, LocalizedString, int> writeProgress)
		{
			string value = null;
			int num = 0;
			bool flag = false;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			using (Folder folder = Folder.Bind(session, session.GetDumpsterRootFolderId(), PublicFolderMailboxDumpsterInfo.DumpsterPropertiesToLoad))
			{
				byte[] valueOrDefault = folder.GetValueOrDefault<byte[]>(FolderSchema.PublicFolderDumpsterHolderEntryId, null);
				if (valueOrDefault != null)
				{
					value = session.IdConverter.CreateFolderId(session.IdConverter.GetIdFromLongTermId(valueOrDefault)).ToHexEntryId();
				}
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, PublicFolderMailboxDumpsterInfo.DumpsterSubfoldersProperties))
				{
					int estimatedRowCount = queryResult.EstimatedRowCount;
					bool flag2 = false;
					int num7 = 0;
					for (;;)
					{
						object[][] rows = queryResult.GetRows(10000);
						if (rows == null || rows.Length == 0)
						{
							break;
						}
						if (num == 0 && estimatedRowCount > rows.Length && writeProgress != null)
						{
							flag2 = true;
						}
						foreach (object[] array2 in rows)
						{
							num++;
							if (flag2)
							{
								if (num > estimatedRowCount)
								{
									estimatedRowCount = queryResult.EstimatedRowCount;
								}
								int num8 = num * 100 / estimatedRowCount;
								if (num8 != num7 && num8 <= 100)
								{
									writeProgress(ClientStrings.PublicFolderMailboxDumpsterInfo, ClientStrings.PublicFolderMailboxInfoFolderEnumeration(num, estimatedRowCount), num8);
									num7 = num8;
								}
							}
							string text = array2[1] as string;
							if (!string.IsNullOrEmpty(text))
							{
								bool flag3 = StringComparer.OrdinalIgnoreCase.Equals("\\NON_IPM_SUBTREE\\DUMPSTER_ROOT\\DUMPSTER_EXTEND", text);
								if (flag3)
								{
									flag = true;
								}
								else if (!text.StartsWith("\\NON_IPM_SUBTREE\\DUMPSTER_ROOT\\DUMPSTER_EXTEND\\", StringComparison.OrdinalIgnoreCase))
								{
									num2++;
								}
								else
								{
									object obj = array2[0];
									if (obj is int)
									{
										int num9 = (int)obj;
										if (num9 == 2)
										{
											num3++;
										}
										else if (num9 == 3)
										{
											num4++;
										}
										else if (num9 == 4)
										{
											num5++;
										}
										else if (num9 > 4)
										{
											num6++;
										}
									}
								}
							}
						}
					}
				}
			}
			PublicFolderMailboxDumpsterInfo publicFolderMailboxDumpsterInfo = new PublicFolderMailboxDumpsterInfo();
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.DumpsterHolderEntryId] = value;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.CountTotalFolders] = num;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.HasDumpsterExtended] = flag;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.CountLegacyDumpsters] = num2;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.CountContainerLevel1] = num3;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.CountContainerLevel2] = num4;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.CountDumpsters] = num5;
			publicFolderMailboxDumpsterInfo[PublicFolderDumpsterInfoSchema.CountDeletedFolders] = num6;
			publicFolderMailboxDumpsterInfo.propertyBag.ResetChangeTracking();
			return publicFolderMailboxDumpsterInfo;
		}

		// Token: 0x04003751 RID: 14161
		private const int FolderHierarchyDepthPropertyIndex = 0;

		// Token: 0x04003752 RID: 14162
		private const int FolderPathNamePropertyIndex = 1;

		// Token: 0x04003753 RID: 14163
		private const string DumpsterExtendedFolderPath = "\\NON_IPM_SUBTREE\\DUMPSTER_ROOT\\DUMPSTER_EXTEND";

		// Token: 0x04003754 RID: 14164
		private const string UnderDumpsterExtendedFolderPath = "\\NON_IPM_SUBTREE\\DUMPSTER_ROOT\\DUMPSTER_EXTEND\\";

		// Token: 0x04003755 RID: 14165
		private static readonly PublicFolderDumpsterInfoSchema schema = ObjectSchema.GetInstance<PublicFolderDumpsterInfoSchema>();

		// Token: 0x04003756 RID: 14166
		private static readonly PropertyDefinition[] DumpsterPropertiesToLoad = new PropertyDefinition[]
		{
			FolderSchema.PublicFolderDumpsterHolderEntryId
		};

		// Token: 0x04003757 RID: 14167
		private static readonly PropertyDefinition[] DumpsterSubfoldersProperties = new PropertyDefinition[]
		{
			FolderSchema.FolderHierarchyDepth,
			FolderSchema.FolderPathName
		};
	}
}
