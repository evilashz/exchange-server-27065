using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.Exchange.Autodiscover;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000002 RID: 2
	[ExcludeFromCodeCoverage]
	internal class AutoDiscoverV2HandlerBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AutoDiscoverV2HandlerBase()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		internal AutoDiscoverV2HandlerBase(RequestDetailsLogger logger)
		{
			this.Logger = logger;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E7 File Offset: 0x000002E7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		protected RequestDetailsLogger Logger { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020FB File Offset: 0x000002FB
		public void ProcessRequest(HttpContext context)
		{
			this.Logger = RequestDetailsLoggerBase<RequestDetailsLogger>.Current;
			this.ProcessRequest(new HttpContextWrapper(context));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		public virtual string GetEmailAddressFromUrl(HttpContextBase context)
		{
			return null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002117 File Offset: 0x00000317
		public virtual bool Validate(HttpContextBase context)
		{
			return true;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002210 File Offset: 0x00000410
		internal void ProcessRequest(HttpContextBase context)
		{
			try
			{
				Common.SendWatsonReportOnUnhandledException(delegate
				{
					this.Logger.Set(ActivityStandardMetadata.Action, "AutoDiscoverV2");
					FlightSettingRepository flightSettings = new FlightSettingRepository();
					try
					{
						if (this.Validate(context))
						{
							AutoDiscoverV2 autoDiscoverV = new AutoDiscoverV2(this.Logger, flightSettings, new TenantRepository(this.Logger));
							string emailAddressFromUrl = this.GetEmailAddressFromUrl(context);
							AutoDiscoverV2Request request = autoDiscoverV.CreateRequestFromContext(context, emailAddressFromUrl);
							AutoDiscoverV2Response autoDiscoverV2Response = autoDiscoverV.ProcessRequest(request, flightSettings);
							if (autoDiscoverV2Response != null)
							{
								this.EmitResponse(context, autoDiscoverV2Response);
							}
							else
							{
								this.LogAndEmitErrorResponse(context, AutoDiscoverResponseException.NotFound());
							}
						}
					}
					catch (AutoDiscoverResponseException exception)
					{
						this.LogAndEmitErrorResponse(context, exception);
					}
				});
			}
			catch (Exception innerException)
			{
				this.EmitErrorResponse(context, AutoDiscoverResponseException.InternalServerError("InternalServerError", innerException));
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002278 File Offset: 0x00000478
		internal void EmitResponse(HttpContextBase context, AutoDiscoverV2Response response)
		{
			context.Response.ContentType = "application/json";
			if (response.RedirectUrl != null)
			{
				context.Response.Redirect(response.RedirectUrl, false);
				this.Logger.AppendGenericInfo("EmitResponseRedirect", response.RedirectUrl);
				return;
			}
			if (response.Url != null)
			{
				context.Response.StatusCode = 200;
				JsonSuccessResponse jsonSuccessResponse = new JsonSuccessResponse
				{
					Protocol = response.ProtocolName,
					Url = response.Url
				};
				string text = jsonSuccessResponse.SerializeToJson(jsonSuccessResponse);
				this.Logger.AppendGenericInfo("EmitResponseJsonString", text);
				context.Response.Write(text);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002324 File Offset: 0x00000524
		internal void EmitErrorResponse(HttpContextBase context, AutoDiscoverResponseException exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.Headers["jsonerror"] = "true";
			context.Response.StatusCode = exception.HttpStatusCodeValue;
			JsonErrorResponse jsonErrorResponse = new JsonErrorResponse
			{
				ErrorMessage = exception.Message,
				ErrorCode = exception.ErrorCode
			};
			string text = jsonErrorResponse.SerializeToJson(jsonErrorResponse);
			this.Logger.AppendGenericInfo("EmitErrorResponseJsonString", text);
			context.Response.Write(text);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023B1 File Offset: 0x000005B1
		private void LogAndEmitErrorResponse(HttpContextBase context, AutoDiscoverResponseException exception)
		{
			this.Logger.AppendGenericError("AutoDiscoverResponseException", exception.ToString());
			this.EmitErrorResponse(context, exception);
		}
	}
}
