using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000D78 RID: 3448
	[Cmdlet("Remove", "UserPhoto", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUserPhoto : RemoveRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x1700291B RID: 10523
		// (get) Token: 0x0600844E RID: 33870 RVA: 0x0021CFEC File Offset: 0x0021B1EC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemovePhoto(this.Identity.ToString());
			}
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x0021CFFE File Offset: 0x0021B1FE
		protected override void InternalBeginProcessing()
		{
			this.tracer = new PhotoCmdletTracer(base.IsVerboseOn);
			base.InternalBeginProcessing();
		}

		// Token: 0x06008450 RID: 33872 RVA: 0x0021D018 File Offset: 0x0021B218
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, base.DataObject, false, this.ConfirmationMessage, null);
			try
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(base.DataObject, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Remove-UserPhoto"))
				{
					new PhotoRemovalPipeline(RemoveUserPhoto.PhotosConfiguration, mailboxSession, (IRecipientSession)base.DataSession, this.tracer).Upload(this.CreateRemovePhotoRequest(exchangePrincipal), Stream.Null);
				}
			}
			catch (WrongServerException ex)
			{
				this.LogFailedToRemovePhotoEvent(ex);
				this.WriteError(new CannotModifyPhotoBecauseMailboxIsInTransitException(ex), ExchangeErrorCategory.ServerTransient, base.DataObject, true);
			}
			catch (Exception e)
			{
				this.LogFailedToRemovePhotoEvent(e);
				throw;
			}
			finally
			{
				this.tracer.Dump(new PhotoRequestLogWriter(RemoveUserPhoto.RequestLog, RemoveUserPhoto.GenerateRequestId()));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008451 RID: 33873 RVA: 0x0021D120 File Offset: 0x0021B320
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(CannotMapInvalidSmtpAddressToPhotoFileException).IsInstanceOfType(exception) || typeof(AggregateOperationFailedException).IsInstanceOfType(exception) || typeof(ObjectNotFoundException).IsInstanceOfType(exception) || typeof(IOException).IsInstanceOfType(exception) || typeof(UnauthorizedAccessException).IsInstanceOfType(exception) || typeof(Win32Exception).IsInstanceOfType(exception) || typeof(ADNoSuchObjectException).IsInstanceOfType(exception) || typeof(ADOperationException).IsInstanceOfType(exception) || typeof(StoragePermanentException).IsInstanceOfType(exception) || typeof(StorageTransientException).IsInstanceOfType(exception);
		}

		// Token: 0x06008452 RID: 33874 RVA: 0x0021D1F4 File Offset: 0x0021B3F4
		private PhotoRequest CreateRemovePhotoRequest(ExchangePrincipal principal)
		{
			return new PhotoRequest
			{
				Preview = false,
				UploadCommand = UploadCommand.Clear,
				UploadTo = principal.ObjectId,
				TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString()
			};
		}

		// Token: 0x06008453 RID: 33875 RVA: 0x0021D244 File Offset: 0x0021B444
		private void LogFailedToRemovePhotoEvent(Exception e)
		{
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FailedToRemovePhoto, new string[]
			{
				base.DataObject.ToString(),
				base.DataObject.UserPrincipalName,
				e.ToString()
			});
		}

		// Token: 0x06008454 RID: 33876 RVA: 0x0021D288 File Offset: 0x0021B488
		private static string GenerateRequestId()
		{
			return RandomPhotoRequestIdGenerator.Generate();
		}

		// Token: 0x04004013 RID: 16403
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04004014 RID: 16404
		private static readonly PhotoRequestLog RequestLog = new PhotoRequestLogFactory(RemoveUserPhoto.PhotosConfiguration, ExchangeSetupContext.InstalledVersion.ToString()).Create();

		// Token: 0x04004015 RID: 16405
		private PhotoCmdletTracer tracer;
	}
}
