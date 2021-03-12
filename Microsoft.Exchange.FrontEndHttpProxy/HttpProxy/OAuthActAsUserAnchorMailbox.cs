using System;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000010 RID: 16
	internal class OAuthActAsUserAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00004142 File Offset: 0x00002342
		public OAuthActAsUserAnchorMailbox(OAuthActAsUser actAsUser, IRequestContext requestContext) : base(AnchorSource.OAuthActAsUser, actAsUser, requestContext)
		{
			this.actAsUser = actAsUser;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004164 File Offset: 0x00002364
		protected override ADRawEntry LoadADRawEntry()
		{
			ADRawEntry result;
			try
			{
				ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => this.actAsUser.ADRawEntry);
				result = base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
			}
			catch (InvalidOAuthTokenException ex)
			{
				throw new HttpException((ex.ErrorCategory == OAuthErrorCategory.InternalError) ? 500 : 401, string.Empty, ex);
			}
			return result;
		}

		// Token: 0x0400002E RID: 46
		private readonly OAuthActAsUser actAsUser;
	}
}
