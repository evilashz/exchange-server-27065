using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Pipeline
{
	// Token: 0x02000002 RID: 2
	internal class ErrorHandler : IPipelineErrorHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private ErrorHandler()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		internal static ErrorHandler Instance
		{
			get
			{
				return ErrorHandler.instance;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		public DocumentResolution HandleException(IPipelineComponent component, ComponentException exception)
		{
			if (exception is PoisonComponentException)
			{
				return DocumentResolution.PoisonComponentAndContinue;
			}
			return DocumentResolution.CompleteSuccess;
		}

		// Token: 0x04000001 RID: 1
		private static ErrorHandler instance = new ErrorHandler();
	}
}
