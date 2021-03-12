﻿using System;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200004D RID: 77
	internal class TransportConfigValidator : ConfigValidator
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00009F34 File Offset: 0x00008134
		public TransportConfigValidator(ReplicationTopology topology) : base(topology, "Transport Configuration")
		{
			base.SearchScope = SearchScope.Base;
			base.ConfigDirectoryPath = "CN=Transport Settings";
			base.LdapQuery = Schema.Query.QueryAll;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009F5F File Offset: 0x0000815F
		protected override string[] PayloadAttributes
		{
			get
			{
				return Schema.TransportConfig.PayloadAttributes;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009F68 File Offset: 0x00008168
		protected override bool CompareAttributes(ExSearchResultEntry edgeEntry, ExSearchResultEntry hubEntry, string[] copyAttributes)
		{
			int i = 0;
			while (i < copyAttributes.Length)
			{
				string text = copyAttributes[i];
				DirectoryAttribute attribute;
				bool flag = edgeEntry.Attributes.TryGetValue(text, out attribute);
				DirectoryAttribute attribute2;
				bool flag2 = hubEntry.Attributes.TryGetValue(text, out attribute2);
				if (flag == flag2)
				{
					if (flag)
					{
						if (string.Equals(text, ADAMTransportConfigContainerSchema.Flags.LdapDisplayName))
						{
							int num = TransportConfigValidator.ParseTransportFlagsFromDirectoryAttribute(attribute);
							int num2 = TransportConfigValidator.ParseTransportFlagsFromDirectoryAttribute(attribute2);
							num &= TransportConfigValidator.TransportSettingsSyncedFlags;
							num2 &= TransportConfigValidator.TransportSettingsSyncedFlags;
							if (num2 != num)
							{
								return false;
							}
						}
						else if (!base.CompareAttributeValues(edgeEntry.Attributes[text], hubEntry.Attributes[text]))
						{
							return false;
						}
					}
					i++;
					continue;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A030 File Offset: 0x00008230
		public static int ParseTransportFlagsFromDirectoryAttribute(DirectoryAttribute attribute)
		{
			int result;
			try
			{
				object[] values = attribute.GetValues(typeof(string));
				if (values == null || values.Length == 0)
				{
					throw new ExDirectoryException(new ArgumentNullException("TransportConfigContainerSchema.Flags attribute is null"));
				}
				result = (int)ADValueConvertor.GetValueFromDirectoryAttributeValues(ADAMTransportConfigContainerSchema.Flags, values);
			}
			catch (DataValidationException e)
			{
				throw new ExDirectoryException(e);
			}
			return result;
		}

		// Token: 0x04000152 RID: 338
		internal static int TransportSettingsSyncedFlags = 512;
	}
}
