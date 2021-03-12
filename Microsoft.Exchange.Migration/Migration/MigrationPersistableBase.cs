using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationPersistableBase : IMigrationPersistable, IMigrationSerializable
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000DD90 File Offset: 0x0000BF90
		protected MigrationPersistableBase()
		{
			this.Version = -1L;
			this.ExtendedProperties = new PersistableDictionary();
			this.reportData = new Lazy<ReportData>(new Func<ReportData>(this.CreateDiagnosticReport), LazyThreadSafetyMode.ExecutionAndPublication);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000DDC3 File Offset: 0x0000BFC3
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000DDCB File Offset: 0x0000BFCB
		public long Version { get; protected set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public StoreObjectId StoreObjectId { get; protected set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000DDE5 File Offset: 0x0000BFE5
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000DDED File Offset: 0x0000BFED
		public ExDateTime CreationTime { get; protected set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000DDF6 File Offset: 0x0000BFF6
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000DDFE File Offset: 0x0000BFFE
		public PersistableDictionary ExtendedProperties { get; protected set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000DE07 File Offset: 0x0000C007
		public virtual PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MigrationPersistableBase.MigrationBaseDefinitions;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000DE0E File Offset: 0x0000C00E
		public virtual PropertyDefinition[] InitializationPropertyDefinitions
		{
			get
			{
				return MigrationPersistableBase.VersionPropertyDefinitions;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000DE15 File Offset: 0x0000C015
		public virtual long CurrentSupportedVersion
		{
			get
			{
				return this.MinimumSupportedVersion;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003BD RID: 957
		public abstract long MinimumSupportedVersion { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003BE RID: 958
		public abstract long MaximumSupportedVersion { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000DE1D File Offset: 0x0000C01D
		public virtual long MinimumSupportedPersistableVersion
		{
			get
			{
				return this.MinimumSupportedVersion;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000DE25 File Offset: 0x0000C025
		public ReportData ReportData
		{
			get
			{
				return this.reportData.Value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000DE32 File Offset: 0x0000C032
		protected bool IsPersisted
		{
			get
			{
				return this.Version != -1L;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000DE41 File Offset: 0x0000C041
		protected virtual Guid ReportGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000DE48 File Offset: 0x0000C048
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000DE50 File Offset: 0x0000C050
		protected OrganizationId OrganizationId { get; set; }

		// Token: 0x060003C5 RID: 965 RVA: 0x0000DF24 File Offset: 0x0000C124
		public virtual bool TryLoad(IMigrationDataProvider dataProvider, StoreObjectId id)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(id, "id");
			bool success = true;
			MigrationUtil.RunTimedOperation(delegate()
			{
				using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(dataProvider, id, this.InitializationPropertyDefinitions))
				{
					this.OrganizationId = dataProvider.OrganizationId;
					if (!this.InitializeFromMessageItem(migrationStoreObject))
					{
						success = false;
						return;
					}
					migrationStoreObject.Load(this.PropertyDefinitions);
					if (!this.ReadFromMessageItem(migrationStoreObject))
					{
						success = false;
						return;
					}
					this.LoadLinkedStoredObjects(migrationStoreObject, dataProvider);
				}
				this.CheckVersion();
			}, this);
			return success;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000DF8C File Offset: 0x0000C18C
		public virtual bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			if (this.Version < this.MinimumSupportedPersistableVersion)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "not reading extended properties for version:" + this.Version, new object[0]);
				return false;
			}
			this.ExtendedProperties = MigrationHelper.GetDictionaryProperty(message, MigrationBatchMessageSchema.MigrationPersistableDictionary, true);
			this.ReadStoreObjectIdProperties(message);
			return true;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		public virtual void CreateInStore(IMigrationDataProvider dataProvider, Action<IMigrationStoreObject> streamAction)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.AssertOrThrow(this.StoreObjectId == null, "Object should not have been created already.", new object[0]);
			using (IMigrationStoreObject migrationStoreObject = this.CreateStoreObject(dataProvider))
			{
				this.InitializeVersion();
				if (streamAction != null)
				{
					streamAction(migrationStoreObject);
				}
				this.WriteToMessageItem(migrationStoreObject, false);
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
				migrationStoreObject.Load(this.InitializationPropertyDefinitions);
				this.ReadStoreObjectIdProperties(migrationStoreObject);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E078 File Offset: 0x0000C278
		public virtual void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			this.CheckVersion();
			message[MigrationBatchMessageSchema.MigrationVersion] = this.Version;
			if (this.Version < this.MinimumSupportedPersistableVersion)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "not writing extended properties for version:" + this.Version, new object[0]);
				return;
			}
			this.WriteExtendedPropertiesToMessageItem(message);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E0E3 File Offset: 0x0000C2E3
		public virtual XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return this.GetDiagnosticInfo(dataProvider, argument, new XElement("PersistableBase"));
		}

		// Token: 0x060003CA RID: 970
		public abstract IMigrationStoreObject FindStoreObject(IMigrationDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties);

		// Token: 0x060003CB RID: 971 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public IMigrationStoreObject FindStoreObject(IMigrationDataProvider dataProvider, PropertyDefinition[] properties)
		{
			ExAssert.RetailAssert(this.StoreObjectId != null, "Need to persist the objects before trying to retrieve their storage object.");
			if (properties == null)
			{
				properties = this.PropertyDefinitions;
			}
			return this.FindStoreObject(dataProvider, this.StoreObjectId, properties);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E12D File Offset: 0x0000C32D
		public IMigrationStoreObject FindStoreObject(IMigrationDataProvider dataProvider)
		{
			return this.FindStoreObject(dataProvider, null);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E137 File Offset: 0x0000C337
		protected virtual void LoadLinkedStoredObjects(IMigrationStoreObject item, IMigrationDataProvider dataProvider)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E144 File Offset: 0x0000C344
		protected XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument, XElement parent)
		{
			parent.Add(new XElement("version", this.Version));
			parent.Add(new XElement("created", (this.StoreObjectId != null) ? this.CreationTime.ToString() : null));
			parent.Add(new XElement("reportGuid", this.ExtendedProperties.Get<Guid>("report", Guid.Empty)));
			if (this.Version >= this.MinimumSupportedPersistableVersion)
			{
				IOrderedEnumerable<DictionaryEntry> orderedEnumerable = from DictionaryEntry item in this.ExtendedProperties
				orderby item.Key
				select item;
				foreach (DictionaryEntry dictionaryEntry in orderedEnumerable)
				{
					IEnumerable<object> enumerable = dictionaryEntry.Value as IEnumerable<object>;
					if (enumerable != null)
					{
						XElement xelement = new XElement((string)dictionaryEntry.Key);
						foreach (object obj in enumerable)
						{
							xelement.Add(new XElement("Element", MigrationPersistableBase.GetDiagnosticObject(obj)));
						}
						parent.Add(xelement);
					}
					else if (dictionaryEntry.Value != null)
					{
						parent.Add(new XElement((string)dictionaryEntry.Key, MigrationPersistableBase.GetDiagnosticObject(dictionaryEntry.Value)));
					}
					else
					{
						parent.Add(new XElement((string)dictionaryEntry.Key));
					}
				}
			}
			if (dataProvider != null && argument.HasArgument("storage"))
			{
				parent.Add(this.GetStorageDiagnosticInfo(dataProvider, argument));
			}
			return parent;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E344 File Offset: 0x0000C544
		protected virtual XElement GetStorageDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			ExAssert.RetailAssert(this.StoreObjectId != null, "Need to persist the objects before trying to retrieve their diagnostics");
			XElement diagnosticInfo;
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(dataProvider, this.StoreObjectId, this.PropertyDefinitions))
			{
				diagnosticInfo = migrationStoreObject.GetDiagnosticInfo(this.PropertyDefinitions, argument);
			}
			return diagnosticInfo;
		}

		// Token: 0x060003D0 RID: 976
		protected abstract IMigrationStoreObject CreateStoreObject(IMigrationDataProvider dataProvider);

		// Token: 0x060003D1 RID: 977 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		protected void InitializeVersion()
		{
			if (this.CurrentSupportedVersion == -1L)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "not writing version. we're not allowed", new object[0]);
				return;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "setting version to {0}", new object[]
			{
				this.CurrentSupportedVersion
			});
			this.Version = this.CurrentSupportedVersion;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E40C File Offset: 0x0000C60C
		protected virtual bool InitializeFromMessageItem(IMigrationStoreObject message)
		{
			long valueOrDefault = message.GetValueOrDefault<long>(MigrationBatchMessageSchema.MigrationVersion, -1L);
			if (valueOrDefault == -1L)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "object doesn't exist", new object[0]);
				return false;
			}
			this.Version = valueOrDefault;
			return true;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E448 File Offset: 0x0000C648
		protected void SetVersion(IMigrationDataProvider dataProvider, long version)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(dataProvider))
			{
				this.CheckVersion(version);
				migrationStoreObject.OpenAsReadWrite();
				migrationStoreObject[MigrationBatchMessageSchema.MigrationVersion] = version;
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
				this.Version = version;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		protected void CheckVersion()
		{
			this.CheckVersion(this.Version);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		protected void CheckVersion(long version)
		{
			if (version < this.MinimumSupportedVersion || version > this.MaximumSupportedVersion)
			{
				throw new MigrationVersionMismatchException(version, this.MaximumSupportedVersion);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		protected virtual void SaveExtendedProperties(IMigrationDataProvider provider)
		{
			MigrationUtil.AssertOrThrow(this.StoreObjectId != null, "Object should have been created before trying to save properties.", new object[0]);
			using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(provider, MigrationPersistableBase.MigrationBaseDefinitions))
			{
				migrationStoreObject.OpenAsReadWrite();
				this.WriteExtendedPropertiesToMessageItem(migrationStoreObject);
				migrationStoreObject.Save(SaveMode.NoConflictResolution);
			}
			if (this.ReportGuid != Guid.Empty)
			{
				provider.FlushReport(this.ReportData);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E564 File Offset: 0x0000C764
		protected virtual void WriteExtendedPropertiesToMessageItem(IMigrationStoreObject message)
		{
			MigrationHelper.SetDictionaryProperty(message, MigrationBatchMessageSchema.MigrationPersistableDictionary, this.ExtendedProperties);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E577 File Offset: 0x0000C777
		protected void ReadStoreObjectIdProperties(IMigrationStoreObject storeObject)
		{
			this.StoreObjectId = storeObject.Id;
			this.CreationTime = storeObject.CreationTime;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E594 File Offset: 0x0000C794
		private static object GetDiagnosticObject(object obj)
		{
			byte[] array = obj as byte[];
			if (array != null)
			{
				try
				{
					obj = new Guid(array);
				}
				catch (ArgumentException)
				{
				}
			}
			return obj;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		private ReportData CreateDiagnosticReport()
		{
			if (!(this.ReportGuid != Guid.Empty))
			{
				return null;
			}
			return new ReportData(this.ReportGuid, ReportVersion.ReportE14R6Compression);
		}

		// Token: 0x0400010E RID: 270
		protected const long InvalidVersion = -1L;

		// Token: 0x0400010F RID: 271
		protected static readonly PropertyDefinition[] VersionPropertyDefinitions = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationVersion
		};

		// Token: 0x04000110 RID: 272
		protected static readonly PropertyDefinition[] MigrationBaseDefinitions = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationPersistableDictionary
		};

		// Token: 0x04000111 RID: 273
		private readonly Lazy<ReportData> reportData;
	}
}
