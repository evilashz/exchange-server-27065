using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005D6 RID: 1494
	[DataServiceKey("objectId")]
	public class DirectoryObject
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x0002F664 File Offset: 0x0002D864
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectoryObject CreateDirectoryObject(string objectId)
		{
			return new DirectoryObject
			{
				objectId = objectId
			};
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0002F67F File Offset: 0x0002D87F
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x0002F687 File Offset: 0x0002D887
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

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0002F690 File Offset: 0x0002D890
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x0002F698 File Offset: 0x0002D898
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

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0002F6A1 File Offset: 0x0002D8A1
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x0002F6A9 File Offset: 0x0002D8A9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? softDeletionTimestamp
		{
			get
			{
				return this._softDeletionTimestamp;
			}
			set
			{
				this._softDeletionTimestamp = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0002F6B2 File Offset: 0x0002D8B2
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x0002F6BA File Offset: 0x0002D8BA
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

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0002F6C3 File Offset: 0x0002D8C3
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x0002F6CB File Offset: 0x0002D8CB
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

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0002F6D7 File Offset: 0x0002D8D7
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0002F6DF File Offset: 0x0002D8DF
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

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0002F6E8 File Offset: 0x0002D8E8
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
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

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x0002F704 File Offset: 0x0002D904
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

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0002F710 File Offset: 0x0002D910
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x0002F718 File Offset: 0x0002D918
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

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0002F724 File Offset: 0x0002D924
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x0002F72C File Offset: 0x0002D92C
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

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0002F738 File Offset: 0x0002D938
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x0002F740 File Offset: 0x0002D940
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

		// Token: 0x04001B01 RID: 6913
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectType;

		// Token: 0x04001B02 RID: 6914
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001B03 RID: 6915
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _softDeletionTimestamp;

		// Token: 0x04001B04 RID: 6916
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DirectoryObject _createdOnBehalfOf;

		// Token: 0x04001B05 RID: 6917
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _createdObjects = new Collection<DirectoryObject>();

		// Token: 0x04001B06 RID: 6918
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DirectoryObject _manager;

		// Token: 0x04001B07 RID: 6919
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _directReports = new Collection<DirectoryObject>();

		// Token: 0x04001B08 RID: 6920
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _members = new Collection<DirectoryObject>();

		// Token: 0x04001B09 RID: 6921
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _memberOf = new Collection<DirectoryObject>();

		// Token: 0x04001B0A RID: 6922
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _owners = new Collection<DirectoryObject>();

		// Token: 0x04001B0B RID: 6923
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _ownedObjects = new Collection<DirectoryObject>();
	}
}
