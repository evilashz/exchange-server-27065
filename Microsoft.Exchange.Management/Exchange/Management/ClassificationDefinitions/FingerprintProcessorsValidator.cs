using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200086D RID: 2157
	internal sealed class FingerprintProcessorsValidator : IClassificationRuleCollectionValidator
	{
		// Token: 0x06004A7D RID: 19069 RVA: 0x00132D70 File Offset: 0x00130F70
		public void Validate(ValidationContext context, XDocument rulePackXDocument)
		{
			List<XElement> list = XmlProcessingUtils.GetFingerprintProcessorsInRulePackage(rulePackXDocument).ToList<XElement>();
			if (context != null && context.IsPayloadFingerprintsRuleCollection)
			{
				int num = (int)DataClassificationConfigSchema.MaxFingerprints.DefaultValue;
				if (context.DcValidationConfig != null)
				{
					num = context.DcValidationConfig.MaxFingerprints;
				}
				if (list.Count > num)
				{
					throw new ClassificationRuleCollectionFingerprintValidationException(Strings.ClassificationRuleCollectionFingerprintsExceedLimit(list.Count, num), null);
				}
			}
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
			List<string> list2 = new List<string>();
			foreach (XElement xelement in list)
			{
				byte[] array = null;
				try
				{
					array = Convert.FromBase64String(xelement.Value);
				}
				catch (FormatException)
				{
				}
				if (array == null || array.Length % 2 != 0)
				{
					list2.Add(xelement.Attribute("id").Value);
				}
				else
				{
					string item = string.Format("{0}_{1}", xelement.Attribute("threshold").Value, xelement.Attribute("shingleCount").Value);
					HashSet<string> hashSet = null;
					dictionary.TryGetValue(xelement.Value, out hashSet);
					if (hashSet == null)
					{
						hashSet = new HashSet<string>();
					}
					if (!hashSet.Contains(item))
					{
						hashSet.Add(item);
					}
					else
					{
						list2.Add(xelement.Attribute("id").Value);
					}
					dictionary[xelement.Value] = hashSet;
				}
			}
			if (list2.Count > 0)
			{
				throw new ClassificationRuleCollectionFingerprintValidationException(Strings.ClassificationRuleCollectionFingerprintValidationFailure(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list2)), null);
			}
		}
	}
}
