using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000459 RID: 1113
	public class GetMime : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002912 RID: 10514 RVA: 0x000E8700 File Offset: 0x000E6900
		protected override void OnPreRender(EventArgs e)
		{
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000E8704 File Offset: 0x000E6904
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "GetMime.OnLoad");
			base.OnLoad(e);
			base.InitializeAsMessageItem(new PropertyDefinition[0]);
			base.Item.Load(StoreObjectSchema.ContentConversionProperties);
			base.Response.AppendHeader("X-OWA-EventResult", "0");
			base.Response.ContentType = Utilities.GetContentTypeString(OwaEventContentType.PlainText);
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000E8774 File Offset: 0x000E6974
		protected void RenderMime()
		{
			OutboundConversionOptions outboundConversionOptions = Utilities.CreateOutboundConversionOptions(base.UserContext);
			outboundConversionOptions.AllowPartialStnefConversion = true;
			outboundConversionOptions.DemoteBcc = true;
			try
			{
				ItemConversion.ConvertItemToMime(base.Item, base.Response.OutputStream, outboundConversionOptions);
			}
			catch (NotSupportedException innerException)
			{
				throw new OwaInvalidRequestException("Conversion failed", innerException);
			}
			catch (NotImplementedException innerException2)
			{
				throw new OwaInvalidRequestException("Conversion failed", innerException2);
			}
			catch (StoragePermanentException innerException3)
			{
				throw new OwaInvalidRequestException("Conversion failed", innerException3);
			}
			catch (StorageTransientException innerException4)
			{
				throw new OwaInvalidRequestException("Conversion failed", innerException4);
			}
		}
	}
}
