using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CacheEntryBase : IDiagnosableObject
	{
		// Token: 0x06000128 RID: 296 RVA: 0x000050A4 File Offset: 0x000032A4
		public CacheEntryBase(AnchorContext context, ADUser user)
		{
			this.Context = context;
			this.ObjectId = user.ObjectId;
			this.OrganizationId = (user.OrganizationId ?? OrganizationId.ForestWideOrgId);
			this.UserPrincipalName = user.UserPrincipalName;
			this.LastSyncTime = ExDateTime.MinValue;
			this.ADProvider = new AnchorADProvider(this.Context, this.OrganizationId, null);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000510E File Offset: 0x0000330E
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00005116 File Offset: 0x00003316
		public ADObjectId ObjectId { get; protected set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000511F File Offset: 0x0000331F
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005127 File Offset: 0x00003327
		public OrganizationId OrganizationId { get; protected set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005130 File Offset: 0x00003330
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005138 File Offset: 0x00003338
		public string UserPrincipalName { get; protected set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012F RID: 303
		public abstract bool IsLocal { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000130 RID: 304
		public abstract bool IsActive { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000131 RID: 305
		public abstract int UniqueEntryCount { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005141 File Offset: 0x00003341
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005149 File Offset: 0x00003349
		public ExDateTime LastSyncTime { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005152 File Offset: 0x00003352
		public bool IsStale
		{
			get
			{
				return this.LastSyncTime + this.StalenessInterval < ExDateTime.UtcNow;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000516F File Offset: 0x0000336F
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005177 File Offset: 0x00003377
		public Exception ServiceException { get; internal set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005180 File Offset: 0x00003380
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005188 File Offset: 0x00003388
		public IAnchorADProvider ADProvider { get; protected set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005191 File Offset: 0x00003391
		string IDiagnosableObject.HashableIdentity
		{
			get
			{
				return this.UserPrincipalName;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005199 File Offset: 0x00003399
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000051A1 File Offset: 0x000033A1
		private protected AnchorContext Context { protected get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000051AC File Offset: 0x000033AC
		private TimeSpan StalenessInterval
		{
			get
			{
				TimeSpan config = this.Context.Config.GetConfig<TimeSpan>("ScannerTimeDelay");
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)((int)(this.Context.Config.GetConfig<TimeSpan>("ScannerInitialTimeDelay").TotalSeconds / 2.0)));
				if (!(timeSpan != TimeSpan.Zero) || !(timeSpan < config))
				{
					return config;
				}
				return timeSpan;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005217 File Offset: 0x00003417
		public virtual bool Sync()
		{
			this.LastSyncTime = ExDateTime.UtcNow;
			return true;
		}

		// Token: 0x0600013E RID: 318
		public abstract void Activate();

		// Token: 0x0600013F RID: 319
		public abstract void Deactivate();

		// Token: 0x06000140 RID: 320 RVA: 0x00005228 File Offset: 0x00003428
		public virtual bool Validate()
		{
			if (this.IsStale && !this.Sync())
			{
				this.Context.Logger.Log(MigrationEventType.Error, "CacheEntry {0} couldn't sync", new object[]
				{
					this
				});
				return false;
			}
			if (!this.IsActive)
			{
				this.Context.Logger.Log(MigrationEventType.Error, "CacheEntry {0} isn't active", new object[]
				{
					this
				});
				return false;
			}
			if (!this.IsLocal)
			{
				this.Context.Logger.Log(MigrationEventType.Error, "CacheEntry {0} doesn't exist on the local server", new object[]
				{
					this.OrganizationId
				});
				return false;
			}
			int config = this.Context.Config.GetConfig<int>("MaximumCacheEntryCountPerOrganization");
			if (config >= 0)
			{
				int uniqueEntryCount = this.UniqueEntryCount;
				if (uniqueEntryCount > config)
				{
					string text = string.Format("max cache entry count {0} for organization {1} exists in {2} multiple locations", config, this.OrganizationId, uniqueEntryCount);
					this.Context.Logger.LogEvent(MigrationEventType.Error, new string[]
					{
						text
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005331 File Offset: 0x00003531
		public string GetDiagnosticComponentName()
		{
			return "CacheEntryBase";
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005338 File Offset: 0x00003538
		public XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			xelement.Add(new XElement("objectId", this.ObjectId));
			xelement.Add(new XElement("organizationId", this.OrganizationId));
			xelement.Add(new XElement("userPrincipalName", this.UserPrincipalName));
			return xelement;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000053A8 File Offset: 0x000035A8
		public override string ToString()
		{
			return this.UserPrincipalName;
		}
	}
}
