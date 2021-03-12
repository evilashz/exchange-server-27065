using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200069E RID: 1694
	internal class ProxyResponseMessage : Message
	{
		// Token: 0x06003423 RID: 13347 RVA: 0x000BBEBA File Offset: 0x000BA0BA
		private ProxyResponseMessage(HttpWebResponse response, Stream responseStream, Message wrappedMessage)
		{
			this.webResponse = response;
			this.responseStream = responseStream;
			this.wrappedMessage = wrappedMessage;
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000BBED8 File Offset: 0x000BA0D8
		public static ProxyResponseMessage Create()
		{
			Stream stream = null;
			bool flag = false;
			ProxyResponseMessage result;
			try
			{
				stream = EWSSettings.ProxyResponse.GetResponseStream();
				XmlReaderSettings settings = new XmlReaderSettings
				{
					CheckCharacters = false
				};
				XmlReader envelopeReader = SafeXmlFactory.CreateSafeXmlReader(stream, settings);
				Message message = Message.CreateMessage(envelopeReader, int.MaxValue, EwsOperationContextBase.Current.IncomingMessageVersion);
				ProxyResponseMessage proxyResponseMessage = new ProxyResponseMessage(EWSSettings.ProxyResponse, stream, message);
				flag = true;
				result = proxyResponseMessage;
			}
			finally
			{
				if (!flag)
				{
					if (stream != null)
					{
						stream.Close();
					}
					EWSSettings.ProxyResponse.Close();
					EWSSettings.ProxyResponse = null;
				}
			}
			return result;
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000BBF6C File Offset: 0x000BA16C
		public override MessageHeaders Headers
		{
			get
			{
				return this.wrappedMessage.Headers;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000BBF79 File Offset: 0x000BA179
		public override bool IsFault
		{
			get
			{
				return this.wrappedMessage.IsFault;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000BBF86 File Offset: 0x000BA186
		public override bool IsEmpty
		{
			get
			{
				return this.wrappedMessage.IsEmpty;
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000BBF94 File Offset: 0x000BA194
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)3594923325U);
				this.wrappedMessage.WriteBodyContents(writer);
			}
			catch (IOException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>((long)this.GetHashCode(), "[ProxyResponseMessage::OnWriteBodyContents] Caught IOException trying to write body contents from proxy request stream: {0}", ex.ToString());
				FaultException ex2 = FaultExceptionUtilities.CreateFault(new TransientException(CoreResources.GetLocalizedString((CoreResources.IDs)3995283118U), ex), FaultParty.Receiver);
				throw ex2;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000BC000 File Offset: 0x000BA200
		public override MessageProperties Properties
		{
			get
			{
				return this.wrappedMessage.Properties;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000BC00D File Offset: 0x000BA20D
		public override MessageVersion Version
		{
			get
			{
				return this.wrappedMessage.Version;
			}
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000BC01A File Offset: 0x000BA21A
		protected override void OnClose()
		{
			base.OnClose();
			this.responseStream.Close();
			this.responseStream = null;
			this.webResponse.Close();
			this.webResponse = null;
		}

		// Token: 0x04001D80 RID: 7552
		private Message wrappedMessage;

		// Token: 0x04001D81 RID: 7553
		private HttpWebResponse webResponse;

		// Token: 0x04001D82 RID: 7554
		private Stream responseStream;
	}
}
