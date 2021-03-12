using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD6 RID: 3542
	public class WrappedWcfOperationContext : EwsOperationContextBase
	{
		// Token: 0x06005A7E RID: 23166 RVA: 0x0011A646 File Offset: 0x00118846
		public WrappedWcfOperationContext(OperationContext operationContext)
		{
			this.operationContext = operationContext;
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x0011A655 File Offset: 0x00118855
		public override Message RequestMessage
		{
			get
			{
				if (this.operationContext.RequestContext != null)
				{
					return this.operationContext.RequestContext.RequestMessage;
				}
				return null;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06005A80 RID: 23168 RVA: 0x0011A676 File Offset: 0x00118876
		public override MessageProperties IncomingMessageProperties
		{
			get
			{
				return this.operationContext.IncomingMessageProperties;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06005A81 RID: 23169 RVA: 0x0011A683 File Offset: 0x00118883
		public override MessageHeaders IncomingMessageHeaders
		{
			get
			{
				return this.operationContext.IncomingMessageHeaders;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06005A82 RID: 23170 RVA: 0x0011A690 File Offset: 0x00118890
		public override MessageVersion IncomingMessageVersion
		{
			get
			{
				return this.operationContext.IncomingMessageVersion;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06005A83 RID: 23171 RVA: 0x0011A69D File Offset: 0x0011889D
		public override IEnumerable<SupportingTokenSpecification> SupportingTokens
		{
			get
			{
				return this.operationContext.SupportingTokens;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06005A84 RID: 23172 RVA: 0x0011A6AA File Offset: 0x001188AA
		public override MessageProperties OutgoingMessageProperties
		{
			get
			{
				return this.operationContext.OutgoingMessageProperties;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06005A85 RID: 23173 RVA: 0x0011A6B7 File Offset: 0x001188B7
		public override Uri LocalAddressUri
		{
			get
			{
				return this.operationContext.Channel.LocalAddress.Uri;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06005A86 RID: 23174 RVA: 0x0011A6CE File Offset: 0x001188CE
		internal override OperationContext BackingOperationContext
		{
			get
			{
				return this.operationContext;
			}
		}

		// Token: 0x04003206 RID: 12806
		private OperationContext operationContext;
	}
}
