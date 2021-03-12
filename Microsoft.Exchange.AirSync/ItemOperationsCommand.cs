using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000D0 RID: 208
	internal sealed class ItemOperationsCommand : Command
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003F5D9 File Offset: 0x0003D7D9
		internal ItemOperationsCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfItemOperations;
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0003F5F7 File Offset: 0x0003D7F7
		internal override int MinVersion
		{
			get
			{
				return 120;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0003F5FB File Offset: 0x0003D7FB
		internal override bool RightsManagementSupportFlag
		{
			get
			{
				return this.currentProvider != null && this.currentProvider.RightsManagementSupport;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0003F612 File Offset: 0x0003D812
		protected override string RootNodeName
		{
			get
			{
				return "ItemOperations";
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0003F619 File Offset: 0x0003D819
		private IItemOperationsProvider ItemFetchProvider
		{
			get
			{
				if (this.itemFetchProvider == null)
				{
					this.itemFetchProvider = new MailboxItemFetchProvider(base.SyncStateStorage, base.Version, base.MailboxSession);
				}
				return this.itemFetchProvider;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003F646 File Offset: 0x0003D846
		private IItemOperationsProvider DocumentFetchProvider
		{
			get
			{
				if (this.documentFetchProvider == null)
				{
					this.documentFetchProvider = new DocumentLibraryFetchProvider(base.User);
				}
				return this.documentFetchProvider;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0003F668 File Offset: 0x0003D868
		private IItemOperationsProvider AttachmentFetchProvider
		{
			get
			{
				if (this.attachmentFetchProvider == null)
				{
					PolicyData policyData = ADNotificationManager.GetPolicyData(base.User);
					this.attachmentFetchProvider = new MailboxAttachmentFetchProvider(base.MailboxSession, base.SyncStateStorage, base.ProtocolLogger, (policyData != null) ? policyData.MaxAttachmentSize : Unlimited<ByteQuantifiedSize>.UnlimitedValue, policyData == null || policyData.AttachmentsEnabled);
				}
				return this.attachmentFetchProvider;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
		private IItemOperationsProvider EntityAttachmentFetchProvider
		{
			get
			{
				if (this.entityAttachmentFetchProvider == null)
				{
					PolicyData policyData = ADNotificationManager.GetPolicyData(base.User);
					this.entityAttachmentFetchProvider = new EntityAttachmentFetchProvider(base.MailboxSession, base.SyncStateStorage, base.ProtocolLogger, (policyData != null) ? policyData.MaxAttachmentSize : Unlimited<ByteQuantifiedSize>.UnlimitedValue, policyData == null || policyData.AttachmentsEnabled);
				}
				return this.entityAttachmentFetchProvider;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0003F728 File Offset: 0x0003D928
		private IItemOperationsProvider EmptyFolderContentsProvider
		{
			get
			{
				if (this.emptyFolderContentsProvider == null)
				{
					this.emptyFolderContentsProvider = new EmptyFolderContentsProvider(base.SyncStateStorage, base.MailboxSession);
				}
				return this.emptyFolderContentsProvider;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0003F74F File Offset: 0x0003D94F
		private IItemOperationsProvider MoveProvider
		{
			get
			{
				if (this.moveProvider == null)
				{
					this.moveProvider = new MoveProvider(base.SyncStateStorage, base.MailboxSession);
				}
				return this.moveProvider;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003F778 File Offset: 0x0003D978
		internal override Command.ExecutionState ExecuteCommand()
		{
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ItemOperationsCommand received. Processing the command...");
			try
			{
				XmlDocument xmlDocument = null;
				XmlNode xmlRequest = base.XmlRequest;
				if (xmlRequest.ChildNodes.Count > GlobalSettings.MaxRetrievedItems)
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "CapReached");
					string message = string.Format("ItemOperations cap is reached, Cap = {0}, ChildNode = {1}", GlobalSettings.MaxRetrievedItems, xmlRequest.ChildNodes.Count);
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, message);
					xmlDocument = this.BuildXmlResponse(11.ToString(CultureInfo.InvariantCulture));
					base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, 11);
				}
				else
				{
					xmlDocument = this.BuildXmlResponse(1.ToString(CultureInfo.InvariantCulture));
					base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, 1);
					int i = 0;
					while (i < xmlRequest.ChildNodes.Count)
					{
						XmlNode xmlNode = xmlRequest.ChildNodes[i];
						string localName;
						if ((localName = xmlNode.LocalName) == null)
						{
							goto IL_278;
						}
						if (localName == "EmptyFolderContents")
						{
							this.currentProvider = this.EmptyFolderContentsProvider;
							base.ProtocolLogger.IncrementValue(ProtocolLoggerData.IOEmptyFolderContents);
							goto IL_27E;
						}
						if (!(localName == "Fetch"))
						{
							if (!(localName == "Move"))
							{
								goto IL_278;
							}
							this.currentProvider = this.MoveProvider;
							base.ProtocolLogger.IncrementValue(ProtocolLoggerData.IOMoves);
							goto IL_27E;
						}
						else
						{
							XmlNode xmlNode2 = xmlNode["Store", "ItemOperations:"];
							string a;
							if ((a = xmlNode2.InnerText.ToLower(CultureInfo.InvariantCulture)) != null)
							{
								if (a == "documentlibrary")
								{
									this.currentProvider = this.DocumentFetchProvider;
									base.ProtocolLogger.IncrementValue(ProtocolLoggerData.IOFetchDocs);
									goto IL_27E;
								}
								if (a == "mailbox")
								{
									XmlNode xmlNode3 = xmlNode["FileReference", "AirSyncBase:"];
									if (xmlNode3 == null)
									{
										this.currentProvider = this.ItemFetchProvider;
										base.ProtocolLogger.IncrementValue(ProtocolLoggerData.IOFetchItems);
										goto IL_27E;
									}
									if (base.Version >= 160 && xmlNode3.InnerText != null && xmlNode3.InnerText.IndexOf("%3a") > 16)
									{
										this.currentProvider = this.EntityAttachmentFetchProvider;
										base.ProtocolLogger.IncrementValue(ProtocolLoggerData.IOFetchEntAtts);
										goto IL_27E;
									}
									this.currentProvider = this.AttachmentFetchProvider;
									base.ProtocolLogger.IncrementValue(ProtocolLoggerData.IOFetchAtts);
									goto IL_27E;
								}
							}
							this.AppendFetchErrorXml(xmlDocument, 9.ToString(CultureInfo.InvariantCulture));
							base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, 9);
						}
						IL_3C0:
						i++;
						continue;
						Block_15:
						try
						{
							IL_27E:
							this.currentProvider.ParseRequest(xmlNode);
							this.currentProvider.Execute();
							if (base.Request.AcceptMultiPartResponse && this.currentProvider is IMultipartResponse)
							{
								IMultipartResponse multipartResponse = this.currentProvider as IMultipartResponse;
								multipartResponse.BuildResponse(this.responseNode, this.multipartStreams.Count + 1);
								Stream responseStream = multipartResponse.GetResponseStream();
								if (responseStream != null)
								{
									this.multipartStreams.Add(responseStream);
								}
							}
							else
							{
								this.currentProvider.BuildResponse(this.responseNode);
							}
							this.currentProvider.Reset();
						}
						catch (AirSyncPermanentException ex)
						{
							base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, (int)ex.AirSyncStatusCode);
							if (ex.HttpStatusCode != HttpStatusCode.OK)
							{
								throw;
							}
							base.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.Error, ex.ErrorStringForProtocolLogger);
							AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
							AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.ProtocolTracer, this, "AirSyncPermanentException was thrown. Location Execute.\r\n{0}", arg);
							if (base.MailboxLogger != null)
							{
								base.MailboxLogger.SetData(MailboxLogDataName.ItemOperationsCommand_Execute_Fetch_Exception, ex);
							}
							this.currentProvider.BuildErrorResponse(ex.AirSyncStatusCodeInInt.ToString(CultureInfo.InvariantCulture), this.responseNode, base.ProtocolLogger);
							this.currentProvider.Reset();
							base.PartialFailure = true;
						}
						goto IL_3C0;
						IL_278:
						AirSyncDiagnostics.Assert(false);
						goto Block_15;
					}
				}
				if (base.Request.AcceptMultiPartResponse)
				{
					this.BuildMultiPartResponse(xmlDocument);
				}
				else
				{
					base.XmlResponse = xmlDocument;
				}
			}
			finally
			{
				this.DisposeResources();
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
		protected override bool HandleQuarantinedState()
		{
			base.XmlResponse = this.BuildXmlResponse(3.ToString(CultureInfo.InvariantCulture));
			base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, 3);
			return false;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
		internal override XmlDocument GetValidationErrorXml()
		{
			if (ItemOperationsCommand.validationErrorXml == null)
			{
				XmlDocument commandXmlStub = base.GetCommandXmlStub();
				XmlElement xmlElement = commandXmlStub.CreateElement("Status", this.RootNodeNamespace);
				xmlElement.InnerText = XmlConvert.ToString(2);
				commandXmlStub[this.RootNodeName].AppendChild(xmlElement);
				base.ProtocolLogger.SetValue(ProtocolLoggerData.StatusCode, 2);
				ItemOperationsCommand.validationErrorXml = commandXmlStub;
			}
			return ItemOperationsCommand.validationErrorXml;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003FC58 File Offset: 0x0003DE58
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.DisposeResources();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003FC88 File Offset: 0x0003DE88
		private void AppendFetchErrorXml(XmlDocument responseDocument, string statusCode)
		{
			XmlNode xmlNode = responseDocument.CreateElement("Fetch", "ItemOperations:");
			XmlNode xmlNode2 = responseDocument.CreateElement("Status", "ItemOperations:");
			xmlNode2.InnerText = statusCode;
			xmlNode.AppendChild(xmlNode2);
			this.responseNode.AppendChild(xmlNode);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003FCD4 File Offset: 0x0003DED4
		private XmlDocument BuildXmlResponse(string statusCode)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("ItemOperations", "ItemOperations:");
			xmlDocument.AppendChild(xmlNode);
			XmlNode xmlNode2 = xmlDocument.CreateElement("Status", "ItemOperations:");
			xmlNode2.InnerText = statusCode;
			xmlNode.AppendChild(xmlNode2);
			this.responseNode = xmlDocument.CreateElement("Response", "ItemOperations:");
			if (statusCode == 1.ToString(CultureInfo.InvariantCulture))
			{
				xmlNode.AppendChild(this.responseNode);
			}
			return xmlDocument;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003FD5C File Offset: 0x0003DF5C
		private void BuildMultiPartResponse(XmlDocument xmlResponse)
		{
			StringBuilder stringBuilder = null;
			if (base.MailboxLoggingEnabled && base.MailboxLogger != null)
			{
				stringBuilder = new StringBuilder();
				stringBuilder.Append("----------------------- Multipart Response ---------------------\r\n");
			}
			using (BinaryWriter binaryWriter = new BinaryWriter(base.OutputStream))
			{
				AirSyncStream airSyncStream = new AirSyncStream();
				base.Context.Response.BuildMultiPartWbXmlResponse(xmlResponse, airSyncStream);
				this.multipartStreams.Insert(0, airSyncStream);
				binaryWriter.Write(this.multipartStreams.Count);
				if (stringBuilder != null)
				{
					stringBuilder.AppendFormat("Number of Parts: {0}\r\n", this.multipartStreams.Count);
				}
				int num = 4 + this.multipartStreams.Count * 8;
				for (int i = 0; i < this.multipartStreams.Count; i++)
				{
					long length = this.multipartStreams[i].Length;
					binaryWriter.Write(num);
					binaryWriter.Write((int)length);
					if (stringBuilder != null)
					{
						stringBuilder.AppendFormat("Part {0}: offset {1}, size {2}\r\n", i, num, length);
					}
					num += (int)length;
				}
				for (int j = 0; j < this.multipartStreams.Count; j++)
				{
					StreamHelper.CopyStream(this.multipartStreams[j], base.OutputStream, (int)this.multipartStreams[j].Length);
					if (stringBuilder != null && j == 0)
					{
						stringBuilder.AppendLine();
						this.multipartStreams[j].Seek(0L, SeekOrigin.Begin);
						using (WbxmlReader wbxmlReader = new WbxmlReader(this.multipartStreams[j]))
						{
							stringBuilder.Append(AirSyncUtility.BuildOuterXml(wbxmlReader.ReadXmlDocument(), !GlobalSettings.EnableMailboxLoggingVerboseMode));
						}
						stringBuilder.AppendLine();
					}
				}
				if (stringBuilder != null)
				{
					base.MailboxLogger.SetData(MailboxLogDataName.ResponseBody, stringBuilder.ToString());
				}
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003FF70 File Offset: 0x0003E170
		private void DisposeResources()
		{
			if (this.itemFetchProvider != null)
			{
				this.itemFetchProvider.Dispose();
				this.itemFetchProvider = null;
			}
			if (this.documentFetchProvider != null)
			{
				this.documentFetchProvider.Dispose();
				this.documentFetchProvider = null;
			}
			if (this.attachmentFetchProvider != null)
			{
				this.attachmentFetchProvider.Dispose();
				this.attachmentFetchProvider = null;
			}
			if (this.entityAttachmentFetchProvider != null)
			{
				this.entityAttachmentFetchProvider.Dispose();
				this.entityAttachmentFetchProvider = null;
			}
			foreach (Stream stream in this.multipartStreams)
			{
				stream.Dispose();
			}
			this.multipartStreams.Clear();
		}

		// Token: 0x04000748 RID: 1864
		private static XmlDocument validationErrorXml;

		// Token: 0x04000749 RID: 1865
		private XmlNode responseNode;

		// Token: 0x0400074A RID: 1866
		private DocumentLibraryFetchProvider documentFetchProvider;

		// Token: 0x0400074B RID: 1867
		private MailboxItemFetchProvider itemFetchProvider;

		// Token: 0x0400074C RID: 1868
		private MailboxAttachmentFetchProvider attachmentFetchProvider;

		// Token: 0x0400074D RID: 1869
		private EntityAttachmentFetchProvider entityAttachmentFetchProvider;

		// Token: 0x0400074E RID: 1870
		private EmptyFolderContentsProvider emptyFolderContentsProvider;

		// Token: 0x0400074F RID: 1871
		private MoveProvider moveProvider;

		// Token: 0x04000750 RID: 1872
		private IItemOperationsProvider currentProvider;

		// Token: 0x04000751 RID: 1873
		private List<Stream> multipartStreams = new List<Stream>();
	}
}
