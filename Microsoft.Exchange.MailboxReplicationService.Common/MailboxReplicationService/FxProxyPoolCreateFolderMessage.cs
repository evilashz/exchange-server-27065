using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200024C RID: 588
	internal class FxProxyPoolCreateFolderMessage : DataMessageBase
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x0003F162 File Offset: 0x0003D362
		public FxProxyPoolCreateFolderMessage(FolderRec folderRec)
		{
			this.Data = folderRec;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0003F174 File Offset: 0x0003D374
		public FxProxyPoolCreateFolderMessage(byte[] blob)
		{
			FolderRec folderRec = CommonUtils.DataContractDeserialize<FolderRec>(blob);
			if (folderRec != null)
			{
				this.Data = folderRec;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x0003F198 File Offset: 0x0003D398
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolCreateFolder
				};
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0003F1B2 File Offset: 0x0003D3B2
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0003F1BA File Offset: 0x0003D3BA
		public FolderRec Data { get; private set; }

		// Token: 0x06001E77 RID: 7799 RVA: 0x0003F1C3 File Offset: 0x0003D3C3
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolCreateFolderMessage(data);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0003F1CC File Offset: 0x0003D3CC
		protected override int GetSizeInternal()
		{
			int num = 0;
			if (this.Data != null)
			{
				num = 13;
				if (this.Data.EntryId != null)
				{
					num += this.Data.EntryId.Length;
				}
				if (this.Data.ParentId != null)
				{
					num += this.Data.ParentId.Length;
				}
				if (this.Data.FolderName != null)
				{
					num += this.Data.FolderName.Length * 2;
				}
				if (this.Data.FolderClass != null)
				{
					num += this.Data.FolderClass.Length * 2;
				}
				if (this.Data.AdditionalProps != null)
				{
					foreach (PropValueData propValueData in this.Data.AdditionalProps)
					{
						num += propValueData.GetApproximateSize();
					}
				}
				if (this.Data.PromotedPropertiesList != null)
				{
					num += this.Data.PromotedPropertiesList.Length * 4;
				}
				if (this.Data.Views != null)
				{
					foreach (SortOrderData sortOrderData in this.Data.Views)
					{
						num += 6;
						if (sortOrderData.Members != null)
						{
							num += sortOrderData.Members.Length * 9;
						}
					}
				}
				if (this.Data.ICSViews != null)
				{
					foreach (ICSViewData icsviewData in this.Data.ICSViews)
					{
						num++;
						if (icsviewData.CoveringPropertyTags != null)
						{
							num += 4 * icsviewData.CoveringPropertyTags.Length;
						}
					}
				}
				if (this.Data.Restrictions != null)
				{
					foreach (RestrictionData restrictionData in this.Data.Restrictions)
					{
						num += restrictionData.GetApproximateSize();
					}
				}
			}
			return num;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0003F39A File Offset: 0x0003D59A
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolCreateFolder;
			data = CommonUtils.DataContractSerialize<FolderRec>(this.Data);
		}
	}
}
