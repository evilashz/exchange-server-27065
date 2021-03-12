using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A44 RID: 2628
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FavoriteFolderNode
	{
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0010461D File Offset: 0x0010281D
		// (set) Token: 0x06004A51 RID: 19025 RVA: 0x00104625 File Offset: 0x00102825
		[DataMember]
		internal VersionedId NavigationNodeId { get; set; }

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x0010462E File Offset: 0x0010282E
		// (set) Token: 0x06004A53 RID: 19027 RVA: 0x00104636 File Offset: 0x00102836
		[DataMember]
		public byte[] NodeOrdinal { get; set; }

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x0010463F File Offset: 0x0010283F
		// (set) Token: 0x06004A55 RID: 19029 RVA: 0x00104647 File Offset: 0x00102847
		[DataMember]
		public BaseFolderType Folder { get; internal set; }
	}
}
