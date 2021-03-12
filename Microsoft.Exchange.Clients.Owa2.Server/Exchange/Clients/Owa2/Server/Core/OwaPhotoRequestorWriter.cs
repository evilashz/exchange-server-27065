using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000219 RID: 537
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaPhotoRequestorWriter
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x000493A4 File Offset: 0x000475A4
		public OwaPhotoRequestorWriter(IPerformanceDataLogger perfLogger, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.perfLogger = perfLogger;
			this.tracer = upstreamTracer;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000493F4 File Offset: 0x000475F4
		public HttpContext Write(HttpContext authenticatedUserContext, HttpContext output)
		{
			ArgumentValidator.ThrowIfNull("authenticatedUserContext", authenticatedUserContext);
			ArgumentValidator.ThrowIfNull("output", output);
			HttpContext result;
			try
			{
				using (new StopwatchPerformanceTracker("PhotoRequestorWriterResolveIdentity", this.perfLogger))
				{
					using (new ADPerformanceTracker("PhotoRequestorWriterResolveIdentity", this.perfLogger))
					{
						using (OwaIdentity owaIdentity = OwaIdentity.ResolveLogonIdentity(authenticatedUserContext, null))
						{
							if (owaIdentity == null)
							{
								this.tracer.TraceDebug((long)this.GetHashCode(), "OwaPhotoRequestorWriter:  requestor could NOT be resolved.");
								return output;
							}
							OrganizationId organization = OwaPhotoRequestorWriter.GetOrganization(owaIdentity);
							if (organization == null)
							{
								this.tracer.TraceError((long)this.GetHashCode(), "OwaPhotoRequestorWriter:  could NOT determine requestor's organization.");
								return output;
							}
							output.Items["Photo.Requestor"] = new PhotoPrincipal
							{
								OrganizationId = organization,
								EmailAddresses = this.ExtractSmtpAddresses(owaIdentity)
							};
							output.Items["Photo.Requestor.EnabledInFasterPhotoFlightHttpContextKey"] = OwaPhotoRequestorWriter.InPhotoFasterPhotoFlight(owaIdentity.OwaMiniRecipient);
						}
					}
				}
				result = output;
			}
			catch (OwaADObjectNotFoundException arg)
			{
				this.tracer.TraceError<OwaADObjectNotFoundException>((long)this.GetHashCode(), "OwaPhotoRequestorWriter:  requestor NOT found.  Exception: {0}", arg);
				result = output;
			}
			catch (TransientException arg2)
			{
				this.tracer.TraceError<TransientException>((long)this.GetHashCode(), "OwaPhotoRequestorWriter:  failed to resolve requestor with a transient error.  Exception: {0}", arg2);
				result = output;
			}
			catch (ADOperationException arg3)
			{
				this.tracer.TraceError<ADOperationException>((long)this.GetHashCode(), "OwaPhotoRequestorWriter:  failed to resolve requestor with permanent directory error.  Exception: {0}", arg3);
				result = output;
			}
			return result;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x000495BC File Offset: 0x000477BC
		private static OrganizationId GetOrganization(OwaIdentity requestor)
		{
			if (requestor.UserOrganizationId != null)
			{
				return requestor.UserOrganizationId;
			}
			OWAMiniRecipient owaminiRecipient = requestor.OwaMiniRecipient ?? requestor.GetOWAMiniRecipient();
			if (owaminiRecipient != null)
			{
				return owaminiRecipient.OrganizationId;
			}
			return null;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0004960C File Offset: 0x0004780C
		private ICollection<string> ExtractSmtpAddresses(OwaIdentity requestor)
		{
			if (requestor == null)
			{
				return Array<string>.Empty;
			}
			OWAMiniRecipient owaminiRecipient = requestor.OwaMiniRecipient ?? requestor.GetOWAMiniRecipient();
			if (owaminiRecipient == null)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "OwaPhotoRequestorWriter:  cannot extract SMTP addresses because recipient information has NOT been initialized or computed for requestor.");
				return Array<string>.Empty;
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			SmtpAddress primarySmtpAddress = requestor.PrimarySmtpAddress;
			hashSet.Add(requestor.PrimarySmtpAddress.ToString());
			if (owaminiRecipient.EmailAddresses != null && owaminiRecipient.EmailAddresses.Count > 0)
			{
				hashSet.UnionWith(from a in owaminiRecipient.EmailAddresses
				where OwaPhotoRequestorWriter.IsNonBlankSmtpAddress(a)
				select a.ValueString);
			}
			return hashSet;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000496E7 File Offset: 0x000478E7
		private static bool IsNonBlankSmtpAddress(ProxyAddress address)
		{
			return !(address == null) && ProxyAddressPrefix.Smtp.Equals(address.Prefix) && !string.IsNullOrWhiteSpace(address.ValueString);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00049718 File Offset: 0x00047918
		private static bool InPhotoFasterPhotoFlight(OWAMiniRecipient requestor)
		{
			return requestor != null && VariantConfiguration.GetSnapshot(requestor.GetContext(null), null, null).OwaClientServer.FasterPhoto.Enabled;
		}

		// Token: 0x04000B35 RID: 2869
		private const string RequestorKeyInHttpContext = "Photo.Requestor";

		// Token: 0x04000B36 RID: 2870
		private const string EnabledInFasterPhotoFlightKeyInHttpContext = "Photo.Requestor.EnabledInFasterPhotoFlightHttpContextKey";

		// Token: 0x04000B37 RID: 2871
		private readonly ITracer tracer = NullTracer.Instance;

		// Token: 0x04000B38 RID: 2872
		private readonly IPerformanceDataLogger perfLogger = NullPerformanceDataLogger.Instance;
	}
}
