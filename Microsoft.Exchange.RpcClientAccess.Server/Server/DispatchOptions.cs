using System;
using System.Linq;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200000B RID: 11
	internal sealed class DispatchOptions
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000039DA File Offset: 0x00001BDA
		internal DispatchOptions()
		{
			this.isMonitoringContext = false;
			this.isMapiHttp = false;
			this.isExchangeSubsystem = false;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039F7 File Offset: 0x00001BF7
		internal DispatchOptions(string protocolSequence, AuxiliaryData auxiliaryData)
		{
			this.isMonitoringContext = DispatchOptions.IsInMonitoringContext(auxiliaryData);
			this.isMapiHttp = "MapiHttp".Equals(protocolSequence);
			this.isExchangeSubsystem = DispatchOptions.IsExchangeSubsystem(auxiliaryData);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003A28 File Offset: 0x00001C28
		internal bool AppendMonitoringAuxiliaryBlock
		{
			get
			{
				return this.isMonitoringContext;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003A30 File Offset: 0x00001C30
		internal bool DoNotRethrowExceptionsOnFailure
		{
			get
			{
				return !this.isMapiHttp && this.isMonitoringContext;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003A42 File Offset: 0x00001C42
		internal bool EnforceMicrodelays
		{
			get
			{
				return !this.isMonitoringContext;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003A4D File Offset: 0x00001C4D
		internal bool UseRandomAdditionalRetryDelay
		{
			get
			{
				return !this.isMonitoringContext && !this.isExchangeSubsystem;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003A64 File Offset: 0x00001C64
		private static bool IsInMonitoringContext(AuxiliaryData auxiliaryData)
		{
			SetMonitoringContextAuxiliaryBlock setMonitoringContextAuxiliaryBlock = auxiliaryData.Input.OfType<SetMonitoringContextAuxiliaryBlock>().FirstOrDefault<SetMonitoringContextAuxiliaryBlock>();
			return setMonitoringContextAuxiliaryBlock != null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A8C File Offset: 0x00001C8C
		private static bool IsExchangeSubsystem(AuxiliaryData auxiliaryData)
		{
			PerfClientInfoAuxiliaryBlock perfClientInfoAuxiliaryBlock = auxiliaryData.Input.OfType<PerfClientInfoAuxiliaryBlock>().FirstOrDefault<PerfClientInfoAuxiliaryBlock>();
			return perfClientInfoAuxiliaryBlock != null && perfClientInfoAuxiliaryBlock.ClientMode == ClientMode.ExchangeServer;
		}

		// Token: 0x0400003D RID: 61
		private const string MapiHttpProtocolSequence = "MapiHttp";

		// Token: 0x0400003E RID: 62
		private readonly bool isMonitoringContext;

		// Token: 0x0400003F RID: 63
		private readonly bool isMapiHttp;

		// Token: 0x04000040 RID: 64
		private readonly bool isExchangeSubsystem;
	}
}
