using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000185 RID: 389
	[CLSCompliant(false)]
	[XmlInclude(typeof(BaseFolderIdType))]
	[XmlInclude(typeof(BaseRequestType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(BaseCalendarItemStateDefinitionType))]
	[XmlInclude(typeof(RuleOperationType))]
	[XmlInclude(typeof(BaseEmailAddressType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "PrivateExchangeServiceBinding", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(AttendeeConflictData))]
	[XmlInclude(typeof(ServiceConfiguration))]
	[XmlInclude(typeof(DirectoryEntryType))]
	[XmlInclude(typeof(BaseResponseMessageType))]
	[XmlInclude(typeof(RecurrencePatternBaseType))]
	[XmlInclude(typeof(BaseSubscriptionRequestType))]
	[XmlInclude(typeof(MailboxLocatorType))]
	[XmlInclude(typeof(BaseGroupByType))]
	[XmlInclude(typeof(RecurrenceRangeBaseType))]
	[XmlInclude(typeof(BasePagingType))]
	[XmlInclude(typeof(BaseItemIdType))]
	[XmlInclude(typeof(ChangeDescriptionType))]
	[XmlInclude(typeof(AttachmentType))]
	[XmlInclude(typeof(BasePermissionType))]
	[XmlInclude(typeof(BaseFolderType))]
	public class PrivateExchangeServiceBinding : CustomSoapHttpClientProtocol
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x00023C07 File Offset: 0x00021E07
		public PrivateExchangeServiceBinding()
		{
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06000C6C RID: 3180 RVA: 0x00023C10 File Offset: 0x00021E10
		// (remove) Token: 0x06000C6D RID: 3181 RVA: 0x00023C48 File Offset: 0x00021E48
		public event CreateUMPromptCompletedEventHandler CreateUMPromptCompleted;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000C6E RID: 3182 RVA: 0x00023C80 File Offset: 0x00021E80
		// (remove) Token: 0x06000C6F RID: 3183 RVA: 0x00023CB8 File Offset: 0x00021EB8
		public event DeleteUMPromptsCompletedEventHandler DeleteUMPromptsCompleted;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06000C70 RID: 3184 RVA: 0x00023CF0 File Offset: 0x00021EF0
		// (remove) Token: 0x06000C71 RID: 3185 RVA: 0x00023D28 File Offset: 0x00021F28
		public event GetUMPromptCompletedEventHandler GetUMPromptCompleted;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06000C72 RID: 3186 RVA: 0x00023D60 File Offset: 0x00021F60
		// (remove) Token: 0x06000C73 RID: 3187 RVA: 0x00023D98 File Offset: 0x00021F98
		public event GetUMPromptNamesCompletedEventHandler GetUMPromptNamesCompleted;

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06000C74 RID: 3188 RVA: 0x00023DD0 File Offset: 0x00021FD0
		// (remove) Token: 0x06000C75 RID: 3189 RVA: 0x00023E08 File Offset: 0x00022008
		public event GetClientExtensionCompletedEventHandler GetClientExtensionCompleted;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06000C76 RID: 3190 RVA: 0x00023E40 File Offset: 0x00022040
		// (remove) Token: 0x06000C77 RID: 3191 RVA: 0x00023E78 File Offset: 0x00022078
		public event SetClientExtensionCompletedEventHandler SetClientExtensionCompleted;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000C78 RID: 3192 RVA: 0x00023EB0 File Offset: 0x000220B0
		// (remove) Token: 0x06000C79 RID: 3193 RVA: 0x00023EE8 File Offset: 0x000220E8
		public event StartFindInGALSpeechRecognitionCompletedEventHandler StartFindInGALSpeechRecognitionCompleted;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06000C7A RID: 3194 RVA: 0x00023F20 File Offset: 0x00022120
		// (remove) Token: 0x06000C7B RID: 3195 RVA: 0x00023F58 File Offset: 0x00022158
		public event CompleteFindInGALSpeechRecognitionCompletedEventHandler CompleteFindInGALSpeechRecognitionCompleted;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06000C7C RID: 3196 RVA: 0x00023F90 File Offset: 0x00022190
		// (remove) Token: 0x06000C7D RID: 3197 RVA: 0x00023FC8 File Offset: 0x000221C8
		public event CreateUMCallDataRecordCompletedEventHandler CreateUMCallDataRecordCompleted;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06000C7E RID: 3198 RVA: 0x00024000 File Offset: 0x00022200
		// (remove) Token: 0x06000C7F RID: 3199 RVA: 0x00024038 File Offset: 0x00022238
		public event GetUMCallDataRecordsCompletedEventHandler GetUMCallDataRecordsCompleted;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06000C80 RID: 3200 RVA: 0x00024070 File Offset: 0x00022270
		// (remove) Token: 0x06000C81 RID: 3201 RVA: 0x000240A8 File Offset: 0x000222A8
		public event GetUMCallSummaryCompletedEventHandler GetUMCallSummaryCompleted;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06000C82 RID: 3202 RVA: 0x000240E0 File Offset: 0x000222E0
		// (remove) Token: 0x06000C83 RID: 3203 RVA: 0x00024118 File Offset: 0x00022318
		public event InitUMMailboxCompletedEventHandler InitUMMailboxCompleted;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06000C84 RID: 3204 RVA: 0x00024150 File Offset: 0x00022350
		// (remove) Token: 0x06000C85 RID: 3205 RVA: 0x00024188 File Offset: 0x00022388
		public event ResetUMMailboxCompletedEventHandler ResetUMMailboxCompleted;

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06000C86 RID: 3206 RVA: 0x000241C0 File Offset: 0x000223C0
		// (remove) Token: 0x06000C87 RID: 3207 RVA: 0x000241F8 File Offset: 0x000223F8
		public event ValidateUMPinCompletedEventHandler ValidateUMPinCompleted;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000C88 RID: 3208 RVA: 0x00024230 File Offset: 0x00022430
		// (remove) Token: 0x06000C89 RID: 3209 RVA: 0x00024268 File Offset: 0x00022468
		public event SaveUMPinCompletedEventHandler SaveUMPinCompleted;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000C8A RID: 3210 RVA: 0x000242A0 File Offset: 0x000224A0
		// (remove) Token: 0x06000C8B RID: 3211 RVA: 0x000242D8 File Offset: 0x000224D8
		public event GetUMPinCompletedEventHandler GetUMPinCompleted;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000C8C RID: 3212 RVA: 0x00024310 File Offset: 0x00022510
		// (remove) Token: 0x06000C8D RID: 3213 RVA: 0x00024348 File Offset: 0x00022548
		public event GetClientIntentCompletedEventHandler GetClientIntentCompleted;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000C8E RID: 3214 RVA: 0x00024380 File Offset: 0x00022580
		// (remove) Token: 0x06000C8F RID: 3215 RVA: 0x000243B8 File Offset: 0x000225B8
		public event GetUMSubscriberCallAnsweringDataCompletedEventHandler GetUMSubscriberCallAnsweringDataCompleted;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06000C90 RID: 3216 RVA: 0x000243F0 File Offset: 0x000225F0
		// (remove) Token: 0x06000C91 RID: 3217 RVA: 0x00024428 File Offset: 0x00022628
		public event UpdateMailboxAssociationCompletedEventHandler UpdateMailboxAssociationCompleted;

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06000C92 RID: 3218 RVA: 0x00024460 File Offset: 0x00022660
		// (remove) Token: 0x06000C93 RID: 3219 RVA: 0x00024498 File Offset: 0x00022698
		public event UpdateGroupMailboxCompletedEventHandler UpdateGroupMailboxCompleted;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06000C94 RID: 3220 RVA: 0x000244D0 File Offset: 0x000226D0
		// (remove) Token: 0x06000C95 RID: 3221 RVA: 0x00024508 File Offset: 0x00022708
		public event PostModernGroupItemCompletedEventHandler PostModernGroupItemCompleted;

		// Token: 0x06000C96 RID: 3222 RVA: 0x00024540 File Offset: 0x00022740
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUMPrompt", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateUMPromptResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateUMPromptResponseMessageType CreateUMPrompt([XmlElement("CreateUMPrompt", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUMPromptType CreateUMPrompt1)
		{
			object[] array = this.Invoke("CreateUMPrompt", new object[]
			{
				CreateUMPrompt1
			});
			return (CreateUMPromptResponseMessageType)array[0];
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00024570 File Offset: 0x00022770
		public IAsyncResult BeginCreateUMPrompt(CreateUMPromptType CreateUMPrompt1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateUMPrompt", new object[]
			{
				CreateUMPrompt1
			}, callback, asyncState);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00024598 File Offset: 0x00022798
		public CreateUMPromptResponseMessageType EndCreateUMPrompt(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateUMPromptResponseMessageType)array[0];
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000245B5 File Offset: 0x000227B5
		public void CreateUMPromptAsync(CreateUMPromptType CreateUMPrompt1)
		{
			this.CreateUMPromptAsync(CreateUMPrompt1, null);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000245C0 File Offset: 0x000227C0
		public void CreateUMPromptAsync(CreateUMPromptType CreateUMPrompt1, object userState)
		{
			if (this.CreateUMPromptOperationCompleted == null)
			{
				this.CreateUMPromptOperationCompleted = new SendOrPostCallback(this.OnCreateUMPromptOperationCompleted);
			}
			base.InvokeAsync("CreateUMPrompt", new object[]
			{
				CreateUMPrompt1
			}, this.CreateUMPromptOperationCompleted, userState);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00024608 File Offset: 0x00022808
		private void OnCreateUMPromptOperationCompleted(object arg)
		{
			if (this.CreateUMPromptCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateUMPromptCompleted(this, new CreateUMPromptCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00024650 File Offset: 0x00022850
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteUMPrompts", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("DeleteUMPromptsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteUMPromptsResponseMessageType DeleteUMPrompts([XmlElement("DeleteUMPrompts", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteUMPromptsType DeleteUMPrompts1)
		{
			object[] array = this.Invoke("DeleteUMPrompts", new object[]
			{
				DeleteUMPrompts1
			});
			return (DeleteUMPromptsResponseMessageType)array[0];
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00024680 File Offset: 0x00022880
		public IAsyncResult BeginDeleteUMPrompts(DeleteUMPromptsType DeleteUMPrompts1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteUMPrompts", new object[]
			{
				DeleteUMPrompts1
			}, callback, asyncState);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000246A8 File Offset: 0x000228A8
		public DeleteUMPromptsResponseMessageType EndDeleteUMPrompts(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteUMPromptsResponseMessageType)array[0];
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000246C5 File Offset: 0x000228C5
		public void DeleteUMPromptsAsync(DeleteUMPromptsType DeleteUMPrompts1)
		{
			this.DeleteUMPromptsAsync(DeleteUMPrompts1, null);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000246D0 File Offset: 0x000228D0
		public void DeleteUMPromptsAsync(DeleteUMPromptsType DeleteUMPrompts1, object userState)
		{
			if (this.DeleteUMPromptsOperationCompleted == null)
			{
				this.DeleteUMPromptsOperationCompleted = new SendOrPostCallback(this.OnDeleteUMPromptsOperationCompleted);
			}
			base.InvokeAsync("DeleteUMPrompts", new object[]
			{
				DeleteUMPrompts1
			}, this.DeleteUMPromptsOperationCompleted, userState);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00024718 File Offset: 0x00022918
		private void OnDeleteUMPromptsOperationCompleted(object arg)
		{
			if (this.DeleteUMPromptsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteUMPromptsCompleted(this, new DeleteUMPromptsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00024760 File Offset: 0x00022960
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMPrompt", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUMPromptResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMPromptResponseMessageType GetUMPrompt([XmlElement("GetUMPrompt", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMPromptType GetUMPrompt1)
		{
			object[] array = this.Invoke("GetUMPrompt", new object[]
			{
				GetUMPrompt1
			});
			return (GetUMPromptResponseMessageType)array[0];
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00024790 File Offset: 0x00022990
		public IAsyncResult BeginGetUMPrompt(GetUMPromptType GetUMPrompt1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUMPrompt", new object[]
			{
				GetUMPrompt1
			}, callback, asyncState);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000247B8 File Offset: 0x000229B8
		public GetUMPromptResponseMessageType EndGetUMPrompt(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUMPromptResponseMessageType)array[0];
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000247D5 File Offset: 0x000229D5
		public void GetUMPromptAsync(GetUMPromptType GetUMPrompt1)
		{
			this.GetUMPromptAsync(GetUMPrompt1, null);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000247E0 File Offset: 0x000229E0
		public void GetUMPromptAsync(GetUMPromptType GetUMPrompt1, object userState)
		{
			if (this.GetUMPromptOperationCompleted == null)
			{
				this.GetUMPromptOperationCompleted = new SendOrPostCallback(this.OnGetUMPromptOperationCompleted);
			}
			base.InvokeAsync("GetUMPrompt", new object[]
			{
				GetUMPrompt1
			}, this.GetUMPromptOperationCompleted, userState);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00024828 File Offset: 0x00022A28
		private void OnGetUMPromptOperationCompleted(object arg)
		{
			if (this.GetUMPromptCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUMPromptCompleted(this, new GetUMPromptCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00024870 File Offset: 0x00022A70
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMPromptNames", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUMPromptNamesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMPromptNamesResponseMessageType GetUMPromptNames([XmlElement("GetUMPromptNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMPromptNamesType GetUMPromptNames1)
		{
			object[] array = this.Invoke("GetUMPromptNames", new object[]
			{
				GetUMPromptNames1
			});
			return (GetUMPromptNamesResponseMessageType)array[0];
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000248A0 File Offset: 0x00022AA0
		public IAsyncResult BeginGetUMPromptNames(GetUMPromptNamesType GetUMPromptNames1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUMPromptNames", new object[]
			{
				GetUMPromptNames1
			}, callback, asyncState);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000248C8 File Offset: 0x00022AC8
		public GetUMPromptNamesResponseMessageType EndGetUMPromptNames(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUMPromptNamesResponseMessageType)array[0];
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000248E5 File Offset: 0x00022AE5
		public void GetUMPromptNamesAsync(GetUMPromptNamesType GetUMPromptNames1)
		{
			this.GetUMPromptNamesAsync(GetUMPromptNames1, null);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000248F0 File Offset: 0x00022AF0
		public void GetUMPromptNamesAsync(GetUMPromptNamesType GetUMPromptNames1, object userState)
		{
			if (this.GetUMPromptNamesOperationCompleted == null)
			{
				this.GetUMPromptNamesOperationCompleted = new SendOrPostCallback(this.OnGetUMPromptNamesOperationCompleted);
			}
			base.InvokeAsync("GetUMPromptNames", new object[]
			{
				GetUMPromptNames1
			}, this.GetUMPromptNamesOperationCompleted, userState);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00024938 File Offset: 0x00022B38
		private void OnGetUMPromptNamesOperationCompleted(object arg)
		{
			if (this.GetUMPromptNamesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUMPromptNamesCompleted(this, new GetUMPromptNamesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00024980 File Offset: 0x00022B80
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetClientExtension", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("GetClientExtensionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ClientExtensionResponseType GetClientExtension([XmlElement("GetClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetClientExtensionType GetClientExtension1)
		{
			object[] array = this.Invoke("GetClientExtension", new object[]
			{
				GetClientExtension1
			});
			return (ClientExtensionResponseType)array[0];
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000249B0 File Offset: 0x00022BB0
		public IAsyncResult BeginGetClientExtension(GetClientExtensionType GetClientExtension1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetClientExtension", new object[]
			{
				GetClientExtension1
			}, callback, asyncState);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000249D8 File Offset: 0x00022BD8
		public ClientExtensionResponseType EndGetClientExtension(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ClientExtensionResponseType)array[0];
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000249F5 File Offset: 0x00022BF5
		public void GetClientExtensionAsync(GetClientExtensionType GetClientExtension1)
		{
			this.GetClientExtensionAsync(GetClientExtension1, null);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00024A00 File Offset: 0x00022C00
		public void GetClientExtensionAsync(GetClientExtensionType GetClientExtension1, object userState)
		{
			if (this.GetClientExtensionOperationCompleted == null)
			{
				this.GetClientExtensionOperationCompleted = new SendOrPostCallback(this.OnGetClientExtensionOperationCompleted);
			}
			base.InvokeAsync("GetClientExtension", new object[]
			{
				GetClientExtension1
			}, this.GetClientExtensionOperationCompleted, userState);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00024A48 File Offset: 0x00022C48
		private void OnGetClientExtensionOperationCompleted(object arg)
		{
			if (this.GetClientExtensionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetClientExtensionCompleted(this, new GetClientExtensionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00024A90 File Offset: 0x00022C90
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetClientExtension", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("SetClientExtensionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetClientExtensionResponseType SetClientExtension([XmlElement("SetClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetClientExtensionType SetClientExtension1)
		{
			object[] array = this.Invoke("SetClientExtension", new object[]
			{
				SetClientExtension1
			});
			return (SetClientExtensionResponseType)array[0];
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00024AC0 File Offset: 0x00022CC0
		public IAsyncResult BeginSetClientExtension(SetClientExtensionType SetClientExtension1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetClientExtension", new object[]
			{
				SetClientExtension1
			}, callback, asyncState);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00024AE8 File Offset: 0x00022CE8
		public SetClientExtensionResponseType EndSetClientExtension(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetClientExtensionResponseType)array[0];
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00024B05 File Offset: 0x00022D05
		public void SetClientExtensionAsync(SetClientExtensionType SetClientExtension1)
		{
			this.SetClientExtensionAsync(SetClientExtension1, null);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00024B10 File Offset: 0x00022D10
		public void SetClientExtensionAsync(SetClientExtensionType SetClientExtension1, object userState)
		{
			if (this.SetClientExtensionOperationCompleted == null)
			{
				this.SetClientExtensionOperationCompleted = new SendOrPostCallback(this.OnSetClientExtensionOperationCompleted);
			}
			base.InvokeAsync("SetClientExtension", new object[]
			{
				SetClientExtension1
			}, this.SetClientExtensionOperationCompleted, userState);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00024B58 File Offset: 0x00022D58
		private void OnSetClientExtensionOperationCompleted(object arg)
		{
			if (this.SetClientExtensionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetClientExtensionCompleted(this, new SetClientExtensionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00024BA0 File Offset: 0x00022DA0
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/StartFindInGALSpeechRecognition", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("StartFindInGALSpeechRecognitionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public StartFindInGALSpeechRecognitionResponseMessageType StartFindInGALSpeechRecognition([XmlElement("StartFindInGALSpeechRecognition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] StartFindInGALSpeechRecognitionType StartFindInGALSpeechRecognition1)
		{
			object[] array = this.Invoke("StartFindInGALSpeechRecognition", new object[]
			{
				StartFindInGALSpeechRecognition1
			});
			return (StartFindInGALSpeechRecognitionResponseMessageType)array[0];
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00024BD0 File Offset: 0x00022DD0
		public IAsyncResult BeginStartFindInGALSpeechRecognition(StartFindInGALSpeechRecognitionType StartFindInGALSpeechRecognition1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("StartFindInGALSpeechRecognition", new object[]
			{
				StartFindInGALSpeechRecognition1
			}, callback, asyncState);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00024BF8 File Offset: 0x00022DF8
		public StartFindInGALSpeechRecognitionResponseMessageType EndStartFindInGALSpeechRecognition(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (StartFindInGALSpeechRecognitionResponseMessageType)array[0];
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00024C15 File Offset: 0x00022E15
		public void StartFindInGALSpeechRecognitionAsync(StartFindInGALSpeechRecognitionType StartFindInGALSpeechRecognition1)
		{
			this.StartFindInGALSpeechRecognitionAsync(StartFindInGALSpeechRecognition1, null);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00024C20 File Offset: 0x00022E20
		public void StartFindInGALSpeechRecognitionAsync(StartFindInGALSpeechRecognitionType StartFindInGALSpeechRecognition1, object userState)
		{
			if (this.StartFindInGALSpeechRecognitionOperationCompleted == null)
			{
				this.StartFindInGALSpeechRecognitionOperationCompleted = new SendOrPostCallback(this.OnStartFindInGALSpeechRecognitionOperationCompleted);
			}
			base.InvokeAsync("StartFindInGALSpeechRecognition", new object[]
			{
				StartFindInGALSpeechRecognition1
			}, this.StartFindInGALSpeechRecognitionOperationCompleted, userState);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00024C68 File Offset: 0x00022E68
		private void OnStartFindInGALSpeechRecognitionOperationCompleted(object arg)
		{
			if (this.StartFindInGALSpeechRecognitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.StartFindInGALSpeechRecognitionCompleted(this, new StartFindInGALSpeechRecognitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00024CB0 File Offset: 0x00022EB0
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CompleteFindInGALSpeechRecognition", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("CompleteFindInGALSpeechRecognitionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CompleteFindInGALSpeechRecognitionResponseMessageType CompleteFindInGALSpeechRecognition([XmlElement("CompleteFindInGALSpeechRecognition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CompleteFindInGALSpeechRecognitionType CompleteFindInGALSpeechRecognition1)
		{
			object[] array = this.Invoke("CompleteFindInGALSpeechRecognition", new object[]
			{
				CompleteFindInGALSpeechRecognition1
			});
			return (CompleteFindInGALSpeechRecognitionResponseMessageType)array[0];
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00024CE0 File Offset: 0x00022EE0
		public IAsyncResult BeginCompleteFindInGALSpeechRecognition(CompleteFindInGALSpeechRecognitionType CompleteFindInGALSpeechRecognition1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CompleteFindInGALSpeechRecognition", new object[]
			{
				CompleteFindInGALSpeechRecognition1
			}, callback, asyncState);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00024D08 File Offset: 0x00022F08
		public CompleteFindInGALSpeechRecognitionResponseMessageType EndCompleteFindInGALSpeechRecognition(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CompleteFindInGALSpeechRecognitionResponseMessageType)array[0];
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00024D25 File Offset: 0x00022F25
		public void CompleteFindInGALSpeechRecognitionAsync(CompleteFindInGALSpeechRecognitionType CompleteFindInGALSpeechRecognition1)
		{
			this.CompleteFindInGALSpeechRecognitionAsync(CompleteFindInGALSpeechRecognition1, null);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00024D30 File Offset: 0x00022F30
		public void CompleteFindInGALSpeechRecognitionAsync(CompleteFindInGALSpeechRecognitionType CompleteFindInGALSpeechRecognition1, object userState)
		{
			if (this.CompleteFindInGALSpeechRecognitionOperationCompleted == null)
			{
				this.CompleteFindInGALSpeechRecognitionOperationCompleted = new SendOrPostCallback(this.OnCompleteFindInGALSpeechRecognitionOperationCompleted);
			}
			base.InvokeAsync("CompleteFindInGALSpeechRecognition", new object[]
			{
				CompleteFindInGALSpeechRecognition1
			}, this.CompleteFindInGALSpeechRecognitionOperationCompleted, userState);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00024D78 File Offset: 0x00022F78
		private void OnCompleteFindInGALSpeechRecognitionOperationCompleted(object arg)
		{
			if (this.CompleteFindInGALSpeechRecognitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CompleteFindInGALSpeechRecognitionCompleted(this, new CompleteFindInGALSpeechRecognitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00024DC0 File Offset: 0x00022FC0
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUMCallDataRecord", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("CreateUMCallDataRecordResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateUMCallDataRecordResponseMessageType CreateUMCallDataRecord([XmlElement("CreateUMCallDataRecord", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUMCallDataRecordType CreateUMCallDataRecord1)
		{
			object[] array = this.Invoke("CreateUMCallDataRecord", new object[]
			{
				CreateUMCallDataRecord1
			});
			return (CreateUMCallDataRecordResponseMessageType)array[0];
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00024DF0 File Offset: 0x00022FF0
		public IAsyncResult BeginCreateUMCallDataRecord(CreateUMCallDataRecordType CreateUMCallDataRecord1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateUMCallDataRecord", new object[]
			{
				CreateUMCallDataRecord1
			}, callback, asyncState);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00024E18 File Offset: 0x00023018
		public CreateUMCallDataRecordResponseMessageType EndCreateUMCallDataRecord(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateUMCallDataRecordResponseMessageType)array[0];
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00024E35 File Offset: 0x00023035
		public void CreateUMCallDataRecordAsync(CreateUMCallDataRecordType CreateUMCallDataRecord1)
		{
			this.CreateUMCallDataRecordAsync(CreateUMCallDataRecord1, null);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00024E40 File Offset: 0x00023040
		public void CreateUMCallDataRecordAsync(CreateUMCallDataRecordType CreateUMCallDataRecord1, object userState)
		{
			if (this.CreateUMCallDataRecordOperationCompleted == null)
			{
				this.CreateUMCallDataRecordOperationCompleted = new SendOrPostCallback(this.OnCreateUMCallDataRecordOperationCompleted);
			}
			base.InvokeAsync("CreateUMCallDataRecord", new object[]
			{
				CreateUMCallDataRecord1
			}, this.CreateUMCallDataRecordOperationCompleted, userState);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00024E88 File Offset: 0x00023088
		private void OnCreateUMCallDataRecordOperationCompleted(object arg)
		{
			if (this.CreateUMCallDataRecordCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateUMCallDataRecordCompleted(this, new CreateUMCallDataRecordCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00024ED0 File Offset: 0x000230D0
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMCallDataRecords", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUMCallDataRecordsResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMCallDataRecordsResponseMessageType GetUMCallDataRecords([XmlElement("GetUMCallDataRecords", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMCallDataRecordsType GetUMCallDataRecords1)
		{
			object[] array = this.Invoke("GetUMCallDataRecords", new object[]
			{
				GetUMCallDataRecords1
			});
			return (GetUMCallDataRecordsResponseMessageType)array[0];
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00024F00 File Offset: 0x00023100
		public IAsyncResult BeginGetUMCallDataRecords(GetUMCallDataRecordsType GetUMCallDataRecords1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUMCallDataRecords", new object[]
			{
				GetUMCallDataRecords1
			}, callback, asyncState);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00024F28 File Offset: 0x00023128
		public GetUMCallDataRecordsResponseMessageType EndGetUMCallDataRecords(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUMCallDataRecordsResponseMessageType)array[0];
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00024F45 File Offset: 0x00023145
		public void GetUMCallDataRecordsAsync(GetUMCallDataRecordsType GetUMCallDataRecords1)
		{
			this.GetUMCallDataRecordsAsync(GetUMCallDataRecords1, null);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00024F50 File Offset: 0x00023150
		public void GetUMCallDataRecordsAsync(GetUMCallDataRecordsType GetUMCallDataRecords1, object userState)
		{
			if (this.GetUMCallDataRecordsOperationCompleted == null)
			{
				this.GetUMCallDataRecordsOperationCompleted = new SendOrPostCallback(this.OnGetUMCallDataRecordsOperationCompleted);
			}
			base.InvokeAsync("GetUMCallDataRecords", new object[]
			{
				GetUMCallDataRecords1
			}, this.GetUMCallDataRecordsOperationCompleted, userState);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00024F98 File Offset: 0x00023198
		private void OnGetUMCallDataRecordsOperationCompleted(object arg)
		{
			if (this.GetUMCallDataRecordsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUMCallDataRecordsCompleted(this, new GetUMCallDataRecordsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00024FE0 File Offset: 0x000231E0
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMCallSummary", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUMCallSummaryResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMCallSummaryResponseMessageType GetUMCallSummary([XmlElement("GetUMCallSummary", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMCallSummaryType GetUMCallSummary1)
		{
			object[] array = this.Invoke("GetUMCallSummary", new object[]
			{
				GetUMCallSummary1
			});
			return (GetUMCallSummaryResponseMessageType)array[0];
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00025010 File Offset: 0x00023210
		public IAsyncResult BeginGetUMCallSummary(GetUMCallSummaryType GetUMCallSummary1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUMCallSummary", new object[]
			{
				GetUMCallSummary1
			}, callback, asyncState);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00025038 File Offset: 0x00023238
		public GetUMCallSummaryResponseMessageType EndGetUMCallSummary(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUMCallSummaryResponseMessageType)array[0];
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00025055 File Offset: 0x00023255
		public void GetUMCallSummaryAsync(GetUMCallSummaryType GetUMCallSummary1)
		{
			this.GetUMCallSummaryAsync(GetUMCallSummary1, null);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00025060 File Offset: 0x00023260
		public void GetUMCallSummaryAsync(GetUMCallSummaryType GetUMCallSummary1, object userState)
		{
			if (this.GetUMCallSummaryOperationCompleted == null)
			{
				this.GetUMCallSummaryOperationCompleted = new SendOrPostCallback(this.OnGetUMCallSummaryOperationCompleted);
			}
			base.InvokeAsync("GetUMCallSummary", new object[]
			{
				GetUMCallSummary1
			}, this.GetUMCallSummaryOperationCompleted, userState);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000250A8 File Offset: 0x000232A8
		private void OnGetUMCallSummaryOperationCompleted(object arg)
		{
			if (this.GetUMCallSummaryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUMCallSummaryCompleted(this, new GetUMCallSummaryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000250F0 File Offset: 0x000232F0
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/InitUMMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("InitUMMailboxResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public InitUMMailboxResponseMessageType InitUMMailbox([XmlElement("InitUMMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] InitUMMailboxType InitUMMailbox1)
		{
			object[] array = this.Invoke("InitUMMailbox", new object[]
			{
				InitUMMailbox1
			});
			return (InitUMMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00025120 File Offset: 0x00023320
		public IAsyncResult BeginInitUMMailbox(InitUMMailboxType InitUMMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InitUMMailbox", new object[]
			{
				InitUMMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00025148 File Offset: 0x00023348
		public InitUMMailboxResponseMessageType EndInitUMMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (InitUMMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00025165 File Offset: 0x00023365
		public void InitUMMailboxAsync(InitUMMailboxType InitUMMailbox1)
		{
			this.InitUMMailboxAsync(InitUMMailbox1, null);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00025170 File Offset: 0x00023370
		public void InitUMMailboxAsync(InitUMMailboxType InitUMMailbox1, object userState)
		{
			if (this.InitUMMailboxOperationCompleted == null)
			{
				this.InitUMMailboxOperationCompleted = new SendOrPostCallback(this.OnInitUMMailboxOperationCompleted);
			}
			base.InvokeAsync("InitUMMailbox", new object[]
			{
				InitUMMailbox1
			}, this.InitUMMailboxOperationCompleted, userState);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000251B8 File Offset: 0x000233B8
		private void OnInitUMMailboxOperationCompleted(object arg)
		{
			if (this.InitUMMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InitUMMailboxCompleted(this, new InitUMMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00025200 File Offset: 0x00023400
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ResetUMMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("ResetUMMailboxResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ResetUMMailboxResponseMessageType ResetUMMailbox([XmlElement("ResetUMMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ResetUMMailboxType ResetUMMailbox1)
		{
			object[] array = this.Invoke("ResetUMMailbox", new object[]
			{
				ResetUMMailbox1
			});
			return (ResetUMMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00025230 File Offset: 0x00023430
		public IAsyncResult BeginResetUMMailbox(ResetUMMailboxType ResetUMMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResetUMMailbox", new object[]
			{
				ResetUMMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00025258 File Offset: 0x00023458
		public ResetUMMailboxResponseMessageType EndResetUMMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ResetUMMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00025275 File Offset: 0x00023475
		public void ResetUMMailboxAsync(ResetUMMailboxType ResetUMMailbox1)
		{
			this.ResetUMMailboxAsync(ResetUMMailbox1, null);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00025280 File Offset: 0x00023480
		public void ResetUMMailboxAsync(ResetUMMailboxType ResetUMMailbox1, object userState)
		{
			if (this.ResetUMMailboxOperationCompleted == null)
			{
				this.ResetUMMailboxOperationCompleted = new SendOrPostCallback(this.OnResetUMMailboxOperationCompleted);
			}
			base.InvokeAsync("ResetUMMailbox", new object[]
			{
				ResetUMMailbox1
			}, this.ResetUMMailboxOperationCompleted, userState);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000252C8 File Offset: 0x000234C8
		private void OnResetUMMailboxOperationCompleted(object arg)
		{
			if (this.ResetUMMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResetUMMailboxCompleted(this, new ResetUMMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00025310 File Offset: 0x00023510
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ValidateUMPin", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("ValidateUMPinResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ValidateUMPinResponseMessageType ValidateUMPin([XmlElement("ValidateUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ValidateUMPinType ValidateUMPin1)
		{
			object[] array = this.Invoke("ValidateUMPin", new object[]
			{
				ValidateUMPin1
			});
			return (ValidateUMPinResponseMessageType)array[0];
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00025340 File Offset: 0x00023540
		public IAsyncResult BeginValidateUMPin(ValidateUMPinType ValidateUMPin1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ValidateUMPin", new object[]
			{
				ValidateUMPin1
			}, callback, asyncState);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00025368 File Offset: 0x00023568
		public ValidateUMPinResponseMessageType EndValidateUMPin(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ValidateUMPinResponseMessageType)array[0];
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00025385 File Offset: 0x00023585
		public void ValidateUMPinAsync(ValidateUMPinType ValidateUMPin1)
		{
			this.ValidateUMPinAsync(ValidateUMPin1, null);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00025390 File Offset: 0x00023590
		public void ValidateUMPinAsync(ValidateUMPinType ValidateUMPin1, object userState)
		{
			if (this.ValidateUMPinOperationCompleted == null)
			{
				this.ValidateUMPinOperationCompleted = new SendOrPostCallback(this.OnValidateUMPinOperationCompleted);
			}
			base.InvokeAsync("ValidateUMPin", new object[]
			{
				ValidateUMPin1
			}, this.ValidateUMPinOperationCompleted, userState);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000253D8 File Offset: 0x000235D8
		private void OnValidateUMPinOperationCompleted(object arg)
		{
			if (this.ValidateUMPinCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ValidateUMPinCompleted(this, new ValidateUMPinCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00025420 File Offset: 0x00023620
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SaveUMPin", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("SaveUMPinResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SaveUMPinResponseMessageType SaveUMPin([XmlElement("SaveUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SaveUMPinType SaveUMPin1)
		{
			object[] array = this.Invoke("SaveUMPin", new object[]
			{
				SaveUMPin1
			});
			return (SaveUMPinResponseMessageType)array[0];
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00025450 File Offset: 0x00023650
		public IAsyncResult BeginSaveUMPin(SaveUMPinType SaveUMPin1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SaveUMPin", new object[]
			{
				SaveUMPin1
			}, callback, asyncState);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00025478 File Offset: 0x00023678
		public SaveUMPinResponseMessageType EndSaveUMPin(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SaveUMPinResponseMessageType)array[0];
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00025495 File Offset: 0x00023695
		public void SaveUMPinAsync(SaveUMPinType SaveUMPin1)
		{
			this.SaveUMPinAsync(SaveUMPin1, null);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000254A0 File Offset: 0x000236A0
		public void SaveUMPinAsync(SaveUMPinType SaveUMPin1, object userState)
		{
			if (this.SaveUMPinOperationCompleted == null)
			{
				this.SaveUMPinOperationCompleted = new SendOrPostCallback(this.OnSaveUMPinOperationCompleted);
			}
			base.InvokeAsync("SaveUMPin", new object[]
			{
				SaveUMPin1
			}, this.SaveUMPinOperationCompleted, userState);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000254E8 File Offset: 0x000236E8
		private void OnSaveUMPinOperationCompleted(object arg)
		{
			if (this.SaveUMPinCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SaveUMPinCompleted(this, new SaveUMPinCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00025530 File Offset: 0x00023730
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMPin", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("GetUMPinResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMPinResponseMessageType GetUMPin([XmlElement("GetUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMPinType GetUMPin1)
		{
			object[] array = this.Invoke("GetUMPin", new object[]
			{
				GetUMPin1
			});
			return (GetUMPinResponseMessageType)array[0];
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00025560 File Offset: 0x00023760
		public IAsyncResult BeginGetUMPin(GetUMPinType GetUMPin1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUMPin", new object[]
			{
				GetUMPin1
			}, callback, asyncState);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00025588 File Offset: 0x00023788
		public GetUMPinResponseMessageType EndGetUMPin(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUMPinResponseMessageType)array[0];
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000255A5 File Offset: 0x000237A5
		public void GetUMPinAsync(GetUMPinType GetUMPin1)
		{
			this.GetUMPinAsync(GetUMPin1, null);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000255B0 File Offset: 0x000237B0
		public void GetUMPinAsync(GetUMPinType GetUMPin1, object userState)
		{
			if (this.GetUMPinOperationCompleted == null)
			{
				this.GetUMPinOperationCompleted = new SendOrPostCallback(this.OnGetUMPinOperationCompleted);
			}
			base.InvokeAsync("GetUMPin", new object[]
			{
				GetUMPin1
			}, this.GetUMPinOperationCompleted, userState);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000255F8 File Offset: 0x000237F8
		private void OnGetUMPinOperationCompleted(object arg)
		{
			if (this.GetUMPinCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUMPinCompleted(this, new GetUMPinCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00025640 File Offset: 0x00023840
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetClientIntent", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("GetClientIntentResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetClientIntentResponseMessageType GetClientIntent([XmlElement("GetClientIntent", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetClientIntentType GetClientIntent1)
		{
			object[] array = this.Invoke("GetClientIntent", new object[]
			{
				GetClientIntent1
			});
			return (GetClientIntentResponseMessageType)array[0];
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00025670 File Offset: 0x00023870
		public IAsyncResult BeginGetClientIntent(GetClientIntentType GetClientIntent1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetClientIntent", new object[]
			{
				GetClientIntent1
			}, callback, asyncState);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00025698 File Offset: 0x00023898
		public GetClientIntentResponseMessageType EndGetClientIntent(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetClientIntentResponseMessageType)array[0];
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000256B5 File Offset: 0x000238B5
		public void GetClientIntentAsync(GetClientIntentType GetClientIntent1)
		{
			this.GetClientIntentAsync(GetClientIntent1, null);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000256C0 File Offset: 0x000238C0
		public void GetClientIntentAsync(GetClientIntentType GetClientIntent1, object userState)
		{
			if (this.GetClientIntentOperationCompleted == null)
			{
				this.GetClientIntentOperationCompleted = new SendOrPostCallback(this.OnGetClientIntentOperationCompleted);
			}
			base.InvokeAsync("GetClientIntent", new object[]
			{
				GetClientIntent1
			}, this.GetClientIntentOperationCompleted, userState);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00025708 File Offset: 0x00023908
		private void OnGetClientIntentOperationCompleted(object arg)
		{
			if (this.GetClientIntentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetClientIntentCompleted(this, new GetClientIntentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00025750 File Offset: 0x00023950
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUMSubscriberCallAnsweringData", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetUMSubscriberCallAnsweringDataResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUMSubscriberCallAnsweringDataResponseMessageType GetUMSubscriberCallAnsweringData([XmlElement("GetUMSubscriberCallAnsweringData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUMSubscriberCallAnsweringDataType GetUMSubscriberCallAnsweringData1)
		{
			object[] array = this.Invoke("GetUMSubscriberCallAnsweringData", new object[]
			{
				GetUMSubscriberCallAnsweringData1
			});
			return (GetUMSubscriberCallAnsweringDataResponseMessageType)array[0];
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00025780 File Offset: 0x00023980
		public IAsyncResult BeginGetUMSubscriberCallAnsweringData(GetUMSubscriberCallAnsweringDataType GetUMSubscriberCallAnsweringData1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUMSubscriberCallAnsweringData", new object[]
			{
				GetUMSubscriberCallAnsweringData1
			}, callback, asyncState);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000257A8 File Offset: 0x000239A8
		public GetUMSubscriberCallAnsweringDataResponseMessageType EndGetUMSubscriberCallAnsweringData(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUMSubscriberCallAnsweringDataResponseMessageType)array[0];
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000257C5 File Offset: 0x000239C5
		public void GetUMSubscriberCallAnsweringDataAsync(GetUMSubscriberCallAnsweringDataType GetUMSubscriberCallAnsweringData1)
		{
			this.GetUMSubscriberCallAnsweringDataAsync(GetUMSubscriberCallAnsweringData1, null);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000257D0 File Offset: 0x000239D0
		public void GetUMSubscriberCallAnsweringDataAsync(GetUMSubscriberCallAnsweringDataType GetUMSubscriberCallAnsweringData1, object userState)
		{
			if (this.GetUMSubscriberCallAnsweringDataOperationCompleted == null)
			{
				this.GetUMSubscriberCallAnsweringDataOperationCompleted = new SendOrPostCallback(this.OnGetUMSubscriberCallAnsweringDataOperationCompleted);
			}
			base.InvokeAsync("GetUMSubscriberCallAnsweringData", new object[]
			{
				GetUMSubscriberCallAnsweringData1
			}, this.GetUMSubscriberCallAnsweringDataOperationCompleted, userState);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00025818 File Offset: 0x00023A18
		private void OnGetUMSubscriberCallAnsweringDataOperationCompleted(object arg)
		{
			if (this.GetUMSubscriberCallAnsweringDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUMSubscriberCallAnsweringDataCompleted(this, new GetUMSubscriberCallAnsweringDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00025860 File Offset: 0x00023A60
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateMailboxAssociation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("UpdateMailboxAssociationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateMailboxAssociationResponseType UpdateMailboxAssociation([XmlElement("UpdateMailboxAssociation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateMailboxAssociationType UpdateMailboxAssociation1)
		{
			object[] array = this.Invoke("UpdateMailboxAssociation", new object[]
			{
				UpdateMailboxAssociation1
			});
			return (UpdateMailboxAssociationResponseType)array[0];
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00025890 File Offset: 0x00023A90
		public IAsyncResult BeginUpdateMailboxAssociation(UpdateMailboxAssociationType UpdateMailboxAssociation1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateMailboxAssociation", new object[]
			{
				UpdateMailboxAssociation1
			}, callback, asyncState);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000258B8 File Offset: 0x00023AB8
		public UpdateMailboxAssociationResponseType EndUpdateMailboxAssociation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateMailboxAssociationResponseType)array[0];
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000258D5 File Offset: 0x00023AD5
		public void UpdateMailboxAssociationAsync(UpdateMailboxAssociationType UpdateMailboxAssociation1)
		{
			this.UpdateMailboxAssociationAsync(UpdateMailboxAssociation1, null);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000258E0 File Offset: 0x00023AE0
		public void UpdateMailboxAssociationAsync(UpdateMailboxAssociationType UpdateMailboxAssociation1, object userState)
		{
			if (this.UpdateMailboxAssociationOperationCompleted == null)
			{
				this.UpdateMailboxAssociationOperationCompleted = new SendOrPostCallback(this.OnUpdateMailboxAssociationOperationCompleted);
			}
			base.InvokeAsync("UpdateMailboxAssociation", new object[]
			{
				UpdateMailboxAssociation1
			}, this.UpdateMailboxAssociationOperationCompleted, userState);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00025928 File Offset: 0x00023B28
		private void OnUpdateMailboxAssociationOperationCompleted(object arg)
		{
			if (this.UpdateMailboxAssociationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateMailboxAssociationCompleted(this, new UpdateMailboxAssociationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00025970 File Offset: 0x00023B70
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateGroupMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UpdateGroupMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateGroupMailboxResponseType UpdateGroupMailbox([XmlElement("UpdateGroupMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateGroupMailboxType UpdateGroupMailbox1)
		{
			object[] array = this.Invoke("UpdateGroupMailbox", new object[]
			{
				UpdateGroupMailbox1
			});
			return (UpdateGroupMailboxResponseType)array[0];
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000259A0 File Offset: 0x00023BA0
		public IAsyncResult BeginUpdateGroupMailbox(UpdateGroupMailboxType UpdateGroupMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateGroupMailbox", new object[]
			{
				UpdateGroupMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000259C8 File Offset: 0x00023BC8
		public UpdateGroupMailboxResponseType EndUpdateGroupMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateGroupMailboxResponseType)array[0];
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000259E5 File Offset: 0x00023BE5
		public void UpdateGroupMailboxAsync(UpdateGroupMailboxType UpdateGroupMailbox1)
		{
			this.UpdateGroupMailboxAsync(UpdateGroupMailbox1, null);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000259F0 File Offset: 0x00023BF0
		public void UpdateGroupMailboxAsync(UpdateGroupMailboxType UpdateGroupMailbox1, object userState)
		{
			if (this.UpdateGroupMailboxOperationCompleted == null)
			{
				this.UpdateGroupMailboxOperationCompleted = new SendOrPostCallback(this.OnUpdateGroupMailboxOperationCompleted);
			}
			base.InvokeAsync("UpdateGroupMailbox", new object[]
			{
				UpdateGroupMailbox1
			}, this.UpdateGroupMailboxOperationCompleted, userState);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00025A38 File Offset: 0x00023C38
		private void OnUpdateGroupMailboxOperationCompleted(object arg)
		{
			if (this.UpdateGroupMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateGroupMailboxCompleted(this, new UpdateGroupMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00025A80 File Offset: 0x00023C80
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/PostModernGroupItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("PostModernGroupItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public PostModernGroupItemResponseType PostModernGroupItem([XmlElement("PostModernGroupItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] PostModernGroupItemType PostModernGroupItem1)
		{
			object[] array = this.Invoke("PostModernGroupItem", new object[]
			{
				PostModernGroupItem1
			});
			return (PostModernGroupItemResponseType)array[0];
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00025AB0 File Offset: 0x00023CB0
		public IAsyncResult BeginPostModernGroupItem(PostModernGroupItemType PostModernGroupItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PostModernGroupItem", new object[]
			{
				PostModernGroupItem1
			}, callback, asyncState);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00025AD8 File Offset: 0x00023CD8
		public PostModernGroupItemResponseType EndPostModernGroupItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (PostModernGroupItemResponseType)array[0];
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00025AF5 File Offset: 0x00023CF5
		public void PostModernGroupItemAsync(PostModernGroupItemType PostModernGroupItem1)
		{
			this.PostModernGroupItemAsync(PostModernGroupItem1, null);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00025B00 File Offset: 0x00023D00
		public void PostModernGroupItemAsync(PostModernGroupItemType PostModernGroupItem1, object userState)
		{
			if (this.PostModernGroupItemOperationCompleted == null)
			{
				this.PostModernGroupItemOperationCompleted = new SendOrPostCallback(this.OnPostModernGroupItemOperationCompleted);
			}
			base.InvokeAsync("PostModernGroupItem", new object[]
			{
				PostModernGroupItem1
			}, this.PostModernGroupItemOperationCompleted, userState);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00025B48 File Offset: 0x00023D48
		private void OnPostModernGroupItemOperationCompleted(object arg)
		{
			if (this.PostModernGroupItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PostModernGroupItemCompleted(this, new PostModernGroupItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00025B8D File Offset: 0x00023D8D
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00025B96 File Offset: 0x00023D96
		internal override XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return Constants.EwsNamespaces;
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00025B9D File Offset: 0x00023D9D
		public PrivateExchangeServiceBinding(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(componentId, remoteCertificateValidationCallback, true)
		{
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00025BA8 File Offset: 0x00023DA8
		public PrivateExchangeServiceBinding(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback, bool normalization) : base(componentId, remoteCertificateValidationCallback, normalization)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00025BB4 File Offset: 0x00023DB4
		protected void SetClientRequestIdHeaderFromActivityId()
		{
			Guid? activityId = ActivityContext.ActivityId;
			if (activityId != null)
			{
				base.HttpHeaders["client-request-id"] = activityId.ToString();
				return;
			}
			PrivateExchangeServiceBinding.Tracer.TraceWarning((long)this.GetHashCode(), "ActivityContext.ActivityId is null. Request will omit the client-request-id header.");
		}

		// Token: 0x04000780 RID: 1920
		public RequestServerVersion RequestServerVersionValue;

		// Token: 0x04000781 RID: 1921
		public ServerVersionInfo ServerVersionInfoValue;

		// Token: 0x04000782 RID: 1922
		private SendOrPostCallback CreateUMPromptOperationCompleted;

		// Token: 0x04000783 RID: 1923
		private SendOrPostCallback DeleteUMPromptsOperationCompleted;

		// Token: 0x04000784 RID: 1924
		private SendOrPostCallback GetUMPromptOperationCompleted;

		// Token: 0x04000785 RID: 1925
		private SendOrPostCallback GetUMPromptNamesOperationCompleted;

		// Token: 0x04000786 RID: 1926
		private SendOrPostCallback GetClientExtensionOperationCompleted;

		// Token: 0x04000787 RID: 1927
		private SendOrPostCallback SetClientExtensionOperationCompleted;

		// Token: 0x04000788 RID: 1928
		private SendOrPostCallback StartFindInGALSpeechRecognitionOperationCompleted;

		// Token: 0x04000789 RID: 1929
		private SendOrPostCallback CompleteFindInGALSpeechRecognitionOperationCompleted;

		// Token: 0x0400078A RID: 1930
		private SendOrPostCallback CreateUMCallDataRecordOperationCompleted;

		// Token: 0x0400078B RID: 1931
		private SendOrPostCallback GetUMCallDataRecordsOperationCompleted;

		// Token: 0x0400078C RID: 1932
		private SendOrPostCallback GetUMCallSummaryOperationCompleted;

		// Token: 0x0400078D RID: 1933
		private SendOrPostCallback InitUMMailboxOperationCompleted;

		// Token: 0x0400078E RID: 1934
		private SendOrPostCallback ResetUMMailboxOperationCompleted;

		// Token: 0x0400078F RID: 1935
		private SendOrPostCallback ValidateUMPinOperationCompleted;

		// Token: 0x04000790 RID: 1936
		private SendOrPostCallback SaveUMPinOperationCompleted;

		// Token: 0x04000791 RID: 1937
		private SendOrPostCallback GetUMPinOperationCompleted;

		// Token: 0x04000792 RID: 1938
		private SendOrPostCallback GetClientIntentOperationCompleted;

		// Token: 0x04000793 RID: 1939
		private SendOrPostCallback GetUMSubscriberCallAnsweringDataOperationCompleted;

		// Token: 0x04000794 RID: 1940
		private SendOrPostCallback UpdateMailboxAssociationOperationCompleted;

		// Token: 0x04000795 RID: 1941
		private SendOrPostCallback UpdateGroupMailboxOperationCompleted;

		// Token: 0x04000796 RID: 1942
		private SendOrPostCallback PostModernGroupItemOperationCompleted;

		// Token: 0x040007AC RID: 1964
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.EwsClientTracer;
	}
}
