using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A46 RID: 2630
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateFavoriteFolderRequest
	{
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x00104671 File Offset: 0x00102871
		// (set) Token: 0x06004A5B RID: 19035 RVA: 0x00104679 File Offset: 0x00102879
		[DataMember(Name = "Folder", IsRequired = true)]
		public BaseFolderType Folder { get; set; }

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x00104682 File Offset: 0x00102882
		// (set) Token: 0x06004A5D RID: 19037 RVA: 0x0010468A File Offset: 0x0010288A
		[DataMember(Name = "Operation", IsRequired = true)]
		public UpdateFavoriteOperationType Operation { get; set; }

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x00104693 File Offset: 0x00102893
		// (set) Token: 0x06004A5F RID: 19039 RVA: 0x0010469B File Offset: 0x0010289B
		[DataMember(Name = "TargetFolderId", IsRequired = false)]
		public FolderId TargetFolderId { get; set; }

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x001046A4 File Offset: 0x001028A4
		// (set) Token: 0x06004A61 RID: 19041 RVA: 0x001046AC File Offset: 0x001028AC
		[DataMember(Name = "MoveType", IsRequired = false)]
		public MoveFavoriteFolderType? MoveType { get; set; }
	}
}
