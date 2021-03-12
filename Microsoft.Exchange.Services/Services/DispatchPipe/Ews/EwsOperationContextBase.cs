using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD0 RID: 3536
	public abstract class EwsOperationContextBase
	{
		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x00119C74 File Offset: 0x00117E74
		public static EwsOperationContextBase Current
		{
			get
			{
				if (OperationContext.Current != null)
				{
					return new WrappedWcfOperationContext(OperationContext.Current);
				}
				return EwsOperationContextBase.currentEwsOperationContext;
			}
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x00119C8D File Offset: 0x00117E8D
		public static implicit operator EwsOperationContextBase(OperationContext operationContext)
		{
			return new WrappedWcfOperationContext(operationContext);
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06005A51 RID: 23121
		public abstract Message RequestMessage { get; }

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06005A52 RID: 23122
		public abstract MessageProperties IncomingMessageProperties { get; }

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06005A53 RID: 23123
		public abstract MessageHeaders IncomingMessageHeaders { get; }

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06005A54 RID: 23124
		public abstract MessageVersion IncomingMessageVersion { get; }

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06005A55 RID: 23125
		public abstract IEnumerable<SupportingTokenSpecification> SupportingTokens { get; }

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06005A56 RID: 23126
		public abstract MessageProperties OutgoingMessageProperties { get; }

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06005A57 RID: 23127
		public abstract Uri LocalAddressUri { get; }

		// Token: 0x06005A58 RID: 23128 RVA: 0x00119C95 File Offset: 0x00117E95
		internal static void SetCurrent(EwsOperationContextBase operationContext)
		{
			EwsOperationContextBase.currentEwsOperationContext = operationContext;
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06005A59 RID: 23129
		internal abstract OperationContext BackingOperationContext { get; }

		// Token: 0x040031EE RID: 12782
		[ThreadStatic]
		private static EwsOperationContextBase currentEwsOperationContext;
	}
}
