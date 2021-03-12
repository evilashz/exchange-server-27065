using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C1 RID: 1473
	[Serializable]
	public sealed class StorageGroup : ADLegacyVersionableObject
	{
		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000FE80A File Offset: 0x000FCA0A
		internal override ADObjectSchema Schema
		{
			get
			{
				return StorageGroup.schema;
			}
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x000FE811 File Offset: 0x000FCA11
		internal override string MostDerivedObjectClass
		{
			get
			{
				return StorageGroup.mostDerivedClass;
			}
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000FE818 File Offset: 0x000FCA18
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!base.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				AsciiCharactersOnlyConstraint asciiCharactersOnlyConstraint = new AsciiCharactersOnlyConstraint();
				PropertyConstraintViolationError propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.Name, StorageGroupSchema.Name, null);
				if (propertyConstraintViolationError != null)
				{
					errors.Add(propertyConstraintViolationError);
				}
				if (null != this.LogFolderPath)
				{
					propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.LogFolderPath, StorageGroupSchema.LogFolderPath, null);
					if (propertyConstraintViolationError != null)
					{
						errors.Add(propertyConstraintViolationError);
					}
				}
				if (null != this.SystemFolderPath)
				{
					propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.SystemFolderPath, StorageGroupSchema.SystemFolderPath, null);
					if (propertyConstraintViolationError != null)
					{
						errors.Add(propertyConstraintViolationError);
					}
				}
			}
			if (base.Id.DomainId != null && base.Id.Depth - base.Id.DomainId.Depth < 8)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidDNDepth, this.Identity, string.Empty));
			}
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000FE8FE File Offset: 0x000FCAFE
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(StorageGroupSchema.EventLogSourceID))
			{
				this[StorageGroupSchema.EventLogSourceID] = "MSExchangeIS";
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000FE923 File Offset: 0x000FCB23
		// (set) Token: 0x060043BD RID: 17341 RVA: 0x000FE935 File Offset: 0x000FCB35
		public NonRootLocalLongFullPath LogFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[StorageGroupSchema.LogFolderPath];
			}
			internal set
			{
				this[StorageGroupSchema.LogFolderPath] = value;
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000FE943 File Offset: 0x000FCB43
		// (set) Token: 0x060043BF RID: 17343 RVA: 0x000FE955 File Offset: 0x000FCB55
		public NonRootLocalLongFullPath SystemFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[StorageGroupSchema.SystemFolderPath];
			}
			internal set
			{
				this[StorageGroupSchema.SystemFolderPath] = value;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x060043C0 RID: 17344 RVA: 0x000FE963 File Offset: 0x000FCB63
		// (set) Token: 0x060043C1 RID: 17345 RVA: 0x000FE975 File Offset: 0x000FCB75
		[Parameter(Mandatory = false)]
		public bool CircularLoggingEnabled
		{
			get
			{
				return (bool)this[StorageGroupSchema.CircularLoggingEnabled];
			}
			set
			{
				this[StorageGroupSchema.CircularLoggingEnabled] = value;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x060043C2 RID: 17346 RVA: 0x000FE988 File Offset: 0x000FCB88
		// (set) Token: 0x060043C3 RID: 17347 RVA: 0x000FE99A File Offset: 0x000FCB9A
		[Parameter(Mandatory = false)]
		public bool ZeroDatabasePages
		{
			get
			{
				return (bool)this[StorageGroupSchema.ZeroDatabasePages];
			}
			set
			{
				this[StorageGroupSchema.ZeroDatabasePages] = value;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x000FE9AD File Offset: 0x000FCBAD
		// (set) Token: 0x060043C5 RID: 17349 RVA: 0x000FE9BF File Offset: 0x000FCBBF
		public string LogFilePrefix
		{
			get
			{
				return (string)this[StorageGroupSchema.LogFilePrefix];
			}
			internal set
			{
				this[StorageGroupSchema.LogFilePrefix] = value;
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x060043C6 RID: 17350 RVA: 0x000FE9CD File Offset: 0x000FCBCD
		public int LogFileSize
		{
			get
			{
				return (int)this[StorageGroupSchema.LogFileSize];
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x000FE9DF File Offset: 0x000FCBDF
		public bool RecoveryEnabled
		{
			get
			{
				return (bool)this[StorageGroupSchema.RecoveryEnabled];
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x060043C8 RID: 17352 RVA: 0x000FE9F1 File Offset: 0x000FCBF1
		public bool OnlineDefragEnabled
		{
			get
			{
				return (bool)this[StorageGroupSchema.OnlineDefragEnabled];
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x000FEA03 File Offset: 0x000FCC03
		public bool IndexCheckingEnabled
		{
			get
			{
				return (bool)this[StorageGroupSchema.IndexCheckingEnabled];
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x000FEA15 File Offset: 0x000FCC15
		public string EventLogSourceID
		{
			get
			{
				return (string)this[StorageGroupSchema.EventLogSourceID];
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x000FEA27 File Offset: 0x000FCC27
		public int LogCheckpointDepth
		{
			get
			{
				return (int)this[StorageGroupSchema.LogCheckpointDepth];
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x060043CC RID: 17356 RVA: 0x000FEA39 File Offset: 0x000FCC39
		public bool CommitDefault
		{
			get
			{
				return (int)this[StorageGroupSchema.CommitDefault] != 0;
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x000FEA51 File Offset: 0x000FCC51
		public int DatabaseExtensionSize
		{
			get
			{
				return (int)this[StorageGroupSchema.DatabaseExtensionSize];
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x000FEA63 File Offset: 0x000FCC63
		public int PageFragment
		{
			get
			{
				return (int)this[StorageGroupSchema.PageFragment];
			}
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000FEA75 File Offset: 0x000FCC75
		public int PageTempDBMinimum
		{
			get
			{
				return (int)this[StorageGroupSchema.PageTempDBMinimum];
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x000FEA87 File Offset: 0x000FCC87
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[StorageGroupSchema.Server];
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000FEA99 File Offset: 0x000FCC99
		public string ServerName
		{
			get
			{
				return (string)this[StorageGroupSchema.ServerName];
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x060043D2 RID: 17362 RVA: 0x000FEAAB File Offset: 0x000FCCAB
		// (set) Token: 0x060043D3 RID: 17363 RVA: 0x000FEABD File Offset: 0x000FCCBD
		public bool Recovery
		{
			get
			{
				return (bool)this[StorageGroupSchema.Recovery];
			}
			internal set
			{
				this[StorageGroupSchema.Recovery] = value;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x000FEAD0 File Offset: 0x000FCCD0
		// (set) Token: 0x060043D5 RID: 17365 RVA: 0x000FEAE2 File Offset: 0x000FCCE2
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return (string)this[StorageGroupSchema.Name];
			}
			set
			{
				this[StorageGroupSchema.Name] = value;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x000FEAF0 File Offset: 0x000FCCF0
		// (set) Token: 0x060043D7 RID: 17367 RVA: 0x000FEB02 File Offset: 0x000FCD02
		public CanRunDefaultUpdateState? CanRunDefaultUpdate
		{
			get
			{
				return (CanRunDefaultUpdateState?)this[StorageGroupSchema.CanRunDefaultUpdate];
			}
			internal set
			{
				this[StorageGroupSchema.CanRunDefaultUpdate] = value;
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x000FEB15 File Offset: 0x000FCD15
		// (set) Token: 0x060043D9 RID: 17369 RVA: 0x000FEB27 File Offset: 0x000FCD27
		public CanRunRestoreState? CanRunRestore
		{
			get
			{
				return (CanRunRestoreState?)this[StorageGroupSchema.CanRunRestore];
			}
			internal set
			{
				this[StorageGroupSchema.CanRunRestore] = value;
			}
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000FEB3A File Offset: 0x000FCD3A
		internal Database[] GetDatabases()
		{
			return base.Session.Find<Database>(base.Id, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000FEB51 File Offset: 0x000FCD51
		internal Server GetServer()
		{
			return base.Session.Read<Server>(this.Server);
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000FEB64 File Offset: 0x000FCD64
		internal MailboxDatabase[] GetMailboxDatabases()
		{
			return base.Session.Find<MailboxDatabase>(base.Id, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000FEB7C File Offset: 0x000FCD7C
		internal static object ServerGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				result = ((ADObjectId)propertyBag[ADObjectSchema.Id]).AncestorDN(2);
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", ex.Message), StorageGroupSchema.Server, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000FEBE0 File Offset: 0x000FCDE0
		internal static object ServerNameGetter(IPropertyBag propertyBag)
		{
			return ((ADObjectId)StorageGroup.ServerGetter(propertyBag)).Name;
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000FEBF2 File Offset: 0x000FCDF2
		internal static object StorageGroupNameGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.RawName];
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000FEBFF File Offset: 0x000FCDFF
		internal static void StorageGroupNameSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.RawName] = value;
		}

		// Token: 0x04002E15 RID: 11797
		private static StorageGroupSchema schema = ObjectSchema.GetInstance<StorageGroupSchema>();

		// Token: 0x04002E16 RID: 11798
		private static string mostDerivedClass = "msExchStorageGroup";
	}
}
