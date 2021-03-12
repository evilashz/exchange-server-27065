using System;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000207 RID: 519
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestorHeader
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x0004E4CE File Offset: 0x0004C6CE
		private PhotoRequestorHeader()
		{
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004E4D8 File Offset: 0x0004C6D8
		public void Serialize(PhotoPrincipal requestor, WebRequest request)
		{
			ArgumentValidator.ThrowIfNull("requestor", requestor);
			ArgumentValidator.ThrowIfNull("request", request);
			if (requestor.OrganizationId == null)
			{
				return;
			}
			request.Headers.Set("X-Exchange-Photos-Requestor-Organization-Id", PhotoRequestorHeader.SerializeOrganizationId(requestor.OrganizationId));
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0004E528 File Offset: 0x0004C728
		public PhotoPrincipal Deserialize(HttpRequest request, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			return new PhotoPrincipal
			{
				OrganizationId = this.DeserializeRequestorOrganizationId(request, tracer)
			};
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004E560 File Offset: 0x0004C760
		private static string SerializeOrganizationId(OrganizationId id)
		{
			return Convert.ToBase64String(id.GetBytes(PhotoRequestorHeader.SerializedOrganizationIdEncoding));
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004E574 File Offset: 0x0004C774
		private OrganizationId DeserializeRequestorOrganizationId(HttpRequest request, ITracer tracer)
		{
			string text = request.Headers["X-Exchange-Photos-Requestor-Organization-Id"];
			if (string.IsNullOrEmpty(text))
			{
				tracer.TraceDebug((long)this.GetHashCode(), "Cannot deserialize requestor's organization id because it was not found or is blank in request.");
				return null;
			}
			byte[] bytes;
			try
			{
				bytes = Convert.FromBase64String(text);
			}
			catch (FormatException arg)
			{
				tracer.TraceError<string, FormatException>((long)this.GetHashCode(), "Failed to deserialize requestor's organization id from base64 string: {0}.  Exception: {1}", text, arg);
				return null;
			}
			OrganizationId organizationId;
			if (!OrganizationId.TryCreateFromBytes(bytes, PhotoRequestorHeader.SerializedOrganizationIdEncoding, out organizationId))
			{
				tracer.TraceError<string>((long)this.GetHashCode(), "Failed to deserialize requestor's organization id from base64 string: {0}", text);
				return null;
			}
			tracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Deserialized requestor's organization id: {0}", organizationId);
			return organizationId;
		}

		// Token: 0x04000A5D RID: 2653
		private const string PhotoRequestorHeaderName = "X-Exchange-Photos-Requestor-Organization-Id";

		// Token: 0x04000A5E RID: 2654
		private static readonly Encoding SerializedOrganizationIdEncoding = Encoding.UTF8;

		// Token: 0x04000A5F RID: 2655
		internal static readonly PhotoRequestorHeader Default = new PhotoRequestorHeader();
	}
}
