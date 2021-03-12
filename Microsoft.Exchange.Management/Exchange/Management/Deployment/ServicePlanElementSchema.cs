using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200023E RID: 574
	internal abstract class ServicePlanElementSchema : IEnumerable
	{
		// Token: 0x0600132E RID: 4910 RVA: 0x00054997 File Offset: 0x00052B97
		protected ServicePlanElementSchema()
		{
			this.InitializeAllFeatures();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000549A5 File Offset: 0x00052BA5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.allFeatures.GetEnumerator();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000549B8 File Offset: 0x00052BB8
		private void InitializeAllFeatures()
		{
			List<FeatureDefinition> list = new List<FeatureDefinition>();
			FieldInfo[] fields = base.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.FieldType == typeof(FeatureDefinition))
				{
					FeatureDefinition item = (FeatureDefinition)fieldInfo.GetValue(null);
					list.Add(item);
				}
			}
			this.allFeatures = list;
		}

		// Token: 0x04000846 RID: 2118
		private List<FeatureDefinition> allFeatures;
	}
}
