using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E01 RID: 3585
	internal class ResponseMessageWriter
	{
		// Token: 0x06005CD0 RID: 23760 RVA: 0x001217DB File Offset: 0x0011F9DB
		public ResponseMessageWriter(HttpContext httpContext, ServiceModel serviceModel)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			ArgumentValidator.ThrowIfNull("serviceModel", serviceModel);
			this.HttpContext = httpContext;
			this.ServiceModel = serviceModel;
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x00121807 File Offset: 0x0011FA07
		// (set) Token: 0x06005CD2 RID: 23762 RVA: 0x0012180F File Offset: 0x0011FA0F
		public HttpContext HttpContext { get; private set; }

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x00121818 File Offset: 0x0011FA18
		// (set) Token: 0x06005CD4 RID: 23764 RVA: 0x00121820 File Offset: 0x0011FA20
		public ServiceModel ServiceModel { get; private set; }

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06005CD5 RID: 23765 RVA: 0x00121829 File Offset: 0x0011FA29
		public Uri ServiceRootUri
		{
			get
			{
				return this.HttpContext.GetServiceRootUri();
			}
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00121838 File Offset: 0x0011FA38
		public void WriteDataResult(ODataContext odataContext, object result)
		{
			ArgumentValidator.ThrowIfNull("odataContext", odataContext);
			ArgumentValidator.ThrowIfNull("result", result);
			using (ODataMessageWriter odataMessageWriter = this.CreateODataMessageWriter(odataContext, false))
			{
				ResponseMessageWriter.DataResultWriter dataResultWriter = new ResponseMessageWriter.DataResultWriter(odataContext);
				try
				{
					dataResultWriter.WriteData(odataMessageWriter, odataContext, result);
				}
				catch (ODataContentTypeException innerException)
				{
					throw new InvalidContentTypeException(innerException);
				}
			}
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00121938 File Offset: 0x0011FB38
		public void WriteError(Exception exception)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			AggregateException ex = exception as AggregateException;
			if (ex != null && ex.InnerException != null)
			{
				exception = ex.InnerException;
			}
			ErrorResponse errorResponse = ErrorManager.GetErrorResponse(exception);
			ODataError odataError = new ODataError
			{
				ErrorCode = errorResponse.ErrorDetails.ErrorCode,
				Message = errorResponse.ErrorDetails.ErrorMessage,
				InnerError = new ODataInnerError(exception)
			};
			this.HandleHttpResponseException(delegate
			{
				this.HttpContext.Response.StatusCode = (int)errorResponse.HttpStatusCode;
				ODataResponseException ex2 = exception as ODataResponseException;
				if (ex2 != null)
				{
					ex2.AppendResponseHeader(this.HttpContext);
				}
				using (ODataMessageWriter odataMessageWriter = this.CreateODataMessageWriter(null, true))
				{
					odataMessageWriter.WriteError(odataError, Global.ODataStackTraceInErrorResponse);
				}
			});
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x001219FC File Offset: 0x0011FBFC
		private void HandleHttpResponseException(Action action)
		{
			try
			{
				action();
			}
			catch (WebException innerException)
			{
				throw new HttpResponseTransportException(innerException);
			}
			catch (IOException innerException2)
			{
				throw new HttpResponseTransportException(innerException2);
			}
			catch (SocketException innerException3)
			{
				throw new HttpResponseTransportException(innerException3);
			}
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00121A54 File Offset: 0x0011FC54
		public ODataMessageWriter CreateODataMessageWriter(ODataContext odataContext = null, bool ignoreRequestedFormat = false)
		{
			ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings();
			odataMessageWriterSettings.ODataUri.ServiceRoot = this.ServiceRootUri;
			odataMessageWriterSettings.PayloadBaseUri = this.ServiceRootUri;
			if (odataContext != null)
			{
				OperationSegment operationSegment = odataContext.ODataPath.EntitySegment as OperationSegment;
				if (operationSegment != null)
				{
					UriBuilder uriBuilder = new UriBuilder(odataContext.RequestUri);
					uriBuilder.Path = uriBuilder.Path.Replace("/" + ExtensionMethods.FullName(operationSegment.Operations.FirstOrDefault<IEdmOperation>()) + "/", "/");
					ODataUriParser odataUriParser = new ODataUriParser(odataContext.ServiceModel.EdmModel, odataContext.HttpContext.GetServiceRootUri(), uriBuilder.Uri)
					{
						Resolver = new StringAsEnumResolver
						{
							EnableCaseInsensitive = true
						}
					};
					odataMessageWriterSettings.ODataUri.Path = odataUriParser.ParsePath();
				}
				else
				{
					odataMessageWriterSettings.ODataUri.Path = odataContext.ODataPath.ODataPath;
				}
			}
			if (!ignoreRequestedFormat)
			{
				if (odataContext != null)
				{
					string text = odataContext.QueryString["$format"];
					if (text != null)
					{
						odataMessageWriterSettings.SetContentType(text, Encoding.UTF8.WebName);
					}
				}
				else if (this.HttpContext.Request.AcceptTypes != null)
				{
					odataMessageWriterSettings.SetContentType(this.HttpContext.Request.Headers["Accept"], Encoding.UTF8.WebName);
				}
			}
			odataMessageWriterSettings.AutoComputePayloadMetadataInJson = true;
			ResponseMessage responseMessage = new ResponseMessage(this.HttpContext);
			return new ODataMessageWriter(responseMessage, odataMessageWriterSettings, this.ServiceModel.EdmModel);
		}

		// Token: 0x02000E02 RID: 3586
		private class DataResultWriter
		{
			// Token: 0x06005CDA RID: 23770 RVA: 0x00121BE0 File Offset: 0x0011FDE0
			public DataResultWriter(ODataContext odataContext)
			{
				ArgumentValidator.ThrowIfNull("odataContext", odataContext);
				this.ODataContext = odataContext;
			}

			// Token: 0x170014FE RID: 5374
			// (get) Token: 0x06005CDB RID: 23771 RVA: 0x00121BFA File Offset: 0x0011FDFA
			// (set) Token: 0x06005CDC RID: 23772 RVA: 0x00121C02 File Offset: 0x0011FE02
			private ODataContext ODataContext { get; set; }

			// Token: 0x170014FF RID: 5375
			// (get) Token: 0x06005CDD RID: 23773 RVA: 0x00121C0B File Offset: 0x0011FE0B
			// (set) Token: 0x06005CDE RID: 23774 RVA: 0x00121C13 File Offset: 0x0011FE13
			private ODataWriter Writer { get; set; }

			// Token: 0x06005CDF RID: 23775 RVA: 0x00121C1C File Offset: 0x0011FE1C
			public void WriteData(ODataMessageWriter messageWriter, ODataContext odataContext, object result)
			{
				ArgumentValidator.ThrowIfNull("messageWriter", messageWriter);
				ArgumentValidator.ThrowIfNull("odataContext", odataContext);
				ArgumentValidator.ThrowIfNull("result", result);
				ODataPathSegment lastSegment = odataContext.ODataPath.LastSegment;
				if (lastSegment is CountSegment)
				{
					messageWriter.WriteValue(((IFindEntitiesResult)result).TotalCount);
					return;
				}
				if (result is IFindEntitiesResult && (lastSegment is EntitySetSegment || lastSegment is NavigationPropertySegment))
				{
					this.Writer = messageWriter.CreateODataFeedWriter((IEdmEntitySetBase)this.ODataContext.NavigationSource, this.ODataContext.EntityType);
					this.WriteFeed(result as IFindEntitiesResult, this.ODataContext.RequestUri, odataContext.QueryString.GetCountQueryString());
					return;
				}
				this.Writer = messageWriter.CreateODataEntryWriter(this.ODataContext.NavigationSource, this.ODataContext.EntityType);
				this.WriteEntry(result as Entity);
			}

			// Token: 0x06005CE0 RID: 23776 RVA: 0x00121D08 File Offset: 0x0011FF08
			private void WriteEntry(Entity element)
			{
				Uri webUri = element.GetWebUri(this.ODataContext);
				ODataEntry odataEntry = ODataObjectModelConverter.ConvertToODataEntry(element, webUri);
				this.Writer.WriteStart(odataEntry);
				this.WriteNavigationLinks(element, webUri);
				this.Writer.WriteEnd();
			}

			// Token: 0x06005CE1 RID: 23777 RVA: 0x00121D4C File Offset: 0x0011FF4C
			private void WriteFeed(IFindEntitiesResult entries, Uri currentUrl, bool writeCount = false)
			{
				ODataFeed odataFeed = new ODataFeed();
				if (writeCount)
				{
					odataFeed.Count = new long?((long)entries.TotalCount);
				}
				this.Writer.WriteStart(odataFeed);
				if (entries != null)
				{
					int num = 0;
					foreach (object obj in entries)
					{
						Entity element = (Entity)obj;
						this.WriteEntry(element);
						num++;
					}
					if (!(entries is IFindEntitiesResult<Attachment>) && currentUrl != null)
					{
						Uri nextPageLink = this.GetNextPageLink(currentUrl, num);
						if (nextPageLink != null)
						{
							odataFeed.NextPageLink = nextPageLink;
						}
					}
				}
				this.Writer.WriteEnd();
			}

			// Token: 0x06005CE2 RID: 23778 RVA: 0x00121E0C File Offset: 0x0012000C
			private void WriteNavigationLinks(Entity element, Uri parentEntryUri)
			{
				EdmEntityType edmEntityType = element.Schema.EdmEntityType;
				foreach (IEdmNavigationProperty edmNavigationProperty in ExtensionMethods.NavigationProperties(edmEntityType))
				{
					bool flag = EdmTypeSemantics.IsCollection(edmNavigationProperty.Type);
					PropertyDefinition propertyDefinition = element.Schema.ResolveProperty(edmNavigationProperty.Name);
					ODataNavigationLink odataNavigationLink = new ODataNavigationLink
					{
						IsCollection = new bool?(flag),
						Name = edmNavigationProperty.Name
					};
					this.Writer.WriteStart(odataNavigationLink);
					if (element.PropertyBag.Contains(propertyDefinition))
					{
						object obj = element[propertyDefinition];
						if (obj != null)
						{
							if (flag)
							{
								Uri currentUrl = null;
								if ((propertyDefinition == null || !propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.ChildOnlyEntity)) && parentEntryUri != null)
								{
									currentUrl = new Uri(string.Format("{0}/{1}", parentEntryUri, edmNavigationProperty.Name));
								}
								this.WriteFeed(obj as IFindEntitiesResult, currentUrl, false);
							}
							else
							{
								this.WriteEntry(obj as Entity);
							}
						}
					}
					this.Writer.WriteEnd();
				}
			}

			// Token: 0x06005CE3 RID: 23779 RVA: 0x00121F44 File Offset: 0x00120144
			private Uri GetNextPageLink(Uri currentUri, int resultSize)
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(currentUri.Query);
				int value;
				int pageSize;
				if (int.TryParse(nameValueCollection["$top"], out value))
				{
					pageSize = QueryAdapter.GetPageSize(new int?(value));
				}
				else
				{
					pageSize = QueryAdapter.GetPageSize(null);
				}
				if (pageSize > resultSize)
				{
					return null;
				}
				if (string.IsNullOrEmpty(nameValueCollection["$skip"]))
				{
					nameValueCollection.Add("$skip", resultSize.ToString());
				}
				else
				{
					int num = 0;
					int.TryParse(nameValueCollection["$skip"], out num);
					nameValueCollection.Set("$skip", (num + resultSize).ToString());
				}
				return new UriBuilder
				{
					Host = currentUri.Host,
					Scheme = currentUri.Scheme,
					Port = currentUri.Port,
					Path = currentUri.AbsolutePath,
					Query = nameValueCollection.ToString()
				}.Uri;
			}
		}
	}
}
