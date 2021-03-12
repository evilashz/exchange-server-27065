using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000292 RID: 658
	public enum TimeId
	{
		// Token: 0x04000EC7 RID: 3783
		[TimeId("Root")]
		Root,
		// Token: 0x04000EC8 RID: 3784
		[TimeId("H_BPR")]
		HandlerBeginProcessRequest,
		// Token: 0x04000EC9 RID: 3785
		[TimeId("H_PLC")]
		HandlerProxyLoginCommand,
		// Token: 0x04000ECA RID: 3786
		[TimeId("H_PR")]
		HandlerProxyRequest,
		// Token: 0x04000ECB RID: 3787
		[TimeId("H_SR")]
		HandlerScheduleTask,
		// Token: 0x04000ECC RID: 3788
		[TimeId("H_EPR")]
		HandlerEndProcessRequest,
		// Token: 0x04000ECD RID: 3789
		[TimeId("H_IRD")]
		HandlerIISRecordData,
		// Token: 0x04000ECE RID: 3790
		[TimeId("H_RMBP")]
		HandlerRequestMustBeProxied,
		// Token: 0x04000ECF RID: 3791
		[TimeId("H_CHRC")]
		HandlerCompleteHttpRequestCallback,
		// Token: 0x04000ED0 RID: 3792
		[TimeId("H_SndW")]
		HandlerSendWatson,
		// Token: 0x04000ED1 RID: 3793
		[TimeId("H_CRWD")]
		HandlerCompleteRequestWithDelay,
		// Token: 0x04000ED2 RID: 3794
		[TimeId("H_HPL")]
		HandlerHandleProxyLogin,
		// Token: 0x04000ED3 RID: 3795
		[TimeId("H_RMBP")]
		HandlerRequestsMustBeProxied,
		// Token: 0x04000ED4 RID: 3796
		[TimeId("H_SDLE")]
		HandlerServiceDiscoveryLookupExternal,
		// Token: 0x04000ED5 RID: 3797
		[TimeId("H_SDLI")]
		HandlerServiceDiscoveryLookupInternal,
		// Token: 0x04000ED6 RID: 3798
		[TimeId("C_VXML")]
		CommandValidateXML,
		// Token: 0x04000ED7 RID: 3799
		[TimeId("SM_VXML")]
		SendMailValidateXML,
		// Token: 0x04000ED8 RID: 3800
		[TimeId("V_VXML")]
		ValidatorValidateXML,
		// Token: 0x04000ED9 RID: 3801
		[TimeId("V_VXMLN")]
		ValidatorValidateXMLNode,
		// Token: 0x04000EDA RID: 3802
		[TimeId("V_VCSS")]
		ValidatorCreateSchemaSet,
		// Token: 0x04000EDB RID: 3803
		[TimeId("V_VCSSP")]
		ValidatorCreateSchemaSetParam,
		// Token: 0x04000EDC RID: 3804
		[TimeId("S_EC")]
		SettingsExecuteCommand,
		// Token: 0x04000EDD RID: 3805
		[TimeId("S_GVEX")]
		SettingsGetValidationErrorXml,
		// Token: 0x04000EDE RID: 3806
		[TimeId("S_RXR")]
		SettingsReadXmlRequest,
		// Token: 0x04000EDF RID: 3807
		[TimeId("S_IRX")]
		SettingsInitializeResponseXmlDocument,
		// Token: 0x04000EE0 RID: 3808
		[TimeId("S_FRX")]
		SettingsFinalizeResponseXmlDocument,
		// Token: 0x04000EE1 RID: 3809
		[TimeId("UIS_E")]
		UserInformationSettingsExecute,
		// Token: 0x04000EE2 RID: 3810
		[TimeId("UIS_PG")]
		UserInformationSettingsProcessGet,
		// Token: 0x04000EE3 RID: 3811
		[TimeId("RMS_E")]
		RMSExecute,
		// Token: 0x04000EE4 RID: 3812
		[TimeId("RMS_PG")]
		RMSProcessGet,
		// Token: 0x04000EE5 RID: 3813
		[TimeId("RMS_PE")]
		RMSProcessException,
		// Token: 0x04000EE6 RID: 3814
		[TimeId("OOF_E")]
		OOFSettingsExecute,
		// Token: 0x04000EE7 RID: 3815
		[TimeId("OOF_PG")]
		OOFSettingsProcessGet,
		// Token: 0x04000EE8 RID: 3816
		[TimeId("OOF_PS")]
		OOFSettingsProcessSet,
		// Token: 0x04000EE9 RID: 3817
		[TimeId("OOF_PE")]
		OOFSettingsProcessException,
		// Token: 0x04000EEA RID: 3818
		[TimeId("OOF_TTH")]
		OOFSettingsTextToHtml,
		// Token: 0x04000EEB RID: 3819
		[TimeId("OOF_HTT")]
		OOFSettingsHtmlToText,
		// Token: 0x04000EEC RID: 3820
		[TimeId("DI_E")]
		DeviceInfoExecute,
		// Token: 0x04000EED RID: 3821
		[TimeId("DI_PS")]
		DeviceInfoProcessSet,
		// Token: 0x04000EEE RID: 3822
		[TimeId("DI_PE")]
		DeviceInfoProcessException,
		// Token: 0x04000EEF RID: 3823
		[TimeId("DP_E")]
		DevicePasswordExecute,
		// Token: 0x04000EF0 RID: 3824
		[TimeId("DP_PS")]
		DevicePasswordProcessSet,
		// Token: 0x04000EF1 RID: 3825
		[TimeId("TZ_E")]
		TimeZoneOffsetSettingsExecute,
		// Token: 0x04000EF2 RID: 3826
		[TimeId("TZ_GET")]
		TimeZoneOffsetSettingsGet
	}
}
