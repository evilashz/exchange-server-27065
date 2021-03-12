using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200012A RID: 298
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GetUserPhotoRequestDispatcher
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x00022DB6 File Offset: 0x00020FB6
		public GetUserPhotoRequestDispatcher(ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00022DDC File Offset: 0x00020FDC
		public DispatchStepResult Dispatch(RequestContext context)
		{
			if (context == null || context.HttpContext == null || context.HttpContext.Request == null || context.HttpContext.Response == null)
			{
				return DispatchStepResult.Continue;
			}
			if (!context.HttpContext.Request.Path.EndsWith("/GetUserPhoto", StringComparison.OrdinalIgnoreCase))
			{
				return DispatchStepResult.Continue;
			}
			HttpContext httpContext = new OwaPhotoRequestorWriter(NullPerformanceDataLogger.Instance, this.tracer).Write(context.HttpContext, context.HttpContext);
			if (!GetUserPhotoRequestDispatcher.IsRequestorInPhotoStackV2Flight(httpContext))
			{
				return DispatchStepResult.Continue;
			}
			DispatchStepResult result;
			try
			{
				new PhotoRequestHandler(OwaApplication.GetRequestDetailsLogger).ProcessRequest(httpContext);
				context.HttpStatusCode = (HttpStatusCode)context.HttpContext.Response.StatusCode;
				result = DispatchStepResult.EndResponseWithPrivateCaching;
			}
			catch (TooComplexPhotoRequestException arg)
			{
				this.tracer.TraceDebug<TooComplexPhotoRequestException>((long)this.GetHashCode(), "[GetUserPhotoRequestDispatcher::DispatchIfGetUserPhotoRequest] too complex photo request.  Exception: {0}", arg);
				result = DispatchStepResult.Continue;
			}
			return result;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00022EB4 File Offset: 0x000210B4
		private static bool IsRequestorInPhotoStackV2Flight(HttpContext request)
		{
			return new PhotoRequestorReader().EnabledInFasterPhotoFlight(request);
		}

		// Token: 0x040006A1 RID: 1697
		private const string GetUserPhotoRequestPathSuffix = "/GetUserPhoto";

		// Token: 0x040006A2 RID: 1698
		private readonly ITracer tracer = NullTracer.Instance;
	}
}
