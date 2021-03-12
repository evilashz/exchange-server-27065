using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000341 RID: 833
	internal class UploadPhotoFromForm : ServiceCommand<UploadPhotoResponse>
	{
		// Token: 0x06001B72 RID: 7026 RVA: 0x00068AF5 File Offset: 0x00066CF5
		public UploadPhotoFromForm(CallContext callContext, HttpRequest request) : base(callContext)
		{
			this.ValidateRequest(request);
			this.fileStream = request.Files[0].InputStream;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00068B1C File Offset: 0x00066D1C
		protected override UploadPhotoResponse InternalExecute()
		{
			PhotoRequest request = new PhotoRequest
			{
				TargetPrimarySmtpAddress = base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(),
				UploadTo = base.CallContext.AccessingPrincipal.ObjectId,
				Preview = true,
				UploadCommand = UploadCommand.Upload,
				RawUploadedPhoto = this.fileStream
			};
			new PhotoUploadPipeline(UploadPhotoFromForm.PhotosConfiguration, base.MailboxIdentityMailboxSession, base.CallContext.ADRecipientSessionContext.GetADRecipientSession(), ExTraceGlobals.UserPhotosTracer).Upload(request, Stream.Null);
			return new UploadPhotoResponse();
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00068BC0 File Offset: 0x00066DC0
		private void ValidateRequest(HttpRequest request)
		{
			UploadPhotoCommand uploadPhotoCommand;
			if (!Enum.TryParse<UploadPhotoCommand>(request.Form["UploadPhotoCommand"], true, out uploadPhotoCommand))
			{
				throw FaultExceptionUtilities.CreateFault(new OwaInvalidRequestException(), FaultParty.Sender);
			}
			if (uploadPhotoCommand != UploadPhotoCommand.UploadPreview || request.Files.Count == 0 || request.Files[0].InputStream == null)
			{
				throw FaultExceptionUtilities.CreateFault(new OwaInvalidRequestException(), FaultParty.Sender);
			}
		}

		// Token: 0x04000F7C RID: 3964
		private const string UploadPhotoCommandFieldName = "UploadPhotoCommand";

		// Token: 0x04000F7D RID: 3965
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04000F7E RID: 3966
		private readonly Stream fileStream;
	}
}
