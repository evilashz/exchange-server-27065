using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008EC RID: 2284
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AndOredAggregatorRule : IAggregatorRule
	{
		// Token: 0x060055B0 RID: 21936 RVA: 0x00162AA0 File Offset: 0x00160CA0
		internal AndOredAggregatorRule(PropertyDefinition propertyDefinition, bool isAndOperation, bool defaultValue)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnMismatchType<bool>(propertyDefinition, "propertyDefinition");
			this.propertyDefinition = propertyDefinition;
			this.isAndOperation = isAndOperation;
			this.defaultValue = defaultValue;
			this.result = defaultValue;
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00162ADA File Offset: 0x00160CDA
		public void BeginAggregation()
		{
			this.isInitialized = false;
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00162AE3 File Offset: 0x00160CE3
		public void EndAggregation()
		{
			if (!this.isInitialized)
			{
				this.result = this.defaultValue;
			}
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00162AFC File Offset: 0x00160CFC
		public void AddToAggregation(IStorePropertyBag propertyBag)
		{
			object obj = propertyBag.TryGetProperty(this.propertyDefinition);
			if (obj is bool)
			{
				bool flag = (bool)obj;
				if (this.isInitialized)
				{
					if (this.isAndOperation)
					{
						this.result = (this.result && flag);
						return;
					}
					this.result = (this.result || flag);
					return;
				}
				else
				{
					this.result = flag;
					this.isInitialized = true;
				}
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060055B4 RID: 21940 RVA: 0x00162B69 File Offset: 0x00160D69
		public bool Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04002DFB RID: 11771
		private PropertyDefinition propertyDefinition;

		// Token: 0x04002DFC RID: 11772
		private bool isAndOperation;

		// Token: 0x04002DFD RID: 11773
		private bool isInitialized;

		// Token: 0x04002DFE RID: 11774
		private bool defaultValue;

		// Token: 0x04002DFF RID: 11775
		private bool result;
	}
}
