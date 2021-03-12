using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000340 RID: 832
	internal class UploadPhoto : ServiceCommand<UploadPhotoResponse>
	{
		// Token: 0x06001B6C RID: 7020 RVA: 0x00068698 File Offset: 0x00066898
		public UploadPhoto(CallContext callContext, UploadPhotoRequest uploadPhotoRequest) : base(callContext)
		{
			this.uploadPhotoRequest = uploadPhotoRequest;
			if (string.IsNullOrEmpty(this.uploadPhotoRequest.EmailAddress))
			{
				throw FaultExceptionUtilities.CreateFault(new OwaInvalidRequestException(), FaultParty.Sender);
			}
			this.adRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId), 60, ".ctor", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\ServiceCommands\\UploadPhoto.cs");
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0006874C File Offset: 0x0006694C
		protected override UploadPhotoResponse InternalExecute()
		{
			DisposeGuard disposeGuard = default(DisposeGuard);
			Action action = null;
			ExchangePrincipal exchangePrincipal;
			MailboxSession mailboxSession;
			if (this.IsRequestForCurrentUser())
			{
				exchangePrincipal = base.CallContext.AccessingPrincipal;
				mailboxSession = base.CallContext.SessionCache.GetMailboxSessionBySmtpAddress(this.uploadPhotoRequest.EmailAddress);
			}
			else
			{
				ProxyAddress proxyAddress = ProxyAddress.Parse(this.uploadPhotoRequest.EmailAddress);
				ADUser groupAdUser = this.adRecipientSession.FindByProxyAddress(proxyAddress) as ADUser;
				if (groupAdUser == null)
				{
					throw FaultExceptionUtilities.CreateFault(new OwaInvalidRequestException(), FaultParty.Sender);
				}
				if (groupAdUser.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox || !this.IsOwnedModernGroup(groupAdUser))
				{
					OwaInvalidOperationException exception = new OwaInvalidOperationException(string.Format("User does not have sufficient privileges on {0}", this.uploadPhotoRequest.EmailAddress));
					throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
				}
				if (groupAdUser.IsCached)
				{
					this.adRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(groupAdUser.OriginatingServer, false, ConsistencyMode.IgnoreInvalid, this.adRecipientSession.SessionSettings, 102, "InternalExecute", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\ServiceCommands\\UploadPhoto.cs");
				}
				exchangePrincipal = ExchangePrincipal.FromADUser(groupAdUser, null);
				mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, base.CallContext.ClientCulture, "Client=OWA;Action=GroupPhotoUpload");
				action = delegate()
				{
					DirectorySessionFactory.Default.GetTenantOrRootRecipientReadOnlySession(this.adRecipientSession, groupAdUser.OriginatingServer, 125, "InternalExecute", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\ServiceCommands\\UploadPhoto.cs").FindByProxyAddress(proxyAddress);
				};
				disposeGuard.Add<MailboxSession>(mailboxSession);
			}
			using (disposeGuard)
			{
				PhotoRequest request = this.CreateRequest(exchangePrincipal);
				new PhotoUploadPipeline(UploadPhoto.PhotosConfiguration, mailboxSession, this.adRecipientSession, ExTraceGlobals.UserPhotosTracer).Upload(request, Stream.Null);
				if (action != null)
				{
					action();
				}
			}
			return new UploadPhotoResponse();
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00068918 File Offset: 0x00066B18
		private bool IsRequestForCurrentUser()
		{
			return StringComparer.OrdinalIgnoreCase.Compare(this.uploadPhotoRequest.EmailAddress, base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString()) == 0;
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00068960 File Offset: 0x00066B60
		private bool IsOwnedModernGroup(ADUser groupMailboxAdUser)
		{
			return groupMailboxAdUser.Owners.Contains(base.CallContext.AccessingPrincipal.ObjectId);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00068980 File Offset: 0x00066B80
		private PhotoRequest CreateRequest(IExchangePrincipal principal)
		{
			switch (this.uploadPhotoRequest.Command)
			{
			case UploadPhotoCommand.UploadPhoto:
				return new PhotoRequest
				{
					TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString(),
					UploadTo = principal.ObjectId,
					Preview = false,
					UploadCommand = UploadCommand.Upload
				};
			case UploadPhotoCommand.UploadPreview:
				return new PhotoRequest
				{
					TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString(),
					UploadTo = principal.ObjectId,
					Preview = true,
					UploadCommand = UploadCommand.Upload,
					RawUploadedPhoto = new MemoryStream(Convert.FromBase64String(this.uploadPhotoRequest.Content))
				};
			case UploadPhotoCommand.ClearPhoto:
				return new PhotoRequest
				{
					Preview = false,
					UploadCommand = UploadCommand.Clear,
					UploadTo = principal.ObjectId,
					TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString()
				};
			case UploadPhotoCommand.ClearPreview:
				return new PhotoRequest
				{
					TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString(),
					UploadTo = principal.ObjectId,
					Preview = true,
					UploadCommand = UploadCommand.Clear
				};
			default:
				throw FaultExceptionUtilities.CreateFault(new OwaInvalidRequestException(), FaultParty.Sender);
			}
		}

		// Token: 0x04000F79 RID: 3961
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04000F7A RID: 3962
		private readonly UploadPhotoRequest uploadPhotoRequest;

		// Token: 0x04000F7B RID: 3963
		private IRecipientSession adRecipientSession;
	}
}
