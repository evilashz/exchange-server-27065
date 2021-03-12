using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public abstract class CompoundSyncObjectIdParameter : IIdentityParameter
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x0001F43D File Offset: 0x0001D63D
		protected CompoundSyncObjectIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.RawIdentity = identity;
			this.InitializeServiceAndObjectIds();
			this.CheckNoWildcardedServiceInstnaceIdParses();
			this.CheckNoWildcardedSyncObjectIdParses();
			this.CheckOnlyOneWildCardAtTheStartOrEnd();
			this.CheckObjectClassIsValid();
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001F478 File Offset: 0x0001D678
		protected CompoundSyncObjectIdParameter(ObjectId compoundSyncObjectId)
		{
			this.Initialize(compoundSyncObjectId);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001F487 File Offset: 0x0001D687
		protected CompoundSyncObjectIdParameter()
		{
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001F48F File Offset: 0x0001D68F
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0001F497 File Offset: 0x0001D697
		public string RawIdentity { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		internal string ServiceInstanceIdentity { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001F4B1 File Offset: 0x0001D6B1
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001F4B9 File Offset: 0x0001D6B9
		internal string SyncObjectIdentity { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001F4C2 File Offset: 0x0001D6C2
		internal bool IsServiceInstanceDefinied
		{
			get
			{
				return this.serviceInstance != null;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		internal ServiceInstanceId ServiceInstance
		{
			get
			{
				return this.serviceInstance;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000900 RID: 2304
		public abstract IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new();

		// Token: 0x06000901 RID: 2305 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		public void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			CompoundSyncObjectId compoundSyncObjectId = objectId as CompoundSyncObjectId;
			if (compoundSyncObjectId == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(CompoundSyncObjectId).Name), "objectId");
			}
			this.serviceInstance = compoundSyncObjectId.ServiceInstanceId;
			this.ServiceInstanceIdentity = this.serviceInstance.ToString();
			this.syncObjectId = compoundSyncObjectId.SyncObjectId;
			this.SyncObjectIdentity = this.syncObjectId.ToString();
			this.RawIdentity = compoundSyncObjectId.ToString();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001F584 File Offset: 0x0001D784
		public override string ToString()
		{
			return string.Format("{0}\\{1}", this.ServiceInstanceIdentity, this.SyncObjectIdentity);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001F59C File Offset: 0x0001D79C
		protected void GetSyncObjectIdElements(out string contextIdString, out string objectIdString, out string objectClassString)
		{
			if (this.syncObjectId != null)
			{
				contextIdString = this.syncObjectId.ContextId;
				objectIdString = this.syncObjectId.ObjectId;
				objectClassString = this.syncObjectId.ObjectClass.ToString();
				return;
			}
			contextIdString = null;
			objectIdString = null;
			objectClassString = null;
			string[] identityElements = SyncObjectId.GetIdentityElements(this.SyncObjectIdentity);
			if (this.SyncObjectIdentity.EndsWith("*"))
			{
				switch (identityElements.Length)
				{
				case 1:
					goto IL_8A;
				case 2:
					break;
				case 3:
					objectIdString = identityElements[2];
					break;
				default:
					return;
				}
				objectClassString = identityElements[1];
				IL_8A:
				contextIdString = identityElements[0];
				return;
			}
			switch (identityElements.Length)
			{
			case 1:
				objectIdString = identityElements[0];
				return;
			case 2:
				objectIdString = identityElements[1];
				objectClassString = identityElements[0];
				return;
			case 3:
				objectIdString = identityElements[2];
				objectClassString = identityElements[1];
				contextIdString = identityElements[0];
				return;
			default:
				return;
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001F674 File Offset: 0x0001D874
		private static bool TryParseSyncObjectId(string identity, out SyncObjectId syncObjectId)
		{
			syncObjectId = null;
			try
			{
				syncObjectId = SyncObjectId.Parse(identity);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		private static bool TryParseServiceInstanceId(string identity, out ServiceInstanceId serviceInstanceId)
		{
			serviceInstanceId = null;
			try
			{
				serviceInstanceId = new ServiceInstanceId(identity);
			}
			catch (InvalidServiceInstanceIdException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		private void InitializeServiceAndObjectIds()
		{
			string[] array = this.RawIdentity.Split(CompoundSyncObjectIdParameter.Separators);
			if (array.Length > 2)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
			if (array.Length == 2)
			{
				this.ServiceInstanceIdentity = array[0];
				this.SyncObjectIdentity = array[1];
			}
			else
			{
				this.ServiceInstanceIdentity = "*";
				this.SyncObjectIdentity = array[0];
			}
			if (string.IsNullOrEmpty(this.SyncObjectIdentity))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001F770 File Offset: 0x0001D970
		private void CheckNoWildcardedServiceInstnaceIdParses()
		{
			if (!this.ServiceInstanceIdentity.Contains("*") && !CompoundSyncObjectIdParameter.TryParseServiceInstanceId(this.ServiceInstanceIdentity, out this.serviceInstance))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001F7BC File Offset: 0x0001D9BC
		private void CheckNoWildcardedSyncObjectIdParses()
		{
			if (!this.SyncObjectIdentity.Contains("*") && !CompoundSyncObjectIdParameter.TryParseSyncObjectId(this.SyncObjectIdentity, out this.syncObjectId))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001F808 File Offset: 0x0001DA08
		private void CheckOnlyOneWildCardAtTheStartOrEnd()
		{
			if (this.SyncObjectIdentity.Contains("*") && !this.SyncObjectIdentity.StartsWith("*") && !this.SyncObjectIdentity.EndsWith("*"))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
			if (this.SyncObjectIdentity.Contains("*") && this.SyncObjectIdentity.IndexOf("*") != this.SyncObjectIdentity.LastIndexOf("*"))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001F8B4 File Offset: 0x0001DAB4
		private void CheckObjectClassIsValid()
		{
			string[] identityElements = SyncObjectId.GetIdentityElements(this.SyncObjectIdentity);
			if (this.SyncObjectIdentity.EndsWith("*") && identityElements.Length > 1 && identityElements[1] != "*" && !Enum.IsDefined(typeof(DirectoryObjectClass), identityElements[1]))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
			if (this.SyncObjectIdentity.StartsWith("*") && identityElements.Length > 1 && identityElements[identityElements.Length - 2] != "*" && !Enum.IsDefined(typeof(DirectoryObjectClass), identityElements[identityElements.Length - 2]))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
		}

		// Token: 0x0400025F RID: 607
		private static readonly char[] Separators = new char[]
		{
			'\\'
		};

		// Token: 0x04000260 RID: 608
		private ServiceInstanceId serviceInstance;

		// Token: 0x04000261 RID: 609
		private SyncObjectId syncObjectId;
	}
}
