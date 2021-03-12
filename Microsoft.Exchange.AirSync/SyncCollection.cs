using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV141;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200008F RID: 143
	internal class SyncCollection : DisposeTrackableBase
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00029524 File Offset: 0x00027724
		protected SyncCollection(StoreSession storeSession, int protocolVersion)
		{
			this.storeSession = storeSession;
			this.protocolVersion = protocolVersion;
			this.WindowSize = ((this.protocolVersion >= 121) ? 100 : 512);
			this.Permissions = SyncPermissions.FullAccess;
			this.folderType = DefaultFolderType.None;
			this.ConflictResolutionPolicy = ConflictResolutionPolicy.ServerWins;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000295E3 File Offset: 0x000277E3
		private static QueryFilter BuildIcsPropertyGroupFilter()
		{
			return new BitMaskFilter(AirSyncStateSchema.PropertyGroupChangeMask, 1012222UL, true);
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000295F8 File Offset: 0x000277F8
		public virtual StoreObjectId NativeStoreObjectId
		{
			get
			{
				if (this.nativeStoreObjectId == null)
				{
					FolderSyncStateMetadata folderSyncStateMetadata = this.GetFolderSyncStateMetadata();
					this.nativeStoreObjectId = ((folderSyncStateMetadata == null || folderSyncStateMetadata.IPMFolderId == null) ? StoreObjectId.Deserialize(this.SyncProviderFactory.GetCollectionIdBytes()) : folderSyncStateMetadata.IPMFolderId);
				}
				return this.nativeStoreObjectId;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00029643 File Offset: 0x00027843
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0002964B File Offset: 0x0002784B
		public SyncPermissions Permissions { get; protected set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00029654 File Offset: 0x00027854
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0002965C File Offset: 0x0002785C
		public int MaxItems
		{
			get
			{
				return this.maxItems;
			}
			set
			{
				this.maxItems = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00029665 File Offset: 0x00027865
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0002966D File Offset: 0x0002786D
		public bool DeletesAsMoves
		{
			get
			{
				return this.deletesAsMoves;
			}
			set
			{
				this.deletesAsMoves = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00029676 File Offset: 0x00027876
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0002967E File Offset: 0x0002787E
		public bool GetChanges
		{
			get
			{
				return this.getChanges;
			}
			set
			{
				this.getChanges = value;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00029687 File Offset: 0x00027887
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0002968F File Offset: 0x0002788F
		public SyncCommandItem[] ClientCommands
		{
			get
			{
				return this.clientCommands;
			}
			set
			{
				this.clientCommands = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00029698 File Offset: 0x00027898
		public Dictionary<ISyncItemId, SyncCommandItem> ClientFetchedItems
		{
			get
			{
				return this.clientFetchedItems;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000296A0 File Offset: 0x000278A0
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x000296A8 File Offset: 0x000278A8
		public XmlNode CommandRequestXmlNode
		{
			get
			{
				return this.commandRequestXmlNode;
			}
			set
			{
				this.commandRequestXmlNode = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x000296B1 File Offset: 0x000278B1
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x000296B9 File Offset: 0x000278B9
		public XmlNode CommandResponseXmlNode
		{
			get
			{
				return this.commandResponseXmlNode;
			}
			set
			{
				this.commandResponseXmlNode = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000296C2 File Offset: 0x000278C2
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x000296CA File Offset: 0x000278CA
		public XmlNode ResponsesResponseXmlNode
		{
			get
			{
				return this.responsesResponseXmlNode;
			}
			set
			{
				this.responsesResponseXmlNode = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000296D3 File Offset: 0x000278D3
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x000296DB File Offset: 0x000278DB
		public XmlNode CollectionResponseXmlNode
		{
			get
			{
				return this.collectionResponseXmlNode;
			}
			set
			{
				this.collectionResponseXmlNode = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000296E4 File Offset: 0x000278E4
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x000296EC File Offset: 0x000278EC
		public XmlNode CollectionNode
		{
			get
			{
				return this.collectionNode;
			}
			set
			{
				this.collectionNode = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000296F5 File Offset: 0x000278F5
		public List<SyncCommandItem> Responses
		{
			get
			{
				return this.responses;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000296FD File Offset: 0x000278FD
		public List<SyncCommandItem> DupeList
		{
			get
			{
				return this.dupeList;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00029705 File Offset: 0x00027905
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0002970D File Offset: 0x0002790D
		public bool DupesFilledWindowSize
		{
			get
			{
				return this.dupesFilledWindowSize;
			}
			set
			{
				this.dupesFilledWindowSize = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00029716 File Offset: 0x00027916
		public bool HasOptionsNodes
		{
			get
			{
				return this.optionsList != null && this.optionsList.Count != 0 && this.optionsList[0].OptionsNode != null;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00029745 File Offset: 0x00027945
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x0002974D File Offset: 0x0002794D
		public bool HasAddsOrChangesToReturnToClientImmediately
		{
			get
			{
				return this.hasAddsOrChangesToReturnToClientImmediately;
			}
			set
			{
				this.hasAddsOrChangesToReturnToClientImmediately = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00029756 File Offset: 0x00027956
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0002975E File Offset: 0x0002795E
		public bool HasServerChanges
		{
			get
			{
				return this.hasServerChanges;
			}
			set
			{
				this.hasServerChanges = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00029767 File Offset: 0x00027967
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0002976F File Offset: 0x0002796F
		public bool HaveChanges
		{
			get
			{
				return this.haveChanges;
			}
			set
			{
				this.haveChanges = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00029778 File Offset: 0x00027978
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x00029780 File Offset: 0x00027980
		public bool HasBeenSaved
		{
			get
			{
				return this.hasBeenSaved;
			}
			set
			{
				this.hasBeenSaved = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00029789 File Offset: 0x00027989
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00029791 File Offset: 0x00027991
		public AirSyncV25FilterTypes FilterTypeInSyncState
		{
			get
			{
				return this.filterTypeInSyncState;
			}
			set
			{
				this.filterTypeInSyncState = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0002979A File Offset: 0x0002799A
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x000297A2 File Offset: 0x000279A2
		public bool OptionsSentAreDifferentForV121AndLater
		{
			get
			{
				return this.optionsSentAreDifferentForV121AndLater;
			}
			set
			{
				this.optionsSentAreDifferentForV121AndLater = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x000297AB File Offset: 0x000279AB
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x000297B3 File Offset: 0x000279B3
		public AirSyncV25FilterTypes FilterType
		{
			get
			{
				return this.filterType;
			}
			set
			{
				this.filterType = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000297BC File Offset: 0x000279BC
		public bool MidnightRollover
		{
			get
			{
				bool flag = false;
				AirSyncDiagnostics.FaultInjectionTracer.TraceTest<bool>(2928028989U, ref flag);
				if (flag)
				{
					return true;
				}
				FolderSyncStateMetadata folderSyncStateMetadata = this.GetFolderSyncStateMetadata();
				return folderSyncStateMetadata != null && folderSyncStateMetadata.HasValidNullSyncData && this.today.UtcTicks > folderSyncStateMetadata.AirSyncLastSyncTime;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00029808 File Offset: 0x00027A08
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00029810 File Offset: 0x00027A10
		public ISyncProviderFactory SyncProviderFactory
		{
			get
			{
				return this.syncProviderFactory;
			}
			set
			{
				this.syncProviderFactory = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00029819 File Offset: 0x00027A19
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00029824 File Offset: 0x00027A24
		public string ClassType
		{
			get
			{
				return this.classType;
			}
			set
			{
				this.classType = value;
				if (value != null && this.classTypeValidations.Count > 0)
				{
					foreach (KeyValuePair<object, Action<object>> keyValuePair in this.classTypeValidations)
					{
						keyValuePair.Value(keyValuePair.Key);
					}
					this.classTypeValidations.Clear();
				}
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000298A8 File Offset: 0x00027AA8
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x000298B0 File Offset: 0x00027AB0
		public FolderSyncState SyncState
		{
			get
			{
				return this.syncState;
			}
			set
			{
				this.syncState = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000298B9 File Offset: 0x00027AB9
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x000298C1 File Offset: 0x00027AC1
		public FolderSync FolderSync
		{
			get
			{
				return this.folderSync;
			}
			set
			{
				this.folderSync = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000298CA File Offset: 0x00027ACA
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x000298D2 File Offset: 0x00027AD2
		public uint SyncKey
		{
			get
			{
				return this.syncKey;
			}
			set
			{
				this.syncKey = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x000298DB File Offset: 0x00027ADB
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x000298E3 File Offset: 0x00027AE3
		public uint RecoverySyncKey
		{
			get
			{
				return this.recoverySyncKey;
			}
			set
			{
				this.recoverySyncKey = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000298EC File Offset: 0x00027AEC
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x000298F4 File Offset: 0x00027AF4
		public string SyncTypeString
		{
			get
			{
				return this.syncType;
			}
			set
			{
				this.syncType = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x000298FD File Offset: 0x00027AFD
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00029905 File Offset: 0x00027B05
		public string SyncKeyString
		{
			get
			{
				return this.syncKeyString;
			}
			set
			{
				this.syncKeyString = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0002990E File Offset: 0x00027B0E
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00029916 File Offset: 0x00027B16
		public uint ResponseSyncKey
		{
			get
			{
				return this.responseSyncKey;
			}
			set
			{
				this.responseSyncKey = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0002991F File Offset: 0x00027B1F
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00029927 File Offset: 0x00027B27
		public string CollectionId
		{
			get
			{
				return this.collectionId;
			}
			set
			{
				this.collectionId = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00029930 File Offset: 0x00027B30
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00029938 File Offset: 0x00027B38
		public bool ReturnCollectionId
		{
			get
			{
				return this.returnCollectionId;
			}
			set
			{
				this.returnCollectionId = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00029941 File Offset: 0x00027B41
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00029949 File Offset: 0x00027B49
		public int WindowSize
		{
			get
			{
				return this.windowSize;
			}
			set
			{
				this.windowSize = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00029952 File Offset: 0x00027B52
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0002995A File Offset: 0x00027B5A
		public ConflictResolutionPolicy ConflictResolutionPolicy { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00029963 File Offset: 0x00027B63
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0002996B File Offset: 0x00027B6B
		public ConflictResolutionPolicy ClientConflictResolutionPolicy
		{
			get
			{
				return this.clientConflictResolutionPolicy;
			}
			set
			{
				this.clientConflictResolutionPolicy = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00029974 File Offset: 0x00027B74
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0002997C File Offset: 0x00027B7C
		public bool MoreAvailable
		{
			get
			{
				return this.moreAvailable;
			}
			set
			{
				this.moreAvailable = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00029985 File Offset: 0x00027B85
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0002998D File Offset: 0x00027B8D
		public SyncOperations ServerChanges
		{
			get
			{
				return this.serverChanges;
			}
			set
			{
				this.serverChanges = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00029996 File Offset: 0x00027B96
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0002999E File Offset: 0x00027B9E
		public SyncBase.ErrorCodeStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000299A7 File Offset: 0x00027BA7
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000299AF File Offset: 0x00027BAF
		public bool HasFilterNode
		{
			get
			{
				return this.hasFilterNode;
			}
			set
			{
				this.hasFilterNode = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000299B8 File Offset: 0x00027BB8
		public string InternalName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.collectionId))
				{
					return this.collectionId;
				}
				if (this.classType != null)
				{
					return this.classType;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000299E2 File Offset: 0x00027BE2
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x000299EA File Offset: 0x00027BEA
		public bool AllowRecovery
		{
			get
			{
				return this.allowRecovery;
			}
			set
			{
				this.allowRecovery = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000299F3 File Offset: 0x00027BF3
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x000299FB File Offset: 0x00027BFB
		public StoreSession StoreSession
		{
			protected get
			{
				return this.storeSession;
			}
			set
			{
				this.storeSession = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00029A04 File Offset: 0x00027C04
		public int ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00029A0C File Offset: 0x00027C0C
		public virtual bool SupportsSubscriptions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00029A0F File Offset: 0x00027C0F
		public virtual PropertyDefinition[] PropertiesToSaveForNullSync
		{
			get
			{
				return SyncCollection.propertiesToSaveForNullSync;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00029A18 File Offset: 0x00027C18
		internal int FilterTypeHash
		{
			get
			{
				if (!this.HasOptionsNodes)
				{
					return this.FilterType.GetHashCode();
				}
				StringBuilder stringBuilder = new StringBuilder(20);
				foreach (SyncCollection.Options options in this.optionsList)
				{
					stringBuilder.AppendFormat("{0}:{1},", options.ParsedClassNode ? options.Class : string.Empty, (int)options.FilterType);
				}
				return stringBuilder.ToString().GetHashCode();
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00029ABC File Offset: 0x00027CBC
		internal DefaultFolderType FolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00029AC4 File Offset: 0x00027CC4
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x00029ACC File Offset: 0x00027CCC
		internal Folder MailboxFolder
		{
			get
			{
				return this.mailboxFolder;
			}
			set
			{
				this.mailboxFolder = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00029AD5 File Offset: 0x00027CD5
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00029ADD File Offset: 0x00027CDD
		internal bool ConversationMode
		{
			get
			{
				return this.conversationMode;
			}
			set
			{
				this.conversationMode = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00029AE6 File Offset: 0x00027CE6
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00029AEE File Offset: 0x00027CEE
		internal bool ConversationModeInSyncState
		{
			get
			{
				return this.conversationModeInSyncState;
			}
			set
			{
				this.conversationModeInSyncState = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00029AF7 File Offset: 0x00027CF7
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00029AFF File Offset: 0x00027CFF
		internal bool NullSyncWorked
		{
			get
			{
				return this.nullSyncWorked;
			}
			set
			{
				this.nullSyncWorked = true;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00029B08 File Offset: 0x00027D08
		internal Dictionary<string, bool> SupportedTags
		{
			get
			{
				return this.supportedTags;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00029B10 File Offset: 0x00027D10
		internal MailboxSchemaOptionsParser MailboxSchemaOptions
		{
			get
			{
				return this.optionsList[this.currentOptions].MailboxSchemaOptions;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00029B28 File Offset: 0x00027D28
		internal bool RightsManagementSupport
		{
			get
			{
				if (this.isIrmSupportFlag == null)
				{
					if (this.optionsList != null && this.optionsList.Count > 0)
					{
						this.isIrmSupportFlag = new bool?(false);
						using (List<SyncCollection.Options>.Enumerator enumerator = this.optionsList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SyncCollection.Options options = enumerator.Current;
								if (options.MailboxSchemaOptions.RightsManagementSupport)
								{
									this.isIrmSupportFlag = new bool?(true);
									break;
								}
							}
							goto IL_7B;
						}
					}
					return false;
				}
				IL_7B:
				return this.isIrmSupportFlag.Value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00029BCC File Offset: 0x00027DCC
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00029BE4 File Offset: 0x00027DE4
		protected AirSyncSchemaState SchemaState
		{
			get
			{
				return this.optionsList[this.currentOptions].SchemaState;
			}
			set
			{
				this.optionsList[this.currentOptions].SchemaState = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00029BFD File Offset: 0x00027DFD
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00029C15 File Offset: 0x00027E15
		protected AirSyncDataObject AirSyncDataObject
		{
			get
			{
				return this.optionsList[this.currentOptions].AirSyncDataObject;
			}
			set
			{
				this.optionsList[this.currentOptions].AirSyncDataObject = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00029C2E File Offset: 0x00027E2E
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00029C46 File Offset: 0x00027E46
		protected IChangeTrackingFilter ChangeTrackFilter
		{
			get
			{
				return this.optionsList[this.currentOptions].ChangeTrackingFilter;
			}
			set
			{
				this.optionsList[this.currentOptions].ChangeTrackingFilter = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00029C5F File Offset: 0x00027E5F
		protected XmlNode OptionsNode
		{
			get
			{
				return this.optionsList[this.currentOptions].OptionsNode;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00029C77 File Offset: 0x00027E77
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00029C7F File Offset: 0x00027E7F
		protected bool HasMaxItemsNode { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00029C88 File Offset: 0x00027E88
		protected ItemIdMapping ItemIdMapping
		{
			get
			{
				if (this.itemIdMapping != null)
				{
					return this.itemIdMapping;
				}
				if (this.SyncState != null)
				{
					this.itemIdMapping = (ItemIdMapping)this.SyncState[CustomStateDatumType.IdMapping];
					if (this.itemIdMapping != null)
					{
						return this.itemIdMapping;
					}
				}
				this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
				this.ResponseSyncKey = this.SyncKey;
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = ((this.SyncState == null) ? "NoSyncState" : "NoIdMapping")
				};
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00029D0B File Offset: 0x00027F0B
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00029D23 File Offset: 0x00027F23
		private AirSyncDataObject ReadFlagAirSyncDataObject
		{
			get
			{
				return this.optionsList[this.currentOptions].ReadFlagAirSyncDataObject;
			}
			set
			{
				this.optionsList[this.currentOptions].ReadFlagAirSyncDataObject = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00029D3C File Offset: 0x00027F3C
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00029D54 File Offset: 0x00027F54
		private XsoDataObject MailboxDataObject
		{
			get
			{
				return this.optionsList[this.currentOptions].MailboxDataObject;
			}
			set
			{
				this.optionsList[this.currentOptions].MailboxDataObject = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00029D6D File Offset: 0x00027F6D
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00029D85 File Offset: 0x00027F85
		private AirSyncDataObject TruncationSizeZeroAirSyncDataObject
		{
			get
			{
				return this.optionsList[this.currentOptions].TruncationSizeZeroAirSyncDataObject;
			}
			set
			{
				this.optionsList[this.currentOptions].TruncationSizeZeroAirSyncDataObject = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00029DA0 File Offset: 0x00027FA0
		private bool HasMmsAnnotation
		{
			get
			{
				return this.optionsList != null && this.currentOptions < this.optionsList.Count && this.optionsList[this.currentOptions].Class == "SMS" && this.RequestAnnotations.ContainsAnnotation(Constants.SyncMms, this.collectionId, "SMS");
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00029E09 File Offset: 0x00028009
		private bool HasSmsExtension
		{
			get
			{
				return this.Context.Request.SupportsExtension(OutlookExtension.Sms);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00029E20 File Offset: 0x00028020
		public static SyncCollection CreateSyncCollection(MailboxSession mailboxSession, int protocolVersion, string collectionId)
		{
			switch (AirSyncUtility.GetCollectionType(collectionId))
			{
			case SyncCollection.CollectionTypes.Mailbox:
				if (protocolVersion >= 160)
				{
					using (CustomSyncState customSyncState = Command.CurrentCommand.SyncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]))
					{
						if (customSyncState != null)
						{
							FolderIdMapping folderIdMapping = (FolderIdMapping)customSyncState[CustomStateDatumType.IdMapping];
							if (folderIdMapping == null)
							{
								AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, Command.CurrentCommand, "[Id: {0}] FolderIdMappingMissing", collectionId);
							}
							else if (!folderIdMapping.Contains(collectionId))
							{
								AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, Command.CurrentCommand, "[Id: {0}] CollectionIdMissing", collectionId);
							}
							else
							{
								string airSyncFolderTypeClass = AirSyncUtility.GetAirSyncFolderTypeClass(folderIdMapping[collectionId]);
								if (airSyncFolderTypeClass == "Calendar")
								{
									return new EntitySyncCollection(mailboxSession, protocolVersion);
								}
							}
						}
						else
						{
							AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, Command.CurrentCommand, "[Id: {0}] FolderIdMappingSyncStateMissing", collectionId);
						}
					}
				}
				return new SyncCollection(mailboxSession, protocolVersion);
			case SyncCollection.CollectionTypes.RecipientInfoCache:
				return new RecipientInfoCacheSyncCollection(mailboxSession, protocolVersion);
			default:
				return new SyncCollection(mailboxSession, protocolVersion);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00029F2C File Offset: 0x0002812C
		public static SyncCollection ParseCollection(List<XmlNode> itemLevelProtocolErrorNodes, XmlNode collectionNode, int protocolVersion, MailboxSession mailboxSession)
		{
			if (collectionNode == null)
			{
				throw new ArgumentNullException("collectionNode");
			}
			XmlNode xmlNode = collectionNode["CollectionId"];
			SyncCollection syncCollection = SyncCollection.CreateSyncCollection(mailboxSession, protocolVersion, (xmlNode == null) ? null : xmlNode.InnerText);
			bool flag = false;
			try
			{
				syncCollection.CollectionNode = collectionNode;
				syncCollection.ParseCollection(itemLevelProtocolErrorNodes, collectionNode);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					syncCollection.Dispose();
				}
			}
			return syncCollection;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00029F98 File Offset: 0x00028198
		public virtual EventCondition CreateEventCondition()
		{
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[Id: {0}] SyncCollection.CreateEventCondition", this.InternalName);
			EventCondition eventCondition = new EventCondition();
			eventCondition.ObjectType = EventObjectType.Item;
			eventCondition.EventType = (EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.ObjectMoved);
			if (this.SyncState == null || this.SyncState.TryGetStoreObjectId() == null)
			{
				eventCondition.ContainerFolderIds.Add(this.NativeStoreObjectId);
			}
			else
			{
				eventCondition.ContainerFolderIds.Add(this.SyncState.TryGetStoreObjectId());
			}
			return eventCondition;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0002A00F File Offset: 0x0002820F
		protected IAirSyncContext Context
		{
			get
			{
				return Command.CurrentCommand.Context;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002A01B File Offset: 0x0002821B
		protected AnnotationsManager RequestAnnotations
		{
			get
			{
				return Command.CurrentCommand.RequestAnnotations;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002A034 File Offset: 0x00028234
		public virtual void OpenFolderSync()
		{
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[Id: {0}] SyncCollection.OpenFolderSync", this.InternalName);
			MailboxSyncProviderFactory mailboxSyncProviderFactory = this.SyncProviderFactory as MailboxSyncProviderFactory;
			if (mailboxSyncProviderFactory != null)
			{
				if (this.Context.User.Features.IsEnabled(EasFeature.EasPartialIcsSync))
				{
					mailboxSyncProviderFactory.IcsPropertyGroupFilter = SyncCollection.IcsPropertyGroupFilter;
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenFolderSync] Id: {0}, Using Ics property group filtering", this.InternalName);
				}
				else
				{
					mailboxSyncProviderFactory.IcsPropertyGroupFilter = null;
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenFolderSync] Id: {0}, NOT using Ics property group filtering", this.InternalName);
				}
			}
			if (this.ClassType == "Email" && this.FolderType != DefaultFolderType.DeletedItems && this.syncProviderFactory is FirstTimeSyncProviderFactory)
			{
				bool flag = this.SyncState.Contains(SyncStateProp.CurSnapShotWatermark);
				bool flag2 = this.SyncState.Contains(SyncStateProp.CurFTSMaxWatermark);
				if (!flag || flag2)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenFolderSync] Id: {0}, Use FTS provider.", this.InternalName);
					(this.SyncProviderFactory as FirstTimeSyncProviderFactory).UseNewProvider = true;
					this.FolderSync = this.SyncState.GetFolderSync(this.ConflictResolutionPolicy, (ISyncProvider tempProvider, FolderSyncState tempFolderSyncState, ConflictResolutionPolicy tempConflictResolutionPolicy, bool tempDeferStateModifications) => new FirstTimeFolderSync(tempProvider, tempFolderSyncState, tempConflictResolutionPolicy, tempDeferStateModifications));
					FirstTimeFolderSync firstTimeFolderSync = this.FolderSync as FirstTimeFolderSync;
					if (firstTimeFolderSync != null)
					{
						firstTimeFolderSync.CollectionId = this.InternalName;
					}
				}
				else
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenFolderSync]  Id: {0}, Use old provider.", this.InternalName);
					(this.SyncProviderFactory as FirstTimeSyncProviderFactory).UseNewProvider = false;
					this.FolderSync = this.SyncState.GetFolderSync(this.ConflictResolutionPolicy);
				}
			}
			else
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenFolderSync]  Id: {0}, Use old provider.", this.InternalName);
				this.FolderSync = this.SyncState.GetFolderSync(this.ConflictResolutionPolicy);
			}
			if (this.FolderSync == null)
			{
				this.Status = SyncBase.ErrorCodeStatus.InvalidCollection;
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "FolderSyncStateGone"
				};
			}
			this.FolderSync.FastReadFlagFilterCheck = true;
			this.FolderSync.MidnightRollover = this.MidnightRollover;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002A244 File Offset: 0x00028444
		public void CommitOrClearItemIdMapping()
		{
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[Id: {0}], SyncCollection.CommitOrClearItemIdMapping", this.InternalName);
			if ("S" == this.SyncTypeString)
			{
				this.ItemIdMapping.CommitChanges();
				return;
			}
			if ("R" == this.SyncTypeString)
			{
				this.ItemIdMapping.ClearChanges();
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002A2A2 File Offset: 0x000284A2
		public void Recover()
		{
			this.FolderSync.Recover(this.ClientCommands);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002A2B8 File Offset: 0x000284B8
		public void VerifySyncKey(bool expectCurrentSynckeyInRecoverySynckey, GlobalInfo globalInfo)
		{
			AirSyncDiagnostics.TraceInfo<string, bool>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.VerifySyncKey] Id: {0}, expectCurrentSynckeyInRecoverySynckey:{1}", this.InternalName, expectCurrentSynckeyInRecoverySynckey);
			if (this.SyncKey != 0U)
			{
				if (!this.SyncState.Contains(CustomStateDatumType.SyncKey))
				{
					AirSyncDiagnostics.TraceError<string, uint>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.VerifySyncKey] Id: {0},  No sync key found in sync state; client should have used SK0, instead sent: {1}", this.InternalName, this.SyncKey);
					this.SyncTypeString = "I";
					this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
					this.ResponseSyncKey = this.SyncKey;
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "InvalidSyncKey"
					};
				}
				uint data = ((UInt32Data)this.SyncState[CustomStateDatumType.SyncKey]).Data;
				uint num = 0U;
				if (this.SyncState.Contains(CustomStateDatumType.RecoverySyncKey))
				{
					num = ((UInt32Data)this.SyncState[CustomStateDatumType.RecoverySyncKey]).Data;
					AirSyncDiagnostics.TraceInfo<string, uint>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.VerifySyncKey] Id: {0}, getting recovery sync from sync state: {1}", this.InternalName, num);
				}
				if (this.SyncKey != data && (!this.allowRecovery || this.SyncKey != num))
				{
					this.SyncTypeString = "I";
					this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
					this.ResponseSyncKey = this.SyncKey;
					AirSyncDiagnostics.TraceError<string, uint, uint>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.VerifySyncKey] Id: {0}, found mismatched sync keys.  Sync state key: {1}, client key: {2}", this.InternalName, data, this.SyncKey);
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "MismatchSyncKey"
					};
				}
				if (expectCurrentSynckeyInRecoverySynckey && this.SyncKey == num)
				{
					this.SyncTypeString = "S";
					return;
				}
				if (this.SyncKey != data)
				{
					AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.VerifySyncKey] Id: {0},  Commencing recovery sync operations", this.InternalName);
					this.SyncTypeString = "R";
					AirSyncCounters.NumberOfRecoverySyncRequests.Increment();
					this.Recover();
					return;
				}
				this.SyncTypeString = "S";
				Exception ex = this.FolderSync.AcknowledgeServerOperations();
				if (ex != null)
				{
					Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ASO_InvalidOpsException");
					MailboxLogger mailboxLogger = Command.CurrentCommand.MailboxLogger;
					if (mailboxLogger != null && mailboxLogger.Enabled)
					{
						mailboxLogger.SetData(MailboxLogDataName.SyncCollection_VerifySyncKey_Exception, ex);
						return;
					}
				}
				else if (globalInfo != null && globalInfo.ABQMailState == ABQMailState.MailSent)
				{
					globalInfo.ABQMailState = ABQMailState.MailReceived;
					return;
				}
			}
			else
			{
				this.SyncTypeString = "F";
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002A4DC File Offset: 0x000286DC
		public virtual uint GetServerChanges(uint maxWindowSize, bool enumerateAllOperations)
		{
			AirSyncDiagnostics.TraceInfo<string, uint, bool>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, maxWindowSize: {1} enumerateAllOperations: {2}", this.InternalName, maxWindowSize, enumerateAllOperations);
			if (enumerateAllOperations)
			{
				this.WindowSize = -1;
			}
			int num = (int)Math.Min((long)this.WindowSize, (long)((ulong)maxWindowSize));
			if (num == 0)
			{
				this.MoreAvailable = true;
				this.ServerChanges = new SyncOperations(this.FolderSync, new Dictionary<ISyncItemId, ServerManifestEntry>(), true);
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, just put moreAvailable", this.InternalName);
			}
			else
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, Calling FolderSync.EnumerateServerOperations()", this.InternalName);
				try
				{
					this.ServerChanges = this.FolderSync.EnumerateServerOperations(num);
					this.MoreAvailable = this.ServerChanges.MoreAvailable;
					this.Context.ProtocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.GetChangesIterations, this.FolderSync.EnumerateServerOperationsIterations);
					this.Context.ProtocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.GetChangesTime, (int)this.FolderSync.EnumerateServerOperationsElapsed.TotalMilliseconds);
				}
				catch (CorruptSyncStateException innerException)
				{
					this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
					this.ResponseSyncKey = this.SyncKey;
					throw new AirSyncPermanentException(new LocalizedString("The supplied SyncKey is not valid"), innerException, false)
					{
						ErrorStringForProtocolLogger = "CorruptSyncStateInSyncCollection"
					};
				}
			}
			AirSyncDiagnostics.TraceDebug<string, int>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, FolderSync.EnumerateServerOperations returned {1}", this.InternalName, this.ServerChanges.Count);
			bool flag = true;
			foreach (SyncCollection.Options options in this.optionsList)
			{
				if (!(options.SchemaState is IClassFilter))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, shouldFilterResult == true", this.InternalName);
				for (int i = this.ServerChanges.Count - 1; i >= 0; i--)
				{
					SyncOperation syncOperation = this.ServerChanges[i];
					if (syncOperation.ChangeType != ChangeType.Delete)
					{
						if (syncOperation.ChangeType == ChangeType.SoftDelete)
						{
							if (this.isSendingABQMail)
							{
								syncOperation.Reject();
								this.ServerChanges.RemoveAt(i);
							}
						}
						else
						{
							bool flag2 = false;
							SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, syncOperation.MessageClass);
							foreach (SyncCollection.Options options2 in this.optionsList)
							{
								QueryFilter supportedClassFilter = ((IClassFilter)options2.SchemaState).SupportedClassFilter;
								if (EvaluatableFilter.Evaluate(supportedClassFilter, propertyBag))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.ConversionTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, Unable to find SchemaConverter for MessageClass {1}, change rejected", this.InternalName, syncOperation.MessageClass);
								syncOperation.Reject();
								this.ServerChanges.RemoveAt(i);
							}
						}
					}
				}
			}
			AirSyncDiagnostics.TraceDebug<string, int>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetServerChanges] Id: {0}, returned {1} changes", this.InternalName, this.ServerChanges.Count);
			return (uint)this.ServerChanges.Count;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002A7F8 File Offset: 0x000289F8
		public bool ParseSynckeyAndDetermineRecovery()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.ParseSynckeyAndDetermineRecovery");
			bool result = true;
			string text = this.SyncKeyString;
			int num = text.LastIndexOf('}');
			if (num > 0 && num < text.Length - 1)
			{
				text = text.Substring(num + 1);
				result = false;
			}
			uint num2;
			if (!uint.TryParse(text, out num2))
			{
				this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
				this.ResponseSyncKey = this.SyncKey;
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "SyncFormatError"
				};
			}
			this.SyncKey = num2;
			return result;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0002A87E File Offset: 0x00028A7E
		public void SetDeviceSettings(SyncBase command)
		{
			this.devicePhoneNumberForSms = command.DevicePhoneNumberForSms;
			this.deviceEnableOutboundSMS = command.DeviceEnableOutboundSMS;
			this.deviceSettingsHash = command.DeviceSettingsHash;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0002A8A4 File Offset: 0x00028AA4
		public virtual void CreateSyncProvider()
		{
			this.SyncProviderFactory = new FirstTimeSyncProviderFactory(this.storeSession);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0002A8B8 File Offset: 0x00028AB8
		public virtual void ParseFilterType(XmlNode filterTypeNode)
		{
			AirSyncDiagnostics.TraceInfo<string, XmlNode>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ParseFilterType] Id: {0}, filterTypeNode:{1}", this.InternalName, filterTypeNode);
			if (this.HasFilterNode)
			{
				AirSyncV25FilterTypes airSyncV25FilterTypes = this.FilterType;
			}
			else
			{
				this.FilterType = SyncCollection.ParseFilterTypeString(filterTypeNode.InnerText);
				this.HasFilterNode = true;
			}
			if (!SyncCollection.ClassSupportsFilterType(this.FilterType, this.ClassType))
			{
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "UnsupportedFilterForItemClass"
				};
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002A92C File Offset: 0x00028B2C
		public void InitializeSchemaConverter(IAirSyncVersionFactory versionFactory, GlobalInfo globalInfo)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.InitializeSchemaConverter");
			this.currentOptions = 0;
			while (this.currentOptions < this.optionsList.Count)
			{
				string @class = this.optionsList[this.currentOptions].Class;
				string key;
				if ((key = @class) == null)
				{
					goto IL_184;
				}
				if (<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x60007b7-1 == null)
				{
					<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x60007b7-1 = new Dictionary<string, int>(7)
					{
						{
							"Calendar",
							0
						},
						{
							"Email",
							1
						},
						{
							"Contacts",
							2
						},
						{
							"Tasks",
							3
						},
						{
							"Notes",
							4
						},
						{
							"RecipientInfoCache",
							5
						},
						{
							"SMS",
							6
						}
					};
				}
				int num;
				if (!<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x60007b7-1.TryGetValue(key, out num))
				{
					goto IL_184;
				}
				switch (num)
				{
				case 0:
					this.SchemaState = versionFactory.CreateCalendarSchema();
					break;
				case 1:
					this.SchemaState = versionFactory.CreateEmailSchema(this.ItemIdMapping);
					break;
				case 2:
					this.SchemaState = versionFactory.CreateContactsSchema();
					break;
				case 3:
					this.SchemaState = versionFactory.CreateTasksSchema();
					break;
				case 4:
					this.SchemaState = versionFactory.CreateNotesSchema();
					break;
				case 5:
					this.SchemaState = versionFactory.CreateRecipientInfoCacheSchema();
					break;
				case 6:
					if (this.HasSmsExtension || this.RequestAnnotations.ContainsAnnotation(Constants.SyncMms, this.collectionId, @class))
					{
						this.SchemaState = versionFactory.CreateConsumerSmsAndMmsSchema();
					}
					else
					{
						this.SchemaState = versionFactory.CreateSmsSchema();
					}
					this.CreateSmsSearchFolderIfNeeded(globalInfo);
					break;
				default:
					goto IL_184;
				}
				IL_18B:
				if (this.SchemaState == null)
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
					{
						ErrorStringForProtocolLogger = "InvalidClassType"
					};
				}
				this.currentOptions++;
				continue;
				IL_184:
				this.SchemaState = null;
				goto IL_18B;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002AB08 File Offset: 0x00028D08
		public void SetSyncProviderOptions(bool trackAssociatedMessageChanges)
		{
			AirSyncDiagnostics.TraceInfo<bool>(ExTraceGlobals.RequestsTracer, this, "SyncCollection.SetSyncProviderOptions. TrackAssociatedMessage:{0}", trackAssociatedMessageChanges);
			if (this.ClassType == "Email")
			{
				MailboxSyncProviderFactory mailboxSyncProviderFactory = this.SyncProviderFactory as MailboxSyncProviderFactory;
				if (mailboxSyncProviderFactory != null)
				{
					mailboxSyncProviderFactory.GenerateReadFlagChanges();
					if (trackAssociatedMessageChanges)
					{
						mailboxSyncProviderFactory.GenerateAssociatedMessageChanges();
					}
				}
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002AB56 File Offset: 0x00028D56
		public void SetEmptyServerChanges()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.SetEmptyServerChanges");
			this.ServerChanges = new SyncOperations(this.FolderSync, new Dictionary<ISyncItemId, ServerManifestEntry>(), false);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002AB80 File Offset: 0x00028D80
		public virtual bool AllowGetChangesOnSyncKeyZero()
		{
			if (this.ClassType == "Calendar")
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.AllowGetChangesOnSyncKeyZero] Id: {0}, true", this.InternalName);
				return true;
			}
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.AllowGetChangesOnSyncKeyZero] Id: {0}, false", this.InternalName);
			return false;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		public virtual void SetFolderSyncOptions(IAirSyncVersionFactory versionFactory, bool isQuarantineMailAvailable, GlobalInfo globalInfo)
		{
			AirSyncDiagnostics.TraceInfo<string, IAirSyncVersionFactory, bool>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.SetFolderSyncOptions] Id: {0}, versionFactory {1} isQuarantineMailAvailable:{2}", this.InternalName, versionFactory, isQuarantineMailAvailable);
			if ("Calendar" == this.ClassType && this.SyncKey == 0U)
			{
				MailboxSyncProviderFactory mailboxSyncProviderFactory = this.SyncProviderFactory as MailboxSyncProviderFactory;
				if (mailboxSyncProviderFactory == null)
				{
					goto IL_8E;
				}
				StoreObjectId folderId = mailboxSyncProviderFactory.FolderId;
				using (Folder folder = Folder.Bind(this.storeSession, folderId))
				{
					DateTimeCustomSyncFilter dateTimeCustomSyncFilter = new DateTimeCustomSyncFilter(this.SyncState);
					dateTimeCustomSyncFilter.Prepopulate(folder);
					this.FolderSync.SetSyncFilters(dateTimeCustomSyncFilter, new ISyncFilter[0]);
					goto IL_8E;
				}
			}
			this.SetFilterType(isQuarantineMailAvailable, globalInfo);
			IL_8E:
			if (this.SyncKey == 0U)
			{
				this.SyncState[CustomStateDatumType.SupportedTags] = new GenericDictionaryData<StringData, string, BooleanData, bool>(this.supportedTags);
				try
				{
					IAirSyncMissingPropertyStrategy missingPropertyStrategy = versionFactory.CreateMissingPropertyStrategy(this.supportedTags);
					this.currentOptions = 0;
					while (this.currentOptions < this.optionsList.Count)
					{
						this.AirSyncDataObject = this.SchemaState.GetAirSyncDataObject(SyncCollection.emptyPropertyCollection, missingPropertyStrategy);
						this.currentOptions++;
					}
					goto IL_140;
				}
				catch (ConversionException innerException)
				{
					this.Status = SyncBase.ErrorCodeStatus.ProtocolError;
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, innerException, false)
					{
						ErrorStringForProtocolLogger = "SchemaCreationFail"
					};
				}
			}
			this.supportedTags = this.SyncState.GetData<GenericDictionaryData<StringData, string, BooleanData, bool>, Dictionary<string, bool>>(CustomStateDatumType.SupportedTags, null);
			IL_140:
			this.SyncState[CustomStateDatumType.FilterType] = new Int32Data((int)this.FilterType);
			this.SyncState[CustomStateDatumType.ConversationMode] = new BooleanData(this.ConversationMode);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002AD70 File Offset: 0x00028F70
		public virtual void ConvertServerToClientObject(ISyncItem syncItem, XmlNode airSyncParentNode, SyncOperation changeObject, GlobalInfo globalInfo)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.ConvertServerToClientObject");
			Item item = (Item)syncItem.NativeItem;
			if (globalInfo != null && globalInfo.ABQMailState == ABQMailState.MailPosted && object.Equals(item.Id.ObjectId, globalInfo.ABQMailId))
			{
				globalInfo.ABQMailState = ABQMailState.MailSent;
			}
			if (changeObject != null && ChangeType.ReadFlagChange == changeObject.ChangeType)
			{
				this.ReadFlagAirSyncDataObject.Unbind();
				ReadFlagDataObject readFlagDataObject = new ReadFlagDataObject(this.MailboxDataObject.Children, changeObject);
				readFlagDataObject.SetChangedProperties(SyncCollection.ReadFlagChangedOnly);
				this.ReadFlagAirSyncDataObject.Bind(airSyncParentNode);
				this.ReadFlagAirSyncDataObject.CopyFrom(readFlagDataObject);
				this.ReadFlagAirSyncDataObject.Unbind();
			}
			else
			{
				this.MailboxDataObject.Unbind();
				if (this.MailboxSchemaOptions.RightsManagementSupport)
				{
					Command.CurrentCommand.DecodeIrmMessage(item, false);
				}
				this.MailboxDataObject.Bind(item);
				if (!this.MailboxDataObject.CanConvertItemClassUsingCurrentSchema(item.ClassName))
				{
					throw new ConversionException(string.Concat(new object[]
					{
						"Cannot convert item : ",
						item.Id,
						" of class \"",
						item.ClassName,
						"\" using current schema."
					}));
				}
				this.AirSyncDataObject.Unbind();
				this.AirSyncDataObject.Bind(airSyncParentNode);
				AirSyncDiagnostics.FaultInjectionTracer.TraceTest(2170957117U);
				this.AirSyncDataObject.CopyFrom(this.MailboxDataObject);
				this.AirSyncDataObject.Unbind();
				this.MailboxDataObject.Unbind();
			}
			this.ApplyChangeTrackFilter(changeObject, airSyncParentNode);
			if (syncItem.NativeItem is CalendarItem)
			{
				SyncCollection.PostProcessExceptions(airSyncParentNode);
				SyncCollection.PostProcessAllDayEventNodes(airSyncParentNode);
			}
			this.SetHasChanges(changeObject);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0002AF34 File Offset: 0x00029134
		public virtual void ParseSupportedTags(XmlNode parentNode)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.ParseSupportedTags");
			if (this.SyncKey != 0U)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
				{
					ErrorStringForProtocolLogger = "SupportedSentPastSK0"
				};
			}
			this.supportedTags = new Dictionary<string, bool>();
			using (IEnumerator enumerator = parentNode.ChildNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					XmlNode node = (XmlNode)enumerator.Current;
					if (this.ClassType == null)
					{
						this.AddClassTypeValidation(node, delegate(object param0)
						{
							this.ValidateSupportTag(node);
						});
					}
					else
					{
						this.ValidateSupportTag(node);
					}
					if (this.supportedTags.ContainsKey(node.LocalName))
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
						{
							ErrorStringForProtocolLogger = "DupeSupportedTags-" + node.LocalName
						};
					}
					this.supportedTags.Add(node.LocalName, true);
				}
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002B068 File Offset: 0x00029268
		public virtual void SetSchemaConverterOptions(IDictionary schemaConverterOptions, IAirSyncVersionFactory versionFactory)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.SetSchemaConverterOptions");
			AirSyncXsoSchemaState airSyncXsoSchemaState = (AirSyncXsoSchemaState)this.SchemaState;
			IAirSyncMissingPropertyStrategy missingPropertyStrategy;
			if ("Email" == this.ClassType)
			{
				missingPropertyStrategy = versionFactory.CreateReadFlagMissingPropertyStrategy();
				this.ReadFlagAirSyncDataObject = airSyncXsoSchemaState.GetAirSyncDataObject(schemaConverterOptions, missingPropertyStrategy);
			}
			this.ChangeTrackFilter = ChangeTrackingFilterFactory.CreateFilter(this.ClassType, this.protocolVersion);
			missingPropertyStrategy = versionFactory.CreateMissingPropertyStrategy(this.supportedTags);
			this.AirSyncDataObject = airSyncXsoSchemaState.GetAirSyncDataObject(schemaConverterOptions, missingPropertyStrategy);
			this.MailboxDataObject = airSyncXsoSchemaState.GetXsoDataObject();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002B0F8 File Offset: 0x000292F8
		public ISyncItem BindToSyncItem(SyncOperation changeObject)
		{
			return changeObject.GetItem((this.MailboxDataObject == null) ? null : this.MailboxDataObject.GetPrefetchProperties());
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002B124 File Offset: 0x00029324
		public virtual ISyncItem BindToSyncItem(ISyncItemId syncItemId, bool prefetchProperties)
		{
			ISyncItem syncItem = this.FolderSync.GetItem(syncItemId, (this.MailboxDataObject == null) ? null : this.MailboxDataObject.GetPrefetchProperties());
			if (syncItem != null && syncItem.NativeItem != null)
			{
				CalendarItemOccurrence calendarItemOccurrence = syncItem.NativeItem as CalendarItemOccurrence;
				if (calendarItemOccurrence != null)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.BindToSyncItem] Id: {0}, Got CalendarItemOccurrence from GetItem(). Id:{1}, Subject:{2}, OriginalStartTime:{3}, Getting the master item instead.", new object[]
					{
						this.InternalName,
						syncItemId.NativeId,
						calendarItemOccurrence.Subject,
						calendarItemOccurrence.OriginalStartTime
					});
					MailboxSyncItem mailboxSyncItem = MailboxSyncItem.Bind(calendarItemOccurrence.GetMaster());
					syncItem.Dispose();
					syncItem = mailboxSyncItem;
				}
			}
			return syncItem;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002B1C4 File Offset: 0x000293C4
		public virtual OperationResult DeleteSyncItem(SyncCommandItem commandItem, bool deletesAsMoves)
		{
			return this.DeleteSyncItem(commandItem.ServerId, deletesAsMoves);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002B1D4 File Offset: 0x000293D4
		public virtual OperationResult DeleteSyncItem(ISyncItemId syncItemId, bool deletesAsMoves)
		{
			this.CheckFullAccess();
			DeleteItemFlags deleteFlags = DeleteItemFlags.SoftDelete;
			if (deletesAsMoves)
			{
				deleteFlags = DeleteItemFlags.MoveToDeletedItems;
			}
			StoreObjectId[] array = new StoreObjectId[]
			{
				(StoreObjectId)syncItemId.NativeId
			};
			this.TrackCalendarChanges(array[0]);
			OperationResult operationResult = this.storeSession.Delete(deleteFlags, array).OperationResult;
			if (OperationResult.Failed != operationResult)
			{
				this.ItemIdMapping.Delete(new ISyncItemId[]
				{
					syncItemId
				});
			}
			return operationResult;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002B240 File Offset: 0x00029440
		public virtual ISyncItem CreateSyncItem(SyncCommandItem item)
		{
			this.CheckFullAccess();
			ItemIdMapping itemIdMapping = this.ItemIdMapping;
			MailboxSyncProviderFactory mailboxSyncProviderFactory = this.SyncProviderFactory as MailboxSyncProviderFactory;
			if (mailboxSyncProviderFactory == null)
			{
				throw new NotImplementedException(string.Format("CreateSyncItem is not defined for {0}", this.SyncProviderFactory.GetType().FullName));
			}
			StoreObjectId folderId = mailboxSyncProviderFactory.FolderId;
			string key;
			if ((key = item.ClassType) != null)
			{
				if (<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x60007c3-1 == null)
				{
					<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x60007c3-1 = new Dictionary<string, int>(6)
					{
						{
							"Calendar",
							0
						},
						{
							"Email",
							1
						},
						{
							"Contacts",
							2
						},
						{
							"Tasks",
							3
						},
						{
							"Notes",
							4
						},
						{
							"SMS",
							5
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{AB27278A-C276-498D-AAD6-319A054A2659}.$$method0x60007c3-1.TryGetValue(key, out num))
				{
					Item item2;
					switch (num)
					{
					case 0:
						item2 = CalendarItem.Create(this.storeSession, folderId);
						break;
					case 1:
						if (this.Context.Request.Version < 160)
						{
							throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
							{
								ErrorStringForProtocolLogger = "EmailAddsNotSupportedForLessThanV16"
							};
						}
						item2 = MessageItem.Create(this.storeSession, folderId);
						item2.ClassName = "IPM.Note";
						((MessageItem)item2).IsDraft = true;
						break;
					case 2:
						item2 = Contact.Create(this.storeSession, folderId);
						break;
					case 3:
					{
						Task task = Task.Create(this.storeSession, folderId);
						task.SuppressRecurrenceAdjustment = true;
						item2 = task;
						break;
					}
					case 4:
						item2 = Item.Create(this.storeSession, "IPM.StickyNote", folderId);
						break;
					case 5:
						item2 = MessageItem.Create(this.storeSession, folderId);
						if (item.IsMms)
						{
							item2.ClassName = "IPM.Note.Mobile.MMS";
						}
						else
						{
							item2.ClassName = "IPM.Note.Mobile.SMS";
						}
						((MessageItem)item2).IsDraft = false;
						break;
					default:
						goto IL_1D5;
					}
					return this.CreateSyncItem(item2);
				}
			}
			IL_1D5:
			throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
			{
				ErrorStringForProtocolLogger = "BadClassType(" + this.classType + ")onSync"
			};
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002B458 File Offset: 0x00029658
		public void DeleteId(ISyncItemId id)
		{
			this.ItemIdMapping.Delete(new ISyncItemId[]
			{
				id
			});
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002B47C File Offset: 0x0002967C
		public string GetStringIdFromSyncItemId(ISyncItemId syncId, bool createIfDoesntExist)
		{
			ItemIdMapping itemIdMapping = this.ItemIdMapping;
			if (createIfDoesntExist && !itemIdMapping.Contains(syncId))
			{
				return itemIdMapping.Add(syncId);
			}
			return itemIdMapping[syncId];
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002B4AD File Offset: 0x000296AD
		public ISyncItemId TryGetSyncItemIdFromStringId(string strId)
		{
			return this.ItemIdMapping[strId];
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002B4BC File Offset: 0x000296BC
		public virtual bool HasSchemaPropertyChanged(ISyncItem syncItem, int?[] oldChangeTrackingInformation, XmlDocument xmlResponse, MailboxLogger mailboxLogger)
		{
			bool flag = false;
			XmlNode xmlItemRoot = xmlResponse.CreateElement("ApplicationData", "AirSync:");
			Item item = (Item)syncItem.NativeItem;
			try
			{
				this.MailboxDataObject.Unbind();
				this.MailboxDataObject.Bind(item);
				this.AirSyncDataObject.Unbind();
				this.AirSyncDataObject.Bind(xmlItemRoot);
				this.AirSyncDataObject.CopyFrom(this.MailboxDataObject);
				this.MailboxDataObject.Unbind();
				this.AirSyncDataObject.Unbind();
			}
			catch (Exception ex)
			{
				if (!SyncCommand.IsItemSyncTolerableException(ex))
				{
					throw;
				}
				if (mailboxLogger != null)
				{
					mailboxLogger.SetData(MailboxLogDataName.MailboxSyncCommand_HasSchemaPropertyChanged_Exception, ex);
				}
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<string, AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.HasSchemaPropertyChanged] Id: {0}, Sync-tolerable Item conversion Exception was thrown. HasSchemaPropertyChanged() {1}", this.InternalName, arg);
				flag = true;
			}
			if (!flag)
			{
				int?[] array = this.ChangeTrackFilter.UpdateChangeTrackingInformation(xmlItemRoot, oldChangeTrackingInformation);
				AirSyncDiagnostics.TraceDebug<string, int?[], int?[]>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.HasSchemaPropertyChanged] Id: {0}, oldCTI {1} newCTI {2}", this.InternalName, oldChangeTrackingInformation, array);
				flag = !ChangeTrackingFilter.IsEqual(array, oldChangeTrackingInformation);
			}
			return flag;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002B5CC File Offset: 0x000297CC
		public void ConvertClientToServerObjectAndSendIfNeeded(SyncCommandItem syncCommandItem, bool sendEnabled)
		{
			Item item = (Item)syncCommandItem.Item.NativeItem;
			item.OpenAsReadWrite();
			this.MailboxDataObject.Unbind();
			this.MailboxDataObject.Bind(item);
			if (syncCommandItem.ClassType == "Email" || syncCommandItem.ClassType == "SMS" || syncCommandItem.ClassType == "MMS")
			{
				this.ReadFlagAirSyncDataObject.Unbind();
				this.ReadFlagAirSyncDataObject.Bind(syncCommandItem.XmlNode);
				this.MailboxDataObject.CopyFrom(this.ReadFlagAirSyncDataObject);
				this.ReadFlagAirSyncDataObject.Unbind();
			}
			else
			{
				this.AirSyncDataObject.Unbind();
				this.AirSyncDataObject.Bind(syncCommandItem.XmlNode);
				this.MailboxDataObject.CopyFrom(this.AirSyncDataObject);
				this.AirSyncDataObject.Unbind();
			}
			this.MailboxDataObject.Unbind();
			if (sendEnabled)
			{
				((MessageItem)item).Send();
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002B6C8 File Offset: 0x000298C8
		public virtual ISyncItemId ConvertClientToServerObjectAndSave(SyncCommandItem syncCommandItem, ref uint maxWindowSize, ref bool mergeToClient)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.ConvertClientToServerObjectAndSave");
			this.CheckFullAccess();
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				throw new InvalidOperationException("ConvertClientToServerObjectAndSave(): storeSession is not a MailboxSession!");
			}
			if (syncCommandItem.ClassType == "Notes")
			{
				string text = null;
				foreach (object obj in syncCommandItem.XmlNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name == "MessageClass" && xmlNode.NamespaceURI == "Notes:")
					{
						text = xmlNode.InnerText;
						break;
					}
				}
				if (string.IsNullOrEmpty(text) || !this.MailboxDataObject.CanConvertItemClassUsingCurrentSchema(text))
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
					{
						ErrorStringForProtocolLogger = "MessageClassOnNotesSync"
					};
				}
			}
			if (syncCommandItem.ClassType == "Calendar" && syncCommandItem.ChangeType == ChangeType.Change)
			{
				for (int i = syncCommandItem.XmlNode.ChildNodes.Count - 1; i >= 0; i--)
				{
					XmlNode xmlNode2 = syncCommandItem.XmlNode.ChildNodes[i];
					if (xmlNode2.Name == "OrganizerName" || xmlNode2.Name == "OrganizerEmail")
					{
						AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObject] Id: {0}, Delete node {1} for calendar change", this.InternalName, xmlNode2.Name);
						syncCommandItem.XmlNode.RemoveChild(xmlNode2);
					}
				}
			}
			if ((syncCommandItem.ClassType == "SMS" || syncCommandItem.ClassType == "MMS") && syncCommandItem.ChangeType == ChangeType.Change)
			{
				foreach (object obj2 in syncCommandItem.XmlNode.ChildNodes)
				{
					XmlNode xmlNode3 = (XmlNode)obj2;
					if (xmlNode3.Name == "Importance" || xmlNode3.Name == "To" || xmlNode3.Name == "From" || xmlNode3.Name == "DateReceived" || xmlNode3.Name == "Body")
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
						{
							ErrorStringForProtocolLogger = string.Format("UnSupportedNodeInXml:{0}", xmlNode3.Name)
						};
					}
				}
				string commandAnnotationGroup = AnnotationsManager.GetCommandAnnotationGroup(this.collectionId, syncCommandItem.SyncId);
				if (this.RequestAnnotations.ContainsAnnotation("SimSlotNumber", commandAnnotationGroup) || this.RequestAnnotations.ContainsAnnotation("SentItem", commandAnnotationGroup) || this.RequestAnnotations.ContainsAnnotation("SentTime", commandAnnotationGroup) || this.RequestAnnotations.ContainsAnnotation("Subject", commandAnnotationGroup))
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, false)
					{
						ErrorStringForProtocolLogger = "UnAnnotationNodeInXml"
					};
				}
			}
			ItemIdMapping itemIdMapping = this.ItemIdMapping;
			Item item = (Item)syncCommandItem.Item.NativeItem;
			this.ConvertClientToServerObjectAndSendIfNeeded(syncCommandItem, false);
			if (syncCommandItem.ChangeType == ChangeType.Change && syncCommandItem.ClassType == "Tasks")
			{
				Task task = (Task)item;
				if (!task.SuppressCreateOneOff)
				{
					mergeToClient = true;
				}
			}
			if (syncCommandItem.ChangeType == ChangeType.Add && syncCommandItem.ClassType == "Calendar")
			{
				CalendarItemBase calendarItemBase = (CalendarItemBase)item;
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, DefaultFolderType.Calendar, null))
				{
					VersionedId versionedId = null;
					try
					{
						versionedId = calendarFolder.GetCalendarItemId(calendarItemBase.GlobalObjectId.Bytes);
					}
					catch (NullReferenceException)
					{
						AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, CalendarItem has no GlobalObjectId", this.InternalName);
					}
					catch (ArgumentNullException)
					{
						AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, CalendarItem has no GlobalObjectId", this.InternalName);
					}
					bool flag = versionedId != null && (calendarItemBase.Id == null || calendarItemBase.Id.CompareTo(versionedId) != 0);
					if (flag)
					{
						if (!this.GetChanges)
						{
							AirSyncDiagnostics.TraceError<string, GlobalObjectId>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, Client attempted to add a duplicate calendar item.  Reported conflict to the device: GlobObjId={1}", this.InternalName, calendarItemBase.GlobalObjectId);
							syncCommandItem.Status = "7";
						}
						else
						{
							AirSyncDiagnostics.TraceError<string, GlobalObjectId>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, Client attempted to add a duplicate calendar item.  Reported success, but item was not added to the server: GlobObjId={1}", this.InternalName, calendarItemBase.GlobalObjectId);
							syncCommandItem.Status = "1";
							syncCommandItem.SyncId = string.Concat(new object[]
							{
								"00:",
								this.InternalName,
								":",
								this.dupeId++
							});
							this.DupeList.Add(syncCommandItem);
							if (this.WindowSize > 0 && maxWindowSize > 0U)
							{
								this.WindowSize--;
								maxWindowSize -= 1U;
							}
							else
							{
								AirSyncDiagnostics.TraceError<string, int>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, Too many duplicated calendar items.  Commands returned will exceed windowsize: {1}", this.InternalName, this.WindowSize);
							}
							if (this.WindowSize == 0 || maxWindowSize == 0U)
							{
								AirSyncDiagnostics.TraceError<string, int, uint>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, we filled the windowSize now. windowsize: {1}, maxWindowSize:{2}", this.InternalName, this.WindowSize, maxWindowSize);
								this.DupesFilledWindowSize = true;
							}
						}
						if (calendarItemBase.Id != null)
						{
							DeleteItemFlags deleteFlags = DeleteItemFlags.HardDelete;
							StoreObjectId[] ids = new StoreObjectId[]
							{
								calendarItemBase.Id.ObjectId
							};
							OperationResult operationResult = this.storeSession.Delete(deleteFlags, ids).OperationResult;
							AirSyncDiagnostics.TraceError<string, OperationResult>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.ConvertClientToServerObjectAndSave] Id: {0}, Attempted to delete partially saved duplicate item with result: {1}", this.InternalName, operationResult);
						}
						return null;
					}
				}
			}
			string a = null;
			if (syncCommandItem.Item.NativeItem != null && syncCommandItem.Item.NativeItem is Contact)
			{
				Contact contact = syncCommandItem.Item.NativeItem as Contact;
				a = contact.DisplayName;
			}
			syncCommandItem.Item.Save();
			syncCommandItem.Item.Load();
			if (syncCommandItem.ClassType == "SMS")
			{
				object obj3 = ((Item)syncCommandItem.Item.NativeItem).TryGetProperty(ItemSchema.ConversationId);
				syncCommandItem.ConversationId = (obj3 as ConversationId);
				obj3 = ((Item)syncCommandItem.Item.NativeItem).TryGetProperty(ItemSchema.ConversationIndex);
				syncCommandItem.ConversationIndex = (obj3 as byte[]);
			}
			if (syncCommandItem.ClassType == "Email" && syncCommandItem.ChangeType != ChangeType.Delete)
			{
				object obj4 = ((Item)syncCommandItem.Item.NativeItem).TryGetProperty(MessageItemSchema.IsDraft);
				if (!(obj4 is PropertyError))
				{
					syncCommandItem.IsDraft = (bool)obj4;
				}
				else
				{
					AirSyncDiagnostics.TraceError<string, PropertyErrorCode>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection::ConvertClientToServerObjectAndSave] Id: {0}, Error retrieving IsDraft from Added/Changed email item. ErrorCode {1}", this.InternalName, (obj4 as PropertyError).PropertyErrorCode);
				}
			}
			if (syncCommandItem.Item.NativeItem != null && syncCommandItem.Item.NativeItem is Contact)
			{
				Contact contact2 = syncCommandItem.Item.NativeItem as Contact;
				if (a != contact2.DisplayName)
				{
					if (contact2.DisplayName != null)
					{
						contact2[ItemSchema.Subject] = ((contact2.DisplayName.Length < 256) ? contact2.DisplayName : contact2.DisplayName.Substring(0, 255));
					}
					else
					{
						contact2[ItemSchema.Subject] = null;
					}
					syncCommandItem.Item.Save();
					syncCommandItem.Item.Load();
				}
			}
			if (syncCommandItem.ClassType == "Email" && syncCommandItem.ChangeType == ChangeType.Change)
			{
				XmlElement xmlElement = syncCommandItem.XmlNode["Flag", "Email:"];
				if (xmlElement != null && xmlElement.HasChildNodes && xmlElement["ReminderSet", "Tasks:"] == null)
				{
					XmlElement xmlElement2 = xmlElement.OwnerDocument.CreateElement("ReminderSet", "Tasks:");
					xmlElement2.InnerText = "0";
					xmlElement.AppendChild(xmlElement2);
				}
			}
			syncCommandItem.ChangeTrackingInformation = this.ChangeTrackFilter.UpdateChangeTrackingInformation(syncCommandItem.XmlNode, syncCommandItem.ChangeTrackingInformation);
			StoreObjectId objectId = item.Id.ObjectId;
			ISyncItemId syncItemId = MailboxSyncItemId.CreateForNewItem(objectId);
			if (SyncBase.SyncCommandType.Add == syncCommandItem.CommandType)
			{
				itemIdMapping.Add(syncItemId);
			}
			syncCommandItem.SyncId = itemIdMapping[syncItemId];
			return syncItemId;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002BF98 File Offset: 0x0002A198
		public bool IsClassLegal(string classType)
		{
			if (this.ClassType == null)
			{
				throw new InvalidOperationException();
			}
			if (classType == this.ClassType)
			{
				return true;
			}
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				throw new InvalidOperationException("IsClassLegal(): storeSession is not a MailboxSession!");
			}
			return this.ClassType == "Email" && classType == "SMS" && (this.protocolVersion >= 141 || this.NativeStoreObjectId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)) || this.NativeStoreObjectId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.Outbox)) || this.NativeStoreObjectId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems)));
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002C04C File Offset: 0x0002A24C
		public void ParseSyncOptions()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.ParseSyncOptions");
			HashSet<string> hashSet = new HashSet<string>();
			this.currentOptions = 0;
			while (this.currentOptions < this.optionsList.Count)
			{
				SyncCollection.Options options = this.optionsList[this.currentOptions];
				if (options.Class == null)
				{
					options.Class = this.ClassType;
				}
				if (!this.IsClassLegal(options.Class))
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
					{
						ErrorStringForProtocolLogger = string.Format("InvalidClassType({0})InSync+CurrentClass({1})", options.Class, this.ClassType)
					};
				}
				this.ParseSyncOptionsNode();
				if (hashSet.Contains(options.Class))
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
					{
						ErrorStringForProtocolLogger = "DupeClassType(" + options.Class + ")InSync"
					};
				}
				hashSet.Add(options.Class);
				if (this.optionsList.Count > 1 && options.Class != "Email" && options.Class != "SMS")
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
					{
						ErrorStringForProtocolLogger = "DupeOptionsNodeInSync"
					};
				}
				if (!SyncCollection.ClassSupportsFilterType(options.FilterType, options.Class))
				{
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "FilterClassMismatch"
					};
				}
				this.currentOptions++;
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0002C1B0 File Offset: 0x0002A3B0
		public virtual void ParseSyncOptionsNode()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.ParseSyncOptionsNode");
			using (XmlNodeList childNodes = this.OptionsNode.ChildNodes)
			{
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string localName;
					if ((localName = xmlNode.LocalName) != null)
					{
						if (!(localName == "FilterType"))
						{
							if (!(localName == "Conflict"))
							{
								if (localName == "MaxItems")
								{
									this.Status = SyncBase.ErrorCodeStatus.ProtocolError;
									throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidXML, null, false)
									{
										ErrorStringForProtocolLogger = "BadNode(" + xmlNode.LocalName + ")InSyncOptions"
									};
								}
							}
							else
							{
								this.ParseConflictResolutionPolicy(xmlNode);
							}
						}
						else
						{
							this.ParseFilterType(xmlNode);
						}
					}
				}
			}
			this.MailboxSchemaOptions.Parse(this.OptionsNode);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002C2C8 File Offset: 0x0002A4C8
		public XmlNode ParseAndRemoveAnnotationInAppData(XmlNode appDataNode, string id)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(appDataNode.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("live", "WindowsLive:");
			XmlNode xmlNode = appDataNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, "live:{0}", new object[]
			{
				"Annotations"
			}), xmlNamespaceManager);
			if (xmlNode != null)
			{
				string commandAnnotationGroup = AnnotationsManager.GetCommandAnnotationGroup(this.collectionId, id);
				this.RequestAnnotations.ParseWLAnnotations(xmlNode, commandAnnotationGroup);
				appDataNode.RemoveChild(xmlNode);
			}
			return appDataNode;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002C344 File Offset: 0x0002A544
		public void ParseConsumerSmsAndMmsDataInApplicationData(SyncCommandItem item)
		{
			XmlNode xmlNode = item.XmlNode;
			string commandAnnotationGroup = AnnotationsManager.GetCommandAnnotationGroup(this.collectionId, item.ClientAddId);
			XmlNode bodyNode = null;
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode2.LocalName) != null)
				{
					if (!(localName == "MessageClass"))
					{
						if (localName == "Body")
						{
							bodyNode = xmlNode2;
						}
					}
					else if (string.Equals(xmlNode2.InnerText, "IPM.NOTE.MOBILE.MMS", StringComparison.OrdinalIgnoreCase))
					{
						item.IsMms = true;
					}
					else if (!string.Equals(xmlNode2.InnerText, "IPM.NOTE.MOBILE.SMS", StringComparison.OrdinalIgnoreCase))
					{
						this.Status = SyncBase.ErrorCodeStatus.ProtocolError;
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
						{
							ErrorStringForProtocolLogger = "InvalidMessageClassForSMSItem"
						};
					}
				}
			}
			this.ValidateBodyForConsumerSmsAndMmsClassType(item, bodyNode);
			AirSyncUtility.ReplaceAnnotationWithExtensionIfExists(xmlNode, "SentTime", commandAnnotationGroup, "WindowsLive:");
			AirSyncUtility.ReplaceAnnotationWithExtensionIfExists(xmlNode, "SentItem", commandAnnotationGroup, "WindowsLive:");
			AirSyncUtility.ReplaceAnnotationWithExtensionIfExists(xmlNode, "Subject", commandAnnotationGroup, "Email:");
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002C474 File Offset: 0x0002A674
		public void ValidateBodyForConsumerSmsAndMmsClassType(SyncCommandItem item, XmlNode bodyNode)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(bodyNode.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("AirSyncBase", "AirSyncBase:");
			XmlNode xmlNode = bodyNode.SelectSingleNode(string.Format("AirSyncBase:{0}", "Type"), xmlNamespaceManager);
			if (xmlNode == null)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.ConversionTracer, this, "[SyncCollection.ValidateBodyForConsumerSmsAndMmsClassType] No body type.");
				throw new ConversionException("No body type");
			}
			BodyType bodyType;
			if (Enum.TryParse<BodyType>(xmlNode.InnerText, out bodyType))
			{
				if (item.IsMms)
				{
					if (bodyType != BodyType.Html && bodyType != BodyType.PlainText && bodyType != BodyType.Mime)
					{
						AirSyncDiagnostics.TraceError<BodyType>(ExTraceGlobals.ConversionTracer, this, "[SyncCollection.ValidateBodyForConsumerSmsAndMmsClassType] Type: {0}, Not a valid body type for consumer MMS item.", bodyType);
						throw new ConversionException("Invalid body type for consumer MMS item");
					}
				}
				else if (bodyType != BodyType.Html && bodyType != BodyType.PlainText)
				{
					AirSyncDiagnostics.TraceError<BodyType>(ExTraceGlobals.ConversionTracer, this, "[SyncCollection.ValidateBodyForConsumerSmsAndMmsClassType] Type: {0}, Not a valid body type for consumer SMS item.", bodyType);
					throw new ConversionException("Invalid body type for consumer SMS item");
				}
				return;
			}
			AirSyncDiagnostics.TraceError<BodyType>(ExTraceGlobals.ConversionTracer, this, "[SyncCollection.ValidateBodyForConsumerSmsAndMmsClassType] Type: {0}, Not a valid body type.", bodyType);
			throw new ConversionException("Invalid body type");
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002C558 File Offset: 0x0002A758
		public void SetSchemaOptionsConvertServerToClient(string deviceType, IAirSyncVersionFactory versionFactory)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.SetSchemaOptionsConvertServerToClient");
			IDictionary schemaConverterOptions = this.MailboxSchemaOptions.BuildOptionsCollection(deviceType);
			this.SetSchemaConverterOptions(schemaConverterOptions, versionFactory);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002C58C File Offset: 0x0002A78C
		public virtual void OpenSyncState(bool autoLoadFilterAndSyncKey, SyncStateStorage syncStateStorage)
		{
			AirSyncDiagnostics.TraceInfo<bool>(ExTraceGlobals.RequestsTracer, this, "SyncCollection.OpenSyncState autoLoadFilterAndSyncKey:{0}", autoLoadFilterAndSyncKey);
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				throw new InvalidOperationException();
			}
			MailboxSyncProviderFactory mailboxSyncProviderFactory = this.SyncProviderFactory as MailboxSyncProviderFactory;
			if (mailboxSyncProviderFactory == null)
			{
				throw new NotImplementedException(string.Format("OpenSyncState is not defined for {0}", this.SyncProviderFactory.GetType().FullName));
			}
			if (this.CollectionId == null)
			{
				AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenSyncState] Id: {0}, this.ClassType:{1}", this.InternalName, this.ClassType);
				string a;
				if ((a = this.ClassType) != null)
				{
					StoreObjectId defaultFolderId;
					if (!(a == "Calendar"))
					{
						if (!(a == "Email"))
						{
							if (!(a == "Contacts"))
							{
								if (!(a == "Tasks"))
								{
									goto IL_320;
								}
								defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Tasks);
								this.folderType = DefaultFolderType.Tasks;
							}
							else
							{
								defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Contacts);
								this.folderType = DefaultFolderType.Contacts;
							}
						}
						else
						{
							defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
							this.folderType = DefaultFolderType.Inbox;
						}
					}
					else
					{
						defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
						this.folderType = DefaultFolderType.Calendar;
					}
					mailboxSyncProviderFactory.FolderId = defaultFolderId;
					this.ReturnCollectionId = false;
					if (this.SyncKey == 0U)
					{
						goto IL_3E7;
					}
					try
					{
						mailboxSyncProviderFactory.Folder = this.mailboxFolder;
						this.mailboxFolder = null;
						this.SyncState = syncStateStorage.GetFolderSyncState(this.SyncProviderFactory);
					}
					finally
					{
						if (mailboxSyncProviderFactory.Folder != null)
						{
							mailboxSyncProviderFactory.Folder.Dispose();
							mailboxSyncProviderFactory.Folder = null;
						}
					}
					if (this.SyncState == null)
					{
						this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
						this.ResponseSyncKey = this.SyncKey;
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "SyncKeyErrorInSync3"
						};
					}
					this.CheckProtocolVersion();
					this.CollectionId = this.SyncState.SyncFolderId;
					goto IL_3E7;
				}
				IL_320:
				throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
				{
					ErrorStringForProtocolLogger = "BadClassTypeInSync"
				};
			}
			if (this.SyncKey == 0U)
			{
				if (!autoLoadFilterAndSyncKey)
				{
					goto IL_3E7;
				}
			}
			try
			{
				mailboxSyncProviderFactory.Folder = this.mailboxFolder;
				this.mailboxFolder = null;
				this.SyncState = syncStateStorage.GetFolderSyncState(this.SyncProviderFactory, this.CollectionId);
				if (this.SyncState != null)
				{
					StoreObjectId storeObjectId = this.SyncState.TryGetStoreObjectId();
					if (this.folderType == DefaultFolderType.None && storeObjectId != null)
					{
						this.folderType = mailboxSession.IsDefaultFolderType(storeObjectId);
					}
				}
			}
			finally
			{
				if (mailboxSyncProviderFactory.Folder != null)
				{
					mailboxSyncProviderFactory.Folder.Dispose();
					mailboxSyncProviderFactory.Folder = null;
				}
			}
			if (this.SyncState == null)
			{
				if (autoLoadFilterAndSyncKey)
				{
					this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
					this.ResponseSyncKey = this.SyncKey;
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "SyncKeyErrorInSync"
					};
				}
				using (CustomSyncState customSyncState = syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]))
				{
					if (customSyncState != null)
					{
						FolderIdMapping folderIdMapping = (FolderIdMapping)customSyncState[CustomStateDatumType.IdMapping];
						if (folderIdMapping != null && !folderIdMapping.Contains(this.CollectionId))
						{
							this.Status = SyncBase.ErrorCodeStatus.InvalidCollection;
							throw new AirSyncPermanentException(false)
							{
								ErrorStringForProtocolLogger = "BadCollectionIdInSync"
							};
						}
					}
				}
				this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
				this.ResponseSyncKey = this.SyncKey;
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "NoSyncStateInSync"
				};
			}
			else
			{
				this.CheckProtocolVersion();
				if (autoLoadFilterAndSyncKey)
				{
					if (!this.SyncState.Contains(CustomStateDatumType.SyncKey))
					{
						this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
						this.ResponseSyncKey = this.SyncKey;
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "SyncKeyErrorInSync2"
						};
					}
					this.SyncKey = ((UInt32Data)this.SyncState[CustomStateDatumType.SyncKey]).Data;
					if (this.SyncState.Contains(CustomStateDatumType.RecoverySyncKey))
					{
						this.RecoverySyncKey = ((UInt32Data)this.SyncState[CustomStateDatumType.RecoverySyncKey]).Data;
					}
					this.FilterType = (AirSyncV25FilterTypes)this.SyncState.GetData<Int32Data, int>(CustomStateDatumType.FilterType, 0);
					this.ConversationMode = this.SyncState.GetData<BooleanData, bool>(CustomStateDatumType.ConversationMode, false);
				}
			}
			IL_3E7:
			if (this.SyncKey == 0U)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenSyncState] Id: {0}, SyncKey == 0", this.InternalName);
				SyncState syncState = null;
				MailboxSyncItemId mailboxSyncItemId = null;
				try
				{
					syncState = syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]);
					FolderIdMapping folderIdMapping2 = null;
					FolderTree folderTree = null;
					if (syncState == null)
					{
						if (this.CollectionId != null)
						{
							this.Status = SyncBase.ErrorCodeStatus.ObjectNotFound;
							throw new AirSyncPermanentException(false)
							{
								ErrorStringForProtocolLogger = "NoFolderMappingInSync"
							};
						}
						syncState = syncStateStorage.CreateCustomSyncState(new FolderIdMappingSyncStateInfo());
						folderIdMapping2 = new FolderIdMapping();
						this.CollectionId = folderIdMapping2.Add(MailboxSyncItemId.CreateForNewItem(mailboxSyncProviderFactory.FolderId));
						syncState[CustomStateDatumType.IdMapping] = folderIdMapping2;
						folderTree = new FolderTree();
						MailboxSyncItemId folderId = MailboxSyncItemId.CreateForNewItem(mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox));
						folderTree.AddFolder(folderId);
						folderId = MailboxSyncItemId.CreateForNewItem(mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar));
						folderTree.AddFolder(folderId);
						folderId = MailboxSyncItemId.CreateForNewItem(mailboxSession.GetDefaultFolderId(DefaultFolderType.Contacts));
						folderTree.AddFolder(folderId);
						folderId = MailboxSyncItemId.CreateForNewItem(mailboxSession.GetDefaultFolderId(DefaultFolderType.Tasks));
						folderTree.AddFolder(folderId);
						syncState[CustomStateDatumType.FullFolderTree] = folderTree;
						syncState[CustomStateDatumType.RecoveryFullFolderTree] = syncState[CustomStateDatumType.FullFolderTree];
						syncState.Commit();
					}
					if (folderIdMapping2 == null)
					{
						folderIdMapping2 = (FolderIdMapping)syncState[CustomStateDatumType.IdMapping];
						folderTree = (FolderTree)syncState[CustomStateDatumType.FullFolderTree];
						if (folderIdMapping2 == null || folderTree == null)
						{
							this.Status = SyncBase.ErrorCodeStatus.ObjectNotFound;
							throw new AirSyncPermanentException(false)
							{
								ErrorStringForProtocolLogger = "BadMapTreeInSync"
							};
						}
					}
					StoreObjectId storeObjectId2;
					if (this.CollectionId != null)
					{
						AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenSyncState] Id: {0}, remove the current syncstate on SyncKey 0", this.InternalName);
						mailboxSyncItemId = (folderIdMapping2[this.CollectionId] as MailboxSyncItemId);
						storeObjectId2 = null;
						if (mailboxSyncItemId != null)
						{
							storeObjectId2 = (StoreObjectId)mailboxSyncItemId.NativeId;
							this.Permissions = folderTree.GetPermissions(mailboxSyncItemId);
						}
						syncStateStorage.DeleteFolderSyncState(this.CollectionId);
						mailboxSyncProviderFactory.FolderId = storeObjectId2;
						if (mailboxSyncItemId == null)
						{
							this.Status = SyncBase.ErrorCodeStatus.ObjectNotFound;
							throw new AirSyncPermanentException(false)
							{
								ErrorStringForProtocolLogger = "BadCollectionIdInSync2"
							};
						}
					}
					else
					{
						storeObjectId2 = mailboxSyncProviderFactory.FolderId;
						mailboxSyncItemId = MailboxSyncItemId.CreateForNewItem(storeObjectId2);
						syncStateStorage.DeleteFolderSyncState(this.SyncProviderFactory);
						this.CollectionId = folderIdMapping2[mailboxSyncItemId];
						if (this.CollectionId == null)
						{
							this.CollectionId = folderIdMapping2.Add(mailboxSyncItemId);
							syncState.Commit();
						}
					}
					if (this.folderType == DefaultFolderType.None && storeObjectId2 != null)
					{
						this.folderType = mailboxSession.IsDefaultFolderType(storeObjectId2);
					}
				}
				finally
				{
					if (syncState != null)
					{
						syncState.Dispose();
					}
				}
				this.SyncState = syncStateStorage.CreateFolderSyncState(this.SyncProviderFactory, this.CollectionId);
				this.SyncState.RegisterColdDataKey("IdMapping");
				this.SyncState.RegisterColdDataKey("CustomCalendarSyncFilter");
				this.SyncState[CustomStateDatumType.IdMapping] = new ItemIdMapping(this.CollectionId);
				this.SyncState["Permissions"] = new Int32Data((int)this.Permissions);
				if (this.protocolVersion >= 121)
				{
					this.ClassType = AirSyncUtility.GetAirSyncFolderTypeClass(mailboxSyncItemId);
					this.SyncState[CustomStateDatumType.AirSyncClassType] = new ConstStringData(StaticStringPool.Instance.Intern(this.ClassType));
				}
			}
			else if (this.protocolVersion >= 121)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenSyncState] Id: {0}, this.protocolVersion >= 121", this.InternalName);
				ConstStringData constStringData = (ConstStringData)this.SyncState[CustomStateDatumType.AirSyncClassType];
				if (constStringData != null && constStringData.Data != null && !string.Equals("Unknown", constStringData.Data, StringComparison.OrdinalIgnoreCase))
				{
					this.ClassType = constStringData.Data;
					AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenSyncState] Id: {0}, ClassType populated from SyncState is {1}:", this.InternalName, this.ClassType);
				}
				else
				{
					this.ClassType = AirSyncUtility.GetAirSyncFolderTypeClass(mailboxSyncProviderFactory.FolderId);
					AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.OpenSyncState] Id: {0}, ClassType is not found in SyncState. Populate it from factory.FolderId as {1}", this.InternalName, this.ClassType);
					this.SyncState[CustomStateDatumType.AirSyncClassType] = new ConstStringData(StaticStringPool.Instance.Intern(this.ClassType));
				}
			}
			if (this.ClassType != "Email" && this.ConversationMode)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
				{
					ErrorStringForProtocolLogger = "NonEmailConversationMode"
				};
			}
			if (this.SyncState.CustomVersion != null && this.SyncState.CustomVersion.Value > 9)
			{
				throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.SyncStateVersionInvalid, EASServerStrings.MismatchSyncStateError, true)
				{
					ErrorStringForProtocolLogger = "MixedCASinSync"
				};
			}
			if (this.SyncState[CustomStateDatumType.Permissions] != null)
			{
				this.Permissions = (SyncPermissions)((Int32Data)this.SyncState[CustomStateDatumType.Permissions]).Data;
				this.SyncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data(this.protocolVersion);
				return;
			}
			throw new AirSyncPermanentException(false)
			{
				ErrorStringForProtocolLogger = "NoFolderPermissions"
			};
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public virtual bool GenerateResponsesXmlNode(XmlDocument xmlResponse, IAirSyncVersionFactory versionFactory, string deviceType, GlobalInfo globalInfo, ProtocolLogger protocolLogger, MailboxLogger mailboxLogger)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.GenerateResponsesXmlNode");
			if (this.Responses.Count <= 0)
			{
				return false;
			}
			this.ResponsesResponseXmlNode = xmlResponse.CreateElement("Responses", "AirSync:");
			XmlNode xmlNode = null;
			int num = 0;
			this.currentOptions = 0;
			while (this.currentOptions < this.optionsList.Count)
			{
				if (this.protocolVersion <= 25)
				{
					this.SetSchemaConverterOptions(SyncCollection.emptyPropertyCollection, versionFactory);
				}
				else
				{
					this.SetSchemaOptionsConvertServerToClient(deviceType, versionFactory);
				}
				this.currentOptions++;
			}
			foreach (SyncCommandItem syncCommandItem in this.Responses)
			{
				if (syncCommandItem.ClassType == null)
				{
					syncCommandItem.ClassType = this.ClassType;
				}
				this.SelectSchemaConverterByAirsyncClass(syncCommandItem.ClassType);
				XmlNode xmlNode2 = null;
				if (this.protocolVersion >= 140 && syncCommandItem.ClassType != this.ClassType)
				{
					xmlNode2 = xmlResponse.CreateElement("Class", "AirSync:");
					xmlNode2.InnerText = AirSyncUtility.HtmlEncode(syncCommandItem.ClassType, false);
				}
				switch (syncCommandItem.CommandType)
				{
				case SyncBase.SyncCommandType.Add:
				{
					XmlNode xmlNode3 = xmlResponse.CreateElement("Add", "AirSync:");
					XmlNode xmlNode4 = xmlResponse.CreateElement("ClientId", "AirSync:");
					XmlNode xmlNode5 = xmlResponse.CreateElement("ServerId", "AirSync:");
					XmlNode xmlNode6 = xmlResponse.CreateElement("Status", "AirSync:");
					if (xmlNode2 != null)
					{
						xmlNode3.AppendChild(xmlNode2);
					}
					xmlNode4.InnerText = syncCommandItem.ClientAddId;
					xmlNode3.AppendChild(xmlNode4);
					if (syncCommandItem.SyncId != null)
					{
						xmlNode5.InnerText = syncCommandItem.SyncId;
						xmlNode3.AppendChild(xmlNode5);
					}
					xmlNode6.InnerText = syncCommandItem.Status;
					xmlNode3.AppendChild(xmlNode6);
					if (syncCommandItem.ClassType == "SMS")
					{
						XmlNode xmlNode7 = xmlResponse.CreateElement("ApplicationData", "AirSync:");
						if (syncCommandItem.ConversationId != null)
						{
							xmlNode7.AppendChild(new AirSyncBlobXmlNode(null, "ConversationId", "Email2:", xmlResponse)
							{
								ByteArray = syncCommandItem.ConversationId.GetBytes()
							});
						}
						if (syncCommandItem.ConversationIndex != null)
						{
							xmlNode7.AppendChild(new AirSyncBlobXmlNode(null, "ConversationIndex", "Email2:", xmlResponse)
							{
								ByteArray = syncCommandItem.ConversationIndex
							});
						}
						if (xmlNode7.ChildNodes.Count != 0)
						{
							xmlNode3.AppendChild(xmlNode7);
						}
					}
					else if (this.ClassType == "Email" && syncCommandItem.IsDraft && !syncCommandItem.SendEnabled)
					{
						XmlNode xmlNode8;
						this.CreateApplicationDataNode(xmlResponse, syncCommandItem.ServerId, globalInfo, protocolLogger, mailboxLogger, out xmlNode8);
						if (xmlNode8 != null && xmlNode8.ChildNodes.Count != 0)
						{
							xmlNode3.AppendChild(xmlNode8);
						}
					}
					xmlNode = xmlNode3;
					break;
				}
				case SyncBase.SyncCommandType.Change:
				{
					XmlNode xmlNode9 = xmlResponse.CreateElement("Change", "AirSync:");
					XmlNode xmlNode5 = xmlResponse.CreateElement("ServerId", "AirSync:");
					XmlNode xmlNode6 = xmlResponse.CreateElement("Status", "AirSync:");
					xmlNode5.InnerText = syncCommandItem.SyncId;
					xmlNode6.InnerText = syncCommandItem.Status;
					if (xmlNode2 != null)
					{
						xmlNode9.AppendChild(xmlNode2);
					}
					xmlNode9.AppendChild(xmlNode5);
					xmlNode9.AppendChild(xmlNode6);
					if (syncCommandItem.IsDraft && syncCommandItem.Status == "1")
					{
						XmlNodeList xmlNodeList = syncCommandItem.XmlNode.SelectNodes("//*[contains(name(), 'Attachments')]");
						if (xmlNodeList != null && xmlNodeList.Count > 0)
						{
							XmlNode xmlNode10;
							this.CreateApplicationDataNode(xmlResponse, syncCommandItem.ServerId, globalInfo, protocolLogger, mailboxLogger, out xmlNode10);
							if (xmlNode10 != null && xmlNode10.ChildNodes.Count != 0)
							{
								xmlNode9.AppendChild(xmlNode10);
							}
						}
					}
					xmlNode = xmlNode9;
					break;
				}
				case SyncBase.SyncCommandType.Delete:
				{
					XmlNode xmlNode11 = xmlResponse.CreateElement("Delete", "AirSync:");
					XmlNode xmlNode5 = xmlResponse.CreateElement("ServerId", "AirSync:");
					XmlNode xmlNode6 = xmlResponse.CreateElement("Status", "AirSync:");
					xmlNode5.InnerText = syncCommandItem.SyncId;
					xmlNode6.InnerText = syncCommandItem.Status;
					xmlNode11.AppendChild(xmlNode5);
					xmlNode11.AppendChild(xmlNode6);
					xmlNode = xmlNode11;
					break;
				}
				case SyncBase.SyncCommandType.Fetch:
				{
					XmlNode xmlNode12 = xmlResponse.CreateElement("Fetch", "AirSync:");
					XmlNode xmlNode5 = xmlResponse.CreateElement("ServerId", "AirSync:");
					XmlNode xmlNode6 = xmlResponse.CreateElement("Status", "AirSync:");
					XmlNode xmlNode13 = null;
					string text = syncCommandItem.Status;
					if (text == null)
					{
						text = "1";
						if (this.ClassType == "Email")
						{
							this.ModifyFetchTruncationOption(versionFactory);
							try
							{
								text = this.CreateApplicationDataNode(xmlResponse, syncCommandItem.ServerId, globalInfo, protocolLogger, mailboxLogger, out xmlNode13);
								goto IL_4C2;
							}
							finally
							{
								if (this.protocolVersion > 25)
								{
									this.SetSchemaOptionsConvertServerToClient(deviceType, versionFactory);
								}
							}
						}
						text = "4";
					}
					IL_4C2:
					xmlNode5.InnerText = syncCommandItem.SyncId;
					xmlNode6.InnerText = text;
					if (xmlNode2 != null)
					{
						xmlNode12.AppendChild(xmlNode2);
					}
					xmlNode12.AppendChild(xmlNode5);
					xmlNode12.AppendChild(xmlNode6);
					if (xmlNode13 != null)
					{
						xmlNode12.AppendChild(xmlNode13);
					}
					xmlNode = xmlNode12;
					protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ClientFetches);
					break;
				}
				}
				this.AddExtraNodes(xmlNode, syncCommandItem);
				this.ResponsesResponseXmlNode.AppendChild(xmlNode);
				num++;
			}
			if (num == 0)
			{
				this.ResponsesResponseXmlNode = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002DF58 File Offset: 0x0002C158
		public List<SyncCommand.BadItem> GenerateCommandsXmlNode(XmlDocument xmlResponse, IAirSyncVersionFactory versionFactory, string deviceType, GlobalInfo globalInfo, ProtocolLogger protocolLogger, MailboxLogger mailboxLogger)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.GenerateCommandsXmlNode");
			if (this.GetChanges && ((this.ServerChanges != null && this.ServerChanges.Count > 0) || this.DupeList.Count > 0))
			{
				this.CommandResponseXmlNode = xmlResponse.CreateElement("Commands", "AirSync:");
				XmlNode commandNode;
				foreach (SyncCommandItem syncCommandItem in this.DupeList)
				{
					commandNode = xmlResponse.CreateElement("Delete", "AirSync:");
					XmlNode xmlNode = xmlResponse.CreateElement("ServerId", "AirSync:");
					xmlNode.InnerText = syncCommandItem.SyncId;
					commandNode.AppendChild(xmlNode);
					this.CommandResponseXmlNode.AppendChild(commandNode);
				}
				this.currentOptions = 0;
				while (this.currentOptions < this.optionsList.Count)
				{
					this.SetSchemaOptionsConvertServerToClient(deviceType, versionFactory);
					this.currentOptions++;
				}
				List<SyncCommand.BadItem> itemFailureList = new List<SyncCommand.BadItem>();
				FirstTimeSyncProvider firstTimeSyncProvider = this.FolderSync.SyncProvider as FirstTimeSyncProvider;
				if (firstTimeSyncProvider != null && firstTimeSyncProvider.BadItems != null)
				{
					this.Context.ProtocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FirstTimeSyncItemsDiscarded, firstTimeSyncProvider.BadItems.Count);
					foreach (SyncCommand.BadItem item in firstTimeSyncProvider.BadItems)
					{
						itemFailureList.Add(item);
					}
				}
				Action<SyncOperation> action = delegate(SyncOperation changeObject)
				{
					string text = null;
					string classFromMessageClass;
					if (string.IsNullOrEmpty(changeObject.MessageClass))
					{
						classFromMessageClass = this.ClassType;
					}
					else
					{
						classFromMessageClass = versionFactory.GetClassFromMessageClass(changeObject.MessageClass);
					}
					if (this.protocolVersion == 140 && changeObject.ChangeType == ChangeType.Delete && string.Equals(classFromMessageClass, "SMS", StringComparison.OrdinalIgnoreCase) && !this.RequestAnnotations.ContainsAnnotation(Constants.ServerSideDeletes, this.collectionId, classFromMessageClass))
					{
						this.DeleteId(changeObject.Id);
						AirSyncDiagnostics.TraceInfo<string, object>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, Skipping Delete for SMS. Item Id: {1}", this.InternalName, changeObject.Id.NativeId);
						protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.SkippedDeletes);
						return;
					}
					XmlNode xmlNode2 = null;
					if (this.protocolVersion >= 140 && classFromMessageClass != this.ClassType)
					{
						xmlNode2 = xmlResponse.CreateElement("Class", "AirSync:");
						xmlNode2.InnerText = AirSyncUtility.HtmlEncode(classFromMessageClass, false);
					}
					if (changeObject.ChangeType == ChangeType.SoftDelete)
					{
						if (this.protocolVersion >= 25)
						{
							commandNode = xmlResponse.CreateElement("SoftDelete", "AirSync:");
						}
						else
						{
							commandNode = xmlResponse.CreateElement("Delete", "AirSync:");
						}
						if (xmlNode2 != null)
						{
							commandNode.AppendChild(xmlNode2);
						}
						XmlNode xmlNode3 = xmlResponse.CreateElement("ServerId", "AirSync:");
						text = this.GetStringIdFromSyncItemId(changeObject.Id, false);
						xmlNode3.InnerText = text;
						this.DeleteId(changeObject.Id);
						commandNode.AppendChild(xmlNode3);
						this.HasServerChanges = true;
						protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerSoftDeletes);
					}
					else
					{
						if (changeObject.ChangeType != ChangeType.Delete)
						{
							this.SelectSchemaConverterByAirsyncClass(classFromMessageClass);
							commandNode = xmlResponse.CreateElement((changeObject.ChangeType == ChangeType.Add || changeObject.ChangeType == ChangeType.AssociatedAdd) ? "Add" : "Change", "AirSync:");
							if (xmlNode2 != null)
							{
								commandNode.AppendChild(xmlNode2);
							}
							XmlNode xmlNode4 = xmlResponse.CreateElement("ServerId", "AirSync:");
							XmlNode xmlNode5 = xmlResponse.CreateElement("ApplicationData", "AirSync:");
							xmlResponse.CreateElement("Status", "AirSync:");
							bool flag2 = false;
							if (changeObject.ChangeType == ChangeType.Add || changeObject.ChangeType == ChangeType.AssociatedAdd)
							{
								text = this.GetStringIdFromSyncItemId(changeObject.Id, true);
								if (changeObject.ChangeType == ChangeType.AssociatedAdd)
								{
									protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerAssociatedAdds);
								}
								else
								{
									protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerAdds);
								}
							}
							else
							{
								text = this.GetStringIdFromSyncItemId(changeObject.Id, false);
								protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerChanges);
							}
							AirSyncDataObject airSyncDataObject = null;
							try
							{
								using (ISyncItem syncItem = this.BindToSyncItem(changeObject))
								{
									try
									{
										if (syncItem.NativeItem is StoreObject && (syncItem.NativeItem as StoreObject).GetValueOrDefault<bool>(MessageItemSchema.HasBeenSubmitted))
										{
											changeObject.Reject();
											this.DeleteId(syncItem.Id);
											AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, ChangeTrackItemRejectedException thrown to avoid syncing Transient Draft message.. Location GenerateCommandsXmlNode.", this.InternalName);
											throw new ChangeTrackingItemRejectedException();
										}
										this.ConvertServerToClientObject(syncItem, xmlNode5, changeObject, globalInfo);
										if (this.ProtocolVersion == 140 && changeObject.ChangeType == ChangeType.AssociatedAdd)
										{
											airSyncDataObject = this.AirSyncDataObject;
											if (this.TruncationSizeZeroAirSyncDataObject == null)
											{
												this.CreateTruncationSizeZeroAirSyncDataObject(deviceType, versionFactory);
											}
											this.AirSyncDataObject = this.TruncationSizeZeroAirSyncDataObject;
											xmlNode5.RemoveAll();
											this.ConvertServerToClientObject(syncItem, xmlNode5, null, globalInfo);
										}
										MessageItem messageItem;
										if (this.folderType == DefaultFolderType.Outbox && ObjectClass.IsOfClass(changeObject.MessageClass, "IPM.Note.Mobile.SMS") && (messageItem = (syncItem.NativeItem as MessageItem)) != null)
										{
											messageItem.Load(new PropertyDefinition[]
											{
												MessageItemSchema.TextMessageDeliveryStatus
											});
											int valueOrDefault = messageItem.GetValueOrDefault<int>(MessageItemSchema.TextMessageDeliveryStatus, 0);
											if (50 > valueOrDefault)
											{
												messageItem.OpenAsReadWrite();
												messageItem.SetProperties(SyncCollection.propertyTextMessageDeliveryStatus, SyncCollection.propertyValueTextMessageDeliveryStatus);
												messageItem.Save(SaveMode.ResolveConflicts);
											}
										}
									}
									catch (Exception ex)
									{
										flag2 = true;
										if (ex is ChangeTrackingItemRejectedException)
										{
											AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
											AirSyncDiagnostics.TraceInfo<string, AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, ChangeTrackItemRejectedException was caught while syncing. Location GenerateCommandsXmlNode.{1}", this.InternalName, arg);
											protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerChangeTrackingRejected);
											return;
										}
										if (SyncCommand.IsItemSyncTolerableException(ex))
										{
											AirSyncUtility.ExceptionToStringHelper arg2 = new AirSyncUtility.ExceptionToStringHelper(ex);
											AirSyncDiagnostics.TraceError<string, AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.CorruptItemTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, Sync-tolerable exception caught while syncing. Location GenerateCommandsXmlNode.\r\n{1}", this.InternalName, arg2);
											changeObject.Reject();
											protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerFailedToConvert);
											if (mailboxLogger != null)
											{
												mailboxLogger.SetData(MailboxLogDataName.SyncCommand_GenerateResponsesXmlNode_AddChange_Exception, ex);
											}
											AirSyncCounters.NumberOfServerItemConversionFailure.Increment();
											Command.CurrentCommand.PartialFailure = true;
											if (SyncCommand.IsConversionFailedTolerableException(ex))
											{
												AirSyncDiagnostics.TraceInfo<string, Exception>(ExTraceGlobals.CorruptItemTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, Conversion-tolerable exception caught while syncing: {1}", this.InternalName, ex.InnerException);
											}
											else if (syncItem is MailboxSyncItem)
											{
												Item dataItem = syncItem.NativeItem as Item;
												SyncCommand.BadItem badItem = SyncCommand.BadItem.CreateFromItem(dataItem, this.SyncTypeString == "R", ex);
												if (badItem != null)
												{
													itemFailureList.Add(badItem);
												}
											}
											return;
										}
										throw;
									}
								}
							}
							catch (Exception ex2)
							{
								if (ex2 is VirusScanInProgressException)
								{
									AirSyncUtility.ExceptionToStringHelper arg3 = new AirSyncUtility.ExceptionToStringHelper(ex2);
									AirSyncDiagnostics.TraceError<string, AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, VirusScanInProgressException caught while syncing.\r\n{1}", this.InternalName, arg3);
									if (mailboxLogger != null)
									{
										mailboxLogger.SetData(MailboxLogDataName.SyncCommand_GenerateResponsesXmlNode_AddChange_Exception, ex2);
									}
									return;
								}
								if (!flag2 && (SyncCommand.IsItemSyncTolerableException(ex2) || SyncCommand.IsObjectNotFound(ex2)))
								{
									AirSyncUtility.ExceptionToStringHelper arg4 = new AirSyncUtility.ExceptionToStringHelper(ex2);
									AirSyncDiagnostics.TraceError<string, AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.CorruptItemTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, Sync-tolerable exception caught while syncing.\r\n{1}", this.InternalName, arg4);
									changeObject.Reject();
									protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerFailedToConvert);
									AirSyncCounters.NumberOfServerItemConversionFailure.Increment();
									if (mailboxLogger != null)
									{
										mailboxLogger.SetData(MailboxLogDataName.SyncCommand_GenerateResponsesXmlNode_AddChange_Exception, ex2);
									}
									return;
								}
								throw;
							}
							finally
							{
								if (airSyncDataObject != null)
								{
									this.AirSyncDataObject = airSyncDataObject;
								}
							}
							if (!this.ClientFetchedItems.ContainsKey(changeObject.Id))
							{
								xmlNode4.InnerText = text;
								commandNode.AppendChild(xmlNode4);
								if (xmlNode5.HasChildNodes)
								{
									commandNode.AppendChild(xmlNode5);
									goto IL_9B4;
								}
								goto IL_9B4;
							}
							return;
						}
						commandNode = xmlResponse.CreateElement("Delete", "AirSync:");
						if (xmlNode2 != null)
						{
							commandNode.AppendChild(xmlNode2);
						}
						XmlNode xmlNode6 = xmlResponse.CreateElement("ServerId", "AirSync:");
						text = this.GetStringIdFromSyncItemId(changeObject.Id, false);
						xmlNode6.InnerText = text;
						this.DeleteId(changeObject.Id);
						commandNode.AppendChild(xmlNode6);
						this.HasServerChanges = true;
						protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ServerDeletes);
					}
					IL_9B4:
					if (text == null)
					{
						AirSyncDiagnostics.TraceError<string, ChangeType, ISyncItemId>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GenerateCommandsXmlNode] Id: {0}, syncId == null, changeType = {1}, UniqueItemId = {2}", this.InternalName, changeObject.ChangeType, changeObject.Id);
						return;
					}
					this.CommandResponseXmlNode.AppendChild(commandNode);
				};
				bool flag = false;
				foreach (SyncCollection.Options options in this.optionsList)
				{
					if (options.MailboxSchemaOptions.HasBodyPartPreferences)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					try
					{
						Command.CurrentCommand.EnableConversationDoubleLoadCheck(true);
						IEnumerable<IGrouping<ConversationId, SyncOperation>> enumerable = from change in this.ServerChanges
						group change by change.ConversationId;
						foreach (IGrouping<ConversationId, SyncOperation> grouping in enumerable)
						{
							if (grouping.Key != null)
							{
								IEnumerable<StoreObjectId> source = from change in grouping
								where change.ChangeType == ChangeType.Add || change.ChangeType == ChangeType.Change || change.ChangeType == ChangeType.AssociatedAdd
								select (StoreObjectId)change.Id.NativeId;
								IList<StoreObjectId> list = source.ToList<StoreObjectId>();
								if (list.Count > 0)
								{
									Conversation conversation;
									bool orCreateConversation = Command.CurrentCommand.GetOrCreateConversation(grouping.Key, true, out conversation);
									if (orCreateConversation && conversation != null)
									{
										conversation.LoadItemParts(list);
									}
								}
							}
							foreach (SyncOperation obj in grouping)
							{
								action(obj);
							}
						}
						goto IL_3D0;
					}
					finally
					{
						Command.CurrentCommand.EnableConversationDoubleLoadCheck(false);
					}
				}
				int num = 0;
				while (this.ServerChanges != null && num < this.ServerChanges.Count)
				{
					action(this.ServerChanges[num]);
					num++;
				}
				IL_3D0:
				return itemFailureList;
			}
			return null;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002E3D4 File Offset: 0x0002C5D4
		public bool IsLogicallyEmptyResponse
		{
			get
			{
				return this.ResponseSyncKey == this.SyncKey && (this.Responses == null || this.Responses.Count == 0);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0002E3FE File Offset: 0x0002C5FE
		public bool IsEmptyWithMoreAvailableResponse
		{
			get
			{
				return this.MoreAvailable && this.IsLogicallyEmptyResponse;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002E410 File Offset: 0x0002C610
		public void FinalizeCollectionXmlNode(XmlDocument xmlResponse)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.FinalizeCollectionXmlNode");
			XmlElement xmlElement = xmlResponse.CreateElement("Collection", "AirSync:");
			this.CollectionResponseXmlNode = xmlElement;
			XmlElement xmlElement2 = xmlResponse.CreateElement("Class", "AirSync:");
			xmlElement2.InnerText = this.ClassType;
			XmlElement xmlElement3 = xmlResponse.CreateElement("Status", "AirSync:");
			xmlElement3.InnerText = SyncCommand.GetStaticStatusString(this.Status);
			XmlElement xmlElement4 = xmlResponse.CreateElement("SyncKey", "AirSync:");
			xmlElement4.InnerText = this.ResponseSyncKey.ToString(CultureInfo.InvariantCulture);
			XmlElement xmlElement5 = xmlResponse.CreateElement("CollectionId", "AirSync:");
			xmlElement5.InnerText = this.CollectionId;
			if (this.Status != SyncBase.ErrorCodeStatus.Success)
			{
				if (this.protocolVersion < 121)
				{
					this.CollectionResponseXmlNode.AppendChild(xmlElement2);
				}
				if (this.Status == SyncBase.ErrorCodeStatus.InvalidSyncKey || this.Status == SyncBase.ErrorCodeStatus.ServerError)
				{
					this.CollectionResponseXmlNode.AppendChild(xmlElement4);
					this.CollectionResponseXmlNode.AppendChild(xmlElement5);
				}
				this.CollectionResponseXmlNode.AppendChild(xmlElement3);
				return;
			}
			if (this.protocolVersion < 121)
			{
				this.CollectionResponseXmlNode.AppendChild(xmlElement2);
			}
			this.CollectionResponseXmlNode.AppendChild(xmlElement4);
			if (this.ReturnCollectionId)
			{
				this.CollectionResponseXmlNode.AppendChild(xmlElement5);
			}
			this.CollectionResponseXmlNode.AppendChild(xmlElement3);
			if (this.MoreAvailable || this.DupesFilledWindowSize)
			{
				XmlElement newChild = xmlResponse.CreateElement("MoreAvailable", "AirSync:");
				this.CollectionResponseXmlNode.AppendChild(newChild);
			}
			if (this.ResponsesResponseXmlNode != null)
			{
				this.CollectionResponseXmlNode.AppendChild(this.ResponsesResponseXmlNode);
			}
			if (this.CommandResponseXmlNode != null && this.CommandResponseXmlNode.HasChildNodes)
			{
				this.CollectionResponseXmlNode.AppendChild(this.CommandResponseXmlNode);
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002E5E4 File Offset: 0x0002C7E4
		public void LogCollectionData(ProtocolLogger protocolLogger)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.LogCollectionData");
			protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.SyncType, this.SyncTypeString);
			protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.MidnightRollover, this.MidnightRollover ? 1 : 0);
			foreach (SyncCollection.Options options in this.optionsList)
			{
				if (options.Class == "SMS")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.SmsFilterType, (int)options.FilterType);
				}
				else
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FilterType, (int)options.FilterType);
				}
			}
			protocolLogger.IncrementValue(ProtocolLoggerData.TotalFolders);
			if (this.ClassType != null)
			{
				if (this.ClassType == "Email")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderDataType, "Em");
				}
				else if (this.ClassType == "Calendar")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderDataType, "Ca");
				}
				else if (this.ClassType == "Contacts")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderDataType, "Co");
				}
				else if (this.ClassType == "Tasks")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderDataType, "Ta");
				}
				else if (this.ClassType == "Notes")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderDataType, "Nt");
				}
				else if (this.ClassType == "RecipientInfoCache")
				{
					protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderDataType, "Ri");
				}
			}
			if (this.CollectionId != null)
			{
				protocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.FolderId, this.CollectionId);
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002E7BC File Offset: 0x0002C9BC
		public virtual void UpdateSavedNullSyncPropertiesInCache(object[] values)
		{
			FolderSyncStateMetadata folderSyncStateMetadata = this.GetFolderSyncStateMetadata();
			if (folderSyncStateMetadata != null)
			{
				folderSyncStateMetadata.UpdateSyncCollectionNullSyncValues((bool)values[5], (int)values[2], (int)values[4], (long)values[0], (long)values[1], (int)values[6], (int)values[3]);
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002E814 File Offset: 0x0002CA14
		public virtual object[] GetNullSyncPropertiesToSave()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.GetNullSyncPropertiesToSave");
			MailboxSyncProviderFactory mailboxSyncProviderFactory = this.SyncProviderFactory as MailboxSyncProviderFactory;
			FolderSyncStateMetadata folderSyncStateMetadata = this.SyncState.SyncStateMetadata as FolderSyncStateMetadata;
			if (mailboxSyncProviderFactory == null || folderSyncStateMetadata == null)
			{
				return null;
			}
			IStorePropertyBag nullSyncPropertiesFromIPMFolder = folderSyncStateMetadata.GetNullSyncPropertiesFromIPMFolder(this.StoreSession as MailboxSession);
			ExDateTime exDateTime = ExDateTime.MinValue;
			ExDateTime exDateTime2;
			if (this.SyncKey != 0U && !this.MoreAvailable && !this.DupesFilledWindowSize && AirSyncUtility.TryGetPropertyFromBag<ExDateTime>(nullSyncPropertiesFromIPMFolder, FolderSchema.LocalCommitTimeMax, out exDateTime2))
			{
				exDateTime = exDateTime2;
			}
			int num;
			if (!AirSyncUtility.TryGetPropertyFromBag<int>(nullSyncPropertiesFromIPMFolder, FolderSchema.DeletedCountTotal, out num))
			{
				num = 0;
			}
			return new object[]
			{
				this.lastSyncTime.UtcTicks,
				exDateTime.UtcTicks,
				num,
				(int)this.ResponseSyncKey,
				this.FilterTypeHash,
				this.ConversationMode,
				this.deviceSettingsHash
			};
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002E924 File Offset: 0x0002CB24
		public virtual bool CollectionRequiresSync(bool ignoreSyncKeyAndFilter, bool nullSyncAllowed)
		{
			AirSyncDiagnostics.TraceInfo<bool>(ExTraceGlobals.RequestsTracer, this, "SyncCollection.CollectionRequiresSync ignoreSyncKeyAndFilter:{0}", ignoreSyncKeyAndFilter);
			if (!ignoreSyncKeyAndFilter && this.SyncKey == 0U)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (SyncKey0):true", this.InternalName);
				return true;
			}
			if (!nullSyncAllowed)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncColleciton.CollectionRequiresSync] Id: {0}, (NullSync not allowed) true", this.InternalName);
				return true;
			}
			FolderSyncStateMetadata folderSyncStateMetadata = this.GetFolderSyncStateMetadata();
			if (folderSyncStateMetadata == null || folderSyncStateMetadata.IPMFolderId == null || !folderSyncStateMetadata.HasValidNullSyncData)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (NoMetadata):true", this.InternalName);
				return true;
			}
			try
			{
				using (Folder folder = Folder.Bind(this.storeSession, folderSyncStateMetadata.IPMFolderId, FolderSyncStateMetadata.IPMFolderNullSyncProperties))
				{
					try
					{
						if (folderSyncStateMetadata.AirSyncLocalCommitTime != ((ExDateTime)folder[FolderSchema.LocalCommitTimeMax]).UtcTicks)
						{
							AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (LocalCommitTime):true", this.InternalName);
							return true;
						}
						if (folderSyncStateMetadata.AirSyncDeletedCountTotal != (int)folder[FolderSchema.DeletedCountTotal])
						{
							AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (DeletedCount):true", this.InternalName);
							return true;
						}
						if (!ignoreSyncKeyAndFilter && this.SyncKey != (uint)folderSyncStateMetadata.AirSyncSyncKey)
						{
							AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (SyncKey):true", this.InternalName);
							return true;
						}
						if (!ignoreSyncKeyAndFilter && (this.HasFilterNode || this.HasOptionsNodes || this.protocolVersion < 121) && this.FilterTypeHash != folderSyncStateMetadata.AirSyncFilter)
						{
							AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (Filter):true", this.InternalName);
							return true;
						}
						if (this.lastSyncTime.AddDays(-1.0).UtcTicks > folderSyncStateMetadata.AirSyncLastSyncTime)
						{
							AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (LastSync):true", this.InternalName);
							return true;
						}
						if (this.protocolVersion > 121 && this.deviceSettingsHash != folderSyncStateMetadata.AirSyncSettingsHash)
						{
							AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (SettingsHash):true", this.InternalName);
							return true;
						}
					}
					catch (NotInBagPropertyErrorException ex)
					{
						AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (notInBag: {1})", this.InternalName, ex.Message);
						return true;
					}
					catch (PropertyErrorException ex2)
					{
						AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (propertyError: {1})", this.InternalName, ex2.Message);
						return true;
					}
				}
			}
			catch (ObjectNotFoundException ex3)
			{
				folderSyncStateMetadata.ParentDevice.TryRemove(folderSyncStateMetadata.Name, null);
				AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, (IPMFolderNotFound: {1})", this.InternalName, ex3.Message);
				return true;
			}
			this.nullSyncWorked = true;
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CollectionRequiresSync] Id: {0}, false", this.InternalName);
			return false;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002EC44 File Offset: 0x0002CE44
		public byte[] SerializeOptions()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "SyncCollection.SerializeOptions");
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(50))
			{
				WbxmlWriter wbxmlWriter = new WbxmlWriter(memoryStream);
				XmlElement xmlElement = this.optionsList[0].OptionsNode.OwnerDocument.CreateElement("Collection", "AirSync:");
				foreach (SyncCollection.Options options in this.optionsList)
				{
					xmlElement.AppendChild(options.OptionsNode);
				}
				wbxmlWriter.WriteXmlDocumentFromElement(xmlElement);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002ED0C File Offset: 0x0002CF0C
		public void ParseStickyOptions()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "ParseStickyOptions");
			XmlNode xmlNode = null;
			ByteArrayData byteArrayData = (ByteArrayData)this.SyncState[CustomStateDatumType.CachedOptionsNode];
			if (byteArrayData != null && byteArrayData.Data != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(byteArrayData.Data))
				{
					using (WbxmlReader wbxmlReader = new WbxmlReader(memoryStream))
					{
						xmlNode = wbxmlReader.ReadXmlDocument().FirstChild;
					}
				}
			}
			if (xmlNode == null)
			{
				return;
			}
			this.optionsList = new List<SyncCollection.Options>(xmlNode.ChildNodes.Count);
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode node = (XmlNode)obj;
				this.ParseOptionsNode(node);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002EE0C File Offset: 0x0002D00C
		public void AddDefaultOptions()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "AddDefaultOptions");
			if (this.optionsList == null)
			{
				this.optionsList = new List<SyncCollection.Options>(1);
			}
			else if (this.optionsList.Count > 0)
			{
				return;
			}
			SyncCollection.Options options = new SyncCollection.Options(null);
			options.Class = this.ClassType;
			options.FilterType = this.FilterType;
			this.optionsList.Add(options);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002EE7C File Offset: 0x0002D07C
		public void InsertOptionsNode()
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "InsertOptionsNode");
			if (this.collectionNode != null)
			{
				foreach (object obj in this.collectionNode)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.LocalName.Equals("Options"))
					{
						return;
					}
				}
				this.currentOptions = 0;
				while (this.currentOptions < this.optionsList.Count)
				{
					XmlNode xmlNode2 = this.OptionsNode;
					if (xmlNode2 != null)
					{
						xmlNode2 = this.collectionNode.OwnerDocument.ImportNode(xmlNode2, true);
						this.collectionNode.AppendChild(xmlNode2);
					}
					this.currentOptions++;
				}
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002EF54 File Offset: 0x0002D154
		public override string ToString()
		{
			return string.Format("Collection ID:{0} Type:{1}", this.CollectionId, this.classType);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002EF6C File Offset: 0x0002D16C
		protected void ApplyChangeTrackFilter(SyncOperation changeObject, XmlNode airSyncParentNode)
		{
			if (changeObject != null && (changeObject.ChangeType == ChangeType.Add || changeObject.ChangeType == ChangeType.Change || changeObject.ChangeType == ChangeType.ReadFlagChange || changeObject.ChangeType == ChangeType.AssociatedAdd))
			{
				changeObject.ChangeTrackingInformation = this.ChangeTrackFilter.Filter(airSyncParentNode, changeObject.ChangeTrackingInformation);
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002EFB8 File Offset: 0x0002D1B8
		protected void SetHasChanges(SyncOperation changeObject)
		{
			if (changeObject != null && (changeObject.ChangeType == ChangeType.Add || changeObject.ChangeType == ChangeType.Change || changeObject.ChangeType == ChangeType.AssociatedAdd))
			{
				this.HasAddsOrChangesToReturnToClientImmediately = true;
			}
			this.HasServerChanges = true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002EFE6 File Offset: 0x0002D1E6
		protected virtual ISyncItem CreateSyncItem(Item mailboxItem)
		{
			return MailboxSyncItem.Bind(mailboxItem);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		protected virtual void SetFilterType(bool isQuarantineMailAvailable, GlobalInfo globalInfo)
		{
			if (this.optionsList.Count == 1 && this.optionsList[0].Class == "Calendar")
			{
				this.SetCalendarFilterType(this.optionsList[0]);
				return;
			}
			QueryFilter[] array = new QueryFilter[this.optionsList.Count];
			int num = 0;
			bool flag = false;
			foreach (SyncCollection.Options options in this.optionsList)
			{
				QueryFilter supportedClassFilter = options.AirSyncXsoSchemaState.SupportedClassFilter;
				if (supportedClassFilter == null)
				{
					throw new InvalidOperationException();
				}
				List<QueryFilter> list = new List<QueryFilter>(4);
				list.Add(supportedClassFilter);
				string @class;
				if ((@class = options.Class) != null)
				{
					if (!(@class == "Email"))
					{
						if (!(@class == "SMS"))
						{
							if (!(@class == "Tasks"))
							{
								if (!(@class == "Contacts") && !(@class == "Notes"))
								{
									goto IL_1D7;
								}
							}
							else
							{
								QueryFilter queryFilter = this.BuildRestrictiveFilter(options.FilterType);
								if (queryFilter != null)
								{
									list.Add(queryFilter);
								}
								flag = true;
							}
						}
						else
						{
							this.FolderSync.SetConversationMode(this.ConversationMode);
							QueryFilter queryFilter = this.BuildRestrictiveFilter(options.FilterType);
							if (queryFilter != null)
							{
								list.Add(queryFilter);
							}
							if (this.folderType == DefaultFolderType.Outbox)
							{
								if (this.deviceEnableOutboundSMS)
								{
									E164Number e164Number;
									if (E164Number.TryParse(this.devicePhoneNumberForSms, out e164Number))
									{
										queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.From, new Participant(null, e164Number.Number, "MOBILE"));
									}
									else
									{
										queryFilter = SyncCollection.falseFilterInstance;
									}
								}
								else
								{
									queryFilter = SyncCollection.falseFilterInstance;
								}
								list.Add(queryFilter);
							}
							flag = true;
						}
					}
					else
					{
						this.FolderSync.SetConversationMode(this.ConversationMode);
						QueryFilter queryFilter = this.BuildRestrictiveFilter(options.FilterType);
						if (queryFilter != null)
						{
							list.Add(queryFilter);
						}
						flag = true;
					}
					if (isQuarantineMailAvailable)
					{
						if (globalInfo.ABQMailId == null)
						{
							list.Add(SyncCollection.falseFilterInstance);
						}
						else
						{
							MailboxSyncProvider mailboxSyncProvider = (MailboxSyncProvider)this.FolderSync.SyncProvider;
							mailboxSyncProvider.ItemQueryOptimizationFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Id, globalInfo.ABQMailId);
							mailboxSyncProvider.UseSortOrder = false;
							this.isSendingABQMail = true;
							FirstTimeSyncProvider firstTimeSyncProvider = this.FolderSync.SyncProvider as FirstTimeSyncProvider;
							if (firstTimeSyncProvider != null)
							{
								firstTimeSyncProvider.ABQMailId = globalInfo.ABQMailId;
							}
						}
						flag = false;
					}
					if (list.Count == 1)
					{
						array[num++] = list[0];
						continue;
					}
					array[num++] = new AndFilter(list.ToArray());
					continue;
				}
				IL_1D7:
				throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
				{
					ErrorStringForProtocolLogger = "BadClassWithFilterSetOnSync"
				};
			}
			QueryFilter activeFilter;
			if (array.Length == 1)
			{
				activeFilter = array[0];
			}
			else
			{
				activeFilter = new OrFilter(array);
			}
			if (flag)
			{
				MailboxSyncProvider mailboxSyncProvider2 = (MailboxSyncProvider)this.FolderSync.SyncProvider;
				mailboxSyncProvider2.ItemQueryOptimizationFilter = this.BuildLeastRestrictiveFilter();
			}
			this.FolderSync.SetSyncFilters(activeFilter, this.GetFilterId(isQuarantineMailAvailable), new ISyncFilter[0]);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002F324 File Offset: 0x0002D524
		protected virtual void AddExtraNodes(XmlNode responseNode, SyncCommandItem item)
		{
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002F328 File Offset: 0x0002D528
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.syncState != null)
				{
					this.syncState.Dispose();
					this.syncState = null;
				}
				if (this.clientCommands != null)
				{
					foreach (SyncCommandItem syncCommandItem in this.clientCommands)
					{
						syncCommandItem.Dispose();
					}
				}
				if (this.responses != null)
				{
					foreach (SyncCommandItem syncCommandItem2 in this.responses)
					{
						syncCommandItem2.Dispose();
					}
				}
				if (this.mailboxFolder != null)
				{
					this.mailboxFolder.Dispose();
					this.mailboxFolder = null;
				}
				this.storeSession = null;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002F3EC File Offset: 0x0002D5EC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncCollection>(this);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		internal bool SelectSchemaConverterByAirsyncClass(string airsyncClass)
		{
			this.currentOptions = 0;
			while (this.currentOptions < this.optionsList.Count)
			{
				if (this.optionsList[this.currentOptions].Class == airsyncClass)
				{
					return true;
				}
				this.currentOptions++;
			}
			return false;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002F44C File Offset: 0x0002D64C
		internal string GetClassFromISyncItemId(ISyncItemId id, IAirSyncVersionFactory versionFactory)
		{
			string messageClassFromItemId = this.FolderSync.GetMessageClassFromItemId(id);
			if (messageClassFromItemId != null)
			{
				return versionFactory.GetClassFromMessageClass(messageClassFromItemId);
			}
			return null;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002F472 File Offset: 0x0002D672
		internal void SetAllSchemaConverterOptions(IDictionary schemaConverterOptions, IAirSyncVersionFactory versionFactory)
		{
			this.currentOptions = 0;
			while (this.currentOptions < this.optionsList.Count)
			{
				this.SetSchemaConverterOptions(schemaConverterOptions, versionFactory);
				this.currentOptions++;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
		protected void CheckProtocolVersion()
		{
			int data = this.SyncState.GetData<Int32Data, int>(CustomStateDatumType.AirSyncProtocolVersion, -1);
			if ((data > 25 || this.protocolVersion > 25) && (data != 120 || this.protocolVersion != 121) && data != this.protocolVersion)
			{
				this.Status = SyncBase.ErrorCodeStatus.InvalidSyncKey;
				this.ResponseSyncKey = this.SyncKey;
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "VersionSwitchWithoutSk0"
				};
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002F518 File Offset: 0x0002D718
		protected void ParseClientCommands(List<XmlNode> itemLevelProtocolErrorNodes)
		{
			this.CheckFullAccess();
			using (XmlNodeList childNodes = this.CommandRequestXmlNode.ChildNodes)
			{
				List<SyncCommandItem> list = new List<SyncCommandItem>(childNodes.Count);
				List<SyncCommandItem> list2 = new List<SyncCommandItem>();
				HashSet<string> hashSet = new HashSet<string>();
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					foreach (object obj in childNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.NodeType == XmlNodeType.Element)
						{
							SyncCommandItem syncCommandItem = new SyncCommandItem();
							disposeGuard.Add<SyncCommandItem>(syncCommandItem);
							try
							{
								string localName;
								if ((localName = xmlNode.LocalName) != null)
								{
									if (!(localName == "Add"))
									{
										if (!(localName == "Change"))
										{
											if (!(localName == "Delete"))
											{
												if (localName == "Fetch")
												{
													syncCommandItem.CommandType = SyncBase.SyncCommandType.Fetch;
												}
											}
											else
											{
												syncCommandItem.CommandType = SyncBase.SyncCommandType.Delete;
												syncCommandItem.ChangeType = ChangeType.Delete;
											}
										}
										else
										{
											syncCommandItem.CommandType = SyncBase.SyncCommandType.Change;
											syncCommandItem.ChangeType = ChangeType.Change;
										}
									}
									else
									{
										syncCommandItem.CommandType = SyncBase.SyncCommandType.Add;
										syncCommandItem.ChangeType = ChangeType.Add;
									}
								}
								foreach (object obj2 in xmlNode.ChildNodes)
								{
									XmlNode xmlNode2 = (XmlNode)obj2;
									string localName2;
									if ((localName2 = xmlNode2.LocalName) != null)
									{
										if (!(localName2 == "ClientId"))
										{
											if (!(localName2 == "ServerId"))
											{
												if (!(localName2 == "ApplicationData"))
												{
													if (!(localName2 == "Class"))
													{
														if (localName2 == "Send")
														{
															syncCommandItem.SendEnabled = true;
														}
													}
													else
													{
														syncCommandItem.ClassType = xmlNode2.InnerText;
													}
												}
												else
												{
													syncCommandItem.XmlNode = xmlNode2;
												}
											}
											else
											{
												syncCommandItem.SyncId = xmlNode2.InnerText;
											}
										}
										else
										{
											syncCommandItem.ClientAddId = xmlNode2.InnerText;
											if (hashSet.Contains(syncCommandItem.ClientAddId))
											{
												this.Status = SyncBase.ErrorCodeStatus.ProtocolError;
												throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
												{
													ErrorStringForProtocolLogger = "DupeIdsInSync"
												};
											}
											hashSet.Add(syncCommandItem.ClientAddId);
										}
									}
								}
								if (itemLevelProtocolErrorNodes.Contains(xmlNode) || (syncCommandItem.ChangeType == ChangeType.Change && syncCommandItem.XmlNode == null && !syncCommandItem.SendEnabled))
								{
									AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ConversionTracer, this, "[SyncCollection.ParseClientCommandsList] Id: {0}, Conversion error ParseClientCommands.", this.InternalName);
									throw new ConversionException("Conversion error occurred while parsing command");
								}
								if (syncCommandItem.CommandType == SyncBase.SyncCommandType.Add)
								{
									this.ParseAndRemoveAnnotationInAppData(syncCommandItem.XmlNode, syncCommandItem.ClientAddId);
									if ((this.HasSmsExtension || this.HasMmsAnnotation) && syncCommandItem.ClassType == "SMS")
									{
										this.ParseConsumerSmsAndMmsDataInApplicationData(syncCommandItem);
									}
								}
								else if (syncCommandItem.CommandType == SyncBase.SyncCommandType.Change)
								{
									this.ParseAndRemoveAnnotationInAppData(syncCommandItem.XmlNode, syncCommandItem.SyncId);
								}
								list.Add(syncCommandItem);
							}
							catch (ConversionException)
							{
								syncCommandItem.Status = "6";
								list2.Add(syncCommandItem);
							}
						}
					}
					if (list2.Count > 0)
					{
						this.Responses.AddRange(list2);
					}
					if (list.Count > 0)
					{
						this.ClientCommands = list.ToArray();
					}
					disposeGuard.Success();
				}
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002F8F0 File Offset: 0x0002DAF0
		protected void CheckFullAccess()
		{
			if (this.Permissions != SyncPermissions.FullAccess)
			{
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.AccessDenied, null, false)
				{
					ErrorStringForProtocolLogger = "AccessDeniedInSync"
				};
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002F924 File Offset: 0x0002DB24
		protected FolderSyncStateMetadata GetFolderSyncStateMetadata()
		{
			if (this.syncState != null)
			{
				return this.syncState.SyncStateMetadata as FolderSyncStateMetadata;
			}
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				return null;
			}
			UserSyncStateMetadata userSyncStateMetadata = UserSyncStateMetadataCache.Singleton.Get(mailboxSession, this.Context);
			DeviceSyncStateMetadata device = userSyncStateMetadata.GetDevice(mailboxSession, this.Context.Request.DeviceIdentity, this.Context);
			if (this.CollectionId != null)
			{
				return device.GetSyncState(mailboxSession, this.CollectionId, this.Context) as FolderSyncStateMetadata;
			}
			StoreObjectId ipmfolderIdFromClassType = this.GetIPMFolderIdFromClassType();
			FolderSyncStateMetadata result = null;
			if (!device.SyncStatesByIPMFolderId.TryGetValue(ipmfolderIdFromClassType, out result))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.GetFolderSyncStateMetadata] Id: {0}, Did not find sync state metadata for Mailbox: {1}, Device: {2}, class type: {3}, IPM FolderId: {4}", new object[]
				{
					this.InternalName,
					mailboxSession.MailboxOwner.MailboxInfo.DisplayName,
					device.Id,
					this.ClassType,
					ipmfolderIdFromClassType
				});
				return null;
			}
			return result;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002FA20 File Offset: 0x0002DC20
		private StoreObjectId GetIPMFolderIdFromClassType()
		{
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			string a;
			if ((a = this.ClassType) != null)
			{
				StoreObjectId defaultFolderId;
				if (!(a == "Calendar"))
				{
					if (!(a == "Email"))
					{
						if (!(a == "Contacts"))
						{
							if (!(a == "Tasks"))
							{
								goto IL_77;
							}
							defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Tasks);
						}
						else
						{
							defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Contacts);
						}
					}
					else
					{
						defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
					}
				}
				else
				{
					defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
				}
				return defaultFolderId;
			}
			IL_77:
			throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
			{
				ErrorStringForProtocolLogger = "BadClassTypeInSync"
			};
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		private string CreateApplicationDataNode(XmlDocument xmlResponse, ISyncItemId itemId, GlobalInfo globalInfo, ProtocolLogger protocolLogger, MailboxLogger mailboxLogger, out XmlNode applicationNode)
		{
			string result = "1";
			applicationNode = xmlResponse.CreateElement("ApplicationData", "AirSync:");
			try
			{
				using (ISyncItem syncItem = this.BindToSyncItem(itemId, true))
				{
					this.ConvertServerToClientObject(syncItem, applicationNode, null, globalInfo);
				}
			}
			catch (Exception ex)
			{
				Command.CurrentCommand.PartialFailure = true;
				if (ex is VirusScanInProgressException)
				{
					AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
					AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.ConversionTracer, this, "Virus scanning in progress exception was thrown. Location GenerateResponsesXmlNode.Fetch.\r\n{0}", arg);
					result = "5";
					applicationNode = null;
					if (mailboxLogger != null)
					{
						mailboxLogger.SetData(MailboxLogDataName.SyncCommand_GenerateResponsesXmlNode_AddChange_Exception, ex);
					}
				}
				else if (SyncCommand.IsObjectNotFound(ex))
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.ConversionTracer, this, "ObjectNotFoundException was thrown. Location GenerateResponsesXmlNode.Fetch.\r\n");
					result = "8";
					applicationNode = null;
					if (mailboxLogger != null)
					{
						mailboxLogger.SetData(MailboxLogDataName.SyncCommand_GenerateResponsesXmlNode_AddChange_Exception, ex);
					}
				}
				else
				{
					if (!SyncCommand.IsItemSyncTolerableException(ex))
					{
						throw;
					}
					AirSyncUtility.ExceptionToStringHelper arg2 = new AirSyncUtility.ExceptionToStringHelper(ex);
					AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.ConversionTracer, this, "Sync-tolerable Item conversion Exception was thrown. Location GenerateResponsesXmlNode.Fetch.\r\n{0}", arg2);
					result = "6";
					applicationNode = null;
					if (mailboxLogger != null)
					{
						mailboxLogger.SetData(MailboxLogDataName.SyncCommand_GenerateResponsesXmlNode_AddChange_Exception, ex);
					}
					protocolLogger.IncrementValue(this.InternalName, PerFolderProtocolLoggerData.ClientFailedToConvert);
				}
			}
			return result;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002FBF8 File Offset: 0x0002DDF8
		private static void PostProcessExceptions(XmlNode masterNode)
		{
			XmlNode xmlNode = null;
			Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
			foreach (object obj in masterNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string name = xmlNode2.Name;
				if (name == "Exceptions")
				{
					xmlNode = xmlNode2;
				}
				else if (name == "DtStamp" || name == "StartTime" || name == "Subject" || name == "OrganizerName" || name == "OrganizerEmail" || name == "EndTime" || name == "Body" || name == "Reminder" || name == "Categories" || name == "Sensitivity" || name == "ExceptionStartTime" || name == "AllDayEvent" || name == "BusyStatus" || name == "Attendees" || name == "AppointmentReplyTime" || name == "ResponseType")
				{
					int xmlNodeIdentity = SyncCollection.GetXmlNodeIdentity(xmlNode2);
					dictionary[name] = xmlNodeIdentity;
				}
			}
			if (xmlNode != null)
			{
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode3 = (XmlNode)obj2;
					List<XmlNode> list = new List<XmlNode>(xmlNode3.ChildNodes.Count);
					foreach (object obj3 in xmlNode3.ChildNodes)
					{
						XmlNode xmlNode4 = (XmlNode)obj3;
						string name = xmlNode4.Name;
						int xmlNodeIdentity2 = SyncCollection.GetXmlNodeIdentity(xmlNode4);
						int num;
						if (string.Equals(name, "AppointmentReplyTime") && xmlNodeIdentity2 == 0)
						{
							list.Add(xmlNode4);
						}
						else if (dictionary.TryGetValue(name, out num) && num == xmlNodeIdentity2)
						{
							list.Add(xmlNode4);
						}
					}
					foreach (XmlNode oldChild in list)
					{
						xmlNode3.RemoveChild(oldChild);
					}
					if (!xmlNode3.HasChildNodes)
					{
						xmlNode.RemoveChild(xmlNode3);
					}
				}
				if (!xmlNode.HasChildNodes)
				{
					masterNode.RemoveChild(xmlNode);
				}
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002FF00 File Offset: 0x0002E100
		private static void PostProcessAllDayEventNodes(XmlNode masterNode)
		{
			XmlNode xmlNode = null;
			SyncCollection.ProcessAllDayEventNode(masterNode);
			foreach (object obj in masterNode)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.Name == "Exceptions")
				{
					xmlNode = xmlNode2;
					break;
				}
			}
			if (xmlNode != null)
			{
				foreach (object obj2 in xmlNode)
				{
					XmlNode masterNode2 = (XmlNode)obj2;
					SyncCollection.ProcessAllDayEventNode(masterNode2);
				}
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		private static void ProcessAllDayEventNode(XmlNode masterNode)
		{
			bool flag = false;
			bool flag2 = false;
			ExDateTime minValue = ExDateTime.MinValue;
			ExDateTime minValue2 = ExDateTime.MinValue;
			XmlNode xmlNode = null;
			int num = 0;
			foreach (object obj in masterNode)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string name;
				if ((name = xmlNode2.Name) != null)
				{
					if (!(name == "StartTime"))
					{
						if (!(name == "EndTime"))
						{
							if (name == "AllDayEvent")
							{
								if (int.TryParse(xmlNode2.InnerText, out num) && num == 1)
								{
									xmlNode = xmlNode2;
								}
							}
						}
						else
						{
							flag2 = ExDateTime.TryParseExact(xmlNode2.InnerText, "yyyyMMdd\\THHmmss\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out minValue2);
						}
					}
					else
					{
						flag = ExDateTime.TryParseExact(xmlNode2.InnerText, "yyyyMMdd\\THHmmss\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out minValue);
					}
				}
			}
			if (flag && flag2 && xmlNode != null && (minValue2.Hour != minValue.Hour || minValue2.Minute != minValue.Minute))
			{
				xmlNode.InnerText = "0";
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000300EC File Offset: 0x0002E2EC
		private static int GetXmlNodeIdentity(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				int num = 0;
				foreach (object obj in node.ChildNodes)
				{
					XmlNode node2 = (XmlNode)obj;
					num ^= SyncCollection.GetXmlNodeIdentity(node2);
				}
				return num;
			}
			AirSyncBlobXmlNode airSyncBlobXmlNode = node as AirSyncBlobXmlNode;
			if (airSyncBlobXmlNode != null)
			{
				return airSyncBlobXmlNode.GetHashCode();
			}
			string innerText = node.InnerText;
			if (string.IsNullOrEmpty(innerText))
			{
				return 0;
			}
			return innerText.GetHashCode();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00030184 File Offset: 0x0002E384
		private static AirSyncV25FilterTypes ParseFilterTypeString(string filterType)
		{
			int num;
			if (!int.TryParse(filterType, out num) || num > 8 || num < 0)
			{
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "InvalidFilterOnSync"
				};
			}
			return (AirSyncV25FilterTypes)num;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000301B8 File Offset: 0x0002E3B8
		private static bool ClassSupportsFilterType(AirSyncV25FilterTypes filterType, string classType)
		{
			if (classType != null)
			{
				if (!(classType == "Calendar"))
				{
					if (!(classType == "Email") && !(classType == "SMS"))
					{
						if (classType == "Tasks")
						{
							if (filterType != AirSyncV25FilterTypes.NoFilter && filterType != AirSyncV25FilterTypes.IncompleteFilter)
							{
								return false;
							}
						}
					}
					else if (filterType >= AirSyncV25FilterTypes.ThreeMonthFilter)
					{
						return false;
					}
				}
				else if (filterType >= AirSyncV25FilterTypes.OneDayFilter && filterType <= AirSyncV25FilterTypes.OneWeekFilter)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0003021A File Offset: 0x0002E41A
		private void AddClassTypeValidation(object state, Action<object> validationDelegate)
		{
			this.classTypeValidations.Add(new KeyValuePair<object, Action<object>>(state, validationDelegate));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00030230 File Offset: 0x0002E430
		private void TrackCalendarChanges(StoreObjectId itemId)
		{
			if (itemId.ObjectType == StoreObjectType.CalendarItem)
			{
				try
				{
					using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(this.storeSession, itemId))
					{
						Command.CurrentCommand.LoadMeetingOrganizerSyncState();
						if (Command.CurrentCommand.MeetingOrganizerSyncState != null && Command.CurrentCommand.MeetingOrganizerSyncState.MeetingOrganizerInfo != null)
						{
							Command.CurrentCommand.MeetingOrganizerSyncState.MeetingOrganizerInfo.Add(calendarItemBase);
						}
						else
						{
							Command.CurrentCommand.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, string.Format("Null{0}.", (Command.CurrentCommand.MeetingOrganizerSyncState == null) ? "MeetingOrgSyncState" : "MeetingOrgInfo"));
							AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.TrackCalendarChanges] Id: {0}, MeetingOrganizerSyncState is {1}.", this.InternalName, (Command.CurrentCommand.MeetingOrganizerSyncState == null) ? "<null>" : "<NotNull>");
						}
					}
				}
				catch (ObjectNotFoundException arg)
				{
					Command.CurrentCommand.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, string.Format("NoCalTracking+ID:{0}", itemId));
					AirSyncDiagnostics.TraceError<string, StoreObjectId, ObjectNotFoundException>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.TrackCalendarChanges] Id: {0}, Failed to bind to calendar item: {1}, Exception: {2}", this.InternalName, itemId, arg);
				}
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00030358 File Offset: 0x0002E558
		private void ValidateSupportTag(object state)
		{
			XmlNode xmlNode = state as XmlNode;
			string a;
			if ((a = this.ClassType) != null)
			{
				if (!(a == "Contacts"))
				{
					if (!(a == "Calendar"))
					{
						if (!(a == "Tasks"))
						{
							goto IL_CA;
						}
						if (xmlNode.NamespaceURI != "Tasks:")
						{
							throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
							{
								ErrorStringForProtocolLogger = "SupportedTasksErrorOnSync"
							};
						}
					}
					else if (xmlNode.NamespaceURI != "Calendar:")
					{
						throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
						{
							ErrorStringForProtocolLogger = "SupportedCalendarErrorOnSync"
						};
					}
				}
				else if (xmlNode.NamespaceURI != "Contacts:" && xmlNode.NamespaceURI != "Contacts2:")
				{
					throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
					{
						ErrorStringForProtocolLogger = "SupportedContactsErrorOnSync"
					};
				}
				return;
			}
			IL_CA:
			throw new AirSyncPermanentException(StatusCode.Sync_ProtocolError, false)
			{
				ErrorStringForProtocolLogger = "SupportedTagErrorOnSync"
			};
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0003049C File Offset: 0x0002E69C
		private void ModifyFetchTruncationOption(IAirSyncVersionFactory versionFactory)
		{
			IDictionary dictionary = null;
			if (this.protocolVersion > 25)
			{
				dictionary = new PropertyCollection();
				dictionary["BodyPreference"] = (from bodyPref in this.MailboxSchemaOptions.BodyPreferences
				select new BodyPreference
				{
					Type = bodyPref.Type,
					Preview = bodyPref.Preview
				}).ToList<BodyPreference>();
				dictionary["BodyPartPreference"] = (from bodyPartPref in this.MailboxSchemaOptions.BodyPartPreferences
				select new BodyPartPreference
				{
					Type = bodyPartPref.Type,
					Preview = bodyPartPref.Preview
				}).ToList<BodyPartPreference>();
			}
			if (this.MailboxSchemaOptions.MIMESupport != MIMESupportValue.NeverSendMimeData)
			{
				if (dictionary == null)
				{
					dictionary = new PropertyCollection();
				}
				dictionary["MIMESupport"] = this.MailboxSchemaOptions.MIMESupport;
			}
			if (dictionary != null)
			{
				this.SetSchemaConverterOptions(dictionary, versionFactory);
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00030574 File Offset: 0x0002E774
		private void CreateTruncationSizeZeroAirSyncDataObject(string deviceType, IAirSyncVersionFactory versionFactory)
		{
			IDictionary dictionary = this.MailboxSchemaOptions.BuildOptionsCollection(deviceType);
			if (dictionary != null)
			{
				List<BodyPreference> list = (List<BodyPreference>)dictionary["BodyPreference"];
				List<BodyPreference> list2 = new List<BodyPreference>(list.Count);
				foreach (BodyPreference bodyPreference in list)
				{
					BodyPreference item = bodyPreference.Clone();
					list2.Add(item);
				}
				for (int i = 0; i < list2.Count; i++)
				{
					list2[i].TruncationSize = 0L;
				}
				dictionary["BodyPreference"] = list2;
			}
			IAirSyncMissingPropertyStrategy missingPropertyStrategy = versionFactory.CreateMissingPropertyStrategy(this.supportedTags);
			AirSyncXsoSchemaState airSyncXsoSchemaState = (AirSyncXsoSchemaState)this.SchemaState;
			this.TruncationSizeZeroAirSyncDataObject = airSyncXsoSchemaState.GetAirSyncDataObject(dictionary, missingPropertyStrategy);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00030658 File Offset: 0x0002E858
		private void SetCalendarFilterType(SyncCollection.Options options)
		{
			if (options.FilterType == AirSyncV25FilterTypes.NoFilter)
			{
				this.FolderSync.SetSyncFilters(new DateTimeCustomSyncFilter(ExDateTime.MinValue, this.SyncState), new ISyncFilter[]
				{
					new DateTimeCustomSyncFilter(this.SyncState)
				});
				return;
			}
			this.FolderSync.SetSyncFilters(new DateTimeCustomSyncFilter(this.GetBeginDate(options.FilterType), this.SyncState), new ISyncFilter[0]);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000306C8 File Offset: 0x0002E8C8
		private QueryFilter BuildRestrictiveFilter(AirSyncV25FilterTypes filterType)
		{
			switch (filterType)
			{
			case AirSyncV25FilterTypes.InvalidFilter:
				throw new InvalidOperationException();
			case AirSyncV25FilterTypes.NoFilter:
				return null;
			default:
				if (filterType != AirSyncV25FilterTypes.IncompleteFilter)
				{
					return new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ReceivedTime, this.GetBeginDate(filterType));
				}
				return new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IsComplete, false);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00030720 File Offset: 0x0002E920
		private QueryFilter BuildLeastRestrictiveFilter()
		{
			AirSyncV25FilterTypes airSyncV25FilterTypes = AirSyncV25FilterTypes.NoFilter;
			foreach (SyncCollection.Options options in this.optionsList)
			{
				AirSyncV25FilterTypes airSyncV25FilterTypes2 = options.FilterType;
				if (airSyncV25FilterTypes2 == AirSyncV25FilterTypes.NoFilter)
				{
					return null;
				}
				if (airSyncV25FilterTypes2 == AirSyncV25FilterTypes.IncompleteFilter)
				{
					return this.BuildRestrictiveFilter(AirSyncV25FilterTypes.IncompleteFilter);
				}
				if (options.FilterType > airSyncV25FilterTypes)
				{
					airSyncV25FilterTypes = options.FilterType;
				}
			}
			return this.BuildRestrictiveFilter(airSyncV25FilterTypes);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000307AC File Offset: 0x0002E9AC
		private string GetFilterId(bool isQuarantineMailAvailable)
		{
			StringBuilder stringBuilder = new StringBuilder(16);
			foreach (SyncCollection.Options options in this.optionsList)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(options.Class);
				string @class;
				switch (@class = options.Class)
				{
				case "Email":
					stringBuilder.AppendFormat(":{0}:", (int)options.FilterType);
					if (options.FilterType != AirSyncV25FilterTypes.NoFilter)
					{
						stringBuilder.AppendFormat("{0}", this.GetBeginDate(options.FilterType).ToString("yyyyMMdd", CultureInfo.InvariantCulture));
					}
					if (isQuarantineMailAvailable)
					{
						stringBuilder.Append("Quarantine");
						continue;
					}
					continue;
				case "SMS":
					stringBuilder.AppendFormat(":{0}:", (int)options.FilterType);
					if (options.FilterType != AirSyncV25FilterTypes.NoFilter)
					{
						stringBuilder.AppendFormat("{0}", this.GetBeginDate(options.FilterType).ToString("yyyyMMdd", CultureInfo.InvariantCulture));
					}
					if (this.folderType == DefaultFolderType.Outbox)
					{
						string arg = string.Empty;
						E164Number e164Number;
						if (this.deviceEnableOutboundSMS && E164Number.TryParse(this.devicePhoneNumberForSms, out e164Number))
						{
							arg = e164Number.Number;
						}
						stringBuilder.AppendFormat(":{0}:{1}", this.deviceEnableOutboundSMS, arg);
						continue;
					}
					continue;
				case "Calendar":
					stringBuilder.AppendFormat(":{0}", (int)options.FilterType);
					continue;
				case "Tasks":
					if (options.FilterType == AirSyncV25FilterTypes.IncompleteFilter)
					{
						stringBuilder.Append(":I");
						continue;
					}
					continue;
				case "Contacts":
				case "Notes":
				case "RecipientInfoCache":
					continue;
				}
				throw new AirSyncPermanentException(HttpStatusCode.NotImplemented, StatusCode.UnexpectedItemClass, null, false)
				{
					ErrorStringForProtocolLogger = "BadClassWithFilterGetOnSync"
				};
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00030A34 File Offset: 0x0002EC34
		private ExDateTime GetBeginDate(AirSyncV25FilterTypes filterType)
		{
			ExDateTime result;
			switch (filterType)
			{
			case AirSyncV25FilterTypes.NoFilter:
				result = ExDateTime.MinValue;
				break;
			case AirSyncV25FilterTypes.OneDayFilter:
				result = this.today.AddDays(-1.0);
				break;
			case AirSyncV25FilterTypes.ThreeDayFilter:
				result = this.today.AddDays(-3.0);
				break;
			case AirSyncV25FilterTypes.OneWeekFilter:
				result = this.today.AddDays(-7.0);
				break;
			case AirSyncV25FilterTypes.TwoWeekFilter:
				result = this.today.AddDays(-14.0);
				break;
			case AirSyncV25FilterTypes.OneMonthFilter:
				result = this.today.AddMonths(-1);
				break;
			case AirSyncV25FilterTypes.ThreeMonthFilter:
				result = this.today.AddMonths(-3);
				break;
			case AirSyncV25FilterTypes.SixMonthFilter:
				result = this.today.AddMonths(-6);
				break;
			case AirSyncV25FilterTypes.IncompleteFilter:
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "FilterTypeMismatchInSync"
				};
			default:
				this.Status = SyncBase.ErrorCodeStatus.ProtocolError;
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "BadFilterTypeInSync"
				};
			}
			return result;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00030B40 File Offset: 0x0002ED40
		private void ParseCollection(List<XmlNode> itemLevelProtocolErrorNodes, XmlNode collectionNode)
		{
			this.optionsList = new List<SyncCollection.Options>(2);
			List<XmlNode> list = new List<XmlNode>(2);
			foreach (object obj in collectionNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				switch (localName = xmlNode.LocalName)
				{
				case "Class":
					this.ClassType = xmlNode.InnerText;
					continue;
				case "SyncKey":
					this.SyncKeyString = xmlNode.InnerText;
					if (this.protocolVersion >= 121 && this.SyncKeyString != null && !this.SyncKeyString.Equals("0"))
					{
						this.DeletesAsMoves = true;
						this.GetChanges = true;
						continue;
					}
					continue;
				case "NotifyGUID":
					continue;
				case "Supported":
					this.ParseSupportedTags(xmlNode);
					continue;
				case "CollectionId":
					if (xmlNode.InnerText.Trim() == string.Empty)
					{
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "[SyncCollection.ParseCollection] CollectionId passed in was EMPTY.  Actual text: '{0}'", xmlNode.InnerText);
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "EmptyCollectionId"
						};
					}
					this.CollectionId = xmlNode.InnerText;
					continue;
				case "DeletesAsMoves":
					if (this.protocolVersion < 121 || string.IsNullOrEmpty(xmlNode.InnerText))
					{
						this.DeletesAsMoves = true;
						continue;
					}
					if (xmlNode.InnerText.Equals("1"))
					{
						this.DeletesAsMoves = true;
						continue;
					}
					if (xmlNode.InnerText.Equals("0"))
					{
						this.DeletesAsMoves = false;
						continue;
					}
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "InvalidDeletesAsMoveInSync"
					};
				case "GetChanges":
					if (this.protocolVersion < 121 || string.IsNullOrEmpty(xmlNode.InnerText))
					{
						this.GetChanges = true;
						continue;
					}
					if (xmlNode.InnerText.Equals("1"))
					{
						this.GetChanges = true;
						continue;
					}
					if (xmlNode.InnerText.Equals("0"))
					{
						this.GetChanges = false;
						continue;
					}
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "InvalidGetChangesInSync"
					};
				case "WindowSize":
				{
					uint num2;
					if (!uint.TryParse(xmlNode.InnerText, out num2))
					{
						throw new AirSyncPermanentException(false)
						{
							ErrorStringForProtocolLogger = "InvalidWindowSize"
						};
					}
					if (num2 == 0U || num2 > 512U)
					{
						num2 = 512U;
					}
					this.WindowSize = (int)num2;
					continue;
				}
				case "Options":
					if (this.optionsList.Count > 0 && this.protocolVersion < 140)
					{
						AirSyncPermanentException ex = new AirSyncPermanentException(false);
						throw ex;
					}
					this.ParseOptionsNode(xmlNode);
					list.Add(xmlNode);
					continue;
				case "Commands":
					this.CommandRequestXmlNode = xmlNode;
					this.ParseClientCommands(itemLevelProtocolErrorNodes);
					list.Add(xmlNode);
					continue;
				case "ConversationMode":
					if (string.IsNullOrEmpty(xmlNode.InnerText))
					{
						this.ConversationMode = true;
						continue;
					}
					if (xmlNode.InnerText.Equals("1"))
					{
						this.ConversationMode = true;
						continue;
					}
					if (xmlNode.InnerText.Equals("0"))
					{
						this.ConversationMode = false;
						continue;
					}
					throw new AirSyncPermanentException(false)
					{
						ErrorStringForProtocolLogger = "InvalidConversationMode(" + xmlNode.InnerText + ")"
					};
				}
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidXML, null, false)
				{
					ErrorStringForProtocolLogger = "InvalidNode(" + xmlNode.InnerText + ")inCollectionSync"
				};
			}
			if (this.optionsList.Count > 1 && this.HasMaxItemsNode)
			{
				throw new AirSyncPermanentException(false)
				{
					ErrorStringForProtocolLogger = "DupeOptionsNodeInCollectionSync"
				};
			}
			foreach (XmlNode oldChild in list)
			{
				collectionNode.RemoveChild(oldChild);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00031004 File Offset: 0x0002F204
		private void ParseOptionsNode(XmlNode node)
		{
			SyncCollection.Options options = new SyncCollection.Options(node);
			this.optionsList.Add(options);
			XmlNode xmlNode = null;
			using (XmlNodeList childNodes = node.ChildNodes)
			{
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					string localName;
					if ((localName = xmlNode2.LocalName) != null)
					{
						if (!(localName == "Class"))
						{
							if (!(localName == "Conflict"))
							{
								if (!(localName == "FilterType"))
								{
									if (!(localName == "MaxItems"))
									{
										if (localName == "Annotations")
										{
											xmlNode = xmlNode2;
										}
									}
									else
									{
										int num;
										if (!int.TryParse(xmlNode2.InnerText, out num) || num < 1)
										{
											throw new AirSyncPermanentException(false)
											{
												ErrorStringForProtocolLogger = "InvalidMaxItemsNode"
											};
										}
										this.MaxItems = num;
										this.HasMaxItemsNode = true;
									}
								}
								else
								{
									options.FilterType = SyncCollection.ParseFilterTypeString(xmlNode2.InnerText);
								}
							}
							else
							{
								this.ParseConflictResolutionPolicy(xmlNode2);
							}
						}
						else
						{
							options.Class = xmlNode2.InnerText;
							options.ParsedClassNode = true;
						}
					}
				}
				if (xmlNode != null)
				{
					this.RequestAnnotations.ParseWLAnnotations(xmlNode, this.collectionId, options.Class);
				}
			}
			if (options.Class == null)
			{
				options.Class = this.ClassType;
			}
			options.MailboxSchemaOptions.Parse(node);
			Command.CurrentCommand.ProtocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.BodyRequested, options.MailboxSchemaOptions.HasBodyPreferences ? 1 : 0);
			Command.CurrentCommand.ProtocolLogger.SetValue(this.InternalName, PerFolderProtocolLoggerData.BodyPartRequested, options.MailboxSchemaOptions.HasBodyPartPreferences ? 1 : 0);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00031200 File Offset: 0x0002F400
		private void ParseConflictResolutionPolicy(XmlNode conflictResolutionPolicyNode)
		{
			string innerText;
			if ((innerText = conflictResolutionPolicyNode.InnerText) != null)
			{
				if (!(innerText == "0"))
				{
					if (!(innerText == "1"))
					{
						goto IL_38;
					}
					this.ConflictResolutionPolicy = ConflictResolutionPolicy.ServerWins;
				}
				else
				{
					this.ConflictResolutionPolicy = ConflictResolutionPolicy.ClientWins;
				}
				this.ClientConflictResolutionPolicy = this.ConflictResolutionPolicy;
				if (this.ClassType == "Email")
				{
					this.ConflictResolutionPolicy = ConflictResolutionPolicy.ServerWins;
				}
				return;
			}
			IL_38:
			throw new AirSyncPermanentException(false)
			{
				ErrorStringForProtocolLogger = "InvalidConflictResolutionNode"
			};
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00031280 File Offset: 0x0002F480
		private void CreateSmsSearchFolderIfNeeded(GlobalInfo globalInfo)
		{
			if (globalInfo == null)
			{
				return;
			}
			if (globalInfo.SmsSearchFolderCreated)
			{
				AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CreateSmsSearchFolderIdNeeded] Id: {0}, SMS search folder already created.", this.InternalName);
				return;
			}
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				throw new InvalidOperationException("CreateSmsSearchFolderIfNeeded(): storeSession is not a MailboxSession!");
			}
			try
			{
				string text = Strings.SmsSearchFolder.ToString(mailboxSession.PreferedCulture);
				AirSyncDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CreateSmsSearchFolderIfNeeded] Id: {0}, Creating new SMS search folder with displayName: '{1}' for MailboxCulture:{2}...", this.InternalName, text, (mailboxSession.PreferedCulture == null) ? "<null>" : mailboxSession.PreferedCulture.Name);
				if (string.IsNullOrEmpty(text))
				{
					text = Strings.SmsSearchFolder.ToString(CultureInfo.CurrentCulture);
					AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CreateSmsSearchFolderIfNeeded] Id: {0}, search folderDisplayName is empty. Default to english name.{1}", this.InternalName, text);
					Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, string.Format("EmptySrchFolderName:{0}", (mailboxSession.PreferedCulture == null) ? "<null>" : mailboxSession.PreferedCulture.LCID.ToString()));
					if (string.IsNullOrEmpty(text))
					{
						text = Strings.SmsSearchFolder.ToString(CultureInfo.GetCultureInfo("en-US"));
					}
					if (string.IsNullOrEmpty(text))
					{
						throw new InvalidOperationException("CreateSmsSearchFolderIfNeeded(): searchFolderDisplayName does not have a valid value.");
					}
				}
				using (OutlookSearchFolder outlookSearchFolder = OutlookSearchFolder.Create(mailboxSession, text))
				{
					FolderSaveResult folderSaveResult = outlookSearchFolder.Save();
					if (folderSaveResult.OperationResult != OperationResult.Succeeded)
					{
						AirSyncDiagnostics.TraceDebug<string, FolderSaveResult>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CreateSmsSearchFolderIfNeeded] Id: {0}, Fail to save SMS search folder. Error: {1}", this.InternalName, folderSaveResult);
					}
					else
					{
						outlookSearchFolder.Load();
						outlookSearchFolder.MakeVisibleToOutlook(true, new SearchFolderCriteria(SmsPrototypeSchemaState.SupportedClassQueryFilter, new StoreId[]
						{
							mailboxSession.GetDefaultFolderId(DefaultFolderType.Root)
						})
						{
							DeepTraversal = true
						});
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CreateSmsSearchFolderIfNeeded] Id: {0}, Created search criteria on SMS search folder", this.InternalName);
						globalInfo.SmsSearchFolderCreated = true;
					}
				}
			}
			catch (ObjectExistedException ex)
			{
				AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, this, "[SyncCollection.CreateSmsSearchFolderIfNeeded] Id: {0}, Failed trying to create SMS search folder. Error: {1}", this.InternalName, ex.ToString());
			}
		}

		// Token: 0x0400050F RID: 1295
		private const int AirSyncLastSyncTimeIndex = 0;

		// Token: 0x04000510 RID: 1296
		private const int AirSyncLocalCommitTimeIndex = 1;

		// Token: 0x04000511 RID: 1297
		private const int AirSyncDeletedCountTotalIndex = 2;

		// Token: 0x04000512 RID: 1298
		private const int AirSyncSyncKeyIndex = 3;

		// Token: 0x04000513 RID: 1299
		private const int AirSyncFilterIndex = 4;

		// Token: 0x04000514 RID: 1300
		private const int AirSyncConversationModeIndex = 5;

		// Token: 0x04000515 RID: 1301
		private const int AirSyncSettingsHashIndex = 6;

		// Token: 0x04000516 RID: 1302
		private const int EasPropertyGroupMask = 1012222;

		// Token: 0x04000517 RID: 1303
		private static readonly PropertyDefinition[] propertiesToSaveForNullSync = new PropertyDefinition[]
		{
			AirSyncStateSchema.MetadataLastSyncTime,
			AirSyncStateSchema.MetadataLocalCommitTimeMax,
			AirSyncStateSchema.MetadataDeletedCountTotal,
			AirSyncStateSchema.MetadataSyncKey,
			AirSyncStateSchema.MetadataFilter,
			AirSyncStateSchema.MetadataConversationMode,
			AirSyncStateSchema.MetadataSettingsHash
		};

		// Token: 0x04000518 RID: 1304
		private static readonly QueryFilter IcsPropertyGroupFilter = SyncCollection.BuildIcsPropertyGroupFilter();

		// Token: 0x04000519 RID: 1305
		private static readonly PropertyDefinition[] propertyTextMessageDeliveryStatus = new PropertyDefinition[]
		{
			MessageItemSchema.TextMessageDeliveryStatus
		};

		// Token: 0x0400051A RID: 1306
		private static readonly object[] propertyValueTextMessageDeliveryStatus = new object[]
		{
			50
		};

		// Token: 0x0400051B RID: 1307
		internal static readonly PropertyDefinition[] ReadFlagChangedOnly = new PropertyDefinition[]
		{
			MessageItemSchema.IsRead
		};

		// Token: 0x0400051C RID: 1308
		private static readonly ConcurrentDictionary<string, int> perCollectionData = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400051D RID: 1309
		private static readonly PropertyCollection emptyPropertyCollection = new PropertyCollection();

		// Token: 0x0400051E RID: 1310
		private static readonly FalseFilter falseFilterInstance = new FalseFilter();

		// Token: 0x0400051F RID: 1311
		private List<KeyValuePair<object, Action<object>>> classTypeValidations = new List<KeyValuePair<object, Action<object>>>();

		// Token: 0x04000520 RID: 1312
		private ExDateTime today = ExDateTime.Today;

		// Token: 0x04000521 RID: 1313
		private bool allowRecovery = true;

		// Token: 0x04000522 RID: 1314
		private AirSyncV25FilterTypes filterType;

		// Token: 0x04000523 RID: 1315
		private ISyncProviderFactory syncProviderFactory;

		// Token: 0x04000524 RID: 1316
		private string classType;

		// Token: 0x04000525 RID: 1317
		private FolderSyncState syncState;

		// Token: 0x04000526 RID: 1318
		private FolderSync folderSync;

		// Token: 0x04000527 RID: 1319
		private uint syncKey;

		// Token: 0x04000528 RID: 1320
		private uint recoverySyncKey;

		// Token: 0x04000529 RID: 1321
		private string syncType;

		// Token: 0x0400052A RID: 1322
		private string syncKeyString;

		// Token: 0x0400052B RID: 1323
		private uint responseSyncKey;

		// Token: 0x0400052C RID: 1324
		private string collectionId;

		// Token: 0x0400052D RID: 1325
		private bool returnCollectionId = true;

		// Token: 0x0400052E RID: 1326
		private int windowSize;

		// Token: 0x0400052F RID: 1327
		private ConflictResolutionPolicy clientConflictResolutionPolicy = ConflictResolutionPolicy.ServerWins;

		// Token: 0x04000530 RID: 1328
		private bool moreAvailable;

		// Token: 0x04000531 RID: 1329
		private SyncOperations serverChanges;

		// Token: 0x04000532 RID: 1330
		private SyncBase.ErrorCodeStatus status = SyncBase.ErrorCodeStatus.Success;

		// Token: 0x04000533 RID: 1331
		private bool hasFilterNode;

		// Token: 0x04000534 RID: 1332
		private Dictionary<string, bool> supportedTags;

		// Token: 0x04000535 RID: 1333
		private bool deletesAsMoves;

		// Token: 0x04000536 RID: 1334
		private bool getChanges;

		// Token: 0x04000537 RID: 1335
		private SyncCommandItem[] clientCommands;

		// Token: 0x04000538 RID: 1336
		private Dictionary<ISyncItemId, SyncCommandItem> clientFetchedItems = new Dictionary<ISyncItemId, SyncCommandItem>();

		// Token: 0x04000539 RID: 1337
		private XmlNode commandRequestXmlNode;

		// Token: 0x0400053A RID: 1338
		private XmlNode commandResponseXmlNode;

		// Token: 0x0400053B RID: 1339
		private XmlNode responsesResponseXmlNode;

		// Token: 0x0400053C RID: 1340
		private XmlNode collectionResponseXmlNode;

		// Token: 0x0400053D RID: 1341
		private XmlNode collectionNode;

		// Token: 0x0400053E RID: 1342
		private List<SyncCommandItem> responses = new List<SyncCommandItem>();

		// Token: 0x0400053F RID: 1343
		private List<SyncCommandItem> dupeList = new List<SyncCommandItem>();

		// Token: 0x04000540 RID: 1344
		private int dupeId = 1;

		// Token: 0x04000541 RID: 1345
		private bool dupesFilledWindowSize;

		// Token: 0x04000542 RID: 1346
		private bool hasAddsOrChangesToReturnToClientImmediately;

		// Token: 0x04000543 RID: 1347
		private bool hasServerChanges;

		// Token: 0x04000544 RID: 1348
		private bool haveChanges;

		// Token: 0x04000545 RID: 1349
		private bool hasBeenSaved;

		// Token: 0x04000546 RID: 1350
		private AirSyncV25FilterTypes filterTypeInSyncState;

		// Token: 0x04000547 RID: 1351
		private bool optionsSentAreDifferentForV121AndLater;

		// Token: 0x04000548 RID: 1352
		private StoreSession storeSession;

		// Token: 0x04000549 RID: 1353
		private Folder mailboxFolder;

		// Token: 0x0400054A RID: 1354
		private int protocolVersion;

		// Token: 0x0400054B RID: 1355
		private ExDateTime lastSyncTime = ExDateTime.Now;

		// Token: 0x0400054C RID: 1356
		private int maxItems = int.MaxValue;

		// Token: 0x0400054D RID: 1357
		private bool conversationMode;

		// Token: 0x0400054E RID: 1358
		private bool conversationModeInSyncState;

		// Token: 0x0400054F RID: 1359
		private bool nullSyncWorked;

		// Token: 0x04000550 RID: 1360
		private List<SyncCollection.Options> optionsList;

		// Token: 0x04000551 RID: 1361
		private int currentOptions;

		// Token: 0x04000552 RID: 1362
		private DefaultFolderType folderType;

		// Token: 0x04000553 RID: 1363
		private string devicePhoneNumberForSms;

		// Token: 0x04000554 RID: 1364
		private bool deviceEnableOutboundSMS;

		// Token: 0x04000555 RID: 1365
		private int deviceSettingsHash;

		// Token: 0x04000556 RID: 1366
		private StoreObjectId nativeStoreObjectId;

		// Token: 0x04000557 RID: 1367
		private ItemIdMapping itemIdMapping;

		// Token: 0x04000558 RID: 1368
		private bool? isIrmSupportFlag;

		// Token: 0x04000559 RID: 1369
		private bool isSendingABQMail;

		// Token: 0x02000090 RID: 144
		public enum CollectionTypes
		{
			// Token: 0x04000564 RID: 1380
			Mailbox,
			// Token: 0x04000565 RID: 1381
			RecipientInfoCache,
			// Token: 0x04000566 RID: 1382
			Unknown
		}

		// Token: 0x02000091 RID: 145
		private class Options
		{
			// Token: 0x06000841 RID: 2113 RVA: 0x00031568 File Offset: 0x0002F768
			internal Options(XmlNode node)
			{
				this.optionsNode = node;
			}

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000842 RID: 2114 RVA: 0x00031582 File Offset: 0x0002F782
			// (set) Token: 0x06000843 RID: 2115 RVA: 0x0003158A File Offset: 0x0002F78A
			internal string Class
			{
				get
				{
					return this.classType;
				}
				set
				{
					this.classType = value;
				}
			}

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000844 RID: 2116 RVA: 0x00031593 File Offset: 0x0002F793
			// (set) Token: 0x06000845 RID: 2117 RVA: 0x0003159B File Offset: 0x0002F79B
			internal AirSyncSchemaState SchemaState
			{
				get
				{
					return this.schemaState;
				}
				set
				{
					this.schemaState = value;
				}
			}

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000846 RID: 2118 RVA: 0x000315A4 File Offset: 0x0002F7A4
			// (set) Token: 0x06000847 RID: 2119 RVA: 0x000315AC File Offset: 0x0002F7AC
			internal AirSyncV25FilterTypes FilterType
			{
				get
				{
					return this.filterType;
				}
				set
				{
					this.filterType = value;
				}
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000848 RID: 2120 RVA: 0x000315B5 File Offset: 0x0002F7B5
			// (set) Token: 0x06000849 RID: 2121 RVA: 0x000315BD File Offset: 0x0002F7BD
			internal bool ParsedClassNode
			{
				get
				{
					return this.parsedClassNode;
				}
				set
				{
					this.parsedClassNode = value;
				}
			}

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x0600084A RID: 2122 RVA: 0x000315C6 File Offset: 0x0002F7C6
			// (set) Token: 0x0600084B RID: 2123 RVA: 0x000315CE File Offset: 0x0002F7CE
			internal XsoDataObject MailboxDataObject
			{
				get
				{
					return this.mailboxDataObject;
				}
				set
				{
					this.mailboxDataObject = value;
				}
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x0600084C RID: 2124 RVA: 0x000315D7 File Offset: 0x0002F7D7
			// (set) Token: 0x0600084D RID: 2125 RVA: 0x000315DF File Offset: 0x0002F7DF
			internal AirSyncDataObject AirSyncDataObject
			{
				get
				{
					return this.airSyncDataObject;
				}
				set
				{
					this.airSyncDataObject = value;
				}
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x0600084E RID: 2126 RVA: 0x000315E8 File Offset: 0x0002F7E8
			// (set) Token: 0x0600084F RID: 2127 RVA: 0x000315F0 File Offset: 0x0002F7F0
			internal IChangeTrackingFilter ChangeTrackingFilter
			{
				get
				{
					return this.changeTrackingFilter;
				}
				set
				{
					this.changeTrackingFilter = value;
				}
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000850 RID: 2128 RVA: 0x000315F9 File Offset: 0x0002F7F9
			internal XmlNode OptionsNode
			{
				get
				{
					return this.optionsNode;
				}
			}

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06000851 RID: 2129 RVA: 0x00031601 File Offset: 0x0002F801
			internal MailboxSchemaOptionsParser MailboxSchemaOptions
			{
				get
				{
					return this.mailboxSchemaOptions;
				}
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06000852 RID: 2130 RVA: 0x00031609 File Offset: 0x0002F809
			internal AirSyncXsoSchemaState AirSyncXsoSchemaState
			{
				get
				{
					return (AirSyncXsoSchemaState)this.SchemaState;
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06000853 RID: 2131 RVA: 0x00031616 File Offset: 0x0002F816
			// (set) Token: 0x06000854 RID: 2132 RVA: 0x0003161E File Offset: 0x0002F81E
			internal AirSyncDataObject ReadFlagAirSyncDataObject
			{
				get
				{
					return this.readFlagAirSyncDataObject;
				}
				set
				{
					this.readFlagAirSyncDataObject = value;
				}
			}

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06000855 RID: 2133 RVA: 0x00031627 File Offset: 0x0002F827
			// (set) Token: 0x06000856 RID: 2134 RVA: 0x0003162F File Offset: 0x0002F82F
			internal AirSyncDataObject TruncationSizeZeroAirSyncDataObject
			{
				get
				{
					return this.truncationSizeZeroAirSyncDataObject;
				}
				set
				{
					this.truncationSizeZeroAirSyncDataObject = value;
				}
			}

			// Token: 0x04000567 RID: 1383
			private readonly XmlNode optionsNode;

			// Token: 0x04000568 RID: 1384
			private MailboxSchemaOptionsParser mailboxSchemaOptions = new MailboxSchemaOptionsParser();

			// Token: 0x04000569 RID: 1385
			private AirSyncDataObject airSyncDataObject;

			// Token: 0x0400056A RID: 1386
			private AirSyncDataObject readFlagAirSyncDataObject;

			// Token: 0x0400056B RID: 1387
			private IChangeTrackingFilter changeTrackingFilter;

			// Token: 0x0400056C RID: 1388
			private XsoDataObject mailboxDataObject;

			// Token: 0x0400056D RID: 1389
			private AirSyncDataObject truncationSizeZeroAirSyncDataObject;

			// Token: 0x0400056E RID: 1390
			private string classType;

			// Token: 0x0400056F RID: 1391
			private AirSyncSchemaState schemaState;

			// Token: 0x04000570 RID: 1392
			private AirSyncV25FilterTypes filterType;

			// Token: 0x04000571 RID: 1393
			private bool parsedClassNode;
		}
	}
}
