using System;

namespace Microsoft.Exchange.Management.ControlPanel.DDI
{
	// Token: 0x0200015E RID: 350
	public class LambdaExpressionException : Exception
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x000656A2 File Offset: 0x000638A2
		public LambdaExpressionException(string errorExpression, Exception innerException) : base("\r\nSystem throws an exception at calculating Lambda Expression : [ " + errorExpression + " ]\r\n" + innerException.Message, innerException)
		{
		}
	}
}
