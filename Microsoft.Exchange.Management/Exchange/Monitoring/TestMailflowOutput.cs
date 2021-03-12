using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005FC RID: 1532
	[Serializable]
	public class TestMailflowOutput : ConfigurableObject
	{
		// Token: 0x060036B0 RID: 14000 RVA: 0x000E2AC4 File Offset: 0x000E0CC4
		internal TestMailflowOutput(string testMailflowResult, EnhancedTimeSpan messageLatencyTime, bool isRemoteTest) : base(new SimpleProviderPropertyBag())
		{
			this.TestMailflowResult = testMailflowResult;
			this.MessageLatencyTime = messageLatencyTime;
			this.IsRemoteTest = isRemoteTest;
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000E2AE6 File Offset: 0x000E0CE6
		// (set) Token: 0x060036B2 RID: 14002 RVA: 0x000E2AFD File Offset: 0x000E0CFD
		public string TestMailflowResult
		{
			get
			{
				return (string)this.propertyBag[TestMailflowOutputSchema.TestMailflowResult];
			}
			private set
			{
				this.propertyBag[TestMailflowOutputSchema.TestMailflowResult] = value;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000E2B10 File Offset: 0x000E0D10
		// (set) Token: 0x060036B4 RID: 14004 RVA: 0x000E2B27 File Offset: 0x000E0D27
		public EnhancedTimeSpan MessageLatencyTime
		{
			get
			{
				return (EnhancedTimeSpan)this.propertyBag[TestMailflowOutputSchema.MessageLatencyTime];
			}
			private set
			{
				this.propertyBag[TestMailflowOutputSchema.MessageLatencyTime] = value;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000E2B3F File Offset: 0x000E0D3F
		// (set) Token: 0x060036B6 RID: 14006 RVA: 0x000E2B56 File Offset: 0x000E0D56
		public bool IsRemoteTest
		{
			get
			{
				return (bool)this.propertyBag[TestMailflowOutputSchema.IsRemoteTest];
			}
			private set
			{
				this.propertyBag[TestMailflowOutputSchema.IsRemoteTest] = value;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000E2B6E File Offset: 0x000E0D6E
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TestMailflowOutput.schema;
			}
		}

		// Token: 0x0400255B RID: 9563
		private static ObjectSchema schema = ObjectSchema.GetInstance<TestMailflowOutputSchema>();
	}
}
