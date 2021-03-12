using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x0200009C RID: 156
	internal class DiagnosticsProcessor : DiagnosticsProcessorBase
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0001903B File Offset: 0x0001723B
		public new DiagnosticsArgument Arguments
		{
			get
			{
				return base.Arguments;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00019043 File Offset: 0x00017243
		public DiagnosticsProcessor(DiagnosableParameters parameters) : base(new DiagnosticsArgument(parameters.Argument))
		{
			this.tbaProcessor = new DiagnosticsTbaProcessor(this.Arguments);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00019068 File Offset: 0x00017268
		public XElement Process(TimeBasedAssistantControllerWrapper[] assistantControllers)
		{
			ArgumentValidator.ThrowIfNull("assistantControllers", assistantControllers);
			if (this.Arguments.ArgumentCount != 0)
			{
				return this.tbaProcessor.Process(assistantControllers);
			}
			return DiagnosticsFormatter.FormatHelpElement(this.Arguments);
		}

		// Token: 0x040002D5 RID: 725
		private readonly DiagnosticsTbaProcessor tbaProcessor;
	}
}
