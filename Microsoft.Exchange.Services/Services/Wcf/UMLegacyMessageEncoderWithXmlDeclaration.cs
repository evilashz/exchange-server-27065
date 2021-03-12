using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA5 RID: 3493
	internal class UMLegacyMessageEncoderWithXmlDeclaration : MessageEncoderWithXmlDeclaration
	{
		// Token: 0x060058B2 RID: 22706 RVA: 0x00114455 File Offset: 0x00112655
		public UMLegacyMessageEncoderWithXmlDeclaration(MessageEncoderWithXmlDeclarationFactory factory) : base(factory)
		{
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x0011445E File Offset: 0x0011265E
		protected override bool ShouldValidateRequest(string methodName, string methodNamespace, ExchangeVersion requestVersion, int requestSize)
		{
			return false;
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x00114461 File Offset: 0x00112661
		protected override long MaxTraceRequestSize
		{
			get
			{
				return 0L;
			}
		}
	}
}
