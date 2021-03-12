using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007A6 RID: 1958
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderViewState
	{
		// Token: 0x060049DD RID: 18909 RVA: 0x001351BD File Offset: 0x001333BD
		internal FolderViewState()
		{
			this.Init();
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x001351CB File Offset: 0x001333CB
		// (set) Token: 0x060049DF RID: 18911 RVA: 0x001351D3 File Offset: 0x001333D3
		[DataMember(Name = "FolderId")]
		public FolderIdType FolderId { get; set; }

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x001351DC File Offset: 0x001333DC
		// (set) Token: 0x060049E1 RID: 18913 RVA: 0x001351E4 File Offset: 0x001333E4
		[DataMember(Name = "View", IsRequired = false)]
		public FolderViewType View { get; set; }

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x001351ED File Offset: 0x001333ED
		// (set) Token: 0x060049E3 RID: 18915 RVA: 0x001351F5 File Offset: 0x001333F5
		[DataMember(Name = "SortOrder", IsRequired = false)]
		public SortOrder SortOrder { get; set; }

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x001351FE File Offset: 0x001333FE
		// (set) Token: 0x060049E5 RID: 18917 RVA: 0x00135206 File Offset: 0x00133406
		[DataMember(Name = "SortColumn", IsRequired = false)]
		public FolderViewColumnId SortColumn { get; set; }

		// Token: 0x060049E6 RID: 18918 RVA: 0x00135210 File Offset: 0x00133410
		public static FolderViewState FindViewState(string[] folderViewStates, string folderId)
		{
			if (folderViewStates != null)
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(FolderViewState));
				foreach (string s in folderViewStates)
				{
					try
					{
						using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
						{
							FolderViewState folderViewState = dataContractJsonSerializer.ReadObject(memoryStream) as FolderViewState;
							if (folderViewState != null && folderViewState.FolderId != null && folderViewState.FolderId.Id == folderId)
							{
								return folderViewState;
							}
						}
					}
					catch (IOException)
					{
					}
					catch (InvalidDataContractException)
					{
					}
					catch (SerializationException)
					{
					}
				}
			}
			return FolderViewState.Default;
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x001352E4 File Offset: 0x001334E4
		public SortBy[] ConvertToSortBy()
		{
			return MailSortOptions.GetSortByForFolderViewState(this) ?? FolderViewState.defaultSortBy;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x001352F5 File Offset: 0x001334F5
		[OnDeserializing]
		private void SetValuesOnDeserializing(StreamingContext streamingContext)
		{
			this.Init();
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x001352FD File Offset: 0x001334FD
		private void Init()
		{
			this.FolderId = new FolderIdType();
			this.SortColumn = FolderViewColumnId.DateTime;
			this.SortOrder = SortOrder.Descending;
			this.View = FolderViewType.ConversationView;
		}

		// Token: 0x040027DB RID: 10203
		public static FolderViewState Default = new FolderViewState();

		// Token: 0x040027DC RID: 10204
		private static SortBy[] defaultSortBy = new SortBy[]
		{
			new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
		};
	}
}
