using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x02000109 RID: 265
	internal class ValidationResultCollector
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000391F9 File Offset: 0x000373F9
		public static ValidationResultCollector NullInstance
		{
			get
			{
				return ValidationResultCollector.nullInstance;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00039200 File Offset: 0x00037400
		public ValidationResultCollector()
		{
			this.nodes = new List<ValidationResultNode>(20);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00039215 File Offset: 0x00037415
		public virtual void Add(LocalizedString task, LocalizedString detail, ResultType resultType)
		{
			this.nodes.Add(new ValidationResultNode(task, detail, resultType));
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0003922A File Offset: 0x0003742A
		public IEnumerable<ValidationResultNode> Results
		{
			get
			{
				return this.nodes;
			}
		}

		// Token: 0x040007F1 RID: 2033
		private static readonly ValidationResultCollector nullInstance = new ValidationResultCollector.NullValidationResultCollector();

		// Token: 0x040007F2 RID: 2034
		private List<ValidationResultNode> nodes;

		// Token: 0x0200010A RID: 266
		internal sealed class NullValidationResultCollector : ValidationResultCollector
		{
			// Token: 0x060008BE RID: 2238 RVA: 0x0003923E File Offset: 0x0003743E
			public override void Add(LocalizedString task, LocalizedString detail, ResultType resultType)
			{
			}
		}
	}
}
