using System;
using System.Xml;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002E2 RID: 738
	internal class ExecuteDiagnosticMethod : SingleStepServiceCommand<ExecuteDiagnosticMethodRequest, XmlNode>
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x00066B88 File Offset: 0x00064D88
		public ExecuteDiagnosticMethod(CallContext callContext, ExecuteDiagnosticMethodRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00066B94 File Offset: 0x00064D94
		internal override ServiceResult<XmlNode> Execute()
		{
			DiagnosticMethodDelegate @delegate = DiagnosticMethodDelegateCollection.Singleton.GetDelegate(base.Request.Verb);
			return new ServiceResult<XmlNode>(@delegate(base.Request.Parameter));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00066BD0 File Offset: 0x00064DD0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			BaseInfoResponse baseInfoResponse = new ExecuteDiagnosticMethodResponse();
			baseInfoResponse.ProcessServiceResult<XmlNode>(base.Result);
			return baseInfoResponse;
		}

		// Token: 0x04000DA9 RID: 3497
		public const string MethodName = "ExecuteDiagnosticMethod";
	}
}
