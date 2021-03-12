using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary.SharepointService
{
	// Token: 0x020006F4 RID: 1780
	[ClassAccessLevel(AccessLevel.Implementation)]
	[WebServiceBinding(Name = "ListsSoap", Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	internal class Lists : SoapHttpClientProtocol
	{
		// Token: 0x0600468F RID: 18063 RVA: 0x0012C917 File Offset: 0x0012AB17
		public Lists(string serverUrl)
		{
			base.Url = serverUrl + "/_vti_bin/Lists.asmx";
			base.Credentials = CredentialCache.DefaultCredentials;
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x0012C93C File Offset: 0x0012AB3C
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListCollection()
		{
			object[] array = base.Invoke("GetListCollection", Array<object>.Empty);
			return (XmlNode)array[0];
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x0012C964 File Offset: 0x0012AB64
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListAndView", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListAndView(string listName, string viewName)
		{
			object[] array = base.Invoke("GetListAndView", new object[]
			{
				listName,
				viewName
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x0012C998 File Offset: 0x0012AB98
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListItems", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListItems(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions)
		{
			object[] array = base.Invoke("GetListItems", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions
			});
			return (XmlNode)array[0];
		}
	}
}
