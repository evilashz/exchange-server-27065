using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200002E RID: 46
	internal class AttachmentDataProviderManager
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003A25 File Offset: 0x00001C25
		public bool UseMockAttachmentDataProvider
		{
			get
			{
				return this.useMockAttachmentDataProvider;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003A30 File Offset: 0x00001C30
		public AttachmentDataProviderManager()
		{
			this.isOneDriveProProviderAvailable = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.OneDriveProProviderAvailable.Enabled;
			this.lockObject = new object();
			this.useMockAttachmentDataProvider = AttachmentDataProviderManager.IsMockDataProviderEnabled();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003A7D File Offset: 0x00001C7D
		public static bool IsMockDataProviderEnabled()
		{
			return new BoolAppSettingsEntry("UseMockAttachmentDataProvider", false, ExTraceGlobals.AttachmentHandlingTracer).Value;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003AAC File Offset: 0x00001CAC
		public AttachmentDataProvider GetDefaultUploadDataProvider(CallContext callContext)
		{
			if (this.defaultUploadDataProvider == null)
			{
				this.EnsureAttachmentDataProviders(callContext);
				this.defaultUploadDataProvider = this.dataProviders.Values.FirstOrDefault((AttachmentDataProvider x) => x.GetType() == typeof(OneDriveProAttachmentDataProvider));
			}
			return this.defaultUploadDataProvider;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003B01 File Offset: 0x00001D01
		public AttachmentDataProvider[] GetProviders(CallContext callContext)
		{
			return this.GetProviders(callContext, null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003B0C File Offset: 0x00001D0C
		public AttachmentDataProvider[] GetProviders(CallContext callContext, GetAttachmentDataProvidersRequest request)
		{
			this.EnsureAttachmentDataProviders(callContext);
			Dictionary<string, AttachmentDataProvider>.ValueCollection values = this.dataProviders.Values;
			foreach (AttachmentDataProvider attachmentDataProvider in values)
			{
				attachmentDataProvider.PostInitialize(request);
			}
			return values.ToArray<AttachmentDataProvider>();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003B74 File Offset: 0x00001D74
		public AttachmentDataProvider GetProvider(CallContext callContext, string id)
		{
			this.EnsureAttachmentDataProviders(callContext);
			if (this.dataProviders.ContainsKey(id))
			{
				return this.dataProviders[id];
			}
			ExTraceGlobals.DocumentsTracer.TraceDebug<string>((long)this.GetHashCode(), "Provider with id {0} was not found, getting the default upload provider", id);
			UserContext userContext = UserContextManager.GetUserContext(callContext.HttpContext, callContext.EffectiveCaller, true);
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("ADPM.GP", userContext, "GetProvider", string.Format("Provider with id {0} was not found", id)));
			return this.GetDefaultUploadDataProvider(callContext);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public AttachmentDataProvider AddProvider(CallContext callContext, AttachmentDataProvider attachmentDataProvider)
		{
			this.EnsureAttachmentDataProviders(callContext);
			attachmentDataProvider.Id = Guid.NewGuid().ToString();
			this.AddProviderInternal(callContext, new PolymorphicConfiguration<AttachmentDataProvider>(), attachmentDataProvider);
			return attachmentDataProvider;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003C2F File Offset: 0x00001E2F
		public void RemoveProvider(string id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003C38 File Offset: 0x00001E38
		public GetAttachmentDataProviderItemsResponse GetRecentItems(CallContext callContext)
		{
			GetAttachmentDataProviderItemsResponse getAttachmentDataProviderItemsResponse = new GetAttachmentDataProviderItemsResponse();
			try
			{
				this.EnsureAttachmentDataProviders(callContext);
				List<AttachmentDataProviderItem> list = new List<AttachmentDataProviderItem>();
				foreach (AttachmentDataProvider attachmentDataProvider in this.dataProviders.Values)
				{
					AttachmentDataProviderItem[] recentItems = attachmentDataProvider.GetRecentItems();
					if (recentItems != null)
					{
						list.AddRange(recentItems);
					}
				}
				getAttachmentDataProviderItemsResponse.Items = list.ToArray();
				getAttachmentDataProviderItemsResponse.TotalItemCount = getAttachmentDataProviderItemsResponse.Items.Count<AttachmentDataProviderItem>();
				getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.Success;
			}
			catch (Exception)
			{
				getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.GenericFailure;
			}
			return getAttachmentDataProviderItemsResponse;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003CEC File Offset: 0x00001EEC
		public GetAttachmentDataProviderItemsResponse GetGroups(CallContext callContext, MailboxSession mailboxSession)
		{
			AttachmentDataProvider attachmentDataProvider = this.GetDefaultUploadDataProvider(callContext);
			GetAttachmentDataProviderItemsResponse getAttachmentDataProviderItemsResponse;
			if (attachmentDataProvider is OneDriveProAttachmentDataProvider)
			{
				OneDriveProAttachmentDataProvider oneDriveProAttachmentDataProvider = (OneDriveProAttachmentDataProvider)attachmentDataProvider;
				getAttachmentDataProviderItemsResponse = oneDriveProAttachmentDataProvider.GetGroups(mailboxSession);
			}
			else
			{
				getAttachmentDataProviderItemsResponse = new GetAttachmentDataProviderItemsResponse();
				getAttachmentDataProviderItemsResponse.Items = new AttachmentDataProviderItem[0];
				getAttachmentDataProviderItemsResponse.TotalItemCount = 0;
				getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.GenericFailure;
			}
			return getAttachmentDataProviderItemsResponse;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003D50 File Offset: 0x00001F50
		private void EnsureAttachmentDataProviders(CallContext callContext)
		{
			if (this.dataProviders == null || this.dataProviders.Count == 0)
			{
				lock (this.lockObject)
				{
					if (this.dataProviders == null || this.dataProviders.Count == 0)
					{
						UserContext userContext = UserContextManager.GetUserContext(callContext.HttpContext, callContext.EffectiveCaller, true);
						PolymorphicConfiguration<AttachmentDataProvider> polymorphicConfiguration = null;
						if (!userContext.IsGroupUserContext)
						{
							polymorphicConfiguration = new PolymorphicConfiguration<AttachmentDataProvider>();
							try
							{
								polymorphicConfiguration.Load(callContext);
							}
							catch (TypeLoadException)
							{
							}
							this.dataProviders = polymorphicConfiguration.Entries.ToDictionary((AttachmentDataProvider x) => x.Id);
						}
						else
						{
							this.dataProviders = new Dictionary<string, AttachmentDataProvider>();
						}
						this.EnsureOneDriveProDataProvider(callContext, polymorphicConfiguration, userContext);
						IEnumerable<AttachmentDataProvider> enumerable = from x in this.dataProviders.Values
						where x is IAttachmentDataProviderChanged
						select x;
						foreach (AttachmentDataProvider attachmentDataProvider in enumerable)
						{
							((IAttachmentDataProviderChanged)attachmentDataProvider).AttachmentDataProviderChanged += this.ProviderChanged;
						}
					}
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003F0C File Offset: 0x0000210C
		private void EnsureOneDriveProDataProvider(CallContext callContext, PolymorphicConfiguration<AttachmentDataProvider> attachmentDataProvidersConfig, UserContext userContext)
		{
			if (!this.useMockAttachmentDataProvider)
			{
				if (!this.isOneDriveProProviderAvailable)
				{
					return;
				}
				if (!userContext.IsBposUser)
				{
					return;
				}
			}
			if (!this.dataProviders.Values.Any((AttachmentDataProvider x) => x.GetType() == typeof(OneDriveProAttachmentDataProvider)))
			{
				lock (this.lockObject)
				{
					if (!this.dataProviders.Values.Any((AttachmentDataProvider x) => x.GetType() == typeof(OneDriveProAttachmentDataProvider)))
					{
						OneDriveProAttachmentDataProvider oneDriveProAttachmentDataProvider = OneDriveProAttachmentDataProvider.CreateFromBpos(userContext, callContext, this.useMockAttachmentDataProvider);
						if (oneDriveProAttachmentDataProvider != null)
						{
							this.AddProviderInternal(callContext, attachmentDataProvidersConfig, oneDriveProAttachmentDataProvider);
						}
					}
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003FD8 File Offset: 0x000021D8
		private void AddProviderInternal(CallContext callContext, PolymorphicConfiguration<AttachmentDataProvider> attachmentDataProvidersConfig, AttachmentDataProvider provider)
		{
			lock (this.lockObject)
			{
				this.dataProviders[provider.Id] = provider;
				if (attachmentDataProvidersConfig != null)
				{
					attachmentDataProvidersConfig.Entries.Add(provider);
					attachmentDataProvidersConfig.Save(callContext);
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000403C File Offset: 0x0000223C
		private void ProviderChanged(AttachmentDataProvider provider, AttachmentDataProviderChangedEventArgs args)
		{
			if (this.dataProviders.ContainsKey(provider.Id))
			{
				lock (this.lockObject)
				{
					if (this.dataProviders.ContainsKey(provider.Id))
					{
						PolymorphicConfiguration<AttachmentDataProvider> polymorphicConfiguration = new PolymorphicConfiguration<AttachmentDataProvider>();
						foreach (AttachmentDataProvider item in this.dataProviders.Values)
						{
							polymorphicConfiguration.Entries.Add(item);
							polymorphicConfiguration.Save(args.MailboxSession);
						}
					}
				}
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly bool isOneDriveProProviderAvailable;

		// Token: 0x04000059 RID: 89
		private Dictionary<string, AttachmentDataProvider> dataProviders;

		// Token: 0x0400005A RID: 90
		private object lockObject;

		// Token: 0x0400005B RID: 91
		private AttachmentDataProvider defaultUploadDataProvider;

		// Token: 0x0400005C RID: 92
		private readonly bool useMockAttachmentDataProvider;
	}
}
