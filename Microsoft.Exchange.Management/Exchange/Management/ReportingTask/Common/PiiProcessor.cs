using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A4 RID: 1700
	internal class PiiProcessor
	{
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x00100DF0 File Offset: 0x000FEFF0
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x00100DF8 File Offset: 0x000FEFF8
		public bool SuppressPiiEnabled { get; set; }

		// Token: 0x06003C49 RID: 15433 RVA: 0x00100E04 File Offset: 0x000FF004
		public void Supress(object dataObject)
		{
			if (!this.SuppressPiiEnabled)
			{
				return;
			}
			Type type = dataObject.GetType();
			List<PiiProcessor.PiiProperty> piiProperties = this.GetPiiProperties(type);
			foreach (PiiProcessor.PiiProperty piiProperty in piiProperties)
			{
				string text = (string)piiProperty.PropertyInfo.GetValue(dataObject, null);
				if (piiProperty.PiiDataType == PiiDataType.Smtp)
				{
					piiProperty.PropertyInfo.SetValue(dataObject, SuppressingPiiData.RedactSmtpAddress(text), null);
				}
				else
				{
					if (piiProperty.PiiDataType != PiiDataType.String)
					{
						throw new NotSupportedException("Unsupported PII data type: " + piiProperty.PiiDataType);
					}
					piiProperty.PropertyInfo.SetValue(dataObject, SuppressingPiiData.Redact(text), null);
				}
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00100ED0 File Offset: 0x000FF0D0
		private List<PiiProcessor.PiiProperty> GetPiiProperties(Type type)
		{
			List<PiiProcessor.PiiProperty> list;
			if (this.piiPropertyDictionary.ContainsKey(type))
			{
				list = this.piiPropertyDictionary[type];
			}
			else
			{
				list = new List<PiiProcessor.PiiProperty>();
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
				foreach (PropertyInfo propertyInfo in properties)
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(SuppressPiiAttribute), true);
					if (propertyInfo.PropertyType == typeof(string) && customAttributes.Length > 0)
					{
						SuppressPiiAttribute suppressPiiAttribute = (SuppressPiiAttribute)customAttributes[0];
						list.Add(new PiiProcessor.PiiProperty
						{
							PropertyInfo = propertyInfo,
							PiiDataType = suppressPiiAttribute.PiiDataType
						});
					}
				}
				this.piiPropertyDictionary.Add(type, list);
			}
			return list;
		}

		// Token: 0x0400272B RID: 10027
		private readonly Dictionary<Type, List<PiiProcessor.PiiProperty>> piiPropertyDictionary = new Dictionary<Type, List<PiiProcessor.PiiProperty>>();

		// Token: 0x020006A5 RID: 1701
		private class PiiProperty
		{
			// Token: 0x17001210 RID: 4624
			// (get) Token: 0x06003C4C RID: 15436 RVA: 0x00100FA7 File Offset: 0x000FF1A7
			// (set) Token: 0x06003C4D RID: 15437 RVA: 0x00100FAF File Offset: 0x000FF1AF
			public PropertyInfo PropertyInfo { get; set; }

			// Token: 0x17001211 RID: 4625
			// (get) Token: 0x06003C4E RID: 15438 RVA: 0x00100FB8 File Offset: 0x000FF1B8
			// (set) Token: 0x06003C4F RID: 15439 RVA: 0x00100FC0 File Offset: 0x000FF1C0
			public PiiDataType PiiDataType { get; set; }
		}
	}
}
