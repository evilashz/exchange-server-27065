using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005B9 RID: 1465
	[DataServiceKey("objectId")]
	public class DirectoryObject
	{
		// Token: 0x0600163E RID: 5694 RVA: 0x0002DD98 File Offset: 0x0002BF98
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectoryObject CreateDirectoryObject(string objectId)
		{
			return new DirectoryObject
			{
				objectId = objectId
			};
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0002DDB3 File Offset: 0x0002BFB3
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x0002DDBB File Offset: 0x0002BFBB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string objectType
		{
			get
			{
				return this._objectType;
			}
			set
			{
				this._objectType = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0002DDC4 File Offset: 0x0002BFC4
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0002DDCC File Offset: 0x0002BFCC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string objectId
		{
			get
			{
				return this._objectId;
			}
			set
			{
				this._objectId = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0002DDD5 File Offset: 0x0002BFD5
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0002DDDD File Offset: 0x0002BFDD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DirectoryObject createdOnBehalfOf
		{
			get
			{
				return this._createdOnBehalfOf;
			}
			set
			{
				this._createdOnBehalfOf = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0002DDE6 File Offset: 0x0002BFE6
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x0002DDEE File Offset: 0x0002BFEE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> createdObjects
		{
			get
			{
				return this._createdObjects;
			}
			set
			{
				if (value != null)
				{
					this._createdObjects = value;
				}
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0002DDFA File Offset: 0x0002BFFA
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x0002DE02 File Offset: 0x0002C002
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DirectoryObject manager
		{
			get
			{
				return this._manager;
			}
			set
			{
				this._manager = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0002DE0B File Offset: 0x0002C00B
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x0002DE13 File Offset: 0x0002C013
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> directReports
		{
			get
			{
				return this._directReports;
			}
			set
			{
				if (value != null)
				{
					this._directReports = value;
				}
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0002DE1F File Offset: 0x0002C01F
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x0002DE27 File Offset: 0x0002C027
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != null)
				{
					this._members = value;
				}
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0002DE33 File Offset: 0x0002C033
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x0002DE3B File Offset: 0x0002C03B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> memberOf
		{
			get
			{
				return this._memberOf;
			}
			set
			{
				if (value != null)
				{
					this._memberOf = value;
				}
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0002DE47 File Offset: 0x0002C047
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0002DE4F File Offset: 0x0002C04F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> owners
		{
			get
			{
				return this._owners;
			}
			set
			{
				if (value != null)
				{
					this._owners = value;
				}
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0002DE5B File Offset: 0x0002C05B
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x0002DE63 File Offset: 0x0002C063
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> ownedObjects
		{
			get
			{
				return this._ownedObjects;
			}
			set
			{
				if (value != null)
				{
					this._ownedObjects = value;
				}
			}
		}

		// Token: 0x04001A15 RID: 6677
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectType;

		// Token: 0x04001A16 RID: 6678
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001A17 RID: 6679
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DirectoryObject _createdOnBehalfOf;

		// Token: 0x04001A18 RID: 6680
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _createdObjects = new Collection<DirectoryObject>();

		// Token: 0x04001A19 RID: 6681
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DirectoryObject _manager;

		// Token: 0x04001A1A RID: 6682
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _directReports = new Collection<DirectoryObject>();

		// Token: 0x04001A1B RID: 6683
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _members = new Collection<DirectoryObject>();

		// Token: 0x04001A1C RID: 6684
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _memberOf = new Collection<DirectoryObject>();

		// Token: 0x04001A1D RID: 6685
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _owners = new Collection<DirectoryObject>();

		// Token: 0x04001A1E RID: 6686
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _ownedObjects = new Collection<DirectoryObject>();
	}
}
