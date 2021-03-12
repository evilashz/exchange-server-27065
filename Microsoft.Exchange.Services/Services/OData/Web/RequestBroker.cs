using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000DFD RID: 3581
	internal class RequestBroker : DisposeTrackableBase
	{
		// Token: 0x06005C9C RID: 23708 RVA: 0x00120AD8 File Offset: 0x0011ECD8
		public RequestBroker(HttpContext httpContext)
		{
			this.HttpContext = httpContext;
			this.RequestDetailsLogger = RequestDetailsLogger.Current;
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06005C9D RID: 23709 RVA: 0x00120AF2 File Offset: 0x0011ECF2
		// (set) Token: 0x06005C9E RID: 23710 RVA: 0x00120AFA File Offset: 0x0011ECFA
		public HttpContext HttpContext { get; private set; }

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06005C9F RID: 23711 RVA: 0x00120B03 File Offset: 0x0011ED03
		// (set) Token: 0x06005CA0 RID: 23712 RVA: 0x00120B0B File Offset: 0x0011ED0B
		private ODataContext ODataContext { get; set; }

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06005CA1 RID: 23713 RVA: 0x00120B14 File Offset: 0x0011ED14
		// (set) Token: 0x06005CA2 RID: 23714 RVA: 0x00120B1C File Offset: 0x0011ED1C
		private RequestDetailsLogger RequestDetailsLogger { get; set; }

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06005CA3 RID: 23715 RVA: 0x00120B25 File Offset: 0x0011ED25
		// (set) Token: 0x06005CA4 RID: 23716 RVA: 0x00120B2D File Offset: 0x0011ED2D
		private ServiceModel ServiceModel { get; set; }

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00120B36 File Offset: 0x0011ED36
		protected override void InternalDispose(bool disposing)
		{
			if (this.ODataContext != null)
			{
				this.ODataContext.Dispose();
			}
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x00120B4B File Offset: 0x0011ED4B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RequestBroker>(this);
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x00120B54 File Offset: 0x0011ED54
		public Task Process()
		{
			Task result;
			try
			{
				this.RequestDetailsLogger.Set(ActivityStandardMetadata.Action, "OData:");
				this.InitializeServiceModel();
				DocumentPublisher documentPublisher = null;
				if (this.IsDocumentRequest(out documentPublisher))
				{
					result = documentPublisher.Publish().ContinueWith(new Action<Task>(this.ExecutionComplete));
				}
				else
				{
					this.RequestDetailsLogger.UpdateLatency(ServiceLatencyMetadata.HttpPipelineLatency, this.RequestDetailsLogger.ActivityScope.TotalMilliseconds);
					this.InitializeODataContext();
					Task<ODataResponse> task = this.SelectOperation(this.ODataContext);
					Task task2 = task.ContinueWith(new Action<Task<ODataResponse>>(this.ExecutionComplete));
					result = task2;
				}
			}
			catch (Exception ex)
			{
				result = this.CreateErrorTask(ex);
			}
			return result;
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x00120C10 File Offset: 0x0011EE10
		private void InitializeServiceModel()
		{
			this.ServiceModel = ServiceModel.Version1Model.Member;
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00120C24 File Offset: 0x0011EE24
		private void InitializeODataContext()
		{
			try
			{
				Uri uri = this.NormalizeRequestUrl(this.HttpContext.GetRequestUri());
				ODataUriParser odataUriParser = new ODataUriParser(this.ServiceModel.EdmModel, this.HttpContext.GetServiceRootUri(), uri)
				{
					Resolver = new StringAsEnumResolver
					{
						EnableCaseInsensitive = true
					}
				};
				odataUriParser.UrlConventions = ODataUrlConventions.KeyAsSegment;
				ODataPath odataPath = odataUriParser.ParsePath();
				ODataPathWrapper odataPath2 = new ODataPathWrapper(odataPath);
				this.ValidatePath(odataPath2);
				this.ODataContext = new ODataContext(this.HttpContext, uri, this.ServiceModel, odataPath2, odataUriParser);
			}
			catch (ODataException odataException)
			{
				throw new UrlResolutionException(odataException);
			}
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x00120CD4 File Offset: 0x0011EED4
		private Uri NormalizeRequestUrl(Uri requestUri)
		{
			UriBuilder uriBuilder = new UriBuilder(requestUri);
			if (!uriBuilder.Path.EndsWith("/"))
			{
				UriBuilder uriBuilder2 = uriBuilder;
				uriBuilder2.Path += "/";
			}
			if (this.HttpContext.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
			{
				IEnumerable<IEdmOperation> enumerable = this.ServiceModel.EdmModel.SchemaElements.OfType<IEdmOperation>();
				foreach (IEdmOperation edmOperation in enumerable)
				{
					string text = "/" + edmOperation.Name + "/";
					if (uriBuilder.Path.EndsWith(text, StringComparison.OrdinalIgnoreCase))
					{
						uriBuilder.Path = uriBuilder.Path.Replace(text, "/" + ExtensionMethods.FullName(edmOperation) + "/");
					}
				}
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x00120DD4 File Offset: 0x0011EFD4
		private void ValidatePath(ODataPathWrapper odataPath)
		{
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x00120DD8 File Offset: 0x0011EFD8
		private bool IsDocumentRequest(out DocumentPublisher publisher)
		{
			publisher = null;
			string text = this.HttpContext.Request.RawUrl.TrimEnd(new char[]
			{
				'/'
			}).ToLower();
			if (text.EndsWith("/odata"))
			{
				publisher = new ServiceDocumentPublisher(this.HttpContext, this.ServiceModel);
				return true;
			}
			if (text.EndsWith("/odata/$metadata"))
			{
				publisher = new MetadataPublisher(this.HttpContext, this.ServiceModel);
				return true;
			}
			return false;
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00120E58 File Offset: 0x0011F058
		private Task<ODataResponse> SelectOperation(ODataContext context)
		{
			OperationSelector operationSelector = new OperationSelector(context);
			ODataRequest odataRequest = operationSelector.SelectOperation();
			odataRequest.ODataContext.RequestDetailsLogger.Set(ActivityStandardMetadata.Action, odataRequest.GetOperationNameForLogging());
			try
			{
				odataRequest.LoadFromHttpRequest();
			}
			catch (HttpRequestTransportException readException)
			{
				throw new RequestBodyReadException(readException);
			}
			ODataPermission.Create(odataRequest).Check();
			return ODataTask.CreateTask(odataRequest);
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x00120EC4 File Offset: 0x0011F0C4
		private void ExecutionComplete(Task<ODataResponse> operationTask)
		{
			try
			{
				if (operationTask.Exception != null)
				{
					this.RequestDetailsLogger.AppendGenericError("ODataCommandException", operationTask.Exception.ToString());
					this.WriteException(operationTask.Exception);
				}
				else
				{
					ODataResponse result = operationTask.Result;
					result.WriteHttpResponse();
				}
			}
			catch (HttpResponseTransportException ex)
			{
				this.RequestDetailsLogger.AppendGenericError("HttpResponseTransportException", ex.ToString());
			}
			catch (Exception ex2)
			{
				this.RequestDetailsLogger.AppendGenericError("UnknownResponseException", ex2.ToString());
				this.WriteException(ex2);
			}
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00120F68 File Offset: 0x0011F168
		private void ExecutionComplete(Task publishTask)
		{
			try
			{
				if (publishTask.Exception != null)
				{
					this.RequestDetailsLogger.AppendGenericError("ODataCommandException", publishTask.Exception.ToString());
					this.WriteException(publishTask.Exception);
				}
			}
			catch (HttpResponseTransportException ex)
			{
				this.RequestDetailsLogger.AppendGenericError("HttpResponseTransportException", ex.ToString());
			}
			catch (Exception ex2)
			{
				this.RequestDetailsLogger.AppendGenericError("UnknownResponseException", ex2.ToString());
				this.WriteException(ex2);
			}
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00120FFC File Offset: 0x0011F1FC
		private void WriteException(Exception exception)
		{
			try
			{
				ResponseMessageWriter responseMessageWriter = new ResponseMessageWriter(this.HttpContext, this.ServiceModel);
				responseMessageWriter.WriteError(exception);
			}
			catch (HttpResponseTransportException ex)
			{
				this.RequestDetailsLogger.AppendGenericError("HttpResponseTransportException", ex.ToString());
			}
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x0012106C File Offset: 0x0011F26C
		private Task CreateErrorTask(Exception ex)
		{
			return Task.Factory.StartNew(delegate()
			{
				this.ProcessException(ex);
			});
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001210A4 File Offset: 0x0011F2A4
		private void ProcessException(Exception ex)
		{
			this.RequestDetailsLogger.Set(ServiceCommonMetadata.GenericErrors, ex.ToString());
			if (this.ServiceModel == null)
			{
				this.HttpContext.Response.StatusCode = 500;
				this.HttpContext.Response.Output.Write(ex.ToString());
				return;
			}
			this.WriteException(ex);
		}
	}
}
