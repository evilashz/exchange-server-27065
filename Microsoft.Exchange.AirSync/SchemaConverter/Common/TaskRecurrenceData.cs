using System;
using System.Collections;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	internal class TaskRecurrenceData : INestedData
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x00060193 File Offset: 0x0005E393
		public TaskRecurrenceData()
		{
			this.subProperties = new Hashtable();
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000601A6 File Offset: 0x0005E3A6
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x000601BE File Offset: 0x0005E3BE
		public bool IsRecurring
		{
			get
			{
				return this.subProperties["IsRecurring"] != null;
			}
			set
			{
				this.subProperties["IsRecurring"] = value.ToString();
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000601D7 File Offset: 0x0005E3D7
		public IDictionary SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000601DF File Offset: 0x0005E3DF
		public void Clear()
		{
			this.subProperties.Clear();
		}

		// Token: 0x04000B37 RID: 2871
		private IDictionary subProperties;
	}
}
