using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000020 RID: 32
	internal abstract class SyncStateDataInfo : DisposeTrackableBase
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public SyncStateDataInfo(CustomSyncState wrappedSyncState)
		{
			this.customSyncState = wrappedSyncState;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000EA76 File Offset: 0x0000CC76
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000EA7E File Offset: 0x0000CC7E
		protected internal bool IsDirty { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public int? BackendVersion
		{
			get
			{
				int? backendVersion;
				lock (this.lockObject)
				{
					backendVersion = this.customSyncState.BackendVersion;
				}
				return backendVersion;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		public int Version
		{
			get
			{
				int version;
				lock (this.lockObject)
				{
					version = this.customSyncState.Version;
				}
				return version;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000EB18 File Offset: 0x0000CD18
		public void SaveToMailbox()
		{
			if (!this.IsDirty)
			{
				return;
			}
			lock (this.lockObject)
			{
				this.customSyncState.Commit();
				this.IsDirty = false;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000EB70 File Offset: 0x0000CD70
		public object TryGetProperty(StorePropertyDefinition propDef)
		{
			object result;
			lock (this.lockObject)
			{
				result = this.customSyncState.StoreObject.TryGetProperty(propDef);
			}
			return result;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public bool TryGetProperty<T>(StorePropertyDefinition propDef, out T propertyValue)
		{
			bool result;
			lock (this.lockObject)
			{
				result = AirSyncUtility.TryGetPropertyFromBag<T>(this.customSyncState.StoreObject, propDef, out propertyValue);
			}
			return result;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public void DeleteProperty(StorePropertyDefinition propDef)
		{
			lock (this.lockObject)
			{
				this.customSyncState.StoreObject.Delete(propDef);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		public void SetProperty(StorePropertyDefinition propDef, object value)
		{
			lock (this.lockObject)
			{
				this.customSyncState.StoreObject[propDef] = value;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				lock (this.lockObject)
				{
					if (this.customSyncState != null)
					{
						this.customSyncState.Dispose();
						this.customSyncState = null;
					}
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000ED00 File Offset: 0x0000CF00
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncStateDataInfo>(this);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000ED08 File Offset: 0x0000CF08
		protected void Assign<T, RawT>(string name, RawT value) where T : ComponentData<RawT>, new()
		{
			lock (this.lockObject)
			{
				object obj2 = this.customSyncState[name];
				if (obj2 != null && obj2 is T)
				{
					T t = (T)((object)obj2);
					RawT data = t.Data;
					if ((data != null && data.Equals(value)) || (data == null && value == null))
					{
						return;
					}
				}
				T t2 = Activator.CreateInstance<T>();
				t2.Data = value;
				this.customSyncState[name] = t2;
				this.IsDirty = true;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		protected RawT Fetch<T, RawT>(string name, RawT valueIfNotSet) where T : ComponentData<RawT>, new()
		{
			RawT result;
			lock (this.lockObject)
			{
				RawT data = this.customSyncState.GetData<T, RawT>(name, valueIfNotSet);
				result = data;
			}
			return result;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000EE1C File Offset: 0x0000D01C
		protected ExDateTime? FetchDateTime(string name)
		{
			ExDateTime? result;
			lock (this.lockObject)
			{
				object obj2 = this.customSyncState[name];
				if (obj2 == null)
				{
					result = null;
				}
				else if (obj2 is NullableData<DateTimeData, ExDateTime>)
				{
					ExDateTime? data = ((NullableData<DateTimeData, ExDateTime>)obj2).Data;
					result = data;
				}
				else
				{
					if (!(obj2 is DateTimeData))
					{
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "CorruptDateTimeObjectInSyncState"
						};
					}
					ExDateTime? exDateTime = new ExDateTime?(((DateTimeData)obj2).Data);
					result = exDateTime;
				}
			}
			return result;
		}

		// Token: 0x0400023F RID: 575
		private CustomSyncState customSyncState;

		// Token: 0x04000240 RID: 576
		private object lockObject = new object();
	}
}
