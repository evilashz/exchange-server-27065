using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;
using Microsoft.Online.BOX.UI.Shell.AllSettings;

// Token: 0x02000088 RID: 136
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class ShellServiceClient : ClientBase<IShellService>, IShellService
{
	// Token: 0x06000506 RID: 1286 RVA: 0x0000E87B File Offset: 0x0000CA7B
	public ShellServiceClient()
	{
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0000E883 File Offset: 0x0000CA83
	public ShellServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0000E88C File Offset: 0x0000CA8C
	public ShellServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0000E896 File Offset: 0x0000CA96
	public ShellServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
	public ShellServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0000E8AA File Offset: 0x0000CAAA
	public string GetMetaData(BrandInfo brandInfo, string locale, UserInfo userInfo, Options options)
	{
		return base.Channel.GetMetaData(brandInfo, locale, userInfo, options);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0000E8BC File Offset: 0x0000CABC
	public IAsyncResult BeginGetMetaData(BrandInfo brandInfo, string locale, UserInfo userInfo, Options options, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetMetaData(brandInfo, locale, userInfo, options, callback, asyncState);
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0000E8D2 File Offset: 0x0000CAD2
	public string EndGetMetaData(IAsyncResult result)
	{
		return base.Channel.EndGetMetaData(result);
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
	public NavBarInfo GetNavBarInfo(NavBarInfoRequest navBarInfoRequest)
	{
		return base.Channel.GetNavBarInfo(navBarInfoRequest);
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0000E8EE File Offset: 0x0000CAEE
	public IAsyncResult BeginGetNavBarInfo(NavBarInfoRequest navBarInfoRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetNavBarInfo(navBarInfoRequest, callback, asyncState);
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0000E8FE File Offset: 0x0000CAFE
	public NavBarInfo EndGetNavBarInfo(IAsyncResult result)
	{
		return base.Channel.EndGetNavBarInfo(result);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0000E90C File Offset: 0x0000CB0C
	public void SetYammerEnabled(SetYammerEnabledRequest setYammerEnabledRequest)
	{
		base.Channel.SetYammerEnabled(setYammerEnabledRequest);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0000E91A File Offset: 0x0000CB1A
	public IAsyncResult BeginSetYammerEnabled(SetYammerEnabledRequest setYammerEnabledRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginSetYammerEnabled(setYammerEnabledRequest, callback, asyncState);
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0000E92A File Offset: 0x0000CB2A
	public void EndSetYammerEnabled(IAsyncResult result)
	{
		base.Channel.EndSetYammerEnabled(result);
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0000E938 File Offset: 0x0000CB38
	public ShellInfo GetShellInfo(ShellInfoRequest shellInfoRequest)
	{
		return base.Channel.GetShellInfo(shellInfoRequest);
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0000E946 File Offset: 0x0000CB46
	public IAsyncResult BeginGetShellInfo(ShellInfoRequest shellInfoRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetShellInfo(shellInfoRequest, callback, asyncState);
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0000E956 File Offset: 0x0000CB56
	public ShellInfo EndGetShellInfo(IAsyncResult result)
	{
		return base.Channel.EndGetShellInfo(result);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0000E964 File Offset: 0x0000CB64
	public void SetUserTheme(SetUserThemeRequest setUserThemeRequest)
	{
		base.Channel.SetUserTheme(setUserThemeRequest);
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0000E972 File Offset: 0x0000CB72
	public IAsyncResult BeginSetUserTheme(SetUserThemeRequest setUserThemeRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginSetUserTheme(setUserThemeRequest, callback, asyncState);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0000E982 File Offset: 0x0000CB82
	public void EndSetUserTheme(IAsyncResult result)
	{
		base.Channel.EndSetUserTheme(result);
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0000E990 File Offset: 0x0000CB90
	public void DoNothing(NavBarData ignored0)
	{
		base.Channel.DoNothing(ignored0);
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0000E99E File Offset: 0x0000CB9E
	public IAsyncResult BeginDoNothing(NavBarData ignored0, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginDoNothing(ignored0, callback, asyncState);
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0000E9AE File Offset: 0x0000CBAE
	public void EndDoNothing(IAsyncResult result)
	{
		base.Channel.EndDoNothing(result);
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0000E9BC File Offset: 0x0000CBBC
	public Alert[] GetAlerts(GetAlertRequest getAlertRequest)
	{
		return base.Channel.GetAlerts(getAlertRequest);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0000E9CA File Offset: 0x0000CBCA
	public IAsyncResult BeginGetAlerts(GetAlertRequest getAlertRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetAlerts(getAlertRequest, callback, asyncState);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0000E9DA File Offset: 0x0000CBDA
	public Alert[] EndGetAlerts(IAsyncResult result)
	{
		return base.Channel.EndGetAlerts(result);
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
	public SuiteServiceInfo GetSuiteServiceInfo(GetSuiteServiceInfoRequest getSuiteServiceInfoRequest)
	{
		return base.Channel.GetSuiteServiceInfo(getSuiteServiceInfoRequest);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0000E9F6 File Offset: 0x0000CBF6
	public IAsyncResult BeginGetSuiteServiceInfo(GetSuiteServiceInfoRequest getSuiteServiceInfoRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetSuiteServiceInfo(getSuiteServiceInfoRequest, callback, asyncState);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0000EA06 File Offset: 0x0000CC06
	public SuiteServiceInfo EndGetSuiteServiceInfo(IAsyncResult result)
	{
		return base.Channel.EndGetSuiteServiceInfo(result);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0000EA14 File Offset: 0x0000CC14
	public ShellSettingsResponse GetShellSettings(ShellSettingsRequest shellSettingsRequest)
	{
		return base.Channel.GetShellSettings(shellSettingsRequest);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0000EA22 File Offset: 0x0000CC22
	public IAsyncResult BeginGetShellSettings(ShellSettingsRequest shellSettingsRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetShellSettings(shellSettingsRequest, callback, asyncState);
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0000EA32 File Offset: 0x0000CC32
	public ShellSettingsResponse EndGetShellSettings(IAsyncResult result)
	{
		return base.Channel.EndGetShellSettings(result);
	}
}
