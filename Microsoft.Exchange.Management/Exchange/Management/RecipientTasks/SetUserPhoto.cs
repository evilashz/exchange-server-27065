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
	// Token: 0x02000D79 RID: 3449
	[Cmdlet("Set", "UserPhoto", DefaultParameterSetName = "UploadPhotoData", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetUserPhoto : SetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x1700291C RID: 10524
		// (get) Token: 0x06008457 RID: 33879 RVA: 0x0021D2C6 File Offset: 0x0021B4C6
		// (set) Token: 0x06008458 RID: 33880 RVA: 0x0021D2DD File Offset: 0x0021B4DD
		[Parameter(Mandatory = true, ParameterSetName = "SavePhoto", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "UploadPhotoStream", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "UploadPhotoData", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "CancelPhoto", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "UploadPreview", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700291D RID: 10525
		// (get) Token: 0x06008459 RID: 33881 RVA: 0x0021D2F0 File Offset: 0x0021B4F0
		// (set) Token: 0x0600845A RID: 33882 RVA: 0x0021D307 File Offset: 0x0021B507
		[Parameter(ParameterSetName = "UploadPreview")]
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "UploadPhotoStream")]
		public Stream PictureStream
		{
			get
			{
				return (Stream)base.Fields["PictureStream"];
			}
			set
			{
				base.Fields["PictureStream"] = value;
			}
		}

		// Token: 0x1700291E RID: 10526
		// (get) Token: 0x0600845B RID: 33883 RVA: 0x0021D31A File Offset: 0x0021B51A
		// (set) Token: 0x0600845C RID: 33884 RVA: 0x0021D331 File Offset: 0x0021B531
		[Parameter(ParameterSetName = "UploadPreview")]
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "UploadPhotoData")]
		public byte[] PictureData
		{
			get
			{
				return (byte[])base.Fields["PictureData"];
			}
			set
			{
				base.Fields["PictureData"] = value;
			}
		}

		// Token: 0x1700291F RID: 10527
		// (get) Token: 0x0600845D RID: 33885 RVA: 0x0021D344 File Offset: 0x0021B544
		// (set) Token: 0x0600845E RID: 33886 RVA: 0x0021D36A File Offset: 0x0021B56A
		[Parameter(Mandatory = true, ParameterSetName = "CancelPhoto")]
		public SwitchParameter Cancel
		{
			get
			{
				return (SwitchParameter)(base.Fields["Cancel"] ?? false);
			}
			set
			{
				base.Fields["Cancel"] = value;
			}
		}

		// Token: 0x17002920 RID: 10528
		// (get) Token: 0x0600845F RID: 33887 RVA: 0x0021D382 File Offset: 0x0021B582
		// (set) Token: 0x06008460 RID: 33888 RVA: 0x0021D3A8 File Offset: 0x0021B5A8
		[Parameter(Mandatory = true, ParameterSetName = "SavePhoto")]
		public SwitchParameter Save
		{
			get
			{
				return (SwitchParameter)(base.Fields["Save"] ?? false);
			}
			set
			{
				base.Fields["Save"] = value;
			}
		}

		// Token: 0x17002921 RID: 10529
		// (get) Token: 0x06008461 RID: 33889 RVA: 0x0021D3C0 File Offset: 0x0021B5C0
		// (set) Token: 0x06008462 RID: 33890 RVA: 0x0021D3E6 File Offset: 0x0021B5E6
		[Parameter(Mandatory = true, ParameterSetName = "UploadPreview")]
		public SwitchParameter Preview
		{
			get
			{
				return (SwitchParameter)(base.Fields["Preview"] ?? false);
			}
			set
			{
				base.Fields["Preview"] = value;
			}
		}

		// Token: 0x17002922 RID: 10530
		// (get) Token: 0x06008463 RID: 33891 RVA: 0x0021D400 File Offset: 0x0021B600
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Save)
				{
					return Strings.ConfirmationMessageSaveUserPhoto(this.Identity.ToString());
				}
				if (this.Cancel)
				{
					return Strings.ConfirmationMessageCancelUserPhoto(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageUploadUserPhoto(this.Identity.ToString());
			}
		}

		// Token: 0x06008464 RID: 33892 RVA: 0x0021D459 File Offset: 0x0021B659
		protected override void InternalBeginProcessing()
		{
			this.tracer = new PhotoCmdletTracer(base.IsVerboseOn);
			base.InternalBeginProcessing();
		}

		// Token: 0x06008465 RID: 33893 RVA: 0x0021D474 File Offset: 0x0021B674
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, (ADUser)configurable, false, this.ConfirmationMessage, null);
			return configurable;
		}

		// Token: 0x06008466 RID: 33894 RVA: 0x0021D4A4 File Offset: 0x0021B6A4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(this.DataObject, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Set-UserPhoto"))
				{
					PhotoRequest request = this.CreateRequest(exchangePrincipal);
					PhotoUploadPipeline photoUploadPipeline = new PhotoUploadPipeline(SetUserPhoto.PhotosConfiguration, mailboxSession, (IRecipientSession)base.DataSession, this.tracer);
					photoUploadPipeline.Upload(request, Stream.Null);
					if (!this.Save && !this.Cancel && !this.Preview)
					{
						photoUploadPipeline.Upload(this.CreateSavePreviewRequest(exchangePrincipal), Stream.Null);
					}
				}
			}
			catch (WrongServerException ex)
			{
				this.LogFailedToUploadPhotoEvent(ex);
				this.WriteError(new CannotModifyPhotoBecauseMailboxIsInTransitException(ex), ExchangeErrorCategory.ServerTransient, this.DataObject, true);
			}
			catch (Exception e)
			{
				this.LogFailedToUploadPhotoEvent(e);
				throw;
			}
			finally
			{
				this.tracer.Dump(new PhotoRequestLogWriter(SetUserPhoto.RequestLog, SetUserPhoto.GenerateRequestId()));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x0021D5D8 File Offset: 0x0021B7D8
		public override object GetDynamicParameters()
		{
			return null;
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x0021D5DC File Offset: 0x0021B7DC
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(CannotMapInvalidSmtpAddressToPhotoFileException).IsInstanceOfType(exception) || typeof(UserPhotoProcessingException).IsInstanceOfType(exception) || typeof(UserPhotoNotFoundException).IsInstanceOfType(exception) || typeof(ObjectNotFoundException).IsInstanceOfType(exception) || typeof(IOException).IsInstanceOfType(exception) || typeof(UnauthorizedAccessException).IsInstanceOfType(exception) || typeof(Win32Exception).IsInstanceOfType(exception) || typeof(ADNoSuchObjectException).IsInstanceOfType(exception) || typeof(ADOperationException).IsInstanceOfType(exception) || typeof(StoragePermanentException).IsInstanceOfType(exception) || typeof(StorageTransientException).IsInstanceOfType(exception);
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x0021D6C4 File Offset: 0x0021B8C4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.Save && !this.Cancel)
			{
				bool flag = this.PictureStream == null || this.PictureStream.Length == 0L;
				bool flag2 = this.PictureData == null || this.PictureData.Length == 0;
				if (flag && flag2)
				{
					this.WriteError(new PhotoMustNotBeBlankException(), ExchangeErrorCategory.Client, this.Identity, false);
				}
			}
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x0021D740 File Offset: 0x0021B940
		private PhotoRequest CreateRequest(ExchangePrincipal principal)
		{
			if (this.Save)
			{
				return this.CreateSavePreviewRequest(principal);
			}
			if (this.Cancel)
			{
				return new PhotoRequest
				{
					TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString(),
					UploadTo = principal.ObjectId,
					Preview = true,
					UploadCommand = UploadCommand.Clear
				};
			}
			return new PhotoRequest
			{
				TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString(),
				UploadTo = principal.ObjectId,
				Preview = true,
				UploadCommand = UploadCommand.Upload,
				RawUploadedPhoto = ((this.PictureStream == null) ? new MemoryStream(this.PictureData) : this.PictureStream)
			};
		}

		// Token: 0x0600846B RID: 33899 RVA: 0x0021D814 File Offset: 0x0021BA14
		private PhotoRequest CreateSavePreviewRequest(ExchangePrincipal principal)
		{
			return new PhotoRequest
			{
				TargetPrimarySmtpAddress = principal.MailboxInfo.PrimarySmtpAddress.ToString(),
				UploadTo = principal.ObjectId,
				Preview = false,
				UploadCommand = UploadCommand.Upload
			};
		}

		// Token: 0x0600846C RID: 33900 RVA: 0x0021D864 File Offset: 0x0021BA64
		private void LogFailedToUploadPhotoEvent(Exception e)
		{
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FailedToUploadPhoto, new string[]
			{
				this.DataObject.ToString(),
				this.DataObject.UserPrincipalName,
				e.ToString()
			});
		}

		// Token: 0x0600846D RID: 33901 RVA: 0x0021D8A8 File Offset: 0x0021BAA8
		private static string GenerateRequestId()
		{
			return RandomPhotoRequestIdGenerator.Generate();
		}

		// Token: 0x04004016 RID: 16406
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04004017 RID: 16407
		private static readonly PhotoRequestLog RequestLog = new PhotoRequestLogFactory(SetUserPhoto.PhotosConfiguration, ExchangeSetupContext.InstalledVersion.ToString()).Create();

		// Token: 0x04004018 RID: 16408
		private PhotoCmdletTracer tracer;

		// Token: 0x02000D7A RID: 3450
		private static class ParameterSet
		{
			// Token: 0x04004019 RID: 16409
			internal const string CancelPhoto = "CancelPhoto";

			// Token: 0x0400401A RID: 16410
			internal const string SavePhoto = "SavePhoto";

			// Token: 0x0400401B RID: 16411
			internal const string UploadPhotoData = "UploadPhotoData";

			// Token: 0x0400401C RID: 16412
			internal const string UploadPhotoStream = "UploadPhotoStream";

			// Token: 0x0400401D RID: 16413
			internal const string UploadPreview = "UploadPreview";
		}
	}
}
