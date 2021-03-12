using System;
using System.Linq;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EEA RID: 3818
	[AllowedOAuthGrant("Mail.Send")]
	[AllowedOAuthGrant("Mail.Write")]
	internal class CreateMessageRequest : CreateEntityRequest<Message>
	{
		// Token: 0x06006286 RID: 25222 RVA: 0x001341A2 File Offset: 0x001323A2
		public CreateMessageRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06006287 RID: 25223 RVA: 0x001341AB File Offset: 0x001323AB
		// (set) Token: 0x06006288 RID: 25224 RVA: 0x001341B3 File Offset: 0x001323B3
		public string ParentFolderId { get; protected set; }

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x001341BC File Offset: 0x001323BC
		// (set) Token: 0x0600628A RID: 25226 RVA: 0x001341C4 File Offset: 0x001323C4
		public MessageDisposition MessageDisposition { get; protected set; }

		// Token: 0x0600628B RID: 25227 RVA: 0x001341D0 File Offset: 0x001323D0
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.EntitySegment is NavigationPropertySegment)
			{
				if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment && base.ODataContext.ODataPath.ParentOfEntitySegment.EdmType.Equals(Folder.EdmEntityType))
				{
					this.ParentFolderId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
				}
				else if (base.ODataContext.ODataPath.ParentOfEntitySegment is NavigationPropertySegment)
				{
					this.ParentFolderId = base.ODataContext.ODataPath.ParentOfEntitySegment.GetPropertyName();
				}
			}
			MessageDisposition? queryEnumValue = base.ODataContext.QueryString.GetQueryEnumValue("MessageDisposition");
			if (queryEnumValue == null)
			{
				if (string.IsNullOrEmpty(this.ParentFolderId))
				{
					queryEnumValue = new MessageDisposition?(MessageDisposition.SendOnly);
				}
				else
				{
					queryEnumValue = new MessageDisposition?(MessageDisposition.SaveOnly);
				}
			}
			this.MessageDisposition = queryEnumValue.Value;
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x001342D0 File Offset: 0x001324D0
		public override void PerformAdditionalGrantCheck(string[] grantPresented)
		{
			base.PerformAdditionalGrantCheck(grantPresented);
			if (this.MessageDisposition == MessageDisposition.SendOnly && grantPresented.Contains("Mail.Send"))
			{
				return;
			}
			if (this.MessageDisposition == MessageDisposition.SaveOnly && grantPresented.Contains("Mail.Write"))
			{
				return;
			}
			if (this.MessageDisposition == MessageDisposition.SendAndSaveCopy && grantPresented.Contains("Mail.Send") && grantPresented.Contains("Mail.Write"))
			{
				return;
			}
			throw new ODataAuthorizationException(new InvalidOAuthTokenException(OAuthErrors.NotEnoughGrantPresented, null, null));
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x00134346 File Offset: 0x00132546
		public override ODataCommand GetODataCommand()
		{
			return new CreateMessageCommand(this);
		}

		// Token: 0x040034FE RID: 13566
		public const string MessageDispositionParameter = "MessageDisposition";
	}
}
