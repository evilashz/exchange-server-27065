using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001CD RID: 461
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CompositePhotoHandler : IPhotoHandler
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x000489F0 File Offset: 0x00046BF0
		public CompositePhotoHandler(IPhotoHandler first, IPhotoHandler second)
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

		// Token: 0x0600116E RID: 4462 RVA: 0x00048A22 File Offset: 0x00046C22
		public PhotoResponse Retrieve(PhotoRequest request, PhotoResponse response)
		{
			return this.second.Retrieve(request, this.first.Retrieve(request, response));
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00048A3D File Offset: 0x00046C3D
		public IPhotoHandler Then(IPhotoHandler next)
		{
			return new CompositePhotoHandler(this, next);
		}

		// Token: 0x0400093C RID: 2364
		private readonly IPhotoHandler first;

		// Token: 0x0400093D RID: 2365
		private readonly IPhotoHandler second;
	}
}
