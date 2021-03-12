using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A6E RID: 2670
	[Serializable]
	public sealed class PublicFolderMailboxHierarchyInfo : ConfigurableObject
	{
		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06006198 RID: 24984 RVA: 0x0019C7E7 File Offset: 0x0019A9E7
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PublicFolderMailboxHierarchyInfo.schema;
			}
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x0019C7EE File Offset: 0x0019A9EE
		private PublicFolderMailboxHierarchyInfo() : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			this.propertyBag[SimpleProviderObjectSchema.Identity] = new PublicFolderHierarchyInfoId();
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x0600619A RID: 24986 RVA: 0x0019C81F File Offset: 0x0019AA1F
		public int TotalFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.TotalFolderCount];
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x0600619B RID: 24987 RVA: 0x0019C831 File Offset: 0x0019AA31
		public int MaxFolderChildCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.MaxFolderChildCount];
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x0600619C RID: 24988 RVA: 0x0019C843 File Offset: 0x0019AA43
		public int HierarchyDepth
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.HierarchyDepth];
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x0600619D RID: 24989 RVA: 0x0019C855 File Offset: 0x0019AA55
		public int MailPublicFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.MailPublicFolderCount];
			}
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x0600619E RID: 24990 RVA: 0x0019C867 File Offset: 0x0019AA67
		public int CalendarFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.CalendarFolderCount];
			}
		}

		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x0600619F RID: 24991 RVA: 0x0019C879 File Offset: 0x0019AA79
		public int ContactFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.ContactFolderCount];
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x060061A0 RID: 24992 RVA: 0x0019C88B File Offset: 0x0019AA8B
		public int InfoPathFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.InfoPathFolderCount];
			}
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x060061A1 RID: 24993 RVA: 0x0019C89D File Offset: 0x0019AA9D
		public int JournalFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.JournalFolderCount];
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x060061A2 RID: 24994 RVA: 0x0019C8AF File Offset: 0x0019AAAF
		public int StickyNoteFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.StickyNoteFolderCount];
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x060061A3 RID: 24995 RVA: 0x0019C8C1 File Offset: 0x0019AAC1
		public int TaskFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.TaskFolderCount];
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x060061A4 RID: 24996 RVA: 0x0019C8D3 File Offset: 0x0019AAD3
		public int NoteFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.NoteFolderCount];
			}
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x0019C8E5 File Offset: 0x0019AAE5
		public int OtherFolderCount
		{
			get
			{
				return (int)this[PublicFolderHierarchyInfoSchema.OtherFolderCount];
			}
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x060061A6 RID: 24998 RVA: 0x0019C8F7 File Offset: 0x0019AAF7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x0019C900 File Offset: 0x0019AB00
		internal static PublicFolderMailboxHierarchyInfo LoadInfo(PublicFolderSession session, Action<LocalizedString, LocalizedString, int> writeProgress)
		{
			int num = 1;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 1;
			using (Folder folder = Folder.Bind(session, session.GetIpmSubtreeFolderId(), PublicFolderMailboxHierarchyInfo.PublicFoldersProperties))
			{
				num2 = folder.GetValueOrDefault<int>(FolderSchema.ChildCount, 0);
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, PublicFolderMailboxHierarchyInfo.PublicFoldersProperties))
				{
					int estimatedRowCount = queryResult.EstimatedRowCount;
					bool flag = false;
					int num13 = 0;
					int num14 = 0;
					for (;;)
					{
						object[][] rows = queryResult.GetRows(10000);
						if (rows == null || rows.Length == 0)
						{
							break;
						}
						if (num14 == 0 && estimatedRowCount > rows.Length && writeProgress != null)
						{
							flag = true;
						}
						foreach (object[] array2 in rows)
						{
							num14++;
							if (flag)
							{
								if (num14 > estimatedRowCount)
								{
									estimatedRowCount = queryResult.EstimatedRowCount;
								}
								int num15 = num14 * 100 / estimatedRowCount;
								if (num15 != num13 && num15 <= 100)
								{
									writeProgress(ClientStrings.PublicFolderMailboxHierarchyInfo, ClientStrings.PublicFolderMailboxInfoFolderEnumeration(num14, estimatedRowCount), num15);
									num13 = num15;
								}
							}
							object obj = array2[0];
							int num16 = (obj is int) ? ((int)obj) : 0;
							object obj2 = array2[1];
							bool flag2 = obj2 is bool && (bool)obj2;
							object obj3 = array2[2];
							int num17 = (obj3 is int) ? ((int)obj3) : 0;
							string containerClass = array2[3] as string;
							num++;
							if (num4 < num16)
							{
								num4 = num16;
							}
							if (flag2)
							{
								num3++;
							}
							if (num2 < num17)
							{
								num2 = num17;
							}
							if (ObjectClass.IsCalendarFolder(containerClass))
							{
								num5++;
							}
							else if (ObjectClass.IsContactsFolder(containerClass))
							{
								num6++;
							}
							else if (ObjectClass.IsInfoPathFormFolder(containerClass))
							{
								num7++;
							}
							else if (ObjectClass.IsJournalFolder(containerClass))
							{
								num8++;
							}
							else if (ObjectClass.IsMessageFolder(containerClass))
							{
								num11++;
							}
							else if (ObjectClass.IsNotesFolder(containerClass))
							{
								num9++;
							}
							else if (ObjectClass.IsTaskFolder(containerClass))
							{
								num10++;
							}
							else
							{
								num12++;
							}
						}
					}
				}
			}
			PublicFolderMailboxHierarchyInfo publicFolderMailboxHierarchyInfo = new PublicFolderMailboxHierarchyInfo();
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.TotalFolderCount] = num;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.MailPublicFolderCount] = num3;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.MaxFolderChildCount] = num2;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.HierarchyDepth] = num4;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.CalendarFolderCount] = num5;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.ContactFolderCount] = num6;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.InfoPathFolderCount] = num7;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.JournalFolderCount] = num8;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.NoteFolderCount] = num11;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.StickyNoteFolderCount] = num9;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.TaskFolderCount] = num10;
			publicFolderMailboxHierarchyInfo[PublicFolderHierarchyInfoSchema.OtherFolderCount] = num12;
			publicFolderMailboxHierarchyInfo.propertyBag.ResetChangeTracking();
			return publicFolderMailboxHierarchyInfo;
		}

		// Token: 0x04003760 RID: 14176
		private const int FolderHierarchyDepthPropertyIndex = 0;

		// Token: 0x04003761 RID: 14177
		private const int MailEnabledPropertyIndex = 1;

		// Token: 0x04003762 RID: 14178
		private const int ChildCountPropertyIndex = 2;

		// Token: 0x04003763 RID: 14179
		private const int ContainerClassPropertyIndex = 3;

		// Token: 0x04003764 RID: 14180
		private static readonly PublicFolderHierarchyInfoSchema schema = ObjectSchema.GetInstance<PublicFolderHierarchyInfoSchema>();

		// Token: 0x04003765 RID: 14181
		private static readonly PropertyDefinition[] PublicFoldersProperties = new PropertyDefinition[]
		{
			FolderSchema.FolderHierarchyDepth,
			FolderSchema.MailEnabled,
			FolderSchema.ChildCount,
			InternalSchema.ContainerClass
		};
	}
}
