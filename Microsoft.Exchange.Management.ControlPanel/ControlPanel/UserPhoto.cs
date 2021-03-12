using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002ED RID: 749
	[DataContract]
	public class UserPhoto : BaseRow
	{
		// Token: 0x06002D24 RID: 11556 RVA: 0x0008A48A File Offset: 0x0008868A
		public UserPhoto(UserPhotoConfiguration photo) : base(photo.Identity.ToIdentity(string.Empty), photo)
		{
			this.Photo = photo;
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x0008A4AA File Offset: 0x000886AA
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x0008A4B2 File Offset: 0x000886B2
		internal UserPhotoConfiguration Photo { get; private set; }
	}
}
