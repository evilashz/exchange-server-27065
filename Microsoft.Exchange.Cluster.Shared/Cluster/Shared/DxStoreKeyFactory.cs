﻿using System;
using System.Collections.Concurrent;
using System.ServiceModel.Description;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200009E RID: 158
	public class DxStoreKeyFactory
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00016310 File Offset: 0x00014510
		public DxStoreKeyFactory(string componentName, Func<Exception, Exception> exceptionTranslator, DxStoreRegistryConfigProvider configProvider = null, string groupName = null, string self = null, bool isZeroboxMode = false)
		{
			this.ExceptionTranslator = exceptionTranslator;
			if (configProvider == null)
			{
				configProvider = new DistributedStore.DxStoreRegistryProviderWithVariantConfig();
				configProvider.Initialize(componentName, self, null, null, isZeroboxMode);
			}
			this.ConfigProvider = configProvider;
			this.GroupConfig = configProvider.GetGroupConfig(groupName, true);
			if (this.GroupConfig.Settings.IsUseHttpTransportForClientCommunication)
			{
				HttpConfiguration.Configure(this.GroupConfig);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001637F File Offset: 0x0001457F
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00016387 File Offset: 0x00014587
		public DxStoreRegistryConfigProvider ConfigProvider { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00016390 File Offset: 0x00014590
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00016398 File Offset: 0x00014598
		public InstanceGroupConfig GroupConfig { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000163A1 File Offset: 0x000145A1
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000163A9 File Offset: 0x000145A9
		public Func<Exception, Exception> ExceptionTranslator { get; set; }

		// Token: 0x060005E6 RID: 1510 RVA: 0x000163C8 File Offset: 0x000145C8
		public void RunOperationAndTranslateException(OperationCategory operationCategory, string keyName, Action action)
		{
			this.RunOperationAndTranslateException<int>(operationCategory, keyName, delegate()
			{
				action();
				return 0;
			}, false);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000163F8 File Offset: 0x000145F8
		public T RunOperationAndTranslateException<T>(OperationCategory operationCategory, string keyName, Func<T> action, bool isBestEffort = false)
		{
			try
			{
				return action();
			}
			catch (Exception innerException)
			{
				if (!isBestEffort)
				{
					DxStoreKeyApiOperationException ex = new DxStoreKeyApiOperationException(operationCategory.ToString(), keyName ?? string.Empty, innerException);
					if (this.ExceptionTranslator != null)
					{
						throw this.ExceptionTranslator(ex);
					}
					throw ex;
				}
			}
			return default(T);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00016464 File Offset: 0x00014664
		public CachedChannelFactory<IDxStoreAccess> GetFactory(string nodeName = null, WcfTimeout wcfTimeout = null)
		{
			if (this.GroupConfig.Settings.IsUseHttpTransportForClientCommunication)
			{
				return null;
			}
			CachedChannelFactory<IDxStoreAccess> cachedChannelFactory = null;
			bool flag = false;
			if (this.IsSelf(nodeName))
			{
				cachedChannelFactory = this.localChannelFactory;
				flag = true;
			}
			if (cachedChannelFactory == null)
			{
				ServiceEndpoint storeAccessEndpoint = EndpointBuilder.GetStoreAccessEndpoint(this.GroupConfig, nodeName, this.IsDefaultGroupIdentifier(this.GroupConfig.Name), false, wcfTimeout);
				cachedChannelFactory = this.GetFactoryByEndPoint(storeAccessEndpoint);
			}
			if (flag && this.localChannelFactory == null)
			{
				this.localChannelFactory = cachedChannelFactory;
			}
			return cachedChannelFactory;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00016650 File Offset: 0x00014850
		public IDistributedStoreKey GetBaseKey(DxStoreKeyAccessMode mode, CachedChannelFactory<IDxStoreAccess> channelFactory = null, string nodeName = null, bool isPrivate = false)
		{
			return this.RunOperationAndTranslateException<DxStoreKey>(OperationCategory.GetBaseKey, string.Empty, delegate()
			{
				channelFactory = (channelFactory ?? this.GetFactory(nodeName, null));
				IDxStoreAccessClient dxStoreAccessClient;
				if (this.GroupConfig.Settings.IsUseHttpTransportForClientCommunication)
				{
					dxStoreAccessClient = new HttpStoreAccessClient(this.GroupConfig.Self, HttpClient.TargetInfo.BuildFromNode(nodeName, this.GroupConfig), this.GroupConfig.Settings.StoreAccessHttpTimeoutInMSec);
				}
				else
				{
					dxStoreAccessClient = new WcfStoreAccessClient(channelFactory, null);
				}
				DxStoreAccessRequest.CheckKey checkKey = new DxStoreAccessRequest.CheckKey();
				checkKey.Initialize(string.Empty, isPrivate, this.ConfigProvider.Self);
				DxStoreAccessReply.CheckKey checkKey2 = dxStoreAccessClient.CheckKey(checkKey, null);
				if (checkKey2.ReadResult.IsStale)
				{
					throw new DxStoreInstanceStaleStoreException();
				}
				if (!checkKey2.IsExist)
				{
					throw new DxStoreKeyNotFoundException(string.Empty);
				}
				DxStoreKey.BaseKeyParameters baseParameters = new DxStoreKey.BaseKeyParameters
				{
					Client = dxStoreAccessClient,
					KeyFactory = this,
					Self = this.ConfigProvider.Self,
					IsPrivate = isPrivate,
					DefaultReadOptions = new ReadOptions(),
					DefaultWriteOptions = new WriteOptions()
				};
				return new DxStoreKey(string.Empty, mode, baseParameters);
			}, false);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000166A8 File Offset: 0x000148A8
		private CachedChannelFactory<IDxStoreAccess> GetFactoryByEndPoint(ServiceEndpoint serviceEndPoint)
		{
			return this.factoryByEndPoint.GetOrAdd(serviceEndPoint, (ServiceEndpoint e) => new CachedChannelFactory<IDxStoreAccess>(e));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000166E0 File Offset: 0x000148E0
		private bool IsSelf(string nodeName)
		{
			return string.IsNullOrEmpty(nodeName) || Utils.IsEqual(nodeName, this.ConfigProvider.Self, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000166FE File Offset: 0x000148FE
		private bool IsDefaultGroupIdentifier(string groupName)
		{
			return string.IsNullOrEmpty(groupName) || Utils.IsEqual(groupName, "B1563499-EA40-4101-A9E6-59A8EB26FF1E", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000325 RID: 805
		private ConcurrentDictionary<ServiceEndpoint, CachedChannelFactory<IDxStoreAccess>> factoryByEndPoint = new ConcurrentDictionary<ServiceEndpoint, CachedChannelFactory<IDxStoreAccess>>();

		// Token: 0x04000326 RID: 806
		private CachedChannelFactory<IDxStoreAccess> localChannelFactory;
	}
}
