using System;
using System.IO;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Xml;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA3 RID: 3491
	internal class SoapWcfResponseRenderer : BaseResponseRenderer
	{
		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060058A8 RID: 22696 RVA: 0x00114347 File Offset: 0x00112547
		// (set) Token: 0x060058A9 RID: 22697 RVA: 0x0011434F File Offset: 0x0011254F
		internal HttpStatusCode? StatusCode { get; private set; }

		// Token: 0x060058AA RID: 22698 RVA: 0x00114358 File Offset: 0x00112558
		public static SoapWcfResponseRenderer Create(HttpStatusCode statusCode)
		{
			return new SoapWcfResponseRenderer(new HttpStatusCode?(statusCode));
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x00114365 File Offset: 0x00112565
		private SoapWcfResponseRenderer(HttpStatusCode? statusCode)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x00114374 File Offset: 0x00112574
		internal override void Render(Message message, Stream stream)
		{
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8, false);
			if (this.StatusCode != null && HttpContext.Current != null)
			{
				HttpContext.Current.Response.StatusCode = (int)this.StatusCode.Value;
			}
			EWSSettings.WritingToWire = true;
			xmlDictionaryWriter.WriteStartDocument();
			message.WriteMessage(xmlDictionaryWriter);
			xmlDictionaryWriter.WriteEndDocument();
			xmlDictionaryWriter.Flush();
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x001143E1 File Offset: 0x001125E1
		internal override void Render(Message message, Stream stream, HttpResponse response)
		{
			this.Render(message, stream);
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060058AE RID: 22702 RVA: 0x001143EB File Offset: 0x001125EB
		public static SoapWcfResponseRenderer Singleton
		{
			get
			{
				return SoapWcfResponseRenderer.singleton;
			}
		}

		// Token: 0x0400313D RID: 12605
		private static SoapWcfResponseRenderer singleton = new SoapWcfResponseRenderer(null);
	}
}
