using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml.Schema;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DBC RID: 3516
	internal class ResponseSchemaValidatorInspector : IOutboundInspector
	{
		// Token: 0x06005977 RID: 22903 RVA: 0x00117F32 File Offset: 0x00116132
		private string GetMessageResponseText()
		{
			return this.message.ToString();
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x00117F68 File Offset: 0x00116168
		public void ProcessOutbound(ExchangeVersion requestVersion, Message reply)
		{
			if (!this.ShouldValidateResponse(requestVersion))
			{
				return;
			}
			if (!reply.IsFault)
			{
				this.message = reply;
				bool treatWarningsAsErrors = MessageInspectorManager.IsAvailabilityRequest(reply.Headers.Action);
				SchemaValidator schemaValidator = new SchemaValidator(delegate(XmlSchemaException exception, SoapSavvyReader.SoapSection section)
				{
					throw FaultExceptionUtilities.CreateFault(new ResponseSchemaValidationException(exception, exception.LineNumber, exception.LinePosition, exception.Message, this.GetMessageResponseText()), FaultParty.Receiver);
				});
				string s = reply.ToString();
				using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
				{
					RequestDetailsLogger.Current.Set(ServiceCommonMetadata.ResponseSize, memoryStream.Length);
					schemaValidator.ValidateMessage(memoryStream, ExchangeVersion.Current, treatWarningsAsErrors, false);
				}
			}
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x00118020 File Offset: 0x00116220
		private bool ShouldValidateResponse(ExchangeVersion requestVersion)
		{
			bool? enableSchemaValidationOverride = Global.EnableSchemaValidationOverride;
			if (enableSchemaValidationOverride != null)
			{
				return enableSchemaValidationOverride.Value;
			}
			bool flag = requestVersion > ExchangeVersion.Exchange2013;
			return !flag;
		}

		// Token: 0x0400318B RID: 12683
		private Message message;
	}
}
