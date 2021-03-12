using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A45 RID: 2629
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetFavoritesResponse
	{
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x00104658 File Offset: 0x00102858
		// (set) Token: 0x06004A58 RID: 19032 RVA: 0x00104660 File Offset: 0x00102860
		[DataMember]
		public BaseFolderType[] Favorites { get; internal set; }
	}
}
