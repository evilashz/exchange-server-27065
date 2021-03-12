using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000785 RID: 1925
	internal abstract class LogSerializer
	{
		// Token: 0x060043D0 RID: 17360 RVA: 0x00116326 File Offset: 0x00114526
		public LogSerializer()
		{
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x00116339 File Offset: 0x00114539
		public LogSerializer(IEnumerable<PropertyDefinition> propertyMask)
		{
			this.propertyMask = propertyMask;
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x00116354 File Offset: 0x00114554
		public string Serialize(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, IEnumerable<PropertyDefinition> maskedProperties = null)
		{
			if (maskedProperties == null)
			{
				maskedProperties = new List<PropertyDefinition>();
			}
			StringBuilder stringBuilder = new StringBuilder();
			properties = properties.Except(maskedProperties);
			this.SerializeHeader(logs, properties, ref stringBuilder);
			this.SerializeBody(logs, properties, ref stringBuilder);
			this.SerializeFooter(logs, properties, ref stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x0011639E File Offset: 0x0011459E
		protected virtual void SerializeHeader(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x001163A0 File Offset: 0x001145A0
		protected virtual void SerializeBody(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x001163A2 File Offset: 0x001145A2
		protected virtual void SerializeFooter(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
		}

		// Token: 0x04002A28 RID: 10792
		protected IEnumerable<PropertyDefinition> propertyMask = new List<PropertyDefinition>();
	}
}
