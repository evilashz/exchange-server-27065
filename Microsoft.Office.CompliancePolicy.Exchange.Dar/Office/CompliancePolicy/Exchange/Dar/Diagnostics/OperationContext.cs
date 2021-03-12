using System;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x0200001A RID: 26
	public class OperationContext : IDisposable
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x0000506C File Offset: 0x0000326C
		public OperationContext(string correlationId) : this()
		{
			this.correlationId = correlationId;
			OperationContext.current = this;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005084 File Offset: 0x00003284
		public OperationContext() : this(Guid.NewGuid().ToString())
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000050AA File Offset: 0x000032AA
		public static string CorrelationId
		{
			get
			{
				if (OperationContext.current == null)
				{
					return null;
				}
				return OperationContext.current.correlationId;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000050BF File Offset: 0x000032BF
		public void Dispose()
		{
			OperationContext.current = null;
		}

		// Token: 0x04000051 RID: 81
		[ThreadStatic]
		private static OperationContext current;

		// Token: 0x04000052 RID: 82
		private readonly string correlationId;
	}
}
