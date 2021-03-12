using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000241 RID: 577
	internal sealed class SearchCommand : Command
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x0007C118 File Offset: 0x0007A318
		internal SearchCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfSearches;
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0007C12B File Offset: 0x0007A32B
		protected override string RootNodeName
		{
			get
			{
				return "Search";
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0007C132 File Offset: 0x0007A332
		protected override bool IsInteractiveCommand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0007C135 File Offset: 0x0007A335
		internal override bool RightsManagementSupportFlag
		{
			get
			{
				return this.provider != null && this.provider.RightsManagementSupport;
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0007C14C File Offset: 0x0007A34C
		internal override Command.ExecutionState ExecuteCommand()
		{
			try
			{
				this.ParseXmlRequest();
				this.provider = this.CreateProvider(this.reqStoreName);
				if (this.provider == null)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "Unknown search provider specified!");
					base.XmlResponse = this.GetValidationErrorXml();
					return Command.ExecutionState.Complete;
				}
				XmlDocument xmlResponse = this.BuildXmlResponse();
				this.provider.ParseOptions(this.reqOptionsNode);
				this.provider.ParseQueryNode(this.reqQueryNode);
				MailboxSearchProvider mailboxSearchProvider = this.provider as MailboxSearchProvider;
				if (mailboxSearchProvider != null)
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.TotalFolders, mailboxSearchProvider.Folders);
					if (mailboxSearchProvider.DeepTraversal)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.SearchDeep, 1);
					}
				}
				this.provider.Execute();
				this.provider.BuildResponse(this.respStoreNode);
				base.ProtocolLogger.IncrementValueBy("S", PerFolderProtocolLoggerData.ServerAdds, this.provider.NumberResponses);
				base.XmlResponse = xmlResponse;
			}
			catch (AirSyncPermanentException ex)
			{
				if (ex.HttpStatusCode != HttpStatusCode.OK)
				{
					throw;
				}
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.ProtocolTracer, this, "AirSyncPermanentException was thrown. Location Execute.\r\n{0}", arg);
				base.ProtocolLogger.IncrementValue(ProtocolLoggerData.NumErrors);
				base.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, ex.ErrorStringForProtocolLogger);
				if (base.MailboxLogger != null)
				{
					base.MailboxLogger.SetData(MailboxLogDataName.SearchCommand_Execute_Exception, ex);
				}
				base.XmlResponse = this.GetProviderErrorXml(ex.AirSyncStatusCodeInInt);
				base.PartialFailure = true;
			}
			finally
			{
				if (this.provider is IDisposable)
				{
					((IDisposable)this.provider).Dispose();
				}
				this.provider = null;
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0007C318 File Offset: 0x0007A518
		protected override bool HandleQuarantinedState()
		{
			base.XmlResponse = this.GetProviderErrorXml(3);
			return false;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0007C328 File Offset: 0x0007A528
		internal override XmlDocument GetValidationErrorXml()
		{
			if (SearchCommand.validationErrorXml == null)
			{
				SearchCommand.validationErrorXml = this.GetProviderErrorXml(2);
			}
			return SearchCommand.validationErrorXml;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0007C344 File Offset: 0x0007A544
		private ISearchProvider CreateProvider(string provider)
		{
			if (provider != null)
			{
				if (provider == "mailbox")
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.SearchProvider, "Mbx");
					return new MailboxSearchProvider(base.MailboxSession, base.SyncStateStorage, base.Context);
				}
				if (provider == "gal")
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.SearchProvider, "Gal");
					return new GalSearchProvider(base.User, base.Request.Culture.LCID, base.Version);
				}
				if (provider == "documentlibrary")
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.SearchProvider, "Doc");
					return new DocumentLibrarySearchProvider(base.User);
				}
			}
			return null;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0007C404 File Offset: 0x0007A604
		private void ParseXmlRequest()
		{
			XmlNode xmlRequest = base.XmlRequest;
			XmlNode firstChild = xmlRequest.FirstChild;
			XmlNode xmlNode = firstChild["Name", "Search:"];
			this.reqStoreName = xmlNode.InnerText.ToLower(CultureInfo.InvariantCulture);
			this.reqQueryNode = firstChild["Query", "Search:"];
			this.reqOptionsNode = firstChild["Options", "Search:"];
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0007C474 File Offset: 0x0007A674
		private XmlDocument BuildXmlResponse()
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("Search", "Search:");
			xmlDocument.AppendChild(xmlNode);
			XmlNode xmlNode2 = xmlDocument.CreateElement("Status", "Search:");
			xmlNode2.InnerText = 1.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = xmlDocument.CreateElement("Response", "Search:");
			xmlNode.AppendChild(xmlNode3);
			this.respStoreNode = xmlDocument.CreateElement("Store", "Search:");
			xmlNode3.AppendChild(this.respStoreNode);
			return xmlDocument;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0007C50C File Offset: 0x0007A70C
		private XmlDocument GetProviderErrorXml(int statusCode)
		{
			XmlDocument xmlDocument = this.BuildXmlResponse();
			XmlNode xmlNode = xmlDocument["Search", "Search:"];
			XmlNode xmlNode2 = xmlNode["Response", "Search:"];
			XmlNode xmlNode3 = xmlNode2["Store", "Search:"];
			XmlNode xmlNode4 = xmlNode3.OwnerDocument.CreateElement("Status", "Search:");
			xmlNode3.AppendChild(xmlNode4);
			xmlNode4.InnerText = statusCode.ToString(CultureInfo.InvariantCulture);
			return xmlDocument;
		}

		// Token: 0x04000C89 RID: 3209
		private static XmlDocument validationErrorXml;

		// Token: 0x04000C8A RID: 3210
		private string reqStoreName;

		// Token: 0x04000C8B RID: 3211
		private XmlElement respStoreNode;

		// Token: 0x04000C8C RID: 3212
		private XmlElement reqQueryNode;

		// Token: 0x04000C8D RID: 3213
		private XmlElement reqOptionsNode;

		// Token: 0x04000C8E RID: 3214
		private ISearchProvider provider;

		// Token: 0x02000242 RID: 578
		private struct SearchProviders
		{
			// Token: 0x04000C8F RID: 3215
			public const string Mailbox = "mailbox";

			// Token: 0x04000C90 RID: 3216
			public const string Gal = "gal";

			// Token: 0x04000C91 RID: 3217
			public const string DocumentLibrary = "documentlibrary";
		}
	}
}
