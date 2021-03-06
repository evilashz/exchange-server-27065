using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003B8 RID: 952
	internal static class OtherAttribtuesUtils
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x00098D8C File Offset: 0x00096F8C
		internal static bool TryGetVersionedItemFromOtherAttributes(IOtherAttributes element, string name, IList<IVersionedItem> versionedItems)
		{
			Dictionary<string, string> ids = new Dictionary<string, string>();
			Dictionary<string, string> versions = new Dictionary<string, string>();
			foreach (var <>f__AnonymousType in from a in element.OtherAttributes
			let match = OtherAttribtuesUtils.attributeExtractorRE.Match(a.Name)
			where match.Success
			let baseName = match.Groups["name"].Value
			let fullName = baseName + match.Groups["index"].Value
			let isId = match.Groups["isId"].Success
			where baseName == name
			select new
			{
				Name = fullName,
				IsId = isId,
				Value = a.Value
			})
			{
				Dictionary<string, string> dictionary = <>f__AnonymousType.IsId ? ids : versions;
				if (dictionary.ContainsKey(<>f__AnonymousType.Name))
				{
					return false;
				}
				dictionary.Add(<>f__AnonymousType.Name, <>f__AnonymousType.Value);
			}
			if (ids.Keys.Any((string k) => !versions.ContainsKey(k)) || versions.Keys.Any((string k) => !ids.ContainsKey(k)))
			{
				return false;
			}
			foreach (KeyValuePair<string, string> keyValuePair in ids)
			{
				long dateData;
				if (!long.TryParse(versions[keyValuePair.Key], out dateData))
				{
					return false;
				}
				DateTime version;
				try
				{
					version = DateTime.FromBinary(dateData);
				}
				catch (ArgumentException)
				{
					return false;
				}
				versionedItems.Add(new VersionedItem(keyValuePair.Value, version));
			}
			return true;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00098FF4 File Offset: 0x000971F4
		internal static void ApplyVersionedItems(XmlElement element, string name, IEnumerable<IVersionedItem> versionedItems)
		{
			int num = 0;
			foreach (IVersionedItem versionedItem in versionedItems)
			{
				element.SetAttribute(name + num.ToString() + "id", versionedItem.ID);
				element.SetAttribute(name + num.ToString() + "version", versionedItem.Version.ToBinary().ToString());
				num++;
			}
		}

		// Token: 0x04001191 RID: 4497
		private static Regex attributeExtractorRE = new Regex("^(?<name>[A-Za-z]+)(?<index>\\d+)((?<isId>id)|(?<isVersion>version))$", RegexOptions.Compiled);
	}
}
