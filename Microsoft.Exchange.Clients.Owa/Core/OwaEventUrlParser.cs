using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000192 RID: 402
	internal sealed class OwaEventUrlParser : OwaEventParserBase
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0005D683 File Offset: 0x0005B883
		internal OwaEventUrlParser(OwaEventHandlerBase eventHandler) : base(eventHandler, 4)
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0005D690 File Offset: 0x0005B890
		protected override Hashtable ParseParameters()
		{
			NameValueCollection queryString = base.EventHandler.HttpContext.Request.QueryString;
			if (queryString.Count == 0)
			{
				return base.ParameterTable;
			}
			for (int i = 0; i < queryString.Count; i++)
			{
				string key = queryString.GetKey(i);
				if (string.IsNullOrEmpty(key))
				{
					this.ThrowParserException("Parameter name is empty. Url=" + base.EventHandler.HttpContext.Request.RawUrl);
				}
				if (string.CompareOrdinal(key, "ns") != 0 && string.CompareOrdinal(key, "ev") != 0 && string.CompareOrdinal(key, "oeh") != 0 && string.CompareOrdinal(key, "cpc") != 0 && string.CompareOrdinal(key, "calist") != 0 && string.CompareOrdinal(key, "pfmk") != 0 && string.CompareOrdinal(key, Globals.RealmParameter) != 0)
				{
					OwaEventParameterAttribute paramInfo = base.GetParamInfo(key);
					if (paramInfo.IsStruct || paramInfo.IsArray)
					{
						this.ThrowParserException("Structs and arrays are not supported in GET requests");
					}
					string text = queryString[i];
					if (text == null)
					{
						base.AddEmptyParameter(paramInfo);
					}
					else
					{
						base.AddSimpleTypeParameter(paramInfo, text);
					}
				}
			}
			return base.ParameterTable;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0005D7B8 File Offset: 0x0005B9B8
		protected override void ThrowParserException(string description)
		{
			throw new OwaInvalidRequestException(string.Format(CultureInfo.InvariantCulture, "Invalid request. Url: {0}. {1}.", new object[]
			{
				base.EventHandler.HttpContext.Request.RawUrl,
				(description != null) ? (" " + description) : string.Empty
			}), null, this);
		}
	}
}
