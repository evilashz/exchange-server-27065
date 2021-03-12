using System;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020003AD RID: 941
	public static class RemoteDomainCodeBehind
	{
		// Token: 0x06003188 RID: 12680 RVA: 0x00098CB4 File Offset: 0x00096EB4
		public static void GetSDOPostAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			if (table.Rows[0] == null)
			{
				return;
			}
			DataRow dataRow = table.Rows[0];
			string mimeCharset = table.Rows[0]["CharacterSet"].ToString();
			string nonMimeCharset = table.Rows[0]["NonMimeCharacterSet"].ToString();
			table.Rows[0]["CharacterSet"] = DomainContentConfig.CharacterSetList.First((DomainContentConfig.CharacterSetInfo aCharSet) => aCharSet.CharsetName.Equals(mimeCharset, StringComparison.OrdinalIgnoreCase)).CharsetDescription;
			table.Rows[0]["NonMimeCharacterSet"] = DomainContentConfig.CharacterSetList.First((DomainContentConfig.CharacterSetInfo aCharSet) => aCharSet.CharsetName.Equals(nonMimeCharset, StringComparison.OrdinalIgnoreCase)).CharsetDescription;
			string name = DomainContentConfigSchema.AllowedOOFType.Name;
			AllowedOOFType allowedOOFType = (AllowedOOFType)Enum.Parse(typeof(AllowedOOFType), table.Rows[0][name].ToString());
			AllowedOOFType allowedOOFType2 = allowedOOFType;
			switch (allowedOOFType2)
			{
			case AllowedOOFType.External:
				table.Rows[0][name] = Strings.RemoteDomainsAutomaticReplyExternal.ToString();
				return;
			case AllowedOOFType.ExternalLegacy:
				table.Rows[0][name] = Strings.RemoteDomainsAutomaticReplyExternalLegacy.ToString();
				return;
			default:
				if (allowedOOFType2 == AllowedOOFType.None)
				{
					table.Rows[0][name] = Strings.RemoteDomainsAutomaticReplyNone.ToString();
					return;
				}
				if (allowedOOFType2 != AllowedOOFType.InternalLegacy)
				{
					return;
				}
				table.Rows[0][name] = Strings.InternalInsteadOfInternalLegacy.ToString();
				return;
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x00098E74 File Offset: 0x00097074
		public static bool? ConvertTNEFEnabled(object webInput)
		{
			if (webInput == null)
			{
				return null;
			}
			bool value;
			if (!bool.TryParse(webInput.ToString(), out value))
			{
				return null;
			}
			return new bool?(value);
		}
	}
}
