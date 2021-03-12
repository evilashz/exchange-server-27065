using System;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002C4 RID: 708
	internal sealed class CreateManagedFolder : MultiStepServiceCommand<CreateManagedFolderRequest, BaseFolderType>
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x00062294 File Offset: 0x00060494
		public CreateManagedFolder(CallContext callContext, CreateManagedFolderRequest request) : base(callContext, request)
		{
			this.folderNames = base.Request.FolderNames;
			ServiceCommandBase.ThrowIfNull(this.folderNames, "folderNames", "CreateManagedFolder::Execute");
			this.mailbox = base.Request.Mailbox;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000622E0 File Offset: 0x000604E0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateManagedFolderResponse createManagedFolderResponse = new CreateManagedFolderResponse();
			createManagedFolderResponse.BuildForResults<BaseFolderType>(base.Results);
			return createManagedFolderResponse;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00062300 File Offset: 0x00060500
		internal override int StepCount
		{
			get
			{
				return this.folderNames.Length;
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0006230C File Offset: 0x0006050C
		internal override void PreExecuteCommand()
		{
			string text;
			if (this.mailbox == null)
			{
				text = base.CallContext.GetEffectiveAccessingSmtpAddress();
				if (string.IsNullOrEmpty(text))
				{
					throw new MissingEmailAddressForManagedFolderException();
				}
			}
			else
			{
				text = CreateManagedFolder.ExtractEmailAddress(this.mailbox);
				if (string.IsNullOrEmpty(text))
				{
					throw new NonExistentMailboxException((CoreResources.IDs)4088802584U, string.Empty);
				}
			}
			ExchangePrincipal fromCache = ExchangePrincipalCache.GetFromCache(text, base.CallContext.ADRecipientSessionContext);
			RecipientIdentity recipientIdentity;
			if (!ADIdentityInformationCache.Singleton.TryGetRecipientIdentity(base.CallContext.EffectiveCallerSid, base.CallContext.ADRecipientSessionContext, out recipientIdentity))
			{
				ExTraceGlobals.SessionCacheTracer.TraceDebug<string, SecurityIdentifier>(0L, "[CreatemanageFolder:CreateManagedFolders] In {0} access mode, we found that the caller needed delegate access to a mailbox, but the caller is actually a computer account and not a recipient and therefore cannot possibly have delegate rights to your mailbox.  Caller sid: {1}", (base.CallContext.MailboxAccessType == MailboxAccessType.Normal) ? "normal" : "S2S", base.CallContext.EffectiveCallerSid);
				throw new ServiceAccessDeniedException();
			}
			IADOrgPerson iadorgPerson = recipientIdentity.Recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				ExTraceGlobals.SessionCacheTracer.TraceDebug<string, SecurityIdentifier>(0L, "[CreatemanageFolder:CreateManagedFolders] In {0} access mode, we found that the caller needed delegate access to a mailbox, but searching for the caller by sid returned no records.  Possibly a cross forest trust with no cross-forest contact to speak of.  Caller sid: {1}", (base.CallContext.MailboxAccessType == MailboxAccessType.Normal) ? "normal" : "S2S", base.CallContext.EffectiveCallerSid);
				throw new ServiceAccessDeniedException();
			}
			OptInFolders optInFolders = new OptInFolders(fromCache, iadorgPerson.LegacyExchangeDN, base.CallContext.EffectiveCaller.ClientSecurityContext, base.CallerBudget);
			this.idAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.Root, text);
			this.ids = optInFolders.CreateOrganizationalFolders(this.folderNames);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0006246C File Offset: 0x0006066C
		internal override ServiceResult<BaseFolderType> Execute()
		{
			ServiceResult<BaseFolderType> result;
			using (Folder folder = Folder.Bind(this.idAndSession.Session, this.ids[base.CurrentStep]))
			{
				BaseFolderType baseFolderType = new FolderType();
				base.LoadServiceObject(baseFolderType, folder, this.idAndSession, ServiceCommandBase.DefaultFolderResponseShape);
				result = new ServiceResult<BaseFolderType>(baseFolderType);
			}
			return result;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000624D4 File Offset: 0x000606D4
		private static string ExtractEmailAddress(XmlElement mailboxElement)
		{
			string result = null;
			XmlNodeList xmlNodeList = mailboxElement.SelectNodes("t:EmailAddress[position() = 1]", ServiceXml.NamespaceManager);
			if (xmlNodeList.Count > 0)
			{
				XmlElement textNodeParent = (XmlElement)xmlNodeList[0];
				result = ServiceXml.GetXmlTextNodeValue(textNodeParent);
			}
			return result;
		}

		// Token: 0x04000D5C RID: 3420
		private string[] folderNames;

		// Token: 0x04000D5D RID: 3421
		private XmlElement mailbox;

		// Token: 0x04000D5E RID: 3422
		private VersionedId[] ids;

		// Token: 0x04000D5F RID: 3423
		private IdAndSession idAndSession;
	}
}
