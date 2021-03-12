using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020001A7 RID: 423
	internal class ServerPickerClient
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00042311 File Offset: 0x00040511
		public virtual ADPropertyDefinition OverrideListPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00042314 File Offset: 0x00040514
		public virtual PickerServer CreatePickerServer(Server server, PickerServerList pickerServerList)
		{
			return new PickerServer(server, pickerServerList);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004231D File Offset: 0x0004051D
		public virtual void LogNoActiveServerEvent(string serverRole)
		{
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004231F File Offset: 0x0004051F
		public virtual void UpdateServersTotalPerfmon(int count)
		{
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00042321 File Offset: 0x00040521
		public virtual void UpdateServersInRetryPerfmon(int count)
		{
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00042323 File Offset: 0x00040523
		public virtual void UpdateServersPercentageActivePerfmon(int percentage)
		{
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00042325 File Offset: 0x00040525
		public virtual void LocalServerDiscovered(Server server)
		{
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00042327 File Offset: 0x00040527
		public virtual void ServerListUpdated(List<PickerServer> serverList)
		{
		}

		// Token: 0x0400089F RID: 2207
		internal static readonly ServerPickerClient Default = new ServerPickerClient();
	}
}
