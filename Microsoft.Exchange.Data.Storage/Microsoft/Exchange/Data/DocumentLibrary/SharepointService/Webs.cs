using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary.SharepointService
{
	// Token: 0x020006F5 RID: 1781
	[DesignerCategory("code")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[WebServiceBinding(Name = "WebsSoap", Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
	[DebuggerStepThrough]
	internal class Webs : SoapHttpClientProtocol
	{
		// Token: 0x06004693 RID: 18067 RVA: 0x0012C9DC File Offset: 0x0012ABDC
		public Webs(string url)
		{
			this.Url = url + "/_vti_bin/webs.asmx";
			this.UseDefaultCredentials = true;
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x0012C9FC File Offset: 0x0012ABFC
		// (set) Token: 0x06004695 RID: 18069 RVA: 0x0012CA04 File Offset: 0x0012AC04
		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				base.Url = value;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06004696 RID: 18070 RVA: 0x0012CA0D File Offset: 0x0012AC0D
		// (set) Token: 0x06004697 RID: 18071 RVA: 0x0012CA15 File Offset: 0x0012AC15
		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
			}
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x0012CA20 File Offset: 0x0012AC20
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/WebUrlFromPageUrl", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string WebUrlFromPageUrl(string pageUrl)
		{
			object[] array = base.Invoke("WebUrlFromPageUrl", new object[]
			{
				pageUrl
			});
			return (string)array[0];
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x0012CA50 File Offset: 0x0012AC50
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetWebCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetWebCollection()
		{
			object[] array = base.Invoke("GetWebCollection", Array<object>.Empty);
			return (XmlNode)array[0];
		}
	}
}
