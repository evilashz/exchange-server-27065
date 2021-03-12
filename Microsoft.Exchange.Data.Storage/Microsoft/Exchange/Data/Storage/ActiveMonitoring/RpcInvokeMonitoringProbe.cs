using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000332 RID: 818
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcInvokeMonitoringProbe
	{
		// Token: 0x0600242C RID: 9260 RVA: 0x00093A50 File Offset: 0x00091C50
		public static RpcInvokeMonitoringProbe.Reply Invoke(string serverName, string identity, string propertyBagXml, string extensionAttributes, int timeoutInMSec = 300000)
		{
			RpcInvokeNowCommon.Request attachedRequest = new RpcInvokeNowCommon.Request(identity, propertyBagXml, extensionAttributes);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.InvokeMonitoringProbe, 1, 0);
			return ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcInvokeMonitoringProbe.Reply>(requestInfo, serverName, timeoutInMSec);
		}

		// Token: 0x040015A9 RID: 5545
		public const int MajorVersion = 1;

		// Token: 0x040015AA RID: 5546
		public const int MinorVersion = 0;

		// Token: 0x040015AB RID: 5547
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.InvokeMonitoringProbe;

		// Token: 0x02000333 RID: 819
		[Serializable]
		public class Reply
		{
			// Token: 0x17000C12 RID: 3090
			// (get) Token: 0x0600242D RID: 9261 RVA: 0x00093A7A File Offset: 0x00091C7A
			// (set) Token: 0x0600242E RID: 9262 RVA: 0x00093A82 File Offset: 0x00091C82
			public RpcInvokeMonitoringProbe.RpcMonitorProbeResult ProbeResult { get; set; }

			// Token: 0x17000C13 RID: 3091
			// (get) Token: 0x0600242F RID: 9263 RVA: 0x00093A8B File Offset: 0x00091C8B
			// (set) Token: 0x06002430 RID: 9264 RVA: 0x00093A93 File Offset: 0x00091C93
			public string ErrorMessage { get; set; }
		}

		// Token: 0x02000334 RID: 820
		[Serializable]
		public class RpcMonitorProbeResult
		{
			// Token: 0x17000C14 RID: 3092
			// (get) Token: 0x06002433 RID: 9267 RVA: 0x00093AAC File Offset: 0x00091CAC
			// (set) Token: 0x06002434 RID: 9268 RVA: 0x00093AB4 File Offset: 0x00091CB4
			public string MonitorIdentity { get; set; }

			// Token: 0x17000C15 RID: 3093
			// (get) Token: 0x06002435 RID: 9269 RVA: 0x00093ABD File Offset: 0x00091CBD
			// (set) Token: 0x06002436 RID: 9270 RVA: 0x00093AC5 File Offset: 0x00091CC5
			public Guid RequestId { get; set; }

			// Token: 0x17000C16 RID: 3094
			// (get) Token: 0x06002437 RID: 9271 RVA: 0x00093ACE File Offset: 0x00091CCE
			// (set) Token: 0x06002438 RID: 9272 RVA: 0x00093AD6 File Offset: 0x00091CD6
			public DateTime ExecutionStartTime { get; set; }

			// Token: 0x17000C17 RID: 3095
			// (get) Token: 0x06002439 RID: 9273 RVA: 0x00093ADF File Offset: 0x00091CDF
			// (set) Token: 0x0600243A RID: 9274 RVA: 0x00093AE7 File Offset: 0x00091CE7
			public DateTime ExecutionEndTime { get; set; }

			// Token: 0x17000C18 RID: 3096
			// (get) Token: 0x0600243B RID: 9275 RVA: 0x00093AF0 File Offset: 0x00091CF0
			// (set) Token: 0x0600243C RID: 9276 RVA: 0x00093AF8 File Offset: 0x00091CF8
			public string Error { get; set; }

			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x0600243D RID: 9277 RVA: 0x00093B01 File Offset: 0x00091D01
			// (set) Token: 0x0600243E RID: 9278 RVA: 0x00093B09 File Offset: 0x00091D09
			public string Exception { get; set; }

			// Token: 0x17000C1A RID: 3098
			// (get) Token: 0x0600243F RID: 9279 RVA: 0x00093B12 File Offset: 0x00091D12
			// (set) Token: 0x06002440 RID: 9280 RVA: 0x00093B1A File Offset: 0x00091D1A
			public byte PoisonedCount { get; set; }

			// Token: 0x17000C1B RID: 3099
			// (get) Token: 0x06002441 RID: 9281 RVA: 0x00093B23 File Offset: 0x00091D23
			// (set) Token: 0x06002442 RID: 9282 RVA: 0x00093B2B File Offset: 0x00091D2B
			public int ExecutionId { get; set; }

			// Token: 0x17000C1C RID: 3100
			// (get) Token: 0x06002443 RID: 9283 RVA: 0x00093B34 File Offset: 0x00091D34
			// (set) Token: 0x06002444 RID: 9284 RVA: 0x00093B3C File Offset: 0x00091D3C
			public double SampleValue { get; set; }

			// Token: 0x17000C1D RID: 3101
			// (get) Token: 0x06002445 RID: 9285 RVA: 0x00093B45 File Offset: 0x00091D45
			// (set) Token: 0x06002446 RID: 9286 RVA: 0x00093B4D File Offset: 0x00091D4D
			public string ExecutionContext { get; set; }

			// Token: 0x17000C1E RID: 3102
			// (get) Token: 0x06002447 RID: 9287 RVA: 0x00093B56 File Offset: 0x00091D56
			// (set) Token: 0x06002448 RID: 9288 RVA: 0x00093B5E File Offset: 0x00091D5E
			public string FailureContext { get; set; }

			// Token: 0x17000C1F RID: 3103
			// (get) Token: 0x06002449 RID: 9289 RVA: 0x00093B67 File Offset: 0x00091D67
			// (set) Token: 0x0600244A RID: 9290 RVA: 0x00093B6F File Offset: 0x00091D6F
			public string ExtensionXml { get; set; }

			// Token: 0x17000C20 RID: 3104
			// (get) Token: 0x0600244B RID: 9291 RVA: 0x00093B78 File Offset: 0x00091D78
			// (set) Token: 0x0600244C RID: 9292 RVA: 0x00093B80 File Offset: 0x00091D80
			public ResultType ResultType { get; set; }

			// Token: 0x17000C21 RID: 3105
			// (get) Token: 0x0600244D RID: 9293 RVA: 0x00093B89 File Offset: 0x00091D89
			// (set) Token: 0x0600244E RID: 9294 RVA: 0x00093B91 File Offset: 0x00091D91
			public byte RetryCount { get; set; }

			// Token: 0x17000C22 RID: 3106
			// (get) Token: 0x0600244F RID: 9295 RVA: 0x00093B9A File Offset: 0x00091D9A
			// (set) Token: 0x06002450 RID: 9296 RVA: 0x00093BA2 File Offset: 0x00091DA2
			public string ResultName { get; set; }

			// Token: 0x17000C23 RID: 3107
			// (get) Token: 0x06002451 RID: 9297 RVA: 0x00093BAB File Offset: 0x00091DAB
			// (set) Token: 0x06002452 RID: 9298 RVA: 0x00093BB3 File Offset: 0x00091DB3
			public bool IsNotified { get; set; }

			// Token: 0x17000C24 RID: 3108
			// (get) Token: 0x06002453 RID: 9299 RVA: 0x00093BBC File Offset: 0x00091DBC
			// (set) Token: 0x06002454 RID: 9300 RVA: 0x00093BC4 File Offset: 0x00091DC4
			public int ResultId { get; set; }

			// Token: 0x17000C25 RID: 3109
			// (get) Token: 0x06002455 RID: 9301 RVA: 0x00093BCD File Offset: 0x00091DCD
			// (set) Token: 0x06002456 RID: 9302 RVA: 0x00093BD5 File Offset: 0x00091DD5
			public string ServiceName { get; set; }

			// Token: 0x17000C26 RID: 3110
			// (get) Token: 0x06002457 RID: 9303 RVA: 0x00093BDE File Offset: 0x00091DDE
			// (set) Token: 0x06002458 RID: 9304 RVA: 0x00093BE6 File Offset: 0x00091DE6
			public string StateAttribute1 { get; set; }

			// Token: 0x17000C27 RID: 3111
			// (get) Token: 0x06002459 RID: 9305 RVA: 0x00093BEF File Offset: 0x00091DEF
			// (set) Token: 0x0600245A RID: 9306 RVA: 0x00093BF7 File Offset: 0x00091DF7
			public string StateAttribute2 { get; set; }

			// Token: 0x17000C28 RID: 3112
			// (get) Token: 0x0600245B RID: 9307 RVA: 0x00093C00 File Offset: 0x00091E00
			// (set) Token: 0x0600245C RID: 9308 RVA: 0x00093C08 File Offset: 0x00091E08
			public string StateAttribute3 { get; set; }

			// Token: 0x17000C29 RID: 3113
			// (get) Token: 0x0600245D RID: 9309 RVA: 0x00093C11 File Offset: 0x00091E11
			// (set) Token: 0x0600245E RID: 9310 RVA: 0x00093C19 File Offset: 0x00091E19
			public string StateAttribute4 { get; set; }

			// Token: 0x17000C2A RID: 3114
			// (get) Token: 0x0600245F RID: 9311 RVA: 0x00093C22 File Offset: 0x00091E22
			// (set) Token: 0x06002460 RID: 9312 RVA: 0x00093C2A File Offset: 0x00091E2A
			public string StateAttribute5 { get; set; }

			// Token: 0x17000C2B RID: 3115
			// (get) Token: 0x06002461 RID: 9313 RVA: 0x00093C33 File Offset: 0x00091E33
			// (set) Token: 0x06002462 RID: 9314 RVA: 0x00093C3B File Offset: 0x00091E3B
			public double StateAttribute6 { get; set; }

			// Token: 0x17000C2C RID: 3116
			// (get) Token: 0x06002463 RID: 9315 RVA: 0x00093C44 File Offset: 0x00091E44
			// (set) Token: 0x06002464 RID: 9316 RVA: 0x00093C4C File Offset: 0x00091E4C
			public double StateAttribute7 { get; set; }

			// Token: 0x17000C2D RID: 3117
			// (get) Token: 0x06002465 RID: 9317 RVA: 0x00093C55 File Offset: 0x00091E55
			// (set) Token: 0x06002466 RID: 9318 RVA: 0x00093C5D File Offset: 0x00091E5D
			public double StateAttribute8 { get; set; }

			// Token: 0x17000C2E RID: 3118
			// (get) Token: 0x06002467 RID: 9319 RVA: 0x00093C66 File Offset: 0x00091E66
			// (set) Token: 0x06002468 RID: 9320 RVA: 0x00093C6E File Offset: 0x00091E6E
			public double StateAttribute9 { get; set; }

			// Token: 0x17000C2F RID: 3119
			// (get) Token: 0x06002469 RID: 9321 RVA: 0x00093C77 File Offset: 0x00091E77
			// (set) Token: 0x0600246A RID: 9322 RVA: 0x00093C7F File Offset: 0x00091E7F
			public double StateAttribute10 { get; set; }

			// Token: 0x17000C30 RID: 3120
			// (get) Token: 0x0600246B RID: 9323 RVA: 0x00093C88 File Offset: 0x00091E88
			// (set) Token: 0x0600246C RID: 9324 RVA: 0x00093C90 File Offset: 0x00091E90
			public string StateAttribute11 { get; set; }

			// Token: 0x17000C31 RID: 3121
			// (get) Token: 0x0600246D RID: 9325 RVA: 0x00093C99 File Offset: 0x00091E99
			// (set) Token: 0x0600246E RID: 9326 RVA: 0x00093CA1 File Offset: 0x00091EA1
			public string StateAttribute12 { get; set; }

			// Token: 0x17000C32 RID: 3122
			// (get) Token: 0x0600246F RID: 9327 RVA: 0x00093CAA File Offset: 0x00091EAA
			// (set) Token: 0x06002470 RID: 9328 RVA: 0x00093CB2 File Offset: 0x00091EB2
			public string StateAttribute13 { get; set; }

			// Token: 0x17000C33 RID: 3123
			// (get) Token: 0x06002471 RID: 9329 RVA: 0x00093CBB File Offset: 0x00091EBB
			// (set) Token: 0x06002472 RID: 9330 RVA: 0x00093CC3 File Offset: 0x00091EC3
			public string StateAttribute14 { get; set; }

			// Token: 0x17000C34 RID: 3124
			// (get) Token: 0x06002473 RID: 9331 RVA: 0x00093CCC File Offset: 0x00091ECC
			// (set) Token: 0x06002474 RID: 9332 RVA: 0x00093CD4 File Offset: 0x00091ED4
			public string StateAttribute15 { get; set; }

			// Token: 0x17000C35 RID: 3125
			// (get) Token: 0x06002475 RID: 9333 RVA: 0x00093CDD File Offset: 0x00091EDD
			// (set) Token: 0x06002476 RID: 9334 RVA: 0x00093CE5 File Offset: 0x00091EE5
			public double StateAttribute16 { get; set; }

			// Token: 0x17000C36 RID: 3126
			// (get) Token: 0x06002477 RID: 9335 RVA: 0x00093CEE File Offset: 0x00091EEE
			// (set) Token: 0x06002478 RID: 9336 RVA: 0x00093CF6 File Offset: 0x00091EF6
			public double StateAttribute17 { get; set; }

			// Token: 0x17000C37 RID: 3127
			// (get) Token: 0x06002479 RID: 9337 RVA: 0x00093CFF File Offset: 0x00091EFF
			// (set) Token: 0x0600247A RID: 9338 RVA: 0x00093D07 File Offset: 0x00091F07
			public double StateAttribute18 { get; set; }

			// Token: 0x17000C38 RID: 3128
			// (get) Token: 0x0600247B RID: 9339 RVA: 0x00093D10 File Offset: 0x00091F10
			// (set) Token: 0x0600247C RID: 9340 RVA: 0x00093D18 File Offset: 0x00091F18
			public double StateAttribute19 { get; set; }

			// Token: 0x17000C39 RID: 3129
			// (get) Token: 0x0600247D RID: 9341 RVA: 0x00093D21 File Offset: 0x00091F21
			// (set) Token: 0x0600247E RID: 9342 RVA: 0x00093D29 File Offset: 0x00091F29
			public double StateAttribute20 { get; set; }

			// Token: 0x17000C3A RID: 3130
			// (get) Token: 0x0600247F RID: 9343 RVA: 0x00093D32 File Offset: 0x00091F32
			// (set) Token: 0x06002480 RID: 9344 RVA: 0x00093D3A File Offset: 0x00091F3A
			public string StateAttribute21 { get; set; }

			// Token: 0x17000C3B RID: 3131
			// (get) Token: 0x06002481 RID: 9345 RVA: 0x00093D43 File Offset: 0x00091F43
			// (set) Token: 0x06002482 RID: 9346 RVA: 0x00093D4B File Offset: 0x00091F4B
			public string StateAttribute22 { get; set; }

			// Token: 0x17000C3C RID: 3132
			// (get) Token: 0x06002483 RID: 9347 RVA: 0x00093D54 File Offset: 0x00091F54
			// (set) Token: 0x06002484 RID: 9348 RVA: 0x00093D5C File Offset: 0x00091F5C
			public string StateAttribute23 { get; set; }

			// Token: 0x17000C3D RID: 3133
			// (get) Token: 0x06002485 RID: 9349 RVA: 0x00093D65 File Offset: 0x00091F65
			// (set) Token: 0x06002486 RID: 9350 RVA: 0x00093D6D File Offset: 0x00091F6D
			public string StateAttribute24 { get; set; }

			// Token: 0x17000C3E RID: 3134
			// (get) Token: 0x06002487 RID: 9351 RVA: 0x00093D76 File Offset: 0x00091F76
			// (set) Token: 0x06002488 RID: 9352 RVA: 0x00093D7E File Offset: 0x00091F7E
			public string StateAttribute25 { get; set; }
		}
	}
}
