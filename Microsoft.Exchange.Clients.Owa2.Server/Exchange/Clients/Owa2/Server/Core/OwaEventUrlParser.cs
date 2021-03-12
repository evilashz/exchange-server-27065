using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E0 RID: 480
	internal sealed class OwaEventUrlParser : OwaEventParserBase
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x00040EA8 File Offset: 0x0003F0A8
		internal OwaEventUrlParser(OwaEventHandlerBase eventHandler) : base(eventHandler, 4)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00040EB4 File Offset: 0x0003F0B4
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
					this.ThrowParserException("Parameter name is empty.");
				}
				if (string.CompareOrdinal(key, "ns") != 0 && string.CompareOrdinal(key, "ev") != 0 && string.CompareOrdinal(key, "oeh2") != 0)
				{
					OwaEventParameterAttribute paramInfo = base.GetParamInfo(key);
					if (paramInfo.IsArray)
					{
						this.ThrowParserException("Arrays are not supported in GET requests");
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

		// Token: 0x06001104 RID: 4356 RVA: 0x00040F80 File Offset: 0x0003F180
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
