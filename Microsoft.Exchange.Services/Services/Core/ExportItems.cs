using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002E5 RID: 741
	internal class ExportItems : MultiStepServiceCommand<ExportItemsRequest, ExportItemsResponseMessage>, IDisposable
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x00067446 File Offset: 0x00065646
		internal override bool IsDelayExecuted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0006744C File Offset: 0x0006564C
		public ExportItems(CallContext callContext, ExportItemsRequest request) : base(callContext, request)
		{
			if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ExchangeVersion>(0L, "[ExportItems::ExportItems] ExportItems is only supported on or after E14SP1. The request was from {0}. Failing request.", ExchangeVersion.Current);
				throw new InvalidRequestException();
			}
			ServiceCommandBase.ThrowIfNull(request.Ids, "itemIds", "ExportItems::ExportItems");
			this.itemIds = request.Ids.Nodes;
			ServiceCommandBase.ThrowIfNullOrEmpty<XmlNode>(this.itemIds, "itemIds", "ExportItems::ExportItems");
			this.exportItemsResponse = new ExportItemsResponse();
			try
			{
				this.isFlighted = DiscoveryFlighting.IsDocIdHintFlighted(callContext);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, string>(0L, "ExportItems::ExportItems encountered exception - Class: {0}, Message: {1}.", ex.GetType().FullName, ex.Message);
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00067528 File Offset: 0x00065728
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00067538 File Offset: 0x00065738
		internal static void WriteItemFXStream(XmlWriter writer, object item, object buffer, params object[] parameters)
		{
			if (EWSSettings.WritingToWire)
			{
				EwsFxProxy ewsFxProxy = null;
				ewsFxProxy = new EwsFxProxy(writer);
				Item item2 = item as Item;
				try
				{
					try
					{
						if (item2 == null)
						{
							ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExportItems::WriteItemFXStream -- xsoItem is null");
						}
						item2.MapiMessage.ExportObject(ewsFxProxy, null);
					}
					catch (Exception ex)
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, string>(0L, "ExportItems::WriteItemFXStream encountered exception - Class: {0}, Message: {1}.", ex.GetType().FullName, ex.Message);
					}
					return;
				}
				finally
				{
					if (ewsFxProxy != null)
					{
						ewsFxProxy.Dispose();
					}
					if (item2 != null)
					{
						item2.Dispose();
					}
				}
			}
			writer.WriteRaw(ExportItems.ZeroFileItem);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000675E4 File Offset: 0x000657E4
		internal bool IsPreviousResultStopBatchProcessingError(int currentIndex)
		{
			if (!this.isPreviousResultStopBatchProcessingError && currentIndex > 0)
			{
				ArrayOfResponseMessages responseMessages = this.exportItemsResponse.ResponseMessages;
				ResponseMessage responseMessage = (ResponseMessage)responseMessages.Items.GetValue(currentIndex - 1);
				this.isPreviousResultStopBatchProcessingError = responseMessage.StopsBatchProcessing;
			}
			return this.isPreviousResultStopBatchProcessingError;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0006762F File Offset: 0x0006582F
		internal override ServiceResult<ExportItemsResponseMessage> Execute()
		{
			return new ServiceResult<ExportItemsResponseMessage>(new ExportItemsResponseMessage(base.CurrentStep, this));
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00067642 File Offset: 0x00065842
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.exportItemsResponse.BuildForExportItemsResults(base.Results);
			return this.exportItemsResponse;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0006765B File Offset: 0x0006585B
		internal override int StepCount
		{
			get
			{
				return this.itemIds.Count;
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00067668 File Offset: 0x00065868
		internal ServiceResult<XmlNode> ExportItemsResult(int itemIndex)
		{
			return ExceptionHandler<XmlNode>.Execute(new ExceptionHandler<XmlNode>.CreateServiceResult(this.ExportItemsFromRequest), itemIndex);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0006767C File Offset: 0x0006587C
		private ServiceResult<XmlNode> ExportItemsFromRequest(int itemIndex)
		{
			XmlElement xmlElement = (XmlElement)this.itemIds[itemIndex];
			ServiceCommandBase.ThrowIfNull(xmlElement, "itemIdElement", "ExportItems::ExportItemsFromRequest");
			XmlAttribute xmlAttribute = xmlElement.Attributes["Id"];
			ServiceCommandBase.ThrowIfNull(xmlAttribute, "itemIdAttribute", "ExportItems::ExportItemsFromRequest");
			string value = xmlAttribute.Value;
			int num = value.IndexOf("4887312c-8b40-4fec-a252-f2749065c0e5");
			string text = null;
			if (num > 0)
			{
				xmlAttribute.Value = value.Substring(0, num);
				xmlElement.Attributes.SetNamedItem(xmlAttribute);
				if (value.Length > num + 1 + "4887312c-8b40-4fec-a252-f2749065c0e5".Length)
				{
					text = value.Substring(num + "4887312c-8b40-4fec-a252-f2749065c0e5".Length);
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ExportItems::ExportItemsFromRequest -- found a DocumentId: {0}", text);
				}
			}
			Item item = null;
			bool flag = false;
			ServiceResult<XmlNode> result = null;
			IdAndSession idAndSession = null;
			try
			{
				idAndSession = base.IdConverter.ConvertXmlToIdAndSessionReadOnly(xmlElement, BasicTypes.Item, false, false);
			}
			catch (ObjectNotFoundException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ExportItems::ExportItemsFromRequest -- threw an ObjectNotFoundException: {0}, will try DocumentId lookup.", ex.ToString());
				if (!this.isFlighted || string.IsNullOrEmpty(text))
				{
					throw;
				}
				idAndSession = null;
				int num2 = int.Parse(text);
				if (num2 > 0)
				{
					idAndSession = this.GetIdAndSessionFromDocumentIdXml(xmlElement, num2);
				}
				if (idAndSession == null)
				{
					throw;
				}
			}
			try
			{
				ServiceError serviceError;
				XmlElement xmlElement2 = (XmlElement)this.GetItemXML(idAndSession, out item, out serviceError);
				this.objectsChanged++;
				this.SerializeFXStream(xmlElement2, item);
				if (serviceError == null)
				{
					result = new ServiceResult<XmlNode>(xmlElement2);
				}
				else
				{
					result = new ServiceResult<XmlNode>(xmlElement2, serviceError);
				}
				flag = true;
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ExportItems::ExportItemsFromRequest -- threw an exception: {0}", ex2.ToString());
				throw;
			}
			finally
			{
				if (!flag && item != null)
				{
					item.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00067854 File Offset: 0x00065A54
		private IdAndSession GetIdAndSessionFromDocumentIdXml(XmlElement itemIdElement, int documentId)
		{
			if (itemIdElement == null)
			{
				return null;
			}
			if (itemIdElement.Attributes["Id"] == null)
			{
				return null;
			}
			IdAndSession idAndSession = null;
			try
			{
				idAndSession = base.IdConverter.ConvertXmlToIdAndSessionReadOnly(itemIdElement, BasicTypes.Item, false, true);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ExportItems::GetIdAndSessionFromDocumentId -- threw an Exception trying to get a sessionOnly: {0}, we don't want to return this to the client.", ex.ToString());
				return null;
			}
			if (idAndSession == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExportItems::GetIdAndSessionFromDocumentIdXml -- When querying for ItemId from DocumentId: idAndSessionOnly was null");
				return null;
			}
			StoreSession session = idAndSession.Session;
			Folder folder = null;
			ItemQueryType queryFlags = ItemQueryType.None;
			ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.DocumentId, documentId);
			if (session != null)
			{
				if (session.IsPublicFolderSession)
				{
					StoreId parentFolderId = idAndSession.ParentFolderId;
					if (parentFolderId != null)
					{
						folder = Folder.Bind(session, parentFolderId);
					}
				}
				else
				{
					folder = Folder.Bind(session, DefaultFolderType.Configuration, new PropertyDefinition[]
					{
						FolderSchema.Id
					});
					queryFlags = ItemQueryType.DocumentIdView;
				}
			}
			if (folder == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExportItems::GetIdAndSessionFromDocumentIdXml -- When querying for ItemId from DocumentId: folder was null");
				return null;
			}
			object[][] array = null;
			List<PropertyDefinition> list = new List<PropertyDefinition>(1);
			list.Add(ItemSchema.Id);
			using (folder)
			{
				using (QueryResult queryResult = folder.ItemQuery(queryFlags, queryFilter, null, list))
				{
					array = queryResult.GetRows(1);
				}
			}
			if (array == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExportItems::GetIdAndSessionFromDocumentIdXml -- When querying for ItemId from DocumentId: value was null");
				return null;
			}
			VersionedId versionedId = array[0][0] as VersionedId;
			if (versionedId == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ExportItems::GetIdAndSessionFromDocumentIdXml -- When querying for ItemId from DocumentId: storeId was null");
				return null;
			}
			return new IdAndSession(versionedId, idAndSession.Session);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00067A0C File Offset: 0x00065C0C
		private XmlNode GetItemXML(IdAndSession idAndSession, out Item xsoItem, out ServiceError warning)
		{
			warning = null;
			ItemResponseShape responseShape = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, null);
			ToXmlPropertyList propertyList = XsoDataConverter.GetPropertyList(idAndSession.Id, idAndSession.Session, responseShape);
			xsoItem = idAndSession.GetRootXsoItem(propertyList.GetPropertyDefinitions());
			XmlElement xmlElement = ServiceXml.CreateElement(base.XmlDocument, "Item", "http://schemas.microsoft.com/exchange/services/2006/messages");
			IdConverter.CreateStoreIdXml(xmlElement, idAndSession.Id, idAndSession, "ItemId", "http://schemas.microsoft.com/exchange/services/2006/messages");
			return xmlElement;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00067A78 File Offset: 0x00065C78
		private void SerializeFXStream(XmlElement parentElement, Item xsoItem)
		{
			XmlTextWithStreamingBehavior textNode = new XmlTextWithStreamingBehavior(parentElement.OwnerDocument, xsoItem, this.ByteBuffer, new WriteStreamData(ExportItems.WriteItemFXStream), new object[0]);
			ServiceXml.CreateTextElement(parentElement, "Data", textNode, "http://schemas.microsoft.com/exchange/services/2006/messages");
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00067ABC File Offset: 0x00065CBC
		private void Dispose(bool isDisposing)
		{
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<int, bool, bool>((long)this.GetHashCode(), "[ExportItems:Dispose(bool)] Hashcode: {0}. IsDisposing: {1}, Already Disposed: {2}", this.GetHashCode(), isDisposing, this.isDisposed);
			if (!this.isDisposed)
			{
				if (isDisposing)
				{
					this.exportItemsResponse = null;
					this.ByteBuffer = null;
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x04000DF7 RID: 3575
		public const string DocumentIdMarker = "4887312c-8b40-4fec-a252-f2749065c0e5";

		// Token: 0x04000DF8 RID: 3576
		private static readonly string ZeroFileItem = "AA==";

		// Token: 0x04000DF9 RID: 3577
		private IList<XmlNode> itemIds;

		// Token: 0x04000DFA RID: 3578
		private bool isDisposed;

		// Token: 0x04000DFB RID: 3579
		private bool isFlighted;

		// Token: 0x04000DFC RID: 3580
		internal byte[] ByteBuffer = new byte[32768];

		// Token: 0x04000DFD RID: 3581
		private ExportItemsResponse exportItemsResponse;

		// Token: 0x04000DFE RID: 3582
		private bool isPreviousResultStopBatchProcessingError;
	}
}
