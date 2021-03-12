using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000342 RID: 834
	internal sealed class InstallApp : SingleStepServiceCommand<InstallAppRequest, ServiceResultNone>
	{
		// Token: 0x0600177A RID: 6010 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		public InstallApp(CallContext callContext, InstallAppRequest request) : base(callContext, request)
		{
			this.manifestString = request.Manifest;
			ServiceCommandBase.ThrowIfNull(this.manifestString, "manifestString", "InstallApp::ctor");
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0007D7E4 File Offset: 0x0007B9E4
		internal static ServiceResult<ServiceResultNone> InternalExecute(CallContext callContext, bool isUserScope, OrgEmptyMasterTableCache orgEmptyMasterTableCache, string manifestString, Action<ExtensionData> configure)
		{
			ServiceError serviceError = GetExtensibilityContext.RunClientExtensionAction(delegate
			{
				ExtensionData extensionData;
				try
				{
					byte[] buffer = Convert.FromBase64String(manifestString);
					using (Stream stream = new MemoryStream(buffer))
					{
						ExtensionData.ValidateManifestSize(stream.Length, true);
						extensionData = ExtensionData.ParseOsfManifest(stream, null, null, ExtensionType.Private, isUserScope ? ExtensionInstallScope.User : ExtensionInstallScope.Organization, true, DisableReasonType.NotDisabled, string.Empty, null);
					}
				}
				catch (FormatException innerException)
				{
					throw new OwaExtensionOperationException(innerException);
				}
				if (configure != null)
				{
					configure(extensionData);
				}
				MailboxSession mailboxIdentityMailboxSession = callContext.SessionCache.GetMailboxIdentityMailboxSession();
				using (InstalledExtensionTable installedExtensionTable = InstalledExtensionTable.CreateInstalledExtensionTable(null, isUserScope, orgEmptyMasterTableCache, mailboxIdentityMailboxSession))
				{
					installedExtensionTable.InstallExtension(extensionData, isUserScope);
				}
			});
			if (serviceError != null)
			{
				return new ServiceResult<ServiceResultNone>(serviceError);
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0007D844 File Offset: 0x0007BA44
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new InstallAppResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0007D86E File Offset: 0x0007BA6E
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			return InstallApp.InternalExecute(base.CallContext, true, null, this.manifestString, null);
		}

		// Token: 0x04000FD3 RID: 4051
		private readonly string manifestString;
	}
}
