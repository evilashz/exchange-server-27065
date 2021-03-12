using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000755 RID: 1877
	[Serializable]
	public sealed class ServerComponentStatePresentationObject : ConfigurableObject
	{
		// Token: 0x06005B29 RID: 23337 RVA: 0x0013F56C File Offset: 0x0013D76C
		public ServerComponentStatePresentationObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x0013F57C File Offset: 0x0013D77C
		public ServerComponentStatePresentationObject(ADObjectId serverId, string serverFqdn, string componentName, MultiValuedProperty<string> componentStates) : base(new SimpleProviderPropertyBag())
		{
			this[SimpleProviderObjectSchema.Identity] = serverId;
			this.ServerFqdn = serverFqdn;
			this.Component = componentName;
			bool serverWideOfflineIsOffline;
			List<ServerComponentStates.ItemEntry> list;
			List<ServerComponentStates.ItemEntry> list2;
			this.State = ServerComponentStates.ReadEffectiveComponentState(this.ServerFqdn, componentStates, ServerComponentStateSources.All, this.Component, ServerComponentStateManager.GetDefaultState(this.Component), out serverWideOfflineIsOffline, out list, out list2);
			this.ServerWideOfflineIsOffline = serverWideOfflineIsOffline;
			if (list != null)
			{
				this.localStates = new List<ServerComponentStatePresentationObject.RequesterDetails>(list.Count);
				foreach (ServerComponentStates.ItemEntry info in list)
				{
					this.localStates.Add(new ServerComponentStatePresentationObject.RequesterDetails(info));
				}
			}
			if (list2 != null)
			{
				this.remoteStates = new List<ServerComponentStatePresentationObject.RequesterDetails>(list2.Count);
				foreach (ServerComponentStates.ItemEntry info2 in list2)
				{
					this.remoteStates.Add(new ServerComponentStatePresentationObject.RequesterDetails(info2));
				}
			}
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0013F6A0 File Offset: 0x0013D8A0
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ServerComponentStatePresentationObject.schema;
			}
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x06005B2C RID: 23340 RVA: 0x0013F6A7 File Offset: 0x0013D8A7
		// (set) Token: 0x06005B2D RID: 23341 RVA: 0x0013F6AF File Offset: 0x0013D8AF
		public string ServerFqdn { get; set; }

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x0013F6B8 File Offset: 0x0013D8B8
		// (set) Token: 0x06005B2F RID: 23343 RVA: 0x0013F6C0 File Offset: 0x0013D8C0
		public string Component { get; set; }

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x0013F6C9 File Offset: 0x0013D8C9
		// (set) Token: 0x06005B31 RID: 23345 RVA: 0x0013F6D1 File Offset: 0x0013D8D1
		public ServiceState State { get; private set; }

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x0013F6DA File Offset: 0x0013D8DA
		// (set) Token: 0x06005B33 RID: 23347 RVA: 0x0013F6E2 File Offset: 0x0013D8E2
		public bool ServerWideOfflineIsOffline { get; private set; }

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x0013F6EB File Offset: 0x0013D8EB
		public List<ServerComponentStatePresentationObject.RequesterDetails> RemoteStates
		{
			get
			{
				return this.remoteStates;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x0013F6F3 File Offset: 0x0013D8F3
		public List<ServerComponentStatePresentationObject.RequesterDetails> LocalStates
		{
			get
			{
				return this.localStates;
			}
		}

		// Token: 0x04003E12 RID: 15890
		private static ServerComponentStateSchema schema = ObjectSchema.GetInstance<ServerComponentStateSchema>();

		// Token: 0x04003E13 RID: 15891
		private List<ServerComponentStatePresentationObject.RequesterDetails> remoteStates;

		// Token: 0x04003E14 RID: 15892
		private List<ServerComponentStatePresentationObject.RequesterDetails> localStates;

		// Token: 0x02000756 RID: 1878
		[Serializable]
		public class RequesterDetails
		{
			// Token: 0x06005B37 RID: 23351 RVA: 0x0013F707 File Offset: 0x0013D907
			internal RequesterDetails(ServerComponentStates.ItemEntry info)
			{
				this.Requester = info.Requester;
				this.Component = info.Component;
				this.State = info.State;
				this.Timestamp = info.Timestamp;
			}

			// Token: 0x17001FAC RID: 8108
			// (get) Token: 0x06005B38 RID: 23352 RVA: 0x0013F73F File Offset: 0x0013D93F
			// (set) Token: 0x06005B39 RID: 23353 RVA: 0x0013F747 File Offset: 0x0013D947
			public string Requester { get; private set; }

			// Token: 0x17001FAD RID: 8109
			// (get) Token: 0x06005B3A RID: 23354 RVA: 0x0013F750 File Offset: 0x0013D950
			// (set) Token: 0x06005B3B RID: 23355 RVA: 0x0013F758 File Offset: 0x0013D958
			public ServiceState State { get; private set; }

			// Token: 0x17001FAE RID: 8110
			// (get) Token: 0x06005B3C RID: 23356 RVA: 0x0013F761 File Offset: 0x0013D961
			// (set) Token: 0x06005B3D RID: 23357 RVA: 0x0013F769 File Offset: 0x0013D969
			public DateTime Timestamp { get; private set; }

			// Token: 0x17001FAF RID: 8111
			// (get) Token: 0x06005B3E RID: 23358 RVA: 0x0013F772 File Offset: 0x0013D972
			// (set) Token: 0x06005B3F RID: 23359 RVA: 0x0013F77A File Offset: 0x0013D97A
			public string Component { get; private set; }
		}
	}
}
