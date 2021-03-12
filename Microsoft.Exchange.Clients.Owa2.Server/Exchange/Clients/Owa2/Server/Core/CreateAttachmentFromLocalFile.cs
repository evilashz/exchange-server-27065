using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F8 RID: 760
	internal class CreateAttachmentFromLocalFile : ServiceCommand<CreateAttachmentResponse>
	{
		// Token: 0x06001992 RID: 6546 RVA: 0x00059C24 File Offset: 0x00057E24
		public CreateAttachmentFromLocalFile(CallContext callContext, CreateAttachmentRequest request) : base(callContext)
		{
			this.request = request;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00059C34 File Offset: 0x00057E34
		protected override CreateAttachmentResponse InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			CreateAttachmentResponse createAttachmentResponse;
			if (this.request.CancellationId != null && userContext.CancelAttachmentManager.OnCreateAttachment(this.request.CancellationId, null))
			{
				createAttachmentResponse = CreateAttachmentHelper.BuildCreateAttachmentResponseForCancelled();
			}
			else
			{
				createAttachmentResponse = CreateAttachmentFromLocalFile.CreateAttachment(this.request);
				if (this.request.CancellationId != null)
				{
					AttachmentIdType attachmentIdFromCreateAttachmentResponse = CreateAttachmentHelper.GetAttachmentIdFromCreateAttachmentResponse(createAttachmentResponse);
					userContext.CancelAttachmentManager.CreateAttachmentCompleted(this.request.CancellationId, attachmentIdFromCreateAttachmentResponse);
				}
			}
			return createAttachmentResponse;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00059CC4 File Offset: 0x00057EC4
		public static CreateAttachmentResponse CreateAttachment(CreateAttachmentRequest request)
		{
			CreateAttachmentJsonRequest createAttachmentJsonRequest = new CreateAttachmentJsonRequest();
			createAttachmentJsonRequest.Body = request;
			OWAService owaservice = new OWAService();
			IAsyncResult asyncResult = owaservice.BeginCreateAttachment(createAttachmentJsonRequest, null, null);
			asyncResult.AsyncWaitHandle.WaitOne();
			return owaservice.EndCreateAttachment(asyncResult).Body;
		}

		// Token: 0x04000E24 RID: 3620
		private CreateAttachmentRequest request;
	}
}
