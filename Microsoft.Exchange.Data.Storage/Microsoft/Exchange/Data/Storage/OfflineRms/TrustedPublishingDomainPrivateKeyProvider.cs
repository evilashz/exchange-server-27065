﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.RightsManagementServices.Core;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AC0 RID: 2752
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TrustedPublishingDomainPrivateKeyProvider : ITrustedPublishingDomainPrivateKeyProvider, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600643D RID: 25661 RVA: 0x001A84A0 File Offset: 0x001A66A0
		public TrustedPublishingDomainPrivateKeyProvider(RmsClientManagerContext clientContext, Dictionary<string, PrivateKeyInformation> privateKeyInfos)
		{
			this.privateKeyInfos = privateKeyInfos;
			this.clientContext = clientContext;
			foreach (PrivateKeyInformation privateKeyInformation in this.privateKeyInfos.Values)
			{
				if (privateKeyInformation.IsSLCKey)
				{
					this.slcKey = privateKeyInformation;
				}
			}
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x001A852C File Offset: 0x001A672C
		public byte[] Decrypt(string idType, string id, byte[] encryptedData, bool usePadding)
		{
			RsaCapiKey key = this.GetKey(PrivateKeyInformation.GetIdentity(id, idType));
			return key.Decrypt(encryptedData, usePadding);
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x001A8550 File Offset: 0x001A6750
		public byte[] GenerateSignature(string idType, string id, byte[] digest, HashAlgorithmType hashAlgorithm)
		{
			RsaCapiKey key = this.GetKey(PrivateKeyInformation.GetIdentity(id, idType));
			return key.SignDigestValue(digest, hashAlgorithm);
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x001A8574 File Offset: 0x001A6774
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TrustedPublishingDomainPrivateKeyProvider>(this);
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x001A857C File Offset: 0x001A677C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x001A8594 File Offset: 0x001A6794
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.capiKeys != null && this.capiKeys.Count > 0)
			{
				foreach (RsaCapiKey rsaCapiKey in this.capiKeys.Values)
				{
					rsaCapiKey.Dispose();
				}
				this.capiKeys.Clear();
			}
			this.disposed = true;
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x001A8638 File Offset: 0x001A6838
		private RsaCapiKey GetKey(string keyIdentification)
		{
			PrivateKeyInformation privateKeyInformation;
			if (string.IsNullOrEmpty(keyIdentification))
			{
				privateKeyInformation = this.slcKey;
			}
			else if (!this.privateKeyInfos.TryGetValue(keyIdentification, out privateKeyInformation))
			{
				ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.TrustedPublishingDomainPrivateKeyProvider, ServerManagerLog.EventType.Error, this.clientContext, string.Format("Failed to locate Key {0} in TrustedPublishingDomainPrivateKeyProvider", keyIdentification));
				StringBuilder stringBuilder = new StringBuilder();
				foreach (PrivateKeyInformation privateKeyInformation2 in this.privateKeyInfos.Values)
				{
					stringBuilder.Append(privateKeyInformation2.KeyId);
					stringBuilder.Append(" ");
				}
				ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.TrustedPublishingDomainPrivateKeyProvider, ServerManagerLog.EventType.Verbose, this.clientContext, string.Format("Dump of TPDs keyIds for current tenant is {0} ", stringBuilder));
				throw new PrivateKeyProviderException(false, "Failed to locate private key for " + keyIdentification, null);
			}
			if (this.capiKeys.ContainsKey(privateKeyInformation.Identity))
			{
				return this.capiKeys[privateKeyInformation.Identity];
			}
			CspParameters parameters = new CspParameters
			{
				Flags = CspProviderFlags.UseMachineKeyStore,
				ProviderType = privateKeyInformation.CSPType,
				KeyNumber = privateKeyInformation.KeyNumber,
				KeyContainerName = null,
				ProviderName = (string.IsNullOrEmpty(privateKeyInformation.CSPName) ? null : privateKeyInformation.CSPName)
			};
			RsaCapiKey rsaCapiKey = new RsaCapiKey(parameters)
			{
				PersistKeyInCryptoServiceProvider = false
			};
			byte[] array = null;
			RsaCapiKey result;
			try
			{
				Guid guid = (this.clientContext == null) ? Guid.Empty : this.clientContext.ExternalDirectoryOrgId;
				ExchangeGroupKey exchangeGroupKey;
				Exception ex;
				if (!ServerManager.TryGetDkmKey(guid, out exchangeGroupKey, out ex))
				{
					ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.TrustedPublishingDomainPrivateKeyProvider, ServerManagerLog.EventType.Error, this.clientContext, string.Format("Unable to create ExchangeGroupKey object for tenant with external directory org id {0} with exception {1}", guid, ServerManagerLog.GetExceptionLogString(ex, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
					throw new PrivateKeyProviderException(true, string.Format("Unable to create ExchangeGroupKey object for tenant with external directory org id {0}", guid), ex);
				}
				if (!exchangeGroupKey.TryEncryptedStringToBuffer(privateKeyInformation.EncryptedPrivateKeyBlob, out array, out ex))
				{
					ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.TrustedPublishingDomainPrivateKeyProvider, ServerManagerLog.EventType.Error, this.clientContext, string.Format("Failed to DKM decrypt private key {0} with Exception {1}", keyIdentification, ServerManagerLog.GetExceptionLogString(ex, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_DkmDecryptionFailure, null, new object[]
					{
						(this.clientContext == null) ? Guid.Empty.ToString() : this.clientContext.OrgId.ToString(),
						ex
					});
					throw new PrivateKeyProviderException(true, "Failed to DKM decrypt the private key", ex);
				}
				try
				{
					rsaCapiKey.Init(array);
					this.capiKeys[privateKeyInformation.Identity] = rsaCapiKey;
				}
				catch (CryptographicException ex2)
				{
					ServerManagerLog.LogEvent(ServerManagerLog.Subcomponent.TrustedPublishingDomainPrivateKeyProvider, ServerManagerLog.EventType.Error, this.clientContext, string.Format("Failed to intialize RsaCapiKey for {0} with Exception {1}", keyIdentification, ServerManagerLog.GetExceptionLogString(ex2, ServerManagerLog.ExceptionLogOption.IncludeStack | ServerManagerLog.ExceptionLogOption.IncludeInnerException)));
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_RsaCapiKeyImportFailure, null, new object[]
					{
						(this.clientContext == null) ? Guid.Empty.ToString() : this.clientContext.OrgId.ToString(),
						ex2
					});
					throw new PrivateKeyProviderException(true, "Failed to intialize RsaCapiKey", ex2);
				}
				result = rsaCapiKey;
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, array.Length);
				}
			}
			return result;
		}

		// Token: 0x040038D8 RID: 14552
		private readonly Dictionary<string, PrivateKeyInformation> privateKeyInfos;

		// Token: 0x040038D9 RID: 14553
		private readonly PrivateKeyInformation slcKey;

		// Token: 0x040038DA RID: 14554
		private readonly RmsClientManagerContext clientContext;

		// Token: 0x040038DB RID: 14555
		private readonly Dictionary<string, RsaCapiKey> capiKeys = new Dictionary<string, RsaCapiKey>();

		// Token: 0x040038DC RID: 14556
		private DisposeTracker disposeTracker;

		// Token: 0x040038DD RID: 14557
		private bool disposed;
	}
}
