using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000211 RID: 529
	public sealed class PreFormActionResponse
	{
		// Token: 0x060011D6 RID: 4566 RVA: 0x0006BFC6 File Offset: 0x0006A1C6
		public PreFormActionResponse()
		{
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0006BFD0 File Offset: 0x0006A1D0
		public PreFormActionResponse(HttpRequest request, params string[] parameters)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (parameters == null || parameters.Length == 0)
			{
				throw new ArgumentException("parameters may not be null or empty array");
			}
			foreach (string name in parameters)
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(request, name, false);
				if (queryStringParameter != null)
				{
					this.AddParameter(name, queryStringParameter);
				}
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0006C02C File Offset: 0x0006A22C
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0006C034 File Offset: 0x0006A234
		public ApplicationElement ApplicationElement
		{
			get
			{
				return this.applicationElement;
			}
			set
			{
				this.applicationElement = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0006C03D File Offset: 0x0006A23D
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0006C045 File Offset: 0x0006A245
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0006C04E File Offset: 0x0006A24E
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0006C056 File Offset: 0x0006A256
		public string State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0006C05F File Offset: 0x0006A25F
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0006C067 File Offset: 0x0006A267
		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0006C070 File Offset: 0x0006A270
		public string GetUrl()
		{
			return this.GetUrl(true);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0006C07C File Offset: 0x0006A27C
		public string GetUrl(bool needApplicationElement)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (needApplicationElement && this.applicationElement != ApplicationElement.NotSet)
			{
				PreFormActionResponse.AppendUrlParameter("ae", FormsRegistry.ApplicationElementParser.GetString((int)this.applicationElement), stringBuilder);
			}
			PreFormActionResponse.AppendUrlParameter("t", this.type, stringBuilder);
			PreFormActionResponse.AppendUrlParameter("a", this.action, stringBuilder);
			PreFormActionResponse.AppendUrlParameter("s", this.state, stringBuilder);
			if (this.parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.parameters)
				{
					PreFormActionResponse.AppendUrlParameter(keyValuePair.Key, keyValuePair.Value, stringBuilder);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0006C14C File Offset: 0x0006A34C
		public void AddParameter(string name, string value)
		{
			if (this.parameters == null)
			{
				this.parameters = new Dictionary<string, string>();
			}
			this.parameters.Add(name, value);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0006C16E File Offset: 0x0006A36E
		private static void AppendUrlParameter(string name, string value, StringBuilder builder)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (builder.Length > 0)
				{
					builder.Append('&');
				}
				builder.Append(name);
				builder.Append('=');
				builder.Append(Utilities.UrlEncode(value));
			}
		}

		// Token: 0x04000C23 RID: 3107
		private ApplicationElement applicationElement;

		// Token: 0x04000C24 RID: 3108
		private string type;

		// Token: 0x04000C25 RID: 3109
		private string state;

		// Token: 0x04000C26 RID: 3110
		private string action;

		// Token: 0x04000C27 RID: 3111
		private Dictionary<string, string> parameters;
	}
}
