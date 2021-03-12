using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.SharePoint.SOAP
{
	// Token: 0x0200092F RID: 2351
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[WebServiceBinding(Name = "ListsSoap", Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
	internal class Lists : WsAsyncProxyWrapper
	{
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x0600327A RID: 12922 RVA: 0x0007C2F0 File Offset: 0x0007A4F0
		// (remove) Token: 0x0600327B RID: 12923 RVA: 0x0007C328 File Offset: 0x0007A528
		public event GetListCompletedEventHandler GetListCompleted;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x0600327C RID: 12924 RVA: 0x0007C360 File Offset: 0x0007A560
		// (remove) Token: 0x0600327D RID: 12925 RVA: 0x0007C398 File Offset: 0x0007A598
		public event GetListAndViewCompletedEventHandler GetListAndViewCompleted;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x0600327E RID: 12926 RVA: 0x0007C3D0 File Offset: 0x0007A5D0
		// (remove) Token: 0x0600327F RID: 12927 RVA: 0x0007C408 File Offset: 0x0007A608
		public event DeleteListCompletedEventHandler DeleteListCompleted;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06003280 RID: 12928 RVA: 0x0007C440 File Offset: 0x0007A640
		// (remove) Token: 0x06003281 RID: 12929 RVA: 0x0007C478 File Offset: 0x0007A678
		public event AddListCompletedEventHandler AddListCompleted;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06003282 RID: 12930 RVA: 0x0007C4B0 File Offset: 0x0007A6B0
		// (remove) Token: 0x06003283 RID: 12931 RVA: 0x0007C4E8 File Offset: 0x0007A6E8
		public event AddListFromFeatureCompletedEventHandler AddListFromFeatureCompleted;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06003284 RID: 12932 RVA: 0x0007C520 File Offset: 0x0007A720
		// (remove) Token: 0x06003285 RID: 12933 RVA: 0x0007C558 File Offset: 0x0007A758
		public event UpdateListCompletedEventHandler UpdateListCompleted;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06003286 RID: 12934 RVA: 0x0007C590 File Offset: 0x0007A790
		// (remove) Token: 0x06003287 RID: 12935 RVA: 0x0007C5C8 File Offset: 0x0007A7C8
		public event GetListCollectionCompletedEventHandler GetListCollectionCompleted;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06003288 RID: 12936 RVA: 0x0007C600 File Offset: 0x0007A800
		// (remove) Token: 0x06003289 RID: 12937 RVA: 0x0007C638 File Offset: 0x0007A838
		public event GetListItemsCompletedEventHandler GetListItemsCompleted;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x0600328A RID: 12938 RVA: 0x0007C670 File Offset: 0x0007A870
		// (remove) Token: 0x0600328B RID: 12939 RVA: 0x0007C6A8 File Offset: 0x0007A8A8
		public event GetListItemChangesCompletedEventHandler GetListItemChangesCompleted;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x0600328C RID: 12940 RVA: 0x0007C6E0 File Offset: 0x0007A8E0
		// (remove) Token: 0x0600328D RID: 12941 RVA: 0x0007C718 File Offset: 0x0007A918
		public event GetListItemChangesWithKnowledgeCompletedEventHandler GetListItemChangesWithKnowledgeCompleted;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x0600328E RID: 12942 RVA: 0x0007C750 File Offset: 0x0007A950
		// (remove) Token: 0x0600328F RID: 12943 RVA: 0x0007C788 File Offset: 0x0007A988
		public event GetListItemChangesSinceTokenCompletedEventHandler GetListItemChangesSinceTokenCompleted;

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06003290 RID: 12944 RVA: 0x0007C7C0 File Offset: 0x0007A9C0
		// (remove) Token: 0x06003291 RID: 12945 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		public event UpdateListItemsCompletedEventHandler UpdateListItemsCompleted;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06003292 RID: 12946 RVA: 0x0007C830 File Offset: 0x0007AA30
		// (remove) Token: 0x06003293 RID: 12947 RVA: 0x0007C868 File Offset: 0x0007AA68
		public event UpdateListItemsWithKnowledgeCompletedEventHandler UpdateListItemsWithKnowledgeCompleted;

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06003294 RID: 12948 RVA: 0x0007C8A0 File Offset: 0x0007AAA0
		// (remove) Token: 0x06003295 RID: 12949 RVA: 0x0007C8D8 File Offset: 0x0007AAD8
		public event AddDiscussionBoardItemCompletedEventHandler AddDiscussionBoardItemCompleted;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06003296 RID: 12950 RVA: 0x0007C910 File Offset: 0x0007AB10
		// (remove) Token: 0x06003297 RID: 12951 RVA: 0x0007C948 File Offset: 0x0007AB48
		public event AddWikiPageCompletedEventHandler AddWikiPageCompleted;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06003298 RID: 12952 RVA: 0x0007C980 File Offset: 0x0007AB80
		// (remove) Token: 0x06003299 RID: 12953 RVA: 0x0007C9B8 File Offset: 0x0007ABB8
		public event GetVersionCollectionCompletedEventHandler GetVersionCollectionCompleted;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600329A RID: 12954 RVA: 0x0007C9F0 File Offset: 0x0007ABF0
		// (remove) Token: 0x0600329B RID: 12955 RVA: 0x0007CA28 File Offset: 0x0007AC28
		public event AddAttachmentCompletedEventHandler AddAttachmentCompleted;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x0600329C RID: 12956 RVA: 0x0007CA60 File Offset: 0x0007AC60
		// (remove) Token: 0x0600329D RID: 12957 RVA: 0x0007CA98 File Offset: 0x0007AC98
		public event GetAttachmentCollectionCompletedEventHandler GetAttachmentCollectionCompleted;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600329E RID: 12958 RVA: 0x0007CAD0 File Offset: 0x0007ACD0
		// (remove) Token: 0x0600329F RID: 12959 RVA: 0x0007CB08 File Offset: 0x0007AD08
		public event DeleteAttachmentCompletedEventHandler DeleteAttachmentCompleted;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x060032A0 RID: 12960 RVA: 0x0007CB40 File Offset: 0x0007AD40
		// (remove) Token: 0x060032A1 RID: 12961 RVA: 0x0007CB78 File Offset: 0x0007AD78
		public event CheckOutFileCompletedEventHandler CheckOutFileCompleted;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x060032A2 RID: 12962 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
		// (remove) Token: 0x060032A3 RID: 12963 RVA: 0x0007CBE8 File Offset: 0x0007ADE8
		public event UndoCheckOutCompletedEventHandler UndoCheckOutCompleted;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060032A4 RID: 12964 RVA: 0x0007CC20 File Offset: 0x0007AE20
		// (remove) Token: 0x060032A5 RID: 12965 RVA: 0x0007CC58 File Offset: 0x0007AE58
		public event CheckInFileCompletedEventHandler CheckInFileCompleted;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060032A6 RID: 12966 RVA: 0x0007CC90 File Offset: 0x0007AE90
		// (remove) Token: 0x060032A7 RID: 12967 RVA: 0x0007CCC8 File Offset: 0x0007AEC8
		public event GetListContentTypesCompletedEventHandler GetListContentTypesCompleted;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060032A8 RID: 12968 RVA: 0x0007CD00 File Offset: 0x0007AF00
		// (remove) Token: 0x060032A9 RID: 12969 RVA: 0x0007CD38 File Offset: 0x0007AF38
		public event GetListContentTypesAndPropertiesCompletedEventHandler GetListContentTypesAndPropertiesCompleted;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x060032AA RID: 12970 RVA: 0x0007CD70 File Offset: 0x0007AF70
		// (remove) Token: 0x060032AB RID: 12971 RVA: 0x0007CDA8 File Offset: 0x0007AFA8
		public event GetListContentTypeCompletedEventHandler GetListContentTypeCompleted;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x060032AC RID: 12972 RVA: 0x0007CDE0 File Offset: 0x0007AFE0
		// (remove) Token: 0x060032AD RID: 12973 RVA: 0x0007CE18 File Offset: 0x0007B018
		public event CreateContentTypeCompletedEventHandler CreateContentTypeCompleted;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x060032AE RID: 12974 RVA: 0x0007CE50 File Offset: 0x0007B050
		// (remove) Token: 0x060032AF RID: 12975 RVA: 0x0007CE88 File Offset: 0x0007B088
		public event UpdateContentTypeCompletedEventHandler UpdateContentTypeCompleted;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060032B0 RID: 12976 RVA: 0x0007CEC0 File Offset: 0x0007B0C0
		// (remove) Token: 0x060032B1 RID: 12977 RVA: 0x0007CEF8 File Offset: 0x0007B0F8
		public event DeleteContentTypeCompletedEventHandler DeleteContentTypeCompleted;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060032B2 RID: 12978 RVA: 0x0007CF30 File Offset: 0x0007B130
		// (remove) Token: 0x060032B3 RID: 12979 RVA: 0x0007CF68 File Offset: 0x0007B168
		public event UpdateContentTypeXmlDocumentCompletedEventHandler UpdateContentTypeXmlDocumentCompleted;

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060032B4 RID: 12980 RVA: 0x0007CFA0 File Offset: 0x0007B1A0
		// (remove) Token: 0x060032B5 RID: 12981 RVA: 0x0007CFD8 File Offset: 0x0007B1D8
		public event UpdateContentTypesXmlDocumentCompletedEventHandler UpdateContentTypesXmlDocumentCompleted;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x060032B6 RID: 12982 RVA: 0x0007D010 File Offset: 0x0007B210
		// (remove) Token: 0x060032B7 RID: 12983 RVA: 0x0007D048 File Offset: 0x0007B248
		public event DeleteContentTypeXmlDocumentCompletedEventHandler DeleteContentTypeXmlDocumentCompleted;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x060032B8 RID: 12984 RVA: 0x0007D080 File Offset: 0x0007B280
		// (remove) Token: 0x060032B9 RID: 12985 RVA: 0x0007D0B8 File Offset: 0x0007B2B8
		public event ApplyContentTypeToListCompletedEventHandler ApplyContentTypeToListCompleted;

		// Token: 0x060032BA RID: 12986 RVA: 0x0007D0F0 File Offset: 0x0007B2F0
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetList(string listName)
		{
			object[] array = base.Invoke("GetList", new object[]
			{
				listName
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x0007D120 File Offset: 0x0007B320
		public IAsyncResult BeginGetList(string listName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetList", new object[]
			{
				listName
			}, callback, asyncState);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0007D148 File Offset: 0x0007B348
		public XmlNode EndGetList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0007D165 File Offset: 0x0007B365
		public void GetListAsync(string listName)
		{
			this.GetListAsync(listName, null);
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x0007D170 File Offset: 0x0007B370
		public void GetListAsync(string listName, object userState)
		{
			if (this.GetListOperationCompleted == null)
			{
				this.GetListOperationCompleted = new SendOrPostCallback(this.OnGetListOperationCompleted);
			}
			base.InvokeAsync("GetList", new object[]
			{
				listName
			}, this.GetListOperationCompleted, userState);
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x0007D1B8 File Offset: 0x0007B3B8
		private void OnGetListOperationCompleted(object arg)
		{
			if (this.GetListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListCompleted(this, new GetListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x0007D200 File Offset: 0x0007B400
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

		// Token: 0x060032C1 RID: 12993 RVA: 0x0007D234 File Offset: 0x0007B434
		public IAsyncResult BeginGetListAndView(string listName, string viewName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListAndView", new object[]
			{
				listName,
				viewName
			}, callback, asyncState);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x0007D260 File Offset: 0x0007B460
		public XmlNode EndGetListAndView(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x0007D27D File Offset: 0x0007B47D
		public void GetListAndViewAsync(string listName, string viewName)
		{
			this.GetListAndViewAsync(listName, viewName, null);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0007D288 File Offset: 0x0007B488
		public void GetListAndViewAsync(string listName, string viewName, object userState)
		{
			if (this.GetListAndViewOperationCompleted == null)
			{
				this.GetListAndViewOperationCompleted = new SendOrPostCallback(this.OnGetListAndViewOperationCompleted);
			}
			base.InvokeAsync("GetListAndView", new object[]
			{
				listName,
				viewName
			}, this.GetListAndViewOperationCompleted, userState);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x0007D2D4 File Offset: 0x0007B4D4
		private void OnGetListAndViewOperationCompleted(object arg)
		{
			if (this.GetListAndViewCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListAndViewCompleted(this, new GetListAndViewCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x0007D31C File Offset: 0x0007B51C
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/DeleteList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteList(string listName)
		{
			base.Invoke("DeleteList", new object[]
			{
				listName
			});
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0007D344 File Offset: 0x0007B544
		public IAsyncResult BeginDeleteList(string listName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteList", new object[]
			{
				listName
			}, callback, asyncState);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0007D36A File Offset: 0x0007B56A
		public void EndDeleteList(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x0007D374 File Offset: 0x0007B574
		public void DeleteListAsync(string listName)
		{
			this.DeleteListAsync(listName, null);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x0007D380 File Offset: 0x0007B580
		public void DeleteListAsync(string listName, object userState)
		{
			if (this.DeleteListOperationCompleted == null)
			{
				this.DeleteListOperationCompleted = new SendOrPostCallback(this.OnDeleteListOperationCompleted);
			}
			base.InvokeAsync("DeleteList", new object[]
			{
				listName
			}, this.DeleteListOperationCompleted, userState);
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x0007D3C8 File Offset: 0x0007B5C8
		private void OnDeleteListOperationCompleted(object arg)
		{
			if (this.DeleteListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteListCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x0007D408 File Offset: 0x0007B608
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/AddList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode AddList(string listName, string description, int templateID)
		{
			object[] array = base.Invoke("AddList", new object[]
			{
				listName,
				description,
				templateID
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x0007D444 File Offset: 0x0007B644
		public IAsyncResult BeginAddList(string listName, string description, int templateID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddList", new object[]
			{
				listName,
				description,
				templateID
			}, callback, asyncState);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0007D47C File Offset: 0x0007B67C
		public XmlNode EndAddList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x0007D499 File Offset: 0x0007B699
		public void AddListAsync(string listName, string description, int templateID)
		{
			this.AddListAsync(listName, description, templateID, null);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0007D4A8 File Offset: 0x0007B6A8
		public void AddListAsync(string listName, string description, int templateID, object userState)
		{
			if (this.AddListOperationCompleted == null)
			{
				this.AddListOperationCompleted = new SendOrPostCallback(this.OnAddListOperationCompleted);
			}
			base.InvokeAsync("AddList", new object[]
			{
				listName,
				description,
				templateID
			}, this.AddListOperationCompleted, userState);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0007D4FC File Offset: 0x0007B6FC
		private void OnAddListOperationCompleted(object arg)
		{
			if (this.AddListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddListCompleted(this, new AddListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0007D544 File Offset: 0x0007B744
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/AddListFromFeature", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode AddListFromFeature(string listName, string description, Guid featureID, int templateID)
		{
			object[] array = base.Invoke("AddListFromFeature", new object[]
			{
				listName,
				description,
				featureID,
				templateID
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x0007D588 File Offset: 0x0007B788
		public IAsyncResult BeginAddListFromFeature(string listName, string description, Guid featureID, int templateID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddListFromFeature", new object[]
			{
				listName,
				description,
				featureID,
				templateID
			}, callback, asyncState);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0007D5C8 File Offset: 0x0007B7C8
		public XmlNode EndAddListFromFeature(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0007D5E5 File Offset: 0x0007B7E5
		public void AddListFromFeatureAsync(string listName, string description, Guid featureID, int templateID)
		{
			this.AddListFromFeatureAsync(listName, description, featureID, templateID, null);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x0007D5F4 File Offset: 0x0007B7F4
		public void AddListFromFeatureAsync(string listName, string description, Guid featureID, int templateID, object userState)
		{
			if (this.AddListFromFeatureOperationCompleted == null)
			{
				this.AddListFromFeatureOperationCompleted = new SendOrPostCallback(this.OnAddListFromFeatureOperationCompleted);
			}
			base.InvokeAsync("AddListFromFeature", new object[]
			{
				listName,
				description,
				featureID,
				templateID
			}, this.AddListFromFeatureOperationCompleted, userState);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x0007D654 File Offset: 0x0007B854
		private void OnAddListFromFeatureOperationCompleted(object arg)
		{
			if (this.AddListFromFeatureCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddListFromFeatureCompleted(this, new AddListFromFeatureCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x0007D69C File Offset: 0x0007B89C
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UpdateList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UpdateList(string listName, XmlNode listProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string listVersion)
		{
			object[] array = base.Invoke("UpdateList", new object[]
			{
				listName,
				listProperties,
				newFields,
				updateFields,
				deleteFields,
				listVersion
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x0007D6E0 File Offset: 0x0007B8E0
		public IAsyncResult BeginUpdateList(string listName, XmlNode listProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string listVersion, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateList", new object[]
			{
				listName,
				listProperties,
				newFields,
				updateFields,
				deleteFields,
				listVersion
			}, callback, asyncState);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x0007D720 File Offset: 0x0007B920
		public XmlNode EndUpdateList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x0007D73D File Offset: 0x0007B93D
		public void UpdateListAsync(string listName, XmlNode listProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string listVersion)
		{
			this.UpdateListAsync(listName, listProperties, newFields, updateFields, deleteFields, listVersion, null);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0007D750 File Offset: 0x0007B950
		public void UpdateListAsync(string listName, XmlNode listProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string listVersion, object userState)
		{
			if (this.UpdateListOperationCompleted == null)
			{
				this.UpdateListOperationCompleted = new SendOrPostCallback(this.OnUpdateListOperationCompleted);
			}
			base.InvokeAsync("UpdateList", new object[]
			{
				listName,
				listProperties,
				newFields,
				updateFields,
				deleteFields,
				listVersion
			}, this.UpdateListOperationCompleted, userState);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x0007D7B0 File Offset: 0x0007B9B0
		private void OnUpdateListOperationCompleted(object arg)
		{
			if (this.UpdateListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateListCompleted(this, new UpdateListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x0007D7F8 File Offset: 0x0007B9F8
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListCollection()
		{
			object[] array = base.Invoke("GetListCollection", new object[0]);
			return (XmlNode)array[0];
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x0007D81F File Offset: 0x0007BA1F
		public IAsyncResult BeginGetListCollection(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListCollection", new object[0], callback, asyncState);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0007D834 File Offset: 0x0007BA34
		public XmlNode EndGetListCollection(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0007D851 File Offset: 0x0007BA51
		public void GetListCollectionAsync()
		{
			this.GetListCollectionAsync(null);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x0007D85A File Offset: 0x0007BA5A
		public void GetListCollectionAsync(object userState)
		{
			if (this.GetListCollectionOperationCompleted == null)
			{
				this.GetListCollectionOperationCompleted = new SendOrPostCallback(this.OnGetListCollectionOperationCompleted);
			}
			base.InvokeAsync("GetListCollection", new object[0], this.GetListCollectionOperationCompleted, userState);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x0007D890 File Offset: 0x0007BA90
		private void OnGetListCollectionOperationCompleted(object arg)
		{
			if (this.GetListCollectionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListCollectionCompleted(this, new GetListCollectionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x0007D8D8 File Offset: 0x0007BAD8
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListItems", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListItems(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string webID)
		{
			object[] array = base.Invoke("GetListItems", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				webID
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0007D924 File Offset: 0x0007BB24
		public IAsyncResult BeginGetListItems(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string webID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListItems", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				webID
			}, callback, asyncState);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0007D968 File Offset: 0x0007BB68
		public XmlNode EndGetListItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0007D988 File Offset: 0x0007BB88
		public void GetListItemsAsync(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string webID)
		{
			this.GetListItemsAsync(listName, viewName, query, viewFields, rowLimit, queryOptions, webID, null);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x0007D9A8 File Offset: 0x0007BBA8
		public void GetListItemsAsync(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string webID, object userState)
		{
			if (this.GetListItemsOperationCompleted == null)
			{
				this.GetListItemsOperationCompleted = new SendOrPostCallback(this.OnGetListItemsOperationCompleted);
			}
			base.InvokeAsync("GetListItems", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				webID
			}, this.GetListItemsOperationCompleted, userState);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x0007DA0C File Offset: 0x0007BC0C
		private void OnGetListItemsOperationCompleted(object arg)
		{
			if (this.GetListItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListItemsCompleted(this, new GetListItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x0007DA54 File Offset: 0x0007BC54
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListItemChanges", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListItemChanges(string listName, XmlNode viewFields, string since, XmlNode contains)
		{
			object[] array = base.Invoke("GetListItemChanges", new object[]
			{
				listName,
				viewFields,
				since,
				contains
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x0007DA90 File Offset: 0x0007BC90
		public IAsyncResult BeginGetListItemChanges(string listName, XmlNode viewFields, string since, XmlNode contains, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListItemChanges", new object[]
			{
				listName,
				viewFields,
				since,
				contains
			}, callback, asyncState);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x0007DAC8 File Offset: 0x0007BCC8
		public XmlNode EndGetListItemChanges(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0007DAE5 File Offset: 0x0007BCE5
		public void GetListItemChangesAsync(string listName, XmlNode viewFields, string since, XmlNode contains)
		{
			this.GetListItemChangesAsync(listName, viewFields, since, contains, null);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0007DAF4 File Offset: 0x0007BCF4
		public void GetListItemChangesAsync(string listName, XmlNode viewFields, string since, XmlNode contains, object userState)
		{
			if (this.GetListItemChangesOperationCompleted == null)
			{
				this.GetListItemChangesOperationCompleted = new SendOrPostCallback(this.OnGetListItemChangesOperationCompleted);
			}
			base.InvokeAsync("GetListItemChanges", new object[]
			{
				listName,
				viewFields,
				since,
				contains
			}, this.GetListItemChangesOperationCompleted, userState);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x0007DB48 File Offset: 0x0007BD48
		private void OnGetListItemChangesOperationCompleted(object arg)
		{
			if (this.GetListItemChangesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListItemChangesCompleted(this, new GetListItemChangesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x0007DB90 File Offset: 0x0007BD90
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListItemChangesWithKnowledge", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListItemChangesWithKnowledge(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string syncScope, XmlNode knowledge, XmlNode contains)
		{
			object[] array = base.Invoke("GetListItemChangesWithKnowledge", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				syncScope,
				knowledge,
				contains
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x0007DBE4 File Offset: 0x0007BDE4
		public IAsyncResult BeginGetListItemChangesWithKnowledge(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string syncScope, XmlNode knowledge, XmlNode contains, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListItemChangesWithKnowledge", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				syncScope,
				knowledge,
				contains
			}, callback, asyncState);
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x0007DC34 File Offset: 0x0007BE34
		public XmlNode EndGetListItemChangesWithKnowledge(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x0007DC54 File Offset: 0x0007BE54
		public void GetListItemChangesWithKnowledgeAsync(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string syncScope, XmlNode knowledge, XmlNode contains)
		{
			this.GetListItemChangesWithKnowledgeAsync(listName, viewName, query, viewFields, rowLimit, queryOptions, syncScope, knowledge, contains, null);
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x0007DC78 File Offset: 0x0007BE78
		public void GetListItemChangesWithKnowledgeAsync(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string syncScope, XmlNode knowledge, XmlNode contains, object userState)
		{
			if (this.GetListItemChangesWithKnowledgeOperationCompleted == null)
			{
				this.GetListItemChangesWithKnowledgeOperationCompleted = new SendOrPostCallback(this.OnGetListItemChangesWithKnowledgeOperationCompleted);
			}
			base.InvokeAsync("GetListItemChangesWithKnowledge", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				syncScope,
				knowledge,
				contains
			}, this.GetListItemChangesWithKnowledgeOperationCompleted, userState);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x0007DCE8 File Offset: 0x0007BEE8
		private void OnGetListItemChangesWithKnowledgeOperationCompleted(object arg)
		{
			if (this.GetListItemChangesWithKnowledgeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListItemChangesWithKnowledgeCompleted(this, new GetListItemChangesWithKnowledgeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0007DD30 File Offset: 0x0007BF30
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListItemChangesSinceToken", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListItemChangesSinceToken(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string changeToken, XmlNode contains)
		{
			object[] array = base.Invoke("GetListItemChangesSinceToken", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				changeToken,
				contains
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x0007DD80 File Offset: 0x0007BF80
		public IAsyncResult BeginGetListItemChangesSinceToken(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string changeToken, XmlNode contains, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListItemChangesSinceToken", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				changeToken,
				contains
			}, callback, asyncState);
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x0007DDCC File Offset: 0x0007BFCC
		public XmlNode EndGetListItemChangesSinceToken(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x0007DDEC File Offset: 0x0007BFEC
		public void GetListItemChangesSinceTokenAsync(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string changeToken, XmlNode contains)
		{
			this.GetListItemChangesSinceTokenAsync(listName, viewName, query, viewFields, rowLimit, queryOptions, changeToken, contains, null);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x0007DE10 File Offset: 0x0007C010
		public void GetListItemChangesSinceTokenAsync(string listName, string viewName, XmlNode query, XmlNode viewFields, string rowLimit, XmlNode queryOptions, string changeToken, XmlNode contains, object userState)
		{
			if (this.GetListItemChangesSinceTokenOperationCompleted == null)
			{
				this.GetListItemChangesSinceTokenOperationCompleted = new SendOrPostCallback(this.OnGetListItemChangesSinceTokenOperationCompleted);
			}
			base.InvokeAsync("GetListItemChangesSinceToken", new object[]
			{
				listName,
				viewName,
				query,
				viewFields,
				rowLimit,
				queryOptions,
				changeToken,
				contains
			}, this.GetListItemChangesSinceTokenOperationCompleted, userState);
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x0007DE78 File Offset: 0x0007C078
		private void OnGetListItemChangesSinceTokenOperationCompleted(object arg)
		{
			if (this.GetListItemChangesSinceTokenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListItemChangesSinceTokenCompleted(this, new GetListItemChangesSinceTokenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x0007DEC0 File Offset: 0x0007C0C0
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UpdateListItems", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UpdateListItems(string listName, XmlNode updates)
		{
			object[] array = base.Invoke("UpdateListItems", new object[]
			{
				listName,
				updates
			});
			return (XmlNode)array[0];
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x0007DEF4 File Offset: 0x0007C0F4
		public IAsyncResult BeginUpdateListItems(string listName, XmlNode updates, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateListItems", new object[]
			{
				listName,
				updates
			}, callback, asyncState);
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x0007DF20 File Offset: 0x0007C120
		public XmlNode EndUpdateListItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x0007DF3D File Offset: 0x0007C13D
		public void UpdateListItemsAsync(string listName, XmlNode updates)
		{
			this.UpdateListItemsAsync(listName, updates, null);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x0007DF48 File Offset: 0x0007C148
		public void UpdateListItemsAsync(string listName, XmlNode updates, object userState)
		{
			if (this.UpdateListItemsOperationCompleted == null)
			{
				this.UpdateListItemsOperationCompleted = new SendOrPostCallback(this.OnUpdateListItemsOperationCompleted);
			}
			base.InvokeAsync("UpdateListItems", new object[]
			{
				listName,
				updates
			}, this.UpdateListItemsOperationCompleted, userState);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x0007DF94 File Offset: 0x0007C194
		private void OnUpdateListItemsOperationCompleted(object arg)
		{
			if (this.UpdateListItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateListItemsCompleted(this, new UpdateListItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x0007DFDC File Offset: 0x0007C1DC
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UpdateListItemsWithKnowledge", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UpdateListItemsWithKnowledge(string listName, XmlNode updates, string syncScope, XmlNode knowledge)
		{
			object[] array = base.Invoke("UpdateListItemsWithKnowledge", new object[]
			{
				listName,
				updates,
				syncScope,
				knowledge
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x0007E018 File Offset: 0x0007C218
		public IAsyncResult BeginUpdateListItemsWithKnowledge(string listName, XmlNode updates, string syncScope, XmlNode knowledge, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateListItemsWithKnowledge", new object[]
			{
				listName,
				updates,
				syncScope,
				knowledge
			}, callback, asyncState);
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x0007E050 File Offset: 0x0007C250
		public XmlNode EndUpdateListItemsWithKnowledge(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x0007E06D File Offset: 0x0007C26D
		public void UpdateListItemsWithKnowledgeAsync(string listName, XmlNode updates, string syncScope, XmlNode knowledge)
		{
			this.UpdateListItemsWithKnowledgeAsync(listName, updates, syncScope, knowledge, null);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x0007E07C File Offset: 0x0007C27C
		public void UpdateListItemsWithKnowledgeAsync(string listName, XmlNode updates, string syncScope, XmlNode knowledge, object userState)
		{
			if (this.UpdateListItemsWithKnowledgeOperationCompleted == null)
			{
				this.UpdateListItemsWithKnowledgeOperationCompleted = new SendOrPostCallback(this.OnUpdateListItemsWithKnowledgeOperationCompleted);
			}
			base.InvokeAsync("UpdateListItemsWithKnowledge", new object[]
			{
				listName,
				updates,
				syncScope,
				knowledge
			}, this.UpdateListItemsWithKnowledgeOperationCompleted, userState);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x0007E0D0 File Offset: 0x0007C2D0
		private void OnUpdateListItemsWithKnowledgeOperationCompleted(object arg)
		{
			if (this.UpdateListItemsWithKnowledgeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateListItemsWithKnowledgeCompleted(this, new UpdateListItemsWithKnowledgeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x0007E118 File Offset: 0x0007C318
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/AddDiscussionBoardItem", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode AddDiscussionBoardItem(string listName, [XmlElement(DataType = "base64Binary")] byte[] message)
		{
			object[] array = base.Invoke("AddDiscussionBoardItem", new object[]
			{
				listName,
				message
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x0007E14C File Offset: 0x0007C34C
		public IAsyncResult BeginAddDiscussionBoardItem(string listName, byte[] message, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddDiscussionBoardItem", new object[]
			{
				listName,
				message
			}, callback, asyncState);
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x0007E178 File Offset: 0x0007C378
		public XmlNode EndAddDiscussionBoardItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x0007E195 File Offset: 0x0007C395
		public void AddDiscussionBoardItemAsync(string listName, byte[] message)
		{
			this.AddDiscussionBoardItemAsync(listName, message, null);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x0007E1A0 File Offset: 0x0007C3A0
		public void AddDiscussionBoardItemAsync(string listName, byte[] message, object userState)
		{
			if (this.AddDiscussionBoardItemOperationCompleted == null)
			{
				this.AddDiscussionBoardItemOperationCompleted = new SendOrPostCallback(this.OnAddDiscussionBoardItemOperationCompleted);
			}
			base.InvokeAsync("AddDiscussionBoardItem", new object[]
			{
				listName,
				message
			}, this.AddDiscussionBoardItemOperationCompleted, userState);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x0007E1EC File Offset: 0x0007C3EC
		private void OnAddDiscussionBoardItemOperationCompleted(object arg)
		{
			if (this.AddDiscussionBoardItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddDiscussionBoardItemCompleted(this, new AddDiscussionBoardItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x0007E234 File Offset: 0x0007C434
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/AddWikiPage", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode AddWikiPage(string strListName, string listRelPageUrl, string wikiContent)
		{
			object[] array = base.Invoke("AddWikiPage", new object[]
			{
				strListName,
				listRelPageUrl,
				wikiContent
			});
			return (XmlNode)array[0];
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x0007E26C File Offset: 0x0007C46C
		public IAsyncResult BeginAddWikiPage(string strListName, string listRelPageUrl, string wikiContent, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddWikiPage", new object[]
			{
				strListName,
				listRelPageUrl,
				wikiContent
			}, callback, asyncState);
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x0007E29C File Offset: 0x0007C49C
		public XmlNode EndAddWikiPage(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x0007E2B9 File Offset: 0x0007C4B9
		public void AddWikiPageAsync(string strListName, string listRelPageUrl, string wikiContent)
		{
			this.AddWikiPageAsync(strListName, listRelPageUrl, wikiContent, null);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x0007E2C8 File Offset: 0x0007C4C8
		public void AddWikiPageAsync(string strListName, string listRelPageUrl, string wikiContent, object userState)
		{
			if (this.AddWikiPageOperationCompleted == null)
			{
				this.AddWikiPageOperationCompleted = new SendOrPostCallback(this.OnAddWikiPageOperationCompleted);
			}
			base.InvokeAsync("AddWikiPage", new object[]
			{
				strListName,
				listRelPageUrl,
				wikiContent
			}, this.AddWikiPageOperationCompleted, userState);
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x0007E318 File Offset: 0x0007C518
		private void OnAddWikiPageOperationCompleted(object arg)
		{
			if (this.AddWikiPageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddWikiPageCompleted(this, new AddWikiPageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x0007E360 File Offset: 0x0007C560
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetVersionCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetVersionCollection(string strlistID, string strlistItemID, string strFieldName)
		{
			object[] array = base.Invoke("GetVersionCollection", new object[]
			{
				strlistID,
				strlistItemID,
				strFieldName
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x0007E398 File Offset: 0x0007C598
		public IAsyncResult BeginGetVersionCollection(string strlistID, string strlistItemID, string strFieldName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetVersionCollection", new object[]
			{
				strlistID,
				strlistItemID,
				strFieldName
			}, callback, asyncState);
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x0007E3C8 File Offset: 0x0007C5C8
		public XmlNode EndGetVersionCollection(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x0007E3E5 File Offset: 0x0007C5E5
		public void GetVersionCollectionAsync(string strlistID, string strlistItemID, string strFieldName)
		{
			this.GetVersionCollectionAsync(strlistID, strlistItemID, strFieldName, null);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x0007E3F4 File Offset: 0x0007C5F4
		public void GetVersionCollectionAsync(string strlistID, string strlistItemID, string strFieldName, object userState)
		{
			if (this.GetVersionCollectionOperationCompleted == null)
			{
				this.GetVersionCollectionOperationCompleted = new SendOrPostCallback(this.OnGetVersionCollectionOperationCompleted);
			}
			base.InvokeAsync("GetVersionCollection", new object[]
			{
				strlistID,
				strlistItemID,
				strFieldName
			}, this.GetVersionCollectionOperationCompleted, userState);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x0007E444 File Offset: 0x0007C644
		private void OnGetVersionCollectionOperationCompleted(object arg)
		{
			if (this.GetVersionCollectionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetVersionCollectionCompleted(this, new GetVersionCollectionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x0007E48C File Offset: 0x0007C68C
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/AddAttachment", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string AddAttachment(string listName, string listItemID, string fileName, [XmlElement(DataType = "base64Binary")] byte[] attachment)
		{
			object[] array = base.Invoke("AddAttachment", new object[]
			{
				listName,
				listItemID,
				fileName,
				attachment
			});
			return (string)array[0];
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x0007E4C8 File Offset: 0x0007C6C8
		public IAsyncResult BeginAddAttachment(string listName, string listItemID, string fileName, byte[] attachment, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddAttachment", new object[]
			{
				listName,
				listItemID,
				fileName,
				attachment
			}, callback, asyncState);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x0007E500 File Offset: 0x0007C700
		public string EndAddAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x0007E51D File Offset: 0x0007C71D
		public void AddAttachmentAsync(string listName, string listItemID, string fileName, byte[] attachment)
		{
			this.AddAttachmentAsync(listName, listItemID, fileName, attachment, null);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x0007E52C File Offset: 0x0007C72C
		public void AddAttachmentAsync(string listName, string listItemID, string fileName, byte[] attachment, object userState)
		{
			if (this.AddAttachmentOperationCompleted == null)
			{
				this.AddAttachmentOperationCompleted = new SendOrPostCallback(this.OnAddAttachmentOperationCompleted);
			}
			base.InvokeAsync("AddAttachment", new object[]
			{
				listName,
				listItemID,
				fileName,
				attachment
			}, this.AddAttachmentOperationCompleted, userState);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x0007E580 File Offset: 0x0007C780
		private void OnAddAttachmentOperationCompleted(object arg)
		{
			if (this.AddAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddAttachmentCompleted(this, new AddAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x0007E5C8 File Offset: 0x0007C7C8
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetAttachmentCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetAttachmentCollection(string listName, string listItemID)
		{
			object[] array = base.Invoke("GetAttachmentCollection", new object[]
			{
				listName,
				listItemID
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x0007E5FC File Offset: 0x0007C7FC
		public IAsyncResult BeginGetAttachmentCollection(string listName, string listItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAttachmentCollection", new object[]
			{
				listName,
				listItemID
			}, callback, asyncState);
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x0007E628 File Offset: 0x0007C828
		public XmlNode EndGetAttachmentCollection(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x0007E645 File Offset: 0x0007C845
		public void GetAttachmentCollectionAsync(string listName, string listItemID)
		{
			this.GetAttachmentCollectionAsync(listName, listItemID, null);
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x0007E650 File Offset: 0x0007C850
		public void GetAttachmentCollectionAsync(string listName, string listItemID, object userState)
		{
			if (this.GetAttachmentCollectionOperationCompleted == null)
			{
				this.GetAttachmentCollectionOperationCompleted = new SendOrPostCallback(this.OnGetAttachmentCollectionOperationCompleted);
			}
			base.InvokeAsync("GetAttachmentCollection", new object[]
			{
				listName,
				listItemID
			}, this.GetAttachmentCollectionOperationCompleted, userState);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x0007E69C File Offset: 0x0007C89C
		private void OnGetAttachmentCollectionOperationCompleted(object arg)
		{
			if (this.GetAttachmentCollectionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAttachmentCollectionCompleted(this, new GetAttachmentCollectionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x0007E6E4 File Offset: 0x0007C8E4
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/DeleteAttachment", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteAttachment(string listName, string listItemID, string url)
		{
			base.Invoke("DeleteAttachment", new object[]
			{
				listName,
				listItemID,
				url
			});
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x0007E714 File Offset: 0x0007C914
		public IAsyncResult BeginDeleteAttachment(string listName, string listItemID, string url, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteAttachment", new object[]
			{
				listName,
				listItemID,
				url
			}, callback, asyncState);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x0007E744 File Offset: 0x0007C944
		public void EndDeleteAttachment(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x0007E74E File Offset: 0x0007C94E
		public void DeleteAttachmentAsync(string listName, string listItemID, string url)
		{
			this.DeleteAttachmentAsync(listName, listItemID, url, null);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0007E75C File Offset: 0x0007C95C
		public void DeleteAttachmentAsync(string listName, string listItemID, string url, object userState)
		{
			if (this.DeleteAttachmentOperationCompleted == null)
			{
				this.DeleteAttachmentOperationCompleted = new SendOrPostCallback(this.OnDeleteAttachmentOperationCompleted);
			}
			base.InvokeAsync("DeleteAttachment", new object[]
			{
				listName,
				listItemID,
				url
			}, this.DeleteAttachmentOperationCompleted, userState);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x0007E7AC File Offset: 0x0007C9AC
		private void OnDeleteAttachmentOperationCompleted(object arg)
		{
			if (this.DeleteAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteAttachmentCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x0007E7EC File Offset: 0x0007C9EC
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/CheckOutFile", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CheckOutFile(string pageUrl, string checkoutToLocal, string lastmodified)
		{
			object[] array = base.Invoke("CheckOutFile", new object[]
			{
				pageUrl,
				checkoutToLocal,
				lastmodified
			});
			return (bool)array[0];
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x0007E824 File Offset: 0x0007CA24
		public IAsyncResult BeginCheckOutFile(string pageUrl, string checkoutToLocal, string lastmodified, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CheckOutFile", new object[]
			{
				pageUrl,
				checkoutToLocal,
				lastmodified
			}, callback, asyncState);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x0007E854 File Offset: 0x0007CA54
		public bool EndCheckOutFile(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x0007E871 File Offset: 0x0007CA71
		public void CheckOutFileAsync(string pageUrl, string checkoutToLocal, string lastmodified)
		{
			this.CheckOutFileAsync(pageUrl, checkoutToLocal, lastmodified, null);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x0007E880 File Offset: 0x0007CA80
		public void CheckOutFileAsync(string pageUrl, string checkoutToLocal, string lastmodified, object userState)
		{
			if (this.CheckOutFileOperationCompleted == null)
			{
				this.CheckOutFileOperationCompleted = new SendOrPostCallback(this.OnCheckOutFileOperationCompleted);
			}
			base.InvokeAsync("CheckOutFile", new object[]
			{
				pageUrl,
				checkoutToLocal,
				lastmodified
			}, this.CheckOutFileOperationCompleted, userState);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x0007E8D0 File Offset: 0x0007CAD0
		private void OnCheckOutFileOperationCompleted(object arg)
		{
			if (this.CheckOutFileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckOutFileCompleted(this, new CheckOutFileCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x0007E918 File Offset: 0x0007CB18
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UndoCheckOut", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool UndoCheckOut(string pageUrl)
		{
			object[] array = base.Invoke("UndoCheckOut", new object[]
			{
				pageUrl
			});
			return (bool)array[0];
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0007E948 File Offset: 0x0007CB48
		public IAsyncResult BeginUndoCheckOut(string pageUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UndoCheckOut", new object[]
			{
				pageUrl
			}, callback, asyncState);
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x0007E970 File Offset: 0x0007CB70
		public bool EndUndoCheckOut(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0007E98D File Offset: 0x0007CB8D
		public void UndoCheckOutAsync(string pageUrl)
		{
			this.UndoCheckOutAsync(pageUrl, null);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x0007E998 File Offset: 0x0007CB98
		public void UndoCheckOutAsync(string pageUrl, object userState)
		{
			if (this.UndoCheckOutOperationCompleted == null)
			{
				this.UndoCheckOutOperationCompleted = new SendOrPostCallback(this.OnUndoCheckOutOperationCompleted);
			}
			base.InvokeAsync("UndoCheckOut", new object[]
			{
				pageUrl
			}, this.UndoCheckOutOperationCompleted, userState);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x0007E9E0 File Offset: 0x0007CBE0
		private void OnUndoCheckOutOperationCompleted(object arg)
		{
			if (this.UndoCheckOutCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UndoCheckOutCompleted(this, new UndoCheckOutCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0007EA28 File Offset: 0x0007CC28
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/CheckInFile", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CheckInFile(string pageUrl, string comment, string CheckinType)
		{
			object[] array = base.Invoke("CheckInFile", new object[]
			{
				pageUrl,
				comment,
				CheckinType
			});
			return (bool)array[0];
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0007EA60 File Offset: 0x0007CC60
		public IAsyncResult BeginCheckInFile(string pageUrl, string comment, string CheckinType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CheckInFile", new object[]
			{
				pageUrl,
				comment,
				CheckinType
			}, callback, asyncState);
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x0007EA90 File Offset: 0x0007CC90
		public bool EndCheckInFile(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x0007EAAD File Offset: 0x0007CCAD
		public void CheckInFileAsync(string pageUrl, string comment, string CheckinType)
		{
			this.CheckInFileAsync(pageUrl, comment, CheckinType, null);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x0007EABC File Offset: 0x0007CCBC
		public void CheckInFileAsync(string pageUrl, string comment, string CheckinType, object userState)
		{
			if (this.CheckInFileOperationCompleted == null)
			{
				this.CheckInFileOperationCompleted = new SendOrPostCallback(this.OnCheckInFileOperationCompleted);
			}
			base.InvokeAsync("CheckInFile", new object[]
			{
				pageUrl,
				comment,
				CheckinType
			}, this.CheckInFileOperationCompleted, userState);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0007EB0C File Offset: 0x0007CD0C
		private void OnCheckInFileOperationCompleted(object arg)
		{
			if (this.CheckInFileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CheckInFileCompleted(this, new CheckInFileCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x0007EB54 File Offset: 0x0007CD54
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListContentTypes", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListContentTypes(string listName, string contentTypeId)
		{
			object[] array = base.Invoke("GetListContentTypes", new object[]
			{
				listName,
				contentTypeId
			});
			return (XmlNode)array[0];
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x0007EB88 File Offset: 0x0007CD88
		public IAsyncResult BeginGetListContentTypes(string listName, string contentTypeId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListContentTypes", new object[]
			{
				listName,
				contentTypeId
			}, callback, asyncState);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x0007EBB4 File Offset: 0x0007CDB4
		public XmlNode EndGetListContentTypes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0007EBD1 File Offset: 0x0007CDD1
		public void GetListContentTypesAsync(string listName, string contentTypeId)
		{
			this.GetListContentTypesAsync(listName, contentTypeId, null);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x0007EBDC File Offset: 0x0007CDDC
		public void GetListContentTypesAsync(string listName, string contentTypeId, object userState)
		{
			if (this.GetListContentTypesOperationCompleted == null)
			{
				this.GetListContentTypesOperationCompleted = new SendOrPostCallback(this.OnGetListContentTypesOperationCompleted);
			}
			base.InvokeAsync("GetListContentTypes", new object[]
			{
				listName,
				contentTypeId
			}, this.GetListContentTypesOperationCompleted, userState);
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x0007EC28 File Offset: 0x0007CE28
		private void OnGetListContentTypesOperationCompleted(object arg)
		{
			if (this.GetListContentTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListContentTypesCompleted(this, new GetListContentTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0007EC70 File Offset: 0x0007CE70
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListContentTypesAndProperties", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListContentTypesAndProperties(string listName, string contentTypeId, string propertyPrefix, bool includeWebProperties, [XmlIgnore] bool includeWebPropertiesSpecified)
		{
			object[] array = base.Invoke("GetListContentTypesAndProperties", new object[]
			{
				listName,
				contentTypeId,
				propertyPrefix,
				includeWebProperties,
				includeWebPropertiesSpecified
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x0007ECBC File Offset: 0x0007CEBC
		public IAsyncResult BeginGetListContentTypesAndProperties(string listName, string contentTypeId, string propertyPrefix, bool includeWebProperties, bool includeWebPropertiesSpecified, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListContentTypesAndProperties", new object[]
			{
				listName,
				contentTypeId,
				propertyPrefix,
				includeWebProperties,
				includeWebPropertiesSpecified
			}, callback, asyncState);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x0007ED00 File Offset: 0x0007CF00
		public XmlNode EndGetListContentTypesAndProperties(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x0007ED1D File Offset: 0x0007CF1D
		public void GetListContentTypesAndPropertiesAsync(string listName, string contentTypeId, string propertyPrefix, bool includeWebProperties, bool includeWebPropertiesSpecified)
		{
			this.GetListContentTypesAndPropertiesAsync(listName, contentTypeId, propertyPrefix, includeWebProperties, includeWebPropertiesSpecified, null);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x0007ED30 File Offset: 0x0007CF30
		public void GetListContentTypesAndPropertiesAsync(string listName, string contentTypeId, string propertyPrefix, bool includeWebProperties, bool includeWebPropertiesSpecified, object userState)
		{
			if (this.GetListContentTypesAndPropertiesOperationCompleted == null)
			{
				this.GetListContentTypesAndPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetListContentTypesAndPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetListContentTypesAndProperties", new object[]
			{
				listName,
				contentTypeId,
				propertyPrefix,
				includeWebProperties,
				includeWebPropertiesSpecified
			}, this.GetListContentTypesAndPropertiesOperationCompleted, userState);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0007ED94 File Offset: 0x0007CF94
		private void OnGetListContentTypesAndPropertiesOperationCompleted(object arg)
		{
			if (this.GetListContentTypesAndPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListContentTypesAndPropertiesCompleted(this, new GetListContentTypesAndPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/GetListContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetListContentType(string listName, string contentTypeId)
		{
			object[] array = base.Invoke("GetListContentType", new object[]
			{
				listName,
				contentTypeId
			});
			return (XmlNode)array[0];
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0007EE10 File Offset: 0x0007D010
		public IAsyncResult BeginGetListContentType(string listName, string contentTypeId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetListContentType", new object[]
			{
				listName,
				contentTypeId
			}, callback, asyncState);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0007EE3C File Offset: 0x0007D03C
		public XmlNode EndGetListContentType(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x0007EE59 File Offset: 0x0007D059
		public void GetListContentTypeAsync(string listName, string contentTypeId)
		{
			this.GetListContentTypeAsync(listName, contentTypeId, null);
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x0007EE64 File Offset: 0x0007D064
		public void GetListContentTypeAsync(string listName, string contentTypeId, object userState)
		{
			if (this.GetListContentTypeOperationCompleted == null)
			{
				this.GetListContentTypeOperationCompleted = new SendOrPostCallback(this.OnGetListContentTypeOperationCompleted);
			}
			base.InvokeAsync("GetListContentType", new object[]
			{
				listName,
				contentTypeId
			}, this.GetListContentTypeOperationCompleted, userState);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x0007EEB0 File Offset: 0x0007D0B0
		private void OnGetListContentTypeOperationCompleted(object arg)
		{
			if (this.GetListContentTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetListContentTypeCompleted(this, new GetListContentTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x0007EEF8 File Offset: 0x0007D0F8
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/CreateContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string CreateContentType(string listName, string displayName, string parentType, XmlNode fields, XmlNode contentTypeProperties, string addToView)
		{
			object[] array = base.Invoke("CreateContentType", new object[]
			{
				listName,
				displayName,
				parentType,
				fields,
				contentTypeProperties,
				addToView
			});
			return (string)array[0];
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x0007EF3C File Offset: 0x0007D13C
		public IAsyncResult BeginCreateContentType(string listName, string displayName, string parentType, XmlNode fields, XmlNode contentTypeProperties, string addToView, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateContentType", new object[]
			{
				listName,
				displayName,
				parentType,
				fields,
				contentTypeProperties,
				addToView
			}, callback, asyncState);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x0007EF7C File Offset: 0x0007D17C
		public string EndCreateContentType(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x0007EF99 File Offset: 0x0007D199
		public void CreateContentTypeAsync(string listName, string displayName, string parentType, XmlNode fields, XmlNode contentTypeProperties, string addToView)
		{
			this.CreateContentTypeAsync(listName, displayName, parentType, fields, contentTypeProperties, addToView, null);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x0007EFAC File Offset: 0x0007D1AC
		public void CreateContentTypeAsync(string listName, string displayName, string parentType, XmlNode fields, XmlNode contentTypeProperties, string addToView, object userState)
		{
			if (this.CreateContentTypeOperationCompleted == null)
			{
				this.CreateContentTypeOperationCompleted = new SendOrPostCallback(this.OnCreateContentTypeOperationCompleted);
			}
			base.InvokeAsync("CreateContentType", new object[]
			{
				listName,
				displayName,
				parentType,
				fields,
				contentTypeProperties,
				addToView
			}, this.CreateContentTypeOperationCompleted, userState);
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x0007F00C File Offset: 0x0007D20C
		private void OnCreateContentTypeOperationCompleted(object arg)
		{
			if (this.CreateContentTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateContentTypeCompleted(this, new CreateContentTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x0007F054 File Offset: 0x0007D254
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UpdateContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UpdateContentType(string listName, string contentTypeId, XmlNode contentTypeProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string addToView)
		{
			object[] array = base.Invoke("UpdateContentType", new object[]
			{
				listName,
				contentTypeId,
				contentTypeProperties,
				newFields,
				updateFields,
				deleteFields,
				addToView
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x0007F0A0 File Offset: 0x0007D2A0
		public IAsyncResult BeginUpdateContentType(string listName, string contentTypeId, XmlNode contentTypeProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string addToView, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateContentType", new object[]
			{
				listName,
				contentTypeId,
				contentTypeProperties,
				newFields,
				updateFields,
				deleteFields,
				addToView
			}, callback, asyncState);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x0007F0E4 File Offset: 0x0007D2E4
		public XmlNode EndUpdateContentType(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x0007F104 File Offset: 0x0007D304
		public void UpdateContentTypeAsync(string listName, string contentTypeId, XmlNode contentTypeProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string addToView)
		{
			this.UpdateContentTypeAsync(listName, contentTypeId, contentTypeProperties, newFields, updateFields, deleteFields, addToView, null);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0007F124 File Offset: 0x0007D324
		public void UpdateContentTypeAsync(string listName, string contentTypeId, XmlNode contentTypeProperties, XmlNode newFields, XmlNode updateFields, XmlNode deleteFields, string addToView, object userState)
		{
			if (this.UpdateContentTypeOperationCompleted == null)
			{
				this.UpdateContentTypeOperationCompleted = new SendOrPostCallback(this.OnUpdateContentTypeOperationCompleted);
			}
			base.InvokeAsync("UpdateContentType", new object[]
			{
				listName,
				contentTypeId,
				contentTypeProperties,
				newFields,
				updateFields,
				deleteFields,
				addToView
			}, this.UpdateContentTypeOperationCompleted, userState);
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x0007F188 File Offset: 0x0007D388
		private void OnUpdateContentTypeOperationCompleted(object arg)
		{
			if (this.UpdateContentTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateContentTypeCompleted(this, new UpdateContentTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x0007F1D0 File Offset: 0x0007D3D0
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/DeleteContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode DeleteContentType(string listName, string contentTypeId)
		{
			object[] array = base.Invoke("DeleteContentType", new object[]
			{
				listName,
				contentTypeId
			});
			return (XmlNode)array[0];
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x0007F204 File Offset: 0x0007D404
		public IAsyncResult BeginDeleteContentType(string listName, string contentTypeId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteContentType", new object[]
			{
				listName,
				contentTypeId
			}, callback, asyncState);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x0007F230 File Offset: 0x0007D430
		public XmlNode EndDeleteContentType(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x0007F24D File Offset: 0x0007D44D
		public void DeleteContentTypeAsync(string listName, string contentTypeId)
		{
			this.DeleteContentTypeAsync(listName, contentTypeId, null);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0007F258 File Offset: 0x0007D458
		public void DeleteContentTypeAsync(string listName, string contentTypeId, object userState)
		{
			if (this.DeleteContentTypeOperationCompleted == null)
			{
				this.DeleteContentTypeOperationCompleted = new SendOrPostCallback(this.OnDeleteContentTypeOperationCompleted);
			}
			base.InvokeAsync("DeleteContentType", new object[]
			{
				listName,
				contentTypeId
			}, this.DeleteContentTypeOperationCompleted, userState);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x0007F2A4 File Offset: 0x0007D4A4
		private void OnDeleteContentTypeOperationCompleted(object arg)
		{
			if (this.DeleteContentTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteContentTypeCompleted(this, new DeleteContentTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x0007F2EC File Offset: 0x0007D4EC
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UpdateContentTypeXmlDocument", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UpdateContentTypeXmlDocument(string listName, string contentTypeId, XmlNode newDocument)
		{
			object[] array = base.Invoke("UpdateContentTypeXmlDocument", new object[]
			{
				listName,
				contentTypeId,
				newDocument
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0007F324 File Offset: 0x0007D524
		public IAsyncResult BeginUpdateContentTypeXmlDocument(string listName, string contentTypeId, XmlNode newDocument, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateContentTypeXmlDocument", new object[]
			{
				listName,
				contentTypeId,
				newDocument
			}, callback, asyncState);
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0007F354 File Offset: 0x0007D554
		public XmlNode EndUpdateContentTypeXmlDocument(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x0007F371 File Offset: 0x0007D571
		public void UpdateContentTypeXmlDocumentAsync(string listName, string contentTypeId, XmlNode newDocument)
		{
			this.UpdateContentTypeXmlDocumentAsync(listName, contentTypeId, newDocument, null);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x0007F380 File Offset: 0x0007D580
		public void UpdateContentTypeXmlDocumentAsync(string listName, string contentTypeId, XmlNode newDocument, object userState)
		{
			if (this.UpdateContentTypeXmlDocumentOperationCompleted == null)
			{
				this.UpdateContentTypeXmlDocumentOperationCompleted = new SendOrPostCallback(this.OnUpdateContentTypeXmlDocumentOperationCompleted);
			}
			base.InvokeAsync("UpdateContentTypeXmlDocument", new object[]
			{
				listName,
				contentTypeId,
				newDocument
			}, this.UpdateContentTypeXmlDocumentOperationCompleted, userState);
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x0007F3D0 File Offset: 0x0007D5D0
		private void OnUpdateContentTypeXmlDocumentOperationCompleted(object arg)
		{
			if (this.UpdateContentTypeXmlDocumentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateContentTypeXmlDocumentCompleted(this, new UpdateContentTypeXmlDocumentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x0007F418 File Offset: 0x0007D618
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/UpdateContentTypesXmlDocument", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UpdateContentTypesXmlDocument(string listName, XmlNode newDocument)
		{
			object[] array = base.Invoke("UpdateContentTypesXmlDocument", new object[]
			{
				listName,
				newDocument
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x0007F44C File Offset: 0x0007D64C
		public IAsyncResult BeginUpdateContentTypesXmlDocument(string listName, XmlNode newDocument, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateContentTypesXmlDocument", new object[]
			{
				listName,
				newDocument
			}, callback, asyncState);
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x0007F478 File Offset: 0x0007D678
		public XmlNode EndUpdateContentTypesXmlDocument(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x0007F495 File Offset: 0x0007D695
		public void UpdateContentTypesXmlDocumentAsync(string listName, XmlNode newDocument)
		{
			this.UpdateContentTypesXmlDocumentAsync(listName, newDocument, null);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0007F4A0 File Offset: 0x0007D6A0
		public void UpdateContentTypesXmlDocumentAsync(string listName, XmlNode newDocument, object userState)
		{
			if (this.UpdateContentTypesXmlDocumentOperationCompleted == null)
			{
				this.UpdateContentTypesXmlDocumentOperationCompleted = new SendOrPostCallback(this.OnUpdateContentTypesXmlDocumentOperationCompleted);
			}
			base.InvokeAsync("UpdateContentTypesXmlDocument", new object[]
			{
				listName,
				newDocument
			}, this.UpdateContentTypesXmlDocumentOperationCompleted, userState);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x0007F4EC File Offset: 0x0007D6EC
		private void OnUpdateContentTypesXmlDocumentOperationCompleted(object arg)
		{
			if (this.UpdateContentTypesXmlDocumentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateContentTypesXmlDocumentCompleted(this, new UpdateContentTypesXmlDocumentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x0007F534 File Offset: 0x0007D734
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/DeleteContentTypeXmlDocument", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode DeleteContentTypeXmlDocument(string listName, string contentTypeId, string documentUri)
		{
			object[] array = base.Invoke("DeleteContentTypeXmlDocument", new object[]
			{
				listName,
				contentTypeId,
				documentUri
			});
			return (XmlNode)array[0];
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0007F56C File Offset: 0x0007D76C
		public IAsyncResult BeginDeleteContentTypeXmlDocument(string listName, string contentTypeId, string documentUri, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteContentTypeXmlDocument", new object[]
			{
				listName,
				contentTypeId,
				documentUri
			}, callback, asyncState);
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x0007F59C File Offset: 0x0007D79C
		public XmlNode EndDeleteContentTypeXmlDocument(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0007F5B9 File Offset: 0x0007D7B9
		public void DeleteContentTypeXmlDocumentAsync(string listName, string contentTypeId, string documentUri)
		{
			this.DeleteContentTypeXmlDocumentAsync(listName, contentTypeId, documentUri, null);
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0007F5C8 File Offset: 0x0007D7C8
		public void DeleteContentTypeXmlDocumentAsync(string listName, string contentTypeId, string documentUri, object userState)
		{
			if (this.DeleteContentTypeXmlDocumentOperationCompleted == null)
			{
				this.DeleteContentTypeXmlDocumentOperationCompleted = new SendOrPostCallback(this.OnDeleteContentTypeXmlDocumentOperationCompleted);
			}
			base.InvokeAsync("DeleteContentTypeXmlDocument", new object[]
			{
				listName,
				contentTypeId,
				documentUri
			}, this.DeleteContentTypeXmlDocumentOperationCompleted, userState);
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0007F618 File Offset: 0x0007D818
		private void OnDeleteContentTypeXmlDocumentOperationCompleted(object arg)
		{
			if (this.DeleteContentTypeXmlDocumentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteContentTypeXmlDocumentCompleted(this, new DeleteContentTypeXmlDocumentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x0007F660 File Offset: 0x0007D860
		[SoapDocumentMethod("http://schemas.microsoft.com/sharepoint/soap/ApplyContentTypeToList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode ApplyContentTypeToList(string webUrl, string contentTypeId, string listName)
		{
			object[] array = base.Invoke("ApplyContentTypeToList", new object[]
			{
				webUrl,
				contentTypeId,
				listName
			});
			return (XmlNode)array[0];
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x0007F698 File Offset: 0x0007D898
		public IAsyncResult BeginApplyContentTypeToList(string webUrl, string contentTypeId, string listName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ApplyContentTypeToList", new object[]
			{
				webUrl,
				contentTypeId,
				listName
			}, callback, asyncState);
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x0007F6C8 File Offset: 0x0007D8C8
		public XmlNode EndApplyContentTypeToList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (XmlNode)array[0];
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0007F6E5 File Offset: 0x0007D8E5
		public void ApplyContentTypeToListAsync(string webUrl, string contentTypeId, string listName)
		{
			this.ApplyContentTypeToListAsync(webUrl, contentTypeId, listName, null);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0007F6F4 File Offset: 0x0007D8F4
		public void ApplyContentTypeToListAsync(string webUrl, string contentTypeId, string listName, object userState)
		{
			if (this.ApplyContentTypeToListOperationCompleted == null)
			{
				this.ApplyContentTypeToListOperationCompleted = new SendOrPostCallback(this.OnApplyContentTypeToListOperationCompleted);
			}
			base.InvokeAsync("ApplyContentTypeToList", new object[]
			{
				webUrl,
				contentTypeId,
				listName
			}, this.ApplyContentTypeToListOperationCompleted, userState);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x0007F744 File Offset: 0x0007D944
		private void OnApplyContentTypeToListOperationCompleted(object arg)
		{
			if (this.ApplyContentTypeToListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ApplyContentTypeToListCompleted(this, new ApplyContentTypeToListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x0007F789 File Offset: 0x0007D989
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04002BEC RID: 11244
		private SendOrPostCallback GetListOperationCompleted;

		// Token: 0x04002BED RID: 11245
		private SendOrPostCallback GetListAndViewOperationCompleted;

		// Token: 0x04002BEE RID: 11246
		private SendOrPostCallback DeleteListOperationCompleted;

		// Token: 0x04002BEF RID: 11247
		private SendOrPostCallback AddListOperationCompleted;

		// Token: 0x04002BF0 RID: 11248
		private SendOrPostCallback AddListFromFeatureOperationCompleted;

		// Token: 0x04002BF1 RID: 11249
		private SendOrPostCallback UpdateListOperationCompleted;

		// Token: 0x04002BF2 RID: 11250
		private SendOrPostCallback GetListCollectionOperationCompleted;

		// Token: 0x04002BF3 RID: 11251
		private SendOrPostCallback GetListItemsOperationCompleted;

		// Token: 0x04002BF4 RID: 11252
		private SendOrPostCallback GetListItemChangesOperationCompleted;

		// Token: 0x04002BF5 RID: 11253
		private SendOrPostCallback GetListItemChangesWithKnowledgeOperationCompleted;

		// Token: 0x04002BF6 RID: 11254
		private SendOrPostCallback GetListItemChangesSinceTokenOperationCompleted;

		// Token: 0x04002BF7 RID: 11255
		private SendOrPostCallback UpdateListItemsOperationCompleted;

		// Token: 0x04002BF8 RID: 11256
		private SendOrPostCallback UpdateListItemsWithKnowledgeOperationCompleted;

		// Token: 0x04002BF9 RID: 11257
		private SendOrPostCallback AddDiscussionBoardItemOperationCompleted;

		// Token: 0x04002BFA RID: 11258
		private SendOrPostCallback AddWikiPageOperationCompleted;

		// Token: 0x04002BFB RID: 11259
		private SendOrPostCallback GetVersionCollectionOperationCompleted;

		// Token: 0x04002BFC RID: 11260
		private SendOrPostCallback AddAttachmentOperationCompleted;

		// Token: 0x04002BFD RID: 11261
		private SendOrPostCallback GetAttachmentCollectionOperationCompleted;

		// Token: 0x04002BFE RID: 11262
		private SendOrPostCallback DeleteAttachmentOperationCompleted;

		// Token: 0x04002BFF RID: 11263
		private SendOrPostCallback CheckOutFileOperationCompleted;

		// Token: 0x04002C00 RID: 11264
		private SendOrPostCallback UndoCheckOutOperationCompleted;

		// Token: 0x04002C01 RID: 11265
		private SendOrPostCallback CheckInFileOperationCompleted;

		// Token: 0x04002C02 RID: 11266
		private SendOrPostCallback GetListContentTypesOperationCompleted;

		// Token: 0x04002C03 RID: 11267
		private SendOrPostCallback GetListContentTypesAndPropertiesOperationCompleted;

		// Token: 0x04002C04 RID: 11268
		private SendOrPostCallback GetListContentTypeOperationCompleted;

		// Token: 0x04002C05 RID: 11269
		private SendOrPostCallback CreateContentTypeOperationCompleted;

		// Token: 0x04002C06 RID: 11270
		private SendOrPostCallback UpdateContentTypeOperationCompleted;

		// Token: 0x04002C07 RID: 11271
		private SendOrPostCallback DeleteContentTypeOperationCompleted;

		// Token: 0x04002C08 RID: 11272
		private SendOrPostCallback UpdateContentTypeXmlDocumentOperationCompleted;

		// Token: 0x04002C09 RID: 11273
		private SendOrPostCallback UpdateContentTypesXmlDocumentOperationCompleted;

		// Token: 0x04002C0A RID: 11274
		private SendOrPostCallback DeleteContentTypeXmlDocumentOperationCompleted;

		// Token: 0x04002C0B RID: 11275
		private SendOrPostCallback ApplyContentTypeToListOperationCompleted;
	}
}
