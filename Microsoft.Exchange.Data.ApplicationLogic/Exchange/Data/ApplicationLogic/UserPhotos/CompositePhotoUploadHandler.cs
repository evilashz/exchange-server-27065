using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001CE RID: 462
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CompositePhotoUploadHandler : IPhotoUploadHandler
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x00048A46 File Offset: 0x00046C46
		public CompositePhotoUploadHandler(IPhotoUploadHandler first, IPhotoUploadHandler second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			this.first = first;
			this.second = second;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00048A78 File Offset: 0x00046C78
		public PhotoResponse Upload(PhotoRequest request, PhotoResponse response)
		{
			return this.second.Upload(request, this.first.Upload(request, response));
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00048A93 File Offset: 0x00046C93
		public IPhotoUploadHandler Then(IPhotoUploadHandler next)
		{
			return new CompositePhotoUploadHandler(this, next);
		}

		// Token: 0x0400093E RID: 2366
		private readonly IPhotoUploadHandler first;

		// Token: 0x0400093F RID: 2367
		private readonly IPhotoUploadHandler second;
	}
}
