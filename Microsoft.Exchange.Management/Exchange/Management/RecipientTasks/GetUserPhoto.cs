using System;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000D75 RID: 3445
	[Cmdlet("Get", "UserPhoto", DefaultParameterSetName = "Identity")]
	public sealed class GetUserPhoto : GetRecipientBase<MailboxIdParameter, ADUser>
	{
		// Token: 0x17002917 RID: 10519
		// (get) Token: 0x0600842A RID: 33834 RVA: 0x0021C9E9 File Offset: 0x0021ABE9
		// (set) Token: 0x0600842B RID: 33835 RVA: 0x0021CA0F File Offset: 0x0021AC0F
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002918 RID: 10520
		// (get) Token: 0x0600842C RID: 33836 RVA: 0x0021CA27 File Offset: 0x0021AC27
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return RecipientConstants.GetMailboxOrSyncMailbox_AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x17002919 RID: 10521
		// (get) Token: 0x0600842D RID: 33837 RVA: 0x0021CA2E File Offset: 0x0021AC2E
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetUserPhoto.SortPropertiesArray;
			}
		}

		// Token: 0x1700291A RID: 10522
		// (get) Token: 0x0600842E RID: 33838 RVA: 0x0021CA35 File Offset: 0x0021AC35
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return new MailboxSchema();
			}
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x0021CA3C File Offset: 0x0021AC3C
		protected override void InternalBeginProcessing()
		{
			this.tracer = new PhotoCmdletTracer(base.IsVerboseOn);
			base.InternalBeginProcessing();
		}

		// Token: 0x06008430 RID: 33840 RVA: 0x0021CA58 File Offset: 0x0021AC58
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser aduser = (ADUser)dataObject;
			UserPhotoConfiguration userPhotoConfiguration = new UserPhotoConfiguration(dataObject.Identity, Stream.Null, null);
			if (CmdletProxy.TryToProxyOutputObject(userPhotoConfiguration, base.CurrentTaskContext, aduser, this.Identity == null, this.ConfirmationMessage, CmdletProxy.AppendIdentityToProxyCmdlet(aduser)))
			{
				return userPhotoConfiguration;
			}
			IConfigurable result;
			try
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-UserPhoto"))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						PhotoManagementRetrievalPipeline photoManagementRetrievalPipeline = new PhotoManagementRetrievalPipeline(GetUserPhoto.PhotosConfiguration, mailboxSession, (IRecipientSession)base.DataSession, this.tracer);
						PhotoResponse photoResponse = photoManagementRetrievalPipeline.Retrieve(this.CreateRetrievePhotoRequest(exchangePrincipal), memoryStream);
						HttpStatusCode status = photoResponse.Status;
						if (status != HttpStatusCode.OK && status == HttpStatusCode.NotFound)
						{
							this.WriteError(new UserPhotoNotFoundException(this.Preview), ExchangeErrorCategory.Client, this.Identity, true);
							throw new InvalidOperationException();
						}
						memoryStream.Seek(0L, SeekOrigin.Begin);
						result = new UserPhotoConfiguration(dataObject.Identity, memoryStream, photoResponse.Thumbprint);
					}
				}
			}
			catch (UserPhotoNotFoundException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FailedToRetrievePhoto, new string[]
				{
					dataObject.ToString(),
					((ADUser)dataObject).UserPrincipalName,
					ex.ToString()
				});
				throw;
			}
			finally
			{
				this.tracer.Dump(new PhotoRequestLogWriter(GetUserPhoto.RequestLog, GetUserPhoto.GenerateRequestId()));
			}
			return result;
		}

		// Token: 0x06008431 RID: 33841 RVA: 0x0021CC58 File Offset: 0x0021AE58
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ADNoSuchObjectException).IsInstanceOfType(exception) || typeof(ADOperationException).IsInstanceOfType(exception) || typeof(IOException).IsInstanceOfType(exception) || typeof(UserPhotoNotFoundException).IsInstanceOfType(exception) || typeof(StoragePermanentException).IsInstanceOfType(exception) || typeof(StorageTransientException).IsInstanceOfType(exception);
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x0021CCDC File Offset: 0x0021AEDC
		private PhotoRequest CreateRetrievePhotoRequest(ExchangePrincipal principal)
		{
			return new PhotoRequest
			{
				Preview = this.Preview,
				Size = UserPhotoSize.HR240x240,
				UploadTo = principal.ObjectId
			};
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x0021CD14 File Offset: 0x0021AF14
		private static string GenerateRequestId()
		{
			return RandomPhotoRequestIdGenerator.Generate();
		}

		// Token: 0x0400400D RID: 16397
		private const UserPhotoSize SizeToReturn = UserPhotoSize.HR240x240;

		// Token: 0x0400400E RID: 16398
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Id
		};

		// Token: 0x0400400F RID: 16399
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04004010 RID: 16400
		private static readonly PhotoRequestLog RequestLog = new PhotoRequestLogFactory(GetUserPhoto.PhotosConfiguration, ExchangeSetupContext.InstalledVersion.ToString()).Create();

		// Token: 0x04004011 RID: 16401
		private PhotoCmdletTracer tracer;
	}
}
