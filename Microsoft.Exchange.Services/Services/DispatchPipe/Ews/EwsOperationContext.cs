using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD1 RID: 3537
	public class EwsOperationContext : EwsOperationContextBase
	{
		// Token: 0x06005A5C RID: 23132 RVA: 0x00119CA7 File Offset: 0x00117EA7
		public static EwsOperationContext Create(Message requestMessage)
		{
			return new EwsOperationContext(requestMessage);
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x00119CAF File Offset: 0x00117EAF
		private EwsOperationContext(Message requestMessage)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x00119CBE File Offset: 0x00117EBE
		public override Message RequestMessage
		{
			get
			{
				return this.requestMessage;
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x00119CC6 File Offset: 0x00117EC6
		public override MessageProperties IncomingMessageProperties
		{
			get
			{
				if (this.requestMessage != null)
				{
					return this.requestMessage.Properties;
				}
				return null;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x00119CDD File Offset: 0x00117EDD
		public override MessageHeaders IncomingMessageHeaders
		{
			get
			{
				if (this.requestMessage != null)
				{
					return this.requestMessage.Headers;
				}
				return null;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x00119CF4 File Offset: 0x00117EF4
		public override MessageVersion IncomingMessageVersion
		{
			get
			{
				if (this.requestMessage != null)
				{
					return this.requestMessage.Version;
				}
				return null;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x00119D0C File Offset: 0x00117F0C
		public override IEnumerable<SupportingTokenSpecification> SupportingTokens
		{
			get
			{
				MessageProperties incomingMessageProperties = this.IncomingMessageProperties;
				if (incomingMessageProperties != null && incomingMessageProperties.Security != null)
				{
					return new ReadOnlyCollection<SupportingTokenSpecification>(incomingMessageProperties.Security.IncomingSupportingTokens);
				}
				return null;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x00119D3D File Offset: 0x00117F3D
		public override MessageProperties OutgoingMessageProperties
		{
			get
			{
				if (this.outgoingMessageProperties == null)
				{
					this.outgoingMessageProperties = new MessageProperties();
				}
				return this.outgoingMessageProperties;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x00119D58 File Offset: 0x00117F58
		public override Uri LocalAddressUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x00119D5B File Offset: 0x00117F5B
		internal override OperationContext BackingOperationContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040031EF RID: 12783
		private MessageProperties outgoingMessageProperties;

		// Token: 0x040031F0 RID: 12784
		private Message requestMessage;
	}
}
