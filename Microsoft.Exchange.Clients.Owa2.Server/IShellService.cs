using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;
using Microsoft.Online.BOX.UI.Shell.AllSettings;

// Token: 0x02000086 RID: 134
[ServiceContract(ConfigurationName = "IShellService")]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IShellService
{
	// Token: 0x060004EB RID: 1259
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/GetMetaDataShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[OperationContract(Action = "http://tempuri.org/IShellService/GetMetaData", ReplyAction = "http://tempuri.org/IShellService/GetMetaDataResponse")]
	string GetMetaData(BrandInfo brandInfo, string locale, UserInfo userInfo, Options options);

	// Token: 0x060004EC RID: 1260
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/GetMetaData", ReplyAction = "http://tempuri.org/IShellService/GetMetaDataResponse")]
	IAsyncResult BeginGetMetaData(BrandInfo brandInfo, string locale, UserInfo userInfo, Options options, AsyncCallback callback, object asyncState);

	// Token: 0x060004ED RID: 1261
	string EndGetMetaData(IAsyncResult result);

	// Token: 0x060004EE RID: 1262
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/GetNavBarInfoShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[OperationContract(Action = "http://tempuri.org/IShellService/GetNavBarInfo", ReplyAction = "http://tempuri.org/IShellService/GetNavBarInfoResponse")]
	NavBarInfo GetNavBarInfo(NavBarInfoRequest navBarInfoRequest);

	// Token: 0x060004EF RID: 1263
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/GetNavBarInfo", ReplyAction = "http://tempuri.org/IShellService/GetNavBarInfoResponse")]
	IAsyncResult BeginGetNavBarInfo(NavBarInfoRequest navBarInfoRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060004F0 RID: 1264
	NavBarInfo EndGetNavBarInfo(IAsyncResult result);

	// Token: 0x060004F1 RID: 1265
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/SetYammerEnabledShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[OperationContract(Action = "http://tempuri.org/IShellService/SetYammerEnabled", ReplyAction = "http://tempuri.org/IShellService/SetYammerEnabledResponse")]
	void SetYammerEnabled(SetYammerEnabledRequest setYammerEnabledRequest);

	// Token: 0x060004F2 RID: 1266
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/SetYammerEnabled", ReplyAction = "http://tempuri.org/IShellService/SetYammerEnabledResponse")]
	IAsyncResult BeginSetYammerEnabled(SetYammerEnabledRequest setYammerEnabledRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060004F3 RID: 1267
	void EndSetYammerEnabled(IAsyncResult result);

	// Token: 0x060004F4 RID: 1268
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/GetShellInfoShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[OperationContract(Action = "http://tempuri.org/IShellService/GetShellInfo", ReplyAction = "http://tempuri.org/IShellService/GetShellInfoResponse")]
	ShellInfo GetShellInfo(ShellInfoRequest shellInfoRequest);

	// Token: 0x060004F5 RID: 1269
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/GetShellInfo", ReplyAction = "http://tempuri.org/IShellService/GetShellInfoResponse")]
	IAsyncResult BeginGetShellInfo(ShellInfoRequest shellInfoRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060004F6 RID: 1270
	ShellInfo EndGetShellInfo(IAsyncResult result);

	// Token: 0x060004F7 RID: 1271
	[OperationContract(Action = "http://tempuri.org/IShellService/SetUserTheme", ReplyAction = "http://tempuri.org/IShellService/SetUserThemeResponse")]
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/SetUserThemeShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	void SetUserTheme(SetUserThemeRequest setUserThemeRequest);

	// Token: 0x060004F8 RID: 1272
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/SetUserTheme", ReplyAction = "http://tempuri.org/IShellService/SetUserThemeResponse")]
	IAsyncResult BeginSetUserTheme(SetUserThemeRequest setUserThemeRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060004F9 RID: 1273
	void EndSetUserTheme(IAsyncResult result);

	// Token: 0x060004FA RID: 1274
	[OperationContract(Action = "http://tempuri.org/IShellService/DoNothing", ReplyAction = "http://tempuri.org/IShellService/DoNothingResponse")]
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/DoNothingShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	void DoNothing(NavBarData ignored0);

	// Token: 0x060004FB RID: 1275
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/DoNothing", ReplyAction = "http://tempuri.org/IShellService/DoNothingResponse")]
	IAsyncResult BeginDoNothing(NavBarData ignored0, AsyncCallback callback, object asyncState);

	// Token: 0x060004FC RID: 1276
	void EndDoNothing(IAsyncResult result);

	// Token: 0x060004FD RID: 1277
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/GetAlertsShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[OperationContract(Action = "http://tempuri.org/IShellService/GetAlerts", ReplyAction = "http://tempuri.org/IShellService/GetAlertsResponse")]
	Alert[] GetAlerts(GetAlertRequest getAlertRequest);

	// Token: 0x060004FE RID: 1278
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/GetAlerts", ReplyAction = "http://tempuri.org/IShellService/GetAlertsResponse")]
	IAsyncResult BeginGetAlerts(GetAlertRequest getAlertRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060004FF RID: 1279
	Alert[] EndGetAlerts(IAsyncResult result);

	// Token: 0x06000500 RID: 1280
	[OperationContract(Action = "http://tempuri.org/IShellService/GetSuiteServiceInfo", ReplyAction = "http://tempuri.org/IShellService/GetSuiteServiceInfoResponse")]
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/GetSuiteServiceInfoShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	SuiteServiceInfo GetSuiteServiceInfo(GetSuiteServiceInfoRequest getSuiteServiceInfoRequest);

	// Token: 0x06000501 RID: 1281
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/GetSuiteServiceInfo", ReplyAction = "http://tempuri.org/IShellService/GetSuiteServiceInfoResponse")]
	IAsyncResult BeginGetSuiteServiceInfo(GetSuiteServiceInfoRequest getSuiteServiceInfoRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06000502 RID: 1282
	SuiteServiceInfo EndGetSuiteServiceInfo(IAsyncResult result);

	// Token: 0x06000503 RID: 1283
	[OperationContract(Action = "http://tempuri.org/IShellService/GetShellSettings", ReplyAction = "http://tempuri.org/IShellService/GetShellSettingsResponse")]
	[FaultContract(typeof(ShellWebServiceFault), Action = "http://tempuri.org/IShellService/GetShellSettingsShellWebServiceFaultFault", Name = "ShellWebServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	ShellSettingsResponse GetShellSettings(ShellSettingsRequest shellSettingsRequest);

	// Token: 0x06000504 RID: 1284
	[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IShellService/GetShellSettings", ReplyAction = "http://tempuri.org/IShellService/GetShellSettingsResponse")]
	IAsyncResult BeginGetShellSettings(ShellSettingsRequest shellSettingsRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06000505 RID: 1285
	ShellSettingsResponse EndGetShellSettings(IAsyncResult result);
}
